using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CloudinaryFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Extension = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudinaryFiles", x => x.Id);
                    table.CheckConstraint("CK_CloudinaryFile_ModifiedAt", "ModifiedAt >= CreatedAt");
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LecturesCount = table.Column<byte>(type: "tinyint", nullable: false),
                    ExercisesCount = table.Column<byte>(type: "tinyint", nullable: false),
                    Mark = table.Column<int>(type: "int", nullable: true),
                    CreditsCount = table.Column<byte>(type: "tinyint", nullable: false),
                    Protocol = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.CheckConstraint("CK_Course_ExercisesCount", "CreditsCount BETWEEN 2 AND 7");
                    table.CheckConstraint("CK_Course_LecturesCount", "LecturesCount >= 0");
                    table.CheckConstraint("CK_Course_ModifiedAt", "ModifiedAt >= CreatedAt");
                });

            migrationBuilder.CreateTable(
                name: "CourseSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSchedules", x => x.Id);
                    table.CheckConstraint("CK_CourseSchedule_ModifiedAt", "ModifiedAt >= CreatedAt");
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                    table.CheckConstraint("CK_Faculty_ModifiedAt", "ModifiedAt >= CreatedAt");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.CheckConstraint("CK_Teacher_ModifiedAt", "ModifiedAt >= CreatedAt");
                    table.ForeignKey(
                        name: "FK_Teachers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                    table.CheckConstraint("CK_Specialty_ModifiedAt", "ModifiedAt >= CreatedAt");
                    table.ForeignKey(
                        name: "FK_Specialties_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LecturerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    SpecialtyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.CheckConstraint("CK_Subject_ModifiedAt", "ModifiedAt >= CreatedAt");
                    table.ForeignKey(
                        name: "FK_Subjects_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subjects_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subjects_Teachers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourseScheduleUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: true),
                    ExactDate = table.Column<DateOnly>(type: "date", nullable: true),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    MonthlyFrequency = table.Column<int>(type: "int", nullable: true),
                    GroupType = table.Column<int>(type: "int", nullable: true),
                    GroupNumber = table.Column<byte>(type: "tinyint", nullable: true),
                    RoomNumber = table.Column<short>(type: "smallint", nullable: false),
                    Place = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Semester = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseScheduleUnits", x => x.Id);
                    table.CheckConstraint("CK_CourseScheduleUnit_ModifiedAt", "ModifiedAt >= CreatedAt");
                    table.CheckConstraint("CK_CourseScheduleUnit_Semester", "Semester BETWEEN 1 AND 16");
                    table.ForeignKey(
                        name: "FK_CourseScheduleUnits_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumSubjectResourcesUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Semester = table.Column<byte>(type: "tinyint", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumSubjectResourcesUnits", x => x.Id);
                    table.CheckConstraint("CK_CurriculumSubjectResourcesUnit_Semester", "Semester BETWEEN 1 AND 16");
                    table.ForeignKey(
                        name: "FK_CurriculumSubjectResourcesUnits_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    Place = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    RoomNumber = table.Column<short>(type: "smallint", nullable: false),
                    GroupNumber = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.CheckConstraint("CK_Exam_GroupNumber", "GroupNumber IS NULL OR GroupNumber BETWEEN 1 AND 5");
                    table.CheckConstraint("CK_Exam_ModifiedAt", "ModifiedAt >= CreatedAt");
                    table.CheckConstraint("CK_Exam_RoomNumber", "RoomNumber BETWEEN 101 AND 520");
                    table.CheckConstraint("CK_Exam_Time", "Time BETWEEN '08:00:00' AND '18:00:00'");
                    table.ForeignKey(
                        name: "FK_Exams_Subjects_Id",
                        column: x => x.Id,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualProtocols",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualProtocols", x => x.Id);
                    table.CheckConstraint("CK_IndividualProtocol_ModifiedAt", "ModifiedAt >= CreatedAt");
                    table.ForeignKey(
                        name: "FK_IndividualProtocols_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTeacher",
                columns: table => new
                {
                    AssistantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssistingSubjectsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTeacher", x => new { x.AssistantsId, x.AssistingSubjectsId });
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_Subjects_AssistingSubjectsId",
                        column: x => x.AssistingSubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_Teachers_AssistantsId",
                        column: x => x.AssistantsId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseScheduleCourseScheduleUnit",
                columns: table => new
                {
                    CourseSchedulesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseScheduleCourseScheduleUnit", x => new { x.CourseSchedulesId, x.UnitsId });
                    table.ForeignKey(
                        name: "FK_CourseScheduleCourseScheduleUnit_CourseScheduleUnits_UnitsId",
                        column: x => x.UnitsId,
                        principalTable: "CourseScheduleUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseScheduleCourseScheduleUnit_CourseSchedules_CourseSchedulesId",
                        column: x => x.CourseSchedulesId,
                        principalTable: "CourseSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurriculumSubjectResourcesUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.CheckConstraint("CK_Resource_ModifiedAt", "ModifiedAt >= CreatedAt");
                    table.ForeignKey(
                        name: "FK_Resources_CloudinaryFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "CloudinaryFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resources_CurriculumSubjectResourcesUnits_CurriculumSubjectResourcesUnitId",
                        column: x => x.CurriculumSubjectResourcesUnitId,
                        principalTable: "CurriculumSubjectResourcesUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.CheckConstraint("CK_Assignment_ModifiedAt", "ModifiedAt >= CreatedAt");
                    table.ForeignKey(
                        name: "FK_Assignments_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentsSolutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SeenOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Mark = table.Column<int>(type: "int", nullable: true),
                    MarkedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentsSolutions", x => x.Id);
                    table.CheckConstraint("CK_AssignmentSolution_Mark", "Mark IS NULL OR Mark BETWEEN 2 AND 6");
                    table.CheckConstraint("CK_AssignmentSolution_MarkedOn_UploadedOn", "MarkedOn IS NULL OR MarkedOn > UploadedOn");
                    table.CheckConstraint("CK_AssignmentSolution_ModifiedAt", "ModifiedAt >= CreatedAt");
                    table.CheckConstraint("CK_AssignmentSolution_SeenOn_CreatedAt", "SeenOn IS NULL OR SeenOn > CreatedAt");
                    table.CheckConstraint("CK_AssignmentSolution_UploadedOn_SeenOn", "UploadedOn IS NULL OR UploadedOn > SeenOn");
                    table.ForeignKey(
                        name: "FK_AssignmentsSolutions_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentsSolutions_CloudinaryFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "CloudinaryFiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssignmentStudent",
                columns: table => new
                {
                    AssignmentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentStudent", x => new { x.AssignmentsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_AssignmentStudent_Assignments_AssignmentsId",
                        column: x => x.AssignmentsId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diplomas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EducationalAndQualificationalDegree = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Series = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    Number = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "varchar(7)", unicode: false, maxLength: 7, nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    IssuedByInstitutionType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiplomaCity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DiplomaArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Specialty = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProfessionalQualification = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diplomas", x => x.Id);
                    table.CheckConstraint("CK_Diploma_ModifiedAt", "ModifiedAt >= CreatedAt");
                });

            migrationBuilder.CreateTable(
                name: "IdCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNumber = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    DateIssued = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdCards", x => x.Id);
                    table.CheckConstraint("CK_IdCard_ModifiedAt", "ModifiedAt >= CreatedAt");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pin = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    PlanNumber = table.Column<int>(type: "int", nullable: false),
                    FacultyNumber = table.Column<int>(type: "int", nullable: false),
                    GroupNumber = table.Column<byte>(type: "tinyint", nullable: false),
                    EnrolledInSemester = table.Column<byte>(type: "tinyint", nullable: true),
                    PersonalDataCity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PersonalDataArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UniqueIdentifierType = table.Column<string>(type: "varchar(900)", unicode: false, nullable: false),
                    UniqueIdentifier = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    InsuraceNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Citizienship = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    PersonalData_IdCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonalDataEmail = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    PermanentResidenceCity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PermanentResidenceArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PermanentResidenceStreet = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PermanentResidencePhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    TemporaryResidenceCity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TemporaryResidenceArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TemporaryResidenceStreet = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TemporaryResidencePhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    UsualResidenceCountry = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EnrollmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EnrollmentReason = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnrollmentMark = table.Column<decimal>(type: "DECIMAL(3,2)", precision: 3, scale: 2, nullable: false),
                    SpecialtyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.CheckConstraint("CK_Student_EnrolledInSemester", "EnrollmentMark BETWEEN 0 AND 30");
                    table.CheckConstraint("CK_Student_GroupNumber", "GroupNumber BETWEEN 1 AND 5");
                    table.CheckConstraint("CK_Student_ModifiedAt", "ModifiedAt >= CreatedAt");
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_CourseSchedules_CourseScheduleId",
                        column: x => x.CourseScheduleId,
                        principalTable: "CourseSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_IdCards_PersonalData_IdCardId",
                        column: x => x.PersonalData_IdCardId,
                        principalTable: "IdCards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Students_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PayedTaxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxNumber = table.Column<int>(type: "int", nullable: false),
                    DocumentNumber = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    DocumentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Semester = table.Column<byte>(type: "tinyint", nullable: false),
                    PlanNumber = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<short>(type: "smallint", nullable: false),
                    Currency = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayedTaxes", x => x.Id);
                    table.CheckConstraint("CK_PayedTax_Amount", "Amount > 0");
                    table.CheckConstraint("CK_PayedTax_ModifiedAt", "ModifiedAt >= CreatedAt");
                    table.CheckConstraint("CK_PayedTax_Semester", "Semester BETWEEN 1 AND 16");
                    table.CheckConstraint("CK_PayedTax_TaxNumber", "TaxNumber > 0");
                    table.ForeignKey(
                        name: "FK_PayedTaxes_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    RegistryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResolutionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestApplications", x => x.Id);
                    table.CheckConstraint("CK_RequestApplication_ModifiedAt", "ModifiedAt >= CreatedAt");
                    table.CheckConstraint("CK_RequestApplication_ResolutionDate", "ResolutionDate IS NULL OR ResolutionDate > RegistryDate");
                    table.ForeignKey(
                        name: "FK_RequestApplications_CloudinaryFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "CloudinaryFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestApplications_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubject",
                columns: table => new
                {
                    StudentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubject", x => new { x.StudentsId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_StudentSubject_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubject_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ResourceId",
                table: "Assignments",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentsSolutions_AssignmentId",
                table: "AssignmentsSolutions",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentsSolutions_FileId",
                table: "AssignmentsSolutions",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentsSolutions_StudentId",
                table: "AssignmentsSolutions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentStudent_StudentsId",
                table: "AssignmentStudent",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseScheduleCourseScheduleUnit_UnitsId",
                table: "CourseScheduleCourseScheduleUnit",
                column: "UnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseScheduleUnits_SubjectId",
                table: "CourseScheduleUnits",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumSubjectResourcesUnits_SubjectId",
                table: "CurriculumSubjectResourcesUnits",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IdCards_StudentId",
                table: "IdCards",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualProtocols_SubjectId",
                table: "IndividualProtocols",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PayedTaxes_StudentId",
                table: "PayedTaxes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxNumber",
                table: "PayedTaxes",
                column: "TaxNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Number",
                table: "RequestApplications",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestApplications_FileId",
                table: "RequestApplications",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestApplications_StudentId",
                table: "RequestApplications",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CurriculumSubjectResourcesUnitId",
                table: "Resources",
                column: "CurriculumSubjectResourcesUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_FileId",
                table: "Resources",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_FacultyId",
                table: "Specialties",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyNumber",
                table: "Students",
                column: "FacultyNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pin",
                table: "Students",
                column: "Pin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseScheduleId",
                table: "Students",
                column: "CourseScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyId",
                table: "Students",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_PersonalData_IdCardId",
                table: "Students",
                column: "PersonalData_IdCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SpecialtyId",
                table: "Students",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueIdentifierType_UniqueIdentifier",
                table: "Students",
                columns: new[] { "UniqueIdentifierType", "UniqueIdentifier" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_SubjectsId",
                table: "StudentSubject",
                column: "SubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_CourseId",
                table: "Subjects",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_LecturerId",
                table: "Subjects",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SpecialtyId",
                table: "Subjects",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_AssistingSubjectsId",
                table: "SubjectTeacher",
                column: "AssistingSubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UserId",
                table: "Teachers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentsSolutions_Students_StudentId",
                table: "AssignmentsSolutions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentStudent_Students_StudentsId",
                table: "AssignmentStudent",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Diplomas_Students_Id",
                table: "Diplomas",
                column: "Id",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdCards_Students_StudentId",
                table: "IdCards",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_IdCards_Students_StudentId",
                table: "IdCards");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssignmentsSolutions");

            migrationBuilder.DropTable(
                name: "AssignmentStudent");

            migrationBuilder.DropTable(
                name: "CourseScheduleCourseScheduleUnit");

            migrationBuilder.DropTable(
                name: "Diplomas");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "IndividualProtocols");

            migrationBuilder.DropTable(
                name: "PayedTaxes");

            migrationBuilder.DropTable(
                name: "RequestApplications");

            migrationBuilder.DropTable(
                name: "StudentSubject");

            migrationBuilder.DropTable(
                name: "SubjectTeacher");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "CourseScheduleUnits");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "CloudinaryFiles");

            migrationBuilder.DropTable(
                name: "CurriculumSubjectResourcesUnits");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "CourseSchedules");

            migrationBuilder.DropTable(
                name: "IdCards");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
