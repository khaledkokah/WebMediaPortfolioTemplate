using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

//This class contains all the needed app constants 
namespace WebMediaPortfolioTemplate.Classes
{
    public static class AppConstants
    {
        //SQL connection string name from web.config
        public static string connName { get { return "dbConn"; } }

        //SQL connection string from web.config
        public static string connStr { get { return ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString; } }

        //Encryption key that will be used for database encryption (Better be saved in database!)
        public static string encryptionKey { get { return "MAKV2SPBNI99212"; } }
    }
}