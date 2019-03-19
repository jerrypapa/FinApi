using FinAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;

namespace FinAPI.Controllers
{
    public class LoginApiController : ApiController
    {
        Users user = new Users();
        MobileUserBranch branch = new MobileUserBranch();

        [HttpPost]
        public Users Login() {
            String email=HttpContext.Current.Request.Params["Email"];
            String password = HttpContext.Current.Request.Params["Password"];

            Users u = user.LoginMobileUser(email, Crypto.Hash(password, "MD5"));
            //int i = user.RegisterMobileUser();

            if (u != null) {
                return u;
            }
            else
            {
                return u;
            }
                
        }


        /*
         * // GET: api/LoginApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/LoginApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LoginApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/LoginApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LoginApi/5
        public void Delete(int id)
        {
        }*/
    }
}
