using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using BasicBackupClasses;

namespace Basic_Backup
{
    public partial class Form1 : Form
    {
        private static bool autorun;
        private static BackupTask myTask;

        public Form1(string[] args)
        {
            InitializeComponent();

            //create a new instance of a backup task
            myTask = new BackupTask();
            autorun = false;

            //bind to the event delegates to events on the form
            myTask.UpdateProgress += UpdateProgress;
            myTask.EndBackup += CloseProgram;

            //was there more than 1 argument passed when the program was run?
            if (args.Length > 1)
            {
                
                //is the last argument "autorun"?
                if (args[1] == "autorun")
                {
                    //if yes, then load the backup task configuration from the config file, use the first argument as the configuration name
                    myTask.loadConfig(args[0]);

                    //tell the background worker to do some work!
                    backgroundWorker1.RunWorkerAsync();

                    //set the auto run variable
                    autorun = true;
                    Application.Exit();
                }
            }
            //if 1 argument has been supplied
            else if (args.Length > 0)
            {
                //load the backup task configuration from the config file, use the first argument as the configuration name
                myTask.loadConfig(args[0]);

                //bind values to controls for editing and displaying
                bind_controls();
            }
            //no arguments supplied - close the application
            else
            {
                Application.Exit();
            }

        }

        /// <summary>
        /// Update the progress control.
        /// </summary>
        private void UpdateProgress(int ProgressPercentage)
        {
            pbBackupProgress.Value = ProgressPercentage;
        }

        private void CloseProgram()
        {
            Application.Exit();
        }

