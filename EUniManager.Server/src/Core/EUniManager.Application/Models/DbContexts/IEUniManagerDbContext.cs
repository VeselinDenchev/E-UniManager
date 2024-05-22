using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EUniManager.Application.Models.DbContexts;

public interface IEUniManagerDbContext
{
    public DbSet<Student> Students { get; set; }
    
    public DbSet<Teacher> Teachers { get; set; }

    public DbSet<Activity> Activities { get; set; }
    
    public DbSet<Diploma> Diplomas { get; set; }
    
    public DbSet<IdCard> IdCards { get; set; }
    
    public DbSet<Assignment> Assignments { get; set; }
    
    public DbSet<AssignmentSolution> AssignmentsSolutions{ get; set; }
    
    public DbSet<CloudinaryFile> CloudinaryFiles { get; set; }
    
    public DbSet<Course> Courses { get; set; }
    
    public DbSet<CourseScheduleUnit> CourseScheduleUnits { get; set; }
    
    public DbSet<Exam> Exams { get; set; }
    
    public DbSet<Faculty> Faculties { get; set; }
    
    public DbSet<IndividualProtocol> IndividualProtocols { get; set; }
    
    public DbSet<PayedTax> PayedTaxes { get; set; }
    
    public DbSet<RequestApplication> RequestApplications { get; set; }
    
    public DbSet<Resource> Resources { get; set; }
    
    public DbSet<Specialty> Specialties { get; set; }
    
    public DbSet<Subject> Subjects { get; set; }
    
    public DbSet<CertifiedSemester> CertifiedSemesters { get; set; }
    
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    
    int SaveChanges();
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}