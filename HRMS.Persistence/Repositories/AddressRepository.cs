using HRMS.Application.Contracts.Persistence;
using HRMS.Domain.Entities;
using HRMS.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Persistence.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(HrDatabaseContext context) : base(context)
        {
        }
    }
}
