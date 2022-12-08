using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDataStatistics
{
    /// <summary>
    /// 全局变量
    /// </summary>
    internal static class GlobalVariable
    {
        /// <summary>
        /// 数据库路径
        /// </summary>
        internal static string dbPath;
        /// <summary>
        /// 开始时间
        /// </summary>
        internal static long startTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        internal static long endTime;
        /// <summary>
        /// 表名
        /// </summary>
        internal static string tableName;
        /// <summary>
        /// 字段名
        /// </summary>
        internal static string fieldName;
        /// <summary>
        /// 最小值
        /// </summary>
        internal static float minimum;
        /// <summary>
        /// 最大值
        /// </summary>
        internal static float maximum;
        /// <summary>
        /// 流量报告路径
        /// </summary>
        internal static string flowReportPath;
    }
}
