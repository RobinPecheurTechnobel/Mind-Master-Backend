using BLL.CustomExceptions;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mind_Master_Backend.DTOs.Enums;
using Mind_Master_Backend.DTOs;
using Mind_Master_Backend.Mappers.Enums;
using Mind_Master_Backend.Mappers;

namespace Mind_Master_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdeaController : ControllerBase
    {
        private IdeaService _IdeaService;

        /// <summary>Constructeur pour les injections de dépendance</summary>
        /// <param name="thinkerService">Injection de dépendance du service de gestion des comptes</param>
        public IdeaController(IdeaService ideaService)
        {
            _IdeaService = ideaService;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<IdeaDTO>))]
        public IActionResult GetAll()
        {
            IEnumerable<IdeaDTO> result = _IdeaService.GetAll().Select(i => i.ToDTO());
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
        [Route("Format")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FormatDTO>))]
        public IActionResult GetAllRole()
        {
            IEnumerable<EnumDTO> result = EnumMapper<FormatDTO>.GetAllValuesAsIEnumerable()
                .Select(d => new EnumDTO(d));
            return Ok(result);
        }

        [HttpGet("Format/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FormatDTO>))]
        [ProducesResponseType(404, Type = typeof(IEnumerable<string>))]
        public IActionResult GetRolebyId(int id)
        {
            try
            {
                EnumDTO? result = EnumMapper<FormatDTO>.GetAllValuesAsIEnumerable()
                    .Where(value => value == (FormatDTO)id)
                    .Select(d => new EnumDTO(d))
                    .FirstOrDefault();

                if (result is null) throw new NotFoundException("Ce format n'existe pas");

                return Ok(result);
            }
            catch (NotFoundException nFException)
            {
                return NotFound(nFException.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(int))]
        public IActionResult Create([FromBody] IdeaDataTO idea)
        {
            try
            {
                int id = _IdeaService.Create(idea.ToModel()).Id;
                return CreatedAtAction(nameof(IdeaController.GetOneById), new { ideaId = id }, new { id });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
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
        [HttpGet("{ideaId}")]
        [ActionName("GetOneById")]
        [ProducesResponseType(200, Type = typeof(IdeaDTO))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult GetOneById([FromRoute] int ideaId)
        {
            try
            {
                return Ok(_IdeaService.GetOneById(ideaId).ToDTO());
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
        [HttpGet("Thinker/{thinkerId}")]
        [ActionName("GetByThinkerId")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<IdeaDTO>))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult GetByThinkerId([FromRoute] int thinkerId)
        {
            try
            {
                return Ok(_IdeaService.GetByThinkerId(thinkerId).Select(i=> i.ToDTO()));
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
        public IActionResult Update([FromRoute] int id, [FromBody] IdeaDataTO DataTo)
        {
            try
            {
                _IdeaService.Update(id, DataTo.ToModel());
                return NoContent();
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
                if (_IdeaService.Delete(id)) return NoContent();
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
