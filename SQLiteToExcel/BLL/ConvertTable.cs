﻿using Spire.Pdf.Graphics;
using SQLiteToExcel.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLiteToExcel.BLL
{
    internal class ConvertTable
    {
        internal static void ConvertBurninTable()   //将Burnin表的数据转换为DataTable中
        {
            FormExport fe = FormExport.GetInstance();
            //准备工作
            DirectoryInfo di = System.IO.Directory.GetParent(fe._dbPath);
            fe._outputPath = di + "\\" + fe._sn + '_' + fe._name + '_' + "burnin" + ".xlsx";
            if (!Tools.FileExistDelete(fe._outputPath))
                return;
            fe.lable提示.Text = "链接数据库...";
            SQLiteDataReader readerHead;
            try
            {
                readerHead = Dal_admin.GetReader(fe._dbPath, "SELECT DISTINCT ckey FROM burnin ORDER BY ckey");
            }
            catch
            {
                MessageBox.Show("当前db文件中不存在老化数据表！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            fe.lable提示.Text = "链接成功！";
            fe.lable提示.Text = "创建数据集...";
            //string[] FiveHeader = {"cid", "YYYY-MM-DD", "HH:MM:SS" };
            DataTable dataTable = new DataTable("ExportData");//*新建一张表 
            ArrayList Header = new ArrayList
            {
                "cid",
                "YYYY-MM-DD",
                "HH:MM:SS",
                "speed"
            };
            while (readerHead.Read())
            {
                Header.Add((string)readerHead["ckey"]);
            }
            foreach (string TemStr in Header)
            {
                DataColumn strNameColumn = new DataColumn
                {
                    DataType = typeof(string),
                    ColumnName = TemStr
                };
                dataTable.Columns.Add(strNameColumn);
            }

            string sql;
            sql = $"SELECT burnin.cid, tsrecord, ckey, cvalue,speed FROM burnin LEFT OUTER JOIN datas ON datas.ts=burnin.tsrecord/1000 where tsrecord >= {fe._start * 1000} and tsrecord< {fe._end * 1000}";
            //导出数据
            //string sql = "select * from datas";
            SQLiteDataReader reader = Dal_admin.GetReader(fe._dbPath, sql);


            long unixTimeStamp = 0;
            DataRow rowData = dataTable.NewRow();   //*建立行数据
            while (reader.Read())
            {
                if (unixTimeStamp == (long)reader["tsrecord"] / 1000)   //判断当前行的时间戳是否变化
                {
                    if (Header.IndexOf(reader["ckey"]) > 0)
                    {
                        rowData[(string)Header[Header.IndexOf(reader["ckey"])]] = Tools.IsError(reader["cvalue"]);
                    }
                }
                else
                {
                    unixTimeStamp = (long)reader["tsrecord"] / 1000;
                    rowData = dataTable.NewRow();
                    rowData["cid"] = reader["cid"];
                    rowData["YYYY-MM-DD"] = TimeConversion.TimeStamp_Data(unixTimeStamp);
                    rowData["HH:MM:SS"] = TimeConversion.TimeStamp_Time(unixTimeStamp);
                    rowData["speed"] = Tools.IsNone(reader["speed"]);
                    if (Header.IndexOf(reader["ckey"]) > 0)
                    {
                        rowData[(string)Header[Header.IndexOf(reader["ckey"])]] = Tools.IsError(reader["cvalue"]);
                    }

                    dataTable.Rows.Add(rowData);
                }

                //count++;
            }
            Console.ReadLine();
            fe.lable提示.Text = "数据集创建成功！";


            fe.lable提示.Text = "转换数据...";
            DataTableTo.DataTableToExcel(dataTable, fe._outputPath, 1);
            fe.EndPrompt();
        }
        internal static void ConvertDatasTable()     //将Datas表的数据转换为DataTable
        {
            FormExport fe = FormExport.GetInstance();
            //准备工作
            DirectoryInfo parentPath = System.IO.Directory.GetParent(fe._dbPath);
            fe._outputPath = parentPath + "\\" + fe._sn + '_' + fe._name + "_datas" + ".xlsx";
            if (!Tools.FileExistDelete(fe._outputPath))
                return;
            fe.lable提示.Text = "链接数据库...";
            string sql;
            SQLiteDataReader reader;
            if (fe._start < 1650985317 || fe._end > 1650985397)
            {
                try
                {
                    sql = $"select  ts, speed, flow, p1, p2, p3, p, t1, t2 from datas where ts >= {fe._start} and ts <= {fe._end} and (speed >= 0 and speed <=6500 or speed is NULL) and (flow>=-2 and flow<=12 or flow is NULL) and (p1>=-400 and p1<=400 or p1 is NULL) and (p2>=-400 and p2<=400 or p2 is NULL) and (p3>=-400 and p3<=400 or p3 is NULL) and ts not in(select ts from datas where ts>=1650985317 and ts <=1650985397 )";
                    reader = Dal_admin.GetReader(fe._dbPath, sql);
                }
                catch
                {
                    sql = $"select  ts, speed, flow, p1, p2, p3, p, t from datas where ts >= {fe._start} and ts < {fe._end}  and (speed >= 0 and speed <=6500 or speed is NULL) and (flow>=-2 and flow<=12 or flow is NULL) and (p1>=-400 and p1<=400 or p1 is NULL) and (p2>=-400 and p2<=400 or p2 is NULL) and (p3>=-400 and p3<=400 or p3 is NULL) and ts not in(select ts from datas where ts>=1650985317 and ts <=1650985397 )";
                    reader = Dal_admin.GetReader(fe._dbPath, sql);
                }
            }
            else
            {
                try
                {
                    sql = $"select ts, speed, flow, p1, p2, p3, p, t1,t2 from datas where ts >= {fe._start} and ts < {fe._end}  and (speed >= 0 and speed <=6500 or speed is NULL) and (flow>=-2 and flow<=12 or flow is NULL) and (p1>=-400 and p1<=400 or p1 is NULL) and (p2>=-400 and p2<=400 or p2 is NULL) and (p3>=-400 and p3<=400 or p3 is NULL)";
                    reader = Dal_admin.GetReader(fe._dbPath, sql);
                }
                catch
                {
                    sql = $"select ts, speed, flow, p1, p2, p3, p, t from datas where ts >= {fe._start} and ts < {fe._end}  and (speed >= 0 and speed <=6500 or speed is NULL) and (flow>=-2 and flow<=12 or flow is NULL) and (p1>=-400 and p1<=400 or p1 is NULL) and (p2>=-400 and p2<=400 or p2 is NULL) and (p3>=-400 and p3<=400 or p3 is NULL)";
                    reader = Dal_admin.GetReader(fe._dbPath, sql);
                }
            }
            //导出数据
            //string sql = "select * from datas";

            fe.lable提示.Text = "链接成功！";
            fe.lable提示.Text = "创建数据集...";
            string[] FiveHeader = {/*"cid",*/ "YYYY-MM-DD", "HH:MM:SS", "speed(RPM)", "flow(LPM)", "P1(mmHg)", "P2(mmHg)", "P3(mmHg)", "P2-P3(mmHg)", "T1(℃)", "T2(℃)" };
            DataTable dataTable = new DataTable("ExportData");//*新建一张表 
            foreach (string TemStr in FiveHeader)
            {
                DataColumn strNameColumn = new DataColumn
                {
                    //if (TemStr == "YYYY-MM-DD" || TemStr == "HH:MM:SS")
                    //{
                    //    strNameColumn.DataType = typeof(String);
                    //}
                    //else
                    //{
                    //    strNameColumn.DataType = typeof(double);
                    //}

                    DataType = typeof(string),
                    ColumnName = TemStr
                };
                dataTable.Columns.Add(strNameColumn);
            }


            int count = 0;
            while (reader.Read())
            {

                DataRow rowData = dataTable.NewRow();   //*建立行数据
                long unixTimeStamp = (long)reader["ts"];
                if (count == 0)
                {
                    if (unixTimeStamp == 1647503856 && Convert.ToDouble(reader["speed"]) == 0)
                    {
                        TimeConversion._correctionValue = 0;
                    }
                    else
                    {
                        TimeConversion._correctionValue = 8 * 60 * 60;
                    }
                    count = 1;
                }
                //rowData["cid"] = reader["cid"];
                rowData["YYYY-MM-DD"] = TimeConversion.TimeStamp_Data(unixTimeStamp);
                rowData["HH:MM:SS"] = TimeConversion.TimeStamp_Time(unixTimeStamp);
                try
                {
                    rowData["speed(RPM)"] = WaveFilter.TimeFilter(WaveFilter.FILTER_LIST_SPEED, unixTimeStamp, Convert.ToDouble(reader["speed"]), 50, 7);//滤波
                }
                catch
                {
                    rowData["speed(RPM)"] = "--";//滤波

                }
                rowData["flow(LPM)"] = Tools.IsNone(reader["flow"]);
                rowData["P1(mmHg)"] = Tools.IsNone(reader["p1"]);
                try
                {
                    rowData["P2(mmHg)"] = WaveFilter.MovingAverageFilter(WaveFilter.SLIDE_LIST_P2, unixTimeStamp, Convert.ToDouble(reader["p2"]));
                }
                catch
                {
                    rowData["P2(mmHg)"] = Tools.IsNone(reader["p2"]); ;
                }
                try
                {
                    rowData["P3(mmHg)"] = WaveFilter.MovingAverageFilter(WaveFilter.SLIDE_LIST_P3, unixTimeStamp, Convert.ToDouble(reader["p3"]));
                }
                catch
                {
                    rowData["P3(mmHg)"] = Tools.IsNone(reader["p3"]);
                }
                
                rowData["P2-P3(mmHg)"] = Tools.IsNone(reader["p"]);

                rowData["T1(℃)"] = Tools.IsNone(reader["t1"]);
                rowData["T2(℃)"] = Tools.IsNone(reader["t2"]);

                dataTable.Rows.Add(rowData);
                //count++;
            }
            fe.lable提示.Text = "数据集创建成功！";
            //MessageBox.Show("count:" + count);
            //progressBar1.Maximum = count + 2;

            fe.lable提示.Text = "转换数据...";

            //MessageBox.Show("已转换为dateTable");

            DataTableTo.DataTableToExcel(dataTable, fe._outputPath, 2);

            //DataTableToCsv(dataTable, _outputPath);

            fe.EndPrompt();
        }


        internal static void ConvertEventsTable()     //将Events表的数据转换为DataTable 
        {
            FormExport fe = FormExport.GetInstance();

            //准备工作
            DirectoryInfo parentPath = System.IO.Directory.GetParent(fe._dbPath);
            fe._outputPath = parentPath + "\\" + fe._sn + '_' + fe._name + "_" + "events" + ".xlsx";
            if (!Tools.FileExistDelete(fe._outputPath))
                return;
            fe.lable提示.Text = "链接数据库...";
            string sql = $"select cid, ts, level, code, para1, ishand from events where ts >= {fe._start} and ts< {fe._end}";
            //导出数据
            SQLiteDataReader reader = Dal_admin.GetReader(fe._dbPath, sql);

            fe.lable提示.Text = "链接成功！";
            fe.lable提示.Text = "创建数据集...";
            string[] FiveHeader = {/*"cid",*/ "YYYY-MM-DD", "HH:MM:SS", "level", "code", "info", "para1", "ishand" };
            DataTable dataTable = new DataTable("ExportData");//*新建一张表 
            foreach (string TemStr in FiveHeader)
            {
                DataColumn strNameColumn = new DataColumn
                {
                    DataType = typeof(string),
                    ColumnName = TemStr
                };
                dataTable.Columns.Add(strNameColumn);
            }


            //int count = 0;
            if (Languagehelper.D2.Count < 1)
            {
                Languagehelper.LanguageHelper();
            }

            while (reader.Read())
            {

                DataRow rowData = dataTable.NewRow();   //*建立行数据

                long unixTimeStamp = (long)reader["ts"];

                //rowData["cid"] = reader["cid"];
                rowData["YYYY-MM-DD"] = TimeConversion.TimeStamp_Data(unixTimeStamp);
                rowData["HH:MM:SS"] = TimeConversion.TimeStamp_Time(unixTimeStamp);
                rowData["level"] = reader["level"];
                rowData["code"] = reader["code"];
                rowData["info"] = Languagehelper.GetNLS(reader["code"]);
                rowData["para1"] = reader["para1"];
                rowData["ishand"] = reader["ishand"];

                dataTable.Rows.Add(rowData);
                //  count++;
            }
            Console.ReadLine();
            fe.lable提示.Text = "数据集创建成功！";
            //MessageBox.Show("count:" + count);
            //progressBar1.Maximum = count + 2;

            fe.lable提示.Text = "转换数据...";
            DataTableTo.DataTableToExcel(dataTable, fe._outputPath, 3);
            fe.EndPrompt();
        }
        /// <summary>
        /// 将db转换为DataTable，内部版
        /// </summary>
        internal static void ConvertDatasTable_Intra()
        {
            FormExport fe = FormExport.GetInstance();
            //准备工作
            DirectoryInfo parentPath = System.IO.Directory.GetParent(fe._dbPath);
            fe._outputPath = parentPath + "\\" + fe._sn + '_' + fe._name + "_datas" + ".xlsx";
            if (!Tools.FileExistDelete(fe._outputPath))
                return;
            fe.lable提示.Text = "链接数据库...";
            string sql;
            SQLiteDataReader reader;
            try
            {
                sql = $"select  ts,setup_speed, speed, flow, p1, p2, p3, p, t1, t2 from datas where ts >= {fe._start} and ts <= {fe._end}";
                reader = Dal_admin.GetReader(fe._dbPath, sql);
            }
            catch
            {
                try
                {
                    sql = $"select  ts, speed, flow, p1, p2, p3, p, t1, t2 from datas where ts >= {fe._start} and ts <= {fe._end}";
                    reader = Dal_admin.GetReader(fe._dbPath, sql);
                }
                catch
                {
                    sql = $"select * from datas where ts >= {fe._start} and ts < {fe._end}";
                    reader = Dal_admin.GetReader(fe._dbPath, sql);
                }
            }
            //导出数据
            //string sql = "select * from datas";

            fe.lable提示.Text = "链接成功！";
            fe.lable提示.Text = "创建数据集...";
            string[] FiveHeader = {/*"cid",*/ "YYYY-MM-DD", "HH:MM:SS", "setup_speed", "speed(RPM)", "flow(LPM)", "P1(mmHg)", "P2(mmHg)", "P3(mmHg)", "P2-P3", "T1(℃)", "T2(℃)" };
            DataTable dataTable = new DataTable("ExportData");//*新建一张表 
            foreach (string TemStr in FiveHeader)
            {
                DataColumn strNameColumn = new DataColumn
                {
                    DataType = typeof(string),
                    ColumnName = TemStr
                };
                dataTable.Columns.Add(strNameColumn);
            }


            int count = 0;
            while (reader.Read())
            {

                DataRow rowData = dataTable.NewRow();   //*建立行数据
                long unixTimeStamp = (long)reader["ts"];
                if (count == 0)
                {
                    if (unixTimeStamp == 1647503856 && Convert.ToDouble(reader["speed"]) == 0)
                    {
                        TimeConversion._correctionValue = 0;
                    }
                    else
                    {
                        TimeConversion._correctionValue = 8 * 60 * 60;
                    }
                    count = 1;
                }
                //rowData["cid"] = reader["cid"];
                rowData["YYYY-MM-DD"] = TimeConversion.TimeStamp_Data(unixTimeStamp);
                rowData["HH:MM:SS"] = TimeConversion.TimeStamp_Time(unixTimeStamp);
                try
                {
                    rowData["setup_speed"] = Tools.IsNone(reader["setup_speed"]);
                }
                catch
                {

                }
                rowData["speed(RPM)"] = Tools.IsNone(reader["speed"]);
                rowData["flow(LPM)"] = Tools.IsNone(reader["flow"]);
                rowData["P1(mmHg)"] = Tools.IsNone(reader["p1"]);
                rowData["P2(mmHg)"] = Tools.IsNone(reader["p2"]);
                rowData["P3(mmHg)"] = Tools.IsNone(reader["p3"]);

                rowData["P2-P3"] = Tools.IsNone(reader["p"]);
                rowData["T1(℃)"] = Tools.IsNone(reader["t1"]);
                rowData["T2(℃)"] = Tools.IsNone(reader["t2"]);

                dataTable.Rows.Add(rowData);
                //count++;
            }
            fe.lable提示.Text = "数据集创建成功！";
            //MessageBox.Show("count:" + count);
            //progressBar1.Maximum = count + 2;

            fe.lable提示.Text = "转换数据...";

            //MessageBox.Show("已转换为dateTable");

            DataTableTo.DataTableToExcel_Intra(dataTable, fe._outputPath, 2);

            //DataTableToCsv(dataTable, _outputPath);

            fe.EndPrompt();
        }


    }
}
