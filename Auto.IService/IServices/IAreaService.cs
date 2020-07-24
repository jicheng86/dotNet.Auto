using Auto.Model;
using Auto.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auto.IService.IServices
{
    public interface IAreaService : IServiceBase<Area>
    {
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="whereLambda">lambda查询条件</param>
        /// <param name="orderLambda">Lambda排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="total">查询数据总条数</param>
        /// <returns>分页数据集合</returns>
        Task<PageData<Area>> LoadPageDataListAsync2(Expression<Func<Area, bool>> whereLambda, Expression<Func<Area, object>> orderLambda, int pageIndex, int pageSize, bool isDesc = false);
    }
}
