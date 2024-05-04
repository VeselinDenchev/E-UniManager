using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.IndividualProtocols.Dtos;
using EUniManager.Application.Models.IndividualProtocols.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public sealed class IndividualProtocolModule()
    : CrudCarterModule<
        IIndividualProtocolService,
        IndividualProtocol,
        IndividualProtocolDto,
        IndividualProtocolDetailsDto,
        CreateIndividualProtocolDto,
        UpdateIndividualProtocolDto>
        (string.Format(BASE_PATH_TEMPLATE, "individual-protocol"));