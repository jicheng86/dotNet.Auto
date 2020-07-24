using System;
using System.Collections.Generic;
using System.Text;

namespace Auto.Model.Auditing
{
    /// <summary>
    /// 修改时间
    /// </summary>
    interface IHasModificationTime
    {
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }
    }
}
