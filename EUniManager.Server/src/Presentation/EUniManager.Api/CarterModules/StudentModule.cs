using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Base.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Application.Models.Students.Dtos;
using EUniManager.Application.Models.Students.Interfaces;
using EUniManager.Domain.Entities.Students;
using EUniManager.Domain.Enums;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using static EUniManager.Api.Constants.PoliciesConstant;
using static EUniManager.Api.Constants.RoutesConstant;

namespace EUniManager.Api.CarterModules;

public class StudentModule()
    : CrudCarterModule<
            IStudentService,
            Student,
            StudentDto,
            StudentDetailsDto,
            CreateStudentDto,
            IUpdateDto>
      (string.Format(BASE_ROUTE_TEMPLATE, nameof(IEUniManagerDbContext.Students).ToLowerInvariant()))
{
    private const string GET_STUDENT_DETAILS_ROUTE = "/details";
    private const string GET_STUDENT_HEADER_DATA_ROUTE = "/header";
    private const string CERTIFY_SEMESTER_ROUTE = "/{id}/certify/semesters/{semester}";
    private const string UPDATE_SPECICALTY_ROUTE = "/{studentId}/specialties/{specialtyId}";
    private const string UPDATE_STATUS_ROUTE = "/{id}/status/{status}";
    private const string UPDATE_GROUP_NUMBER_ROUTE = "/{id}/group-number/{groupNumber}";
    private const string UPDATE_ENROLLED_IN_SEMESTER_ROUTE = "/{id}/enrolled-in-semester/{semester}";
    private const string UPDATE_EMAIL_ROUTE = "/{id}/email/{email}";
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(string.Empty, GetAll).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapGet(ID_ROUTE, GetById).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapGet(GET_STUDENT_DETAILS_ROUTE, GetDetails).RequireAuthorization(STUDENT_POLICY_NAME);
        app.MapGet(GET_STUDENT_HEADER_DATA_ROUTE, GetHeaderData).RequireAuthorization(STUDENT_POLICY_NAME);
        app.MapPost(string.Empty, Create).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPost(CERTIFY_SEMESTER_ROUTE, CertifySemester).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPatch(UPDATE_SPECICALTY_ROUTE, UpdateSpecialty).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPatch(UPDATE_STATUS_ROUTE, UpdateStatus).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPatch(UPDATE_GROUP_NUMBER_ROUTE, UpdateGroupNumber).RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPatch(UPDATE_ENROLLED_IN_SEMESTER_ROUTE, UpdateEnrolledInSemester)
           .RequireAuthorization(ADMIN_POLICY_NAME);
        app.MapPatch(UPDATE_EMAIL_ROUTE, UpdateEmail).RequireAuthorization(ADMIN_POLICY_NAME);
    }

    private async Task<Results<Ok<StudentDetailsDto>, BadRequest, NotFound>> GetDetails
    (
        IStudentService studentService,
        CancellationToken cancellationToken
    )
    {
        StudentDetailsDto student = await studentService.GetDetailsAsync(cancellationToken);

        return TypedResults.Ok(student);
    }
    
    private async Task<Results<Ok<StudentHeaderDto>, BadRequest, NotFound>> GetHeaderData
    (
        IStudentService studentService,
        CancellationToken cancellationToken
    )
    {
        StudentHeaderDto headerData = await studentService.GetHeaderDataAsync(cancellationToken);

        return TypedResults.Ok(headerData);
    }
    
    private async Task<Results<NoContent, BadRequest, NotFound>> CertifySemester
    (
        IStudentService studentService,
        [FromRoute] Guid id,
        [FromRoute] byte semester,
        CancellationToken cancellationToken
    )
    {
        await studentService.CertifySemesterAsync(id, semester, cancellationToken);

        return TypedResults.NoContent();
    }
    
    private async Task<Results<NoContent, BadRequest, NotFound>> UpdateSpecialty
    (
        IStudentService studentService,
        [FromRoute] Guid studentId,
        [FromRoute] Guid specialtyId,
        CancellationToken cancellationToken
    )
    {
        await studentService.UpdateSpecialtyAsync(studentId, specialtyId, cancellationToken);

        return TypedResults.NoContent();
    }
    
    private async Task<Results<NoContent, BadRequest, NotFound>> UpdateStatus
    (
        IStudentService studentService,
        [FromRoute] Guid id,
        [FromRoute] StudentStatus status,
        CancellationToken cancellationToken
    )
    {
        await studentService.UpdateStatusAsync(id, status, cancellationToken);

        return TypedResults.NoContent();
    }
    
    private async Task<Results<NoContent, BadRequest, NotFound>> UpdateGroupNumber
    (
        IStudentService studentService,
        [FromRoute] Guid id,
        [FromRoute] byte groupNumber,
        CancellationToken cancellationToken
    )
    {
        await studentService.UpdateGroupNumberAsync(id, groupNumber, cancellationToken);

        return TypedResults.NoContent();
    }
    
    private async Task<Results<NoContent, BadRequest, NotFound>> UpdateEnrolledInSemester
    (
        IStudentService studentService,
        [FromRoute] Guid id,
        [FromRoute] byte semester,
        CancellationToken cancellationToken
    )
    {
        await studentService.UpdateEnrolledInSemesterAsync(id, semester, cancellationToken);

        return TypedResults.NoContent();
    }
    
    private async Task<Results<NoContent, BadRequest, NotFound>> UpdateEmail
    (
        IStudentService studentService,
        [FromRoute] Guid id,
        [FromRoute] string email,
        CancellationToken cancellationToken
    )
    {
        await studentService.UpdateEmailAsync(id, email, cancellationToken);

        return TypedResults.NoContent();
    }
}