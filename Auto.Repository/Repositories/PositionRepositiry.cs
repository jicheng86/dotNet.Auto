using Auto.IRepository.IRepositories;
using Auto.Model;
using Auto.Model.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Auto.Repository.Repositories
{
    public class PositionRepositiry : RepositoryBase<Position>, IPositionRepository
    {
        public PositionRepositiry(EFDbContext context) : base(context)
        {
        }
    }
}
