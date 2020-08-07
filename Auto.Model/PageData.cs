using System;
using System.Collections.Generic;
using System.Text;

namespace Auto.Model
{
    /// <summary>
    /// 分页数据辅助类
    /// </summary>
    public class PageData<T> where T : class
    {
        public PageData()
        {
            Total = 0;
            Rows = null;
        }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Rows { get; set; }
    }
}