        /// <summary>
        /// This method binds all of the controls to properties in the object
        /// updating a value in a control automatically alters the value in the bound propery
        /// this all means the saveconfig method is able to take the values 
        /// from the objects and save them to the config file
        /// </summary>
        private void bind_controls()
        {

            cbBackupandReportWith.SelectedItem = "Stored Procedure";
            cbBackupTo.SelectedItem = "FTP Server";

            txtEmailAddress.DataBindings.Add("text", myTask, "emailaddress", false, DataSourceUpdateMode.OnPropertyChanged);
            txtEmailPassword.DataBindings.Add("text", myTask, "emailpassword", false, DataSourceUpdateMode.OnPropertyChanged);
            txtEmailServer.DataBindings.Add("text", myTask, "emailserver", false, DataSourceUpdateMode.OnPropertyChanged);
            txtEmailServerPort.DataBindings.Add("text", myTask, "emailserverport", false, DataSourceUpdateMode.OnPropertyChanged);
            txtEmailUsername.DataBindings.Add("text", myTask, "emailusername", false, DataSourceUpdateMode.OnPropertyChanged);

            cbBackupandReportWith.DataBindings.Add("SelectedItem", myTask, "backupandreportwith", false, DataSourceUpdateMode.OnPropertyChanged);

            cbBackupTo.DataBindings.Add("SelectedItem", myTask, "remotebackupto", false, DataSourceUpdateMode.OnPropertyChanged);

            txtFTPAddress.DataBindings.Add("text", myTask, "remoteftpaddress", false, DataSourceUpdateMode.OnPropertyChanged);
            txtFTPPassword.DataBindings.Add("text", myTask, "remoteftppassword", false, DataSourceUpdateMode.OnPropertyChanged);
            txtFTPUsername.DataBindings.Add("text", myTask, "remoteftpusername", false, DataSourceUpdateMode.OnPropertyChanged);

            txtRemotePath.DataBindings.Add("text", myTask, "remoteuncfolder", false, DataSourceUpdateMode.OnPropertyChanged);
            txtUNCPassword.DataBindings.Add("text", myTask, "remoteuncpassword", false, DataSourceUpdateMode.OnPropertyChanged);
            txtUNCUsername.DataBindings.Add("text", myTask, "remoteuncusername", false, DataSourceUpdateMode.OnPropertyChanged);

            txtIISPath.DataBindings.Add("text", myTask, "iismetabackfolder", false, DataSourceUpdateMode.OnPropertyChanged);

            txtSQLInstance.DataBindings.Add("text", myTask, "sqlinstance", false, DataSourceUpdateMode.OnPropertyChanged);

            txtLocalBackupLocation.DataBindings.Add("text", myTask, "localbackupfolder", false, DataSourceUpdateMode.OnPropertyChanged);
            txtLocalBackupName.DataBindings.Add("text", myTask, "backupname", false, DataSourceUpdateMode.OnPropertyChanged);
            txtLocalBackupPrefix.DataBindings.Add("text", myTask, "localbackupprefix", false, DataSourceUpdateMode.OnPropertyChanged);

            dgvDatabases.DataSource = myTask.getdatabasebackups();
            dgvDatabases.Columns["Backup"].DataPropertyName = "backup";
            dgvDatabases.Columns["Backup"].HeaderText = "Backup";
            dgvDatabases.Columns["Backup"].Width = 45;
            dgvDatabases.Columns["DatabaseName"].DataPropertyName = "databasename";
            dgvDatabases.Columns["DatabaseName"].HeaderText = "Database Name";
            dgvDatabases.Columns["DatabaseName"].Width = 150;
            dgvDatabases.Columns["DatabaseName"].ReadOnly = true;
            dgvDatabases.Columns["Threshold"].DataPropertyName = "threshold";
            dgvDatabases.Columns["Threshold"].HeaderText = "Alert Threshold (MB)";
            dgvDatabases.Columns["Threshold"].Width = 66;
            dgvDatabases.Columns["Rebuild"].DataPropertyName = "rebuild";
            dgvDatabases.Columns["Rebuild"].HeaderText = "Rebuild";
            dgvDatabases.Columns["Rebuild"].Width = 50;
            dgvDatabases.Columns["Reindex"].DataPropertyName = "reindex";
            dgvDatabases.Columns["Reindex"].HeaderText = "Reindex";
            dgvDatabases.Columns["Reindex"].Width = 50;
            dgvDatabases.Columns["IndexFillFactor"].DataPropertyName = "indexfillfactor";
            dgvDatabases.Columns["IndexFillFactor"].HeaderText = "Fill Factor";
            dgvDatabases.Columns["IndexFillFactor"].Width = 45;
            dgvDatabases.Columns["Shrink"].DataPropertyName = "shrink";
            dgvDatabases.Columns["Shrink"].HeaderText = "Shrink";
            dgvDatabases.Columns["Shrink"].Width = 45;

            dgvFolderBackups.DataSource = myTask.getfolderbackups();
            dgvFolderBackups.Columns["FolderName"].DataPropertyName = "foldername";
            dgvFolderBackups.Columns["FolderName"].HeaderText = "Folder Name";
            dgvFolderBackups.Columns["FolderName"].Width = 268;
            dgvFolderBackups.Columns["FilterType"].DataPropertyName = "filtertype";
            dgvFolderBackups.Columns["FilterType"].HeaderText = "Filter Type";
            dgvFolderBackups.Columns["FilterType"].Width = 100;
            dgvFolderBackups.Columns["Extensions"].DataPropertyName = "extensions";
            dgvFolderBackups.Columns["Extensions"].HeaderText = "Extensions";
            dgvFolderBackups.Columns["Extensions"].Width = 100;
            dgvFolderBackups.AllowUserToAddRows = true;
            dgvFolderBackups.AllowUserToDeleteRows = true;

            dgvDriveSpace.DataSource = myTask.getdrives();
            dgvDriveSpace.Columns["driveletter"].DataPropertyName = "driveletter";
            dgvDriveSpace.Columns["driveletter"].ReadOnly = true;
            dgvDriveSpace.Columns["driveletter"].HeaderText = "Drive Letter";
            dgvDriveSpace.Columns["thresholdsize"].DataPropertyName = "thresholdsize";
            dgvDriveSpace.Columns["thresholdsize"].HeaderText = "Free Space Threshold (MB)";
            dgvDriveSpace.Columns["thresholdsize"].Width = 200;

            cbDatabases.Items.Add("- SELECT DATABASE -");
            cbDatabases.SelectedItem = "- SELECT DATABASE -";

            foreach (BackupDatabase dbase in myTask.getdatabasebackups())
            {
                cbDatabases.Items.Add(dbase.databasename);
            }

        }

