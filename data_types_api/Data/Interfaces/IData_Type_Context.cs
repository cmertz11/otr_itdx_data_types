using StackExchange.Redis;


namespace data_types_api.Data
{
    public interface IData_Type_Context
    {
        IDatabase Redis { get; }
    }
}
