using BLL.CustomExceptions;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mind_Master_Backend.DTOs;
using Mind_Master_Backend.Mappers;

namespace Mind_Master_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private LabelService _LabelServices;

        public LabelController(LabelService labelServices)
        {
            _LabelServices = labelServices;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LabelDTO>))]
        public IActionResult GetAll()
        {
            IEnumerable<LabelDTO> result = _LabelServices.GetAll().Select(l => l.ToDTO());
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(int))]
        public IActionResult Create([FromBody] LabelDataTO label)
        {
            try
            {
                int id = _LabelServices.Create(label.ToModel()).Id;
                return CreatedAtAction(nameof(LabelController.GetOneById), new { labelId = id }, new { id });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpGet("{labelId}")]
        [ActionName("GetOneById")]
        [ProducesResponseType(200, Type = typeof(LabelDTO))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult GetOneById([FromRoute] int labelId)
        {
            try
            {
                return Ok(_LabelServices.GetOneById(labelId).ToDTO());
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
        public IActionResult Update([FromRoute] int id, [FromBody] LabelDataTO DataTo)
        {
            try
            {
                _LabelServices.Update(id, DataTo.ToModel());
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
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                if (_LabelServices.Delete(id)) return NoContent();
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
        [HttpPost("{labelId}/Concept/{conceptId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult LinkLabelToConcept([FromRoute] int labelId, [FromRoute] int conceptId)
        {
            try
            {
                if (_LabelServices.LinkLabelToConcept(labelId, conceptId)) return NoContent();
                return BadRequest();
            }
            catch (NotFoundException nFException)
            {
                return NotFound(nFException.Message);
            }
            catch (BadRequestException bRException)
            {
                return NotFound(bRException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpPost("{labelId}/Assembly/{assemblyId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult LinkLabelToAssembly([FromRoute] int labelId, [FromRoute] int assemblyId)
        {
            try
            {
                if (_LabelServices.LinkLabelToAssembly(labelId, assemblyId)) return NoContent();
                return BadRequest();
            }
            catch (NotFoundException nFException)
            {
                return NotFound(nFException.Message);
            }
            catch (BadRequestException bRException)
            {
                return NotFound(bRException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpDelete("{labelId}/Concept/{conceptId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult DetachLabelFromConcept([FromRoute] int labelId, [FromRoute] int conceptId)
        {
            try
            {
                if (_LabelServices.DetachLabelFromConcept(labelId, conceptId)) return NoContent();
                return BadRequest();
            }
            catch (NotFoundException nFException)
            {
                return NotFound(nFException.Message);
            }
            catch (BadRequestException bRException)
            {
                return NotFound(bRException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpDelete("{labelId}/Assembly/{assemblyId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult DetachFromAssembly([FromRoute] int labelId, [FromRoute] int assemblyId)
        {
            try
            {
                if (_LabelServices.DetachLabelFromAssembly(labelId, assemblyId)) return NoContent();
                return BadRequest();
            }
            catch (NotFoundException nFException)
            {
                return NotFound(nFException.Message);
            }
            catch (BadRequestException bRException)
            {
                return NotFound(bRException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
