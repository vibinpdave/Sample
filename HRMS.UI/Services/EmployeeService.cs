using HRMS.UI.Models;
using System.Text;
using System.Text.Json;

namespace HRMS.UI.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public EmployeeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"] + "/employee";
        }

        // Get all employees with address
        public async Task<PagedResult<EmployeeViewModel>> GetEmployeesAsync(int pageNumber, int pageSize, string searchText, string sortColumn, string sortOrder)
        {
            var url = $"{_apiBaseUrl}?pageNumber={pageNumber}&pageSize={pageSize}&searchText={searchText}&sortColumn={sortColumn}&sortOrder={sortOrder}";
            var response = await _httpClient.GetStringAsync(url);

            var employeeApiResponse = JsonSerializer.Deserialize<EmployeeApiResponse>(response,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return new PagedResult<EmployeeViewModel>
            {
                Items = employeeApiResponse?.Items ?? new List<EmployeeViewModel>(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = employeeApiResponse?.TotalCount ?? 0
            };
        }


        // Get employee by ID (with address)
        public async Task<EmployeeViewModel> GetEmployeeByIdAsync(int id)
        {
            var response = await _httpClient.GetStringAsync($"{_apiBaseUrl}/{id}");
            return JsonSerializer.Deserialize<EmployeeViewModel>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // Add employee with address
        public async Task<bool> AddEmployeeAsync(EmployeeViewModel employee)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiBaseUrl, jsonContent);
            return response.IsSuccessStatusCode;
        }

        // Update employee with address
        public async Task<bool> UpdateEmployeeAsync(EmployeeViewModel employee)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{employee.Id}", jsonContent);
            return response.IsSuccessStatusCode;
        }

        // Soft Delete employee
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var response = await _httpClient.PatchAsync($"{_apiBaseUrl}/softdelete/{id}", null);
            return response.IsSuccessStatusCode;
        }
    }
}
