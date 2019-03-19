using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public String Username { get; set; }
        public String Surname { get; set; }
        public String  Othernames { get; set; }
        public String  Password { get; set; }
        public String  LoggedInSession { get; set; }
        public String  Email { get; set; }
        public String  Phoneno { get; set; }
        public int  Branchid { get; set; }
        public int StatusId { get; set; }
        public int  ProfileId { get; set; }
        public String  LastPassChange { get; set; }
        public int  ActiveSession { get; set; }
        public int  ChangePass { get; set; }
        public String  LastLogin { get; set; }
        public int  FailedAttempts { get; set; }
        public String IPAddress { get; set; }
        public String DateCreated { get; set; }
        public int  ClientCode { get; set; }
        public int  IsRemoteScan { get; set; }
        public String  City { get; set; }
        public String  Country { get; set; }

        public User Login(User user)
        {
            int count = 0;
            User u = new User();
            DBConnect dbConnect;
            SqlConnection conn = null;
            try
            {
                dbConnect = new DBConnect();
                conn = dbConnect.OpenDBConn();
            }
            catch (Exception e)
            {
                //inserted = "sqlexception-" + e.Message;
            }

            try
            {
                string existQuery = "SELECT * FROM [USER] WHERE EMAIL='"+user.Email+"' AND PASSWORD='"+user.Password+"'";
                //string existQuery = "SELECT * FROM [USER]";
                SqlCommand existQueryCmd = new SqlCommand(existQuery, conn);
                SqlDataReader reader = existQueryCmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    u.UserId = Int32.Parse(reader["USERID"].ToString());
                    u.Username = reader["USERNAME"].ToString();
                    u.Surname = reader["SURNAME"].ToString();
                    u.Othernames = reader["OTHERNAMES"].ToString();
                    u.Password = reader["PASSWORD"].ToString();
                    u.LoggedInSession = reader["LOGGEDINSESSION"].ToString();
                    u.Email = reader["EMAIL"].ToString();
                    u.Phoneno = reader["PHONENO"].ToString();
                    u.Branchid = int.Parse(reader["BRANCHID"].ToString());
                    u.StatusId = int.Parse(reader["STATUSID"].ToString());
                    u.ProfileId = int.Parse(reader["PROFILEID"].ToString());
                    u.LastPassChange = reader["LASTPASSCHANGE"].ToString();
                    u.ActiveSession = int.Parse(reader.GetOrdinal("ACTIVESESSION").ToString());
                    u.ChangePass = int.Parse(reader.GetOrdinal("CHANGEPASS").ToString());
                    u.LastLogin = reader["LASTLOGIN"].ToString();
                    u.FailedAttempts = int.Parse(reader["FAILEDATTEMPTS"].ToString());
                    u.IPAddress = reader["IPADDRESS"].ToString();
                    u.DateCreated = reader["DATE_CREATED"].ToString();
                }
                reader.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
            return u;
        }
    }
}