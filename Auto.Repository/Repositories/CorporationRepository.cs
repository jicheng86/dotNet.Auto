using AutoMapper;
using AutoMapper.QueryableExtensions;

using Auto.IRepository.IRepositories;
using Auto.Model;
using Auto.Model.Dto;
using Auto.Model.Entities;

using Microsoft.EntityFrameworkCore.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Auto.Repository.Repositories
{
    public class CorporationRepository : RepositoryBase<Corporation>, ICorporationRepository
    {
        public CorporationRepository(EFDbContext context, IMapper mapper) : base(context)
        {
            Mapper = mapper;
        }

        public IMapper Mapper { get; }

        public bool IsUseful(Corporation corporation)
        {
            if (corporation == null || corporation.IsActive != true || corporation.IsDeleted != false)
            {
                return false;
            }
            return true;
        }

        public PageData<CorporationDto> LoadPageDataList(Expression<Func<Corporation, bool>> whereLambda,
                                                         Expression<Func<Corporation, object>> orderLambda,
                                                         int pageIndex,
                                                         int pageSize,
                                                         bool isDesc = false)
        {
            if (whereLambda == null)
            {
                whereLambda = w => true;
            }
            var dataSource = dbContext.Corporations.Where(whereLambda);

            PageData<CorporationDto> resualData = new PageData<CorporationDto>();
            if (dataSource == null || !dataSource.Any())
            {
                return resualData;
            }
            if (orderLambda != null)
            {
                dataSource = isDesc ? dataSource.OrderByDescending(orderLambda) : dataSource.OrderByDescending(orderLambda);
            }
            resualData.Total = dataSource.Count();
            dataSource = dataSource.Skip(pageIndex).Take(pageSize);
            //resualData.Data = dataSource.ProjectTo<CorporationDto>(configuration: Mapper.ConfigurationProvider).ToList();
            resualData.Data = Mapper.ProjectTo<CorporationDto>(dataSource).ToList();
            return resualData;
        }


    }
}
