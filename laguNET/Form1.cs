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
using System.Threading;
using System.Windows.Shell;

using System.Runtime.InteropServices;
using System.Windows.Data;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;
//using System.Windows.Controls;
//using System.Windows;



namespace laguNETv0
{
    public partial class Form1 : Form
    {

        int faderCount;
        struct confSetting
        {
            public bool isStatic;
            public string ip;
            public string mask;
            public string Gateway;

        }
        confSetting actConf;
        public Form1()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.HelpButton = true;
            this.HelpButtonClicked += infoLagunet;
            InitializeComponent();


            StartPosition = FormStartPosition.CenterScreen;
            ImageList il = new ImageList();
            il.ImageSize = new System.Drawing.Size(16, 16);
            Icon laguIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            string winDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\";
            Icon folderIcon = Icon.ExtractAssociatedIcon(winDir + "explorer.exe");
            //Icon folderIcon = Icon.ExtractAssociatedIcon(@"c:\windows\system32\explorer.exe");

            il.Images.Add("folder", folderIcon);
            il.Images.Add("lagun", laguIcon);
            treeViewScripts.ImageList = il;
            this.Focus();
        }
        private void infoLagunet(object sender, CancelEventArgs e)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxAdaptador.SelectedIndexChanged += validarDatos;
            radioButtonDHCP.CheckedChanged += validarDatos;
            textBoxNiIp.TextChanged += validarDatos;
            textBoxNiMask.TextChanged += validarDatos;
            textBoxNiGateway.TextChanged += validarDatos;

            textBoxNiIp.Leave += validarDatos;
            textBoxNiMask.Leave += validarDatos;
            textBoxNiGateway.Leave += validarDatos;

            textBoxNiIp.Enter += validarDatos;
            textBoxNiMask.Enter += validarDatos;
            textBoxTestPing.TextChanged += validarDatos;
            checkBoxConectar.CheckedChanged += validarDatos;
            checkBoxTestPing.CheckedChanged += validarDatos;
            comboBoxSSID.SelectedIndexChanged += validarDatos;
            comboBoxSSID.MouseClick += validarDatos;

            contextMenuStrip1.MouseClick += contextMenuClick;

            validarDatos(sender, e);
            leer_archivo_traduccion();

            buttonStatus.BackColor = Form1.DefaultBackColor;
            buttonStatus.FlatAppearance.BorderSize = 0;

            buttonInfo.BackColor = Form1.DefaultBackColor;
            buttonInfo.FlatAppearance.BorderSize = 0;
            buttonInfo.Image = SystemIcons.Question.ToBitmap();

            listaAdapatadores();            // LISTA DE ADAPTADORES DE RED
            showProfiles();           // rellena el combobox con las SSID

            comboBoxSSID.Enabled = checkBoxConectar.Checked;
            treeViewScripts.Focus();
           
