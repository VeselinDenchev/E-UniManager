using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Teachers.Dtos;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.Teachers.Interfaces;

public interface ITeacherService : IBaseService<Teacher, Guid, TeacherDto, TeacherDetailsDto>
{
}