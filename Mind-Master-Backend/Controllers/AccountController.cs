using BLL.CustomExceptions;
using BLL.Services;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mind_Master_Backend.DTOs;
using Mind_Master_Backend.Mappers;

namespace Mind_Master_Backend.Controllers
{
    /// <summary>
    ///     Controller responsable de la gestion des comptes.
    ///     Pour l'enregistement d'un compte ou se connecter, il faut utiliser le <see cref="AuthController">AuthController</see>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>Instance du service qui gère les comptes dans la couche logique</summary>
        private AccountService _AccountService;

        /// <summary>Constructeur pour les injections de dépendance</summary>
        /// <param name="accountService">Injection de dépendance du service de gestion des comptes</param>
        public AccountController(AccountService accountService)
        {
            _AccountService = accountService;
        }

        /// <summary>EndPoint pour récupérer une liste des comptes</summary>
        /// <returns>
        ///     Un IActionResult donnant les réponses suivantes :
        ///     <list type="table">
        ///         <item>
        ///             <term>Code 200</term>
        ///             <description>La liste des comptes</description>
        ///         </item>
        ///     </list>
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountDTO>))]
        public IActionResult GetAll()
        {
            IEnumerable<AccountDTO> result = _AccountService.GetAll().Select(a => a.ToDTO());
            return Ok(result);
        }

        /// <summary>EndPoint donnant la liste des roles avec leur clé</summary>
        /// <returns>
        ///     Un IActionResult donnant les réponses suivantes :
        ///     <list type="table">
        ///         <item>
        ///             <term>Code 200</term>
        ///             <description>La liste des roles</description>
        ///         </item>
        ///     </list>
        /// </returns>
        [HttpGet]
        [Route("Role")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RoleDTO>))]
        public IActionResult GetAllRole()
        {
            IEnumerable<EnumDTO> result = EnumMapper<RoleDTO>.GetAllValuesAsIEnumerable()
                .Select(d => new EnumDTO(d));
            return Ok(result);
        }

        [HttpGet("Role/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RoleDTO>))]
        [ProducesResponseType(404, Type = typeof(IEnumerable<string>))]
        public IActionResult GetRolebyId(int id)
        {
            try
            {
                EnumDTO? result = EnumMapper<RoleDTO>.GetAllValuesAsIEnumerable()
                    .Where(value => value == (RoleDTO)id)
                    .Select(d => new EnumDTO(d))
                    .FirstOrDefault();

                if (result is null) throw new NotFoundException("Ce role n'existe pas");

                return Ok(result);
            }
            catch (NotFoundException nFException)
            {
                return NotFound(nFException.Message);
            }
        }


        /// <summary>EndPoint pour récupérer un compte spécifique</summary>
        /// <param name="id">L'identifiant du compte recherché</param>
        /// <returns>
        ///     Un IActionResult donnant les réponses suivantes :
        ///     <list type="table">
        ///         <item>
        ///             <term>Code 200</term>
        ///             <description>Le compte recherché est remis</description>
        ///         </item>
        ///         <item>
        ///             <term>Code 404</term>
        ///             <description>Le compte n'a pas été trouvé</description>
        ///         </item>
        ///     </list>
        /// </returns>
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

        /// <summary>EndPoint pour modifier un compte spécifique</summary>
        /// <param name="id">L'identifiant du compte à modifié</param>
        /// <param name="DataTo">Modification à enregistrer</param>
        /// <returns>
        ///     Un IActionResult donnant les réponses suivantes :
        ///     <list type="table">
        ///         <item>
        ///             <term>Code 204</term>
        ///             <description>Les modifications ont été enregistré (aucun retour)</description>
        ///         </item>
        ///         <item>
        ///             <term>Code 400</term>
        ///             <description>
        ///                 Un problème est survenu dans la requête
        ///                 Surement une contrainte sur les données qui n'ont pas été respecté (voir le message de l'exception)
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>Code 404</term>
        ///             <description>Le compte n'a pas été trouvé</description>
        ///         </item>
        ///     </list>
        /// </returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Update([FromRoute] int id, [FromBody] AccountDataTO DataTo)
        {
            try
            {
                _AccountService.Update(id, DataTo.ToModel());
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

        /// <summary>EndPoint pour supprimer un compte spécifique</summary>
        /// <param name="id">L'identifiant du compte à modifié</param>
        /// <returns>
        ///     Un IActionResult donnant les réponses suivantes :
        ///     <list type="table">
        ///         <item>
        ///             <term>Code 204</term>
        ///             <description>La suppression s'est bien effectué (aucun retour)</description>
        ///         </item>
        ///         <item>
        ///             <term>Code 404</term>
        ///             <description>Le compte n'a pas été trouvé</description>
        ///         </item>
        ///     </list>
        /// </returns>
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
