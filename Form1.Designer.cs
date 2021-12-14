
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FileEncodingTransform
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        //private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonBrowser = new System.Windows.Forms.Button();
            this.cmbSourceEncode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTargetEncode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnRemove = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnSelectFiles = new System.Windows.Forms.Button();
            this.textBox_fileFilter = new System.Windows.Forms.TextBox();
            this.button_Clipboard = new System.Windows.Forms.Button();
            this.chkIsBackup = new System.Windows.Forms.CheckBox();
            this.chkUnknownEncoding = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonclear = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // buttonRun
            // 
            this.buttonRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRun.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonRun.Location = new System.Drawing.Point(534, 318);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(44, 23);
            this.buttonRun.TabIndex = 10;
            this.buttonRun.Text = "转换";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonBrowser
            // 
            this.buttonBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowser.Location = new System.Drawing.Point(479, 51);
            this.buttonBrowser.Name = "buttonBrowser";
            this.buttonBrowser.Size = new System.Drawing.Size(96, 23);
            this.buttonBrowser.TabIndex = 9;
            this.buttonBrowser.Text = "按目录选文件";
            this.buttonBrowser.UseVisualStyleBackColor = true;
            this.buttonBrowser.Click += new System.EventHandler(this.buttonBrowser_Click);
            // 
            // cmbSourceEncode
            // 
            this.cmbSourceEncode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSourceEncode.AutoCompleteCustomSource.AddRange(new string[] {
            "UTF-8",
            "UTF-7",
            "Unicode",
            "ASCII",
            "GB2312(简体中文)",
            "BIG5 (繁体中文)"});
            this.cmbSourceEncode.FormattingEnabled = true;
            this.cmbSourceEncode.Items.AddRange(new object[] {
            "UTF-8",
            "UTF-7",
            "Unicode",
            "ASCII",
            "GB2312(简体中文)",
            "BIG5 (繁体中文)"});
            this.cmbSourceEncode.Location = new System.Drawing.Point(480, 242);
            this.cmbSourceEncode.Name = "cmbSourceEncode";
            this.cmbSourceEncode.Size = new System.Drawing.Size(96, 20);
            this.cmbSourceEncode.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(479, 225);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "原文件编码";
            // 
            // cmbTargetEncode
            // 
            this.cmbTargetEncode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTargetEncode.AutoCompleteCustomSource.AddRange(new string[] {
            "UTF-8",
            "UTF-7",
            "Unicode",
            "ASCII",
            "GB2312(简体中文)",
            "BIG5 (繁体中文)"});
            this.cmbTargetEncode.FormattingEnabled = true;
            this.cmbTargetEncode.Items.AddRange(new object[] {
            "UTF-8",
            "UTF-8(BOM)",
            "UTF-7",
            "Unicode",
            "ASCII",
            "GB2312(简体中文)",
            "BIG5 (繁体中文)"});
            this.cmbTargetEncode.Location = new System.Drawing.Point(480, 289);
            this.cmbTargetEncode.Name = "cmbTargetEncode";
            this.cmbTargetEncode.Size = new System.Drawing.Size(97, 20);
            this.cmbTargetEncode.TabIndex = 11;
            this.cmbTargetEncode.SelectedIndexChanged += new System.EventHandler(this.cmbTargetEncode_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(479, 272);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "转换后编码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "文件过滤字符串";
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(480, 141);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(96, 23);
            this.btnRemove.TabIndex = 17;
            this.btnRemove.Text = "从列表中移除";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(15, 399);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(561, 88);
            this.txtResult.TabIndex = 16;
            this.txtResult.WordWrap = false;
            // 
            // btnSelectFiles
            // 
            this.btnSelectFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFiles.Location = new System.Drawing.Point(480, 80);
            this.btnSelectFiles.Name = "btnSelectFiles";
            this.btnSelectFiles.Size = new System.Drawing.Size(96, 23);
            this.btnSelectFiles.TabIndex = 15;
            this.btnSelectFiles.Text = "多选文件";
            this.btnSelectFiles.UseVisualStyleBackColor = true;
            this.btnSelectFiles.Click += new System.EventHandler(this.btnSelectFiles_Click);
            // 
            // textBox_fileFilter
            // 
            this.textBox_fileFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_fileFilter.Location = new System.Drawing.Point(107, 7);
            this.textBox_fileFilter.Name = "textBox_fileFilter";
            this.textBox_fileFilter.Size = new System.Drawing.Size(468, 21);
            this.textBox_fileFilter.TabIndex = 18;
            this.textBox_fileFilter.Text = "c文件|*.c|h文件|*.h|txt文件|*.txt";
            // 
            // button_Clipboard
            // 
            this.button_Clipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Clipboard.Location = new System.Drawing.Point(479, 109);
            this.button_Clipboard.Name = "button_Clipboard";
            this.button_Clipboard.Size = new System.Drawing.Size(96, 23);
            this.button_Clipboard.TabIndex = 15;
            this.button_Clipboard.Text = "剪贴板中复制";
            this.button_Clipboard.UseVisualStyleBackColor = true;
            this.button_Clipboard.Click += new System.EventHandler(this.button_Clipboard_Click);
            // 
            // chkIsBackup
            // 
            this.chkIsBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIsBackup.AutoSize = true;
            this.chkIsBackup.Location = new System.Drawing.Point(481, 322);
            this.chkIsBackup.Name = "chkIsBackup";
            this.chkIsBackup.Size = new System.Drawing.Size(48, 16);
            this.chkIsBackup.TabIndex = 20;
            this.chkIsBackup.Text = "备份";
            this.chkIsBackup.UseVisualStyleBackColor = true;
            // 
            // chkUnknownEncoding
            // 
            this.chkUnknownEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUnknownEncoding.AutoSize = true;
            this.chkUnknownEncoding.Checked = true;
            this.chkUnknownEncoding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUnknownEncoding.Location = new System.Drawing.Point(480, 203);
            this.chkUnknownEncoding.Name = "chkUnknownEncoding";
            this.chkUnknownEncoding.Size = new System.Drawing.Size(108, 16);
            this.chkUnknownEncoding.TabIndex = 19;
            this.chkUnknownEncoding.Text = "自动识别原编码";
            this.chkUnknownEncoding.UseVisualStyleBackColor = true;
            this.chkUnknownEncoding.CheckedChanged += new System.EventHandler(this.chkUnknownEncoding_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(105, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(383, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "（坚线分隔，奇数为类型描述，偶数为扩展名。空值或*.*为全部文件）";
            // 
            // listView1
            // 
            this.listView1.AllowDrop = true;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(15, 46);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(457, 337);
            this.listView1.TabIndex = 22;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "文件名";
            this.columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "源编码";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "新编码";
            this.columnHeader3.Width = 80;
            // 
            // buttonclear
            // 
            this.buttonclear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonclear.Location = new System.Drawing.Point(480, 170);
            this.buttonclear.Name = "buttonclear";
            this.buttonclear.Size = new System.Drawing.Size(96, 23);
            this.buttonclear.TabIndex = 23;
            this.buttonclear.Text = "清空列表";
            this.buttonclear.UseVisualStyleBackColor = true;
            this.buttonclear.Click += new System.EventHandler(this.buttonclear_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(481, 372);
            this.progressBar1.MarqueeAnimationSpeed = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(97, 11);
            this.progressBar1.TabIndex = 24;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 495);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonclear);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkIsBackup);
            this.Controls.Add(this.chkUnknownEncoding);
            this.Controls.Add(this.textBox_fileFilter);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.button_Clipboard);
            this.Controls.Add(this.btnSelectFiles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbTargetEncode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSourceEncode);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.buttonBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文本编码助手";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private Button buttonclear;
        private ProgressBar progressBar1;
    }
}

