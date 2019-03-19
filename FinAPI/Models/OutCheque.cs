using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class OutCheque
    {
        [Display(Name = "OutChequeId")]
        public int OutChequeId { get; set; }

        [Display(Name = "ProcNo")]
        public int ProcNo { get; set; }

        [Display(Name = "MLine")]
        public String MLine { get; set; }

        [Display(Name = "UserId")]
        public int UserId { get; set; }

        [Display(Name = "CustBranch")]
        public int CustBranch { get; set; }

        [Display(Name = "CustAccount")]
        public String CustAccount { get; set; }

        [Display(Name = "CustName")]
        public String CustName { get; set; }

        [Display(Name = "BankId")]
        public int BankId { get; set; }

        [Display(Name = "BranchId")]
        public int BranchId { get; set; }

        [Display(Name = "AccountNo")]
        public int AccountNo { get; set; }

        [Display(Name = "AccountName")]
        public String AccountName { get; set; }

        [Display(Name = "ChequeNo")]
        public int ChequeNo { get; set; }

        [Display(Name = "VoucherId")]
        public int VoucherId { get; set; }

        [Display(Name = "Amount")]
        public double Amount { get; set; }

        [Display(Name = "Manual")]
        public int Manual { get; set; }

        [Display(Name = "ClearingCodeId")]
        public int ClearingCodeId { get; set; }

        [Display(Name = "RegionId")]
        public int RegionId { get; set; }

        [Display(Name = "CheckDigit")]
        public String CheckDigit { get; set; }

        [Display(Name = "ValueDate")]
        public String ValueDate { get; set; }

        [Display(Name = "CurrencyId")]
        public int CurrencyId { get; set; }

        [Display(Name = "SlipId")]
        public int SlipId { get; set; }

        [Display(Name = "Remarks")]
        public String Remarks { get; set; }

        [Display(Name = "Captured")]
        public int Captured { get; set; }

        [Display(Name = "Verified")]
        public int Verified { get; set; }

        [Display(Name = "Authorized")]
        public int Authorized { get; set; }

        [Display(Name = "Uploaded")]
        public int Uploaded { get; set; }

        [Display(Name = "ACHCreated")]
        public int ACHCreated { get; set; }

        [Display(Name = "Upload")]
        public int Upload { get; set; }

        [Display(Name = "ACHGenerate")]
        public int ACHGenerate { get; set; }

        [Display(Name = "Returned")]
        public int Returned { get; set; }

        [Display(Name = "ReturnReasonId")]
        public int ReturnReasonId { get; set; }

        [Display(Name = "FileId")]
        public String FileId { get; set; }

        [Display(Name = "BackUpDate")]
        public String BackUpDate { get; set; }

        [Display(Name = "ChequeDate")]
        public String ChequeDate { get; set; }

        [Display(Name = "BatchNo")]
        public String BatchNo { get; set; }



        public ChequeImages chequeImages { get; set; }
    }
}