using AutoMapper;
using HRMS.Application.Contracts.Persistence;
using HRMS.Application.Exceptions;
using HRMS.Domain.Entities;
using MediatR;

namespace HRMS.Application.Features.Employees.Commands.EditEmployee
{
    public class EditEmployeeCommandHandler : IRequestHandler<EditEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper; // Inject AutoMapper

        public EditEmployeeCommandHandler(IEmployeeRepository employeeRepository,
                                          IAddressRepository addressRepository,
                                          IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Retrieve and update the employee
            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            if (employee == null)
            {
                throw new NotFoundException(nameof(Employee), request.Id);
            }

            // Retrieve the address to update
            var address = await _addressRepository.GetByIdAsync(request.AddressId);
            if (address == null)
            {
                throw new NotFoundException(nameof(Address), request.AddressId);
            }

            // Map the EditEmployeeCommand to Employee entity (mapping employee-specific fields)
            _mapper.Map(request, employee);
            // Map the EditEmployeeCommand to Address entity (mapping address fields)
            _mapper.Map(request, address);

            // Save both the address and employee
            await _addressRepository.UpdateAsync(address);
            await _employeeRepository.UpdateAsync(employee);

            return employee.Id;  // Return the updated employee's ID
        }
    }

}
