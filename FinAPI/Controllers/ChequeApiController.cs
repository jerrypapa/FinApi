using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FinAPI.Controllers
{
    public class ChequeApiController : ApiController
    {
        Cheque cheque = new Cheque();

        [HttpPost]
        public Cheque QueryStatus()
        {
            Cheque c = new Cheque();
            
            String chequename = HttpContext.Current.Request.Params["chequename"];
            c = cheque.GetChequeStatus(chequename);
            return c;
        }
        /*
         * 
         * 
        // GET: api/ChequeApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ChequeApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ChequeApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ChequeApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ChequeApi/5
        public void Delete(int id)
        {
        }*/
    }
}
