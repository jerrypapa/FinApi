using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class RejectedCheque
    {
        private Cheque ch;
        private string v1;
        private string v2;
        private double v3;
        private string v4;
        private string v5;

        public String ChequeName { get; set; }
        public String ScannedMicr { get; set; }
        public String Micr { get; set; }
        public double Amount { get; set; }
        [Required]
        [Display(Name = "Reasons")]
        public String Reasons { get; set; }
        public String Currency { get; set; }
        public int Accountno { get; set; }
        public List<Cheque> RejectedCheques { get; set; }
        public List<RejectedCheque> RejectedChequesList { get; set; }
        public Cheque C { get; set; }

        public RejectedCheque() { }

        public RejectedCheque(String ChequeName,String ScannedMicr,String Micr,double Amount,String Reasons,String Currency) {
            this.ChequeName = ChequeName;
            this.Amount = Amount;
            this.Micr = Micr;
            this.ScannedMicr = ScannedMicr;
            this.Reasons = Reasons;
            this.Currency = Currency;
        }

        public RejectedCheque(String ChequeName, String ScannedMicr, String Micr, double Amount, String Reasons, String Currency,int Accountno)
        {
            this.ChequeName = ChequeName;
            this.Amount = Amount;
            this.Micr = Micr;
            this.ScannedMicr = ScannedMicr;
            this.Reasons = Reasons;
            this.Currency = Currency;
            this.Accountno = Accountno;
        }

        public RejectedCheque(Cheque C,String ChequeName, String ScannedMicr, String Micr, double Amount, String Reasons, String Currency, int Accountno)
        {
            this.ChequeName = ChequeName;
            this.Amount = Amount;
            this.Micr = Micr;
            this.ScannedMicr = ScannedMicr;
            this.Reasons = Reasons;
            this.Currency = Currency;
            this.Accountno = Accountno;
            this.C = C;
        }

        public RejectedCheque(Cheque ch, String ChequeName, String ScannedMicr, String Micr, double Amount, String Reasons, String Currency)
        {
            this.C = ch;
            this.ChequeName = ChequeName;
            this.Amount = Amount;
            this.Micr = Micr;
            this.ScannedMicr = ScannedMicr;
            this.Reasons = Reasons;
            this.Currency = Currency;
        }
    }
}