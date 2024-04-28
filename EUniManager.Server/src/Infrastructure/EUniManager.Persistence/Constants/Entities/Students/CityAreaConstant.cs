using EUniManager.Domain.Abstraction.Student;
using EUniManager.Domain.Entities.Students;

namespace EUniManager.Persistence.Constants.Entities.Students;

public static class CityAreaConstant
{
    public const byte CITY_MAX_STRING_LENGTH = 30;
    
    public const byte AREA_MAX_STRING_LENGTH = 50;
    
    public const string DIPLOMA_CITY_COLUMN_NAME = nameof(Diploma) + nameof(CityArea.City);
    
    public const string DIPLOMA_AREA_COLUMN_NAME = nameof(Diploma) + nameof(CityArea.Area);
    
    public const string PERSONAL_DATA_CITY_COLUMN_NAME = nameof(PersonalData) + nameof(CityArea.City);
    
    public const string PERSONAL_DATA_AREA_COLUMN_NAME = nameof(PersonalData) + nameof(CityArea.Area);
    
    public const string PERMANENT_RESIDENCE_CITY_COLUMN_NAME = nameof(Student.PermanentResidence) + nameof(CityArea.City);
    
    public const string PERMANENT_RESIDENCE_AREA_COLUMN_NAME = nameof(Student.PermanentResidence) + nameof(CityArea.Area);
    
    public const string TEMPORARY_RESIDENCE_CITY_COLUMN_NAME = nameof(Student.TemporaryResidence) + nameof(CityArea.City);
    
    public const string TEMPORARY_RESIDENCE_AREA_COLUMN_NAME = nameof(Student.TemporaryResidence) + nameof(CityArea.Area);
}