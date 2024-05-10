using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Activities.Dtos;
using EUniManager.Application.Models.Activities.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public sealed class ActivityModule()
    : CrudCarterModule<
            IActivityService, 
            Activity,
            ActivityDto, 
            ActivityDetailsDto, 
            CreateActivityDto, 
            UpdateActivityDto>
    (string.Format(BASE_PATH_TEMPLATE, nameof(Activity).ToLowerInvariant()));