using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.DirectoryServices;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Ionic.Zip;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;


namespace BasicBackupClasses
{
    
    /// <summary>
    /// Disables file system aliasing for 32 bit applications
    /// on 64 bit systems.
    /// </summary>
    public class DisableWow64Redirect : IDisposable {
      #region P/invoke
      [DllImport("Kernel32")]
      private static extern bool Wow64DisableWow64FsRedirection(out IntPtr oldValue);

      [DllImport("Kernel32")]
      private static extern bool Wow64RevertWow64FsRedirection(IntPtr oldValue);
     #endregion
    
    private readonly IntPtr oldValue;
    
    /// <summary>
    /// Creating a new object disables file system aliasing for the current thread.
    /// </summary>
    /// <remarks>
    /// Use <see cref="Dispose"/> to re-enable file system aliasing.
    /// </remarks>
    public DisableWow64Redirect() {
        Success = Wow64DisableWow64FsRedirection(out oldValue);
    }
    
    public bool Success { get; private set; }
    
        /// <summary>
        /// Disposes this object and reenables the file system aliasing.
        /// </summary>
        public void Dispose() {
            if (Success) {
                Success = Wow64RevertWow64FsRedirection(oldValue);
            }
        }
    }

    /// <summary>
    /// This is the main class which stores all of the maintenance details for a particular backup task, this includes
    /// the database backup options, folder backup options, email settings, backup location setting etc...
    /// </summary>
    class BackupTask
    {
        public string backupname { get; set; }
        public string localbackupprefix { get; set; }
        public string localbackupfolder { get; set; }
        public string iismetabackfolder { get; set; }
        public string remotebackupto { get; set; }
        public string remoteuncfolder { get; set; }
        public string remoteuncusername { get; set; }
        public string remoteuncpassword { get; set; }
        public string remoteftpaddress { get; set; }
        public string remoteftpusername { get; set; }
        public string remoteftppassword { get; set; }
        public string emailaddress { get; set; }
        public string emailserver { get; set; }
        public string emailusername { get; set; }
        public string emailpassword { get; set; }
        public string emailserverport { get; set; }
        public string sqlinstance { get; set; }
        private string _configfilename;

        private BindingList<BackupFolder> folders;
        private BindingList<BackupDatabase> databases;
        private BindingList<BackupDrive> drives;
        private string _backupandreportwith;
        private XmlDocument fullconfigdata;
        private XmlNode backupnode;

        //event delegates for updating controls on the UI
        internal delegate void UpdateProgressDelegate(int ProgressPercentage);
        internal event UpdateProgressDelegate UpdateProgress;

        internal delegate void EndBackupDelegate();
        internal event EndBackupDelegate EndBackup;

        public string backupandreportwith { 
            get {
                return _backupandreportwith;
            }
            set
            {
                if (value == "Stored Procedure" || value == "Inline Queries")
                {
                    _backupandreportwith = value;
                }
                else
                {
                    backupandreportwith = "Inline Queries";
                }
            }
        }

        public BackupTask()
        {
            folders = new BindingList<BackupFolder>();
            databases = new BindingList<BackupDatabase>();
            drives = new BindingList<BackupDrive>();
            fullconfigdata = new XmlDocument();
        }

        private BackupFolder getfolderbackup(string folderpath)
        {
            //arent Lambda expressions just beautiful
            BackupFolder item = folders.SingleOrDefault(folderbackup => folderbackup != null && folderbackup.foldername == folderpath);
            if (item != null)
            {
                return item;
            }
            else
            {
                return null;
            }
        }

        private BackupFolder getfolderbackup(int index)
        {
            return folders[index];
        }

        public void addfolderbackup(BackupFolder folder)
        {
            folders.Add(folder);
        }

        public void adddrive(BackupDrive drive)
        {
            drives.Add(drive);
        }

        private void addfolderbackup(string folderpath, string filtertype, string extensions)
        {
            BackupFolder folder = new BackupFolder(folderpath, filtertype, extensions);
            folders.Add(folder);
            folder = null;
        }

