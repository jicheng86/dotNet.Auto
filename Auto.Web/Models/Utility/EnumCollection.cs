using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Auto.Web.Models.Utility
{
    public static class EnumCollection
    {
        #region 系统相关
        /// <summary>
        /// 日志等级
        /// </summary>
        public enum LogsLevel
        {
            /// <summary>
            /// Debug -调试级别
            /// </summary>
            [Description("Debug")]
            Debug = 0,

            /// <summary>
            /// Info -一般信息级别
            /// </summary>
            [Description("Info")]
            Info = 1,

            /// <summary>
            /// Warn -警告级别
            /// </summary>
            [Description("Warn")]
            Warn = 2,

            /// <summary>
            /// Error -一般错误级别
            /// </summary>
            [Description("Error")]
            Error = 3,

            /// <summary>
            /// Fatal -致命错误级别
            /// </summary>
            [Description("Fatal")]
            Fatal = 4
        }
        /// <summary>
        /// 日志模块需跟log4NetCore.xml配置一致
        /// </summary>
        public enum LogsModule
        {
            /// <summary>
            /// 一般日志模块
            /// </summary>
            [Description("一般日志模块")]
            InfoLogs = 0,

            /// <summary>
            /// 错误日志模块
            /// </summary>
            [Description("错误日志模块")]
            ErrorLogs = 1,

            /// <summary>
            ///调试日志模块
            /// </summary>
            [Description("调试日志模块")]
            DebugLogs = 2,

            /// <summary>
            /// 账号模块日志
            /// </summary>
            [Description("账号模块日志")]
            LoginLogs = 11,

            /// <summary>
            /// 业务模块日志
            /// </summary>
            [Description("业务模块日志")]
            Business = 21,

            /// <summary>
            /// 财务模块日志
            /// </summary>
            [Description("财务模块日志")]
            Finance = 31,

        }
        #endregion

        #region HTTP 请求相关
        /// <summary>
        /// HTTP请求返回码
        /// </summary>
        public enum ResponseStatusCode
        {
            /// <summary>
            /// 操作失败
            /// </summary>
            [Description("操作失败")]
            FAIL = 0,

            /// <summary>
            /// 操作成功
            /// </summary>
            [Description("操作成功")]
            SUCCESS = 1,

            /// <summary>
            /// 操作异常
            /// </summary>
            [Description("操作异常")]
            ERROR = 2,

            /// <summary>
            /// 参数丢失
            /// </summary>
            [Description("参数丢失")]
            ARGUMENTSLOSE = 3,

            /// <summary>
            /// ModelStateInValid
            /// </summary>
            MODELSTATE = 4,

            /// <summary>
            /// 未知错误
            /// </summary>
            [Description("未知错误")]
            UNKNOWN = 99
        }
        #endregion

        #region 地址相关
        /// <summary>
        /// AreaLevel与同名字段一致，值与LevelType字段一致
        /// </summary>
        public enum AreaLevel
        {
            /// <summary>
            /// 国家
            /// </summary>
            country = 0,
            /// <summary>
            /// 省/自治区/直辖市
            /// </summary>
            province = 1,
            /// <summary>
            /// 市
            /// </summary>
            city = 2,
            /// <summary>
            /// 县/区
            /// </summary>
            district = 3,
            /// <summary>
            /// 街道
            /// </summary>
            street = 4

        }

        #endregion
    }
}
