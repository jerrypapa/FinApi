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
    public class SimDetailsApiController : ApiController
    {
        Users user = new Users();
        [HttpPost]
        public String SaveDetails()
        {
            String Accountno = HttpContext.Current.Request.Params["Accountno"];
            String SimcardId = HttpContext.Current.Request.Params["SimCardId"];
            String IMEI = HttpContext.Current.Request.Params["IMEI"];
            
            return user.UpdateSimDetails(Int32.Parse(Accountno),SimcardId,IMEI);
        }
    }
}
