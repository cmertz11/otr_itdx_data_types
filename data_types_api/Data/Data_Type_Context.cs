using data_types_api.Settings;
using StackExchange.Redis;
using System;
using System.IO;

namespace data_types_api.Data
{


    public class Data_Type_Context : IData_Type_Context
    {
        public IDatabase Redis { get; }
    
        private readonly ConnectionMultiplexer _redisConnection;
        public Data_Type_Context(ConnectionMultiplexer redisConnection, IData_Type_Db_Settings settings)
        {
            _redisConnection = redisConnection;
            Redis = redisConnection.GetDatabase();
        }
    }
}
