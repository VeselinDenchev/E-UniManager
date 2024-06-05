using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.IndividualProtocols.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

namespace EUniManager.Application.Models.IndividualProtocols.Interfaces;

public interface IIndividualProtocolService
    : IBaseService<IndividualProtocol, Guid, IndividualProtocolDto, IndividualProtocolDetailsDto>
{
    Task<List<IndividualProtocolDto>> GetAllForStudentAsync(CancellationToken cancellationToken);
    
    Task UpdateStatusAsync(Guid id, IndividualProtocolStatus status, CancellationToken cancellationToken);
}