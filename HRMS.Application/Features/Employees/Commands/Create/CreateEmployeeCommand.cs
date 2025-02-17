using HRMS.Domain.Enum;
using MediatR;

namespace HRMS.Application.Features.Employees.Commands.Create
{
    public class CreateEmployeeCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Status Status { get; set; }
        public string Password { get; set; }

        // Address properties
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

}
