using System;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace apachegui
{
    public partial class Form1 : Form
    {
        public static string[] InstallPlatforms32;
        public static string[] InstallPlatforms64;
        public static string[] IB;
        public static string IbPath = null;
        public static int Type = 0; // 0 - File, 1 - srvr, 2 - ws
        public static bool x32 = false;
        public static bool x64 = false;
        public static bool ApacheInstall = false;
        public static bool Platform;
        public static bool IBChen;
        public static bool ConfPath;
        public static bool Port = true;
        public static bool Alias = false;
        public static bool ServiceName = true;
        public static bool Debug = false;
        ToolTip toolTip = new ToolTip();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GetPath.GetApachePath();
            GetPath.GetPathInstall1c();
            GetPath.GetIbName();
            foreach (string i in IB)
            {
                var bytes = Encoding.GetEncoding("windows-1251").GetBytes(i);
                var res = Encoding.GetEncoding("UTF-8").GetString(bytes);
                comboBox3.Items.Add(res);
            }
            if (x32 == true)
            {
                label2.ForeColor = Color.Green;
                pictureBox1.Image = Properties.Resources.Ok;
                foreach (var i in InstallPlatforms32)
                {
                    comboBox1.Items.Add(i.Replace(GetPath.onecv832 + @"\", ""));
                }
            }
            else
            {
                label2.ForeColor = Color.Red;
                pictureBox1.Image = Properties.Resources.Error;
                comboBox1.Items.Add("N/a");
                checkBox1.Enabled = false;
            }
            if (x64 == true)
            {
                label3.ForeColor = Color.Green;
                pictureBox2.Image = Properties.Resources.Ok;
                foreach (var i in InstallPlatforms64)
                {
                    comboBox2.Items.Add(i.Replace(GetPath.onecv864 + @"\", ""));
                }
            }
            else
            {
                label3.ForeColor = Color.Red;
                pictureBox2.Image = Properties.Resources.Error;
                comboBox2.Items.Add("N/a");
                checkBox2.Enabled = false;
            }
            if (ApacheInstall == true)
            {
                textBox1.Text = GetPath.ApachePath;
                label5.ForeColor = Color.Green;
                pictureBox3.Image = Properties.Resources.Ok;
            }
            else
            {
                label5.ForeColor = Color.Red;
                pictureBox3.Image = Properties.Resources.Error;
            }
            if (GetPath.ApacheConfPath() == true)
            {
                textBox2.Text = GetPath.ApacheConfP;
                pictureBox5.Image = Properties.Resources.Ok;
                ConfPath = true;
            }
            else
            {
                textBox2.Text = GetPath.ApacheConfP;
                pictureBox5.Image = Properties.Resources.Error;
                toolTip.SetToolTip(pictureBox5, "Директория не существует");
                ConfPath = false;
            }
            if (x32 == false && x64 == false)
            {
                Platform = false;
            }
            else
            {
                Platform = true;
            }
            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
            comboBox3.SelectedIndex = 0;
            checkBox1.Text = "Использовать";
            checkBox2.Text = "Использовать";
            pictureBox1.Size = new Size(20, 20);
            pictureBox2.Size = new Size(20, 20);
            pictureBox3.Size = new Size(20, 20);
            pictureBox4.Size = new Size(20, 20);
            pictureBox5.Size = new Size(20, 20);
            pictureBox6.Size = new Size(20, 20);
            pictureBox7.Size = new Size(20, 20);
            pictureBox8.Size = new Size(20, 20);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox7.Image = Properties.Resources.Error;
            pictureBox8.Image = Properties.Resources.Ok;
            toolTip.SetToolTip(pictureBox7, "Не может быть пустым!");
            button3.Visible = false;
            textBox3.MaxLength = 5;
            textBox4.MaxLength = 30;
            textBox3.Text = "80";
            textBox5.Text = $"Apache_{textBox4.Text}_{textBox3.Text}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowNewFolderButton = false;
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(FBD.SelectedPath + @"\bin\httpd.exe"))
                {
                    GetPath.ApachePath = FBD.SelectedPath;
                    textBox1.Text = GetPath.ApachePath;
                    pictureBox3.Image = Properties.Resources.Ok;
                }
                else
                {
                    MessageBox.Show("В указанной директории не установлен Apache!");
                }
            }
            FBD.Dispose();
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool check = false;
            GetPath.CheckDBPath(comboBox3.SelectedItem.ToString(), ref check, ref Type, ref IbPath);
            switch (Type)
            {
                case 0:
                    if (check == true)
                    {
                        label7.Text = IbPath;
                        pictureBox4.Image = Properties.Resources.Ok;
                        IBChen = true;
                        break;
                    }
                    else
                    {
                        label7.Text = IbPath;
                        pictureBox4.Image = Properties.Resources.Error;
                        //toolTip.RemoveAll();
                        toolTip.SetToolTip(pictureBox4, "Файл базы данных не обнаружен");
                        IBChen = false;
                        break;
                    }
                case 1:
                    label7.Text = IbPath;
                    pictureBox4.Image = Properties.Resources.What;
                    //toolTip.RemoveAll();
                    toolTip.SetToolTip(pictureBox4, "Серверная база, нужно проверить её существование!");
                    IBChen = true;
                    break;
                case 2:
                    label7.Text = IbPath;
                    pictureBox4.Image = Properties.Resources.Error;
                    //toolTip.RemoveAll();
                    toolTip.SetToolTip(pictureBox4, "Это публикация!!!");
                    IBChen = false;
                    break;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox2.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox1.Checked = false;
            }
            else
            {
                checkBox2.Checked = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowNewFolderButton = true;
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                GetPath.ApacheConfP = FBD.SelectedPath;
                textBox2.Text = GetPath.ApacheConfP;
            }
            FBD.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(GetPath.ApachePath))
            {
                if (!Directory.Exists(GetPath.ApacheConfP))
                {
                    try
                    {
                        Directory.CreateDirectory(GetPath.ApacheConfP);
                        pictureBox5.Image = Properties.Resources.Ok;
                        button3.Visible = false;
                        textBox2.Text = GetPath.ApacheConfP;
                        ConfPath = true;
                    }
                    catch
                    {
                        MessageBox.Show("Не удалось создать папку!");
                    }
                }
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            bool Available = true;
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
                {
                    if (tcpi.LocalEndPoint.Port == Convert.ToInt32(textBox3.Text))
                    {
                        Available = false;
                        break;
                    }
                }
            }
            else
            {
                pictureBox6.Image = Properties.Resources.Error;
                //toolTip.RemoveAll();
                toolTip.SetToolTip(pictureBox6, "Не указан порт");
                Port = false;
            }
            if (Available == true)
            {
                pictureBox6.Image = Properties.Resources.Ok;
                textBox5.Text = $"Apache_{textBox4.Text}_{textBox3.Text}";
                pictureBox8.Image = Properties.Resources.Ok;
                Port = true;
                ServiceName = true;
            }
            else
            {
                toolTip.SetToolTip(pictureBox6, "Порт занят");
                pictureBox6.Image = Properties.Resources.Error;
                textBox5.Text = $"Apache_{textBox4.Text}_{textBox3.Text}";
                pictureBox8.Image = Properties.Resources.Error;
                Port = false;
                ServiceName = false;
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                pictureBox7.Image = Properties.Resources.Error;
                toolTip.SetToolTip(pictureBox7, "Не может быть пустым!");
                Alias = false;
            }
            else
            {
                textBox5.Text = $"Apache_{textBox4.Text}_{textBox3.Text}";
                pictureBox7.Image = Properties.Resources.Ok;
                pictureBox8.Image = Properties.Resources.Ok;
                Alias = true;
                ServiceName = true;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                pictureBox8.Image = Properties.Resources.Error;
                toolTip.SetToolTip(pictureBox8, "Не может быть пустым!");
                ServiceName = false;
            }
            else
            {
                pictureBox8.Image = Properties.Resources.Ok;
                ServiceName = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*
            if (Platform == true && ApacheInstall == true && IBChen == true && ConfPath == true && Alias == true && ServiceName == true)
            {
                bool vrd = false;
                bool cfg = false;
                CreatePublication.CheckDir(ref vrd, ref cfg);
                if (vrd == true && cfg == true)
                {

                }
                else
                {
                    MessageBox.Show("Не удалось создать подкаталоги /1C/VRD /1C/CFG");
                }
            }
            else
            {
                MessageBox.Show("Выполнены не все условия!");
            }
            */
            CreatePublication.CreateVRD(textBox4.Text);
        }
        internal static void Message(string mes)
        {
            MessageBox.Show(mes);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox3.Text = "Включена";
                Debug = true;
            }
            else
            {
                checkBox3.Text = "Выключена";
                Debug = false;
            }
        }
    }
}