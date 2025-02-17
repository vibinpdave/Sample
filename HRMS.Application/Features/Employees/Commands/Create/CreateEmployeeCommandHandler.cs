using AutoMapper;
using HRMS.Application.Contracts.Persistence;
using HRMS.Application.Exceptions;
using HRMS.Domain.Constants;
using HRMS.Domain.Entities;
using MediatR;

namespace HRMS.Application.Features.Employees.Commands.Create
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper; // Inject AutoMapper
        public CreateEmployeeCommandHandler(IAddressRepository addressRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEmployeeCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Any())
                throw new BadRequestException(ErrorMessages.Invalid, validationResult);

            // Create the address using the AddressRepository
            var address = _mapper.Map<Address>(request);
            await _addressRepository.CreateAsync(address);

            // Now create the employee and associate it with the newly created address
            var employee = _mapper.Map<Employee>(request);
            employee.AddressId = address.Id;

            await _employeeRepository.CreateAsync(employee);
            return employee.Id;
        }
    }
}
