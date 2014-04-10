using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

/// <summary>
/// This class stores the maintenance details for a folder
/// </summary>
namespace BasicBackupClasses
{
    class BackupFolder
    {
        public string foldername { get; set; }
        private string filter;
        public string extensions { get; set; }

        //filter type property
        //involved a bit more then just "get" and "set", as it should be only contain "include" or "exclude"
        public string filtertype 
        {
            get
            {
                return filter; 
            }

            set
            {
                //make sure the value can only be include or exclude
                if (value.ToLower() == "exclude" || value.ToLower() == "include")
                {
                    filter = value;
                }
                else
                {
                    filter = "include";
                }
            }
        }
        

        public BackupFolder()
        {
            foldername = "";
            filtertype = "include";
        }

        public BackupFolder(string strfolderpath, string filter, string ext)
        {
            foldername = strfolderpath;
            filtertype = filter;
            extensions = ext;
        }

        /// <summary>
        /// This method generates the XML of all the values in this class to write to the config file
        /// </summary>
        public string saveConfig()
        {
            string folderxml = "<folder><path>" + foldername + "</path><filtertype>" + filtertype + "</filtertype><extensions>" + extensions + "</extensions></folder>";
            return folderxml;
        }

    }

}
