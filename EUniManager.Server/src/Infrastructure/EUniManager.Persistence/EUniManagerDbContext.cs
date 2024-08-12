using System.Reflection;

using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Persistence.Helpers;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

using static EUniManager.Application.Constants.EnviromentalVariablesConstant;

namespace EUniManager.Persistence;

public class EUniManagerDbContext 
    : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>, IEUniManagerDbContext
{
    public DbSet<Student> Students { get; set; }
    
    public DbSet<Teacher> Teachers { get; set; }

    public DbSet<Activity> Activities { get; set; }
    
    public DbSet<Diploma> Diplomas { get; set; }
    
    public DbSet<IdCard> IdCards { get; set; }
    
    public DbSet<Assignment> Assignments { get; set; }
    
    public DbSet<AssignmentSolution> AssignmentSolutions{ get; set; }
    
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
    
    public DbSet<SubjectMark> SubjectMarks { get; set; }

    public DbSet<CertifiedSemester> CertifiedSemesters { get; set; }

    public override int SaveChanges()
    {
        SetBaseEntityData();
        
        return base.SaveChanges();
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetBaseEntityData();

        return await base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string[] commandLineArgs = Environment.GetCommandLineArgs();
        bool isMigrationCommand = commandLineArgs.Any(a => a.Contains("migrations"));
        if (isMigrationCommand)
        {
            optionsBuilder.UseSqlServer();

            return;
        }
        
        optionsBuilder.UseSqlServer(ConnectionStringHelper.GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        Assembly persistenceAssembly = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(persistenceAssembly);
    }

    private void SetBaseEntityData()
    {
        var newGuidIdEntries = ChangeTracker.Entries<BaseEntity<Guid>>().ToList();
        var newStringIdEntries = ChangeTracker.Entries<BaseEntity<string>>().ToList();
        
        DateTime now = DateTime.Now;
        
        if (newGuidIdEntries.Any(e => e.State == EntityState.Added) || 
            newStringIdEntries.Any(e => e.State == EntityState.Added))
        {
            SequentialGuidValueGenerator idGenerator = new();
            
            foreach (var guidIdEntry in newGuidIdEntries.Where(e => e.State == EntityState.Added))
            {
                guidIdEntry.Entity.Id = idGenerator.Next(guidIdEntry);
                guidIdEntry.Entity.CreatedAt = now;
                guidIdEntry.Entity.ModifiedAt = now;
            }
            foreach (var stringIdEntry in newStringIdEntries.Where(e => e.State == EntityState.Added))
            {
                if (string.IsNullOrWhiteSpace(stringIdEntry.Entity.Id))
                {
                    stringIdEntry.Entity.Id = idGenerator.Next(stringIdEntry).ToString();
                }
                
                stringIdEntry.Entity.CreatedAt = now;
                stringIdEntry.Entity.ModifiedAt = now;
            }
        }
        
        foreach (var modifiedGuidIdEntry in newGuidIdEntries.Where(e => e.State == EntityState.Modified))
        {
            modifiedGuidIdEntry.Entity.ModifiedAt = now;
        }
        foreach (var modifiedStringIdEntry in newStringIdEntries.Where(e => e.State == EntityState.Modified))
        {
            modifiedStringIdEntry.Entity.ModifiedAt = now;
        }
    }
}