using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Auto.IService.IServices;
using Auto.Model;
using Auto.Model.Dto;
using Auto.Model.Entities;
using Auto.Model.Extend;
using Auto.Web.Helpers;
using Auto.Web.Models;
using Auto.Web.Models.Utility;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Auto.Web.Controllers
{
    public class CorporationController : BaseController
    {
        public CorporationController(IMapper mapper, ICorporationService corporationService)
        {
            AutoMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            CorporationService = corporationService ?? throw new ArgumentNullException(nameof(corporationService));
        }

        public IMapper AutoMapper { get; }
        public ICorporationService CorporationService { get; }

        public IActionResult Creation()
        {
            return View();
        }


        /// <summary>
        /// 创建公司数据提交
        /// </summary>
        /// <param name="corporationDto">提交公司信息</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Creation(CorporationDtoCreation corporationDto)
        {
            ResponseJsonData jsondata = new ResponseJsonData();
            if (!ModelState.IsValid)
            {
                //ModelState.AddModelError(string.Empty, "自定义描述错误");
                //ModelState.Remove("AreaID");
                //ViewBag.ModelState = ModelState;
                //ViewData["ModelState"] = ModelState;
                //var values = ModelState.Values.Where(s => s.Errors.Any());
                ViewBag.AreaIDs = string.Join(",", corporationDto.AreaID);
                Dictionary<string, string> ModelErrors = new Dictionary<string, string>();

                var keys = ModelState.Keys.Where(k => ModelState[k].Errors.Count > 0);
                foreach (var key in keys)
                {
                    var ErrorMessages = "";
                    if (key == "AreaID")
                    {
                        ErrorMessages = "请正确选择区划地址！";
                    }
                    else
                    {
                        foreach (var error in ModelState[key].Errors)
                        {
                            ErrorMessages += ' ' + error.ErrorMessage;
                        }
                    }
                    ModelErrors.Add(key, ErrorMessages);
                }
                jsondata.Code = EnumCollection.ResponseStatusCode.MODELSTATE;
                jsondata.Data = new { ModelErrors, AreaIDs = string.Join(",", corporationDto.AreaID) };
                jsondata.Message = "数据校验非法，请核实！";
                return Json(jsondata);
            }

            var firstArerID = corporationDto.AreaID.FirstOrDefault();
            if (Constant.SpecialAdministrativeRegionAreaID.Contains(firstArerID))
            {
                if (corporationDto.AreaID.Count < 2)
                {
                    jsondata.Code = EnumCollection.ResponseStatusCode.ARGUMENTSLOSE;
                    jsondata.Message = "请选择完整的区划地址";
                    return Json(jsondata);
                }
            }
            if (corporationDto.AreaID.Count < 4)
            {
                jsondata.Code = EnumCollection.ResponseStatusCode.ARGUMENTSLOSE;
                jsondata.Message = "请选择完整的区划地址";
                return Json(jsondata);
            }

            Corporation corporation = AutoMapper.Map<Corporation>(corporationDto);
            corporation.CreatorUserID = 1000;
            if (CorporationService.IsExisted(corporation))
            {
                jsondata.Code = EnumCollection.ResponseStatusCode.FAIL;
                jsondata.Message = "操作失败：分公司信息已存在，请核实！";
                return Json(jsondata);
            }
            CorporationService.Create(corporation);
            var result = await CorporationService.SaveChangeAsync();
            if (result > 0)
            {
                jsondata.Code = EnumCollection.ResponseStatusCode.SUCCESS;
                jsondata.Message = "操作成功";
                return Json(jsondata);
            }
            return Json(jsondata);
        }
        /// <summary>
        /// 公司列表
        /// </summary>
        /// <returns></returns>
        public IActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 公司列表数据获取
        /// </summary>
        /// <param name="FromData"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ListPageData([FromBody] JObject FromData)
        {
            // pageSize = 10 & pageNumber = 1 & searchText = &sortOrder = desc & _ = 1596504366939
            CorporationSearchParamsDto Argus = FromData.JsonToClass<CorporationSearchParamsDto>();

            PageData<CorporationDto> pageData = new PageData<CorporationDto>();
            //IQueryable<Corporation> corporations = await CorporationService.GetEntityListAsync(c => c.IsActive == true && c.IsDeleted == false);
            Expression<Func<CorporationDto, bool>> wherelambda = c => c.IsActive == true;
            wherelambda.And(c => c.IsDeleted == false);
            int pageInde = Argus.PageNumber;// string.IsNullOrEmpty(Request.Form["pageInde"]) ? (int)Request.Form["pageInde"] : 10;
            int pageSiz = Argus.PageSize;
            pageData = CorporationService.LoadPageDataList(wherelambda, null, pageInde, pageSiz, true);
            return Json(pageData);
            //return View(pageData.Data);
        }
    }
}
