using BLL.CustomExceptions;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Mind_Master_Backend.DTOs;
using Mind_Master_Backend.Mappers;
using Mind_Master_Backend.Services;
using System.Diagnostics.Eventing.Reader;

namespace Mind_Master_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private TokenService _TokenService;
        private AccountService _AccountService;

        public AuthController(TokenService tokenService, AccountService accountService)
        {
            _TokenService = tokenService;
            _AccountService = accountService;
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthTokenDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody] AuthDTO credentials) // TODO change type objet
        {
            try
            {
                AccountDTO accountDto = _AccountService.Login(credentials.Login, credentials.Password)?.ToDTO();

                string token = "Bearer "+_TokenService.GenerateJwt(accountDto);
                
                return Ok(new AuthTokenDTO { Token = token , Account = accountDto });
            }
            catch(BadRequestException bDException)
            {
                return BadRequest(bDException.Message);
            }
            catch(NotFoundException nFException)
            {
                return BadRequest(nFException.Message);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        [Consumes("application/json")]
        [ProducesResponseType(201, Type = typeof(int))]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult Register([FromBody] AuthRegisterDTO data)
        {
            try
            {
                if (data.Password != data.PasswordConfirmation) throw new DataConstraintException
                        ("La confirmation du mot de passe n'a pas la même valeur que le mot de passe");

                AccountDataTO newAccount = new AccountDataTO
                {
                    Login = data.Login,
                    Password = data.Password
                };

                int id = _AccountService.Create(newAccount.ToNewModel());

                return CreatedAtAction(nameof(AccountController.GetOneById), "Account", new { id }, new { id });
            }
            catch (DataConstraintException dataException)
            {
                return BadRequest(dataException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
