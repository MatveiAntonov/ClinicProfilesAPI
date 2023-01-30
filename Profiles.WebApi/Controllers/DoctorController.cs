using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Profiles.Domain.Entities;
using Profiles.Domain.Entities.ForeignEntities;
using Profiles.Domain.Interfaces.Services;
using Profiles.WebApi.Models.DTOs;

namespace Profiles.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        public DoctorController(IDoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctors();
            if (doctors is not null)
            {
                var doctorsDto = new List<DoctorDto>();
                foreach (var doctor in doctors)
                {
                    doctorsDto.Add(_mapper.Map<DoctorDto>(doctor));
                }
                return Ok(doctorsDto);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetDoctor(int id)
        {
            var doctor = await _doctorService.GetDoctorById(id);
            if (doctor is not null)
            {
                var doctorDto = _mapper.Map<DoctorDto>(doctor);
                return Ok(doctorDto);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDto>> CreateDoctor([FromForm] DoctorDto doctorDto)
        {
            if (doctorDto == null)
                return BadRequest();

            var doctorEntity = _mapper.Map<Doctor>(doctorDto);

            var doctor = await _doctorService.CreateDoctor(doctorEntity);
            if (doctor is not null)
            {
                var doctorReturn = _mapper.Map<DoctorDto>(doctor);
                return Created($"/doctor/{doctorReturn.Id}", doctorReturn);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult<DoctorDto>> UpdateOffice([FromForm] DoctorDto doctorDto)
        {
            if (doctorDto == null)
                return BadRequest();

            var doctorEntity = _mapper.Map<Doctor>(doctorDto);

            var doctor = await _doctorService.UpdateDoctor(doctorEntity);
            if (doctor is not null)
            {
                var doctorResult = _mapper.Map<DoctorDto>(doctor);
                return Ok(doctorResult);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DoctorDto>> DeleteOffice(int id)
        {
            if (_doctorService.GetDoctorById(id) == null)
            {
                return BadRequest();
            };

            var doctor = await _doctorService.DeleteDoctor(id);
            if (doctor is null)
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
