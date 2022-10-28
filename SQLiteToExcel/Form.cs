﻿using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using SQLiteToExcel.BLL;
using SQLiteToExcel.DAL;
using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HorizontalAlignment = NPOI.SS.UserModel.HorizontalAlignment;

namespace SQLiteToExcel
{
    public partial class Form : System.Windows.Forms.Form
    {
        string _dbPath;     //数据库路径
        string _outputPath;     //输出路径

        String _sn;     //标识号
        String _name;   //患者姓名
        long _start;  //开始时间
        long _end;    //结束时间
        int _rows=0;  //数据条数
        Hashtable _filterlistSpeed = new Hashtable();
        Hashtable _slidelistP2 = new Hashtable();
        Hashtable _slidelistP3 = new Hashtable();

        public Form()
        {
            InitializeComponent();
            //允许跨线程操作UI
            CheckForIllegalCrossThreadCalls = false;

            lable提示.Parent = progressBar1;
            lable提示.BackColor = Color.Transparent;
            lable提示.ForeColor = Color.Green;
            lable提示.Location = new Point(5, 5);
        }

        private void 公用_DragEnter(object sender, DragEventArgs e)//拖动到工作区时发生
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }



        private void TextBox_DragDrop(object sender, DragEventArgs e)//拖放完成时发生
        {
            //这里显示文件名
            String path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (Tools.DbExists(path))
            {
                ((TextBox)sender).Text = path;
            }
        }

        private void Form_DragDrop(object sender, DragEventArgs e)//拖放完成时发生
        {
            //这里显示文件名
            String path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (Tools.DbExists(path))
            {
                textBox数据库路径.Text = path;
            }
        }

