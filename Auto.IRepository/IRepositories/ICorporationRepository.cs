using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Auto.IRepository;
using Auto.Model;
using Auto.Model.Dto;
using Auto.Model.Entities;

namespace Auto.IRepository.IRepositories
{
    public interface ICorporationRepository : IRepositoryBase<Corporation>
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
        PageData<CorporationDto> LoadPageDataList(Expression<Func<CorporationDto, bool>> whereLambda, Expression<Func<CorporationDto, object>> orderLambda, int pageIndex, int pageSize, bool isDesc = false);

    }
}
