using BLL.CustomExceptions;
using BLL.Services;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mind_Master_Backend.DTOs;
using Mind_Master_Backend.Mappers;
using System.Security.Cryptography;
using System.Text;

namespace Mind_Master_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private AccountService _AccountService;
        private Argon2 _argon2A;

        public AccountController(AccountService accountService)
        {
            _AccountService = accountService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountDTO>))]
        public IActionResult GetAll()
        {
            IEnumerable<AccountDTO> result = _AccountService.GetAll().Select(a => a.ToDTO());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ActionName("GetOneById")]
        [ProducesResponseType(200, Type = typeof(AccountDTO))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult GetOneById([FromRoute] int id)
        {
            try
            {
                return Ok(_AccountService.GetOneById(id).ToDTO());
            }
            catch (NotFoundException nFException)
            {
                return NotFound(nFException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Update([FromRoute] int id, [FromBody] AccountDataTO DataTo)
        {
            try
            {
                _AccountService.Update(id, DataTo.ToNewModel());
                return NoContent();
            }
            catch(DataConstraintException dataException)
            {
                return BadRequest(dataException.Message);
            }
            catch(NotFoundException nFException)
            {
                return NotFound(nFException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                if(_AccountService.Delete(id)) return NoContent();
                return NotFound();

            }
            catch (DataConstraintException dataException)
            {
                return BadRequest(dataException.Message);
            }
            catch (NotFoundException nFException)
            {
                return NotFound(nFException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
