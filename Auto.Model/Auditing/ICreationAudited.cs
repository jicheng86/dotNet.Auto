using System;
using System.Collections.Generic;
using System.Text;

namespace Auto.Model.Auditing
{
    /// <summary>
    /// 创建信息
    /// </summary>
    interface ICreationAudited : IHasCreationTime
    {
        /// <summary>
        /// 创建者ID
        /// </summary>
        public long CreatorUserId { get; set; }
    }
}
