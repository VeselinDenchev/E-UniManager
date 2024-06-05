using EUniManager.Application.Extensions;
using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.Students.Dtos;
using EUniManager.Application.Models.Students.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static EUniManager.Application.Constants.SemesterConstant;

namespace EUniManager.Application.Services;

public sealed class StudentService
    : BaseService<Student, Guid, StudentDto, StudentDetailsDto>, IStudentService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly StudentMapper _studentMapper = new();
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    
    public StudentService(IEUniManagerDbContext dbContext,
                          IHttpContextAccessor httpContextAccessor,
                          UserManager<IdentityUser<Guid>> userManager)
        : base(dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public override async Task<List<StudentDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Student> studentEntities = await _dbSet.Include(st => st.Specialty)
                                                        .ThenInclude(sp => sp.Faculty)
                                                    .ToListAsync(cancellationToken);
        List<StudentDto> studentDtos = _studentMapper.Map(studentEntities);

        return studentDtos;
    }

    public override async ValueTask<StudentDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Student studentEntity = await _dbSet.AsNoTracking()
                                            .Include(st => st.Specialty)
                                                .ThenInclude(sp => sp.Faculty)
                                            .Include(s => s.PersonalData.IdCard)
                                            .Include(s => s.DiplomaOwned)
                                            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken) ??
                                throw new ArgumentException($"Such {nameof(Student).ToLowerInvariant()} doesn't exist!");

        StudentDetailsDto studentDto = _studentMapper.MapStudentToStudentDetailsDto(studentEntity);

        return studentDto;
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        var createStudentDto = (dto as CreateStudentDto)!;

        Student student = _studentMapper.Map(createStudentDto);

        student.Specialty = await _dbContext.Specialties.FindAsync(createStudentDto.SpecialtyId, cancellationToken) ??
                            throw new ArgumentException($"Such {nameof(Specialty).ToLowerInvariant()} doesn't exist");
        
        student.User = new IdentityUser<Guid>
        {
            Email = createStudentDto.PersonalData.Email,
            UserName = createStudentDto.ServiceData.Pin.ToString(),
            PhoneNumber = createStudentDto.PermanentResidence.PhoneNumber,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        // Initial password for student is personal unique identifier
        string password = createStudentDto.PersonalData.UniqueIdentifier.Identifier;
        IdentityResult result = await _userManager.CreateAsync(student.User, password);
        result.ThrowExceptionIfFailed();
        
        result = await _userManager.AddToRoleAsync(student.User, nameof(UserRole.Student));
        if (!result.Succeeded)
        {
            await _userManager.DeleteAsync(student.User);

            result.ThrowExceptionIfFailed();
        }
        
        try
        {
            if (student.PersonalData.IdCard is not null)
            {
                await _dbContext.IdCards.AddAsync(student.PersonalData.IdCard, cancellationToken);
            }

            await _dbContext.Diplomas.AddAsync(student.DiplomaOwned, cancellationToken);

            await CreateEntityAsync(student, cancellationToken);
        }
        catch (Exception)
        {
            _dbContext.ChangeTracker.Clear();
            await _userManager.DeleteAsync(student.User);
            
            throw;
        }

    }

    // Non-updatable
    public override Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    // Non-deletable
    public override Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<StudentHeaderDto> GetHeaderDataAsync(Guid id, CancellationToken cancellationToken)
    {
        Guid studentId = await GetStudentIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
        
        Student student = await _dbSet.AsNoTracking()
                                         .Include(s => s.Specialty)
                                         .Include(s => s.CertifiedSemesters)
                                         .FirstOrDefaultAsync(s => s.Id == studentId, cancellationToken) ??
                          throw new ArgumentException($"Such {nameof(Student).ToLowerInvariant()} doesn't exist!");
        
        StudentHeaderDto studentHeaderDto = _studentMapper.MapStudentToStudentHeaderDto(student);

        return studentHeaderDto;
    }

    public async Task CertifySemesterAsync(Guid id, byte semester, CancellationToken cancellationToken)
    {
        // Change with fluent validator
        if (semester is < MIN_SEMESTER or > MAX_SEMESTER)
        {
            throw new ArgumentException("Semester is out of range!");
        }
        
        Student student = await _dbSet.FindAsync(id, cancellationToken) ??
                          throw new ArgumentException($"Such {nameof(Student)} doesn't exist!");
        
        CertifiedSemester certifiedSemester = new()
        {
            Student = student,
            Semester = semester
        };

        bool isAlreadyCertified = _dbContext.CertifiedSemesters.Contains(certifiedSemester);
        if (isAlreadyCertified) throw new ArgumentException("Not eligible for certification!");

        List<Subject> currentSemesterSubjectsWithStudents = 
            await _dbContext.Subjects.Include(s => s.Students)
                                     .Include(s => s.Marks).ThenInclude(m => m.Student)
                                     .Where(sub => sub.Students.Contains(student) &&
                                                   sub.Marks.Any(m => m.Student == student) &&
                                                   student.ServiceData.Status == StudentStatus.Studying &&
                                                   sub.Semester == semester)
                                     .ToListAsync(cancellationToken);
        
        bool isEligibleForCertification = currentSemesterSubjectsWithStudents.Any() &&
                                          currentSemesterSubjectsWithStudents.All(s => 
                                              s.Marks.FirstOrDefault(m => m.Student == student)?.Mark > Mark.Poor);

        if (!isEligibleForCertification) throw new ArgumentException("Not eligible for certification!");

        _dbContext.CertifiedSemesters.Add(certifiedSemester);
        student.CertifiedSemesters.Add(certifiedSemester);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateSpecialtyAsync(Guid studentId, Guid specialtyId, CancellationToken cancellationToken)
    {
        Specialty specialty = await _dbContext.Specialties.FindAsync(specialtyId, cancellationToken) ??
                              throw new ArgumentException($"Such {nameof(Specialty)} doesn't exist!");

        Student student = await _dbSet.Include(s => s.Specialty)
                                      .FirstOrDefaultAsync(s => s.Id == studentId, cancellationToken) ??
                          throw new ArgumentException($"Such {nameof(Student)} doesn't exist!");

        student.Specialty = specialty;

        _dbSet.Update(student);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateStatusAsync(Guid id, StudentStatus status, CancellationToken cancellationToken)
    {
        int rowsAffected = await _dbSet.Where(s => s.Id == id)
                                       .ExecuteUpdateAsync(s => s.SetProperty(st => st.ServiceData.Status, status),
                                                           cancellationToken);

        if (rowsAffected > 0) return;

        throw new ArgumentException($"Such {nameof(Student).ToLowerInvariant()} doesn't exist!");
    }

    public async Task UpdateGroupNumberAsync(Guid id, byte groupNumber, CancellationToken cancellationToken)
    {
        int rowsAffected = await _dbSet.Where(s => s.Id == id)
                                       .ExecuteUpdateAsync(s => s.SetProperty(st => st.ServiceData.GroupNumber, groupNumber),
                                                           cancellationToken);

        if (rowsAffected > 0) return;

        throw new ArgumentException($"Such {nameof(Student).ToLowerInvariant()} doesn't exist!");
    }

    public async Task UpdateEnrolledInSemesterAsync(Guid id, byte semester, CancellationToken cancellationToken)
    {
        int rowsAffected = await _dbSet.Where(s => s.Id == id)
                                       .ExecuteUpdateAsync(s => s.SetProperty(st => st.ServiceData.EnrolledInSemester, semester),
                                                           cancellationToken);

        if (rowsAffected > 0) return;

        throw new ArgumentException($"Such {nameof(Student).ToLowerInvariant()} doesn't exist!");
    }

    public async Task UpdateEmailAsync(Guid id, string email, CancellationToken cancellationToken)
    {
        var user =  await _dbSet.Include(s => s.User)
                                .Where(s => s.Id == id)
                                .Select(s => s.User)
                                .FirstOrDefaultAsync(cancellationToken) ??
                    throw new ArgumentException($"Such {nameof(Student).ToLowerInvariant()} doesn't exist!");

        string? oldEmail = user.Email;
        user.Email = email;
        
        IdentityResult result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded) throw new ArgumentException("Unable to update email!");

        try
        {
            await _dbSet.Where(s => s.Id == id)
                        .ExecuteUpdateAsync(s => s.SetProperty(st => st.PersonalData.Email, email), cancellationToken);
        }
        catch (Exception)
        {
            user.Email = oldEmail;
            await _userManager.UpdateAsync(user);
            
            throw;
        }
    }
}