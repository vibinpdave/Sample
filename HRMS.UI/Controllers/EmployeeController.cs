using HRMS.UI.Models;
using HRMS.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.UI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string searchText = "")
        {
            var employees = await _employeeService.GetEmployeesAsync(pageNumber, pageSize, searchText, "FirstName", "asc");

            // Passing query parameters to the view
            ViewData["SearchText"] = searchText;

            return View(employees);
        }


        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                // Add the new employee (e.g., save to the database)
                 await _employeeService.AddEmployeeAsync(employee);

                // Optionally, you can redirect back to the list of employees or show a success message
                return RedirectToAction("Index");
            }

            // If there are validation errors, return the same view with error messages
            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.AddEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.UpdateEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
