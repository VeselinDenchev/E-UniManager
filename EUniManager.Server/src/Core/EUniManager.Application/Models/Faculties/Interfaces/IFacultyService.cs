using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Faculties.Dtos;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.Faculties.Interfaces;

public interface IFacultyService : IBaseService<Faculty, Guid, FacultyDto, FacultyDetailsDto>;