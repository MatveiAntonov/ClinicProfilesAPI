using AutoMapper;
using Profiles.Domain.Entities;
using Profiles.WebApi.Models.DTOs;

namespace Profiles.WebApi.Mappings; 
public class MappingProfile : Profile {
    public MappingProfile()
    {
        CreateMap<Doctor, DoctorDto>().ReverseMap();
        CreateMap<Patient, PatientDto>().ReverseMap();
        CreateMap<Receptionist, ReceptionistDto>().ReverseMap();
    }
}
