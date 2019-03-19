using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class FinAdmin
    {
        public String Email { get; set; }
        public String Password { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public int ProfileId { get; set; }
        public String Phone { get; set; }

        public FinAdmin() { }
        //Register FinAdmin
        public FinAdmin(String Email,String Password, String Firstname,String Lastname,int ProfileId,String Phone) {
            this.Email = Email;
            this.ProfileId = ProfileId;
            this.Phone = Phone;
            this.Password = Password;
            this.Lastname = Lastname;
            this.Firstname = Firstname;
        }
        public int RegisterAdmin(FinAdmin f)
        {
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;

            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "AddFinAdmin";
                com.Parameters.Add("@email", SqlDbType.VarChar).Value =
                    f.Email;
                com.Parameters.Add("@password", SqlDbType.VarChar).Value =
                    f.Password;
                com.Parameters.Add("@phone", SqlDbType.VarChar).Value = f.Phone;
                ;
                com.Parameters.Add("@profileid", SqlDbType.Int).Value =
                    f.ProfileId;
                com.Parameters.Add("@username", SqlDbType.VarChar).Value =
                    f.Firstname + " " + f.Lastname;

                int i = com.ExecuteNonQuery();

                return i;
            }
        }
    }
}