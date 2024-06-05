using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.RequestApplications.Dtos;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.RequestApplications.Interfaces;

public interface IRequestApplicationService
    : IBaseService<RequestApplication, Guid, RequestApplicationDto, IDetailsDto>
{
    Task<List<RequestApplicationDto>> GetAllForStudentAsync(CancellationToken cancellationToken);
}