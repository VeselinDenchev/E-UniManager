using EUniManager.Domain.Abstraction.Base;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

namespace EUniManager.Domain.Entities;

public class Subject : BaseEntity<Guid>
{
    public byte Semester { get; set; }
    
    public Course Course { get; set; } = null!;

    public List<Student> Students { get; set; } = null!;

    public Teacher Lecturer { get; set; } = null!;

    public List<Teacher> Assistants { get; set; } = null!;

    public Specialty Specialty { get; set; } = null!;

    public List<Activity> Activities { get; set; } = null!;

    public List<Exam> Exams { get; set; } = null!;

    public List<SubjectMark> Marks { get; set; } = null!;
    
    public string Protocol { get; set; } = null!;

    public SubjectControlType ControlType { get; set; }
}