using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace data_types_api.Settings
{
    public interface IData_Type_Db_Settings
    { 
        string ConnectionString { get; set; }  
    }
}
