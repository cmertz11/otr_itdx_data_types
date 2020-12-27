using data_types_api.Data.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace data_types_api.Data
{
    public class Data_Type_Context_Seed
    {
        public static bool SeedData(IData_Type_Context _Context, string itdxString)
        {

            IEnumerable<Data_Type> dataTypes = Load_ITDX_Data_Types_From_xsd(itdxString);
            List<string> dataTypeNameList = new List<string>();
            foreach (var dt in dataTypes)
            {
                dataTypeNameList.Add(dt.Name);
                 _Context.Redis.StringSetAsync(dt.Name, JsonConvert.SerializeObject(dt));
            }
            _Context.Redis.StringSet("data_type_name_list", JsonConvert.SerializeObject(dataTypeNameList));
            _Context.Redis.StringSet("ITDX_LOADED", "true");

            return true;
            
        }

        private static IEnumerable<Data_Type> Load_ITDX_Data_Types_From_xsd(string itdxString)
        {
            List<Data_Type> data_Types = new List<Data_Type>();

            XDocument schema = XDocument.Parse(itdxString);
            XNamespace xs = schema.Root.Name.Namespace;
            var dt = schema.Root.Descendants(xs + "simpleType").Where(s => s.FirstAttribute != null);

            foreach (var table in dt)
            {
                Data_Type dtype = new Data_Type();

                dtype.Name = table.FirstAttribute.Value;

                if (table.Descendants(xs + "documentation") != null)
                {
                    var docCount = table.Descendants(xs + "documentation").Count();

                    if (docCount > 0)
                    {
                        dtype.Description = table.Descendants(xs + "documentation").FirstOrDefault().Value;
                    }
                }

                var values =
                    schema
                    .Root
                    .Descendants(xs + "simpleType")
                    .Where(t => (string)t.Attribute("name") == table.FirstAttribute.Value)
                    .Descendants(xs + "enumeration");


                dtype.Data_Type_Record_List = new List<Data_Type_Records>();

                foreach (var i in values)
                {
                    var rec = new Data_Type_Records { Code = i.FirstAttribute.Value, Description = i.Value };

                    dtype.Data_Type_Record_List.Add(rec);
                }

                data_Types.Add(dtype);
            }
            return data_Types;
        }
 
    }
}
 
