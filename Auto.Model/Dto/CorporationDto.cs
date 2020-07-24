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
    public class CorporationDto : BootstrapTableDtoBase, IFullAudited
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
        public List<int> AreaID { get; set; }
        /// <summary>
        /// 公司详细地址：行政区划ID之后地址
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "公司详细地址"), MinLength(5, ErrorMessage = "{0}，长度不能少于{1}")]
        public string CorporationAddress { get; set; }


        /// <summary>
        /// 创建者ID
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "创建者ID")]
        public long CreatorUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "创建时间")]
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 修改者ID
        /// </summary>
        [Display(Name = "修改者ID")]
        public long? ModificationUserID { get; set; }
        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        [Display(Name = "最后一次修改时间")]
        public DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 是否活跃状态
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "是否活跃状态")]
        public bool IsActive { get; set; }
        /// <summary>
        /// 是否删除状态
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "是否删除状态")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 删除操作者ID
        /// </summary>
        [Display(Name = "删除操作时间")]
        public long? DeleterUserId { get; set; }
        /// <summary>
        /// 删除操作时间
        /// </summary>
        [Display(Name = "删除操作时间")]
        public DateTime? DeletionTime { get; set; }
    }
}
