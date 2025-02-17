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
            // Create Employee Mapping
            CreateMap<CreateEmployeeCommand, Employee>();

            // Map EditEmployeeCommand to Employee
            CreateMap<EditEmployeeCommand, Employee>();

            CreateMap<Employee, EmployeeDto>(); 

        }
    }
}
