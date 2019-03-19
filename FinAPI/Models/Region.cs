using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class Region
    {
        public int RegionId { get; set; }
        public String RegionCode { get; set; }
        public String RegionName { get; set; }
        public String StatusId { get; set; }
    }
}