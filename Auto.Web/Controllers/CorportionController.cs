using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Auto.IService.IServices;
using Auto.Model;
using Auto.Model.Dto;
using Auto.Model.Entities;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

namespace Auto.Web.Controllers
{
    public class CorporationController : BaseController
    {
        public CorporationController(IMapper mapper, ICorporationService corporationService)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            CorporationService = corporationService ?? throw new ArgumentNullException(nameof(corporationService));
        }

        public IMapper Mapper { get; }
        public ICorporationService CorporationService { get; }

        public IActionResult Creation()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            PageData<CorporationDto> pageData = new PageData<CorporationDto>();
            IQueryable<Corporation> corporations = await CorporationService.GetEntityListAsync(c => c.IsActive == true && c.IsDeleted == false);
            Expression<Func<Corporation, bool>> wherelambda = c => c.IsActive == true;
            int pageInde = 1;
            int pageSiz = 10;
            pageData = CorporationService.LoadPageDataList(wherelambda, null, pageInde, pageSiz, true);
            pageData.Data = Mapper.ProjectTo<CorporationDto>(corporations).ToList();
            pageData.Total = pageData.Data.Count;
            return Json(pageData.Data);
        }
    }
}
