using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Courses.Dtos;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.Courses.Interfaces;

public interface ICourseService : IBaseService<Course, Guid, CourseDto, CourseDetailsDto>;