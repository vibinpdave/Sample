using HRMS.Domain.Common;
using HRMS.Domain.Enum;

namespace HRMS.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Status Status { get; set; }
        public string Password { get; set; }

        // Foreign Key for Address
        public int AddressId { get; set; }

        // Navigation Property
        public Address Address { get; set; }
    }
}
