using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinAPI
{
    public class DBConnect
    {
        public SqlConnection conn;
        public DBConnect() {
            this.conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            conn.Open();
        }

        public SqlConnection OpenDBConn()
        {
            this.conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            conn.Open();
            return this.conn;
        }

        public void CloseDBConn() {
            this.conn.Close();
        }
    }
}