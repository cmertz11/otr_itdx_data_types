using data_types_api.Data;
using data_types_api.Data.Models;
using data_types_api.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace data_types_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DataTypesController : ControllerBase
    {

        private readonly IData_Type_Repository _repository;
        private readonly ILogger<DataTypesController> _logger;

        public DataTypesController(IData_Type_Repository repository, ILogger<DataTypesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ActionName("GetDataTypeByName")]
        public async Task<ActionResult<IEnumerable<Data_Type>>> GetDataTypeByNameAsync(string name)
        {
            var dataSet = await _repository.GetData_TypeByName(name);
            if (dataSet != null)
            {
                return Ok(dataSet);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ActionName("GetDataTypes")]
        public async Task<ActionResult<IEnumerable<string>>> GetDataSetByNamesAsync()
        {
            var dataSet = await _repository.GetDataTypes();
            if (dataSet != null)
            {
                return Ok(dataSet);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("upload", Name = "upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult UploadITDXFile(IFormFile file, CancellationToken cancellationToken)
        {
            if (Is_xsd(file))
            {
               bool loaded = Load_ITDX(file);
            }
            else
            {
                return BadRequest(new { message = "Invalid file extension" });
            }

            return Ok();
        }

        private bool Load_ITDX(IFormFile file)
        {      
            return _repository.Load_ITDX(file);
        }

        private bool Is_xsd(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".xsd");

        }

    }
}
