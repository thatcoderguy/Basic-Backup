Basic-Backup
============

A basic server backup app; backs up databases, files, and IIS metabases

Installation and usage
============

Execute the T-SQL code in maintenance sql.txt on the SQL Instance(s) that you wish to backup the databases on. 
This will create the maintnance stored procedure on the instance's master database.


The application accepts 1 or 2 arguments:

> The first argument is the name of the backup config - the config file can store multiple configurations, 
  so you only need to run the application from one place.

> The second (optional) argument can only be "autorun" - this makes the application run the configured 
  backup task referred to in the first argument.
  
  
Ideally this application can be setup with one or more Scheduled Tasks so that the backup tasks can 
be executed at specified times.
