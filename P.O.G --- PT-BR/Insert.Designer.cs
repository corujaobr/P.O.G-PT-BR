namespace POG_BR
{
    partial class Insert
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
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this._File = new System.Windows.Forms.OpenFileDialog();
            this.btnFILE = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TBS = new System.Windows.Forms.TabPage();
            this.TBSITE = new System.Windows.Forms.TabPage();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.txtURLSITE = new System.Windows.Forms.TextBox();
            this.txtCommandSITE = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.TBS.SuspendLayout();
            this.TBSITE.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCommand
            // 
            this.txtCommand.Location = new System.Drawing.Point(69, 40);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(161, 20);
            this.txtCommand.TabIndex = 0;
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(69, 97);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(161, 20);
            this.txtURL.TabIndex = 0;
            // 
            // txtProcess
            // 
            this.txtProcess.Location = new System.Drawing.Point(69, 161);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.Size = new System.Drawing.Size(161, 20);
            this.txtProcess.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(111, 252);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Cadastrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _File
            // 
            this._File.FileName = "openFileDialog1";
            // 
            // btnFILE
            // 
            this.btnFILE.Location = new System.Drawing.Point(236, 98);
            this.btnFILE.Name = "btnFILE";
            this.btnFILE.Size = new System.Drawing.Size(24, 19);
            this.btnFILE.TabIndex = 2;
            this.btnFILE.Text = "...";
            this.btnFILE.UseVisualStyleBackColor = true;
            this.btnFILE.Click += new System.EventHandler(this.btnFILE_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TBS);
            this.tabControl1.Controls.Add(this.TBSITE);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(302, 350);
            this.tabControl1.TabIndex = 3;
            // 
            // TBS
            // 
            this.TBS.BackgroundImage = global::POG_BR.Properties.Resources.fundo_1_800x600;
            this.TBS.Controls.Add(this.txtProcess);
            this.TBS.Controls.Add(this.btnFILE);
            this.TBS.Controls.Add(this.txtCommand);
            this.TBS.Controls.Add(this.button1);
            this.TBS.Controls.Add(this.txtURL);
            this.TBS.Location = new System.Drawing.Point(4, 22);
            this.TBS.Name = "TBS";
            this.TBS.Padding = new System.Windows.Forms.Padding(3);
            this.TBS.Size = new System.Drawing.Size(294, 324);
            this.TBS.TabIndex = 0;
            this.TBS.Text = "Softwares";
            this.TBS.UseVisualStyleBackColor = true;
            // 
            // TBSITE
            // 
            this.TBSITE.Controls.Add(this.btnConfirmar);
            this.TBSITE.Controls.Add(this.txtURLSITE);
            this.TBSITE.Controls.Add(this.txtCommandSITE);
            this.TBSITE.Location = new System.Drawing.Point(4, 22);
            this.TBSITE.Name = "TBSITE";
            this.TBSITE.Padding = new System.Windows.Forms.Padding(3);
            this.TBSITE.Size = new System.Drawing.Size(294, 324);
            this.TBSITE.TabIndex = 1;
            this.TBSITE.Text = "Web Sites";
            this.TBSITE.UseVisualStyleBackColor = true;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(105, 233);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmar.TabIndex = 1;
            this.btnConfirmar.Text = "Cadastrar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // txtURLSITE
            // 
            this.txtURLSITE.Location = new System.Drawing.Point(72, 142);
            this.txtURLSITE.Name = "txtURLSITE";
            this.txtURLSITE.Size = new System.Drawing.Size(145, 20);
            this.txtURLSITE.TabIndex = 0;
            // 
            // txtCommandSITE
            // 
            this.txtCommandSITE.Location = new System.Drawing.Point(72, 63);
            this.txtCommandSITE.Name = "txtCommandSITE";
            this.txtCommandSITE.Size = new System.Drawing.Size(145, 20);
            this.txtCommandSITE.TabIndex = 0;
            // 
            // Insert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::POG_BR.Properties.Resources.fundo_1_800x600;
            this.ClientSize = new System.Drawing.Size(333, 381);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Insert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insert";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Insert_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Insert_MouseMove);
            this.tabControl1.ResumeLayout(false);
            this.TBS.ResumeLayout(false);
            this.TBS.PerformLayout();
            this.TBSITE.ResumeLayout(false);
            this.TBSITE.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog _File;
        private System.Windows.Forms.Button btnFILE;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TBS;
        private System.Windows.Forms.TabPage TBSITE;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.TextBox txtURLSITE;
        private System.Windows.Forms.TextBox txtCommandSITE;
    }
}