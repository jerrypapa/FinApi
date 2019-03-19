using FinAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinAPI.Controllers
{
    public class CurrencyApiController : ApiController
    {
        Currency currency = new Currency();
        [HttpGet]
        public List<Currency> Currencies()
        {
            return currency.GetCurrencies();
        }
    }
}