        /// <summary>
        /// When the "Run Now" button is clicked, the background worker is told to do some work.
        /// </summary>
        private void btnRunNow_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        /// <summary>
        /// Save values in the objects to the config file
        /// </summary>
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (myTask.saveConfig())
            {
                MessageBox.Show("Saved!");
            }
            else
            {
                MessageBox.Show("Error trying to save - try again");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// When the list databases button is clicked, this method will get a list of databases.
        /// from the server instance and then rebuilds the list of database Backupdatabase objects
        /// </summary>
        private void btnListDatabases_Click(object sender, EventArgs e)
        {

            SqlConnection sqlcon = new SqlConnection();
            SqlCommand sqlcom;
            SqlDataReader reader;

            //connect to the instance's master database
            sqlcon.ConnectionString = "Server=" + txtSQLInstance.Text + ";Database=master;Trusted_Connection=True;";

            try
            {
                sqlcon.Open();
            }
            catch
            {
                MessageBox.Show("Cannot Connect to SQL Instance");
                myTask.debuglog("Cannot Connect to SQL Instance: " + txtSQLInstance.Text);
                return;
            }

            //if we're using stored procedures to run maintenance on the server
            if (cbBackupandReportWith.SelectedText == "Stored Procedure" && txtSQLInstance.Text != "")
            {

                try
                {
                    //use the SQL Server built-in stored procedure to return the database list
                    sqlcom = new SqlCommand("EXEC sp_databases", sqlcon);
                    reader = sqlcom.ExecuteReader();
                }
                catch
                {
                    sqlcon.Close();
                    sqlcon.Dispose();

                    MessageBox.Show("Cannot Get Databases");
                    myTask.debuglog("Cannot Get Databases");
                    return;
                }

            }
            //if we're using inline quries for database maintenance
            else
            {

                try
                {
                    //run the query to return the database list
                    sqlcom = new SqlCommand("SELECT name FROM master..sysdatabases ORDER BY name;", sqlcon);
                    reader = sqlcom.ExecuteReader();
                }
                catch
                {
                    sqlcon.Close();
                    sqlcon.Dispose();

                    MessageBox.Show("Cannot Get Databases");
                    myTask.debuglog("Cannot Get Databases");
                    return;
                }

            }

            //clear out the databases in the list control
            dgvDatabases.Rows.Clear();
            dgvDatabases.DataBindings.Clear();

            //also clear out the list of BackDatabase objects
            myTask.cleardatabases();

            //also clear out the list of databases from the dropdown control (for custom database queries/SPs)
            cbDatabases.Items.Clear();
            cbDatabases.Items.Add("- SELECT DATABASE -");
            cbDatabases.SelectedItem = "- SELECT DATABASE -";

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //create a new BackupDatabase object for each database returned (with maintenance default options)
                    BackupDatabase dbase = new BackupDatabase(reader[0].ToString(), false, false, 0, false, 0, false);

                    //add the object to the list
                    myTask.adddatabasebackup(dbase);

                    //also add the database name to the dropdown list 
                    cbDatabases.Items.Add(reader[0].ToString());
                }
            }

            //now bind the datagrid to the backupdatabase list
            dgvDatabases.DataSource = myTask.getdatabasebackups();

            //clean up
            reader.Close();
            reader.Dispose();
            sqlcom.Dispose();
            sqlcon.Close();
            sqlcon.Dispose();

        }

        /// <summary>
        /// In the "databse custom" tab, when a database is selected from the dropdown and the select database 
        /// button is clicked, this method will list all of the custom commands for the selected database
        /// </summary>
        private void btnSelectDatabase_Click(object sender, EventArgs e)
        {
            //find the backupdatabase object for the selected database
            BackupDatabase dbase = myTask.getdatabasebackup(cbDatabases.SelectedItem.ToString());

            //if one was found
            if (dbase != null)
            {

                //bind the databaseextraquery objects to the datagrid
                dgvDatabaseCustom.DataSource = dbase.GetExtraQueries();
                dgvDatabaseCustom.Columns["execute"].HeaderText = "Execute";
                dgvDatabaseCustom.Columns["query"].HeaderText = "Query";
                dgvDatabaseCustom.Columns["query"].Width = 368;
                dgvDatabaseCustom.AllowUserToAddRows = true;

                //BindingList<DatabaseExtraQuery> newExtraQuery = dgvDatabaseCustom.DataSource as BindingList<DatabaseExtraQuery>;
            }
        }

        /// <summary>
        /// One the "drive space" tab whent he "clear and list drives" button is clicked, 
        /// this method will clear the datagrid and rebuild the list of BackDrive objects
        /// </summary>
        private void btnClearAndListDrives_Click(object sender, EventArgs e)
        {

            //clear the datagrid and the backupdrive objects
            dgvDriveSpace.Rows.Clear();
            myTask.cleardrives();

            //get a list of the drives
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            //loop through all of the returned drives
            foreach (DriveInfo d in allDrives)
            {
                //if the drive is live
                if (d.IsReady)
                {
                    //create a backupdrive object
                    BackupDrive newDrive = new BackupDrive();
                    
                    //get the drive letter from name
                    newDrive.driveletter = d.Name.Substring(0, 1).ToCharArray()[0];
                    
                    //calculate a default 10% threshold
                    string tenpercent = ((((double.Parse(d.TotalSize.ToString()) / double.Parse("1024")) / double.Parse("1024")) / double.Parse("100")) * double.Parse("5")).ToString();
                    
                    //set the threshold and add to the list
                    newDrive.thresholdsize = long.Parse(tenpercent.Remove(tenpercent.IndexOf('.')));
                    myTask.adddrive(newDrive);
                }
            }

            //bind the gridview to the object list
            dgvDriveSpace.DataSource = myTask.getdrives();

        }

        private void btnReload_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //when the backgroup worker is run, then the "runnow" method in the backuptask object
            myTask.runnow(autorun);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pbBackupProgress_Click(object sender, EventArgs e)
        {

        }

    }
}
