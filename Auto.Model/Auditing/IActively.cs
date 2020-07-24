using System;
using System.Collections.Generic;
using System.Text;

namespace Auto.Model.Auditing
{
    /// <summary>
    /// 是否活跃标记
    /// </summary>
    interface IActively
    {
        /// <summary>
        /// 是否活跃
        /// </summary>
        public bool IsActive { get; set; }
    }
}
