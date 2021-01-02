using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Collections.Generic;

namespace data_types_api.Data.Models
{
    public class Data_Type
    { 
        public string Name { get; set; }
        
 
        public string Description { get; set; }
 
    
        public List<Data_Type_Records> Data_Type_Record_List { get; set; }
    }
}
