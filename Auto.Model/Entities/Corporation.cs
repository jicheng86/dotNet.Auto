﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Auto.Model.Auditing;

namespace Auto.Model.Entities
{
    /// <summary>
    /// 公司信息
    /// </summary>
    [Table("Corporation")]
    public class Corporation : Entity, IFullAudited
    {
        public Corporation()
        {
            //初始化部门集合
            this.Departments = new List<Department>();
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "公司名称"), StringLength(50, MinimumLength = 5, ErrorMessage = "{0}长度范围：{2}~{1}，请核实！")]
        public string FullName { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "营业执照")]
        public string BusinessLicense { get; set; }
        /// <summary>
        /// 公司法人
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "公司法人"), StringLength(50, MinimumLength = 2, ErrorMessage = "{0}长度范围：{2}~{1}，请核实！")]
        public string LegalPerson { get; set; }
        /// <summary>
        /// 公司法人证件号
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "公司法人证件号"), StringLength(50, MinimumLength = 5, ErrorMessage = "{0}长度范围：{2}~{1}，请核实！")]
        public string LegalPersonIDCardNo { get; set; }
        /// <summary>
        /// 公司法人联系电话
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "公司法人联系电话"), Phone(ErrorMessage = "{0}格式不对，请输入正确手机号码")]
        public string LegalPersonPhone { get; set; }
        /// <summary>
        /// 公司联系电话
        /// </summary>
        [Display(Name = "公司联系电话")]
        public string Telephone { get; set; }
        /// <summary>
        /// 传真号码
        /// </summary>
        [Display(Name = "传真号码")]
        public string FaxNumber { get; set; }
        /// <summary>
        /// 服务支持邮件地址
        /// </summary>
        [Display(Name = "服务支持邮件地址")]
        public string SupportEMail { get; set; }
        /// <summary>
        /// 行政区划ID
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "行政区划")]
        public int AreaID { get; set; }
        /// <summary>
        /// 公司详细地址：行政区划ID之后地址
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "公司详细地址"), StringLength(200, MinimumLength = 5, ErrorMessage = "{0}，长度范围{2}~{1}，请核实")]
        public string CorporationAddress { get; set; }
        /// <summary>
        /// 部门集合
        /// </summary>
        [Display(Name = "部门集合")] 
        public virtual List<Department> Departments { get; set; }


        /// <summary>
        /// 创建者ID
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "创建者ID")]
        public long CreatorUserID { get; set; }
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
        public long? DeleterUserID { get; set; }
        /// <summary>
        /// 删除操作时间
        /// </summary>
        [Display(Name = "删除操作时间")]
        public DateTime? DeletionTime { get; set; }
    }
}
