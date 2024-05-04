using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.CourseScheduleUnits.Dtos;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.CourseScheduleUnits.Interfaces;

public interface ICourseScheduleUnitService 
    : IBaseService<CourseScheduleUnit, Guid, CourseScheduleUnitDto, CourseScheduleUnitDetailsDto>
{
    
}