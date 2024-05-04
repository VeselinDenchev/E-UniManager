using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Students.Dtos;
using EUniManager.Application.Models.Students.Interfaces;
using EUniManager.Domain.Entities.Students;

namespace EUniManager.Api.CarterModules;

public class StudentModule()
    : CrudCarterModule<
            IStudentService,
            Student,
            StudentDto,
            StudentDetailsDto,
            CreateStudentDto,
            UpdateStudentDto>
            (string.Format(BASE_PATH_TEMPLATE, nameof(Student).ToLowerInvariant()));