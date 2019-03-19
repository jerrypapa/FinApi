using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FinAPI.Controllers
{
    public class SearchChequeDateApiController : ApiController
    {
        Cheque cheque = new Cheque();
        [HttpPost]
        public List<Cheque> Search()
        { 
            String accountno = HttpContext.Current.Request.Params["Accountno"];
            String startdate = HttpContext.Current.Request.Params["Startdate"];
            String enddate = HttpContext.Current.Request.Params["Enddate"];
            List<Cheque> chequeList = cheque.SearchChequeDate(Int32.Parse(accountno),startdate,enddate);
            return chequeList;
        }
        /*
         * 
         * 
         * // GET: api/SearchChequeDateApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SearchChequeDateApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SearchChequeDateApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SearchChequeDateApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SearchChequeDateApi/5
        public void Delete(int id)
        {
        }*/
    }
}