        private BackupDrive getbackupdrive(char letter)
        {

            //arent Lambda expressions just beautiful
            BackupDrive item = drives.SingleOrDefault(drivebackup => drivebackup != null && drivebackup.driveletter == letter);
            if (item != null)
            {
                return item;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// This method searches for a BackupDatabase object for the supplied database name, and returns the object if one is found
        /// </summary>        
        public BackupDatabase getdatabasebackup(string databasename)
        {
            BackupDatabase item = databases.SingleOrDefault(databasebackup => databasebackup != null && databasebackup.databasename == databasename);
            if (item != null)
            {
                return item;
            }
            else
            {
                return null;
            }
        }

        public BackupDatabase getdatabasebackup(int index)
        {
            return databases[index];
        }

        public void adddatabasebackup(BackupDatabase dbbackup)
        {
            databases.Add(dbbackup);
        }

        public void cleardatabases()
        {
            databases.Clear();
        }

        public void cleardrives()
        {
            drives.Clear();
        }

        public BindingList<BackupFolder> getfolderbackups()
        {
            return folders;
        }

        public BindingList<BackupDatabase> getdatabasebackups()
        {
            return databases;
        }

        public BindingList<BackupDrive> getdrives()
        {
            return drives;
        }

        /// <summary>
        /// This method "cleans" a folder by removing all files and sub-folders
        /// </summary>
        private static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            //loop through and delete all files
            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            //loop through and delete all folders
            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

        }

        /// <summary>
        /// This is the main task method, this is the method that is called from the background worker on the UI.
        /// It loops through all of the tasks set for a particular backup configuration and executes them, generating a report 
        /// as it goes.
        /// 
        /// The flow of this method is as follows:
        /// > Clean local backup folder
        /// > Create the sub folders for backing up the items asked to be backed up (e.g. \databases  \iis etc...)
        /// > Invoke a backup of the IIS metabase
        /// > Copy the metabase backup files
        /// > Copy all files and folders to backup
        /// > Backup each database selected
        /// > Run the database maintenance task for each database
        /// > Run the custom queries configured for databases
        /// > Zip up all of the files in the local backup folder
        /// > Check drive thresholds
        /// > Copy the backup zip to the configured location
        /// > Send the email report
        /// </summary>
        public void runnow(Boolean autorun)
        {

            //set the progress percentage to 0
            UpdateProgress(0);

            string strEmailText = "";
            int errorcount=0;

            //try to clean the local folder where backups as stored - so we can store a fresh backup
            try
            {
                DeleteDirectory(localbackupfolder);
            }
            catch (Exception e)
            {
                debuglog(e.Message + e.InnerException);
            }

            //create the local backup folder
            try
            {
                Directory.CreateDirectory(localbackupfolder);
            }
            catch (Exception e)
            {
                debuglog(e.Message + e.InnerException);
            }

            //create sub folders for backups
            if (!Directory.Exists(localbackupfolder + "\\folders"))
            {
                Directory.CreateDirectory(localbackupfolder + "\\folders");
            }

            if (!Directory.Exists(localbackupfolder + "\\iis"))
            {
                Directory.CreateDirectory(localbackupfolder + "\\iis");
            }

            if (!Directory.Exists(localbackupfolder + "\\databases"))
            {
                Directory.CreateDirectory(localbackupfolder + "\\databases");
            }

            UpdateProgress(20);

            //if the backup tasks needs to backup the IIS metabase
            if(iismetabackfolder!="") {

                //invoke a IIS metabase backup
                try
                {
                    debuglog("Attempting to backup IIS MetaBase");

                    DirectoryEntry de = new DirectoryEntry("IIS://localhost");
                    de.Invoke("Backup", new object[]
				                        {
				                    	    "bbbackup",  //name of the backup
				                    	    0xffffffff,  //MD_BACKUP_NEXT_VERSION
				                    	    0x00000002 | 0x00000004   //MD_BACKUP_SAVE_FIRST (Saves the metabase prior to making the backup.)
                                                                      //MD_BACKUP_FORCE_BACKUP (if you want the backup to proceed even if the save fails.)
				                        });

                    de.Close();
                    de.Dispose();

                    //the emailtext variable stores the body text which will be sent in the report
                    strEmailText += "INFO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IIS Metabase Backup Complete&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OK<br />";
                    debuglog("Backup of IIS MetaBase successful");

                }
                catch(Exception e)
                {
                    errorcount++;
                    //the emailtext variable stores the body text which will be sent in the report - so error information is important!
                    strEmailText += "ALERT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IIS Metabase Backup Fail&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + e.Message + e.InnerException + "<br />";
                    debuglog("Backup of IIS FAILED WITH ERROR " + e.Message + e.InnerException);
                }



                try {

                    debuglog("Disabling x64 Redirection");

                    //disable to 64bit folder, so we can access the IIS metabases with a 32bit app
                    DisableWow64Redirect disablew64 = new DisableWow64Redirect();

                    debuglog("Getting list of MD and SC files");

                    //get a list of all the metabase backup files
                    string[] mdfiles = Directory.GetFiles(iismetabackfolder, "bbackup.MD*", SearchOption.AllDirectories);
                    string[] scfiles = Directory.GetFiles(iismetabackfolder, "bbbackup.SC*", SearchOption.AllDirectories);

                    debuglog("Copying MD files");

                    foreach (string path in mdfiles)
                    {
                        // Remove path from the file name. 
                        string fName = path.Substring(iismetabackfolder.Length + 1);

                        // Use the Path.Combine method to safely append the file name to the path. 
                        // Will overwrite if the destination file already exists.
                        File.Copy(Path.Combine(iismetabackfolder, fName), Path.Combine(localbackupfolder + "\\iis", fName), true);
                    }

                    debuglog("Copying SC files");

                    foreach (string path in scfiles)
                    {
                        // Remove path from the file name. 
                        string fName = path.Substring(iismetabackfolder.Length + 1);

                        // Use the Path.Combine method to safely append the file name to the path. 
                        // Will overwrite if the destination file already exists.
                        File.Copy(Path.Combine(iismetabackfolder, fName), Path.Combine(localbackupfolder + "\\iis", fName), true);
                    }

                    debuglog("Enabling x64 Redirection");

                    disablew64.Dispose();

                    strEmailText += "INFO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IIS Copy COMPLETED&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OK<br /><br />";
                    debuglog("IIS Backup Complete");

                }
                catch(Exception e)
                {
                    errorcount++;
                    strEmailText += "ALERT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IIS Copy Fail<br /><br />";
                    debuglog("Backup of IIS FAILED WITH ERROR " + e.Message + e.InnerException);
                }

            }

            UpdateProgress(25);


            //backup all of the folders selected - excluding or including configured file types
            if(folders.Count>0)
            {

                 debuglog("Attempting to backup folders");

                foreach (BackupFolder item in folders)
                {

                    //get backup folder
                    string newfolder = localbackupfolder  + "\\folders\\" + item.foldername.Split('\\')[item.foldername.Split('\\').Length-1];
                
                    try
                    {
  
                        debuglog("STARTING FILE BACKUP");
                    
                        //create backup dir
                        Directory.CreateDirectory(newfolder);

                        debuglog("CREATING FOLDERS");

                        //copy folders
                        foreach (string dirPath in Directory.GetDirectories(item.foldername, "*", SearchOption.AllDirectories))
                            Directory.CreateDirectory(dirPath.Replace(item.foldername, newfolder));

                        debuglog("FOLDERS CREATED");

                    } catch {

                        errorcount++;
                        strEmailText += "ALERT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Subfolder Creation Folder&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + item.foldername + "<br />";
                        debuglog("FOLDERS CREATION FAILED");

                    }

                    try 
                    {

                        debuglog("Attempting to backup " + item.foldername + (item.filtertype == "include" ? " with " + "\\.(" + item.extensions.ToString().Replace(";", "|").Replace("*", "").Replace(".", "") + ")$" : " without " + "\\.(" + item.extensions.ToString().Replace(";", "|").Replace("*", "").Replace(".", "") + ")$"));

                        //Copy all the files
                        if (item.filtertype == "include")
                        {
                            //filter for included file extentions
                            Regex searchPattern = new Regex("\\.(" + item.extensions.ToString().Replace(";", "|").Replace("*", "").Replace(".", "") + ")$", RegexOptions.IgnoreCase);
                        
                            foreach (string newPath in Directory.GetFiles(item.foldername, "*", SearchOption.AllDirectories).Where(f => searchPattern.IsMatch(f)).ToList())
                                File.Copy(newPath, newPath.Replace(item.foldername, newfolder), true);
                        }
                        else
                        {
                            //filter for excluded file extentions
                            Regex searchPattern = new Regex("\\.(" + item.extensions.ToString().Replace(";", "|").Replace("*", "").Replace(".", "") + ")$", RegexOptions.IgnoreCase);
                        
                            foreach (string newPath in Directory.GetFiles(item.foldername, "*", SearchOption.AllDirectories).Where(f => !searchPattern.IsMatch(f)).ToList())
                                File.Copy(newPath, newPath.Replace(item.foldername, newfolder), true);
                        }

                        debuglog("Backup of " + item.foldername + " complete");

                    }
                    catch
                    {

                        errorcount++;
                        strEmailText += "ALERT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;File Copy Fail&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + item.foldername + "<br /><br />";
                        debuglog("Backup of " + item.foldername + " FAILED");

                    }

                }

                if(errorcount>0)
                {
                    strEmailText += "ALTER&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Files and Folders Copy Failed<br /><br />";
                    debuglog("FOLDERS AND FILES BACKUP COMPLETED");
                } else {
                    strEmailText += "INFO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Files and Folders Copied&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OK<br /><br />";
                    debuglog("FOLDERS AND FILES BACKUP COMPLETED");
                }

            }

            UpdateProgress(55);

            if(databases.Count>0) 
            {

                debuglog("STARTING DATABASE BACKUPS");

                /// <remarks>
                /// Open DB connection
                /// </remarks>
                /// 
                SqlConnection sqlcon = new SqlConnection();
                SqlCommand sqlcom;
                SqlDataReader reader;

                sqlcon.ConnectionString="Server=" + sqlinstance + ";Database=master;Trusted_Connection=True;";

                try 
                {

                    sqlcon.Open();

                } catch {

                    errorcount++;
                    strEmailText += "ALERT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Cannot connect to SQL Instance&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + sqlinstance + "<br /><br />";
                    debuglog("CANNOT CONNECT TO SQL INSTANCE " + sqlinstance);

                }

                /// <remarks>
                /// Open DB connection
                /// </remarks>




                /// <remarks>
                /// Loop through databases listed
                /// </remarks>
                foreach(BackupDatabase db in databases)
                {

                    //if the database has been ticked to be backed up
                    if(db.backup) {

                        try
                        {

                            debuglog("BACKUPING UP DATABASE: BACKUP DATABASE " + db.databasename + " TO DISK = N'" + localbackupfolder + "\\databases\\" + db.databasename + ".bak' WITH NO_COMPRESSION,INIT,NOUNLOAD,NOREWIND,SKIP,CHECKSUM;");

                            //run inline query to backup the database
                            sqlcom = new SqlCommand();
                            sqlcom.Connection = sqlcon;
                            sqlcom.CommandText = "BACKUP DATABASE " + db.databasename + " TO DISK = N'" + localbackupfolder + "\\databases\\" + db.databasename + ".bak' WITH NO_COMPRESSION,INIT,NOUNLOAD,NOREWIND,SKIP,CHECKSUM;";
                            sqlcom.ExecuteNonQuery();
                            sqlcom.Dispose();

                            strEmailText += "INFO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Datbase backup OK&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + db.databasename + "<br />";
                            debuglog("DATABASE BACKUP OK");

                        }
                        catch (Exception e)
                        {

                            errorcount++;
                            strEmailText += "ALERT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DATABASE BACKUP FAILED&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + db.databasename + "<br />";
                            debuglog("DATABASE BACKUP FAILED " + e.Message + e.InnerException);

                        }

                    }

                    //if any of the maintenance tasks have been set for the data
                    if (db.rebuild || db.reindex || db.shrink || db.threshold>0)
                    {

                        //if maintenance should be done with stored procs
                        if (backupandreportwith == "Stored Procedure")
                        {

                            debuglog("DATABASE MAINTENANCE STARTED");

                            try
                            {

                                //run the maintenance SP on the master database.
                                //the stored proc is stored on the master database, so that there is only 1 location to run the SP from and
                                //also only 1 location to update any changes.
                                sqlcom = new SqlCommand();
                                sqlcom.Connection = sqlcon;
                                sqlcom.CommandText = String.Format("USE master;EXEC sp_admin_dbmaintenance '{0}',{1},{2},{3},{4},{5}",
                                                            db.databasename.Replace("'", "''"), convertbooltostring(db.shrink), convertbooltostring(db.reindex),
                                                            convertbooltostring(db.rebuild), db.threshold, db.indexfillfactor);

                                debuglog("DATABASE MAINTENANCE " + String.Format("USE master;EXEC sp_admin_dbmaintenance '{0}',{1},{2},{3},{4},{5}",
                                                            db.databasename.Replace("'", "''"), convertbooltostring(db.shrink), convertbooltostring(db.reindex),
                                                            convertbooltostring(db.rebuild), db.threshold, db.indexfillfactor));

                                reader = sqlcom.ExecuteReader();

                                if (reader.HasRows)
                                {
                                    reader.Read();

                                    //if the stored procedure returns an OK message, all maintenance tasks must have been completed.
                                    if (reader["AlertStatus"].ToString() == "OK")
                                    {
                                        strEmailText += "INFO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Databas Maintenance Complete&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + db.databasename.Replace("'", "''") + "<br />";
                                        debuglog("DATABASE MAINTENANCE COMPLETED - DATABASE OK");
                                    }
                                    else
                                    {
                                        strEmailText += "INFO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Databas Maintenance Complete&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + db.databasename.Replace("'", "''") + "<br />";
                                        strEmailText += "ALERT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + reader["AlterStatus"].ToString() + "<br />";
                                        debuglog("DATABASE MAINTENANCE COMPLETED - DATABASE OK");
                                    }
                                }

                                reader.Close();
                                reader.Dispose();
                                sqlcom.Dispose();

                            }
                            catch (Exception e)
                            {

                                errorcount++;
                                strEmailText += "ALTER&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Database Maintenance Failed&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + db.databasename + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + e.Message + e.InnerException + "<br />";
                                debuglog("DATABASE MAINTENANCE FAILED " + e.Message + e.InnerException);

                            }

                        }
                        //if maintenance should be done with inline queries
                        else
                        {
                            /// <todo>
                            /// inline maintenance quries
                            /// </todo>
                        }

                    }

                    //if there are any custom queries configured for this database
                    if (db.databasecustomcommands.Count > 0)
                    {
                        foreach (DatabaseExtraQuery extraquery in db.GetExtraQueries())
                        {
                            if (extraquery.execute)
                            {

                                //run the custom query
                                sqlcom = new SqlCommand();
                                sqlcom.Connection = sqlcon;
                                sqlcom.CommandText = "USE " + db.databasename + ";EXEC " + extraquery.query;

                                debuglog("DATABASE EXTRA QUERY USE " + db.databasename + ";EXEC " + extraquery.query);

                                reader = sqlcom.ExecuteReader();
                                strEmailText += "<br />EXTRA QUERY (" + extraquery.query + ") DATA DUMP:<br /><br />";

                                /// <remarks>
                                /// if the custom query outputs any information, we'll want to add this to the report email
                                /// create a table by grabbing the field names returned and then 
                                /// </remarks>

                                bool rowheaders=true;

                                /// <remarks>
                                /// Start creating a table from the first resultset returned 
                                /// </remarks>
                                strEmailText += "<br /><br /><table style=\"border: 1px solid black\">";
                                while(reader.Read())
                                {

                                    //make sure we only grab the headers once
                                    if (rowheaders)
                                    {

                                        strEmailText += "<tr>";

                                        //grab results from first set
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            strEmailText += "<th>" + reader.GetName(i) + "</th>";
                                        }

                                        strEmailText += "</tr>";

                                    }
                                    rowheaders = false;


                                    strEmailText += "<tr>";

                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        strEmailText += "<td>" + reader[i].ToString() + "</td>";
                                    }

                                    strEmailText += "</tr>";
                                }

                                strEmailText += "</table>";

                                /// <remarks>
                                /// Now build up a table from the rest of the result sets 
                                /// </remarks>
                                rowheaders = true;
                                while (reader.NextResult())
                                {

                                    strEmailText += "<br /><br /><table style=\"border: 1px solid black\">";

                                    while (reader.Read())
                                    {

                                        //make sure we only grab the headers once
                                        if (rowheaders)
                                        {

                                            strEmailText += "<tr>";

                                            //grab results from first set
                                            for (int i = 0; i < reader.FieldCount; i++)
                                            {
                                                strEmailText += "<th>" + reader.GetName(i) + "</th>";
                                            }

                                            strEmailText += "</tr>";

                                        }
                                        rowheaders = false;


                                        strEmailText += "<tr>";

                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            strEmailText += "<td>" + reader[i].ToString() + "</td>";
                                        }

                                        strEmailText += "</tr>";
                                    }

                                    strEmailText += "</table>";

                                }

                                reader.Close();
                                reader.Dispose();
                                sqlcom.Dispose();

                            }

                        }

                    }

                }
                /// <remarks>
                /// Loop through databases listed
                /// </remarks>



