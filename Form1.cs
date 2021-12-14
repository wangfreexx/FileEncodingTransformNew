using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileEncodingTransform
{


    public partial class Form1 : Form
    {
        private Button btnRemove;
        private Button btnSelectFiles;
        private Button button_Clipboard;
        private Button buttonBrowser;
        private Button buttonRun;
        private CheckBox chkIsBackup;
        private CheckBox chkUnknownEncoding;
        private ComboBox cmbSourceEncode;
        private ComboBox cmbTargetEncode;
        private IContainer components = null;
        private ArrayList ExtNameList;
        private FolderBrowserDialog folderBrowserDialog1;
        private string initPth = @"E:\Projects";
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox_fileFilter;
        private TextBox txtResult;

        public Form1()
        {
            InitializeComponent();
            this.cmbSourceEncode.Enabled = !this.chkUnknownEncoding.Checked;
            this.cmbTargetEncode.SelectedIndex = 0;
            //progressBar1.Value = 0;

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //if (this.listSelectedFiles.SelectedIndex > -1)
            //{
            //    this.listSelectedFiles.Items.RemoveAt(this.listSelectedFiles.SelectedIndex);
            //}
            foreach (ListViewItem lvi in listView1.SelectedItems)  //选中项遍历  
            {
                listView1.Items.RemoveAt(lvi.Index); // 按索引移除  
            }
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            int cfflag = 0;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = true;
                dialog.InitialDirectory = this.initPth;
                if (!this.textBox_fileFilter.Text.Trim().Equals(""))
                {
                    dialog.Filter = this.textBox_fileFilter.Text.Trim();
                }
                if (DialogResult.OK == dialog.ShowDialog())
                {
                    string[] fileNames = dialog.FileNames;
                    this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度  
                    this.Cursor = Cursors.WaitCursor;
                    foreach (string str in fileNames)
                    {
                        for (int nColOrder = 0; nColOrder < listView1.Items.Count; nColOrder++)//每行中的每项
                        {
                            if (str == listView1.Items[nColOrder].Text)
                            {
                                cfflag = 1;
                                break;
                            }

                        }
                        if (cfflag == 0 && File.Exists(str))
                        {

                            ListViewItem lvi = new ListViewItem();
                            lvi.Text = str;
                            FileInfo testfile = new FileInfo(str);
                            IdentifyEncoding encoding2 = new IdentifyEncoding();
                            lvi.SubItems.Add(encoding2.GetEncodingName(testfile));
                            lvi.SubItems.Add(cmbTargetEncode.SelectedItem.ToString());
                            this.listView1.Items.Add(lvi);
                        }

                    }
                    this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void button_Clipboard_Click(object sender, EventArgs e)
        {
            string[] strArray = Clipboard.GetText().Split(new char[] { '\n' });
            string item = "";
            int cfflag = 0;
            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度  
            this.Cursor = Cursors.WaitCursor;
            for (int i = 0; i < strArray.Length; i++)
            {
                item = strArray[i];
                if (item.IndexOf("\r") > -1)
                {
                    item = item.Replace("\r", "");
                }
                if (!item.Trim().Equals(""))
                {
                    for (int nColOrder = 0; nColOrder < listView1.Items.Count; nColOrder++)//每行中的每项
                    {
                        if (item == listView1.Items[nColOrder].Text)
                        {
                            cfflag = 1;
                            break;
                        }
                    }
                    if (cfflag == 0 && File.Exists(item))
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = item;
                        FileInfo testfile = new FileInfo(item);
                        IdentifyEncoding encoding2 = new IdentifyEncoding();
                        lvi.SubItems.Add(encoding2.GetEncodingName(testfile));
                        lvi.SubItems.Add(cmbTargetEncode.SelectedItem.ToString());
                        this.listView1.Items.Add(lvi);
                    }

                }
            }
            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。 
            this.Cursor = Cursors.Default;
        }

        private void buttonBrowser_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.SelectedPath = this.initPth;
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.initPth = this.folderBrowserDialog1.SelectedPath;
                if (!this.textBox_fileFilter.Text.Trim().Equals(""))
                {
                    string[] strArray = this.textBox_fileFilter.Text.Trim().Split(new char[] { '|' });
                    if ((strArray.Length % 2) > 0)
                    {
                        MessageBox.Show("文件过滤字符串设置有误!");
                        return;
                    }
                    this.ExtNameList = new ArrayList();
                    for (int i = 0; i < (strArray.Length / 2); i++)
                    {
                        this.ExtNameList.Add(strArray[(i * 2) + 1].Substring(strArray[(i * 2) + 1].LastIndexOf("."), strArray[(i * 2) + 1].Length - strArray[(i * 2) + 1].LastIndexOf(".")).ToLower());
                    }
                }
                this.Cursor = Cursors.WaitCursor;
                this.FindAllFiles(this.folderBrowserDialog1.SelectedPath);
                this.Cursor = Cursors.Default;
            }
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            this.txtResult.Text = "文件总数：" + this.listView1.Items.Count.ToString() + "\r\n正在执行......";
            progressBar1.Maximum = listView1.Items.Count;
            progressBar1.Value = 0;
            int num = 0;

            this.Cursor = Cursors.WaitCursor;
            while (num < this.listView1.Items.Count)
            {
                num++;
                //编码相同
                if (listView1.Items[num - 1].SubItems[1].Text == listView1.Items[num - 1].SubItems[2].Text)
                {
                    this.txtResult.Text = this.txtResult.Text + "\r\n不需要转换：" + this.listView1.Items[num - 1].Text.ToString();
                    continue;
                }
                //文件不存在
                if (!File.Exists(this.listView1.Items[num - 1].Text))
                {
                    this.txtResult.Text = this.txtResult.Text + "\r\n文件不存在：" + this.listView1.Items[num - 1].Text.ToString();
                    continue;
                }
                //处理文件，包含出错处理
                try
                {
                    ConvertFileEncode(this.listView1.Items[num - 1].Text.ToString());
                }
                catch (Exception exception)
                {
                    txtResult.Text = txtResult.Text + "\r\n执行文件：" + this.listView1.Items[num - 1].Text.ToString() + "转换时出现错误：" + exception.Message;
                    continue;
                }

                this.txtResult.Text = this.txtResult.Text + "\r\n转换完成：" + this.listView1.Items[num - 1].Text.ToString();
                //更新进度条
                if (progressBar1.Value <= progressBar1.Maximum)
                {
                    progressBar1.Value++;
                }

            }


            this.Cursor = Cursors.Default;
            this.listView1.BeginUpdate();   
            for (int nColOrder = 0; nColOrder < listView1.Items.Count; nColOrder++)//每行中的每项
            {
                //更新现有编码
                string str2 = listView1.Items[nColOrder].Text;
                //Console.WriteLine(str2);
                if (File.Exists(str2))
                {
                    FileInfo testfile = new FileInfo(str2.ToString());
                    IdentifyEncoding encoding2 = new IdentifyEncoding();
                    listView1.Items[nColOrder].SubItems[1].Text = encoding2.GetEncodingName(testfile);
                }
            }
            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。 

        }

        private void chkUnknownEncoding_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbSourceEncode.Enabled = !this.chkUnknownEncoding.Checked;
        }

        private void ConvertFileEncode(string PathFile)
        {
            if (!PathFile.Trim().Equals(""))
            {
                if (!File.Exists(PathFile))
                {
                    this.txtResult.Text = this.txtResult.Text + string.Format("\r\n{0}文件不存在。 ", PathFile);
                    return;
                }
                Encoding selectEncoding;
                if (this.chkUnknownEncoding.Checked)
                {
                    IdentifyEncoding encoding2 = new IdentifyEncoding();
                    FileInfo testfile = new FileInfo(PathFile);
                    string name = string.Empty;
                    name = encoding2.GetEncodingName(testfile);
                    testfile = null;
                    if (name.ToLower() == "other")
                    {
                        this.txtResult.Text = this.txtResult.Text + string.Format("\r\n{0}文件格式不正确或已损坏。 ", PathFile);
                        return;
                    }
                    if (name != "UTF-8(BOM)")
                    {
                        selectEncoding = Encoding.GetEncoding(name);
                    }
                    else
                    {
                        selectEncoding = Encoding.UTF8;
                    }

                }
                else
                {
                    selectEncoding = this.GetSelectEncoding(this.cmbSourceEncode.SelectedIndex);
                }
                string contents = File.ReadAllText(PathFile, selectEncoding);
                if (this.chkIsBackup.Checked)
                {
                    File.WriteAllText(PathFile + ".bak", contents, selectEncoding);
                }
                File.WriteAllText(PathFile, contents, this.GetSelectEncoding(this.cmbTargetEncode.SelectedIndex));
            }
        }


        private void FindAllFiles(string path)
        {
            string[] files = Directory.GetFiles(path);
            string str = "";
            int startIndex = 0;
            bool flag = false;
            int cfflag = 0;
            foreach (string str2 in this.ExtNameList)
            {
                if (str2.Equals(".*"))
                {
                    flag = true;
                }
            }
            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度  
            foreach (string str2 in files)
            {
                startIndex = str2.LastIndexOf(".");
                if (startIndex > -1)
                {
                    str = str2.Substring(startIndex, str2.Length - startIndex);
                }
                else
                {
                    str = "";
                }
                if (((this.ExtNameList == null) || (this.ExtNameList.IndexOf(str.ToLower()) > -1)) || flag)
                {
                    //this.listSelectedFiles.Items.Add(str2);
                    for (int nColOrder = 0; nColOrder < listView1.Items.Count; nColOrder++)//每行中的每项
                    {
                        if (str2 == listView1.Items[nColOrder].Text)
                        {
                            cfflag = 1;
                            break;
                        }

                    }
                    if (cfflag == 0 && File.Exists(str2))
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = str2;
                        FileInfo testfile = new FileInfo(str2);
                        IdentifyEncoding encoding2 = new IdentifyEncoding();
                        lvi.SubItems.Add(encoding2.GetEncodingName(testfile));
                        lvi.SubItems.Add(cmbTargetEncode.SelectedItem.ToString());
                        this.listView1.Items.Add(lvi);
                    }


                }
            }
            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。 
            string[] directories = Directory.GetDirectories(path);
            foreach (string str2 in directories)
            {
                this.FindAllFiles(str2);
            }
        }

        private void FormFileEncode_Load(object sender, EventArgs e)
        {
            this.cmbSourceEncode.SelectedIndex = 4;
            this.cmbTargetEncode.SelectedIndex = 0;
        }

        private Encoding GetSelectEncoding(int i)
        {
            switch (i)
            {
                case 0:
                    return new UTF8Encoding(false);
                case 1:
                    return Encoding.UTF8;
                case 2:
                    return Encoding.UTF7;

                case 3:
                    return Encoding.Unicode;

                case 4:
                    return Encoding.ASCII;

                case 5:
                    return Encoding.GetEncoding(0x3a8);

                case 6:
                    return Encoding.GetEncoding("BIG5");
            }
            return new UTF8Encoding(false);
        }

        private void buttonclear_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();  //只移除所有的项。
        }

        private void cmbTargetEncode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度  


            for (int nColOrder = 0; nColOrder < listView1.Items.Count; nColOrder++)//每行中的每项
            {

                string str2 = listView1.Items[nColOrder].Text;
                //Console.WriteLine(str2);
                //FileInfo testfile = new FileInfo(str2.ToString());
                //IdentifyEncoding encoding2 = new IdentifyEncoding();
                listView1.Items[nColOrder].SubItems[2].Text = cmbTargetEncode.SelectedItem.ToString();

            }

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。 
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {

            int cfflag = 0;
            string[] ss = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            this.listView1.BeginUpdate();
            this.Cursor = Cursors.WaitCursor;
            foreach (string s in ss)
            {
                for (int nColOrder = 0; nColOrder < listView1.Items.Count; nColOrder++)//每行中的每项
                {
                    if (s == listView1.Items[nColOrder].Text)
                    {
                        cfflag = 1;
                        break;
                    }

                }
                if (cfflag == 0 && File.Exists(s))
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = s;
                    FileInfo testfile = new FileInfo(s);
                    IdentifyEncoding encoding2 = new IdentifyEncoding();
                    lvi.SubItems.Add(encoding2.GetEncodingName(testfile));
                    lvi.SubItems.Add(cmbTargetEncode.SelectedItem.ToString());
                    this.listView1.Items.Add(lvi);
                }
            }
            this.listView1.EndUpdate();
            this.Cursor = Cursors.Default;

        }
        //拖拽文件
        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                                                              //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }
    }
}
