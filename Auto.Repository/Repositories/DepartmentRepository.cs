using Auto.IRepository.IRepositories;
using Auto.Model;
using Auto.Model.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Auto.Repository.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(EFDbContext context) : base(context)
        {
        }
    }
}
