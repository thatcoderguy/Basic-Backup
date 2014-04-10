using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// This class stores the details for a custom query for a database
/// </summary>
namespace BasicBackupClasses
{
    class DatabaseExtraQuery
    {
        public bool execute { get; set; }
        public string query { get; set; }
    }
}
