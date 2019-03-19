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
    public class RejectedChequesApiController : ApiController
    {
        Users mobileUser = new Users();
        

        [HttpPost]
        public RejectedCheque/*List<Cheque>*/ GetRejectedCheques()
        {
            //List<Cheque> rejectedCheques = null;
            List<RejectedCheque> rejectedCheques = null;
            int accountno = Int32.Parse(HttpContext.Current.Request.Params["Accountno"]);
            rejectedCheques = mobileUser.GetRejectedCheques(accountno);

            RejectedCheque rC = new RejectedCheque {
                RejectedChequesList=rejectedCheques
            };
            //return rejectedCheques;
            return rC;
        }
        /*
         * 
         * 
         * // GET: api/RejectedChequesApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/RejectedChequesApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/RejectedChequesApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/RejectedChequesApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RejectedChequesApi/5
        public void Delete(int id)
        {
        }*/
    }
}