                strEmailText += "<br />";



                /// <remarks>
                /// Close database connection
                /// </remarks>
               
                sqlcon.Close();
                sqlcon.Dispose();

                /// <remarks>
                /// Close database connection
                /// </remarks>

            }

            UpdateProgress(65);

            /// <remarks>
            /// Zip files which have been copied to local backup folder (all files - IIS backups, databases, folders etc..)
            /// </remarks>

            try
            {
                debuglog("STARTING ZIP OF FILES");

                //use the IconicZip library
                ZipFile zip = new ZipFile();
                zip.AddDirectory(localbackupfolder);
                zip.UseZip64WhenSaving = Zip64Option.AsNecessary;
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestSpeed;
                zip.CompressionMethod = CompressionMethod.Deflate;
                zip.Save(localbackupfolder + "\\" + backupname + localbackupprefix.Replace("{!Date}", DateTime.Now.ToShortDateString().Replace("/", "-")).Replace("{!DateTime}", DateTime.Now.ToString().Replace(":", "").Replace("/", "-")).Replace("AM", "").Replace("PM", "") + ".zip");

                strEmailText += "<br /><br />INFO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Zipping of files completed&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OK<br /><br />";
                debuglog("FOLDERS AND FILES ZIPPING COMPLETED");

            }
            catch(Exception e)
            {

                errorcount++;
                strEmailText += "<br /><br />ALERT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Zipping of files FAIL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FAIL<br /><br />";
                debuglog("FOLDERS AND FILES ZIPPING FAILED - " + e.Message.ToString());

            }



