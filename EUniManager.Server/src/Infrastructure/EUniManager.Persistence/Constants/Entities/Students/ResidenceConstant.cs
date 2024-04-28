using EUniManager.Domain.Abstraction.Student;
using EUniManager.Domain.Entities.Students;

namespace EUniManager.Persistence.Constants.Entities.Students;

public static class ResidenceConstant
{
    public const byte STREET_MAX_STRING_LENGTH = 30;
    
    public const byte PHONE_NUMBER_MAX_STRING_LENGTH = 15;

    public const string PERMANENT_RESIDENCE_STREET_COLUMN_NAME =
        nameof(Student.PermanentResidence) + nameof(Residence.Street);
    
    public const string PERMANENT_RESIDENCE_PHONE_NUMBER_COLUMN_NAME =
        nameof(Student.PermanentResidence) + nameof(Residence.PhoneNumber);
    
    public const string TEMPORARY_RESIDENCE_STREET_COLUMN_NAME =
        nameof(Student.TemporaryResidence) + nameof(Residence.Street);
    
    public const string TEMPORARY_RESIDENCE_PHONE_NUMBER_COLUMN_NAME =
        nameof(Student.TemporaryResidence) + nameof(Residence.PhoneNumber);
}