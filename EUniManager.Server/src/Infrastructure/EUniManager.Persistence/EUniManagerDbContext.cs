using System.Reflection;

using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;

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
    
    public DbSet<Diploma> Diplomas { get; set; }
    
    public DbSet<IdCard> IdCards { get; set; }
    
    public DbSet<Assignment> Assignments { get; set; }
    
    public DbSet<AssignmentSolution> AssignmentsSolutions{ get; set; }
    
    public DbSet<CloudinaryFile> CloudinaryFiles { get; set; }
    
    public DbSet<Course> Courses { get; set; }
    
    public DbSet<CourseSchedule> CourseSchedules { get; set; }
    
    public DbSet<CourseScheduleUnit> CourseScheduleUnits { get; set; }
    
    public DbSet<Exam> Exams { get; set; }
    
    public DbSet<Faculty> Faculties { get; set; }
    
    public DbSet<IndividualProtocol> IndividualProtocols { get; set; }
    
    public DbSet<PayedTax> PayedTaxes { get; set; }
    
    public DbSet<RequestApplication> RequestApplications { get; set; }
    
    public DbSet<Resource> Resources { get; set; }
    
    public DbSet<Specialty> Specialties { get; set; }
    
    public DbSet<Subject> Subjects { get; set; }
    
    public DbSet<SubjectResourcesUnit> SubjectResourcesUnits { get; set; }

    public override int SaveChanges()
    {
        SequentialGuidValueGenerator idGenerator = new();
        var entries = ChangeTracker.Entries<BaseEntity<Guid>>();
        foreach (var entry in entries)
        {
            if (entry.State != EntityState.Added && entry.State != EntityState.Modified) continue;
            
            entry.Entity.ModifiedAt = DateTime.Now;
            
            if (entry.State != EntityState.Added) continue;

            entry.Entity.Id = idGenerator.Next(entry);
            entry.Entity.CreatedAt = DateTime.Now;
        }

        return base.SaveChanges();
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SequentialGuidValueGenerator idGenerator = new();
        var entries = ChangeTracker.Entries<BaseEntity<Guid>>();
        foreach (var entry in entries)
        {
            if (entry.State != EntityState.Added && entry.State != EntityState.Modified) continue;
            
            DateTime now = DateTime.Now;
            
            entry.Entity.ModifiedAt = now;
            
            if (entry.State != EntityState.Added) continue;

            entry.Entity.Id = await idGenerator.NextAsync(entry, cancellationToken);
            entry.Entity.CreatedAt = now;
        }

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
        
        const string DATABASE_NAME_ENV_VARIABLE_NAME = "DATABASE_NAME";
        const string SA_PASSWORD_ENV_VARIABLE_NAME = "SA_PASSWORD";

        string canNotLoadEnvVariableMessage =
            string.Format(CAN_NOT_LOAD_ENV_VARIABLE_MESSAGE_TEMPLATE, DATABASE_NAME_ENV_VARIABLE_NAME);
        string databaseName = Environment.GetEnvironmentVariable(DATABASE_NAME_ENV_VARIABLE_NAME) ?? 
                              throw new ArgumentNullException(canNotLoadEnvVariableMessage);

        canNotLoadEnvVariableMessage =
            canNotLoadEnvVariableMessage.Replace(DATABASE_NAME_ENV_VARIABLE_NAME, SA_PASSWORD_ENV_VARIABLE_NAME);
        string saPassword = Environment.GetEnvironmentVariable(SA_PASSWORD_ENV_VARIABLE_NAME) ?? 
                            throw new ArgumentNullException(canNotLoadEnvVariableMessage);
        
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