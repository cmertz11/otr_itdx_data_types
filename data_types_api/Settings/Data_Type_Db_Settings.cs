using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace data_types_api.Settings
{
    public class Data_Type_Db_Settings : IData_Type_Db_Settings
    { 
        public string ConnectionString { get; set; }  
    }
}
