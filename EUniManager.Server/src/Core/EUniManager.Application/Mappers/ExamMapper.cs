using System.Globalization;

using EUniManager.Application.Models.Exams.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class ExamMapper
{
    [UserMapping]
    public List<ExamDto> MapExamList(List<Exam> entities)
    {
        return entities.Select(e => new ExamDto
        {
            Id = e.Id,
            SubjectCourseName = e.Subject.Course.Name,
            SpecialtyName = e.Subject.Specialty.Name,
            ExamType = GetExamTypeString(e.Type),
            Date = e.Date.ToString("dd.MM.yyyy г.", CultureInfo.GetCultureInfo("bg-BG")),
            Time = e.Time.ToShortTimeString(),
            SchedulePlace = GetSchedulePlaceString(e.Place),
            RoomNumber = e.RoomNumber,
            GroupNumber = e.GroupNumber
        }).ToList();
    }
    
    [MapProperty(nameof(@Exam.Subject.Course.Name), nameof(ExamDetailsDto.SubjectCourseName))]
    [MapProperty(nameof(Exam.Type), nameof(ExamDetailsDto.ExamType), Use = nameof(GetExamTypeString))]
    [MapProperty(nameof(Exam.Date), nameof(ExamDetailsDto.Date), StringFormat = "dd.MM.yyyy г.")]
    [MapProperty(nameof(Exam.Place), nameof(ExamDetailsDto.SchedulePlace))]
    public partial ExamDetailsDto Map(Exam entity);
    
    [MapperIgnoreTarget(nameof(Exam.Id))]
    [MapperIgnoreTarget(nameof(Exam.CreatedAt))]
    [MapperIgnoreTarget(nameof(Exam.ModifiedAt))]
    [MapProperty(nameof(CreateExamDto.DateTime), nameof(Exam.Date))]
    [MapProperty(nameof(CreateExamDto.DateTime), nameof(Exam.Time))]
    [MapProperty(nameof(CreateExamDto.ExamType), nameof(Exam.Type))]
    [MapProperty(nameof(CreateExamDto.SchedulePlace), nameof(Exam.Place))]
    public partial Exam Map(CreateExamDto dto);
    
    [MapperIgnoreTarget(nameof(Exam.Id))]
    [MapperIgnoreTarget(nameof(Exam.CreatedAt))]
    [MapperIgnoreTarget(nameof(Exam.ModifiedAt))]
    [MapProperty(nameof(UpdateExamDto.DateTime), nameof(Exam.Date))]
    [MapProperty(nameof(UpdateExamDto.DateTime), nameof(Exam.Time))]
    [MapProperty(nameof(UpdateExamDto.SchedulePlace), nameof(Exam.Place))]
    public partial Exam Map(UpdateExamDto dto);
    
    [UserMapping]
    public List<StudentExamDto> MapStudentExamList(List<Exam> entities)
    {
        return entities.Select(e => new StudentExamDto
        {
            Id = e.Id,
            SubjectCourseName = e.Subject.Course.Name,
            ExamType = GetExamTypeString(e.Type),
            Date = e.Date.ToString("dd.MM.yyyy г.", CultureInfo.GetCultureInfo("bg-BG")),
            Time = e.Time.ToShortTimeString(),
            SchedulePlace = GetSchedulePlaceString(e.Place),
            RoomNumber = e.RoomNumber,
            GroupNumber = e.GroupNumber,
            ExaminerFullName = e.Subject.Lecturer.FullName
        }).ToList();
    }

    private string GetExamTypeString(ExamType? examType)
    {
        return examType switch
        {
            ExamType.Regular => "Редовен",
            ExamType.Remedial => "Поправителен",
            ExamType.Liquidation => "Ликвидационен",
            ExamType.Oral => "Устен",
            ExamType.Written => "Писмен",
            null => string.Empty,
            _ => throw new ArgumentException("Invalid exam type!")
        };
    }
    
    private string GetSchedulePlaceString(SchedulePlace schedulePlace)
    {
        return schedulePlace switch
        {
            SchedulePlace.SportsHall => "Спортна зала",
            SchedulePlace.Corps1 => "Корпус 1",
            SchedulePlace.Corps2 => "Корпус 2",
            SchedulePlace.Corps3 => "Корпус 3",
            SchedulePlace.Corps4 => "Корпус 4",
            SchedulePlace.Corps5 => "Корпус 5",
            _ => throw new ArgumentException("Such schedule place doesn't exist!")
        };
    }
}