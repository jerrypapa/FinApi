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
    public class IssueChequeApiController : ApiController
    {
        Users user = new Users();
        IssuedCheque issuedCheque = new IssuedCheque();

        [HttpPost]
        public IssuedCheque NewIssue()
        {
            IssuedCheque ic = new IssuedCheque();
            String accountno = HttpContext.Current.Request.Params["Accountno"];
            String chequeMicr = HttpContext.Current.Request.Params["micr"];
            Double amount = Double.Parse(HttpContext.Current.Request.Params["Amount"]);
            String dateIssued = HttpContext.Current.Request.Params["DateIssued"];
            String dateToBePresented = HttpContext.Current.Request.Params["DateToBePresented"];
            String currency = HttpContext.Current.Request.Params["Currency"];
            String acccountname = HttpContext.Current.Request.Params["AccountName"];

            ic.Accountno = Int32.Parse(accountno);
            ic.micr = chequeMicr;
            ic.Amount = amount;
            ic.DateIssued = dateIssued;
            ic.DateToBeSubmitted = dateToBePresented;
            ic.Currency = currency;
            ic.DraweeName = acccountname;

            try
            {
                String i = issuedCheque.IssueCheque(ic);
                /*if (i == 1)
                {
                    ic.response = "ok";

                }
                else
                {
                    ic.response = "failed";
                }*/
                ic.response = i;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
            }

            return ic;
        }

        [HttpGet]
        public String Push()
        {
            String s = "";
            s = issuedCheque.pushNotification("147258369875421399852");

            return s;
        }
    }
}
