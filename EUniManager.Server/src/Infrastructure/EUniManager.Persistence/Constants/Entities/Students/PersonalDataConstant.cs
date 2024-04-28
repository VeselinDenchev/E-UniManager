using EUniManager.Domain.Entities.Students;

namespace EUniManager.Persistence.Constants.Entities.Students;

public static class PersonalDataConstant
{
    public const byte INSURANCE_NUMBER_MAX_STRING_LENGTH = 10;

    public const byte GENDER_MAX_STRING_LENGTH = 6;
    
    public const byte CITIZIENSHIP_MAX_STRING_LENGTH = 20;

    public const string EMAIL_COLUMN_NAME = nameof(PersonalData) + nameof(PersonalData.Email);
    
    public const byte EMAIL_COLUMN_MAX_STRING_LENGTH = 30;
}