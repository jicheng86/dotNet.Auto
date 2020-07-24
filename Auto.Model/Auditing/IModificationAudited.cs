using System;
using System.Collections.Generic;
using System.Text;

namespace Auto.Model.Auditing
{
    /// <summary>
    /// 修改信息
    /// </summary>
    interface IModificationAudited : IHasModificationTime
    {
        /// <summary>
        /// 修改者ID
        /// </summary>
        public long? ModificationUserID { get; set; }
    }
}
