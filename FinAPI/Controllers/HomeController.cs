using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FinAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            /*try
            {
                //BEGIN
                DBConnect dbConnect = new DBConnect();
                SqlConnection conn = dbConnect.OpenDBConn();
                DateTime time = DateTime.Now;
                using (SqlCommand com = conn.CreateCommand())
                {

                    //@username,@password,@email,@phone,@firstname,@lastname,@BANKCODE,@BRANCHID,@AccountNo,'3','ISSUER'
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.CommandText = "AddFinAdmin";
                    com.Parameters.Add("@email", SqlDbType.VarChar).Value =
                        "annewangui2000@gmail.com";
                    com.Parameters.Add("@password", SqlDbType.VarChar).Value =
                        Crypto.Hash(Crypto.Hash("annewangui2000@gmail.com", "MD5"));
                    com.Parameters.Add("@phone", SqlDbType.VarChar).Value =
                        "+254738334947";
                    com.Parameters.Add("@profileid", SqlDbType.VarChar).Value =
                        "1";
                    com.Parameters.Add("@username", SqlDbType.VarChar).Value =
                        "Ann Wangui";

                    int i = com.ExecuteNonQuery();
                    //END
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }*/
            /*string str = "";
            try
            {
                var applicationID = "AAAAclVa85Y:APA91bGQZBqxFSWiB7Js3XvoeteB6YncKA6kgLqVRyyIYT8NZpRSNAi2TSjU-iZdLATHqkd2lAd5B5dxBfQmcqAPingw4ZMkyV5IqNtm1V0yjYPO5ZNy02e6TpNW0jcCmHQjMvoUu83i";
                var senderId = "491058295702";
                string deviceId = "eXCsrXqurxg:APA91bELyTXa8LGRqoxAWY59ckZXwoD3PnOmbNlktBbKDMygsQRDb5kN1my_7MNbdmdw0GkMbVRkIi_GMUQhnnuh2_xQuh1dlThvCpKc5SbP78ZarpbignjxPodXGcX2a92lB6dgocXf";
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
                        /*body = "Hello, this is a message fin capture!",
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
            }*/
            return View();
        }
    }
}
/*
 * 
 * @email varchar(100),
	@password varchar(100),
	@phone varchar(50),
	@profileid int,
	@username varchar(100)
*/