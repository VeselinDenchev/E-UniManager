using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.Subjects.Dtos;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.Subjects.Interfaces;

public interface ISubjectService : IBaseService<Subject, Guid, SubjectDto, SubjectDetailsDto>;