
using Auto.IRepository.IRepositories;
using Auto.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auto.Repository.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EFDbContext context) : base(context)
        {
        }

        public Task<Employee> DoSelf(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
