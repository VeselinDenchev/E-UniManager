using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.CourseSchedules.Dtos;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.CourseSchedules.Interfaces;

public interface ICourseScheduleService 
    : IBaseService<CourseSchedule, Guid, CourseScheduleDto, CourseScheduleDetailsDto>
{
    
}