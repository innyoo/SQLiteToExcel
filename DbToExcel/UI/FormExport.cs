using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DbToExcel.BLL;
using DbToExcel.DAL;

namespace DbToExcel
{
    internal partial class FormExport : Form
    {
        internal string dbPath;     //数据库路径
        internal string outputPath;     //输出路径

        internal string sn;     //标识号
        internal string name;   //患者姓名
        internal long start;  //开始时间
        internal long end;    //结束时间
        internal int rows = 0;  //数据条数

        //使用单例模式
        static readonly FormExport INSTANCE = new FormExport();
        FormExport()
        {
            InitializeComponent();
        }
        public static FormExport GetInstance()
        {
            return INSTANCE;
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

        void TextBox数据库路径_TextChanged(object sender, EventArgs e)
        {
            dbPath = textBox数据库路径.Text;
        }

        void TextBox标识号_TextChanged(object sender, EventArgs e)
        {
            sn = textBox标识号.Text;
        }

        void TextBox患者姓名_TextChanged(object sender, EventArgs e)
        {
            name = textBox患者姓名.Text;
        }

        void DateTimePicker开始时间_ValueChanged(object sender, EventArgs e)
        {
            DateTime sdt = dateTimePicker开始时间.Value;
            start = TimeConversion.DataTime_TimeStamp(sdt);
        }

        void DateTimePicker结束时间_ValueChanged(object sender, EventArgs e)
        {
            DateTime edt = dateTimePicker结束时间.Value;
            end = TimeConversion.DataTime_TimeStamp(edt);
        }

        void Form_Load(object sender, EventArgs e)
        {
            //允许跨线程操作UI
            CheckForIllegalCrossThreadCalls = false;
            //设置提示标签   
            lable提示.Parent = progressBar1;
            lable提示.BackColor = Color.Transparent;
            lable提示.ForeColor = Color.Green;
            lable提示.Location = new Point(5, 5);

            lable提示.Text = "";
            //在运行路径下寻找db文件，若存在则默认显示
            String[] dbFiles = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "*.db", SearchOption.TopDirectoryOnly);
            if (dbFiles.Length > 0) 
            {
                textBox数据库路径.Text = dbFiles[0];
            }   
            //获取默认开始时间和结束时间
            //DateTime sdt = dateTimePicker开始时间.Value;
            start = 0;
            //_start = TimeConversion.DataTime_TimeStamp(sdt);
            end = 2147483647;
            //DateTime edt = dateTimePicker结束时间.Value;
            //_end = TimeConversion.DataTime_TimeStamp(edt);

        }
        void CloseButton()
        {
            button浏览文件.Enabled = false;
            button导出老化.Enabled = false;
            button导出病例.Enabled = false;
            button导出事件.Enabled = false;
        }

        void OpenButton()
        {
            button浏览文件.Enabled = true;
            button导出老化.Enabled = true;
            button导出病例.Enabled = true;
            button导出事件.Enabled = true;
        }

        internal void EndPrompt()
        {
            //lable提示.Text = "导出完成！";

            lable提示.Text = "";
            DialogResult dr;
            if (File.Exists(outputPath))
            {
                dr = MessageBox.Show("共导出数据" + rows + "条，是否打开文件？", "导出结果", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            }
            else
            {
                if (rows == 0)
                {
                    dr = MessageBox.Show("导出失败，没有数据！", "导出结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    dr = MessageBox.Show("导出失败，未知错误！", "导出结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            progressBar1.Value = 0;
            rows = 0;
            OpenButton();
            if (dr == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(outputPath);
            }
        }
        void Button导出老化_Click(object sender, EventArgs e)
        {
            if (!Tools.DbExists(dbPath))
            {
                MessageBox.Show("未选择.db文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CloseButton();

            ThreadStart ts = new ThreadStart(ConvertTable.ConvertBurninTable);
            Thread t = new Thread(ts);
            t.Start();
            //ConvertTable.ConvertBurninTable();

            //EndPrompt();
        }
        void Button导出病例_Click(object sender, EventArgs e)
        {
            if (!Tools.DbExists(dbPath))
            {
                MessageBox.Show("未选择.db文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CloseButton();
            ThreadStart ts;
            if (IntraMode.IntraModeSwitch == true)
            {
                ts = new ThreadStart(ConvertTable.ConvertDatasTable_Intra);   
            }
            else
            {
                ts = new ThreadStart(ConvertTable.ConvertDatasTable);  
            }
            Thread t = new Thread(ts);
            t.Start();

            //ConvertDatasTable();  //普通转换
            //MassiveDatasExport();   //大量数据转换
            //EndPrompt();
        }

        void Button导出事件_Click(object sender, EventArgs e)
        {
            if (!Tools.DbExists(dbPath))
            {
                MessageBox.Show("未选择.db文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CloseButton();
            ThreadStart ts = new ThreadStart(ConvertTable.ConvertEventsTable);
            Thread t = new Thread(ts);
            t.Start();
            //ConvertTable.ConvertEventsTable();
            //EndPrompt();
        }

        private void Button内部模式_Click(object sender, EventArgs e)
        {
            if (IntraMode.IntraModeSwitch) 
            {
                IntraMode.IntraModeSwitch = false;
                button内部模式.Text = "外部模式";
                button内部模式.BackColor = System.Drawing.SystemColors.Control;
                button内部模式.ForeColor = System.Drawing.SystemColors.ControlText;
                button导出事件.Visible = false;
                button导出老化.Visible = false;
            }
            else
            {
                string inMsg = InputBox.ShowInputBox("请输入内部模式的密码", string.Empty);
                if (inMsg.Trim() == IntraMode.PASSWORD)
                {
                    IntraMode.IntraModeSwitch = true;
                    button内部模式.Text = "内部模式";
                    button内部模式.BackColor = Color.Teal;
                    button内部模式.ForeColor = System.Drawing.SystemColors.Control;
                    button导出事件.Visible = true;
                    button导出老化.Visible = true;
                }
                else
                {
                    MessageBox.Show("密码错误！", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
