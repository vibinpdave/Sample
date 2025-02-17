using HRMS.Application.DTOs;
using HRMS.Application.Responses;
using MediatR;

namespace HRMS.Application.Features.Employees.Queries.GetAll
{
    public class GetEmployeesQuery : IRequest<PagedResult<EmployeeDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SearchText { get; set; } = string.Empty;
        public string SortColumn { get; set; } = "FirstName";
        public string SortOrder { get; set; } = "asc";
    }
}
