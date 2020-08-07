using System;
using System.Collections.Generic;
using System.Text;
using Auto.Model;
using static Auto.Web.Models.Utility.EnumCollection;

namespace Auto.Web.Models
{
    /// <summary>
    /// 返回json数据实体类
    /// </summary>
    public class ResponseJsonData
    {
        public ResponseJsonData()
        {
            Code = ResponseStatusCode.FAIL;
            Message = "操作失败";
            Data = null;
        }

        /// <summary>
        /// 返回状态码Code
        /// </summary>
        public ResponseStatusCode Code { get; set; }
        /// <summary>
        /// 描述信息Msg
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据体Data
        /// </summary>
        public object Data { get; set; }
    }
}
