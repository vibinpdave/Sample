using AutoMapper;
using HRMS.Application.DTOs;
using HRMS.Application.Features.Employees.Commands.Create;
using HRMS.Application.Features.Employees.Commands.EditEmployee;
using HRMS.Domain.Entities;

namespace HRMS.Application.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeCommand, Employee>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                 .ForMember(dest => dest.Address, opt => opt.Ignore());

            // Map EditEmployeeCommand to Employee
            CreateMap<EditEmployeeCommand, Employee>();

            CreateMap<Employee, EmployeeDto>(); 

        }
    }
}