        private void Button浏览文件_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "SQLite(*.db)|*.db";
            DialogResult dr = openFileDialog1.ShowDialog();
            //获取所打开文件的文件名
            if (dr == DialogResult.OK && !string.IsNullOrEmpty(openFileDialog1.FileName))
            {
                textBox数据库路径.Text = openFileDialog1.FileName;
            }
        }

        private void TextBox数据库路径_TextChanged(object sender, EventArgs e)
        {
            _dbPath = textBox数据库路径.Text;
        }

        private void TextBox标识号_TextChanged(object sender, EventArgs e)
        {
            _sn = textBox标识号.Text;
        }

        private void TextBox患者姓名_TextChanged(object sender, EventArgs e)
        {
            _name = textBox患者姓名.Text;
        }

        private void DateTimePicker开始时间_ValueChanged(object sender, EventArgs e)
        {
            DateTime sdt = dateTimePicker开始时间.Value;
            _start = TimeConversion.DataTime_TimeStamp(sdt);
        }

        private void DateTimePicker结束时间_ValueChanged(object sender, EventArgs e)
        {
            DateTime edt = dateTimePicker结束时间.Value;
            _end = TimeConversion.DataTime_TimeStamp(edt);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            lable提示.Text = "";
            //在运行路径下寻找db文件，若存在则默认显示
            String[] dbFiles = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "*.db", SearchOption.TopDirectoryOnly);
            if (dbFiles.Length > 0)
            {
                textBox数据库路径.Text = dbFiles[0];
            }
            //获取默认开始时间和结束时间
            DateTime sdt = dateTimePicker开始时间.Value;
            _start = 0;
            //_start = TimeConversion.DataTime_TimeStamp(sdt);
            _end = 2147483647;
            //DateTime edt = dateTimePicker结束时间.Value;
            //_end = TimeConversion.DataTime_TimeStamp(edt);

        }
        private void CloseButton()
        {
            button浏览文件.Enabled = false;
            button导出老化.Enabled = false;
            button导出病例.Enabled = false;
            button导出事件.Enabled = false;
        }

        private void OpenButton()
        {
            button浏览文件.Enabled = true;
            button导出老化.Enabled = true;
            button导出病例.Enabled = true;
            button导出事件.Enabled = true;
        }

        private void EndPrompt()
        {
            //lable提示.Text = "导出完成！";

            lable提示.Text = "";
            DialogResult dr;
            if (File.Exists(_outputPath)) {
                dr = MessageBox.Show("共导出数据" + _rows + "条，是否打开文件？", "导出结果", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            } else
            {
                if (_rows == 0)
                {
                    dr = MessageBox.Show("导出失败，没有数据！", "导出结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    dr = MessageBox.Show("导出失败，未知错误！", "导出结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            progressBar1.Value = 0;
            _rows = 0;
            OpenButton();
            if (dr == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(_outputPath);
            }
        }
        private void Button导出老化_Click(object sender, EventArgs e)
        {
            if (!Tools.DbExists(_dbPath))
            {
                MessageBox.Show("未选择.db文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CloseButton();

            //ThreadStart ts = new ThreadStart(exportBurninFile);
            //Thread t = new Thread(ts);
            //t.Start();
            ConvertBurninTable();

            EndPrompt();
        }
        private void Button导出病例_Click(object sender, EventArgs e)
        {
            if (!Tools.DbExists(_dbPath))
            {
                MessageBox.Show("未选择.db文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CloseButton();

            //ThreadStart ts = new ThreadStart(ExportDatasFile);
            //Thread t = new Thread(ts);
            //t.Start();
            ConvertDatasTable();
            EndPrompt();
        }

        private void Button导出事件_Click(object sender, EventArgs e)
        {
            if (!Tools.DbExists(_dbPath))
            {
                MessageBox.Show("未选择.db文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CloseButton();
            ConvertEventsTable();
            EndPrompt();
        }

        private void ConvertBurninTable()   //将Burnin表的数据转换为DataTable
        {
            //准备工作
            DirectoryInfo di = System.IO.Directory.GetParent(_dbPath);
            _outputPath = di + "\\" + _sn + '_' + _name + '_' + "burnin" + ".xlsx";
            if (!Tools.FileExistDelete(_outputPath))
                return;
            lable提示.Text = "链接数据库...";
            SQLiteDataReader readerHead;
            try
            {
                readerHead = Dal_admin.GetReader(_dbPath, "SELECT DISTINCT ckey FROM burnin ORDER BY ckey");
            }
            catch
            {
                MessageBox.Show("当前db文件中不存在老化数据表！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lable提示.Text = "链接成功！";
            lable提示.Text = "创建数据集...";
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
            sql = $"SELECT burnin.cid, tsrecord, ckey, cvalue,speed FROM burnin LEFT OUTER JOIN datas ON datas.ts=burnin.tsrecord/1000 where tsrecord >= {_start * 1000} and tsrecord< {_end * 1000}";
            //导出数据
            //string sql = "select * from datas";
            SQLiteDataReader reader = Dal_admin.GetReader(_dbPath, sql);

             
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
            lable提示.Text = "数据集创建成功！";


            lable提示.Text = "转换数据...";
            DataTableToExcel(dataTable, _outputPath, 1);
        }
        private void ConvertDatasTable()     //将Datas表的数据转换为DataTable
        {
            //准备工作
            DirectoryInfo di = System.IO.Directory.GetParent(_dbPath);
            _outputPath = di + "\\" + _sn + '_' + _name +"_datas"+ ".xlsx";
            if (!Tools.FileExistDelete(_outputPath))
                return;
            lable提示.Text = "链接数据库...";
            string sql;
            SQLiteDataReader reader;
            if (_start < 1650985317 || _end > 1650985397)
            {
                try
                {
                    sql = $"select  ts, speed, flow, p1, p2, p3, p, t1, t2 from datas where ts >= {_start} and ts <= {_end} and (speed >= 0 and speed <=6500 or speed is NULL) and (flow>=-2 and flow<=12 or flow is NULL) and (p1>=-400 and p1<=400 or p1 is NULL) and (p2>=-400 and p2<=400 or p2 is NULL) and (p3>=-400 and p3<=400 or p3 is NULL) and ts not in(select ts from datas where ts>=1650985317 and ts <=1650985397 )";
                    reader = Dal_admin.GetReader(_dbPath, sql);
                }
                catch
                {
                    sql = $"select  ts, speed, flow, p1, p2, p3, p, t from datas where ts >= {_start} and ts < {_end}  and (speed >= 0 and speed <=6500 or speed is NULL) and (flow>=-2 and flow<=12 or flow is NULL) and (p1>=-400 and p1<=400 or p1 is NULL) and (p2>=-400 and p2<=400 or p2 is NULL) and (p3>=-400 and p3<=400 or p3 is NULL) and ts not in(select ts from datas where ts>=1650985317 and ts <=1650985397 )";
                    reader = Dal_admin.GetReader(_dbPath, sql);
                }
            }
            else
            {
                try
                {
                    sql = $"select ts, speed, flow, p1, p2, p3, p, t1,t2 from datas where ts >= {_start} and ts < {_end}  and (speed >= 0 and speed <=6500 or speed is NULL) and (flow>=-2 and flow<=12 or flow is NULL) and (p1>=-400 and p1<=400 or p1 is NULL) and (p2>=-400 and p2<=400 or p2 is NULL) and (p3>=-400 and p3<=400 or p3 is NULL)";
                    reader = Dal_admin.GetReader(_dbPath, sql);
                }
                catch
                {
                    sql = $"select ts, speed, flow, p1, p2, p3, p, t from datas where ts >= {_start} and ts < {_end}  and (speed >= 0 and speed <=6500 or speed is NULL) and (flow>=-2 and flow<=12 or flow is NULL) and (p1>=-400 and p1<=400 or p1 is NULL) and (p2>=-400 and p2<=400 or p2 is NULL) and (p3>=-400 and p3<=400 or p3 is NULL)";
                    reader = Dal_admin.GetReader(_dbPath, sql);
                }
            }
            //导出数据
            //string sql = "select * from datas";

            lable提示.Text = "链接成功！";
            lable提示.Text = "创建数据集...";
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
                    if(unixTimeStamp== 1647503856 && Convert.ToDouble(reader["speed"]) == 0)
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
                    rowData["speed(RPM)"] = TimeFilter(_filterlistSpeed, unixTimeStamp, Convert.ToDouble(reader["speed"]), 50, 7);//滤波
                }
                catch
                {
                    rowData["speed(RPM)"] = "--";//滤波

                }
                rowData["flow(LPM)"] = Tools.IsNone(reader["flow"]);
                rowData["P1(mmHg)"] = Tools.IsNone(reader["p1"]);
                try
                {
                    rowData["P2(mmHg)"] = MovingAverageFilter(_slidelistP2, unixTimeStamp, Convert.ToDouble(reader["p2"]));
                }
                catch
                {
                    rowData["P2(mmHg)"] = Tools.IsNone(reader["p2"]); ;
                }
                try
                {
                    rowData["P3(mmHg)"] = MovingAverageFilter(_slidelistP3, unixTimeStamp, Convert.ToDouble(reader["p3"]));
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
            Console.ReadLine();
            lable提示.Text = "数据集创建成功！";
            //MessageBox.Show("count:" + count);
            //progressBar1.Maximum = count + 2;

            lable提示.Text = "转换数据...";
            DataTableToExcel(dataTable, _outputPath, 2);
        }


        private void ConvertEventsTable()     //将Events表的数据转换为DataTable 
        {
            //准备工作
            DirectoryInfo di = System.IO.Directory.GetParent(_dbPath);
            _outputPath = di + "\\" + _sn + '_' + _name + "_" + "events" + ".xlsx";
            if (!Tools.FileExistDelete(_outputPath))
                return;
            lable提示.Text = "链接数据库...";
            string sql = $"select cid, ts, level, code, para1, ishand from events where ts >= {_start} and ts< {_end}";
            //导出数据
            SQLiteDataReader reader = Dal_admin.GetReader(_dbPath, sql);

            lable提示.Text = "链接成功！";
            lable提示.Text = "创建数据集...";
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
            lable提示.Text = "数据集创建成功！";
            //MessageBox.Show("count:" + count);
            //progressBar1.Maximum = count + 2;

            lable提示.Text = "转换数据...";
            DataTableToExcel(dataTable, _outputPath, 3);
        }


        public void DataTableToExcel(DataTable dataTable, string outPath, int mode)   //将DataTable转换为Excel
        {
            int SheetCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dataTable.Rows.Count) / 1000000));     //表格的数量
            IWorkbook workbook;
            FileStream fs = null;
            //int _rows = 0;  //进度条计数
            try
            {
                 if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    workbook = new XSSFWorkbook();
                    for (int k = 1; k <= SheetCount; k++)
                    {
                        ISheet sheet = workbook.CreateSheet("Sheet" + k);       //创建sheet表格
                        int rowCount = dataTable.Rows.Count;        //获取dataTable行数
                        int columnCount = dataTable.Columns.Count;      //获取dataTable列数

                        //创建单元格样式：列头风格
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;   //水平居中
                        headStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;    //下边框线
                        headStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;      //左边框线
                        headStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;     //右边框线
                        headStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;       //上边框线
                        headStyle.Alignment = HorizontalAlignment.Center;
                        headStyle.WrapText = true;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        //font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                        font.IsBold = true;//是否加粗
                        font.FontName = "微软雅黑";
                        headStyle.SetFont(font);//HEAD 样式


                        //设置列头  
                        IRow row = sheet.CreateRow(0);      //创建sheet的第0行作为列头

                        for (int i = 0; i < columnCount; i++)   //赋值字段名
                        {
                            ICell cell = row.CreateCell(i);
                            cell.CellStyle = headStyle;
                            cell.SetCellValue(dataTable.Columns[i].ColumnName);
                        }
                        int range = 0;  //范围 
                        if (k == SheetCount)
                        {
                            range = rowCount;
                        }
                        else
                        {
                            range = k * 1000000;
                        }

                        //设置进度条最大值
                        progressBar1.Maximum = range;

                        //创建单元格样式：文本型风格
                        ICellStyle textStyle = workbook.CreateCellStyle();

                        IDataFormat dataFormatCustom1 = workbook.CreateDataFormat();
                        textStyle.DataFormat = dataFormatCustom1.GetFormat("text");
                        //cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;    //下边框线
                        //cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;      //左边框线
                        //cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;     //右边框线
                        //cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;       //上边框线
                        textStyle.Alignment = HorizontalAlignment.Center;
                        IFont font1 = workbook.CreateFont();
                        font1.FontHeightInPoints = 11;
                        font1.IsBold = false;//是否加粗
                        font1.FontName = "微软雅黑";
                        textStyle.SetFont(font1);

                        //创建单元格样式：整型风格
                        ICellStyle intStyle = workbook.CreateCellStyle();

                        IDataFormat dataFormatCustom2 = workbook.CreateDataFormat();
                        intStyle.DataFormat = dataFormatCustom2.GetFormat("0");
                        //cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;    //下边框线
                        //cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;      //左边框线
                        //cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;     //右边框线
                        //cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;       //上边框线
                        IFont font2 = workbook.CreateFont();
                        font2.FontHeightInPoints = 11;
                        font2.IsBold = false;//是否加粗
                        font2.FontName = "微软雅黑";
                        intStyle.SetFont(font2);

                        //创建单元格样式：一位浮点型风格
                        ICellStyle floatStyle = workbook.CreateCellStyle();

                        IDataFormat dataFormatCustom3 = workbook.CreateDataFormat();
                        floatStyle.DataFormat = dataFormatCustom3.GetFormat("0.0");
                        floatStyle.SetFont(font2);

                        //创建单元格样式：两位浮点型风格
                        ICellStyle doubleStyle = workbook.CreateCellStyle();

                        IDataFormat dataFormatCustom4 = workbook.CreateDataFormat();
                        doubleStyle.DataFormat = dataFormatCustom4.GetFormat("0.00");
                        doubleStyle.SetFont(font2);
                        double temporarySpeed = 0;      //临时存储当前速度值，用于老化数据表

                        for (int i = (k - 1) * 1000000, l = 1; i < range; i++, l++)
                        {

                            row = sheet.CreateRow(l);
                            if (i <= rowCount)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {

                                    ICell cell = row.CreateCell(j);     //创建单元格
                                    
                                    switch (mode)
                                    {
                                        case 1:     //老化数据表的单元格
                                            if (j == 1 || j == 2)
                                            {
                                                cell.CellStyle = textStyle;
                                                cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                            }
                                            else if(j == 3){
                                                try
                                                {
                                                    cell.CellStyle = intStyle;
                                                    temporarySpeed = Convert.ToDouble(dataTable.Rows[i][j].ToString());
                                                    cell.SetCellValue(temporarySpeed);
                                                }
                                                catch
                                                {
                                                    cell.CellStyle = intStyle;
                                                    cell.SetCellValue(temporarySpeed);
                                                }
                                            }
                                            else
                                            {

                                                try
                                                {
                                                    cell.CellStyle = intStyle;
                                                    cell.SetCellValue(Convert.ToDouble(dataTable.Rows[i][j].ToString()));
                                                }
                                                catch
                                                {
                                                    cell.CellStyle = textStyle;
                                                    cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                                }
                                            }

                                            break;
                                        case 2:     //病例数据表的单元格
                                            if (j == 2 || j == 4 || j == 5 || j == 6)
                                            {
                                                try
                                                {
                                                    cell.CellStyle = intStyle;
                                                    cell.SetCellValue(Convert.ToDouble(dataTable.Rows[i][j].ToString()));
                                                }
                                                catch
                                                {
                                                    cell.CellStyle = textStyle;
                                                    cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                                }
                                            }
                                            else if (j == 3)
                                            {
                                                try
                                                {
                                                    cell.CellStyle = doubleStyle;
                                                    cell.SetCellValue(Convert.ToDouble(dataTable.Rows[i][j].ToString()));
                                                }
                                                catch
                                                {
                                                    cell.CellStyle = textStyle;
                                                    cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                                }
                                            }
                                            else if (j == 8 || j == 9)
                                            {
                                                try
                                                {
                                                    cell.CellStyle = floatStyle;
                                                    cell.SetCellValue(Convert.ToDouble(dataTable.Rows[i][j].ToString()));
                                                }
                                                catch
                                                {
                                                    cell.CellStyle = textStyle;
                                                    cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                                }
                                            }
                                            else if (j == 7)
                                            {
                                                try
                                                {
                                                    cell.CellStyle = intStyle;
                                                    cell.SetCellValue(Convert.ToDouble(dataTable.Rows[i][5].ToString()) - Convert.ToDouble(dataTable.Rows[i][6].ToString()));
                                                }
                                                catch
                                                {
                                                    cell.CellStyle = textStyle;
                                                    cell.SetCellValue("--");
                                                }
                                            }
                                            else
                                            {
                                                cell.CellStyle = textStyle;
                                                cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                            }
                                            break;
                                        case 3:         //系统事件表 的单元格
                                            cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                            if (j == 2 || j == 3 ||j==5|| j == 6)
                                            {
                                                try
                                                {
                                                cell.CellStyle = intStyle;
                                                    cell.SetCellValue(Convert.ToDouble(dataTable.Rows[i][j].ToString()));
                                                }
                                                catch
                                                {
                                                cell.CellStyle = textStyle;
                                                    cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                                }

                                            }
                                            else
                                            {
                                                cell.CellStyle = textStyle;
                                                cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                            }
                                            break;
                                        default:
                                            break;
                                    }



                                    if (_rows < rowCount)
                                        _rows++;

                                    progressBar1.Value = _rows;     //进度条进度
                                    //lable提示.Text = "数据集数据数量：" + count;
                                }

                            }

                        }
                        
                        switch (mode)   //设置列宽
                        {
                            case 1:     //老化数据表的列宽
                                for (int i = 0; i < columnCount; i++)
                                {
                                    if (i == 1)
                                    {
                                        sheet.SetColumnWidth(i, 13 * 256);
                                    }
                                    else if (i == 2)
                                    {
                                        sheet.SetColumnWidth(i, 10 * 256);
                                    }
                                    else
                                    {
                                        sheet.SetColumnWidth(i, 9 * 256);
                                    }
                                }
                                break;
                            case 2:     //病例数据表的列宽
                                for (int i = 0; i < columnCount; i++)
                                {
                                    if (i == 0 || i == 7)
                                    {
                                        sheet.SetColumnWidth(i, 13 * 256);
                                    }
                                    else if (i == 2)
                                    {
                                        sheet.SetColumnWidth(i, 11 * 256);
                                    }
                                    else if (i == 8 || i == 9)
                                    {
                                        sheet.SetColumnWidth(i, 6 * 256);
                                    }
                                    else
                                    {
                                        sheet.SetColumnWidth(i, 10 * 256);
                                    }
                                }
                                break;
                            case 3:     //系统事件的列宽
                                for (int i = 0; i < columnCount; i++)
                                {
                                    if (i == 1)
                                    {
                                        sheet.SetColumnWidth(i, 10 * 256);
                                    }
                                    else if (i == 2)
                                    {
                                        sheet.SetColumnWidth(i, 5 * 256);
                                    }
                                    else if (i == 3)
                                    {
                                        sheet.SetColumnWidth(i, 6 * 256);
                                    }
                                    else if (i == 4)
                                    {
                                        sheet.SetColumnWidth(i, 25 * 256);
                                    }
                                    else if (i == 6)
                                    {
                                        sheet.SetColumnWidth(i, 7 * 256);
                                    }
                                    else
                                    {
                                        sheet.SetColumnWidth(i, 13 * 256);
                                    }
                                }
                                break;
                            default:
                                break;

                        }
                        sheet.SetAutoFilter(new CellRangeAddress(0, 0, 0, dataTable.Columns.Count - 1)); //首行筛选
                        sheet.CreateFreezePane(dataTable.Columns.Count, 1); //首行冻结

                    }

                    //lable_process.Text = "写入到excel...";

                    using (fs = File.OpenWrite(outPath))
                    {
                        workbook.Write(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                if (fs != null)
                {
                    fs.Close();
                }
            }


        }//end

        public static double TimeFilter(Hashtable timelist, long datatime, double currentV, int threshold, int times)
        {
            double returnV = currentV;
            //int lasttime = 0
            ArrayList key = new ArrayList();
            foreach (long k in timelist.Keys)
            {
                key.Add(k);
            }
            foreach (long k in key)
            {
                if ((datatime - k) > times || datatime - k < 0)     //时间范围超出
                {
                    timelist.Remove(k);
                }
            }

            bool needFilter = false;
            double threshold_max = currentV + Math.Abs(threshold);
            double threshold_min = currentV - Math.Abs(threshold);
            ArrayList value = new ArrayList();
            foreach (double v in timelist.Values)
            {
                value.Add(v);
                if (v > threshold_max || v < threshold_min)      //超出阈值，直接使用当前值
                {
                    timelist.Clear();
                    break;
                }
                else
                {
                    if (v != currentV)
                    {
                        needFilter = true;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            if (needFilter)
            {
                double max = (double)value[0];
                foreach (double val in value)
                {
                    if ((double)val > max)
                    {
                        max = (double)val;
                    }
                }
                returnV = max;
            }
            timelist[datatime] = currentV;
            return returnV;
        }

        public static double MovingAverageFilter(Hashtable slidelist, long datatime, double currentV)
        {
            slidelist.Add(datatime, currentV);
            ArrayList key = new ArrayList();
            foreach (long k in slidelist.Keys)
            {
                key.Add(k);
            }

            double slidetotal = 0;
            double slidecount = 0;
            foreach (long k in key)
            {
                if ((datatime - k) > 4 || (datatime - k) < 0)
                {
                    //时间范围超出
                     slidelist.Remove(k);
                }
                else
                {
                    slidecount++;
                    slidetotal += (double)slidelist[k];
                }
            }

            double slideaverage;
            if (slidecount > 0)
            {
                slideaverage = slidetotal / slidecount;
            }
            else
            {
                slideaverage = currentV;
            }
            return slideaverage;
        }


    }
}
