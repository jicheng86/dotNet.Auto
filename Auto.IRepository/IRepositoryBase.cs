using Auto.Model;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Auto.IRepository
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    public interface IRepositoryBase<T> where T : class, new()
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
        /// 修改实体
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
