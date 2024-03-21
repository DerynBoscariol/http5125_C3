using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace School.Models
{
	public class SchoolDbContext
	{
		private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "8889"; } }

        protected static string ConnectionString
        {
            get
            { 
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }
        //Method to connect to database
        /// <summary>
        /// Returns a connection to the school database
        /// </summary>
        /// <example>
        /// private SchoolDbContext School = new SchoolDbContext();
        /// MySqlConnection Conn = School.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase()
        {
            //initializing the MySqlConnection Class to create an object
            return new MySqlConnection(ConnectionString);
        }
    }
}

