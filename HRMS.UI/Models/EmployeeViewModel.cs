namespace HRMS.UI.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Status { get; set; }
        public string Password { get; set; }

        // Address Property
        public AddressViewModel Address { get; set; }
    }
}
