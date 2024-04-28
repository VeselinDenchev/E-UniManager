using System.Reflection;

using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Assignments;
using EUniManager.Domain.Entities.Courses;
using EUniManager.Domain.Entities.Students;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence;

public class EUniManagerDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
{
    public DbSet<Student> Students { get; set; }
    
    public DbSet<Teacher> Teachers { get; set; }
    
    public DbSet<Diploma> Diplomas { get; set; }
    
    public DbSet<IdCard> IdCards { get; set; }
    
    public DbSet<Assignment> Assignments { get; set; }
    
    public DbSet<AssignmentSolution> AssignmentsSolutions{ get; set; }
    
    public DbSet<CloudinaryFile> CloudinaryFiles { get; set; }
    
    public DbSet<Course> Courses { get; set; }
    
    public DbSet<CourseSchedule> CourseSchedules { get; set; }
    
    public DbSet<CourseScheduleUnit> CourseScheduleUnits { get; set; }

    public DbSet<CurriculumSubjectResourcesUnit> CurriculumSubjectResourcesUnits { get; set; }
    
    public DbSet<Exam> Exams { get; set; }
    
    public DbSet<Faculty> Faculties { get; set; }
    
    public DbSet<IndividualProtocol> IndividualProtocols { get; set; }
    
    public DbSet<PayedTax> PayedTaxes { get; set; }
    
    public DbSet<RequestApplication> RequestApplications { get; set; }
    
    public DbSet<Resource> Resources { get; set; }
    
    public DbSet<Specialty> Specialties { get; set; }
    
    public DbSet<Subject> Subjects { get; set; }

    public override int SaveChanges()
    {
        var entities = ChangeTracker.Entries<BaseEntity<string>>();
        foreach (var entity in entities)
        {
            if (entity.State != EntityState.Added && entity.State != EntityState.Modified) continue;
            
            entity.Entity.ModifiedAt = DateTime.Now;
            
            if (entity.State != EntityState.Added) continue;
            
            entity.Entity.CreatedAt = DateTime.Now;
        }

        return base.SaveChanges();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string[] commandLineArgs = Environment.GetCommandLineArgs();
        bool isMigrationCommand = commandLineArgs.Any(a => a.Contains("migrations") || a.Contains("database"));
        if (isMigrationCommand)
        {
            optionsBuilder.UseSqlServer();

            return;
        }

        const string CAN_NOT_LOAD_ENV_VARIABLE_MESSAGE_TEMPLATE = "Can't load environmental variable {0}!";
        const string DATABASE_NAME_ENV_VARIABLE_NAME = "DATABASE_NAME";
        const string SA_PASSWORD_ENV_VARIABLE_NAME = "SA_PASSWORD";

        string canNotLoadDatabaseNameMessage =
            string.Format(CAN_NOT_LOAD_ENV_VARIABLE_MESSAGE_TEMPLATE, DATABASE_NAME_ENV_VARIABLE_NAME);
        string databaseName = Environment.GetEnvironmentVariable(DATABASE_NAME_ENV_VARIABLE_NAME) ?? 
                              throw new ArgumentNullException(canNotLoadDatabaseNameMessage);
        
        string canNotLoadSaPasswordMessage =
            string.Format(CAN_NOT_LOAD_ENV_VARIABLE_MESSAGE_TEMPLATE, SA_PASSWORD_ENV_VARIABLE_NAME);
        string saPassword = Environment.GetEnvironmentVariable(SA_PASSWORD_ENV_VARIABLE_NAME) ?? 
                            throw new ArgumentNullException(canNotLoadSaPasswordMessage);
        
        string connectionString = $"Data Source=sqlserver;Initial Catalog={databaseName};TrustServerCertificate=True;User ID=SA;Password={saPassword}";
        
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        Assembly persistenceAssembly = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(persistenceAssembly);
    }
}