            UpdateProgress(80);



            /// <remarks>
            /// Loop through local drives and check their usage against the set thresholds
            /// </remarks>

            if (drives.Count > 0)
            {
                //cgrab all of the local drives
                DriveInfo[] allDrives = DriveInfo.GetDrives();

                //loop through each one
                foreach (DriveInfo d in allDrives)
                {
                    if (d.IsReady)
                    {

                        //find a BackupDrive object for this drive
                        BackupDrive driveitem = getbackupdrive(d.Name.Substring(0, 1).ToCharArray()[0]);

                        //if one is found
                        if (driveitem != null)
                        {

                            double spaceleft = ((double.Parse(d.AvailableFreeSpace.ToString()) / double.Parse("1024")) / double.Parse("1024"));
                            double totalsize = ((double.Parse(d.TotalSize.ToString()) / double.Parse("1024")) / double.Parse("1024"));

                            //check spaceleft against threshold and attach results to report email
                            if (spaceleft < driveitem.thresholdsize)
                            {
                                debuglog("Drive " + driveitem.driveletter + " OVER THRESHOLD less than " + spaceleft.ToString().Split('.')[0] + "MB left");
                                strEmailText += "ALERT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Drive " + driveitem.driveletter + " OVER THRESHOLD less than " + spaceleft.ToString().Split('.')[0] + "MB left<br />";
                            }
                            else
                            {
                                debuglog("Drive " + driveitem.driveletter + " has " + spaceleft.ToString().Split('.')[0] + "MB of " + totalsize.ToString().Split('.')[0] + "MB free space");
                                strEmailText += "INFO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Drive " + driveitem.driveletter + " has " + spaceleft.ToString().Split('.')[0] + "MB of " + totalsize.ToString().Split('.')[0] + "MB free space<br />";
                            }
                            break;

                        }

                    }
                }

            }

