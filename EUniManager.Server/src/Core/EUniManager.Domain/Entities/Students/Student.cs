using EUniManager.Domain.Abstraction.Base;

using Microsoft.AspNetCore.Identity;

namespace EUniManager.Domain.Entities.Students;

public class Student : BaseEntity<Guid>
{
    public IdentityUser<Guid> User { get; set; } = null!;
    
    public ServiceData ServiceData { get; set; } = new();

    public PersonalData PersonalData { get; set; } = new();

    public string FullName => $"{PersonalData.FirstName} {PersonalData.MiddleName} {PersonalData.LastName}";

    public Residence PermanentResidence { get; set; } = new();

    public Residence TemporaryResidence { get; set; } = new();

    public string UsualResidenceCountry { get; set; } = null!;

    public Enrollment Enrollment { get; set; } = new();

    public Diploma DiplomaOwned { get; set; } = null!;

    public Specialty Specialty { get; set; } = null!;

    public List<Assignment> Assignments { get; set; } = null!;
    
    public List<AssignmentSolution> AssignmentSolutions { get; set; } = null!;

    public List<IndividualProtocol> IndividualProtocols { get; set; } = null!;

    public List<Activity> Activities { get; set; } = null!;
    
    public List<Subject> Subjects { get; set; } = null!;

    public List<SubjectMark> SubjectMarks { get; set; } = null!;

    public List<PayedTax> PayedTaxes { get; set; } = null!;

    public List<RequestApplication> RequestApplications { get; set; } = null!;

    public List<CertifiedSemester> CertifiedSemesters { get; set; } = null!;
}