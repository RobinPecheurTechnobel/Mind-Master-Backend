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
    /// <summary>Controller gèrant la partie Authentification des comptes</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>Instance du service de création de token</summary>
        private TokenService _TokenService;
        /// <summary>Instance du service gèrant les comptes dans la couche logique</summary>
        private AccountService _AccountService;

        /// <summary>Constructeur permettant les injections de dépendance</summary>
        /// <param name="tokenService">Injection de dépendance du service pour les tokens</param>
        /// <param name="accountService">Injection de dépendance du service pour les comptes</param>
        public AuthController(TokenService tokenService, AccountService accountService)
        {
            _TokenService = tokenService;
            _AccountService = accountService;
        }

        /// <summary>
        ///     EndPoint responsable de l'authentification d'un utilisateur
        /// </summary>
        /// <param name="credentials">Information d'authentification</param>
        /// <returns>
        ///     <list type="table">
        ///         <item>
        ///             <term>Code 200</term>
        ///             <description>
        ///                 L'authentification s'est bien passé
        ///                 L'utilisateur reçoit un token pour accéder à plus de requêtes
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>Code 400</term>
        ///             <description>
        ///                 L'authentification a échoué
        ///                 Le détaillé de l'erreur sera a retiré pour la production
        ///             </description>
        ///         </item>
        ///     </list>
        /// </returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthTokenDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody] AuthDTO credentials)
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

        /// <summary>Endpoint permettant l'enregistrement d'un nouvel utilisateur</summary>
        /// <param name="data">Information fournit pour l'enregistrement</param>
        /// <returns>
        ///     <list type="table">
        ///         <item>
        ///             <term>Code 201</term>
        ///             <description>
        ///                 L'enregistré s'est bien déroulé
        ///                 Le token pour demander une reconnexion
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>Code 400</term>
        ///             <description>
        ///                 L'authentification a échoué
        ///             </description>
        ///         </item>
        ///     </list>
        /// </returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        [Consumes("application/json")]
        [ProducesResponseType(201, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult Register([FromBody] AuthRegisterDTO data)
        {
            try
            {
                if (data.Password != data.PasswordConfirmation) throw new DataConstraintException
                        ("La confirmation du mot de passe n'a pas la même valeur que le mot de passe");

                NewAccountDataTO newAccount = new NewAccountDataTO
                {
                    Login = data.Login,
                    Password = data.Password
                };

                AccountDTO account = _AccountService.Create(newAccount.ToNewModel()).ToDTO();

                string token = "Bearer " + _TokenService.GenerateJwt(account);
                return CreatedAtAction(nameof(Reconnection),null, new { token });
                //return CreatedAtAction(nameof(AccountController.GetOneById), "Account", new { id },new {id});
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

        [HttpGet("Reconnection")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthTokenDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Reconnection([FromHeader] string Authorization)
        {
            try
            {

                string tokenReceived = Authorization.Replace("Bearer ", "");
                int id = _TokenService.GetIdFromToken(tokenReceived);

                AccountDTO account = _AccountService.GetOneById(id).ToDTO();

                string token = "Bearer " + _TokenService.GenerateJwt(account);
                return Ok(token);
            }
            catch(TokenException tException)
            {
                return BadRequest(tException.Message);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