            UpdateProgress(90);


            /// <remarks>
            /// Now for the backing up part, check if an FTP server or UNC path has been configured
            /// </remarks>

            if((remotebackupto=="FTP Server"&&remoteftpaddress!="") || (remotebackupto=="Remote Network Storage"&&remoteuncfolder!="")) 
            {

                /// <remarks>
                /// Upload ZIP file to the FTP server
                /// </remarks>

                //transfer backup to remote
                if(remotebackupto=="FTP Server") 
                {


                    /// <remarks>
                    /// When backing up the FTP server, the program will:
                    /// > Delete old previous backup
                    /// > Rename the old backup to previous
                    /// > Copy up the new backup
                    /// </remarks>

                    //parse the username and domain from the username string
                    string domain="";
                    string username="";
                    if(remoteftpusername.Contains('\\')) {
                        username = remoteftpusername.Split('\\')[1];
                        domain = remoteftpusername.Split('\\')[0];
                    } else {
                        username = remoteftpusername;
                    }

                    try 
                    {
                        debuglog("DELETING OLD BACKUP ftp://" + remoteftpaddress + "/" + localbackupprefix + "backup_PREV.zip");

                        //delete the previous previous backup file
                        WebRequest webRequest = FtpWebRequest.Create("ftp://" + remoteftpaddress + "/" + localbackupprefix + "backup_PREV.zip");
                        webRequest.Credentials = new NetworkCredential(username, remoteftppassword, domain);
                        FtpWebRequest ftpRequest = (FtpWebRequest)webRequest;
                        ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                        FtpWebResponse ftpResponse = (FtpWebResponse)webRequest.GetResponse();
                        ftpResponse.Close();

                        debuglog("OLD FILE DELETED");

                    } 
                    catch(Exception e) {

                        errorcount++;
                        strEmailText += "ALERT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Deletion of old backup FAILEDL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FAIL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + e.Message + "<br />";
                        debuglog("OLD FILE FILE DELETION FAILED" + e.Message + e.InnerException);

                    }

                    try 
                    {
                        debuglog("RENAMING PREV BACKUP ftp://" + remoteftpaddress + "/" + localbackupprefix + "backup.zip");

                        //rename the previous backup to PREV
                        WebRequest webRequest = FtpWebRequest.Create("ftp://" + remoteftpaddress + "/" + localbackupprefix + "backup.zip");
                        webRequest.Credentials = new NetworkCredential(username, remoteftppassword, domain);
                        FtpWebRequest ftpRequest = (FtpWebRequest)webRequest;
                        ftpRequest.RenameTo = localbackupprefix + "backup_PREV.zip";
                        ftpRequest.Method = WebRequestMethods.Ftp.Rename;
                        FtpWebResponse ftpResponse = (FtpWebResponse)webRequest.GetResponse();
                        ftpResponse.Close();

                        debuglog("PREV BACKUP RENAMED");

                    } 
                    catch(Exception e) {

                        errorcount++;
                        strEmailText += "ALERT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rename of prev backup FAILEDL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FAIL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + e.Message + "<br />";
                        debuglog("PREV FILE RENAME FAILED " + e.Message + e.InnerException);
                    }

                    try 
                    {

                        debuglog("UPLOADING BACKUP FILE " + localbackupfolder + "\\" + localbackupprefix + "backup.zip");

                        using (System.Net.WebClient client = new System.Net.WebClient())
                        {

                            //upload new backup
                            client.Credentials = new System.Net.NetworkCredential(remoteftpusername, remoteftppassword);
                            client.UploadFile("ftp://" + remoteftpaddress + "/" + new FileInfo(localbackupfolder + "\\" + localbackupprefix + "backup.zip").Name, "STOR", localbackupfolder + "\\" + localbackupprefix + "backup.zip");

                        }

                        strEmailText += "<br />INFO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Upload of backup file OK&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OK<br />";
                        debuglog("UPLOAD OF BACKUP FILE OK");

                    } 
                    catch(Exception e) {

                        errorcount++;
                        strEmailText += "<br />ALERT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Upload of backup file FAIL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FAIL" + e.Message + "<br />";
                        debuglog("UPLOAD OF BACKUP FILE FAILED" + e.Message + e.InnerException);

                    }

                }

                /// <remarks>
                /// Upload ZIP file to a UNC path
                /// </remarks>
                
                else
                {

                    //parse username and domain from credentials
                    string domain="";
                    string username="";
                    if(remoteuncusername.Contains('\\')) {
                        username=remoteuncusername.Split('\\')[1];
                        domain=remoteuncusername.Split('\\')[0];
                    } else {
                        username=remoteuncusername;
                    }

                    //use the UNCAccess to class to "log into" the UNC path
                    UNCAccess remoteaccess = new UNCAccess(remoteuncfolder,username,domain,remoteuncpassword);

                    /// <todo>
                    /// Finish off backups to UNC Paths
                    /// </todo>

                }

                /// <todo>
                /// Also TODO: add backup to email address function (sends the ZIP as an attachment)
                /// </todo>

            }

