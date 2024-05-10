using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Abstraction.Student;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Domain.Entities.Students;

public class Student : BaseEntity<Guid>
{
    public IdentityUser<Guid> User { get; set; } = null!;
    
    public Faculty Faculty { get; set; } = null!;
    
    public ServiceData ServiceData { get; set; } = null!;

    public PersonalData PersonalData { get; set; } = null!;

    public Residence PermanentResidence { get; set; } = null!;

    public Residence TemporaryResidence { get; set; } = null!;

    public string UsualResidenceCountry { get; set; } = null!;

    public Enrollment Enrollment { get; set; } = null!;

    public Diploma DiplomaOwned { get; set; } = null!;

    public Specialty Specialty { get; set; } = null!;

    public List<Assignment> Assignments { get; set; } = null!;
    
    public List<AssignmentSolution> AssignmentSolutions { get; set; } = null!;
    
    public List<Subject> Subjects { get; set; } = null!;

    public List<PayedTax> PayedTaxes { get; set; } = null!;

    public List<RequestApplication> RequestApplications { get; set; } = null!;
}