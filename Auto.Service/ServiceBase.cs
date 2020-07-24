using Auto.IRepository;
using Auto.IService;
using Auto.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auto.Service
{
    /// <summary>
    /// 基层服务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ServiceBase<T> : IServiceBase<T> where T : class, new()
    {
        /// <summary>
        /// 通过在子类的构造函数中注入，这里是基类，不用构造函数
        /// 构造函数注入
        /// </summary>
        protected IRepositoryBase<T> Repository { get; set; }

        public ServiceBase(IRepositoryBase<T> repository)
        {
            Repository = repository;
        }
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Create(T entity)
        {
            return Repository.Create(entity);
        }
        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<T, bool>> whereLambda)
        {
            return Repository.Delete(whereLambda);
        }
       /// <summary>
       /// 删除实体
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
        public bool Delete(T entity)
        {
            return Repository.Delete(entity);
        }

        public T GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            return Repository.GetEntity(whereLambda);
        }

        public Task<T> GetEntityAsync(Expression<Func<T, bool>> whereLambda)
        {
            return Repository.GetEntityAsync(whereLambda);
        }

        public Task<IQueryable<T>> GetEntityListAsync(Expression<Func<T, bool>> whereLambda)
        {
            return Repository.GetEntityListAsync(whereLambda);
        }
        /// <summary>
        /// 数据响应提交
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangeAsync()
        {
            return Repository.SaveChangeAsync();
        }
        /// <summary>
        /// 修改并返回实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Update(T entity)
        {
            return Repository.Update(entity);
        }
        /// <summary>
        /// 是否已存在
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public bool IsExisted(Expression<Func<T, bool>> whereLambda)
        {
            return Repository.IsExisted(whereLambda);
        }
    }
}
