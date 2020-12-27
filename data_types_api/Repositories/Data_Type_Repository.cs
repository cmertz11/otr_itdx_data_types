using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using data_types_api.Data;
using data_types_api.Data.Models;
using data_types_api.Repositories.Interfaces;
using data_types_api.Settings;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace data_types_api.Repositories
{
    public class Data_Type_Repository : IData_Type_Repository
    {
        private readonly IData_Type_Context _context;
        private readonly IData_Type_Db_Settings _settings;

        public Data_Type_Repository(IData_Type_Context context, IData_Type_Db_Settings settings)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<List<string>> GetDataTypes()
        {
            
            var dt = await _context.Redis.StringGetAsync("data_type_name_list");

            if (dt.IsNullOrEmpty)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<List<string>>(dt);
        }

        public async Task<Data_Type> GetData_TypeByName(string name)
        {
            var dt = await _context.Redis.StringGetAsync(name);

            if(dt.IsNullOrEmpty)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Data_Type>(dt);
        }

        public bool Load_ITDX(IFormFile file)
        {  
            _context.Redis.StringSet("ITDX_LOADED", "false");
            using (var ms = new MemoryStream())
            {
                var fileStream = file.OpenReadStream();
                StreamReader reader = new StreamReader(fileStream);

                string xmlString = reader.ReadToEnd();
                 
                var itdxData = Data_Type_Context_Seed.SeedData(_context, xmlString);

                return itdxData;
            }
        }

    }
}
