namespace EUniManager.Persistence.Constants;

public static class SqlConstant
{
    public const string GET_DATE_FUNCTION = "GETDATE()";
    
    public const string CHECK_CONSTRAINT_TEMPLATE = "CK_{0}";
    
    public const string UNIQUE_INDEX_TEMPLATE = "IX_{0}";

    public const string DECIMAL_DATA_TYPE_TEMPLATE = "DECIMAL({0},{1})";
}