using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.IO;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Windows.Documents;
using System.Reflection;

namespace laguNETv0
{
    public partial class Form1 : Form
    {
        private ContextMenu m_menu;
        MenuStrip strip = new MenuStrip();
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            radioButtonDHCP.CheckedChanged += validarDatos;
            textBoxNiIp.TextChanged += validarDatos;
            textBoxNiMask.TextChanged += validarDatos;
            textBoxTestPing.TextChanged += validarDatos;
            checkBoxConectar.CheckedChanged += validarDatos;
            checkBoxTestPing.CheckedChanged += validarDatos;
            validarDatos(sender, e);
            leer_archivo_traduccion();

            buttonInfo.BackColor = Form1.DefaultBackColor;
            buttonInfo.FlatAppearance.BorderSize = 0;
            buttonInfo.Image = SystemIcons.Question.ToBitmap();
            //crearMenu();
            listaAdapatadores();            // LISTA DE ADAPTADORES DE RED
            showProfiles();           // rellena el combobox con las SSID
            //comboBoxAdaptador_SelectedIndexChanged(sender,e);
            comboBoxSSID.Enabled = checkBoxConectar.Checked;
            treeViewScripts.Focus();
           
            string dir = Application.StartupPath + "\\scripts";
            ListDirectory(treeViewScripts,dir);

        }

        private void timerPing_Tick(object sender, EventArgs e)
        {
            Ping HacerPing = new Ping();
            PingReply RespuestaPing;
            string sDireccion = richTextBoxScript.Lines[2].Replace(":: testPing=", "");

            if (ValidateIPv4(sDireccion)) {
                treeViewScripts.SelectedNode.BackColor = Color.Coral;
                try
                {
                    
                    RespuestaPing = HacerPing.Send(sDireccion, 500);
                    if (RespuestaPing.Status == IPStatus.Success)
                    {
                        treeViewScripts.SelectedNode.BackColor = Color.LightGreen;
                        timerPing.Stop();
                    }
                    else
                    {
                        treeViewScripts.SelectedNode.BackColor = Color.Coral;
                    }
                }
                catch (Exception ex)
                {
                    treeViewScripts.SelectedNode.BackColor = Color.Coral;
                }
            }
            else
            {
                timerPing.Stop();
                treeViewScripts.SelectedNode.BackColor = Color.SkyBlue;
            }
        }
        private static void EventoElapsed(object sender, EventArgs e) 
        {


        }
        private void menuAdaptadores(object sender, EventArgs e)
        {
            cmdCommand("/c ncpa.cpl");
        }

        private void cmdCommand(string strCmd)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
            psi.UseShellExecute = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.Verb = "runas";
            psi.Arguments = strCmd;
            var p = Process.Start(psi);
            p.WaitForExit();
 

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    notifyIcon1.Visible = true;
            //    this.Hide();
            //    e.Cancel = true;
            //}
            //if (this.Visible) {
            //    this.Hide();
            //    e.Cancel = true;
            //} else {
            //    Close();
            //}
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {


        }



        protected void Exit_Click(Object sender, System.EventArgs e)
        {
            Close();
            
            
        }
        protected void Hide_Click(Object sender, System.EventArgs e)
        {
            Hide();
        }
        protected void Show_Click(Object sender, System.EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Show();
           
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //if (FormWindowState.Minimized == WindowState)
            //    Hide();

            //if (FormWindowState.Minimized == this.WindowState)
            //{
            //    Hide();
            //}
            //else if (FormWindowState.Normal == this.WindowState)
            //{

            //    Show();
            //}
        }

