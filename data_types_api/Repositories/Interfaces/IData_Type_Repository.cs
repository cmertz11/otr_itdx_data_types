using data_types_api.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace data_types_api.Repositories.Interfaces
{
    public interface IData_Type_Repository
    {
        Task<Data_Type> GetData_TypeByName(string name);

        Task<List<string>> GetDataTypes();
        bool Load_ITDX(IFormFile file);
    }
}
