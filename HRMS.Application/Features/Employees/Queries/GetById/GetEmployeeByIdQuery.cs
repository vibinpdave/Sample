using HRMS.Application.DTOs;
using MediatR;

namespace HRMS.Application.Features.Employees.Query.GetById
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDto>
    {
        public int Id { get; set; }

        public GetEmployeeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
