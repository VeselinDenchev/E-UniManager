using System.Globalization;

using EUniManager.Application.Extensions;
using EUniManager.Application.Models.CourseScheduleUnits.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class CourseScheduleUnitMapper
{
    [UserMapping]
    public List<StudentCourseScheduleUnitDto> MapCourseScheduleUnitsToStudentSchedule(List<CourseScheduleUnit> entities)
    {
        return entities.Select(csu => new StudentCourseScheduleUnitDto
        {
            Id = csu.Id,
            Day = csu.DayOfWeek.HasValue 
                        ? GetDayOfWeekString(csu.DayOfWeek.Value)
                        : csu.ExactDate?.ToBulgarianDateFormatString() ?? 
                          throw new ArgumentException("Day is missing!"),
            Timespan = $"{csu.StartTime} - {csu.EndTime}",
            Week = GetMonthlyFrequencyString(csu.MonthlyFrequency),
            Group = GetGroupString(csu.GroupNumber, csu.GroupType),
            RoomNumber = csu.RoomNumber,
            SchedulePlace = GetSchedulePlaceString(csu.Place),
            ActivityId = csu.Activity.Id,
            ActivityType = GetActivityTypeString(csu.Activity.Type),
            ActivitySubjectCourseName = csu.Activity.Subject.Course.Name,
            ActivityTeacherFullNameWithRank = csu.Activity.Teacher.FullNameWithRank
        }).ToList();
    }
    
    [UserMapping]
    public List<TeacherCourseScheduleUnitDto> MapCourseScheduleUnitsToTeacherSchedule(List<CourseScheduleUnit> entities)
    {
            return entities.Select(csu => new TeacherCourseScheduleUnitDto
            {
                Id = csu.Id,
                Day = (csu.DayOfWeek.HasValue 
                            ? GetDayOfWeekString(csu.DayOfWeek.Value)
                            : csu.ExactDate?.ToBulgarianDateFormatString() ?? 
                      throw new ArgumentException("Day is missing!"))!,
                Timespan = $"{csu.StartTime} - {csu.EndTime}",
                Week = GetMonthlyFrequencyString(csu.MonthlyFrequency),
                SchedulePlace = GetSchedulePlaceString(csu.Place),
                RoomNumber = csu.RoomNumber,
                SpecialtyCurrentYear = csu.Activity.Subject.Specialty.CurrentYear,
                GroupNumber = csu.GroupNumber,
                GroupType = GetGroupTypeString(csu.GroupType),
                ActivitySubjectCourseName = csu.Activity.Subject.Course.Name,
                ActivityType = GetActivityTypeString(csu.Activity.Type),
                SpecialtyEducationType = GetSpecialtyEducationTypeString(csu.Activity.Subject.Specialty.EducationType),
                SemesterType = GetSemesterTypeString(csu.SemesterType),
                ActivitySubjectControlType = GetSubjectControlTypeString(csu.Activity.Subject.ControlType)
            }).ToList();
    }
    
    [UserMapping]
    public CourseScheduleUnitDetailsDto Map(CourseScheduleUnit courseScheduleUnit)
    {
        return new CourseScheduleUnitDetailsDto
        {
            DayOfWeek = GetDayOfWeekString(courseScheduleUnit.DayOfWeek),
            ExactDate = courseScheduleUnit.ExactDate?.ToString("dd.MM.yyyy г.", CultureInfo.GetCultureInfo("bg-BG")),
            StartTime = courseScheduleUnit.StartTime.ToShortTimeString(),
            EndTime = courseScheduleUnit.EndTime.ToShortTimeString(),
            ScheduleMonthlyFrequency = GetMonthlyFrequencyString(courseScheduleUnit.MonthlyFrequency),
            GroupNumber = courseScheduleUnit.GroupNumber,
            SubjectGroupType = GetGroupTypeString(courseScheduleUnit.GroupType),
            RoomNumber = courseScheduleUnit.RoomNumber,
            SchedulePlace = GetSchedulePlaceString(courseScheduleUnit.Place),
            ActivityType = GetActivityTypeString(courseScheduleUnit.Activity.Type),
            ActivitySubjectCourseName = courseScheduleUnit.Activity.Subject.Course.Name,
            ActivityTeacherFullNameWithRank = courseScheduleUnit.Activity.Teacher.FullNameWithRank,
            Semester = courseScheduleUnit.Activity.Subject.Semester,
            SemesterType = GetSemesterTypeString(courseScheduleUnit.SemesterType)
        };
    }
    
    [MapperIgnoreTarget(nameof(CourseScheduleUnit.Id))]
    [MapperIgnoreTarget(nameof(CourseScheduleUnit.CreatedAt))]
    [MapperIgnoreTarget(nameof(CourseScheduleUnit.ModifiedAt))]
    [MapProperty(nameof(CreateCourseScheduleUnitDto.ScheduleMonthlyFrequency), nameof(CourseScheduleUnit.MonthlyFrequency))]
    [MapProperty(nameof(CreateCourseScheduleUnitDto.GroupType), nameof(CourseScheduleUnit.GroupType))]
    [MapProperty(nameof(CreateCourseScheduleUnitDto.SchedulePlace), nameof(CourseScheduleUnit.Place))]
    public partial CourseScheduleUnit Map(CreateCourseScheduleUnitDto dto);
    
    [MapperIgnoreTarget(nameof(CourseScheduleUnit.Id))]
    [MapperIgnoreTarget(nameof(CourseScheduleUnit.CreatedAt))]
    [MapperIgnoreTarget(nameof(CourseScheduleUnit.ModifiedAt))]
    [MapProperty(nameof(UpdateCourseScheduleUnitDto.GroupType), nameof(CourseScheduleUnit.GroupType))]
    [MapProperty(nameof(UpdateCourseScheduleUnitDto.SchedulePlace),nameof(CourseScheduleUnit.Place))]
    public partial CourseScheduleUnit Map(UpdateCourseScheduleUnitDto dto);

    private string? GetDayOfWeekString(DayOfWeek? monthlyFrequency)
    {
        return monthlyFrequency switch
        {
            DayOfWeek.Monday => "Понеделник",
            DayOfWeek.Tuesday => "Вторник",
            DayOfWeek.Wednesday => "Сряда",
            DayOfWeek.Thursday => "Четвъртък",
            DayOfWeek.Friday => "Петък",
            DayOfWeek.Saturday => "Събота",
            DayOfWeek.Sunday => "Неделя",
            null => null,
            _ => throw new ArgumentException("Invalid day of the week!")
        };
    }
    
    private string GetMonthlyFrequencyString(ScheduleMonthlyFrequency? monthlyFrequency)
    {
        return monthlyFrequency switch
        {
            ScheduleMonthlyFrequency.Weekly => "всяка",
            ScheduleMonthlyFrequency.OddWeeks => "1, 3",
            ScheduleMonthlyFrequency.EvenWeeks => "2, 4",
            _ => string.Empty
        };
    }
    
    private string GetGroupString(byte? groupNumber, SubjectGroupType? groupType)
    {
        if (groupNumber is null && groupType is null) return string.Empty;
        
        return groupType switch
        {
            SubjectGroupType.Laboratory => $"{groupNumber} лаб",
            SubjectGroupType.Semester => $"{groupNumber} сем",
            _ => throw new ArgumentException("Invalid subject group type!")
        };
    }
    
    private string GetGroupTypeString(SubjectGroupType? groupType)
    {
        if (groupType is null) return string.Empty;
        
        return groupType switch
        {
            SubjectGroupType.Laboratory => "лаб",
            SubjectGroupType.Semester => "сем",
            _ => throw new ArgumentException("Invalid subject group type!")
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

    private string GetActivityTypeString(ActivityType activityType)
    {
        return activityType switch
        {
            ActivityType.Lecture => "Лекция",
            ActivityType.Exercise => "Упражнение",
            _ => throw new ArgumentException("Invalid activity type!")
        };
    }
    
    private string GetSpecialtyEducationTypeString(SpecialtyEducationType specialtyEducationType)
    {
        return specialtyEducationType switch
        {
            SpecialtyEducationType.FullTime => "Редовна",
            SpecialtyEducationType.PartTime => "Задочна",
            _ => throw new ArgumentException("Invalid subject control type!")
        };
    }
    
    private string GetSubjectControlTypeString(SubjectControlType subjectControlType)
    {
        return subjectControlType switch
        {
            SubjectControlType.Exam => "Изпит",
            SubjectControlType.SemesterAssessment => "Текуща оценка",
            _ => throw new ArgumentException("Invalid subject control type!")
        };
    }

    private string GetSemesterTypeString(SemesterType semesterType)
    {
        return semesterType switch
        {
            SemesterType.Winter => "Зимен",
            SemesterType.Summer => "Летен",
            _ => throw new ArgumentException("Invalid semester type!")
        };
    }
}