            UpdateProgress(100);

            strEmailText += "INFO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BACKUP COMPLETED WITH " + errorcount.ToString() + " ERRORS<br />";


            /// <remarks>
            /// Fire off the report email
            /// </remarks>

            if(emailaddress!="" && emailserver!="")
            {
                MailMessage Mail = new MailMessage();
                try
                {
                    Mail.Subject = backupname + " backup completed";
                    Mail.To.Add(emailaddress);
                    Mail.From = new MailAddress(emailaddress);

                    Mail.Body = strEmailText;
                    Mail.IsBodyHtml = true;

                    SmtpClient SMTP = new SmtpClient(emailserver);
                    SMTP.Credentials = new System.Net.NetworkCredential(emailaddress, emailpassword);

                    SMTP.Port = int.Parse(emailserverport);
                    SMTP.Send(Mail);

                } 
                catch(Exception e) 
                {
                    debuglog("BACKUP EMAIL FAILED " + e.Message + e.InnerException);
                }
            }



            /// <remarks>
            /// If the backup program was set to autorun via an argument, then close the program
            /// </remarks>

            if (autorun)
            {
                EndBackup();
            }

        }


        /// <summary>
        /// This method creates an XMLDocument containing the configuration of the current backup task
        /// the XML can then be appended/replaced in the config file
        /// </summary>
        
        public bool saveConfig()
        {

            string folderxml="";
            string databasexml="";
            string drivexml = "";
            XmlDocument tmp = new XmlDocument();

            /// <remarks>
            /// Each saveConfig method returns an XML string of the configuration data that the object stores
            /// which then gets appended to the main XML string
            /// </remarks>
            try
            {
                foreach (BackupDatabase item in databases)
                {
                    databasexml += item.saveConfig();
                }
                foreach (BackupFolder item in folders)
                {
                    folderxml += item.saveConfig();
                }
                foreach (BackupDrive item in drives)
                {
                    drivexml += item.saveConfig();
                }
            }
            catch
            {
                return false;
            }


            /// <remarks>
            /// Create the main XML string and add in the XML generated by the methods above
            /// </remarks>
            string saveconfigxml="<backuptask name=\"" + backupname + "\">";
            saveconfigxml += "<localbackupconfig><reportandbackupwith>" + backupandreportwith +  "</reportandbackupwith><backupprefix>" + localbackupprefix + "</backupprefix><backupfolder>" + localbackupfolder + "</backupfolder><iislocation>" + iismetabackfolder + "</iislocation></localbackupconfig>";
            saveconfigxml += "<remotebackupconfig><backupto>" + remotebackupto + "</backupto><uncfilepath>" + remoteuncfolder + "</uncfilepath><uncusername>" + remoteuncusername + "</uncusername><uncpassword>" + remoteuncpassword + "</uncpassword><ftpaddress>" + remoteftpaddress + "</ftpaddress><ftpusername>" + remoteftpusername + "</ftpusername><ftppassword>" + remoteftppassword + "</ftppassword></remotebackupconfig>";
            saveconfigxml += "<emailsettings><emailaddress>" + emailaddress + "</emailaddress><emailserver>" + emailserver + "</emailserver><emailusername>" + emailusername + "</emailusername><emailpassword>" + emailpassword + "</emailpassword><emailserverport>" + emailserverport+ "</emailserverport></emailsettings>";
            saveconfigxml += "<drives>" + drivexml + "</drives>";
            saveconfigxml += "<folderstobackup>" + folderxml + "</folderstobackup>";
            saveconfigxml += "<databases instancename=\"" + sqlinstance + "\">" + databasexml + "</databases></backuptask>";

            //if the current config file doesnt exist
            if (fullconfigdata.DocumentElement == null)
            {
                //write the full string to the XMLDocuemtn
                fullconfigdata.LoadXml("<backuptasks>" + saveconfigxml + "</backuptasks>");

                
                //backupnode = fullconfigdata.SelectSingleNode("backuptasks/backuptask[@name='" + backupname + "']");
            }
            
            try
            {

                //grab the backup task node from the XML Document (as we can have multiple backup tasks configured in the XML config file)
                if (fullconfigdata.SelectSingleNode("backuptasks/backuptask[@name='" + backupname + "']") != null)
                {
                    //if the node exists, remove to from the XML document (so that it can be replace with the new XML string we have just generated)
                    fullconfigdata.DocumentElement.RemoveChild(fullconfigdata.SelectSingleNode("backuptasks/backuptask[@name='" + backupname + "']"));
                }

                //prepend the XML from the config file to the XML string generated above
                string configfilexml = fullconfigdata.OuterXml.ToString().Replace("</backuptasks>", saveconfigxml + "</backuptasks>");

                //load the XML string into the XML document
                fullconfigdata.LoadXml(configfilexml);

                //grab the backup node (this is what we will use for the running configuration now)
                backupnode = fullconfigdata.SelectSingleNode("backuptasks/backuptask[@name='" + backupname + "']");

                //save the XML document to the config file
                fullconfigdata.Save(_configfilename);

            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// This method loads in the XML condif file into an XML document, and then setups the program with all of the values from the selected backup task config.
        /// </summary>

        public void loadConfig(string configname)
        {

            try
            {
                
                //grab the path of the app and append the name of the config file.
                _configfilename = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(System.AppDomain.CurrentDomain.FriendlyName.Replace("vshost.", ""), "") + "backupconfig.xml";

                //if the config file exists
                if (File.Exists(_configfilename))
                {
                    //load the config file into an XML document
                    fullconfigdata.Load(_configfilename);

                    //grab the node for the backup task we wish to run
                    backupnode = fullconfigdata.SelectSingleNode("backuptasks/backuptask[@name='" + configname + "']");

                    //setup the properties with values from the config file
                    backupandreportwith = backupnode.SelectSingleNode("localbackupconfig/reportandbackupwith").InnerText;

                    backupname = configname;
                    localbackupprefix = backupnode.SelectSingleNode("localbackupconfig/backupprefix").InnerText;
                    localbackupfolder = backupnode.SelectSingleNode("localbackupconfig/backupfolder").InnerText;
                    iismetabackfolder = backupnode.SelectSingleNode("localbackupconfig/iislocation").InnerText;

                    remotebackupto = backupnode.SelectSingleNode("remotebackupconfig/backupto").InnerText;
                    remoteuncfolder = backupnode.SelectSingleNode("remotebackupconfig/uncfilepath").InnerText;
                    remoteuncpassword = backupnode.SelectSingleNode("remotebackupconfig/uncpassword").InnerText;
                    remoteuncusername = backupnode.SelectSingleNode("remotebackupconfig/uncusername").InnerText;
                    remoteftpaddress = backupnode.SelectSingleNode("remotebackupconfig/ftpaddress").InnerText;
                    remoteftpusername = backupnode.SelectSingleNode("remotebackupconfig/ftpusername").InnerText;
                    remoteftppassword = backupnode.SelectSingleNode("remotebackupconfig/ftppassword").InnerText;

                    emailaddress = backupnode.SelectSingleNode("emailsettings/emailaddress").InnerText;
                    emailserver = backupnode.SelectSingleNode("emailsettings/emailserver").InnerText;
                    emailusername = backupnode.SelectSingleNode("emailsettings/emailusername").InnerText;
                    emailpassword = backupnode.SelectSingleNode("emailsettings/emailpassword").InnerText;
                    emailserverport = backupnode.SelectSingleNode("emailsettings/emailserverport").InnerText;

                    sqlinstance = backupnode.SelectSingleNode("databases").Attributes["instancename"].Value;

                    //load in backupdrive configurations
                    foreach (XmlNode node in backupnode.SelectNodes("drives/drive"))
                    {
                        BackupDrive drive = new BackupDrive(node.Attributes["letter"].Value.ToString().ToArray()[0], Int64.Parse(node.InnerText));
                        adddrive(drive);
                        drive = null;
                    }

                    //load in databasebackup configurations
                    foreach (XmlNode node in backupnode.SelectNodes("databases/database"))
                    {
                        BackupDatabase dbbackup = new BackupDatabase(node.Attributes["name"].Value.ToString(), bool.Parse(node.SelectSingleNode("backup").InnerText), bool.Parse(node.SelectSingleNode("reindex").InnerText), short.Parse(node.SelectSingleNode("indexfillfactor").InnerText), bool.Parse(node.SelectSingleNode("rebuild").InnerText), short.Parse(node.SelectSingleNode("threshold").InnerText), bool.Parse(node.SelectSingleNode("shrinkdb").InnerText));

                        //load in database custom query configurations
                        foreach (XmlNode query in node.SelectNodes("databaseextraqueries/query"))
                        {
                            DatabaseExtraQuery dbquery = new DatabaseExtraQuery();
                            dbquery.execute = bool.Parse(query.Attributes["execute"].Value);
                            dbquery.query = query.InnerText;
                            dbbackup.AddExtraQuery(dbquery);
                            dbquery = null;
                        }

                        adddatabasebackup(dbbackup);
                        dbbackup = null;
                    }

                    //load in backupfolder configurations
                    foreach (XmlNode node in backupnode.SelectNodes("folderstobackup/folder"))
                    {
                        BackupFolder folderbkup = new BackupFolder(node.SelectSingleNode("path").InnerText, node.SelectSingleNode("filtertype").InnerText, node.SelectSingleNode("extensions").InnerText);
                        addfolderbackup(folderbkup);
                        folderbkup = null;
                    }

                    debuglog("CONFIG LOADED\t" + backupnode.OuterXml);

                }
                else
                {
                    backupname = configname;
                    debuglog("config file - " + _configfilename + " does not exist");

                }

            }
            catch(Exception e)
            {
                backupname = configname;
                debuglog("Cannot load config file - " + _configfilename);
            }

        }

        /// <summary>
        /// This method writes the supplied string to a log file - useful for debugging
        /// </summary>
        public void debuglog(string logdata)
        {

            //get the path where this program sits, and create the filename based on the date, so  a new log file is created everyday
            string strDebugFile = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(System.AppDomain.CurrentDomain.FriendlyName.Replace("vshost.", ""), "") + "logs/log-" + DateTime.Now.Date.ToString().Replace("/", "-").Replace("00:00:00", "").Replace("12:00:00", "") + "-" + backupname + ".txt";
            
            //create the logs folder if missing
            if (!Directory.Exists(System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(System.AppDomain.CurrentDomain.FriendlyName.Replace("vshost.", ""), "") + "logs"))
            {
                Directory.CreateDirectory(System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(System.AppDomain.CurrentDomain.FriendlyName.Replace("vshost.", ""), "") + "logs");
            }

            //create the log file if missing
            if (!File.Exists(strDebugFile))
            {
                FileStream tmp = File.Create(strDebugFile);
                tmp.Close();
                tmp.Dispose();
            }

            //set append file
            StreamWriter debuglog = File.AppendText(strDebugFile);

            //write strong to file
            debuglog.WriteLine(logdata);

            //clean up
            debuglog.Close();
            debuglog.Dispose();

        }

        /// <summary>
        /// Convert a boolean value to a string
        /// </summary>
        private string convertbooltostring(bool value)
        {
            if (value)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

    }
}
