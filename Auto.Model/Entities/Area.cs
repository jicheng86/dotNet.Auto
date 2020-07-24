using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

using Auto.Model.Auditing;

namespace Auto.Model.Entities
{
    /// <summary>
    /// 行政区划
    /// </summary>
    [Table("Area")]
    public class Area : Entity, ICreationAudited, IModificationAudited, IActively, ISoftDelete
    {
        /// <summary>
        /// 行政区划Code
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "行政区划Code"), StringLength(50, ErrorMessage = "{0}长度不能超过：{1}")]
        public string CityCode { get; set; }
        /// <summary>
        /// 父级行政区划Code 100000=中国
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "父级行政区划Code"), StringLength(50, ErrorMessage = "{0}长度不能超过：{1}")]
        public string ParentCode { get; set; }
        /// <summary>
        /// 行政区划简称： XX省 或 XX市 或 XX区 或 XX街道
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "行政区划简称"), StringLength(50, ErrorMessage = "{0}长度不能超过：{1}")]
        public string LogogramName { get; set; }
        /// <summary>
        /// 省市区全称聚合：广东省,清远市
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "省市区全称聚合"), StringLength(100, ErrorMessage = "{0}长度不能超过：{1}")]
        public string MergerName { get; set; }
        /// <summary>
        /// 省市区简称聚合：广东,清远
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "省市区简称聚合"), StringLength(50, ErrorMessage = "{0}长度不能超过：{1}")]
        public string MergerLogogramName { get; set; }
        /// <summary>
        /// 行政区划级别country:国家,province:省份,city:市,district:区县,street:街道
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "行政区划级别"), StringLength(50, ErrorMessage = "{0}长度不能超过：{1}")]
        public string AreaLevel { get; set; }
        /// <summary>
        /// 级别 0.国家，1.省(直辖市) 2.市 3.区(县),4.街道
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "行政区划级别值"), Range(0, 4, ErrorMessage = "{0}不能超出取值范围：{1}~{2}")]
        public int LevelType { get; set; }
        /// <summary>
        /// 电话区划号码 清远（0755）
        /// </summary>
        [Display(Name = "电话区划号码"), StringLength(50, ErrorMessage = "{0}长度不能超过：{1}")]
        public string TelephoneCode { get; set; }
        /// <summary>
        /// 邮编 清远（513000）
        /// </summary>
        [Display(Name = "邮编"), StringLength(50, ErrorMessage = "{0}长度不能超过：{1}")]
        public string ZipCode { get; set; }
        /// <summary>
        /// 行政区划全拼：清远市（qingyuanshi）
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "行政区划全拼"), StringLength(50, ErrorMessage = "{0}长度不能超过：{1}")]
        public string CompleteSpelling { get; set; }
        /// <summary>
        /// 行政区划简拼：清远市（qys）
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "行政区划简拼"), StringLength(50, ErrorMessage = "{0}长度不能超过：{1}")]
        public string LogogramSpelling { get; set; }
        /// <summary>
        /// 城市中心坐标点：清远市（113.051227,23.685022）
        /// </summary>
        [Display(Name = "城市中心坐标点"), StringLength(50, ErrorMessage = "{0}长度不能超过：{1}")]
        public string Center { get; set; }
        /// <summary>
        /// 首字母：清远市（q）
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "城市中心坐标点"), StringLength(50, ErrorMessage = "{0}长度不能超过：{1}")]
        public string NameFirstChar { get; set; }
        /// <summary>
        /// 城市经度
        /// </summary>
        [Display(Name = "城市经度值"), StringLength(50, ErrorMessage = "{0}长度不能超过：{1}")]
        public string Longitude { get; set; }
        /// <summary>
        /// 城市纬度值
        /// </summary>
        [Display(Name = "城市纬度值"), StringLength(50, ErrorMessage = "{0}长度不能超过：{1}")]
        public string Latitude { get; set; }
        /// <summary>
        /// 数据版本号 当前版本：20190115
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "数据版本号"), StringLength(50, ErrorMessage = "{0}长度不能超过：{1}")]
        public string Version { get; set; }

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
    }
}
