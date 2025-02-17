using HRMS.Application.Features.Employees.Queries.GetById;
using HRMS.Domain.Enum;

namespace HRMS.Application.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Status Status { get; set; } // Status as a string representation of the Enum
        public AddressDto Address { get; set; }
    }
}
