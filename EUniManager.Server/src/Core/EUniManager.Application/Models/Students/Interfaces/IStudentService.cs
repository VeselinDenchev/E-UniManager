using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Students.Dtos;
using EUniManager.Domain.Entities.Students;

namespace EUniManager.Application.Models.Students.Interfaces;

public interface IStudentService : IBaseService<Student, Guid, StudentDto, StudentDetailsDto>;