using HRMS.Application.Contracts.Persistence;
using HRMS.Domain.Entities;
using HRMS.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;  // for async LINQ methods like CountAsync and ToListAsync

namespace HRMS.Persistence.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(HrDatabaseContext context) : base(context)
        {
        }
        public async Task<Employee?> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            return await _context.Employees
                .Where(e => e.Email == username && e.Password == password) // Adjust this to your password storage strategy
                .FirstOrDefaultAsync();
        }
        public async Task<Employee?> GetEmployeeWithAddressByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Address) // Include the Address
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public IQueryable<Employee> GetQueryable(bool includeAddress = false)
        {
            var query = _context.Employees.AsQueryable();

            if (includeAddress)
            {
                query = query.Include(e => e.Address);  // Eagerly load Address
            }

            return query;
        }
    }
}
