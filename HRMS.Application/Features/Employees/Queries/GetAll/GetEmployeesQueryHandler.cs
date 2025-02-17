using AutoMapper;
using HRMS.Application.Contracts.Persistence;
using HRMS.Application.DTOs;
using HRMS.Application.Responses;
using MediatR;
using System.Linq;

namespace HRMS.Application.Features.Employees.Queries.GetAll
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, PagedResult<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var query = _employeeRepository.GetQueryable(includeAddress: true);

            // Apply search filter if SearchText is provided
            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                query = query.Where(e =>
                    e.FirstName.Contains(request.SearchText) ||
                    e.LastName.Contains(request.SearchText) ||
                    e.Email.Contains(request.SearchText) ||
                    (e.Address != null &&
                     (e.Address.Street.Contains(request.SearchText) ||
                      e.Address.City.Contains(request.SearchText) ||
                      e.Address.State.Contains(request.SearchText) ||
                      e.Address.PostalCode.Contains(request.SearchText) ||
                      e.Address.Country.Contains(request.SearchText))
                    )
                );
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(request.SortColumn))
            {
                switch (request.SortColumn.ToLower())
                {
                    case "firstname":
                        query = request.SortOrder.ToLower() == "asc" ? query.OrderBy(e => e.FirstName) : query.OrderByDescending(e => e.FirstName);
                        break;
                    case "lastname":
                        query = request.SortOrder.ToLower() == "asc" ? query.OrderBy(e => e.LastName) : query.OrderByDescending(e => e.LastName);
                        break;
                    case "email":
                        query = request.SortOrder.ToLower() == "asc" ? query.OrderBy(e => e.Email) : query.OrderByDescending(e => e.Email);
                        break;
                    // Add more columns if needed
                    default:
                        query = query.OrderBy(e => e.FirstName); // Default sorting
                        break;
                }
            }

            // Get total count
            var totalCount = query.Count();

            // Apply pagination (Skip and Take)
            var employees =  query
                .Skip((request.PageNumber - 1) * request.PageSize)  // Skip previous pages' data
                .Take(request.PageSize) // Take only the desired number of items
                .ToList();

            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            return new PagedResult<EmployeeDto>
            {
                TotalCount = totalCount,
                Items = employeeDtos
            };
        }
    }
}
