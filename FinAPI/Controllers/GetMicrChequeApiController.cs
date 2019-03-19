using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FinAPI.Controllers
{
    public class GetMicrChequeApiController : ApiController
    {
        Cheque cheque = new Cheque();

        [HttpPost]
        public Cheque GetMicrCheque()
        {
            Cheque c = new Cheque();
            String micr = HttpContext.Current.Request.Params["micr"];
            c = cheque.GetMicrCheque(micr);

            return c;
        }
    }
}
