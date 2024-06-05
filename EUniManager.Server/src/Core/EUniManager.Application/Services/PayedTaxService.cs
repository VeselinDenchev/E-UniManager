using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.PayedTaxes.Dtos;
using EUniManager.Application.Models.PayedTaxes.Interfaces;
using EUniManager.Application.Services.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Application.Services;

public sealed class PayedTaxService 
    : BaseService<PayedTax, Guid, PayedTaxDto, PayedTaxDetailsDto>, IPayedTaxService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly PayedTaxMapper _payedTaxMapper = new();
    
    public PayedTaxService(IEUniManagerDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        : base(dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<List<PayedTaxDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<PayedTax> payedTaxEntities = await _dbSet.Include(pt => pt.Student)
                                                      .OrderBy(pt => pt.DocumentDate)
                                                        .ThenBy(dt => dt.Student.ServiceData.FacultyNumber)
                                                      .ToListAsync(cancellationToken);
        
        List<PayedTaxDto> payedTaxDtos = _payedTaxMapper.Map(payedTaxEntities);

        return payedTaxDtos;
    }

    public override async ValueTask<PayedTaxDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        PayedTax payedTaxEntity = await _dbSet.Include(pt => pt.Student)
                                              .FirstOrDefaultAsync(pt => pt.Id == id, cancellationToken) ??
                                  throw new ArgumentException("Such payed tax doesn't exist!");

        PayedTaxDetailsDto payedTaxDto = _payedTaxMapper.Map(payedTaxEntity);

        return payedTaxDto;
    }

    public override async Task CreateAsync(ICreateDto dto, CancellationToken cancellationToken)
    {
        var createPayedTaxDto = (dto as CreatePayedTaxDto)!;
        
        PayedTax payedTax = _payedTaxMapper.Map(createPayedTaxDto);
        payedTax.Student = await _dbContext.Students.FindAsync(createPayedTaxDto.StudentId, cancellationToken) ??
                           throw new ArgumentNullException($"Such {nameof(Student).ToLowerInvariant()} doesn't exist!");
        
        await CreateEntityAsync(payedTax, cancellationToken);
    }

    public override async Task UpdateAsync(Guid id, IUpdateDto dto, CancellationToken cancellationToken)
    {
        PayedTax existingPayedTax = await _dbSet.AsNoTracking()
                                                .Include(pt => pt.Student)
                                                .FirstOrDefaultAsync(pt => pt.Id == id, cancellationToken) ??
                                    throw new ArgumentException("Such payed tax doesn't exist");          
        
        PayedTax updatedPayedTax = _payedTaxMapper.Map((dto as UpdatePayedTaxDto)!);
        updatedPayedTax.Id = id;
        updatedPayedTax.Student = await _dbSet.AsNoTracking()
                                              .Include(e => e.Student)
                                              .Where(e => e.Id == id)
                                              .Select(e => e.Student)
                                              .FirstAsync(cancellationToken);
        updatedPayedTax.TaxNumber = existingPayedTax.TaxNumber;
        updatedPayedTax.Semester = existingPayedTax.Semester;
        updatedPayedTax.PlanNumber = existingPayedTax.PlanNumber;
        
        SetNotModifiedPropertiesOnUpdate(updatedPayedTax);

        await UpdateEntityAsync(updatedPayedTax, cancellationToken);
    }
    
    public async Task<List<PayedTaxDto>> GetAllForStudentAsync(CancellationToken cancellationToken)
    {
        Guid studentId = await GetStudentIdFromHttpContextAsync(_httpContextAccessor, cancellationToken);
        
        List<PayedTax> studentPayedTaxEntities = await _dbSet.AsNoTracking()
                                                             .Include(pt => pt.Student)
                                                             .Where(pt => pt.Student.Id == studentId)
                                                             .OrderBy(pt => pt.DocumentDate)
                                                             .ToListAsync(cancellationToken);

        List<PayedTaxDto> studentPayedTaxDtos = _payedTaxMapper.Map(studentPayedTaxEntities);

        return studentPayedTaxDtos;
    }
}