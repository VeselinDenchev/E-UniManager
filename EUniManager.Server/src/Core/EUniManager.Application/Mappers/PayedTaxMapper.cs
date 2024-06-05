using System.Globalization;

using EUniManager.Application.Models.PayedTaxes.Dtos;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Riok.Mapperly.Abstractions;

namespace EUniManager.Application.Mappers;

[Mapper]
public partial class PayedTaxMapper
{
    public List<PayedTaxDto> Map(List<PayedTax> entities)
    {
        return entities.Select(pt => new PayedTaxDto
        {
            Id = pt.Id,
            FacultyNumber = pt.Student.ServiceData.FacultyNumber,
            TaxNumber = pt.TaxNumber,
            DocumentNumber = pt.DocumentNumber,
            DocumentDate = pt.DocumentDate.ToString("dd.MM.yyyy г.", CultureInfo.GetCultureInfo("bg-BG")),
            Semester = pt.Semester,
            PlanNumber = pt.PlanNumber,
            Amount = pt.Amount,
            Currency = GetCurrencyString(pt.Currency)
        }).ToList();
    }
    
    [MapProperty(nameof(@PayedTax.Student.ServiceData.FacultyNumber), nameof(PayedTaxDetailsDto.FacultyNumber))]
    [MapProperty(nameof(@PayedTax.Student.PersonalData.FirstName), nameof(PayedTaxDetailsDto.FirstName))]
    [MapProperty(nameof(@PayedTax.Student.PersonalData.MiddleName), nameof(PayedTaxDetailsDto.MiddleName))]
    [MapProperty(nameof(@PayedTax.Student.PersonalData.LastName), nameof(PayedTaxDetailsDto.LastName))]
    [MapProperty(nameof(PayedTax.DocumentDate), nameof(PayedTaxDetailsDto.DocumentDate), StringFormat = "dd.MM.yyyy г.")]
    public partial PayedTaxDetailsDto Map(PayedTax entity);
    
    [MapperIgnoreTarget(nameof(PayedTax.Id))]
    [MapperIgnoreTarget(nameof(PayedTax.CreatedAt))]
    [MapperIgnoreTarget(nameof(PayedTax.ModifiedAt))]
    [MapperIgnoreTarget(nameof(PayedTax.Student))]
    public partial PayedTax Map(CreatePayedTaxDto dto);
    
    [MapperIgnoreTarget(nameof(PayedTax.Id))]
    [MapperIgnoreTarget(nameof(PayedTax.CreatedAt))]
    [MapperIgnoreTarget(nameof(PayedTax.ModifiedAt))]
    public partial PayedTax Map(UpdatePayedTaxDto dto);

    private int GetStudentFacultyNumber(Student student) => student.ServiceData.FacultyNumber;

    private string GetDateString(DateOnly date) => date.ToString("dd.MM.yyyy г.");
    
    private string GetCurrencyString(Currency currency)
    {
        return currency switch
        {
            Currency.Bgn => "лева",
            Currency.Eur => "евро",
            Currency.Usd => "долара",
            Currency.Rub => "рубли",
            Currency.Jpy => "йени",
            Currency.Cny => "юана",
            _ => "Unknown currency!"
        };
    }
}