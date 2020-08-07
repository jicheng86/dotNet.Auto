using Auto.IRepository;
using Auto.IRepository.IRepositories;
using Auto.IService.IServices;
using Auto.Model;
using Auto.Model.Dto;
using Auto.Model.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auto.Service.Services
{
    public class CorporationService : ServiceBase<Corporation>, ICorporationService
    {
        public CorporationService(ICorporationRepository repository) : base(repository)
        {
            corporationRepository = repository;
        }

        public ICorporationRepository corporationRepository { get; }

        public PageData<CorporationDto> LoadPageDataList(Expression<Func<CorporationDto, bool>> whereLambda,
                                                         Expression<Func<CorporationDto, object>> orderLambda,
                                                         int pageIndex, int pageSize, bool isDesc = false)
        {
            return corporationRepository.LoadPageDataList(whereLambda, orderLambda, pageIndex, pageSize, isDesc);
        }
        /// <summary>
        /// 是否已存在
        /// </summary>
        /// <param name="corporation">实体</param>
        /// <returns></returns>
        public bool IsExisted(Corporation corporation)
        {
            if (corporation == null)
            {
                return true;
            }
            if (IsExisted(c => c.Name == corporation.Name && c.IsDeleted == false && c.IsActive == true))
            {
                return true;
            }
            return false;
        }
    }
}
