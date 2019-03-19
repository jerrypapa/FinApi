using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class BatchMaster
    {
        [Display(Name = "BatchMasterId")]
        public int BatchMasterId { get; set; }

        [Display(Name = "BatchNo")]
        public int BatchNo { get; set; }

        [Display(Name = "BatchTypeId")]
        public int BatchTypeId { get; set; }

        [Display(Name = "UserId")]
        public int UserId { get; set; }
    }
}