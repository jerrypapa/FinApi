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
    public class BankDetailsApiController : ApiController
    {
        Users user = new Users();
        MobileUserBranch branch = new MobileUserBranch();

        [HttpPost]
        public MobileUserBranch Login()
        {
            String email = HttpContext.Current.Request.Params["Email"];
            String accountno = HttpContext.Current.Request.Params["Accountno"];
            String Branchcode = HttpContext.Current.Request.Params["Branchcode"];
            String Bankcode = HttpContext.Current.Request.Params["Bankcode"];
            
            MobileUserBranch mB = branch.GetUserBranch(Convert.ToInt32(Branchcode), Convert.ToInt32(Bankcode));

            if (mB != null)
            {
                return mB;
            }
            else
            {
                MobileUserBranch m = new MobileUserBranch {
                    Branchname="Not found"
                };
                return m;
            }
        }


























        /*
         * 
         * 
         * 
         * 
         * 
         * // GET: api/BankDetails
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/BankDetails/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BankDetails
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/BankDetails/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BankDetails/5
        public void Delete(int id)
        {
        }*/
    }
}
