using System;
using System.Collections.Generic;
using System.Text;

namespace Auto.Model
{
    /// <summary>
    /// 默认int类型主键 
    /// </summary>
    public class Entity : EntityBase<int>
    {
    }
    public class Entity<TPrimaryKey> : EntityBase<TPrimaryKey>
    {
    }
}
