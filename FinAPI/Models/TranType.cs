using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class TranType
    {
        public int TranTypeId { get; set; }
        public String TranTypeCode { get; set; }
        public String TranTypeDesc { get; set; }
        public int StatusId { get; set; }
    }
}