using Auto.IRepository;
using Auto.Model;
using Auto.Model.Extend;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auto.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected readonly EFDbContext dbContext;
        private DbContext context;

        /// <summary>
        /// 实现类初始化时：初始化数据库上下文
        /// </summary>
        /// <param name="context"></param>
        public RepositoryBase(EFDbContext context)
        {
            this.dbContext = context;
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public T Create(T entity)
        {
            dbContext.Set<T>().Add(entity);
            return entity;
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="whereLambda">Lambda查询条件</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<T, bool>> whereLambda)
        {
            if (whereLambda == null)
            {
                throw new ArgumentNullException(nameof(whereLambda));
            }
            T entity = GetEntity(whereLambda);

            return entity != null && Update(entity) != null;
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体,SoftDelete</param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            return entity == null ? false : Update(entity) != null;
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="whereLambda">Lambda查询条件</param>
        /// <returns></returns>
        public T GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            if (whereLambda == null)
            {
                whereLambda = w => true;
            }
            T entity = dbContext.Set<T>().Where(whereLambda).FirstOrDefault();
            return entity;
        }
        /// <summary>
        /// 获取实体（异步）
        /// </summary>
        /// <param name="whereLambda">Lambda查询条件</param>
        /// <returns></returns>
        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> whereLambda)
        {
            if (whereLambda == null)
            {
                whereLambda = w => true;
            }
            return await dbContext.Set<T>().Where(whereLambda).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 获取实体列表（异步）
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<IQueryable<T>> GetEntityListAsync(Expression<Func<T, bool>> whereLambda)
        {
            if (whereLambda == null)
            {
                whereLambda = w => true;
            }
            return await Task.Run(() => dbContext.Set<T>().Where(whereLambda));
        }
     

        /// <summary>
        /// 实体是否已存在
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public bool IsExisted(Expression<Func<T, bool>> whereLambda)
        {
            if (whereLambda == null)
            {
                whereLambda = w => true;
            }
            var entity = GetEntityAsync(whereLambda).Result;
            return entity != null;
        }
 
        /// <summary>
        /// 数据响应提交
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangeAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 修改单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Update(T entity)
        {
            return entity;
        }

    }
}
