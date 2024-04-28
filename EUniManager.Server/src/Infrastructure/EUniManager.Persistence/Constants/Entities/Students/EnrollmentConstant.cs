using EUniManager.Domain.Entities.Students;

namespace EUniManager.Persistence.Constants.Entities.Students;

public static class EnrollmentConstant
{
    public const string ENROLLMENT_DATE_COLUMN_NAME = nameof(Enrollment) + nameof(Enrollment.Date);
    
    public const string ENROLLMENT_REASON_COLUMN_NAME = nameof(Enrollment) + nameof(Enrollment.Reason);
    
    public const byte ENROLLMENT_REASON_MAX_STRING_LENGTH = 50;
    
    public const string ENROLLMENT_MARK_COLUMN_NAME = nameof(Enrollment) + nameof(Enrollment.Mark);

    public const byte ENROLLMENT_MARK_PRECISION = 3;
    
    public const byte ENROLLMENT_MARK_SCALE = 2;
}