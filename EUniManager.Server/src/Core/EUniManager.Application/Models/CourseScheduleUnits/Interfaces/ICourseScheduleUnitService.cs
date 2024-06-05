using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.CourseScheduleUnits.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

namespace EUniManager.Application.Models.CourseScheduleUnits.Interfaces;

public interface ICourseScheduleUnitService 
    : IBaseService<CourseScheduleUnit, Guid, IEntityDto, CourseScheduleUnitDetailsDto>
{
     Task<List<StudentCourseScheduleUnitDto>> GetSpecialtyScheduleByStudentAsync(SemesterType requestedSemesterType,
                                                                                   CancellationToken cancellationToken);

    Task<List<StudentCourseScheduleUnitDto>> GetStudentScheduleAsync(SemesterType requestedSemesterType,
                                                                     CancellationToken cancellationToken);

    Task<List<TeacherCourseScheduleUnitDto>> GetTeacherScheduleAsync(SemesterType requestedSemesterType,
                                                                     CancellationToken cancellationToken);

    Task DeleteOldSchedulesAsync(CancellationToken cancellationToken);
}