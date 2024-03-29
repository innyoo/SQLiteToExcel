﻿using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.Streaming;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteToExcel.BLL
{
    public class DataTableTo
    {
        /// <summary>
        /// DataTable转Csv
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="filePath">要保存到的文件路径</param>
        public static void DataTableToCsv(DataTable dataTable, string filePath)   
        {
            StringBuilder titleBuilder = new StringBuilder();
            StringBuilder lineBuilder = new StringBuilder();
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.Default);

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                titleBuilder.Append(dataTable.Columns[i].ColumnName + ","); //栏位：自动跳到下一单元格
            }

            string title = titleBuilder.ToString();
            title = title.Substring(0, title.Length - 1) + "\n";

            sw.Write(title);

            string line = string.Empty;
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    lineBuilder.Append(row[i].ToString().Trim() + ","); //内容：自动跳到下一单元格
                }

                line = lineBuilder.ToString();
                line = line.Substring(0, line.Length - 1) + "\n";
                sw.Write(line);
                 
                lineBuilder.Clear();
            }

            sw.Close();
            fs.Close();
        }

        internal static void DataTableToExcel(DataTable dataTable, string outPath, int mode)   //将DataTable转换为Excel
        {
            int maximum = 500000;     //每个sheet最大行数
            int SheetCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dataTable.Rows.Count) / maximum));     //表格的数量
            IWorkbook workbook;
            //if (File.Exists(outPath))  
            //{
            //    using (FileStream stream = File.OpenRead(outPath))
            //    workbook = new XSSFWorkbook(stream);
            //}
            FormExport form = FormExport.GetInstance();

            int 进度 = 0;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                workbook = new SXSSFWorkbook(5000);
                for (int k = 1; k <= SheetCount; k++)
                {
                    ISheet sheet = workbook.CreateSheet("Sheet" + k);       //创建sheet表格
                    int rowCount = dataTable.Rows.Count;        //获取dataTable行数
                    int columnCount = dataTable.Columns.Count;      //获取dataTable列数

                    //设置进度条最大值
                    
                    form.progressBar1.Maximum = rowCount;

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
                    int range;  //范围 
                    if (k == SheetCount)
                    {
                        range = rowCount;
                    }
                    else
                    {
                        range = k * maximum;
                    }



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

                    for (int i = (k - 1) * maximum, l = 1; i < range; i++, l++)     //行
                    {


                        row = sheet.CreateRow(l);
                        if (i <= rowCount)
                        {

                            for (int j = 0; j < columnCount; j++)   //列
                            {
                                try
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
                                            else if (j == 3)
                                            {
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
                                                try
                                                {

                                                    cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                                }
                                                catch
                                                {
                                                    cell.SetCellValue("--");
                                                }

                                            }
                                            break;
                                        case 3:         //系统事件表 的单元格
                                            cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                            if (j == 2 || j == 3 || j == 5 || j == 6)
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
                                }
                                catch
                                {
                                    break;
                                }
                            }

                        }
                        if (进度 < range)
                        {
                            进度++;
                            form.progressBar1.Value = 进度;
                        }
                        form._rows++;
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
                                if (i == 0)
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

                form.lable提示.Text = "写入到文件...";

                //释放dataTable
                dataTable.Clear();
                dataTable.Dispose();
                dataTable = null;
                GC.Collect();

                //MemoryStream ms = new MemoryStream();
                //workbook.Write(ms);
                using (FileStream fs = new FileStream(outPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    fs.Flush();
                    workbook.Write(fs);
                    workbook = null;
                    fs.Close();


                    //fs.Close();
                }
                //byte[] bArr = ms.ToArray();
                //MessageBox.Show("总长度：" + bArr.Length);
                //for (int i = 0; i < bArr.Length;)
                //{
                //    using (FileStream fs = new FileStream(outPath, FileMode.Append))
                //    {
                //        int n = 1;
                //        //fs.Seek(0, SeekOrigin.End);
                //        //MessageBox.Show(i + "\t\t" + bArr.Length);


                //        if (i >= bArr.Length - 100000)
                //        {
                //            byte[] brr = bArr.Skip(i).ToArray();
                //             fs.Write(brr, 0, brr.Length);

                //            break;
                //        }
                //        else
                //        {
                //            byte[] brr = bArr.Skip(i).Take(i + 100000).ToArray();
                //            fs.Write(brr, 0, brr.Length);
                //            n++;
                //            i += brr.Length;
                //        }
                //        fs.Flush();
                //        fs.Close();
                //    }
                //}

            }


        }//end
        /// <summary>
        /// 将DataTable转换为Excel，内部版
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="outPath"></param>
        /// <param name="mode"></param>
        internal static void DataTableToExcel_Intra(DataTable dataTable, string outPath, int mode)  
        {
            int maximum = 1000000;     //每个sheet最大行数
            int SheetCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dataTable.Rows.Count) / maximum));     //表格的数量
            IWorkbook workbook;
            FormExport form = FormExport.GetInstance();

            int 进度 = 0;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                workbook = new SXSSFWorkbook(10000);
                for (int k = 1; k <= SheetCount; k++)
                {
                    ISheet sheet = workbook.CreateSheet("Sheet" + k);       //创建sheet表格
                    int rowCount = dataTable.Rows.Count;        //获取dataTable行数
                    int columnCount = dataTable.Columns.Count;      //获取dataTable列数

                    //设置进度条最大值

                    form.progressBar1.Maximum = rowCount;

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
                    int range;  //范围 
                    if (k == SheetCount)
                    {
                        range = rowCount;
                    }
                    else
                    {
                        range = k * maximum;
                    }



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
                    //double temporarySpeed = 0;      //临时存储当前速度值，用于老化数据表

                    for (int i = (k - 1) * maximum, l = 1; i < range; i++, l++)     //行
                    {


                        row = sheet.CreateRow(l);
                        if (i <= rowCount)
                        {

                            for (int j = 0; j < columnCount; j++)   //列
                            {
                                try
                                {
                                    ICell cell = row.CreateCell(j);     //创建单元格

                                    switch (mode)
                                    {
                                        //case 1:     //老化数据表的单元格
                                        //    if (j == 1 || j == 2)
                                        //    {
                                        //        cell.CellStyle = textStyle;
                                        //        cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                        //    }
                                        //    else if (j == 3)
                                        //    {
                                        //        try
                                        //        {
                                        //            cell.CellStyle = intStyle;
                                        //            temporarySpeed = Convert.ToDouble(dataTable.Rows[i][j].ToString());
                                        //            cell.SetCellValue(temporarySpeed);
                                        //        }
                                        //        catch
                                        //        {
                                        //            cell.CellStyle = intStyle;
                                        //            cell.SetCellValue(temporarySpeed);
                                        //        }
                                        //    }
                                        //    else
                                        //    {

                                        //        try
                                        //        {
                                        //            cell.CellStyle = intStyle;
                                        //            cell.SetCellValue(Convert.ToDouble(dataTable.Rows[i][j].ToString()));
                                        //        }
                                        //        catch
                                        //        {
                                        //            cell.CellStyle = textStyle;
                                        //            cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                        //        }
                                        //    }

                                        //    break;
                                        case 2:     //病例数据表的单元格
                                            if (j==2||j == 3 || j == 5 || j == 6 || j == 7)
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
                                            else if (j == 4)
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
                                            else if (j == 9 || j == 10)
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
                                            else if (j == 8)
                                            {
                                                try
                                                {
                                                    cell.CellStyle = intStyle;
                                                    cell.SetCellValue(Convert.ToDouble(dataTable.Rows[i][6].ToString()) - Convert.ToDouble(dataTable.Rows[i][7].ToString()));
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
                                                try
                                                {

                                                    cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                                }
                                                catch
                                                {
                                                    cell.SetCellValue("--");
                                                }

                                            }
                                            break;
                                        //case 3:         //系统事件表 的单元格
                                        //    cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                        //    if (j == 2 || j == 3 || j == 5 || j == 6)
                                        //    {
                                        //        try
                                        //        {
                                        //            cell.CellStyle = intStyle;
                                        //            cell.SetCellValue(Convert.ToDouble(dataTable.Rows[i][j].ToString()));
                                        //        }
                                        //        catch
                                        //        {
                                        //            cell.CellStyle = textStyle;
                                        //            cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                        //        }

                                        //    }
                                        //    else
                                        //    {
                                        //        cell.CellStyle = textStyle;
                                        //        cell.SetCellValue(dataTable.Rows[i][j].ToString());
                                        //    }
                                        //    break;
                                        default:
                                            break;
                                    }
                                }
                                catch
                                {
                                    break;
                                }
                            }

                        }
                        if (进度 < range)
                        {
                            进度++;
                            form.progressBar1.Value = 进度;
                        }
                        form._rows++;
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
                                if (i == 0)
                                {
                                    sheet.SetColumnWidth(i, 13 * 256);
                                }
                                else if (i==2||i == 3)
                                {
                                    sheet.SetColumnWidth(i, 11 * 256);
                                }
                                else if (i == 9 || i == 10)
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

                form.lable提示.Text = "写入到文件...";

                //释放dataTable
                dataTable.Clear();
                dataTable.Dispose();
                dataTable = null;
                GC.Collect();

                //MemoryStream ms = new MemoryStream();
                //workbook.Write(ms);
                using (FileStream fs = new FileStream(outPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    fs.Flush();
                    workbook.Write(fs);
                    workbook = null;
                    fs.Close();
                }

            }


        }//end

    }
}
