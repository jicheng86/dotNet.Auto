using Auto.Model;
using Auto.Model.Entities;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auto.IRepository.IRepositories
{
    /// <summary>
    /// 员工仓储接口
    /// </summary>
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        Task<Employee> DoSelf(Employee entity);
    }
}
