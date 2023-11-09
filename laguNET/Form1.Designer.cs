namespace laguNETv0
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
     
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonCrear = new System.Windows.Forms.Button();
            this.textBoxNiIp = new System.Windows.Forms.TextBox();
            this.textBoxNiMask = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonDHCP = new System.Windows.Forms.RadioButton();
            this.radioButtonStatic = new System.Windows.Forms.RadioButton();
            this.buttonGuardar = new System.Windows.Forms.Button();
            this.richTextBoxScript = new System.Windows.Forms.RichTextBox();
            this.comboBoxSSID = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxConectar = new System.Windows.Forms.CheckBox();
            this.buttonEXE = new System.Windows.Forms.Button();
            this.timerPing = new System.Windows.Forms.Timer(this.components);
            this.comboBoxAdaptador = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxTestPing = new System.Windows.Forms.CheckBox();
            this.textBoxTestPing = new System.Windows.Forms.TextBox();
            this.treeViewScripts = new System.Windows.Forms.TreeView();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCrear
            // 
            this.buttonCrear.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCrear.Location = new System.Drawing.Point(226, 357);
            this.buttonCrear.Name = "buttonCrear";
            this.buttonCrear.Size = new System.Drawing.Size(251, 32);
            this.buttonCrear.TabIndex = 2;
            this.buttonCrear.Text = "Crear Script";
            this.buttonCrear.UseVisualStyleBackColor = true;
            this.buttonCrear.Click += new System.EventHandler(this.buttonCrear_Click);
            // 
            // textBoxNiIp
            // 
            this.textBoxNiIp.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNiIp.Location = new System.Drawing.Point(226, 119);
            this.textBoxNiIp.Name = "textBoxNiIp";
            this.textBoxNiIp.Size = new System.Drawing.Size(238, 23);
            this.textBoxNiIp.TabIndex = 7;
            this.textBoxNiIp.Enter += new System.EventHandler(this.textBoxNiIp_Enter);
            this.textBoxNiIp.Leave += new System.EventHandler(this.textBoxNiIp_Leave);
            // 
            // textBoxNiMask
            // 
            this.textBoxNiMask.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNiMask.Location = new System.Drawing.Point(226, 170);
            this.textBoxNiMask.Name = "textBoxNiMask";
            this.textBoxNiMask.Size = new System.Drawing.Size(237, 23);
            this.textBoxNiMask.TabIndex = 9;
            this.textBoxNiMask.Enter += new System.EventHandler(this.textBoxNiMask_Enter);
            this.textBoxNiMask.Leave += new System.EventHandler(this.textBoxNiMask_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(223, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "ip";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(222, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Mask";
            // 
            // radioButtonDHCP
            // 
            this.radioButtonDHCP.AutoSize = true;
            this.radioButtonDHCP.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonDHCP.Location = new System.Drawing.Point(5, 3);
            this.radioButtonDHCP.Name = "radioButtonDHCP";
            this.radioButtonDHCP.Size = new System.Drawing.Size(56, 20);
            this.radioButtonDHCP.TabIndex = 14;
            this.radioButtonDHCP.TabStop = true;
            this.radioButtonDHCP.Text = "DHCP";
            this.radioButtonDHCP.UseVisualStyleBackColor = true;
            this.radioButtonDHCP.CheckedChanged += new System.EventHandler(this.radioButtonDHCP_CheckedChanged);
            // 
            // radioButtonStatic
            // 
            this.radioButtonStatic.AutoSize = true;
            this.radioButtonStatic.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonStatic.Location = new System.Drawing.Point(112, 3);
            this.radioButtonStatic.Name = "radioButtonStatic";
            this.radioButtonStatic.Size = new System.Drawing.Size(57, 20);
            this.radioButtonStatic.TabIndex = 15;
            this.radioButtonStatic.TabStop = true;
            this.radioButtonStatic.Text = "Static";
            this.radioButtonStatic.UseVisualStyleBackColor = true;
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Enabled = false;
            this.buttonGuardar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGuardar.Location = new System.Drawing.Point(226, 397);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(120, 32);
            this.buttonGuardar.TabIndex = 17;
            this.buttonGuardar.Text = "GUARDAR";
            this.buttonGuardar.UseVisualStyleBackColor = true;
            this.buttonGuardar.Click += new System.EventHandler(this.buttonGuardar_Click);
            // 
            // richTextBoxScript
            // 
            this.richTextBoxScript.Enabled = false;
            this.richTextBoxScript.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxScript.Location = new System.Drawing.Point(6, 434);
            this.richTextBoxScript.Name = "richTextBoxScript";
            this.richTextBoxScript.Size = new System.Drawing.Size(475, 59);
            this.richTextBoxScript.TabIndex = 22;
            this.richTextBoxScript.Text = "";
            this.richTextBoxScript.TextChanged += new System.EventHandler(this.richTextBoxScript_TextChanged);
            // 
            // comboBoxSSID
            // 
            this.comboBoxSSID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSSID.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSSID.FormattingEnabled = true;
            this.comboBoxSSID.Location = new System.Drawing.Point(226, 237);
            this.comboBoxSSID.Name = "comboBoxSSID";
            this.comboBoxSSID.Size = new System.Drawing.Size(251, 24);
            this.comboBoxSSID.TabIndex = 24;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonStatic);
            this.panel1.Controls.Add(this.radioButtonDHCP);
            this.panel1.Location = new System.Drawing.Point(226, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(222, 28);
            this.panel1.TabIndex = 25;
            // 
            // checkBoxConectar
            // 
            this.checkBoxConectar.AutoSize = true;
            this.checkBoxConectar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxConectar.Location = new System.Drawing.Point(229, 211);
            this.checkBoxConectar.Name = "checkBoxConectar";
            this.checkBoxConectar.Size = new System.Drawing.Size(100, 20);
            this.checkBoxConectar.TabIndex = 26;
            this.checkBoxConectar.Text = "Conectar red";
            this.checkBoxConectar.UseVisualStyleBackColor = true;
            this.checkBoxConectar.Click += new System.EventHandler(this.checkBoxConectar_Click);
            // 
            // buttonEXE
            // 
            this.buttonEXE.Enabled = false;
            this.buttonEXE.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEXE.Location = new System.Drawing.Point(357, 396);
            this.buttonEXE.Name = "buttonEXE";
            this.buttonEXE.Size = new System.Drawing.Size(120, 32);
            this.buttonEXE.TabIndex = 28;
            this.buttonEXE.Text = "EXE";
            this.buttonEXE.UseVisualStyleBackColor = true;
            this.buttonEXE.Click += new System.EventHandler(this.buttonEXE_Click);
            // 
            // timerPing
            // 
            this.timerPing.Interval = 1000;
            this.timerPing.Tick += new System.EventHandler(this.timerPing_Tick);
            // 
            // comboBoxAdaptador
            // 
            this.comboBoxAdaptador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAdaptador.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxAdaptador.FormattingEnabled = true;
            this.comboBoxAdaptador.Location = new System.Drawing.Point(226, 37);
            this.comboBoxAdaptador.Name = "comboBoxAdaptador";
            this.comboBoxAdaptador.Size = new System.Drawing.Size(209, 24);
            this.comboBoxAdaptador.TabIndex = 31;
            this.comboBoxAdaptador.SelectedIndexChanged += new System.EventHandler(this.comboBoxAdaptador_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(223, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 32;
            this.label3.Text = "Adaptador";
            // 
            // checkBoxTestPing
            // 
            this.checkBoxTestPing.AutoSize = true;
            this.checkBoxTestPing.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxTestPing.Location = new System.Drawing.Point(229, 281);
            this.checkBoxTestPing.Name = "checkBoxTestPing";
            this.checkBoxTestPing.Size = new System.Drawing.Size(79, 20);
            this.checkBoxTestPing.TabIndex = 33;
            this.checkBoxTestPing.Text = "Test Ping";
            this.checkBoxTestPing.UseVisualStyleBackColor = true;
            this.checkBoxTestPing.CheckedChanged += new System.EventHandler(this.checkBoxTestPing_CheckedChanged);
            // 
            // textBoxTestPing
            // 
            this.textBoxTestPing.Enabled = false;
            this.textBoxTestPing.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTestPing.Location = new System.Drawing.Point(225, 307);
            this.textBoxTestPing.Name = "textBoxTestPing";
            this.textBoxTestPing.Size = new System.Drawing.Size(252, 23);
            this.textBoxTestPing.TabIndex = 34;
            this.textBoxTestPing.Enter += new System.EventHandler(this.textBoxTestPing_Enter);
            this.textBoxTestPing.Leave += new System.EventHandler(this.textBoxTestPing_Leave);
            // 
            // treeViewScripts
            // 
            this.treeViewScripts.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewScripts.Location = new System.Drawing.Point(6, 14);
            this.treeViewScripts.Name = "treeViewScripts";
            this.treeViewScripts.Size = new System.Drawing.Size(214, 414);
            this.treeViewScripts.TabIndex = 35;
            this.treeViewScripts.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewScripts_AfterSelect);
            // 
            // buttonInfo
            // 
            this.buttonInfo.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.buttonInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonInfo.Location = new System.Drawing.Point(451, -1);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(40, 42);
            this.buttonInfo.TabIndex = 37;
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(462, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 20);
            this.label4.TabIndex = 38;
            this.label4.Text = "*";
            this.label4.DoubleClick += new System.EventHandler(this.label4_DoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 499);
            this.Controls.Add(this.textBoxTestPing);
            this.Controls.Add(this.comboBoxSSID);
            this.Controls.Add(this.buttonInfo);
            this.Controls.Add(this.treeViewScripts);
            this.Controls.Add(this.checkBoxTestPing);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxAdaptador);
            this.Controls.Add(this.buttonEXE);
            this.Controls.Add(this.checkBoxConectar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richTextBoxScript);
            this.Controls.Add(this.buttonGuardar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxNiMask);
            this.Controls.Add(this.textBoxNiIp);
            this.Controls.Add(this.buttonCrear);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "laguNET";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonCrear;
        private System.Windows.Forms.TextBox textBoxNiIp;
        private System.Windows.Forms.TextBox textBoxNiMask;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonDHCP;
        private System.Windows.Forms.RadioButton radioButtonStatic;
        private System.Windows.Forms.Button buttonGuardar;
        private System.Windows.Forms.RichTextBox richTextBoxScript;
        private System.Windows.Forms.ComboBox comboBoxSSID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxConectar;
        private System.Windows.Forms.Button buttonEXE;
        private System.Windows.Forms.Timer timerPing;
        private System.Windows.Forms.ComboBox comboBoxAdaptador;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxTestPing;
        private System.Windows.Forms.TextBox textBoxTestPing;
        private System.Windows.Forms.TreeView treeViewScripts;
        private System.Windows.Forms.Button buttonInfo;
        private System.Windows.Forms.Label label4;
    }
}

