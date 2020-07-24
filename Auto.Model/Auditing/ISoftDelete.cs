using System;
using System.Collections.Generic;
using System.Text;

namespace Auto.Model.Auditing
{
    /// <summary>
    /// 软删除标记
    /// </summary>
    interface ISoftDelete
    {
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
