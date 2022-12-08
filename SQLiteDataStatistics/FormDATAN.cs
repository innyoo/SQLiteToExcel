using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using SQLiteToExcel.BLL;
using SQLiteToExcel.DAL;
using SQLiteToExcel;
using System.Data.SQLite;

namespace SQLiteDataStatistics
{
    public partial class FormDATAN : Form
    {



        public FormDATAN()
        {
            InitializeComponent();
        }



        private void FormDATAN_Load(object sender, EventArgs e)
        {
            //获取默认开始时间和结束时间
            //GlobalVariable.startTime = 0;
            //GlobalVariable.endTime = 2147483647;
            DateTime sdt = dateTimePicker开始时间.Value;
            GlobalVariable.startTime = TimeConversion.DataTime_TimeStamp(sdt);
            DateTime edt = dateTimePicker结束时间.Value;
            GlobalVariable.endTime = TimeConversion.DataTime_TimeStamp(edt);
            GlobalVariable.minimum = (float)numericUpDown最小值.Value;
            GlobalVariable.maximum = (float)numericUpDown最大值.Value;


            //在某路径下寻找db文件，若存在则默认显示
            String[] dbFiles = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "*.db", SearchOption.TopDirectoryOnly);
            if (dbFiles.Length > 0)
            {
                textBox数据库路径.Text = dbFiles[0];
                GetAllTableName();
            }
        }


        void 公用_DragEnter(object sender, DragEventArgs e)//拖动到工作区时发生
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }
        void TextBox_DragDrop(object sender, DragEventArgs e)//拖放完成时发生
        {
            //这里显示文件名
            String path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (Tools.DbExists(path))
            {
                ((TextBox)sender).Text = path;
            }
        }
        void Form_DragDrop(object sender, DragEventArgs e)//拖放完成时发生
        {
            //这里显示文件名
            String path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (Tools.DbExists(path))
            {
                textBox数据库路径.Text = path;
            }
        }
        private void TextBox数据库路径_TextChanged(object sender, EventArgs e)
        {
            GlobalVariable.dbPath = ((TextBox)sender).Text;
            GetAllTableName();
        }
        void Button浏览文件_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "SQLite(*.db)|*.db";
            DialogResult dr = openFileDialog1.ShowDialog();
            //获取所打开文件的文件名
            if (dr == DialogResult.OK && !string.IsNullOrEmpty(openFileDialog1.FileName))
            {
                textBox数据库路径.Text = openFileDialog1.FileName;
            }
        }
        private void ComboBox表_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVariable.tableName =((ComboBox)sender).Text;
        }
        private void ComboBox字段_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVariable.fieldName = ((ComboBox)sender).Text;
        }

        private void DateTimePicker开始时间_ValueChanged(object sender, EventArgs e)
        {
            DateTime sdt = dateTimePicker开始时间.Value;
            GlobalVariable.startTime = TimeConversion.DataTime_TimeStamp(sdt);
        }

        private void DateTimePicker结束时间_ValueChanged(object sender, EventArgs e)
        {
            DateTime edt = dateTimePicker结束时间.Value;
            GlobalVariable.endTime = TimeConversion.DataTime_TimeStamp(edt);
        }
        private void NumericUpDown最小值_ValueChanged(object sender, EventArgs e)
        {
            GlobalVariable.minimum = (float)((NumericUpDown)sender).Value;
        }

        private void NumericUpDown最大值_ValueChanged(object sender, EventArgs e)
        {
            GlobalVariable.maximum = (float)((NumericUpDown)sender).Value;
        }
        private void ComboBox表_TextChanged(object sender, EventArgs e)
        {
            comboBox字段.Text = "";
            GetAllFieldName(((ComboBox)sender).Text);

        }
        /// <summary>
        /// 获取全部表的名称，写入到表选择框
        /// </summary>
        private void GetAllTableName()
        {
            string sql = "SELECT name FROM sqlite_master WHERE type = 'table'";
            SQLiteDataReader reader = Dal_admin.GetReader(GlobalVariable.dbPath, sql);

            comboBox表.Items.Clear();
            while (reader.Read())
                {
                string temp = reader.GetString(0);
                comboBox表.Items.Add(temp);
                if (temp == "datas")
                {
                    comboBox表.Text = "datas";
                }
                }
        }
        /// <summary>
        /// 获取指定表的所有字段名
        /// </summary>
        /// <param name="表名"></param>
        private void GetAllFieldName(String 表名)
        {
            string sql = $"PRAGMA table_info({表名});";
            SQLiteDataReader reader = Dal_admin.GetReader(GlobalVariable.dbPath, sql);
            comboBox字段.Items.Clear();
            while (reader.Read())
            {
                string temp = (string)reader["name"];
                comboBox字段.Items.Add(temp);
                if (temp == "flow")
                {
                    comboBox字段.Text = "flow";
                }
            }
        }

        void CloseButton()
        {
            button浏览文件.Enabled = false;
            button输出分析文档.Enabled = false;
        }
        void OpenButton()
        {
            button浏览文件.Enabled = true;
            button输出分析文档.Enabled = true;
        }

        /// <summary>
        /// 收尾
        /// </summary>
        /// <param name="输出路径"></param>
        internal void Ending(string 输出路径)
        {
            //lable提示.Text = "导出完成！";

            label提示语.Text = "";
            DialogResult dr= DialogResult.No;
            if (File.Exists(输出路径))
            {
                dr = MessageBox.Show("分析完成，是否打开分析报告？", "分析结果", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }

            if (dr == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(输出路径);
            }
        }


        private void Button输出分析文档_Click(object sender, EventArgs e)
        {
            label提示语.Text = "分析中，请稍候...";

            CloseButton();
            if (GlobalVariable.tableName == "datas" && GlobalVariable.fieldName == "flow"&&radioButton连续.Checked)
            {
                DirectoryInfo parentPath = System.IO.Directory.GetParent(GlobalVariable.dbPath);

                GlobalVariable.flowReportPath = parentPath + "\\" +parentPath.Name+ "_flow分析" + "" + ".csv";
                if (Tools.FileExistDelete(GlobalVariable.flowReportPath))
                {
                    if (FlowRange.FlowAnalysis())
                    {
                        Ending(GlobalVariable.flowReportPath);
                    }
                }
            }
            else
            {
                MessageBox.Show("该组合的分析方式还未开发", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            
            OpenButton();
            label提示语.Text = "";
        }


    }
}
