using BLL.CustomExceptions;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mind_Master_Backend.DTOs;
using Mind_Master_Backend.Mappers;
using Mind_Master_Backend.Services;
using System.Runtime.Serialization;

namespace Mind_Master_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssemblyController : ControllerBase
    {
        private AssemblyService _AssemblyService;
        private TokenService _TokenService;

        public AssemblyController(AssemblyService assemblyService,
            TokenService tokenService)
        {
            _AssemblyService = assemblyService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AssemblyDTO>))]
        public IActionResult GetAll()
        {
            IEnumerable<AssemblyDTO> result = _AssemblyService.GetAll().Select(a => a.ToDTO());
            return Ok(result);
        }
        [HttpGet("{assemblyId}")]
        [ActionName("GetOneById")]
        [ProducesResponseType(200, Type = typeof(AssemblyDTO))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult GetOneById([FromRoute] int assemblyId)
        {
            try
            {
                return Ok(_AssemblyService.GetOneById(assemblyId).ToDTO());
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
        [HttpGet("title/{title}")]
        [ActionName("GetAllWithTitle")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AssemblyDTO>))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult GetAllWithTitle([FromRoute] string title)
        {
            try
            {
                return Ok(_AssemblyService.GetByTitle(title).Select(c => c.ToDTO()));
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

        [HttpGet("Label/{labelId}")]
        [ActionName("GetAllWithLabel")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AssemblyDTO>))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult GetAllWithLabel([FromRoute] int labelId)
        {
            try
            {
                return Ok(_AssemblyService.GetByLabel(labelId).Select(c => c.ToDTO()));
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
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                if (_AssemblyService.Delete(id)) return NoContent();
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
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(int))]
        public IActionResult Create([FromBody] AssemblyDataTO assembly)
        {
            return this.CreateToAGroup(assembly, 1);
        }
        [HttpPost("{groupId}")]
        [ProducesResponseType(201, Type = typeof(int))]
        public IActionResult CreateToAGroup([FromBody] AssemblyDataTO assembly, [FromRoute] int groupId)
        {
            try
            {
                int id = ((AssemblyService)_AssemblyService).CreateAssembly(assembly.ToModel(),
                    groupId
                    ).Id;
                return CreatedAtAction(nameof(AssemblyController.GetOneById), new { assemblyId = id }, new { id });
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
        public IActionResult Update([FromRoute] int id, [FromBody] AssemblyDataTO DataTo)
        {
            try
            {
                _AssemblyService.Update(id, DataTo.ToModel());
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
        [HttpPost("{assemblyId}/Concept/{conceptId}/{order}")]
        [ProducesResponseType(201, Type = typeof(int))]
        public IActionResult InsertIdea([FromRoute] int assemblyId, [FromRoute] int conceptId, [FromRoute] int order)
        {
            try
            {
                int id = _AssemblyService.InsertConcept(assemblyId, conceptId, order);
                return CreatedAtAction(nameof(LabelController.GetOneById), new { assemblyId = id }, new { id });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpDelete("{assemblyId}/Concept/{conceptId}")]
        [ProducesResponseType(201, Type = typeof(int))]
        public IActionResult RemoveIdea([FromRoute] int assemblyId, [FromRoute] int ConceptId)
        {
            try
            {
                if (_AssemblyService.RemoveConcept(assemblyId, ConceptId)) return NoContent();
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
        [HttpPut("{assemblyId}/Concept/{conceptId}/{order}")]
        [ProducesResponseType(201, Type = typeof(int))]
        public IActionResult MoveIdea([FromRoute] int AssemblyId, [FromRoute] int ConceptId, [FromRoute] int order)
        {
            try
            {
                if (_AssemblyService.MoveConcept(AssemblyId, ConceptId, order)) return NoContent();
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
        [HttpGet("Group/{groupId}")]
        [ActionName("GetAllForThisGroup")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AssemblyDTO>))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult GetAllForThisGroup([FromRoute] int groupId, string? withThis = null)
        {
            try
            {
                if (withThis is null || withThis.Replace(" ","") == "")
                    return Ok(_AssemblyService.GetAllForThisGroup(groupId).Select(c => c.ToDTO()));
                return Ok(_AssemblyService.GetAllForThisGroupWithCriteria(groupId, withThis).Select(c => c.ToDTO()));
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