            ListDirectory(treeViewScripts, Application.StartupPath + "\\scripts");

        }

        private void crearMenu() 
        {
            //MenuItem myMenuItem = new MenuItem("Show Me");
            //ContextMenu mnu = new ContextMenu();
            //mnu.MenuItems.Add("Edit");
            //myMenuItem.Click += new EventHandler(myMenuItem_Click);
        }

        private void timerPing_Tick(object sender, EventArgs e)
        {
            Ping HacerPing = new Ping();
            PingReply RespuestaPing;
            string sDireccion = richTextBoxScript.Lines[2].Replace(":: testPing=", "");
            timerFade.Start();
            //labelNombreScript.ForeColor = Color.FromArgb(faderCount, 0,0);

            if (ValidateIPv4(sDireccion)) {


                try
                {                   
                    RespuestaPing = HacerPing.Send(sDireccion, 500);
                    if (RespuestaPing.Status == IPStatus.Success)
                    {
                        //labelNombreScript.ForeColor = Color.LightGreen;
                        buttonStatus.BackColor = Color.LawnGreen;
                        timerPing.Stop();
                        timerFade.Stop();
                    }

                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }

        private void cmdCommand(string strCmd)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
            psi.UseShellExecute = true;
            psi.RedirectStandardOutput = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.Verb = "runas";
            psi.Arguments = strCmd;

            var p = Process.Start(psi);
            p.WaitForExit();
            // var output = p.StandardOutput.ReadToEnd();
        }

        private void rellenarScript()
        {
            string s, b, l;

            if (radioButtonDHCP.Checked)
                s = "netsh interface ipv4 set address name=\"" + comboBoxAdaptador.Text + "\" source=dhcp";
            else
                s = "netsh interface ipv4 set address name=\"" + comboBoxAdaptador.Text + "\" static " + textBoxNiIp.Text + " " + textBoxNiMask.Text + " " + textBoxNiGateway.Text;

            if (checkBoxConectar.Checked)
                b = "netsh wlan connect name =\"" + comboBoxSSID.Text + "\" ssid =\"" + comboBoxSSID.Text + "\"";
            else
                b = ":: No conectar ";

            if (checkBoxTestPing.Checked)
                l = ":: testPing=" + textBoxTestPing.Text;
            else
                l = ":: No test Ping";
            labelNombreScript.Text = "Script:";
            //labelNombreScript.ForeColor = Color.Black;
            buttonStatus.BackColor = Form1.DefaultBackColor;
            richTextBoxScript.Text = s + "\n" + b + "\n" + l;
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
                Directory.CreateDirectory(dir);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog() {
                Filter = "Batch script|*.bat|Texto plano|*.txt",
                Title = "Guardar script",
                InitialDirectory = Application.StartupPath + "\\scripts",
                };

            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName.ToString() != "") 
            { 
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName.ToString());         
                sw.WriteLine(richTextBoxScript.Text); 
                sw.Close(); 
            }
            ListDirectory(treeViewScripts, dir);

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
                labelNombreScript.Text = ruta.Split('\\').Last();
                buttonStatus.BackColor = Form1.DefaultBackColor;
                //labelNombreScript.ForeColor = Color.Black;
            }

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
            //AdapterList.Add("");// para seleccionarlo cunado ,al leer un script no exite el adaptador
            comboBoxAdaptador.DataSource = AdapterList;
            comboBoxAdaptador.SelectedItem = 0;
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
            //comboBoxSSID.Items.Add(""); // para seleccionarlo cunado ,al leer un script no exite la SSID
            if (comboBoxSSID.Items.Count > 0)
                comboBoxSSID.SelectedIndex = 0;            //MessageBox.Show(reseaux[1]);

        }

        private void checkBoxConectar_Click(object sender, EventArgs e)
        {
            comboBoxSSID.Enabled = checkBoxConectar.Checked;
        }

        private void buttonEXE_Click(object sender, EventArgs e)
        {
            string s = "/c " + richTextBoxScript.Lines[0];
            string v = "/c " + richTextBoxScript.Lines[1];
            string g = richTextBoxScript.Lines[2].Replace(":: testPing=", "");

            resetColor(treeViewScripts.Nodes);
            cmdCommand(s);
            cmdCommand(v);

            if (!g.Contains("No"))
            {
                if (ValidateIPv4(g))
                    timerPing.Start();
                else
                    timerPing.Stop();
            }
            else 
            {
                //labelNombreScript.ForeColor = Color.Blue;
                buttonStatus.BackColor = Color.Turquoise;
            }


        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            treeView.ExpandAll();

            foreach (TreeNode RootNode in treeView.Nodes)
            {
                menuTree(RootNode.Nodes);
            }

            if (treeView.Nodes.Count > 0) 
                treeView.SelectedNode = treeView.Nodes[0];

        }
        private void contextMenuClick(object sender, EventArgs e)
        {
            string f = Application.StartupPath + "\\" + treeViewScripts.SelectedNode.FullPath.ToString();
            //MessageBox.Show(f);
            if (File.Exists(f))
                cmdCommand("/c notepad.exe \"" + f + "\"");

        }
        private void menuTree(TreeNodeCollection theNodes)
        {
            foreach (TreeNode theNode in theNodes)
            {
                if (theNode.Text.ToLower().Contains("bat"))
                {
                    theNode.ContextMenuStrip = contextMenuStrip1;
                    theNode.ImageKey = "lagun";
                    theNode.SelectedImageKey = "lagun";
                }
                else
                {
                    theNode.ImageKey = "folder";
                    theNode.SelectedImageKey = "folder";
                }

                    
                if (theNode.Nodes.Count > 0) menuTree(theNode.Nodes);
            }
        }
        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            foreach (var file in directoryInfo.GetFiles())
                if (file.Name.ToLower().Contains("bat")) // filtro para mostrar solo los .bat
                    directoryNode.Nodes.Add(new TreeNode(file.Name));
            return directoryNode;
        }


        private void comboBoxAdaptador_SelectedIndexChanged(object sender, EventArgs e)
        {
            actConf.ip = "";
            actConf.mask = "";
            actConf.Gateway = "";
            
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.Name == comboBoxAdaptador.SelectedItem.ToString()) 
                {
                    bool IsDhcpEnabled = nic.GetIPProperties().GetIPv4Properties().IsDhcpEnabled;
                    actConf.isStatic = !IsDhcpEnabled;

                    foreach (UnicastIPAddressInformation address in nic.GetIPProperties().UnicastAddresses)
                    {
                        if (address.Address.AddressFamily == AddressFamily.InterNetwork) 
                            actConf.ip = address.Address.ToString();
                        actConf.mask = address.IPv4Mask.ToString();
                        
                    }
                    // pendiente coger la direccion de gateway // prueba
                    foreach (GatewayIPAddressInformation address2 in nic.GetIPProperties().GatewayAddresses)
                    {
                        if (address2.Address.AddressFamily == AddressFamily.InterNetwork)
                            actConf.Gateway = address2.Address.ToString();
                    }

                }

            }
            //NetworkInterface card = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault();
            //if (card != null)
            //{
            //    GatewayIPAddressInformation address = card.GetIPProperties().GatewayAddresses.FirstOrDefault();
            //    if (address != null)
            //    {
            //        textBoxNiGateway.Text = address.Address.ToString();
            //    }
            //}


            radioButtonDHCP.Checked = !actConf.isStatic;
            radioButtonStatic.Checked = actConf.isStatic;
            textBoxNiIp.Enabled = actConf.isStatic;
            textBoxNiMask.Enabled = actConf.isStatic;
            textBoxNiIp.Text = actConf.ip;
            textBoxNiMask.Text = actConf.mask;
            textBoxNiGateway.Text = actConf.Gateway;


        }

        private void checkBoxTestPing_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTestPing.Checked & ValidateIPv4(textBoxNiIp.Text))
            {
                string[] l = textBoxNiIp.Text.Split('.');     //string f = textBoxNiIp.Text.Split('.')[2];                
                textBoxTestPing.Text = l[0] + "." + l[1] + "." + l[2] + ".1";
            }

        }

        public void validarDatos(object sender, EventArgs e)
        {
            bool datosOK;
            bool adaptadorOK = comboBoxAdaptador.Text != "";
            bool ipOK = ValidateIPv4(textBoxNiIp.Text);
            bool maskOK = ValidateIPv4(textBoxNiMask.Text);
            bool gatewayOK = (ValidateIPv4(textBoxNiGateway.Text)) | textBoxNiGateway.Text =="";
            bool dirOK = radioButtonDHCP.Checked | (ipOK & maskOK & gatewayOK);
            bool testPingOK = !checkBoxTestPing.Checked | ValidateIPv4(textBoxTestPing.Text);
            bool conectarOK = !checkBoxConectar.Checked | comboBoxSSID.Text != "";

            datosOK = adaptadorOK & dirOK & testPingOK & conectarOK;

            if (radioButtonStatic.Checked & !ipOK)
                textBoxNiIp.BackColor = Color.Coral;
            else
                textBoxNiIp.BackColor = Color.White;

            if (radioButtonStatic.Checked & !maskOK)
                textBoxNiMask.BackColor = Color.Coral;
            else
                textBoxNiMask.BackColor = Color.White;

            if (radioButtonStatic.Checked & !gatewayOK)
                textBoxNiGateway.BackColor = Color.Coral;
            else
                textBoxNiGateway.BackColor = Color.White;

            if (checkBoxTestPing.Checked & !ValidateIPv4(textBoxTestPing.Text))
                textBoxTestPing.BackColor = Color.Coral;
            else
                textBoxTestPing.BackColor = Color.White;


            textBoxTestPing.Enabled = checkBoxTestPing.Checked;

            if (datosOK)
            {
                rellenarScript();
            }
            else
            {
                richTextBoxScript.Text = ":: Null\n:: Null\n:: Null";
            }


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

        private void treeViewScripts_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = Application.StartupPath + "\\" + treeViewScripts.SelectedNode.FullPath.ToString();
            //MessageBox.Show(path);
            if (File.Exists(path))
                leerArchivo(path);
        }

        private void richTextBoxScript_TextChanged(object sender, EventArgs e)
        {
            timerPing.Stop();
            timerFade.Stop();
            buttonEXE.Enabled = (richTextBoxScript.Lines.Count() == 3);
            buttonGuardar.Enabled = (richTextBoxScript.Lines.Count() == 3);
        }

        private void textBoxNiIp_Enter(object sender, EventArgs e)
        {
            //textBoxNiIp.BackColor = Color.White;
        }
        private void textBoxNiIp_Leave(object sender, EventArgs e)
        {
        // https://asp-blogs.azurewebsites.net/razan/finding-subnet-mask-from-ip4-address-using-c
            if (textBoxNiMask.Text == "")
            {
                string r = textBoxNiIp.Text.Split('.')[0];
                int n = int.Parse(r);
                if (n >= 0 & n <= 127)
                    textBoxNiMask.Text = "255.0.0.0";
                else if (n >= 128 & n <= 191)
                    textBoxNiMask.Text = "255.255.0.0";
                else if(n >= 192 & n <= 223)
                    textBoxNiMask.Text = "255.255.255.0";
                else
                    textBoxNiMask.Text = "0.0.0.0";
            }
        }


        private void buttonInfo_Click(object sender, EventArgs e)
        {
            //if (Application.OpenForms["AboutBox1"] == null)
            //{
            //    AboutBox1 form = new AboutBox1();
            //    form.StartPosition = FormStartPosition.CenterScreen;
            //    form.Show();
            //}
            //else
            //    Application.OpenForms["AboutBox1"].Focus();

        }
        private void leer_archivo_traduccion()
        {
            try
            {
                StreamReader sr = new StreamReader(Application.StartupPath + "\\LaguNET.txt");
                String MenuItem1 = sr.ReadLine().Replace("MenuItem1=", "");
                String MenuItem2 = sr.ReadLine().Replace("MenuItem2=", "");
                String MenuItem3 = sr.ReadLine().Replace("MenuItem3=", "");
                label3.Text = sr.ReadLine().Replace("Text1=", "");
                label1.Text = sr.ReadLine().Replace("Text2=", "");
                label2.Text = sr.ReadLine().Replace("Text3=", "");
                label4.Text = sr.ReadLine().Replace("Text4=", "");
                checkBoxConectar.Text = sr.ReadLine().Replace("Text5=", "");
                checkBoxTestPing.Text = sr.ReadLine().Replace("Text6=", "");
                buttonGuardar.Text = sr.ReadLine().Replace("Button1=", "");
                buttonEXE.Text = sr.ReadLine().Replace("Button2=", "");
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        private void obtenerDatosScript(string st)
        {
            Console.WriteLine("Exception: ");
            try
            {
                StreamReader sr = new StreamReader(Application.StartupPath + "\\" + st);
                string l1 = sr.ReadLine();
                string l2 = sr.ReadLine();
                string l3 = sr.ReadLine();
                string txt = st;
                sr.Close();
                string sAdaptador = "", sIp = "", sMask = "", sConectarRedNombre = "", sTestIpPing = "";
                bool bModoStatic = false, bConectarRed = false, bTestPing = false;


                if (l1.Contains("\""))
                {
                    var reg = new Regex("\".*?\"");
                    var v1 = reg.Matches(l1);
                    sAdaptador = v1[0].ToString().Replace("\"","");
                }
                else
                {
                    sAdaptador = "no adaptador";
                }

                bModoStatic = l1.ToLower().Contains("static");
                if (bModoStatic)
                {
                    radioButtonDHCP.Checked = !bModoStatic;
                    radioButtonStatic.Checked = bModoStatic;

                    string[] lista = l1.Split(' ');
                    sIp   = lista[7];
                    sMask = lista[8];
                }
                else
                {
                    radioButtonDHCP.Checked = !bModoStatic;
                    radioButtonStatic.Checked = bModoStatic;
                }

                bConectarRed = l2.Contains("\"");
                if (bConectarRed){
                    var reg = new Regex("\".*?\"");
                    var v2 = reg.Matches(l2);
                    sConectarRedNombre = v2[0].ToString().Replace("\"", "");
                }

                bTestPing = l3.Contains("=");
                if (bTestPing)
                {
                    sTestIpPing = l3.Replace(":: testPing=", "");
                }

                // para saber si un string existe en la lista del combobox
                bool isValid = comboBoxAdaptador.Items.Cast<Object>().Any(x => comboBoxAdaptador.GetItemText(x) == sAdaptador);
                if (isValid)
                    comboBoxAdaptador.Text = sAdaptador;
                else
                    comboBoxAdaptador.Text = "";
                radioButtonDHCP.Checked = !bModoStatic;
                radioButtonStatic.Checked = bModoStatic;
                if (!bModoStatic)
                    textBoxNiIp.Clear(); ; textBoxNiMask.Clear();
                textBoxNiIp.Text = sIp;
                textBoxNiMask.Text = sMask;
                checkBoxConectar.Checked = bConectarRed;
                if (bConectarRed)
                    comboBoxSSID.Text = sConectarRedNombre;
                else
                    comboBoxSSID.Text = "";
                checkBoxTestPing.Checked = bTestPing;
                if (bTestPing)
                    textBoxTestPing.Text = sTestIpPing;
                else
                    textBoxTestPing.Clear();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }
        private void labelKeyClear_DoubleClick(object sender, EventArgs e)
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
        private void labelIpConfig_DoubleClick(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo
            {
                //netsh interface ip show addresses "Ethernet"
                FileName = Path.Combine(Environment.SystemDirectory, "netsh.exe"),
                Arguments = "interface ip show addresses name=\"" + comboBoxAdaptador.Text + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                Verb = "runas",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            };
            var p = Process.Start(startInfo);
            p.WaitForExit();
            MessageBox.Show(p.StandardOutput.ReadToEnd());
        }

        private void timerFade_Tick(object sender, EventArgs e)
        {
            if (faderCount > 30)
            {
                faderCount -= 2;
                //labelNombreScript.ForeColor = Color.FromArgb(faderCount, 0, 0);
                buttonStatus.BackColor = Color.FromArgb(faderCount, 0, 0);
                if (faderCount < 120)
                    faderCount -= 10;
            }
            else
                faderCount = 255;



        }

        private void treeViewScripts_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            buttonEXE_Click(sender, e);
        }

    }
}








