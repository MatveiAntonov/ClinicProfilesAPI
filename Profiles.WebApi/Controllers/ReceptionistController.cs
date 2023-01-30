using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Profiles.Application.Services;
using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces.Services;
using Profiles.WebApi.Models.DTOs;

namespace Profiles.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ReceptionistController : ControllerBase
    {
        private readonly IReceptionistService _receptionistService;
        private readonly IMapper _mapper;
        public ReceptionistController(IReceptionistService receptionistService, IMapper mapper)
        {
            _receptionistService = receptionistService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceptionistDto>>> GetAllReceptionists()
        {
            var receptionists = await _receptionistService.GetAllReceptionists();
            if (receptionists is not null)
            {
                var receptionistsDto = new List<ReceptionistDto>();
                foreach (var receptionist in receptionists)
                {
                    receptionistsDto.Add(_mapper.Map<ReceptionistDto>(receptionist));
                }
                return Ok(receptionistsDto);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReceptionistDto>> GetReceptionist(int id)
        {
            var receptionist = await _receptionistService.GetReceptionistById(id);
            if (receptionist is not null)
            {
                var receptionistDto = _mapper.Map<ReceptionistDto>(receptionist);
                return Ok(receptionistDto);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReceptionistDto>> CreateReceptionist([FromForm] ReceptionistDto receptionistDto)
        {
            if (receptionistDto == null)
                return BadRequest();

            var receptionistEntity = _mapper.Map<Receptionist>(receptionistDto);

            var receptionist = await _receptionistService.CreateReceptionist(receptionistEntity);
            if (receptionist is not null)
            {
                var receptionistReturn = _mapper.Map<ReceptionistDto>(receptionist);
                return Created($"/receptionist/{receptionistReturn.Id}", receptionistReturn);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult<ReceptionistDto>> UpdateOffice([FromForm] ReceptionistDto receptionistDto)
        {
            if (receptionistDto == null)
                return BadRequest();

            var receptionistEntity = _mapper.Map<Receptionist>(receptionistDto);

            var receptionist = await _receptionistService.UpdateReceptionist(receptionistEntity);
            if (receptionist is not null)
            {
                var receptionistResult = _mapper.Map<ReceptionistDto>(receptionist);
                return Ok(receptionistResult);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ReceptionistDto>> DeleteOffice(int id)
        {
            if (_receptionistService.GetReceptionistById(id) == null)
            {
                return BadRequest();
            };

            var receptionist = await _receptionistService.DeleteReceptionist(id);
            if (receptionist is null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
