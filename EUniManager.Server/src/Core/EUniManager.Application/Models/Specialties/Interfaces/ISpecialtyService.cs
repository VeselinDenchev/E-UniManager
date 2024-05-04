using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Specialties.Dtos;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.Specialties.Interfaces;

public interface ISpecialtyService : IBaseService<Specialty, Guid, SpecialtyDto, SpecialtyDetailsDto>;