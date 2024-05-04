using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.PayedTaxes.Dtos;
using EUniManager.Domain.Entities;

namespace EUniManager.Application.Models.PayedTaxes.Interfaces;

public interface IPayedTaxService : IBaseService<PayedTax, Guid, PayedTaxDto, PayedTaxDetailsDto>;