        private void buttonCrear_Click(object sender, EventArgs e)
        {
            string s, b, l;
            richTextBoxScript.Text = "";

            if (radioButtonDHCP.Checked)
            {
                //s = "netsh interface ipv4 set address name=\"" + textBoxNiName.Text + "\" source=dhcp";
                s = "netsh interface ipv4 set address name=\"" + comboBoxAdaptador.Text + "\" source=dhcp";

            }
            else {
                //s = "netsh interface ipv4 set address name=\"" + textBoxNiName.Text + "\" static " + textBoxNiIp.Text + " " + textBoxNiMask.Text;
                s = "netsh interface ipv4 set address name=\"" + comboBoxAdaptador.Text + "\" static " + textBoxNiIp.Text + " " + textBoxNiMask.Text;

            }
            richTextBoxScript.AppendText(s + "\n");


            if (checkBoxConectar.Checked)
            {
                b = "netsh wlan connect name =\"" + comboBoxSSID.Text + "\" ssid =\"" + comboBoxSSID.Text + "\"";
            }
            else 
            {
                b = ":: No conectar ";
            }

            richTextBoxScript.AppendText(b + "\n");
            if (checkBoxTestPing.Checked) 
            { 
                l = ":: testPing=" + textBoxTestPing.Text;
            } 
            else 
            {
                l = ":: No test Ping";
            }
            richTextBoxScript.AppendText(l);


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        private void radioButtonDHCP_CheckedChanged(object sender, EventArgs e)
        {
            textBoxNiIp.Enabled = !radioButtonDHCP.Checked;
            textBoxNiMask.Enabled = !radioButtonDHCP.Checked;
            textBoxNiIp.BackColor = Color.White;
            textBoxNiMask.BackColor = Color.White;

        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            string dir = "scripts"; // If directory does not exist, create it.
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog() {
                Filter = "Batch script|*.bat|Texto plano|*.txt",
                Title = "Guardar script",
                InitialDirectory = Application.StartupPath + "\\scripts",
            };

            //saveFileDialog1.Filter = "Batch script|*.bat|Texto plano|*.txt";
            //saveFileDialog1.Title = "Guardar script";

            //saveFileDialog1.InitialDirectory = Application.StartupPath + "\\scripts";
            saveFileDialog1.ShowDialog();


            if (saveFileDialog1.FileName.ToString() != "") 
            { 
            StreamWriter sw = new StreamWriter(saveFileDialog1.FileName.ToString());         
            sw.WriteLine(richTextBoxScript.Text); //Write a line of text
            sw.Close(); //Close the file
            }
            ListDirectory(treeViewScripts, dir);

        }

        private void conexionesDeRedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmdCommand("/c ncpa.cpl");
        }

        private void menuClick(object sender, EventArgs e)
        {
            richTextBoxScript.Text = "";
            String line;

            string path = Application.StartupPath + "\\"   ;
            
            StreamReader sr = new StreamReader(path);
            richTextBoxScript.Text = "";

            line = sr.ReadLine();
            richTextBoxScript.AppendText(line + "\n"); //cmdCommand(line);

            while (line != null)    //Continue to read until you reach end of file
            {
                line = sr.ReadLine();
                Debug.WriteLine(line);
                richTextBoxScript.AppendText(line + "\n"); //cmdCommand(line);
            }

            sr.Close();

        }
        private void leerArchivo(string ruta)
        {
            if (File.Exists(ruta))
            {
                StreamReader sr = new StreamReader(ruta);
                richTextBoxScript.Text = "";
                richTextBoxScript.AppendText(sr.ReadLine() + "\n"); 
                richTextBoxScript.AppendText(sr.ReadLine() + "\n");
                richTextBoxScript.AppendText(sr.ReadLine()); 
                sr.Close();
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {

            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
            psi.UseShellExecute = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.Verb = "runas";
            psi.Arguments = "/c netsh wlan show profiles";
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            Process.Start(psi);


            var p = Process.Start(psi);
            //p.WaitForExit();

            var output = p.StandardOutput
                .ReadToEnd()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries))
                .Where(split => split.Length > 1)
                .Select(split => split[1].Trim());

            var listaSSID = new ObservableCollection<string>(output);
            comboBoxSSID.Items.Clear();            //comboBoxSSID.DataSource = null;
            comboBoxSSID.ResetText();
            foreach (string s in listaSSID)
            {
                comboBoxSSID.Items.Add(s);
            }
            comboBoxSSID.SelectedIndex = 0;            //MessageBox.Show(reseaux[1]);
        }


