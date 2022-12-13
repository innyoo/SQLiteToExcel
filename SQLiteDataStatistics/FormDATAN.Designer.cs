namespace SQLiteDataStatistics
{
    partial class FormDATAN
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDATAN));
            this.button输出分析文档 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButton连续 = new System.Windows.Forms.RadioButton();
            this.radioButton独立 = new System.Windows.Forms.RadioButton();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown最大值 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown最小值 = new System.Windows.Forms.NumericUpDown();
            this.dateTimePicker结束时间 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker开始时间 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox表 = new System.Windows.Forms.ComboBox();
            this.comboBox字段 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox数据库路径 = new System.Windows.Forms.TextBox();
            this.button浏览文件 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label提示语 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown最大值)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown最小值)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button输出分析文档
            // 
            this.button输出分析文档.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button输出分析文档.Location = new System.Drawing.Point(244, 292);
            this.button输出分析文档.Name = "button输出分析文档";
            this.button输出分析文档.Size = new System.Drawing.Size(123, 39);
            this.button输出分析文档.TabIndex = 0;
            this.button输出分析文档.Text = "输出分析文档";
            this.button输出分析文档.UseVisualStyleBackColor = true;
            this.button输出分析文档.Click += new System.EventHandler(this.Button输出分析文档_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(305, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "表中字段：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(176, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "~";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(20, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 21);
            this.label4.TabIndex = 8;
            this.label4.Text = "要分析的表：";
            // 
            // radioButton连续
            // 
            this.radioButton连续.AutoSize = true;
            this.radioButton连续.Checked = true;
            this.radioButton连续.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton连续.Location = new System.Drawing.Point(24, 21);
            this.radioButton连续.Name = "radioButton连续";
            this.radioButton连续.Size = new System.Drawing.Size(76, 25);
            this.radioButton连续.TabIndex = 9;
            this.radioButton连续.TabStop = true;
            this.radioButton连续.Text = "连续：";
            this.radioButton连续.UseVisualStyleBackColor = true;
            // 
            // radioButton独立
            // 
            this.radioButton独立.AutoSize = true;
            this.radioButton独立.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton独立.Location = new System.Drawing.Point(309, 19);
            this.radioButton独立.Name = "radioButton独立";
            this.radioButton独立.Size = new System.Drawing.Size(76, 25);
            this.radioButton独立.TabIndex = 10;
            this.radioButton独立.Text = "独立：";
            this.radioButton独立.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(384, 23);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(176, 21);
            this.textBox4.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDown最大值);
            this.groupBox1.Controls.Add(this.numericUpDown最小值);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.radioButton独立);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.radioButton连续);
            this.groupBox1.Location = new System.Drawing.Point(12, 211);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(587, 66);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "取值范围";
            this.groupBox1.Enter += new System.EventHandler(this.GroupBox1_Enter);
            // 
            // numericUpDown最大值
            // 
            this.numericUpDown最大值.DecimalPlaces = 1;
            this.numericUpDown最大值.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown最大值.Location = new System.Drawing.Point(198, 23);
            this.numericUpDown最大值.Name = "numericUpDown最大值";
            this.numericUpDown最大值.Size = new System.Drawing.Size(72, 21);
            this.numericUpDown最大值.TabIndex = 17;
            this.numericUpDown最大值.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown最大值.ValueChanged += new System.EventHandler(this.NumericUpDown最大值_ValueChanged);
            // 
            // numericUpDown最小值
            // 
            this.numericUpDown最小值.DecimalPlaces = 1;
            this.numericUpDown最小值.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown最小值.Location = new System.Drawing.Point(100, 23);
            this.numericUpDown最小值.Name = "numericUpDown最小值";
            this.numericUpDown最小值.Size = new System.Drawing.Size(73, 21);
            this.numericUpDown最小值.TabIndex = 16;
            this.numericUpDown最小值.ValueChanged += new System.EventHandler(this.NumericUpDown最小值_ValueChanged);
            // 
            // dateTimePicker结束时间
            // 
            this.dateTimePicker结束时间.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker结束时间.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker结束时间.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker结束时间.Location = new System.Drawing.Point(383, 30);
            this.dateTimePicker结束时间.MaxDate = new System.DateTime(2070, 12, 31, 0, 0, 0, 0);
            this.dateTimePicker结束时间.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker结束时间.Name = "dateTimePicker结束时间";
            this.dateTimePicker结束时间.Size = new System.Drawing.Size(176, 23);
            this.dateTimePicker结束时间.TabIndex = 20;
            this.dateTimePicker结束时间.Value = new System.DateTime(2022, 12, 1, 0, 0, 0, 0);
            this.dateTimePicker结束时间.ValueChanged += new System.EventHandler(this.DateTimePicker结束时间_ValueChanged);
            // 
            // dateTimePicker开始时间
            // 
            this.dateTimePicker开始时间.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker开始时间.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker开始时间.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker开始时间.Location = new System.Drawing.Point(99, 29);
            this.dateTimePicker开始时间.MaxDate = new System.DateTime(2070, 12, 31, 0, 0, 0, 0);
            this.dateTimePicker开始时间.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker开始时间.Name = "dateTimePicker开始时间";
            this.dateTimePicker开始时间.Size = new System.Drawing.Size(170, 23);
            this.dateTimePicker开始时间.TabIndex = 17;
            this.dateTimePicker开始时间.Value = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker开始时间.ValueChanged += new System.EventHandler(this.DateTimePicker开始时间_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(304, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 21);
            this.label5.TabIndex = 19;
            this.label5.Text = "结束时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(19, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 18;
            this.label2.Text = "开始时间：";
            // 
            // comboBox表
            // 
            this.comboBox表.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox表.FormattingEnabled = true;
            this.comboBox表.Location = new System.Drawing.Point(121, 71);
            this.comboBox表.Name = "comboBox表";
            this.comboBox表.Size = new System.Drawing.Size(149, 22);
            this.comboBox表.TabIndex = 14;
            this.comboBox表.SelectedIndexChanged += new System.EventHandler(this.ComboBox表_SelectedIndexChanged);
            this.comboBox表.TextChanged += new System.EventHandler(this.ComboBox表_TextChanged);
            // 
            // comboBox字段
            // 
            this.comboBox字段.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox字段.FormattingEnabled = true;
            this.comboBox字段.Location = new System.Drawing.Point(384, 71);
            this.comboBox字段.Name = "comboBox字段";
            this.comboBox字段.Size = new System.Drawing.Size(176, 22);
            this.comboBox字段.TabIndex = 15;
            this.comboBox字段.SelectedIndexChanged += new System.EventHandler(this.ComboBox字段_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox数据库路径);
            this.groupBox2.Controls.Add(this.button浏览文件);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.comboBox字段);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboBox表);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(587, 111);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "分析对象";
            // 
            // textBox数据库路径
            // 
            this.textBox数据库路径.AllowDrop = true;
            this.textBox数据库路径.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox数据库路径.Location = new System.Drawing.Point(121, 27);
            this.textBox数据库路径.Name = "textBox数据库路径";
            this.textBox数据库路径.ReadOnly = true;
            this.textBox数据库路径.Size = new System.Drawing.Size(385, 23);
            this.textBox数据库路径.TabIndex = 18;
            this.textBox数据库路径.TextChanged += new System.EventHandler(this.TextBox数据库路径_TextChanged);
            this.textBox数据库路径.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox_DragDrop);
            this.textBox数据库路径.DragEnter += new System.Windows.Forms.DragEventHandler(this.公用_DragEnter);
            // 
            // button浏览文件
            // 
            this.button浏览文件.Font = new System.Drawing.Font("等线", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button浏览文件.Location = new System.Drawing.Point(512, 29);
            this.button浏览文件.Name = "button浏览文件";
            this.button浏览文件.Size = new System.Drawing.Size(48, 23);
            this.button浏览文件.TabIndex = 17;
            this.button浏览文件.Text = "浏览...";
            this.button浏览文件.UseVisualStyleBackColor = true;
            this.button浏览文件.Click += new System.EventHandler(this.Button浏览文件_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(20, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 21);
            this.label6.TabIndex = 16;
            this.label6.Text = "数据库路径：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dateTimePicker开始时间);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.dateTimePicker结束时间);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(13, 130);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(586, 75);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "时间段";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label提示语
            // 
            this.label提示语.AutoSize = true;
            this.label提示语.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label提示语.ForeColor = System.Drawing.Color.Red;
            this.label提示语.Location = new System.Drawing.Point(451, 305);
            this.label提示语.Name = "label提示语";
            this.label提示语.Size = new System.Drawing.Size(0, 14);
            this.label提示语.TabIndex = 22;
            // 
            // FormDATAN
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 350);
            this.Controls.Add(this.label提示语);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button输出分析文档);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDATAN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据分析 v0.1.1";
            this.Load += new System.EventHandler(this.FormDATAN_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.公用_DragEnter);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown最大值)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown最小值)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button输出分析文档;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButton连续;
        private System.Windows.Forms.RadioButton radioButton独立;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox表;
        private System.Windows.Forms.ComboBox comboBox字段;
        private System.Windows.Forms.NumericUpDown numericUpDown最大值;
        private System.Windows.Forms.NumericUpDown numericUpDown最小值;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dateTimePicker结束时间;
        private System.Windows.Forms.DateTimePicker dateTimePicker开始时间;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button浏览文件;
        private System.Windows.Forms.TextBox textBox数据库路径;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label提示语;
    }
}