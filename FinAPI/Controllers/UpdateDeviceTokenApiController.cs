using FinAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FinAPI.Controllers
{
    public class UpdateDeviceTokenApiController : ApiController
    {
        Users user = new Users();

        [HttpPost]
        public String UpdateDeviceToken()
        {
            String Accountno = HttpContext.Current.Request.Params["AccountNo"];
            String DeviceToken= HttpContext.Current.Request.Params["DeviceToken"];

            return user.UpdateDeviceToken(DeviceToken,Int32.Parse(Accountno))/*Accountno+" "+DeviceToken*/;
        }
        /*
         * 
         * // GET: api/UpdateDeviceTokenApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UpdateDeviceTokenApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UpdateDeviceTokenApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UpdateDeviceTokenApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UpdateDeviceTokenApi/5
        public void Delete(int id)
        {
        }*/
    }
}
