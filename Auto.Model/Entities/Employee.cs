using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Auto.Model.Auditing;
using Auto.Model;

namespace Auto.Model.Entities
{
    /// <summary>
    /// 员工信息
    /// </summary>
    [Table("Employee")]
    public class Employee : Entity, IFullAudited
    {
        /// <summary>
        /// 员工性别
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！")]
        public EntityEnum.Gender EmployeeGender { get; set; }

        [Required(ErrorMessage = "{0}不能为空！")]
        public int PositionID { get; set; }
        public Position Position { get; set; }

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
