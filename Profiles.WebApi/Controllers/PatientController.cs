using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profiles.Application.Services;
using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces.Services;
using Profiles.WebApi.Models.DTOs;
using System.Data;

namespace Profiles.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        public PatientController(IPatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatients();
        if (patients is not null)
        {
            var patientsDto = new List<PatientDto>();
            foreach (var patient in patients)
            {
                patientsDto.Add(_mapper.Map<PatientDto>(patient));
            }
        
            return Ok(patientsDto);
        }
        else
        {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetPatient(int id)
        {
            var patient = await _patientService.GetPatientById(id);
            if (patient is not null)
            {
                var patientDto = _mapper.Map<PatientDto>(patient);
                return Ok(patientDto);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        //[Authorize(Roles = "Receptionist")]
        public async Task<ActionResult<PatientDto>> CreatePatient([FromForm] PatientDto patientDto)
        {
            if (patientDto == null)
                return BadRequest();

            var patientEntity = _mapper.Map<Patient>(patientDto);

            var patient = await _patientService.CreatePatient(patientEntity);
            if (patient is not null)
            {
                var patientReturn = _mapper.Map<PatientDto>(patient);
                return Created($"/patient/{patientReturn.Id}", patientReturn);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        //[Authorize(Roles = "Receptionist")]
        public async Task<ActionResult<PatientDto>> UpdateOffice([FromForm] PatientDto patientDto)
        {
            if (patientDto == null)
                return BadRequest();

            var patientEntity = _mapper.Map<Patient>(patientDto);

            var patient = await _patientService.UpdatePatient(patientEntity);
            if (patient is not null)
            {
                var patientResult = _mapper.Map<PatientDto>(patient);
                return Ok(patientResult);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Receptionist")]
        public async Task<ActionResult<PatientDto>> DeleteOffice(int id)
        {
            if (_patientService.GetPatientById(id) == null)
            {
                return BadRequest();
            };

            var patient = await _patientService.DeletePatient(id);
            if (patient is not null)
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
