namespace Basic_Backup
{
    partial class Form1
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
            this.tcBackupConfig = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbBackupTo = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbBackupandReportWith = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtIISPath = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtLocalBackupLocation = new System.Windows.Forms.TextBox();
            this.txtLocalBackupPrefix = new System.Windows.Forms.TextBox();
            this.txtLocalBackupName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtBackupEmailPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBackupEmailUsername = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBackupMailServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBackupEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUNCPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUNCUsername = new System.Windows.Forms.TextBox();
            this.txtFTPPassword = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtFTPUsername = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtFTPAddress = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRemotePath = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtEmailServerPort = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtEmailPassword = new System.Windows.Forms.TextBox();
            this.txtEmailUsername = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtEmailServer = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtEmailAddress = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvFolderBackups = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnListDatabases = new System.Windows.Forms.Button();
            this.dgvDatabases = new System.Windows.Forms.DataGridView();
            this.label22 = new System.Windows.Forms.Label();
            this.txtSQLInstance = new System.Windows.Forms.TextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.btnSelectDatabase = new System.Windows.Forms.Button();
            this.cbDatabases = new System.Windows.Forms.ComboBox();
            this.dgvDatabaseCustom = new System.Windows.Forms.DataGridView();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.btnClearAndListDrives = new System.Windows.Forms.Button();
            this.dgvDriveSpace = new System.Windows.Forms.DataGridView();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnRunNow = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnReload = new System.Windows.Forms.Button();
            this.pbBackupProgress = new System.Windows.Forms.ProgressBar();
            this.tcBackupConfig.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFolderBackups)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatabases)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatabaseCustom)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDriveSpace)).BeginInit();
            this.SuspendLayout();
            // 
            // tcBackupConfig
            // 
            this.tcBackupConfig.Controls.Add(this.tabPage1);
            this.tcBackupConfig.Controls.Add(this.tabPage2);
            this.tcBackupConfig.Controls.Add(this.tabPage3);
            this.tcBackupConfig.Controls.Add(this.tabPage4);
            this.tcBackupConfig.Controls.Add(this.tabPage5);
            this.tcBackupConfig.Controls.Add(this.tabPage6);
            this.tcBackupConfig.Controls.Add(this.tabPage7);
            this.tcBackupConfig.Location = new System.Drawing.Point(8, 8);
            this.tcBackupConfig.Name = "tcBackupConfig";
            this.tcBackupConfig.SelectedIndex = 0;
            this.tcBackupConfig.Size = new System.Drawing.Size(586, 216);
            this.tcBackupConfig.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbBackupTo);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.cbBackupandReportWith);
            this.tabPage1.Controls.Add(this.label28);
            this.tabPage1.Controls.Add(this.txtIISPath);
            this.tabPage1.Controls.Add(this.label24);
            this.tabPage1.Controls.Add(this.txtLocalBackupLocation);
            this.tabPage1.Controls.Add(this.txtLocalBackupPrefix);
            this.tabPage1.Controls.Add(this.txtLocalBackupName);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(578, 190);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Local Config";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cbBackupTo
            // 
            this.cbBackupTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBackupTo.FormattingEnabled = true;
            this.cbBackupTo.Items.AddRange(new object[] {
            "- None -",
            "Remote Network Storage",
            "FTP Server",
            "Email Address"});
            this.cbBackupTo.Location = new System.Drawing.Point(136, 160);
            this.cbBackupTo.Name = "cbBackupTo";
            this.cbBackupTo.Size = new System.Drawing.Size(436, 21);
            this.cbBackupTo.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 162);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Backup To";
            // 
            // cbBackupandReportWith
            // 
            this.cbBackupandReportWith.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBackupandReportWith.FormattingEnabled = true;
            this.cbBackupandReportWith.Items.AddRange(new object[] {
            "Stored Procedure",
            "Inline Queries"});
            this.cbBackupandReportWith.Location = new System.Drawing.Point(136, 96);
            this.cbBackupandReportWith.Name = "cbBackupandReportWith";
            this.cbBackupandReportWith.Size = new System.Drawing.Size(436, 21);
            this.cbBackupandReportWith.TabIndex = 9;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(8, 100);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(125, 13);
            this.label28.TabIndex = 8;
            this.label28.Text = "Backup and Report With";
            // 
            // txtIISPath
            // 
            this.txtIISPath.Location = new System.Drawing.Point(136, 129);
            this.txtIISPath.Name = "txtIISPath";
            this.txtIISPath.Size = new System.Drawing.Size(436, 20);
            this.txtIISPath.TabIndex = 7;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(8, 134);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(97, 13);
            this.label24.TabIndex = 6;
            this.label24.Text = "IIS MetaBack Path";
            // 
            // txtLocalBackupLocation
            // 
            this.txtLocalBackupLocation.Location = new System.Drawing.Point(136, 65);
            this.txtLocalBackupLocation.Name = "txtLocalBackupLocation";
            this.txtLocalBackupLocation.Size = new System.Drawing.Size(436, 20);
            this.txtLocalBackupLocation.TabIndex = 5;
            // 
            // txtLocalBackupPrefix
            // 
            this.txtLocalBackupPrefix.Location = new System.Drawing.Point(136, 36);
            this.txtLocalBackupPrefix.Name = "txtLocalBackupPrefix";
            this.txtLocalBackupPrefix.Size = new System.Drawing.Size(436, 20);
            this.txtLocalBackupPrefix.TabIndex = 4;
            // 
            // txtLocalBackupName
            // 
            this.txtLocalBackupName.Location = new System.Drawing.Point(136, 8);
            this.txtLocalBackupName.Name = "txtLocalBackupName";
            this.txtLocalBackupName.ReadOnly = true;
            this.txtLocalBackupName.Size = new System.Drawing.Size(436, 20);
            this.txtLocalBackupName.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 68);
            this.label11.Name = "label11";
            this.label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label11.Size = new System.Drawing.Size(107, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Backup File Location";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Backup Prefix";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Backup Name";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtBackupEmailPassword);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.txtBackupEmailUsername);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtBackupMailServer);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txtBackupEmail);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.txtUNCPassword);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtUNCUsername);
            this.tabPage2.Controls.Add(this.txtFTPPassword);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.txtFTPUsername);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.txtFTPAddress);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.txtRemotePath);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(578, 190);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Remote Backup";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtBackupEmailPassword
            // 
            this.txtBackupEmailPassword.Location = new System.Drawing.Point(352, 160);
            this.txtBackupEmailPassword.Name = "txtBackupEmailPassword";
            this.txtBackupEmailPassword.Size = new System.Drawing.Size(216, 20);
            this.txtBackupEmailPassword.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(264, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Email Password";
            // 
            // txtBackupEmailUsername
            // 
            this.txtBackupEmailUsername.Location = new System.Drawing.Point(104, 160);
            this.txtBackupEmailUsername.Name = "txtBackupEmailUsername";
            this.txtBackupEmailUsername.Size = new System.Drawing.Size(152, 20);
            this.txtBackupEmailUsername.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Email Username";
            // 
            // txtBackupMailServer
            // 
            this.txtBackupMailServer.Location = new System.Drawing.Point(352, 130);
            this.txtBackupMailServer.Name = "txtBackupMailServer";
            this.txtBackupMailServer.Size = new System.Drawing.Size(216, 20);
            this.txtBackupMailServer.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(264, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Mail Server";
            // 
            // txtBackupEmail
            // 
            this.txtBackupEmail.Location = new System.Drawing.Point(104, 130);
            this.txtBackupEmail.Name = "txtBackupEmail";
            this.txtBackupEmail.Size = new System.Drawing.Size(152, 20);
            this.txtBackupEmail.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Email Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(264, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "UNC Password";
            // 
            // txtUNCPassword
            // 
            this.txtUNCPassword.Location = new System.Drawing.Point(352, 38);
            this.txtUNCPassword.Name = "txtUNCPassword";
            this.txtUNCPassword.Size = new System.Drawing.Size(216, 20);
            this.txtUNCPassword.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "UNC Username";
            // 
            // txtUNCUsername
            // 
            this.txtUNCUsername.Location = new System.Drawing.Point(104, 38);
            this.txtUNCUsername.Name = "txtUNCUsername";
            this.txtUNCUsername.Size = new System.Drawing.Size(152, 20);
            this.txtUNCUsername.TabIndex = 10;
            // 
            // txtFTPPassword
            // 
            this.txtFTPPassword.Location = new System.Drawing.Point(352, 98);
            this.txtFTPPassword.Name = "txtFTPPassword";
            this.txtFTPPassword.Size = new System.Drawing.Size(216, 20);
            this.txtFTPPassword.TabIndex = 9;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(264, 100);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(76, 13);
            this.label16.TabIndex = 8;
            this.label16.Text = "FTP Password";
            // 
            // txtFTPUsername
            // 
            this.txtFTPUsername.Location = new System.Drawing.Point(104, 98);
            this.txtFTPUsername.Name = "txtFTPUsername";
            this.txtFTPUsername.Size = new System.Drawing.Size(152, 20);
            this.txtFTPUsername.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 98);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "FTP Username";
            // 
            // txtFTPAddress
            // 
            this.txtFTPAddress.Location = new System.Drawing.Point(104, 68);
            this.txtFTPAddress.Name = "txtFTPAddress";
            this.txtFTPAddress.Size = new System.Drawing.Size(464, 20);
            this.txtFTPAddress.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 72);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "FTP Address";
            // 
            // txtRemotePath
            // 
            this.txtRemotePath.Location = new System.Drawing.Point(104, 8);
            this.txtRemotePath.Name = "txtRemotePath";
            this.txtRemotePath.Size = new System.Drawing.Size(464, 20);
            this.txtRemotePath.TabIndex = 3;
            this.txtRemotePath.Text = "\\\\";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 11);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "UNC File Path";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtEmailServerPort);
            this.tabPage3.Controls.Add(this.label23);
            this.tabPage3.Controls.Add(this.txtEmailPassword);
            this.tabPage3.Controls.Add(this.txtEmailUsername);
            this.tabPage3.Controls.Add(this.label20);
            this.tabPage3.Controls.Add(this.label19);
            this.tabPage3.Controls.Add(this.txtEmailServer);
            this.tabPage3.Controls.Add(this.label18);
            this.tabPage3.Controls.Add(this.txtEmailAddress);
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(578, 190);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Email Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtEmailServerPort
            // 
            this.txtEmailServerPort.Location = new System.Drawing.Point(168, 136);
            this.txtEmailServerPort.Name = "txtEmailServerPort";
            this.txtEmailServerPort.Size = new System.Drawing.Size(403, 20);
            this.txtEmailServerPort.TabIndex = 11;
            this.txtEmailServerPort.Text = "25";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(8, 136);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(88, 13);
            this.label23.TabIndex = 10;
            this.label23.Text = "Email Server Port";
            // 
            // txtEmailPassword
            // 
            this.txtEmailPassword.Location = new System.Drawing.Point(168, 104);
            this.txtEmailPassword.Name = "txtEmailPassword";
            this.txtEmailPassword.Size = new System.Drawing.Size(403, 20);
            this.txtEmailPassword.TabIndex = 7;
            // 
            // txtEmailUsername
            // 
            this.txtEmailUsername.Location = new System.Drawing.Point(168, 72);
            this.txtEmailUsername.Name = "txtEmailUsername";
            this.txtEmailUsername.Size = new System.Drawing.Size(403, 20);
            this.txtEmailUsername.TabIndex = 6;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 104);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(81, 13);
            this.label20.TabIndex = 5;
            this.label20.Text = "Email Password";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 72);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(83, 13);
            this.label19.TabIndex = 4;
            this.label19.Text = "Email Username";
            // 
            // txtEmailServer
            // 
            this.txtEmailServer.Location = new System.Drawing.Point(168, 40);
            this.txtEmailServer.Name = "txtEmailServer";
            this.txtEmailServer.Size = new System.Drawing.Size(403, 20);
            this.txtEmailServer.TabIndex = 3;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 40);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 13);
            this.label18.TabIndex = 2;
            this.label18.Text = "Email Server";
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Location = new System.Drawing.Point(168, 8);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Size = new System.Drawing.Size(403, 20);
            this.txtEmailAddress.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 8);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(73, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Email Address";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvFolderBackups);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(578, 190);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "File Backups";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvFolderBackups
            // 
            this.dgvFolderBackups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFolderBackups.Location = new System.Drawing.Point(8, 8);
            this.dgvFolderBackups.Name = "dgvFolderBackups";
            this.dgvFolderBackups.Size = new System.Drawing.Size(564, 176);
            this.dgvFolderBackups.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.btnListDatabases);
            this.tabPage5.Controls.Add(this.dgvDatabases);
            this.tabPage5.Controls.Add(this.label22);
            this.tabPage5.Controls.Add(this.txtSQLInstance);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(578, 190);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Database Backups";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btnListDatabases
            // 
            this.btnListDatabases.Location = new System.Drawing.Point(408, 4);
            this.btnListDatabases.Name = "btnListDatabases";
            this.btnListDatabases.Size = new System.Drawing.Size(164, 23);
            this.btnListDatabases.TabIndex = 8;
            this.btnListDatabases.Text = "Clear and Re-List Databases";
            this.btnListDatabases.UseVisualStyleBackColor = true;
            this.btnListDatabases.Click += new System.EventHandler(this.btnListDatabases_Click);
            // 
            // dgvDatabases
            // 
            this.dgvDatabases.AllowUserToAddRows = false;
            this.dgvDatabases.AllowUserToDeleteRows = false;
            this.dgvDatabases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatabases.Location = new System.Drawing.Point(8, 32);
            this.dgvDatabases.Name = "dgvDatabases";
            this.dgvDatabases.Size = new System.Drawing.Size(564, 152);
            this.dgvDatabases.TabIndex = 7;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(8, 8);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(75, 13);
            this.label22.TabIndex = 6;
            this.label22.Text = "SQL Instance:";
            // 
            // txtSQLInstance
            // 
            this.txtSQLInstance.Location = new System.Drawing.Point(104, 6);
            this.txtSQLInstance.Name = "txtSQLInstance";
            this.txtSQLInstance.Size = new System.Drawing.Size(298, 20);
            this.txtSQLInstance.TabIndex = 5;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.btnSelectDatabase);
            this.tabPage6.Controls.Add(this.cbDatabases);
            this.tabPage6.Controls.Add(this.dgvDatabaseCustom);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(578, 190);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Database Custom";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // btnSelectDatabase
            // 
            this.btnSelectDatabase.Location = new System.Drawing.Point(409, 4);
            this.btnSelectDatabase.Name = "btnSelectDatabase";
            this.btnSelectDatabase.Size = new System.Drawing.Size(163, 23);
            this.btnSelectDatabase.TabIndex = 10;
            this.btnSelectDatabase.Text = "Select Database";
            this.btnSelectDatabase.UseVisualStyleBackColor = true;
            this.btnSelectDatabase.Click += new System.EventHandler(this.btnSelectDatabase_Click);
            // 
            // cbDatabases
            // 
            this.cbDatabases.FormattingEnabled = true;
            this.cbDatabases.Location = new System.Drawing.Point(11, 6);
            this.cbDatabases.Name = "cbDatabases";
            this.cbDatabases.Size = new System.Drawing.Size(392, 21);
            this.cbDatabases.TabIndex = 9;
            // 
            // dgvDatabaseCustom
            // 
            this.dgvDatabaseCustom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatabaseCustom.Location = new System.Drawing.Point(8, 32);
            this.dgvDatabaseCustom.Name = "dgvDatabaseCustom";
            this.dgvDatabaseCustom.Size = new System.Drawing.Size(564, 152);
            this.dgvDatabaseCustom.TabIndex = 8;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.btnClearAndListDrives);
            this.tabPage7.Controls.Add(this.dgvDriveSpace);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(578, 190);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Drive Space";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // btnClearAndListDrives
            // 
            this.btnClearAndListDrives.Location = new System.Drawing.Point(8, 6);
            this.btnClearAndListDrives.Name = "btnClearAndListDrives";
            this.btnClearAndListDrives.Size = new System.Drawing.Size(564, 28);
            this.btnClearAndListDrives.TabIndex = 9;
            this.btnClearAndListDrives.Text = "Clear and List Drives";
            this.btnClearAndListDrives.UseVisualStyleBackColor = true;
            this.btnClearAndListDrives.Click += new System.EventHandler(this.btnClearAndListDrives_Click);
            // 
            // dgvDriveSpace
            // 
            this.dgvDriveSpace.AllowUserToAddRows = false;
            this.dgvDriveSpace.AllowUserToDeleteRows = false;
            this.dgvDriveSpace.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDriveSpace.Location = new System.Drawing.Point(8, 35);
            this.dgvDriveSpace.Name = "dgvDriveSpace";
            this.dgvDriveSpace.Size = new System.Drawing.Size(564, 149);
            this.dgvDriveSpace.TabIndex = 8;
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Location = new System.Drawing.Point(315, 230);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(131, 23);
            this.btnSaveChanges.TabIndex = 18;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnRunNow
            // 
            this.btnRunNow.Location = new System.Drawing.Point(8, 230);
            this.btnRunNow.Name = "btnRunNow";
            this.btnRunNow.Size = new System.Drawing.Size(146, 23);
            this.btnRunNow.TabIndex = 17;
            this.btnRunNow.Text = "Run Now";
            this.btnRunNow.UseVisualStyleBackColor = true;
            this.btnRunNow.Click += new System.EventHandler(this.btnRunNow_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(452, 230);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(138, 24);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(160, 230);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(149, 23);
            this.btnReload.TabIndex = 20;
            this.btnReload.Text = "Reload Config";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // pbBackupProgress
            // 
            this.pbBackupProgress.Location = new System.Drawing.Point(8, 259);
            this.pbBackupProgress.Name = "pbBackupProgress";
            this.pbBackupProgress.Size = new System.Drawing.Size(582, 23);
            this.pbBackupProgress.TabIndex = 21;
            this.pbBackupProgress.Click += new System.EventHandler(this.pbBackupProgress_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 288);
            this.Controls.Add(this.pbBackupProgress);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.btnRunNow);
            this.Controls.Add(this.tcBackupConfig);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tcBackupConfig.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFolderBackups)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatabases)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatabaseCustom)).EndInit();
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDriveSpace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcBackupConfig;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cbBackupandReportWith;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtIISPath;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtLocalBackupLocation;
        private System.Windows.Forms.TextBox txtLocalBackupPrefix;
        private System.Windows.Forms.TextBox txtLocalBackupName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtFTPPassword;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtFTPUsername;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtFTPAddress;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtRemotePath;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtEmailServerPort;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtEmailPassword;
        private System.Windows.Forms.TextBox txtEmailUsername;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtEmailServer;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtEmailAddress;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgvFolderBackups;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnListDatabases;
        private System.Windows.Forms.DataGridView dgvDatabases;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtSQLInstance;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataGridView dgvDatabaseCustom;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnRunNow;
        private System.Windows.Forms.Button btnClose;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnSelectDatabase;
        private System.Windows.Forms.ComboBox cbDatabases;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnClearAndListDrives;
        private System.Windows.Forms.DataGridView dgvDriveSpace;
        private System.Windows.Forms.ComboBox cbBackupTo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUNCPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUNCUsername;
        private System.Windows.Forms.TextBox txtBackupEmailPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBackupEmailUsername;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBackupMailServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBackupEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar pbBackupProgress;
    }
}

