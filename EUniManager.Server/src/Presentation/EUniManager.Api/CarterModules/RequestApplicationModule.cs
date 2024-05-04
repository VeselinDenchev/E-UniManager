using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.RequestApplications.Dtos;
using EUniManager.Application.Models.RequestApplications.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public sealed class RequestApplicationModule()
    : CrudCarterModule<
        IRequestApplicationService,
        RequestApplication,
        RequestApplicationDto,
        RequestApplicationDetailsDto,
        CreateRequestApplicationDto,
        UpdateRequestApplicationDto>
        (string.Format(BASE_PATH_TEMPLATE, "request-application"));