using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.PayedTaxes.Dtos;
using EUniManager.Application.Models.PayedTaxes.Interfaces;
using EUniManager.Domain.Entities;

using Microsoft.AspNetCore.Http.HttpResults;

using static EUniManager.Api.Constants.PoliciesConstant;
using static EUniManager.Api.Constants.RoutesConstant;

namespace EUniManager.Api.CarterModules;

public sealed class PayedTaxModule()
    : CrudCarterModule<
        IPayedTaxService,
        PayedTax,
        PayedTaxDto,
        PayedTaxDetailsDto,
        CreatePayedTaxDto,
        UpdatePayedTaxDto>
        (string.Format(BASE_ROUTE_TEMPLATE, PAYED_TAXES_ROUTE))
{
    private const string GET_ALL_FOR_STUDENT_ROUTE = "/students";
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        base.AddRoutes(app);
        
        app.MapGet(GET_ALL_FOR_STUDENT_ROUTE, GetAllForStudent).RequireAuthorization(STUDENT_POLICY_NAME);
    }

    private async Task<Results<Ok<List<PayedTaxDto>>, BadRequest, NotFound>> GetAllForStudent
    (
        IPayedTaxService payedTaxService,
        CancellationToken cancellationToken
    )
    {
        var payedTaxes = await payedTaxService.GetAllForStudentAsync(cancellationToken);

        return TypedResults.Ok(payedTaxes);
    }
}