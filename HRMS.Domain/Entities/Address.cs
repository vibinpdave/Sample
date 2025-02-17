using HRMS.Domain.Common;

namespace HRMS.Domain.Entities
{
    public class Address: BaseEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        // Navigation Property (if required)
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
