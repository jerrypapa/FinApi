using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FinAPI.Controllers
{
    public class ChequeQueriesApiController : ApiController
    {
        Cheque cheque = new Cheque();

        [HttpPost]
        public List<Cheque> Query() {
            Cheque c = new Cheque();
            List<Cheque> chequeList = new List<Cheque>();

            int accountno = Int32.Parse(HttpContext.Current.Request.Params["accountno"]);
            chequeList = cheque.Query(accountno);
            return chequeList;
        }
        /*
         * 
         * 
         *
         * GET: api/ChequeQueriesApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

         // GET: api/ChequeQueriesApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ChequeQueriesApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ChequeQueriesApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ChequeQueriesApi/5
        public void Delete(int id)
        {
        }*/
    }
}
