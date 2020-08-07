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

        public PageData<CorporationDto> LoadPageDataList(Expression<Func<CorporationDto, bool>> whereLambda,
                                                         Expression<Func<CorporationDto, object>> orderLambda,
                                                         int pageIndex,
                                                         int pageSize,
                                                         bool isDesc = false)
        {
            if (whereLambda == null)
            {
                whereLambda = w => true;
            }
            var dataSource =
                //from c in dbContext.Corporations
                //join a in dbContext.Areas on c.ID equals a.ID
                //where c.IsActive == false && c.IsDeleted == false
                //select new CorporationDto
                //{
                //    AreaAddress = a.LogogramName,
                //    AreaID = a.ID,
                //    AreaLevel = a.LevelType,
                //    ID = c.ID,
                //    Name = c.Name,
                //    FullName = c.FullName,
                //    BusinessLicense = c.BusinessLicense,
                //    CorporationAddress = c.CorporationAddress,
                //    CreationTime = c.CreationTime,
                //    CreatorUserId = c.CreatorUserId,
                //    Departments = c.Departments,
                //    IsDeleted = c.IsDeleted,
                //    IsActive = c.IsActive,
                //    LegalPersonIDCardNo = c.LegalPersonIDCardNo,
                //    LastModificationTime = c.LastModificationTime,
                //    LegalPerson = c.LegalPerson,
                //    LegalPersonPhone = c.LegalPersonPhone,
                //    FaxNumber = c.FaxNumber,
                //    Remarks = c.Remarks,
                //    SupportEMail = c.SupportEMail,
                //    Telephone = c.Telephone

                //};

                dbContext.Corporations
                .GroupJoin(dbContext.Areas, c => c.AreaID, a => a.ID, (c, a) => new
                {
                    area = a,
                    corporation = c
                })
                .SelectMany(combination => combination.area.DefaultIfEmpty(), (combination, area) => new CorporationDto
                {
                    AreaAddress = area.MergerName,
                    AreaID = area.ID,
                    AreaLevel = area.LevelType,
                    ID = combination.corporation.ID,
                    Name = combination.corporation.Name,
                    FullName = combination.corporation.FullName,
                    BusinessLicense = combination.corporation.BusinessLicense,
                    CorporationAddress = combination.corporation.CorporationAddress,
                    CreationTime = combination.corporation.CreationTime,
                    CreatorUserID = combination.corporation.CreatorUserID,
                    Departments = combination.corporation.Departments,
                    IsDeleted = combination.corporation.IsDeleted,
                    IsActive = combination.corporation.IsActive,
                    LegalPersonIDCardNo = combination.corporation.LegalPersonIDCardNo,
                    LastModificationTime = combination.corporation.LastModificationTime,
                    LegalPerson = combination.corporation.LegalPerson,
                    LegalPersonPhone = combination.corporation.LegalPersonPhone,
                    FaxNumber = combination.corporation.FaxNumber,
                    Remarks = combination.corporation.Remarks,
                    SupportEMail = combination.corporation.SupportEMail,
                    Telephone = combination.corporation.Telephone
                })
                .Where(whereLambda);
            //CorporationDto  { AreaID=a.ID,AreaLevel=a.LevelType,AreaAddress=a.MergerName })
            // .SelectMany(combination=> combination.areas,(c,a)=> new CorporationDto {AreaID= c.areas.ID, })
            //.GroupJoin(dbContext.Products, person => person.Id, product => product.Id, (person, products) => new { Person = person, Products = products })
            //.SelectMany(combination => combination.Products.DefaultIfEmpty(), (person, products) => new { PersonId = person.Person.Id, PersonName = person.Person.Name, ProductsId = products.Id, ProductsName = products.Product }).ToList();

            //.SelectMany(combination => combination.Areas.ID, (Corporations, Area) => new CorporationDto { AreaID = Area.Areas.ID, AreaAddress = Corporations.Areas, ProductsId = products.Id, ProductsName = products.Product });
            // .Where(whereLambda);

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
            dataSource = dataSource.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            resualData.Rows = dataSource.ToList();
            return resualData;
        }


    }
}
