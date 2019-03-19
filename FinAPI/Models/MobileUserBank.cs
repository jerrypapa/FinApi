using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class MobileUserBank
    {
        public int Bankid { get; set; }
        public int Bankcode { get; set; }
        public String Bankname { get; set; }
        public String Bankabbrev { get; set; }
        public int ClearingBankCode { get; set; }
        public int Clearing { get; set; }
        public int Status { get; set; }

        public MobileUserBank() { }
        public MobileUserBank(int Bankid,int Bankcode,String Bankname,String Bankabbrev,int ClearingBankCode,int Clearing,int Status) {
            this.Bankid = Bankid;
            this.Bankcode = Bankcode;
            this.Bankname = Bankname;
            this.Bankabbrev = Bankabbrev;
            this.ClearingBankCode = ClearingBankCode;
            this.Clearing = Clearing;
            this.Status = Status;
        }
        
    }
}