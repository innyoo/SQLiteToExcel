using DbToExcel.BLL;
using DbToExcel.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Windows.Forms;

namespace SQLiteDataStatistics
{
    internal class FlowRange
    {
        /// <summary>
        /// 流量分析
        /// </summary>
        /// <returns>失败返回false</returns>
        internal static bool FlowAnalysis()
        {
            ArrayList curDuration = new ArrayList();   //当前持续状态的哈希表
            SortedList start_continue = new SortedList();     //key:开始时间 value:持续时间

            string sql;
            SQLiteDataReader reader;
            sql = $"select  ts, flow from datas where ts >= {GlobalVariable.startTime} and ts <= {GlobalVariable.endTime}";
            reader = Dal_admin.GetReader(GlobalVariable.dbPath, sql);
            curDuration.Clear();

            long tempStartTime = 0;     //暂存开始时间
            while (reader.Read())
            {

                float flow = Math.Abs((float)Convert.ToDouble(reader["flow"]));     //取当前流量的绝对值
                if (flow >= GlobalVariable.minimum && flow < GlobalVariable.maximum)
                {
                    if (curDuration.Count == 0)
                    {
                        tempStartTime = Convert.ToInt64(reader["ts"]);
                    }

                    curDuration.Add(flow);
                }
                else
                {
                    if (curDuration.Count > 0)
                    {
                        //Debug.WriteLine(reader["ts"]);
                        //Debug.WriteLine(TimeConversion.TimeStamp_DataTime(Convert.ToInt64(reader["ts"])));
                        start_continue.Add(tempStartTime, curDuration.Count);

                    }
                    curDuration.Clear();
                }

            }
            if(curDuration.Count > 0)
            {
                start_continue.Add(tempStartTime, curDuration.Count);
            }
            curDuration.Clear();
            //foreach (var item in start_continue)
            //{
            //    Debug.WriteLine(item);
            //}
            ICollection key = start_continue.Keys;
            int count = key.Count;  //出现次数
            if (count == 0)
            {
                MessageBox.Show("没有满足条件的数据", "结果", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (count == 1)
            {
                int val = 1;
                foreach (var k in key)
                {
                    val = (int)start_continue[k];
                }
                if (val == 0)
                {
                    MessageBox.Show("没有满足条件的数据", "结果", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;

                }

            }
            SortedListToDataTable(start_continue);
            return true;
        }
        /// <summary>
        /// 转换排序列表为DataTable
        /// </summary>
        /// <param name="开始_持续时间"></param>
        private static void SortedListToDataTable(SortedList 开始_持续时间)
        {
            string[] FiveHeader = { "开始时间", "持续时间（单位：秒）", " ", "统计项", "值" };
            DataTable dataTable = new DataTable("flow统计");       //*新建一张表 
            foreach (string TemStr in FiveHeader)
            {
                DataColumn strNameColumn = new DataColumn
                {
                    DataType = typeof(string),
                    ColumnName = TemStr
                };
                dataTable.Columns.Add(strNameColumn);
            }

            // 获取键的集合
            ICollection key = 开始_持续时间.Keys;
            int count = key.Count;  //出现次数

            long totalDuration = GlobalVariable.endTime - GlobalVariable.startTime;     //总时长
            int sum = 0;    //所有合计时间
            int longest = 0;    //最长持续时间
            int[] duration = new int[count];
            int index = 0;
            float median;
            foreach (var k in key)
            {
                int temp = (int)开始_持续时间[k];
                sum += temp;
                if (temp > longest)
                {
                    longest = temp;
                }
                duration[index] = temp;
                index++;
            }

            Array.Sort(duration);
            if (duration.Length == 1)
            {
                median = duration[0];
            }else if (duration.Length == 2)
            {
                median = (duration[0] + duration[1]) / 2;
            }else if (duration.Length == 3)
            {
                median = duration[1];
            }
            else
            {
                if (duration.Length % 2 == 0)
                {
                    median = (duration[duration.Length / 2] + duration[(duration.Length / 2) - 1]) / 2;
                }
                else
                {
                    median = duration[duration.Length / 2];
                }
            }
           


            float average = sum / count;  //平均持续时间
            double timeRatio = (double)sum / (double)totalDuration;      //时间占比
            int choose = 0;
            
            foreach (long k in key)
            {
                DataRow rowData;
                if (choose <= count)
                {
                    rowData = dataTable.NewRow();   //*建立行数据
                    rowData["开始时间"] = TimeConversion.TimeStamp_DataTime(k);
                    rowData["持续时间（单位：秒）"] = 开始_持续时间[k];
                    switch (choose)
                    {
                        case 0:
                            rowData["统计项"] = "时间范围";
                            rowData["值"] = TimeConversion.TimeStamp_DataTime(GlobalVariable.startTime)
                                + "至" + TimeConversion.TimeStamp_DataTime(GlobalVariable.endTime);
                            choose++;
                            break;
                        case 1:
                            rowData["统计项"] = "总时长(单位：秒）";
                            rowData["值"] = totalDuration;
                            choose++;
                            break;
                        case 2:
                            rowData["统计项"] = "flow范围";
                            rowData["值"] = GlobalVariable.minimum + "~" + GlobalVariable.maximum;
                            choose++;
                            break;
                        case 3:
                            rowData["统计项"] = "出现次数";
                            rowData["值"] = count;
                            choose++;
                            break;
                        case 4:
                            rowData["统计项"] = "累计持续时间";
                            rowData["值"] = sum;
                            choose++;
                            break;
                        case 5:
                            rowData["统计项"] = "累计时间占比";
                            rowData["值"] = timeRatio;
                            choose++;
                            break;
                        case 6:
                            rowData["统计项"] = "单次最长时间";
                            rowData["值"] = longest;
                            choose++;
                            break;
                        case 7:
                            rowData["统计项"] = "平均持续时间";
                            rowData["值"] = average;
                            choose++;
                            break;
                        case 8:
                            rowData["统计项"] = "中位数";
                            rowData["值"] = median;
                            choose++;
                            break;
                        //case 7:
                        //    rowData["统计项"] = "四分位数";
                        //    //rowData["值"] = ;
                        //    choose++;
                        //    break;
                        default:
                            break;
                    }
                    dataTable.Rows.Add(rowData);
                }
                if(choose==count)
                {
                    while (choose < 9)
                    {
                        rowData = dataTable.NewRow();   //*建立行数据
                        switch (choose)
                        {
                            case 0:
                                rowData["统计项"] = "时间范围";
                                rowData["值"] = TimeConversion.TimeStamp_DataTime(GlobalVariable.startTime)
                                    + "至" + TimeConversion.TimeStamp_DataTime(GlobalVariable.endTime);
                                choose++;
                                break;
                            case 1:
                                rowData["统计项"] = "总时长(单位：秒）";
                                rowData["值"] = totalDuration;
                                choose++;
                                break;
                            case 2:
                                rowData["统计项"] = "flow范围";
                                rowData["值"] = GlobalVariable.minimum + "~" + GlobalVariable.maximum;
                                choose++;
                                break;
                            case 3:
                                rowData["统计项"] = "出现次数";
                                rowData["值"] = count;
                                choose++;
                                break;
                            case 4:
                                rowData["统计项"] = "累计持续时间";
                                rowData["值"] = sum;
                                choose++;
                                break;
                            case 5:
                                rowData["统计项"] = "累计时间占比";
                                rowData["值"] = timeRatio;
                                choose++;
                                break;
                            case 6:
                                rowData["统计项"] = "单次最长时间";
                                rowData["值"] = longest;
                                choose++;
                                break;
                            case 7:
                                rowData["统计项"] = "平均持续时间";
                                rowData["值"] = average;
                                choose++;
                                break;
                            case 8:
                                rowData["统计项"] = "中位数";
                                rowData["值"] = median;
                                choose++;
                                break;
                            default:
                                break;

                        }
                        dataTable.Rows.Add(rowData);
                    }

                }

            }

            DataTableTo.DataTableToCsv(dataTable, GlobalVariable.flowReportPath);
        }

    }

}
