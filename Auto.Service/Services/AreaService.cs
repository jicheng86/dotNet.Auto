using Auto.IRepository;
using Auto.IRepository.IRepositories;
using Auto.IService.IServices;
using Auto.Model;
using Auto.Model.Entities;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auto.Service.Services
{
    public class AreaService : ServiceBase<Area>, IAreaService
    {
        public AreaService(IAreaRepository repository) : base(repository)
        {
        }

        public Task<PageData<Area>> LoadPageDataListAsync2(Expression<Func<Area, bool>> whereLambda, Expression<Func<Area, object>> orderLambda, int pageIndex, int pageSize, bool isDesc = false)
        {
            throw new NotImplementedException();
        }
    }
}
