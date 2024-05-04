using EUniManager.Api.CarterModules.Base;
using EUniManager.Application.Models.Exams.Dtos;
using EUniManager.Application.Models.Exams.Interfaces;
using EUniManager.Domain.Entities;

namespace EUniManager.Api.CarterModules;

public sealed class ExamModule()
    : CrudCarterModule<IExamService, Exam, ExamDto, ExamDetailsDto, CreateExamDto, UpdateExamDto>
        (string.Format(BASE_PATH_TEMPLATE, nameof(Exam).ToLowerInvariant()));