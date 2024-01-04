using BLL.CustomExceptions;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mind_Master_Backend.DTOs;
using Mind_Master_Backend.Mappers;
using System.Data;

namespace Mind_Master_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptController : ControllerBase
    {
        private ConceptService _ConceptServices;

        public ConceptController(ConceptService conceptServices)
        {
            _ConceptServices = conceptServices;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ConceptDTO>))]
        public IActionResult GetAll()
        {
            IEnumerable<ConceptDTO> result = _ConceptServices.GetAll().Select(c => c.ToDTO());
            return Ok(result);
        }
        [HttpGet("{conceptId}")]
        [ActionName("GetOneById")]
        [ProducesResponseType(200, Type = typeof(ConceptDTO))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult GetOneById([FromRoute] int conceptId)
        {
            try
            {
                return Ok(_ConceptServices.GetOneById(conceptId).ToDTO());
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<ConceptDTO>))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult GetAllWithTitle([FromRoute] string title)
        {
            try
            {
                return Ok(_ConceptServices.GetByTitle(title).Select(c => c.ToDTO()));
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<ConceptDTO>))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult GetAllWithLabel([FromRoute] int labelId)
        {
            try
            {
                return Ok(_ConceptServices.GetByLabel(labelId).Select(c => c.ToDTO()));
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
                if (_ConceptServices.Delete(id)) return NoContent();
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
        public IActionResult Create([FromBody] ConceptDataTO concept)
        {
            try
            {
                int id = _ConceptServices.Create(concept.ToModel()).Id;
                return CreatedAtAction(nameof(LabelController.GetOneById), new { labelId = id }, new { id });
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
        public IActionResult Update([FromRoute] int id, [FromBody] ConceptDataTO DataTo)
        {
            try
            {
                _ConceptServices.Update(id, DataTo.ToModel());
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
        [HttpPost("{conceptId}/Idea/{ideaId}/{order}")]
        [ProducesResponseType(201, Type = typeof(int))]
        public IActionResult InsertIdea([FromRoute] int conceptId, [FromRoute] int ideaId, [FromRoute] int order)
        {
            try
            {
                int id = _ConceptServices.InsertIdea(conceptId, ideaId, order);
                return CreatedAtAction(nameof(LabelController.GetOneById), new { conceptId = id }, new { id });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpDelete("{conceptId}/Idea/{ideaId}")]
        [ProducesResponseType(201, Type = typeof(int))]
        public IActionResult RemoveIdea([FromRoute] int conceptId, [FromRoute] int ideaId)
        {
            try
            {
                if (_ConceptServices.RemoveIdea(conceptId, ideaId)) return NoContent();
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
        [HttpPut("{conceptId}/Idea/{ideaId}/{order}")]
        [ProducesResponseType(201, Type = typeof(int))]
        public IActionResult MoveIdea([FromRoute] int conceptId, [FromRoute] int ideaId, [FromRoute] int order)
        {
            try
            {
                if (_ConceptServices.MoveIdea(conceptId, ideaId, order)) return NoContent();
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
