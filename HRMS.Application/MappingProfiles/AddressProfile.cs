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
            CreateMap<CreateEmployeeCommand, Address>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));

            CreateMap<EditEmployeeCommand, Address>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Address, AddressDto>(); // Map Address to AddressDto
        }
    }
}
