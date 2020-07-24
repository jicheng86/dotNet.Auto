using System;
using System.Collections.Generic;
using System.Text;

namespace Auto.Model.Auditing
{
    /// <summary>
    /// 创建信息，修改信息
    /// </summary>
    interface IAudited : ICreationAudited, IModificationAudited
    {
    }
}
