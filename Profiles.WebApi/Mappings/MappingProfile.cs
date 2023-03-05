using AutoMapper;
using Events;
using Profiles.Domain.Entities;
using Profiles.Domain.Entities.ForeignEntities;
using Profiles.WebApi.Models.DTOs;

namespace Profiles.WebApi.Mappings; 
public class MappingProfile : Profile {
    public MappingProfile()
    {
        CreateMap<Doctor, DoctorDto>().ReverseMap();
        CreateMap<Patient, PatientDto>().ReverseMap();
        CreateMap<Receptionist, ReceptionistDto>().ReverseMap();

        CreateMap<Account, AccountCreated>().ReverseMap();
        CreateMap<Account, AccountUpdated>().ReverseMap();
    }
}
