using EDI.Crud.Api.Models.UserApiModel;
using EDI.Crud.Data.Dao;
using EDI.Crud.Domain.UserRepo;
using EDI.Crud.Utilities.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDI.Crud.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("getDataUser/{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id, CancellationToken c)
        {
            try
            {
                var result = await _userRepo.Read(id, c);
                if (result == null)
                    return NotFound($"User with id '{id}' not found!");
                return Ok(result);
            }
            catch (DomainLayerException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPost("setDataUser")]
        public async Task<IActionResult> Create([FromBody] User d, CancellationToken c)
        {
            try
            {

                var result = await _userRepo.Create(d, c);
                return Ok(result);
            }
            catch (DomainLayerException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }


        [HttpPost("Paging/DataTable")]
        public async Task<IActionResult> Datatable([FromBody] UserDTParamApiModel d, CancellationToken c)
        {
            try
            {
                var result = await _userRepo.DataTablePaging(d, c);
                return Ok(result);
            }
            catch (DomainLayerException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpDelete("delDataUser/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken c)
        {
            try
            {

                await _userRepo.Delete(id, c);
                return Ok();
            }
            catch (DomainLayerException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
