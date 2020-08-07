using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.IdentityServer4.Migrations
{
    public class DeviceCodes
    {
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "UserCode"), StringLength(2000, ErrorMessage = "{0}长度不能超过：{1}")]
        public string UserCode { get; set; }
        [Required(ErrorMessage = "{0}不能为空！"), Display(Name = "DeviceCode"), StringLength(2000, ErrorMessage = "{0}长度不能超过：{1}")]
        public string DeviceCode { get; set; }
        [Display(Name = "SubjectId"),StringLength(200, ErrorMessage = "{0}长度不饿能超过：{1}")]
        public string SubjectId { get; set; }

    }
}
