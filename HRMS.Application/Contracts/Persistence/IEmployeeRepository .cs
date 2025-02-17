using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Contracts.Persistence
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee> GetUserByUsernameAndPasswordAsync(string username, string password);
        Task<Employee> GetEmployeeWithAddressByIdAsync(int id);
        IQueryable<Employee> GetQueryable(bool includeAddress = false);
    }
}
