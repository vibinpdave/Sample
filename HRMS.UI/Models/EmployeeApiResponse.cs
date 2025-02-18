namespace HRMS.UI.Models
{
    public class EmployeeApiResponse
    {
        public int TotalCount { get; set; }
        public List<EmployeeViewModel> Items { get; set; }
    }

}
