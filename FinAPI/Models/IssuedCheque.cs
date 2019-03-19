using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace FinAPI.Models
{
    public class IssuedCheque:Cheque
    {
        public String DraweeName { get; set; }
        public String DateToBeSubmitted { get; set; }
        Users user = new Users();

        public IssuedCheque() { }
        public IssuedCheque(int Accountno,String DateIssued, Double Amount, String Currency, String micr, String DateToBePresented)
        {
            this.Amount = Amount;
            this.DateIssued = DateIssued;
            this.Currency = Currency;
            this.micr = micr;
            this.DateToBeSubmitted = DateToBeSubmitted;
            this.Accountno = Accountno;
        }

        public String IssueCheque(IssuedCheque c)
        {
            IssuedCheque ic = new IssuedCheque();
            int i = 0;
            String s = "";
            try
            {
                //BEGIN
                DBConnect dbConnect = new DBConnect();
                SqlConnection conn = dbConnect.OpenDBConn();
                DateTime time = DateTime.Now;
                using (SqlCommand com = conn.CreateCommand())
                {

                    //@ChequeMicr,@Accountno,@DateIssued,@DateToBeSubmitted,@Amount,@Currency,@DEPOSITED
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.CommandText = "IssueCheque";
                    com.Parameters.Add("@ChequeMicr", SqlDbType.VarChar).Value =
                        c.micr;
                    com.Parameters.Add("@Accountno", SqlDbType.VarChar).Value =
                        c.Accountno;
                    com.Parameters.Add("@DateIssued", SqlDbType.VarChar).Value = c.DateIssued;
                    com.Parameters.Add("@DateToBeSubmitted", SqlDbType.VarChar).Value =
                        c.DateToBeSubmitted;
                    com.Parameters.Add("@Amount", SqlDbType.VarChar).Value =
                        c.Amount;
                    com.Parameters.Add("@Currency", SqlDbType.VarChar).Value =
                        c.Currency;
                    com.Parameters.Add("@DEPOSITED", SqlDbType.VarChar).Value =
                        "NO";
                    com.Parameters.Add("@DraweeName", SqlDbType.VarChar).Value =
                        c.DraweeName;

                    i = com.ExecuteNonQuery();
                    if (i == 1)
                    {
                        s = "ok";
                    }else
                    {
                        s = "fail";
                    }
                    //END
                }
            }
            catch (Exception e)
            {
                e.StackTrace.ToString();
                s = e.Message;
            }

            return s;
        }

        public String pushNotification(/*Plobj obj*/String chequename)
        {
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;
            int j = 0;
            int i = 0;
            String email = "";
            int accountno = 0;
            String deviceToken = "";
            Double amount = 0;
            String currency = "";
            String micr = "";
            String drawee = "";

            using (SqlCommand com2 = conn.CreateCommand())
            {
                com2.CommandType = System.Data.CommandType.StoredProcedure;
                com2.CommandText = "PickIssuedCheque";

                com2.Parameters.Add("@chequename", SqlDbType.VarChar).Value =
                    chequename;
                using (SqlDataReader reader2 = com2.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        amount = Double.Parse(reader2["Amount"].ToString());
                        //email = reader2["email"].ToString();
                        currency = reader2["Currency"].ToString();
                        accountno = Int32.Parse(reader2["AccountNo"].ToString());
                        micr = reader2["ChequeMicr"].ToString();
                        drawee= reader2["DraweeName"].ToString();
                    }
                    reader2.Close();
                }
            }

            Users users = user.LoginMobileUser(accountno);

            /*using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "PickIssuedCheque";

                com.Parameters.Add("@chequename", SqlDbType.VarChar).Value =
                    chequename;
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        email = reader["email"].ToString();
                        accountno = Int32.Parse(reader["AccountNo"].ToString());
                    }
                    reader.Close();
                }
            }*/

            string str = "";
            try
            {
                var applicationID = "AAAAclVa85Y:APA91bGQZBqxFSWiB7Js3XvoeteB6YncKA6kgLqVRyyIYT8NZpRSNAi2TSjU-iZdLATHqkd2lAd5B5dxBfQmcqAPingw4ZMkyV5IqNtm1V0yjYPO5ZNy02e6TpNW0jcCmHQjMvoUu83i";
                var senderId = "491058295702";
                string deviceId = users.DeviceToken/*"dcABFIAZFUo:APA91bGbTROAejg428iCJRoDNkmNLJsLtBkaHFVRQ_c7jCVRhlexoCchjlSyiZJNuLEigesdclx3iRV1MyJtnjLQM7DcJhx7cBe7JfmvmUZDmBg7NuPwd5dWgcBG-vQlUR3lWVAgpXIA"*/;
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        /*body = obj.Message,

                        title = obj.TagMsg,

                        icon = "myicon"*/
                        body = "Account debit\n "+currency+". "+amount+" is about to be debited from your account for <b>Cheque No.:</b> "+micr+"/n <b>Drawee Name: </b>"+drawee,
                        title = "Capture - Notification",
                        icon = "myicon"
                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                str = sResponseFromServer;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                str = ex.Message;
            }
            return str;
        }
    }
}