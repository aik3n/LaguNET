using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace laguNETv0
{
    public partial class Form2 : Form
    {
        string sDirIP;
        string MenuItem1, MenuItem2, MenuItem3;
        int intentos = 0;
        public Form2()
        {           
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            this.ShowInTaskbar = false;

            if (!File.Exists(Application.StartupPath + "\\LaguNET.txt"))
                escribir_archivo_traduccion();
            leer_archivo_traduccion();

            ContextMenu m_menu = new ContextMenu();
            m_menu.MenuItems.Add(new MenuItem("LaguNET Config", new System.EventHandler(mostrarCfg)));
            m_menu.MenuItems.Add("-");

            if (File.Exists(Application.StartupPath + "\\Menu.txt")){
                var linesRead = File.ReadLines(Application.StartupPath + "\\Menu.txt");
                foreach (var lineRead in linesRead)
                {
                    try
                    {
                        string[] l = lineRead.Split(',');
                        if (lineRead.First() != ';' & l.Count() == 2) 
                        {
                            var menuItem = new MenuItem(l[0], new System.EventHandler(item_Click));
                            menuItem.Tag = l[1];// le paso el comando definido en menu.txt en la propiedad "tag", luego se usara la leer esta propiedad al clickar en el menu
                            m_menu.MenuItems.Add(menuItem);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            } 
            
            m_menu.MenuItems.Add("-");
            m_menu.MenuItems.Add(new MenuItem(MenuItem2, new System.EventHandler(carpeta_scripts)));
            m_menu.MenuItems.Add(new MenuItem(MenuItem3, new System.EventHandler(Exit_Click)));


            notifyIcon1.ContextMenu = m_menu;
            string dir = "scripts"; // If directory does not exist, create it.
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            ListDirectory(treeView1, Application.StartupPath + "\\scripts");

        }
        protected void item_Click(object sender, System.EventArgs e)
        {
            var MI = (MenuItem)sender;
            //MessageBox.Show(MI.Tag.ToString());
            exeCmd(MI.Tag.ToString());
        }
        protected void carpeta_scripts(Object sender, System.EventArgs e)
        {
            exeCmd("explorer.exe " + Application.StartupPath.ToString() + "\\scripts");            //exeCmd("explorer.exe c:");
            this.Hide();
        }

        protected void exeCmd(string s)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
            psi.UseShellExecute = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.Verb = "runas";
            psi.Arguments = "/c " + s;
            var p = Process.Start(psi);
            p.WaitForExit();
        }
        protected void Exit_Click(Object sender, System.EventArgs e)
        {           
            Close();
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            treeView.ExpandAll();
            if (treeView.Nodes.Count > 0)
            {
                treeView.SelectedNode = treeView.Nodes[0];
            }

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


        private void mostrarCfg(object sender, EventArgs e)
        {
            if (Application.OpenForms["Form1"] == null)
            {
                Form1 form = new Form1();
                form.StartPosition = FormStartPosition.CenterScreen;
                //form.MdiParent = this;
                form.Show();

            }
            else
                Application.OpenForms["Form1"].Focus();

        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string path = Application.StartupPath + "\\" + treeView1.SelectedNode.FullPath.ToString();
            sDirIP = leerArchivo(path);
            intentos = 0;
            if (File.Exists(path))
            {
                resetColor(treeView1.Nodes);
                string s = sDirIP.Replace(":: testPing=", "");

                exeScript(path);
                if (!sDirIP.Contains("No"))
                {
                    label1.Text = "Ping: " + s;               
                    label1.Visible = true;
                    if (ValidateIPv4(s))
                        timer1.Start();
                    else
                    {
                        treeView1.SelectedNode.BackColor = Color.SkyBlue;
                        label1.Text = "IP no valida: " + s;
                    }
                }
                else // si no se hace ping cierra directamente
                {
                    this.Hide();
                    notifyIcon1.Text = "Ultima configuracion: " + treeView1.SelectedNode.Text;
                    notifyIcon1.BalloonTipTitle = "LaguNET";
                    notifyIcon1.BalloonTipText = "Datos cambiados";
                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon1.ShowBalloonTip(1000);

                }

                    
                    
            }
            label1.Focus();

        }
        private void exeScript(string ruta)
        {
            ProcessStartInfo psi = new ProcessStartInfo(ruta);
            psi.UseShellExecute = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.Verb = "runas";
            //psi.Arguments = strCmd;
            var p = Process.Start(psi);
            p.WaitForExit();
        }
        private void resetColor(TreeNodeCollection theNodes)
        {
            foreach (TreeNode theNode in theNodes)
            {
                theNode.BackColor = Color.White; //Color.FromArgb(192, 255, 192);
                if (theNode.Nodes.Count > 0) resetColor(theNode.Nodes);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Ping HacerPing = new Ping();
            PingReply RespuestaPing;

            string sDireccion = sDirIP.Replace(":: testPing=", ""); //sDireccion = "192.168.249.101"; // RM1   //sDireccion = "8.8.8.8"; // www.google.es
            label1.Text = "Ping: " + sDireccion; //label1.Text = "Ping("+intentos+"): " + sDireccion;
            if (intentos++ < 17)
            {
                if (ValidateIPv4(sDireccion))
                {
                    label1.Visible = true; // la quito por estetica
                    progressBar1.Visible = true;
                    progressBar1.Value = intentos;
                    try
                    {
                        RespuestaPing = HacerPing.Send(sDireccion, 500);
                        if (RespuestaPing.Status == IPStatus.Success)
                        {
                            treeView1.SelectedNode.BackColor = Color.LightGreen;
                            timer1.Stop();
                            timerClose.Start();
                        }
                        else
                        {
                            treeView1.SelectedNode.BackColor = Color.Khaki;
                        }
                    }
                    catch (Exception ex)
                    {
                        treeView1.SelectedNode.BackColor = Color.Khaki;
                    }
                }
            }
            else
            {
                notifyIcon1.BalloonTipTitle = "LaguNET";
                notifyIcon1.BalloonTipText = "Timeout Ping: " + sDirIP.Replace(":: testPing=", "");
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                notifyIcon1.ShowBalloonTip(1000);
                timer1.Stop();
                this.Hide();
            }
        }
        private string leerArchivo(string ruta)
        {
            if (File.Exists(ruta))
            {
                StreamReader sr = new StreamReader(ruta);
                string l1 = sr.ReadLine();
                string l2 = sr.ReadLine();
                string l3 = sr.ReadLine();
                sr.Close();
                return l3;
            }
            else
                return "no accesible";

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

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Application.OpenForms["Form1"] == null)
            {
                Form1 form = new Form1();
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Show();
            }
            else
                Application.OpenForms["Form1"].Focus();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ListDirectory(treeView1, Application.StartupPath + "\\scripts");
                BringToFront();
                Show();
                treeView1.Focus();
            }

        }
        private void Form2_Deactivate(object sender, EventArgs e)
        {
            label1.Visible = false;
            progressBar1.Visible = false;
            this.Hide();

        }
        private void timerClose_Tick(object sender, EventArgs e)
        {
            timerClose.Stop();
            if (!sDirIP.Contains("No"))
            {
                notifyIcon1.Text= "Ultima conexion: " + treeView1.SelectedNode.Text;
                notifyIcon1.BalloonTipTitle = "LaguNET";
                notifyIcon1.BalloonTipText = "Conexion OK: " + sDirIP.Replace(":: testPing=", "");
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.ShowBalloonTip(1000);
            }
            this.Hide();
        }
        private void escribir_archivo_traduccion()
        {
            try
            {
                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\LaguNET.txt");
                sw.WriteLine("MenuItem1=LaguNET Config");
                sw.WriteLine("MenuItem2=Carpeta Scripts");
                sw.WriteLine("MenuItem3=Salir");
                sw.WriteLine("Text1=Adaptador");
                sw.WriteLine("Text2=IP");
                sw.WriteLine("Text3=Mask");
                sw.WriteLine("Text4=Conectar red");
                sw.WriteLine("Text5=Test Ping");
                sw.WriteLine("Button1=Guardar");
                sw.WriteLine("Button2=EXE");
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        private void leer_archivo_traduccion()
        {
            try
            {
                StreamReader sr = new StreamReader(Application.StartupPath + "\\LaguNET.txt");
                MenuItem1 = sr.ReadLine().Replace("MenuItem1=", "");
                MenuItem2 = sr.ReadLine().Replace("MenuItem2=", "");
                MenuItem3 = sr.ReadLine().Replace("MenuItem3=", "");
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

        }
    }
}
