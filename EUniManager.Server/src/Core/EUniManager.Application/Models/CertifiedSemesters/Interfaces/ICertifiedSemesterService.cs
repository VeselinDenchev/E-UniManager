using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.CertifiedSemesters.Dtos;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.CertifiedSemesters.Interfaces;

public interface ICertifiedSemesterService : IBaseService<CertifiedSemester, Guid, IEntityDto, IDetailsDto>
{
    Task<List<StudentCertifiedSemesterDto>> GetAllForStudentAsync(CancellationToken cancellationToken);
}