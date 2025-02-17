using HRMS.Application.Contracts.Persistence;
using HRMS.Application.Exceptions;
using HRMS.Domain.Entities;
using MediatR;


namespace HRMS.Application.Features.Employees.Commands.SoftDelete
{
    public class SoftDeleteEmployeeCommandHandler : IRequestHandler<SoftDeleteEmployeeCommand,bool>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public SoftDeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(SoftDeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null)
            {
                throw new NotFoundException(nameof(Employee), request.EmployeeId);
            }
            employee.Status = Domain.Enum.Status.Inactive;
            await _employeeRepository.UpdateAsync(employee);

            return true; 
        }
    }

}
