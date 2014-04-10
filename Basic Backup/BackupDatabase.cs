using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;

/// <summary>
/// This class stores the maintenance options of a database
/// </summary>
namespace BasicBackupClasses
{
    class BackupDatabase
    {
        public string databasename { get;  set; }
        public bool backup { get;  set; }
        public bool reindex { get;  set; }
        public Int16 indexfillfactor { get; set; }
        public bool rebuild { get;  set; }
        public bool shrink { get; set; }
        public Int16 threshold { get;  set; }
        public BindingList<DatabaseExtraQuery> databasecustomcommands;

        public BackupDatabase()
        {
            databasename = "";
            backup = false;
            reindex = false;
            rebuild = false;
            threshold = 0;
            indexfillfactor = 0;
            shrink = false;
        }

        public BackupDatabase(string strdatabasename, bool blnbackup, bool blnreindex, Int16 intfillfactor, bool blnrebuild, Int16 intthreshold, bool blnshrink)
        {
            databasename = strdatabasename;
            backup = blnbackup;
            reindex = blnreindex;
            rebuild = blnrebuild;
            threshold = intthreshold;
            shrink = blnshrink;
            indexfillfactor = intfillfactor;
            databasecustomcommands = new BindingList<DatabaseExtraQuery>();
        }

        public void AddExtraQuery(DatabaseExtraQuery storedprocedurename)
        {
            databasecustomcommands.Add(storedprocedurename);
        }

        public void UpdateExtraQuery(int index, DatabaseExtraQuery storedprocedurename)
        {
            databasecustomcommands[index] = storedprocedurename;
        }

        public void RemoveExtraQuery(string storedprocedurename) 
        {
            DatabaseExtraQuery item = databasecustomcommands.SingleOrDefault(databasebackupcustom => databasebackupcustom != null);
            if (item != null)
            {
                databasecustomcommands.Remove(item);
            }
        }

        public void RemoveExtraQuery(int index)
        {
            databasecustomcommands.RemoveAt(index);
        }

        /// <summary>
        /// This method returns a BindingList so that the returned objects can be bound to a gridview.
        /// </summary>
        public BindingList<DatabaseExtraQuery> GetExtraQueries()
        {
            return databasecustomcommands;
        }

        /// <summary>
        /// This method generates the XML of all the values in this class to write to the config file
        /// </summary>
        public string saveConfig()
        {
            string dbxml = "<database name=\"" + databasename + "\"><backup>" + backup.ToString() + "</backup><reindex>" + reindex.ToString() + "</reindex><indexfillfactor>" + indexfillfactor.ToString() + "</indexfillfactor><rebuild>" + rebuild.ToString() + "</rebuild><threshold>" + threshold.ToString() + "</threshold><shrinkdb>" + shrink.ToString() + "</shrinkdb><databaseextraqueries>";
            foreach (DatabaseExtraQuery item in databasecustomcommands)
            {
                dbxml += "<query execute=\"" + item.execute.ToString() + "\">" + item.query + "</query>";
            }
            dbxml += "</databaseextraqueries></database>";
            return dbxml;
        }

    }

}

