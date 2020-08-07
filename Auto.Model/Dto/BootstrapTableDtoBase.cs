using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Auto.Model.Dto
{
    public class BootstrapTableDtoBase : Entity
    {
        public BootstrapTableDtoBase()
        {
            PageSize = 10;
            PageNumber = 1;
            SortOrder = "desc";
        }
        /// <summary>
        /// table每页行数
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "每页行数")]
        public int PageSize { get; set; }
        /// <summary>
        /// table当前页码
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "当前页码")]
        public int PageNumber { get; set; }
        /// <summary>
        /// 查找字符串
        /// </summary>
        [Display(Name = "查找关键字")]
        public string SearchText { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        [Display(Name = "排序字段名称")]
        public string SortName { get; set; }
        /// <summary>
        /// 排序顺序
        /// </summary>
        [Display(Name = "排序顺序")]
        public string SortOrder { get; set; }
    }
}
