using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class OutCheques
    {
        public String ProcNo { get; set; }
        public String MLine { get; set; }
        public int UserId { get; set; }
        public int CustBranch { get; set; }
        public String CustAccount { get; set; }
        public String CustName { get; set; }
        public int BankId { get; set; }
        public int BranchId { get; set; }
        public String AccountNo { get; set; }
        public String AccountName { get; set; }
        public String ChequeNo { get; set; }
        public int VoucherId { get; set; }
        public double Amount { get; set; }
        public int Manual { get; set; }
        public int ClearingCode { get; set; }
        public int RegionId { get; set; }
        public int CheckDigit { get; set; }
        public String ValueDate { get; set; }
        public int CurrencyId { get; set; }
        public int SlipId { get; set; }
        public String SlipNo { get; set; }
        public String Remarks { get; set; }
        public int Captured { get; set; }
        public int Verified { get; set; }
        public int Authorized { get; set; }
        public int Uploaded { get; set; }
        public int ACHCreated { get; set; }
        public int Upload { get; set; }
        public int ACHGenerate { get; set; }
        public int Returned { get; set; }
        public int ReaturnedResonId { get; set; }
        public String FileId { get; set; }
        public String ChequeDate { get; set; }
        public int EmailNotified { get; set; }
        public int StatusId { get; set; }

        public String InsertOutCheque(OutCheques outcheque)
        {
            String inserted = "";
            int count = 0;
            DBConnect dbConnect;
            SqlConnection conn = null;
            try
            {
                dbConnect = new DBConnect();
                conn = dbConnect.OpenDBConn();
            }
            catch (Exception e)
            {
                inserted = "sqlexception-" + e.Message;
            }

            try
            {
                string existQuery = "SELECT * FROM OUTCHEQUE WHERE CHEQUENO='" + outcheque.ChequeNo + "' AND MLINE='" + outcheque.MLine + "' and PROCNO='" + outcheque.ProcNo + "'";
                SqlCommand existQueryCmd = new SqlCommand(existQuery, conn);
                SqlDataReader reader = existQueryCmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                }
                reader.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                inserted = e.Message + "\n" + e.StackTrace;
                //conn.Close();
            }
            finally
            {

            }
            if (count == 0)
            {
                inserted = "outchequenotexists";
                /*
                 [PROCNO]
      ,[MLINE]
      ,[USERID]
      ,[CUSTBRANCH]
      ,[CUSTACCOUNT]
      ,[CUSTNAME]
      ,[BANKID]
      ,[BRANCHID]
      ,[ACCOUNTNO]
      ,[ACCOUNTNAME]
      ,[CHEQUENO]
      ,[VOUCHERID]
      ,[AMOUNT]
      ,[MANUAL]
      ,[CLEARINGCODEID]
      ,[REGIONID]
      ,[CHECKDIGIT]
      ,[VALUEDATE]
      ,[CURRENCYID]
      ,[SLIPID]
      ,[REMARKS]
      ,[CAPTURED]
      ,[VERIFIED]
      ,[AUTHORIZED]
      ,[UPLOADED]
      ,[ACHCREATED]
      ,[UPLOAD]
      ,[ACHGENERATE]
      ,[RETURNED]
      ,[RETURNREASONID]
      ,[FILEID]
      ,[BACKUPDATE]
      ,[SYSTDATE]
      ,[CHEQUEDATE]*/
                string query =
                "INSERT INTO OUTCHEQUE (PROCNO,MLINE,USERID,CUSTBRANCH,CUSTACCOUNT,CUSTNAME,BANKID,BRANCHID,"+
                "ACCOUNTNO,ACCOUNTNAME,CHEQUENO,VOUCHERID,AMOUNT,MANUAL,CLEARINGCODEID,REGIONID,CHECKDIGIT,VALUEDATE,"+
                "CURRENCYID,SLIPID,CAPTURED,RETURNED,RETURNREASONID,VERIFIED)" +
                "VALUES" +
                "('" + outcheque.ProcNo + "','" + outcheque.MLine + "','" + /*outcheque.UserId*/12 + "',"+
                "'" + outcheque.CustBranch + "','" + outcheque.CustAccount + "','" + outcheque.CustName + "','" + outcheque.BankId + "','" + outcheque.BranchId + "','" + outcheque.AccountNo + "',"+
                "'"+outcheque.AccountName+"','"+outcheque.ChequeNo+"','"+5+"','"+outcheque.Amount+"','"+0+"','"+1+"','"+ 2 + "','"+outcheque.CheckDigit+"','2019-02-28 11:28:00','"+outcheque.CurrencyId+"',"+
                "'"+outcheque.SlipId+"','"+1+"','"+0+"','"+4+ "','" + 1 + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        inserted = "outchequeok";
                    }
                    else
                    {
                        inserted = "outchequebad";
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    inserted = e.Message + "\n" + e.StackTrace;
                    //conn.Close();
                }
                finally
                {
                    conn.Close();
                    //Console.ReadKey();
                }
            }
            else
            {
                inserted = "outchequeexists" + outcheque.ChequeNo;
                conn.Close();
            }
            return inserted;
        }
    }
}