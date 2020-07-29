using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auto.IService.IServices;
using Auto.Web.Models;
using Auto.Web.Models.Utility;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Auto.Web.Controllers
{
    public class AreaController : BaseController
    {
        public AreaController(IMapper mapper, IAreaService areaService, ICorporationService corporationService)
        {
            AreaService = areaService ?? throw new ArgumentNullException(nameof(areaService));
            //CorporationService = corporationService ?? throw new ArgumentNullException(nameof(corporationService));
            AutoMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public IAreaService AreaService { get; set; }
        //public ICorporationService CorporationService { get; set; }
        public IMapper AutoMapper { get; set; }

        /// <summary>
        /// 获取Area SelectListItem数据
        /// </summary>
        /// <param name="ParentID">父级主键ID</param>
        /// <param name="SelectedID">默认选中值</param>
        /// <param name="HadEmptyItem">是否包含：“请选择”空选项</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAreasSelectOptions(int ParentID, int SelectedID, bool HadEmptyItem = false)
        {
            JSONData jSONData = new JSONData();
            List<SelectListItem> items = new List<SelectListItem>();
            if (HadEmptyItem)
            {
                items.Add(new SelectListItem { Value = string.Empty, Text = "请选择" });
            }
            var area = AreaService.GetEntity(a => a.ID == ParentID);
            if (area == null)
            {
                return Ok(jSONData);
            }
            items.AddRange(AreaService.GetEntityListAsync(a => a.ParentCode == area.CityCode).Result.Select(a => new SelectListItem() { Value = a.ID.ToString(), Text = a.Name, Selected = a.ID == SelectedID }).ToList());
            StringBuilder stringBuilder = new StringBuilder();
            if (!items.Any())
            {
                return Ok(jSONData);
            }
            foreach (var item in items)
            {
                string option = $"<option{(item.Disabled ? " disabled" : string.Empty)}{(item.Selected ? " selected" : string.Empty)} value = \'{item.Value}\'> {item.Text}</option>";
                stringBuilder.Append(option);
            }
            jSONData.Code = EnumCollection.ResponseStatusCode.SUCCESS;
            jSONData.Data = stringBuilder.ToString();
            jSONData.Message = "successful.";

            return Ok(jSONData);
        }
    }
}
