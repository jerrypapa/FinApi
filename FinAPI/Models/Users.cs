using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace FinAPI.Models
{
    public class Users:RejectedCheque
    {
        public String Email { get; set; }
        public String Password { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public int ProfileId { get; set; }
        public String Phone { get; set; }
        public String Username { get; set; }
        public String Bankcode { get; set; }
        public String Branchid { get; set; }
        public int Accountno { get; set; }
        public String Usertype { get; set; }
        public String DeviceToken { get; set; }

        public Users() { }
        //Register FinAdmin
        public Users(String Email, String Password, String Firstname, String Lastname, int ProfileId, String Phone,String Username,String Bankcode,String Branchid)
        {
            this.Email = Email;
            this.ProfileId = ProfileId;
            this.Phone = Phone;
            this.Password = Password;
            this.Lastname = Lastname;
            this.Firstname = Firstname;
            this.Username = Username;
            this.Bankcode = Bankcode;
            this.Branchid = Branchid;
        }

        public Users(String Email, String Password, String Firstname, String Lastname, int ProfileId, String Phone, String Username, String Bankcode, String Branchid,int Accountno)
        {
            this.Email = Email;
            this.ProfileId = ProfileId;
            this.Phone = Phone;
            this.Password = Password;
            this.Lastname = Lastname;
            this.Firstname = Firstname;
            this.Username = Username;
            this.Bankcode = Bankcode;
            this.Branchid = Branchid;
            this.Accountno = Accountno;
        }

        public Users(String Email, String Password, String Firstname, String Lastname, int ProfileId, String Phone, String Username, String Bankcode, String Branchid, int Accountno,String Usertype)
        {
            this.Email = Email;
            this.ProfileId = ProfileId;
            this.Phone = Phone;
            this.Password = Password;
            this.Lastname = Lastname;
            this.Firstname = Firstname;
            this.Username = Username;
            this.Bankcode = Bankcode;
            this.Branchid = Branchid;
            this.Accountno = Accountno;
            this.Usertype = Usertype;
        }

        public Users(String Email, String Password, String Firstname, String Lastname, int ProfileId, String Phone, String Username, String Bankcode, String Branchid, int Accountno, String Usertype,String DeviceToken)
        {
            this.Email = Email;
            this.ProfileId = ProfileId;
            this.Phone = Phone;
            this.Password = Password;
            this.Lastname = Lastname;
            this.Firstname = Firstname;
            this.Username = Username;
            this.Bankcode = Bankcode;
            this.Branchid = Branchid;
            this.Accountno = Accountno;
            this.Usertype = Usertype;
            this.DeviceToken = DeviceToken;
        }

        public Users LoginMobileUser(String e, String p)
        {
            Users mobileUser = null;
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;

            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "LoginCustomer";
                com.Parameters.Add("@email", SqlDbType.VarChar).Value =
                    e;
                com.Parameters.Add("@password", SqlDbType.VarChar).Value =
                    p;

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    mobileUser = new Users(
                        reader["email"].ToString(),
                        reader["password"].ToString(),
                        reader["firstname"].ToString(),
                        reader["lastname"].ToString(),
                        Convert.ToInt32(reader["profileid"].ToString()),
                        reader["phone"].ToString(),
                        reader["username"].ToString(),
                        reader["BANKCODE"].ToString(),
                        reader["BRANCHID"].ToString(),
                        Convert.ToInt32(reader["AccountNo"].ToString()),
                        reader["USERTYPE"].ToString()
                    );
                }

                return mobileUser;
            }
        }

        public Users LoginMobileUser(int accountno)
        {
            Users mobileUser = null;
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;

            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "CustomerInfoAcc";
                com.Parameters.Add("@accountno", SqlDbType.VarChar).Value =
                    accountno;

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    mobileUser = new Users(
                        reader["email"].ToString(),
                        reader["password"].ToString(),
                        reader["firstname"].ToString(),
                        reader["lastname"].ToString(),
                        Convert.ToInt32(reader["profileid"].ToString()),
                        reader["phone"].ToString(),
                        reader["username"].ToString(),
                        reader["BANKCODE"].ToString(),
                        reader["BRANCHID"].ToString(),
                        Convert.ToInt32(reader["AccountNo"].ToString()),
                        reader["USERTYPE"].ToString(),
                        reader["DEVICETOKEN"].ToString()
                    );
                }

                return mobileUser;
            }
        }

        public int RegisterMobileUser(/*Users moBileUser*/)
        {
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;
            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "RegisterCustomer";
                com.Parameters.Add("@firstname", SqlDbType.VarChar).Value =
                    "Derick";
                com.Parameters.Add("@lastname", SqlDbType.VarChar).Value =
                    "Oduor";
                com.Parameters.Add("@username", SqlDbType.VarChar).Value = "Derick Oduor"/*DateTime.ParseExact(cheque.DateIssued, "yyyy-MM-dd", null)*/;
                /*Convert.ToDateTime(cheque.DateIssued)*/
                ;
                com.Parameters.Add("@phone", SqlDbType.VarChar).Value =
                    "+254715812661";
                com.Parameters.Add("@email", SqlDbType.VarChar).Value =
                    "oduorderick@gmail.com";
                com.Parameters.Add("@password", SqlDbType.VarChar).Value =
                    /*"2018-12-13 11:20:00"*/Crypto.Hash("oduorderick@gmail.com", "MD5");
                com.Parameters.Add("@profileid", SqlDbType.VarChar).Value =
                    3;
                com.Parameters.Add("@bankcode", SqlDbType.VarChar).Value =
                    "41";
                com.Parameters.Add("@branchid", SqlDbType.VarChar).Value =
                    "41102";
                com.Parameters.Add("@accountno", SqlDbType.VarChar).Value =
                    "1023598510";

                int i = com.ExecuteNonQuery();

                return i;
            }
        }

        public List<RejectedCheque> GetRejectedCheques(int accountno)
        {
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            List<Cheque> rejectedCheques = new List<Cheque>();
            List<RejectedCheque> chequeList = new List<RejectedCheque>();
            Cheque c = new Cheque();

            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "GetCustomerRCheques";
                com.Parameters.Add("@accountno", SqlDbType.VarChar).Value =
                    accountno;

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    c.micr = reader["micr"].ToString();
                    c.scannedMicr = reader["scannedMicr"].ToString();
                    c.FImageName = reader["fname"].ToString();
                    c.BImageName = reader["bname"].ToString();
                    c.ChequeName = reader["ChequeName"].ToString();
                    c.Amount = Double.Parse(reader["amount"].ToString());
                    c.DateIssued = reader["date_issued"].ToString();
                    c.DateSubmitted = reader["date_submitted"].ToString();
                    c.Currency = reader["Currency"].ToString();
                    c.Accountno = Int32.Parse(reader["AccountNo"].ToString());
                    c.Status= reader["chequestatus"].ToString();
                    rejectedCheques.Add(c);
                }

                reader.Close();
            }

            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "GetCustomerRejectedCheques";

                com.Parameters.Add("@accountno", SqlDbType.VarChar).Value =
                    accountno;
                using (SqlDataReader reader2 = com.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        /*chequeList.Add(
                                    new RejectedCheque(
                                        reader["cheque_name"].ToString(),
                                        reader["scannedMicr"].ToString(),
                                        reader["micr"].ToString(),
                                        Double.Parse(reader["amount"].ToString()),
                                        reader["reasons"].ToString(),
                                        reader["currency"].ToString()
                                     )
                                );*/
                        Cheque m = new Cheque();
                        foreach (Cheque ch in rejectedCheques) {
                            if (ch.Accountno == Int32.Parse(reader2["accountno"].ToString())) {
                                m = ch;
                            }
                        }
                        chequeList.Add(new RejectedCheque(
                                    m, 
                                    reader2["cheque_name"].ToString(),
                                    reader2["scannedMicr"].ToString(),
                                    reader2["micr"].ToString(),
                                    Double.Parse(reader2["amount"].ToString()),
                                    reader2["reasons"].ToString(),
                                    reader2["currency"].ToString(),
                                    Int32.Parse(reader2["accountno"].ToString())));
                        /*foreach (Cheque ch in rejectedCheques) {
                            if (ch.Accountno == Int32.Parse(reader2["accountno"].ToString()))
                            {
                            }
                        }*/
                        
                    }
                }
                //return chequeList;
            }

            return /*rejectedCheques*/chequeList; ;
        }

        public FinAdmin Login(String e,String p) {
            FinAdmin finAdmin = null;
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;

            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "LoginFinAdmin";
                com.Parameters.Add("@email", SqlDbType.VarChar).Value =
                    e;
                com.Parameters.Add("@password", SqlDbType.VarChar).Value =
                    p;

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read()) {
                    finAdmin = new FinAdmin(reader["EMAIL"].ToString(), reader["PASSWORD"].ToString(), reader["USERNAME"].ToString(), reader["USERNAME"].ToString(), Convert.ToInt32(reader["PROFILEID"].ToString()), reader["PHONENO"].ToString());
                }

                    return finAdmin;
            }
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

        public String UpdateSimDetails(int accountno,String SimCardId,String IMEI)
        {
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;

            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "UpdateUserSimDetails";
                com.Parameters.Add("@SimCardId", SqlDbType.VarChar).Value =
                    SimCardId;
                com.Parameters.Add("@IMEI", SqlDbType.VarChar).Value =
                    IMEI;
                com.Parameters.Add("@Accountno", SqlDbType.VarChar).Value = 
                    accountno;

                int i = com.ExecuteNonQuery();

                if (i == 1)
                {
                    return "ok";
                }else
                {
                    return "failed";
                }
            }
        }

        public String UpdateDeviceToken(String token,int accountno)
        {
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;

            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "UpdateDeviceToken";
                com.Parameters.Add("@AccountNo", SqlDbType.VarChar).Value =
                    accountno;
                com.Parameters.Add("@DEVICETOKEN", SqlDbType.VarChar).Value =
                    token;

                int i = com.ExecuteNonQuery();

                if (i == 1)
                {
                    return "success";
                }else
                {
                    return "failed";
                }
            }
        }
    }
}