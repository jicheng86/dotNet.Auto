using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

using Auto.Model.Auditing;
using Auto.Model.Entities;

namespace Auto.Model.Dto
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public class CorporationDto : Corporation
    {
        /// <summary>
        /// 行政区划地址，xx省，xx市，xx县/区...
        /// </summary>
        [Display(Name = "行政区划地址")]
        public string AreaAddress { get; set; }
        /// <summary>
        /// 行政区划地址等级：0=国家，1=省，2=市，3=县/区 4=街区
        /// </summary>
        [Display(Name = "行政区划地址等级")]
        public int AreaLevel { get; set; }

    }
}
