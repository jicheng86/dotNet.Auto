using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Auto.Model
{
    public class EntityBase<TPrimaryKey>
    {
        /// <summary>
        /// 实体主键ID
        /// </summary>
        [Display(Name = "主键ID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TPrimaryKey ID { get; set; }
        /// <summary>
        /// 实体名称
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "名称"), StringLength(150, MinimumLength = 1, ErrorMessage = "{0}长度范围为：{2}到{1}，请核实！")]
        public string Name { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        [Display(Name = "备注信息"), StringLength(2500, ErrorMessage = "{0}长度不能超过：{1}，请核实！")]
        public string Remarks { get; set; }
    }
}
