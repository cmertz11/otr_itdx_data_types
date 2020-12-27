using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace data_types_api.Data.Models
{
    public class Data_Type_Records
    {
        public string Code { get; set; }

        public string Description { get; set; }
    }
}
