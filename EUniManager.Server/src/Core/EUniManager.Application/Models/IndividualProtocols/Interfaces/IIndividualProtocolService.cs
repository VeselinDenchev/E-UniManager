using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.IndividualProtocols.Dtos;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.IndividualProtocols.Interfaces;

public interface IIndividualProtocolService 
    : IBaseService<IndividualProtocol, Guid, IndividualProtocolDto, IndividualProtocolDetailsDto>;