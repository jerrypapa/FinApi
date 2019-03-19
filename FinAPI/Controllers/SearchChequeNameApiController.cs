using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FinAPI.Controllers
{
    public class SearchChequeNameApiController : ApiController
    {
        Cheque cheque = new Cheque();
        [HttpPost]
        public List<Cheque> Search()
        {
            String accountno = HttpContext.Current.Request.Params["Accountno"];
            String micr = HttpContext.Current.Request.Params["micr"];
            List<Cheque> chequeList = cheque.SearchChequeName(Int32.Parse(accountno),micr);
            return chequeList;
        }
        /*
         * 
         * 
         * // GET: api/SearchChequeNameApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SearchChequeNameApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SearchChequeNameApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SearchChequeNameApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SearchChequeNameApi/5
        public void Delete(int id)
        {
        }*/
    }
}