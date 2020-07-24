using Auto.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auto.IService
{
    public interface IServiceBase<T> where T : class
    {
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        T Create(T entity);
        /// <summary>
        /// 是否已存在
        /// </summary>
        /// <param name="whereLambda">Lambda查询条件</param>
        /// <returns></returns>
        bool IsExisted(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="whereLambda">Lambda查询条件</param>
        /// <returns></returns>
        bool Delete(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool Delete(T entity);
        /// <summary>
        /// 修改并返回实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        T Update(T entity);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="whereLambda">Lambda查询条件</param>
        /// <returns></returns>
        T GetEntity(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="whereLambda">Lambda查询条件</param>
        /// <returns></returns>
        Task<T> GetEntityAsync(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="whereLambda">Lambda查询条件</param>
        /// <returns></returns>
        Task<IQueryable<T>> GetEntityListAsync(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 数据响应提交
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangeAsync();
    }
}
