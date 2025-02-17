using Azure;
using HRMS.Application.Features.Employees.Commands.Create;
using HRMS.Application.Features.Employees.Commands.EditEmployee;
using HRMS.Application.Features.Employees.Commands.SoftDelete;
using HRMS.Application.Features.Employees.Queries.GetAll;
using HRMS.Application.Features.Employees.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeCommand command)
        {
            // Validate the command using FluentValidation
            var result = await _mediator.Send(command);
            if (result <= 0)
            {
                return BadRequest("Employee creation failed.");
            }
            return Ok(result);
            //return CreatedAtAction(nameof(GetEmployeeById), new { id = result }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEmployee([FromBody] EditEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPatch("soft-delete/{employeeId}")]
        public async Task<IActionResult> SoftDeleteEmployee(int employeeId)
        {
            try
            {
                var command = new SoftDeleteEmployeeCommand { EmployeeId = employeeId };
                // Send the command to MediatR
                await _mediator.Send(command);

                return Ok(new { Message = "Employee marked as inactive successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] GetEmployeesQuery queryParams)
        {
            var query = new GetEmployeesQuery
            {
                PageNumber = queryParams.PageNumber,
                PageSize = queryParams.PageSize,
                SearchText = queryParams.SearchText,
                SortColumn = queryParams.SortColumn,
                SortOrder = queryParams.SortOrder
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

    }
}
