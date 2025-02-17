using MediatR;

namespace HRMS.Application.Features.Employees.Commands.SoftDelete
{
    public class SoftDeleteEmployeeCommand : IRequest<bool>
    {
        public int EmployeeId { get; set; }
    }

}
