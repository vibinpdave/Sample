using AutoMapper;
using HRMS.Application.DTOs;
using HRMS.Application.Features.Employees.Commands.Create;
using HRMS.Application.Features.Employees.Commands.EditEmployee;
using HRMS.Domain.Entities;

namespace HRMS.Application.MappingProfiles
{
    public class AddressProfile: Profile
    {
        public AddressProfile()
        {
            // Create Address Mapping
            CreateMap<CreateEmployeeCommand, Domain.Entities.Address>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country));

            CreateMap<EditEmployeeCommand, Domain.Entities.Address>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Domain.Entities.Address, AddressDto>(); // Map Address to AddressDto
        }
    }
}
