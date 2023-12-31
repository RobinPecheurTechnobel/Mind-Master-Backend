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
    public class GroupController : ControllerBase
    {
        private GroupService _GroupService;

        public GroupController(GroupService groupeService)
        {
            _GroupService = groupeService;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GroupDTO>))]
        public IActionResult GetAll()
        {
            IEnumerable<GroupDTO> result = _GroupService.GetAll().Select(a => a.ToDTO());
            return Ok(result);
        }
        [HttpGet("{groupId}")]
        [ProducesResponseType(200, Type = typeof(GroupDTO))]
        [ProducesResponseType(404, Type = typeof(IEnumerable<string>))]
        public IActionResult GetOneById(int groupId)
        {
            try
            {
                GroupDTO result = _GroupService.GetOneById(groupId).ToDTO();
                return Ok(result);
            }
            catch (NotFoundException nFException)
            {
                return NotFound(nFException.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(201,Type = typeof(int))]
        public IActionResult Create([FromBody] GroupDataToObject group)
        {
            try
            {
                int id = _GroupService.Create(group.ToNewModel()).Id;
                return CreatedAtAction(nameof(GroupController.GetOneById), new { groupId = id }, new { id });
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpDelete("{groupId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Delete([FromRoute] int groupId)
        {
            try
            {
                if (_GroupService.Delete(groupId)) return NoContent();
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

        [HttpPut("{groupId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Update([FromRoute] int groupId, [FromBody] GroupDataToObject DataTo)
        {
            try
            {
                _GroupService.Update(groupId, DataTo.ToNewModel());
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
        [HttpGet("Thinker/{groupId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GroupThinkerInGroupDTO>))]
        [ProducesResponseType(404, Type = typeof(IEnumerable<string>))]
        public IActionResult GetThinkerByGroupId(int groupId)
        {
            try
            {
                IEnumerable<GroupThinkerInGroupDTO> result = _GroupService.getThinkers(groupId).Select(gt => gt.ToInternalThinkerDTO());
                return Ok(result);
            }
            catch (NotFoundException nFException)
            {
                return NotFound(nFException.Message);
            }
        }
        [HttpPost("{groupId}/Thinker/{thinkerId}")]
        [ProducesResponseType(201, Type = typeof(int))]
        [ProducesResponseType(404, Type = typeof(IEnumerable<string>))]
        public IActionResult AddThinkerToGroup([FromRoute] int groupId, [FromRoute] int thinkerId)
        {
            try
            {
                int id = _GroupService.AddThinkerToGroup(groupId, thinkerId);
                return CreatedAtAction(nameof(GroupController.GetThinkerByGroupId), new { groupId = groupId }, new { id });
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

        [HttpDelete("{groupId}/Thinker/{thinkerId}")]
        [ProducesResponseType(204, Type = typeof(int))]
        [ProducesResponseType(404, Type = typeof(IEnumerable<string>))]
        public IActionResult RemoveThinkerToGroup([FromRoute] int groupId, [FromRoute] int thinkerId)
        {
            try
            {
                if(_GroupService.RemoveThinkerToGroup(groupId, thinkerId))return NoContent();
                return NotFound();
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
