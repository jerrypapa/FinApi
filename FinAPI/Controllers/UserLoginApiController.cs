using FinAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace FinAPI.Controllers
{
    public class UserLoginApiController : ApiController
    {
        User user = new User();
        Account account = new Account();
        [HttpPost]
        public Account Login(Account acc)
        {
            String filepath = "";
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("log something");
                filepath = HttpContext.Current.Server.MapPath("~/Content/Logs/");
                // flush every 20 seconds as you do it
                File.AppendAllText(filepath + "log_" + acc.Email + ".txt", new JavaScriptSerializer().Serialize(acc));
                File.AppendAllText(filepath + "LoginLogs.txt", "\n" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz") + "\n" + "Login JSON received");

                sb.Clear();
            }
            catch (Exception e)
            {
                //acc.AccountName = e.Message + "\n" + e.StackTrace;
            }
            return account.NewLogin(acc);
            /*String filepath = "";
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("log something");
                filepath = HttpContext.Current.Server.MapPath("~/Content/Logs/");
                // flush every 20 seconds as you do it
                File.AppendAllText(filepath + "log_" + acc.Email + ".txt", new JavaScriptSerializer().Serialize(acc));
                File.AppendAllText(filepath + "LoginLogs.txt", "\n" + new DateTime().ToString() + "\n" + "Login JSON received");

                sb.Clear();
            }
            catch (Exception e)
            {

            }
            Account loggedAccount = account.NewLogin(acc);
            return account.NewLogin(acc);*/
        }
        /*[HttpPost]
        public User Login(User u)
        {
            User loggedUser = user.Login(u);
            return user.Login(u);
        }*/

        [HttpGet]
        public string Get()
        {
            return "call success";
        }
    }
}