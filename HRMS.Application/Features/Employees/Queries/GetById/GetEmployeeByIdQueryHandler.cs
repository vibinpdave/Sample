using AutoMapper;
using HRMS.Application.Contracts.Persistence;
using HRMS.Application.DTOs;
using HRMS.Application.Features.Employees.Query.GetById;
using HRMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.Employees.Queries.GetById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository,IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeWithAddressByIdAsync(request.Id);

            if (employee == null)
            {
                return null;
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }
    }
}
