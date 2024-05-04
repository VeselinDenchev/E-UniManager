using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.PayedTaxes.Dtos;
using EUniManager.Application.Models.PayedTaxes.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public sealed class PayedTaxModule()
    : CrudCarterModule<
        IPayedTaxService, 
        PayedTax, 
        PayedTaxDto, 
        PayedTaxDetailsDto, 
        CreatePayedTaxDto, 
        UpdatePayedTaxDto>
        (string.Format(BASE_PATH_TEMPLATE, "payed-tax"));