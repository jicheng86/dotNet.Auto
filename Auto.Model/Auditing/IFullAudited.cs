using System;
using System.Collections.Generic;
using System.Text;

namespace Auto.Model.Auditing
{
    /// <summary>
    /// 创建信息，修改信息，软删除，是否活跃
    /// </summary>
    interface IFullAudited : IAudited, IDeletionAudited, IActively
    {
    }
}
