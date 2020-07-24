using Auto.IRepository.IRepositories;
using Auto.Model.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace Auto.Repository.Repositories
{
    public class AreaRepository : RepositoryBase<Area>, IAreaRepository
    {
        public AreaRepository(EFDbContext context) : base(context)
        {
        }
    }
}
