using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

using Auto.Model.Auditing;

namespace Auto.Model.Dto
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public class CorporationSearchParamsDto : BootstrapTableDtoBase
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "公司名称")]
        public string Name { get; set; }
        /// <summary>
        /// 行政区划ID
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "行政区划ID")]
        public int AreaID { get; set; }
        /// <summary>
        /// 公司详细地址：行政区划ID之后地址
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "公司详细地址")]
        public string CorporationAddress { get; set; }

    }
}
