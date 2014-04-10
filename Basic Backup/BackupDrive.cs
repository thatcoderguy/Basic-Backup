using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// This class stores the maintenance details for a drive
/// </summary>
namespace BasicBackupClasses
{
    class BackupDrive
    {
        public char driveletter { get; set; }
        public Double thresholdsize { get; set; }

        public BackupDrive()
        {
            driveletter = 'C';
            thresholdsize = 0;
        }

        public BackupDrive(char letter, Int64 threshold)
        {
            driveletter = letter;
            thresholdsize = threshold;
        }

        /// <summary>
        /// This method generates the XML of all the values in this class to write to the config file
        /// </summary>
        public string saveConfig()
        {
            string dbxml = "<drive letter=\"" + driveletter + "\">" +  thresholdsize.ToString() + "</drive>";
            return dbxml;
        }

    }

}
