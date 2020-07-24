using System;
using System.Collections.Generic;
using System.Text;

namespace Auto.Model.Auditing
{
    /// <summary>
    /// 软删除信息
    /// </summary>
    interface IDeletionAudited : ISoftDelete
    {
        /// <summary>
        /// 删除操作者
        /// </summary>
        long? DeleterUserId { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        DateTime? DeletionTime { get; set; }
    }
}