        private void listaAdapatadores() {
            List<string> AdapterList = new List<string>();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    AdapterList.Add(nic.Name);
                }
            }
            comboBoxAdaptador.DataSource = AdapterList;
            comboBoxAdaptador.SelectedItem = 0;
        }

        private void crearMenu() 
        {
            DirectoryInfo d = new DirectoryInfo("scripts"); //Assuming Test is your Folder
            ToolStripMenuItem fileItem = new ToolStripMenuItem("&File");

            ToolStripMenuItem firstSubitem1 = new ToolStripMenuItem("Adaptadores de red", Image.FromFile("NetCard.ico"), menuAdaptadores);
            ToolStripMenuItem firstSubitem2 = new ToolStripMenuItem("Scripts");
            fileItem.DropDownItems.Add(firstSubitem1);
            fileItem.DropDownItems.Add(firstSubitem2);

            FileInfo[] Files = d.GetFiles("*.bat"); //Getting Text files
            foreach (FileInfo file in Files)
            {
                ToolStripMenuItem subSubIte = new ToolStripMenuItem(file.Name, Image.FromFile("NetCard.ico"), menuClick);
                firstSubitem2.DropDownItems.Add(subSubIte);
            }
            ToolStripMenuItem subSubItem1 = new ToolStripMenuItem("Scripts", Image.FromFile("NetCard.ico"), menuClick);

            strip.Items.Add(fileItem);
            this.Controls.Add(strip);
        }

        private void showProfiles()
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(Environment.SystemDirectory, "netsh.exe"),
                Arguments = "wlan show profiles",
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardOutput = true,
            };

        var p = Process.Start(startInfo);
        p.WaitForExit();
            //MessageBox.Show(p.StandardOutput.ReadToEnd());
            var output = p.StandardOutput
                .ReadToEnd()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries))
                .Where(split => split.Length > 1)
                .Select(split => split[1].Trim());
        var reseaux = new ObservableCollection<string>(output);

        comboBoxSSID.Items.Clear();            //comboBoxSSID.DataSource = null;
            comboBoxSSID.ResetText();
            foreach (string s in reseaux)
            {
                comboBoxSSID.Items.Add(s);
            }
            if (comboBoxSSID.Items.Count > 0)
                comboBoxSSID.SelectedIndex = 0;            //MessageBox.Show(reseaux[1]);

        }

        private void checkBoxConectar_Click(object sender, EventArgs e)
        {
            comboBoxSSID.Enabled = checkBoxConectar.Checked;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string b = "/c netsh wlan connect name =\"" + comboBoxSSID.Text + "\" ssid =\"" + comboBoxSSID.Text + "\"";
            cmdCommand(b);
        }

        private void buttonEXE_Click(object sender, EventArgs e)
        {
            string s = "/c " + richTextBoxScript.Lines[0];
            string v = "/c " + richTextBoxScript.Lines[1];
            string g = richTextBoxScript.Lines[2].Replace(":: testPing=", "");

            resetColor(treeViewScripts.Nodes);
            cmdCommand(s);
            cmdCommand(v);

            treeViewScripts.SelectedNode.BackColor = Color.SkyBlue;
            if (ValidateIPv4(g))
                timerPing.Start();
            else
                timerPing.Stop();

        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            treeView.ExpandAll();
            if (treeView.Nodes.Count > 0) 
                treeView.SelectedNode = treeView.Nodes[0];

            //treeView.SelectedNode = treeView.Nodes[0];
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            foreach (var file in directoryInfo.GetFiles())
                directoryNode.Nodes.Add(new TreeNode(file.Name));
            return directoryNode;
        }


        private void comboBoxAdaptador_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxNiIp.Text = "";

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.Name == comboBoxAdaptador.SelectedItem.ToString()) //if (nic.Name == listBox1.SelectedItem.ToString())
                {
                    bool IsDhcpEnabled = nic.GetIPProperties().GetIPv4Properties().IsDhcpEnabled;
                    //textBoxNiName.Text = nic.Name.ToString();
                    radioButtonDHCP.Checked = IsDhcpEnabled;
                    radioButtonStatic.Checked = !IsDhcpEnabled;
                    textBoxNiIp.Enabled = !IsDhcpEnabled;
                    textBoxNiMask.Enabled = !IsDhcpEnabled;
                    foreach (UnicastIPAddressInformation address in nic.GetIPProperties().UnicastAddresses)
                    {
                        if (address.Address.AddressFamily == AddressFamily.InterNetwork)
                            textBoxNiIp.Text = address.Address.ToString();
                        textBoxNiMask.Text = address.IPv4Mask.ToString();
                    }

                }

            }
        }

        private void checkBoxTestPing_CheckedChanged(object sender, EventArgs e)
        {
            if (ValidateIPv4(textBoxTestPing.Text))
                textBoxTestPing.BackColor  = Color.White;
            else
                textBoxTestPing.BackColor  = Color.Coral;

            if (checkBoxTestPing.Checked)
                textBoxTestPing.Enabled = true;
            else
            {
                textBoxTestPing.Enabled = false;
                textBoxTestPing.BackColor = Color.White;
            }

        }

        public void validarDatos(object sender, EventArgs e)
        {
            bool datosOK;
            bool ipOK = ValidateIPv4(textBoxNiIp.Text);
            bool maskOK = ValidateIPv4(textBoxNiMask.Text);
            bool dirOK = radioButtonDHCP.Checked | (ipOK & maskOK);
            bool testPingOK = !checkBoxTestPing.Checked | ValidateIPv4(textBoxTestPing.Text);
            bool conectarOK = !checkBoxConectar.Checked | comboBoxSSID.Text != "";

            datosOK = dirOK & testPingOK & conectarOK;
            buttonCrear.Enabled = datosOK;

        }
        public bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }



        private void resetColor(TreeNodeCollection theNodes)
        {
            foreach (TreeNode theNode in theNodes)
            {
                theNode.BackColor = Color.White;
                if (theNode.Nodes.Count > 0) resetColor(theNode.Nodes);
            }
        }


        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            Form2 frm = new Form2();
            frm.Location = new Point(Screen.PrimaryScreen.Bounds.Width - frm.Width, Screen.PrimaryScreen.Bounds.Height - frm.Height - 30);
            frm.ShowInTaskbar = false;
            frm.Show();
        }

        private void exeScript(string ruta) {
            ProcessStartInfo psi = new ProcessStartInfo(ruta);
            psi.UseShellExecute = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.Verb = "runas";
            //psi.Arguments = strCmd;
            var p = Process.Start(psi);
            p.WaitForExit();              
        }

        private void treeViewScripts_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = Application.StartupPath + "\\" + treeViewScripts.SelectedNode.FullPath.ToString();
            if (File.Exists(path))
            {
                leerArchivo(path);
            }
        }

        private void richTextBoxScript_TextChanged(object sender, EventArgs e)
        {
            buttonEXE.Enabled = (richTextBoxScript.Lines.Count() == 3);
            buttonGuardar.Enabled = (richTextBoxScript.Lines.Count() == 3);
        }



        private void textBoxNiIp_Enter(object sender, EventArgs e)
        {
            textBoxNiIp.BackColor = Color.White;
        }
        private void textBoxNiIp_Leave(object sender, EventArgs e)
        {
            if (!ValidateIPv4(textBoxNiIp.Text) & radioButtonStatic.Checked)
            {
                textBoxNiIp.BackColor = Color.Coral;
            }
            else
            {
                textBoxNiIp.BackColor = Color.White;
            }
        }

        private void textBoxNiMask_Enter(object sender, EventArgs e)
        {
            textBoxNiMask.BackColor = Color.White;
        }

        private void textBoxNiMask_Leave(object sender, EventArgs e)
        {
            if (!ValidateIPv4(textBoxNiMask.Text) & radioButtonStatic.Checked)
            {
                textBoxNiMask.BackColor = Color.Coral;
            }
            else
            {
                textBoxNiMask.BackColor = Color.White;
            }

        }

        private void textBoxTestPing_Enter(object sender, EventArgs e)
        {
            textBoxTestPing.BackColor = Color.White;
        }

        private void textBoxTestPing_Leave(object sender, EventArgs e)
        {
            if (!ValidateIPv4(textBoxTestPing.Text) & checkBoxTestPing.Checked)
                textBoxTestPing.BackColor = Color.Coral;
            else
                textBoxTestPing.BackColor = Color.White;
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["AboutBox1"] == null)
            {
                AboutBox1 form = new AboutBox1();
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Show();
            }
            else
                Application.OpenForms["AboutBox1"].Focus();

        }
        private void leer_archivo_traduccion()
        {
            try
            {
                StreamReader sr = new StreamReader(Application.StartupPath + "\\LaguNET.txt");
                String MenuItem1 = sr.ReadLine().Replace("MenuItem1=", "");
                String MenuItem2 = sr.ReadLine().Replace("MenuItem2=", "");
                String MenuItem3 = sr.ReadLine().Replace("MenuItem3=", "");
                String MenuItem4 = sr.ReadLine().Replace("MenuItem4=", "");
                String MenuItem5 = sr.ReadLine().Replace("MenuItem5=", "");
                String MenuItem6 = sr.ReadLine().Replace("MenuItem6=", "");
                label3.Text = sr.ReadLine().Replace("Text1=", "");
                label1.Text = sr.ReadLine().Replace("Text2=", "");
                label2.Text = sr.ReadLine().Replace("Text3=", "");
                checkBoxConectar.Text = sr.ReadLine().Replace("Text4=", "");
                checkBoxTestPing.Text = sr.ReadLine().Replace("Text5=", "");
                buttonCrear.Text = sr.ReadLine().Replace("Button1=", "");
                buttonGuardar.Text = sr.ReadLine().Replace("Button2=", "");
                buttonEXE.Text = sr.ReadLine().Replace("Button3=", "");
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(Environment.SystemDirectory, "netsh.exe"),
                Arguments = "wlan show profile name=\"" + comboBoxSSID.Text + "\" key=clear",
                WindowStyle = ProcessWindowStyle.Hidden,
                Verb = "runas",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            };
            var p = Process.Start(startInfo);
            p.WaitForExit();
            MessageBox.Show(p.StandardOutput.ReadToEnd());

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}








