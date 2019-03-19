using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class Batch
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "CHEQUE_COUNT")]
        public int CHEQUE_COUNT { get; set; }

        [Display(Name = "DATE_CREATED")]
        public String DATE_CREATED { get; set; }

        [Display(Name = "STATUS")]
        public String STATUS { get; set; }

        [Display(Name = "cheque")]
        public Cheque cheque { get; set; }

        [Display(Name = "chequeList")]
        public List<Cheque> chequeList { get; set; }

        [Display(Name = "OutChequeList")]
        public List<OutCheque> OutChequeList { get; set; }

        /*
         * BATCH TABLE
         * */
        [Display(Name = "BatchNo")]
        public String BatchNo { get; set; }

        [Display(Name = "BranchId")]
        public int BranchId { get; set; }

        [Display(Name = "CurrencyId")]
        public int CurrencyId { get; set; }

        [Display(Name = "BatchTypeId")]
        public int BatchTypeId { get; set; }

        [Display(Name = "ClearingSessionId")]
        public int ClearingSessionId { get; set; }

        [Display(Name = "UserId")]
        public int UserId { get; set; }

        public String insertBatch(Batch batch)
        {
            String inserted = "";
            int count = 0;

            /*
             * BATCHNO,BRANCHID,CURRENCYID,BATCHTYPEID,CLEARINGSESSIONID,USERID,VERIFIER,AUTHORIZER,DAY2,CAPTURED,VERIFIED,AUTHORIZED,UPLOADED,COMMISSIONED,BACKUPDATE,SYSTDATE,REC_BATCH
             * */

            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            SqlCommand sqlComm = new SqlCommand();
            /*sqlComm = conn.CreateCommand();
            sqlComm.CommandText = @"INSERT INTO BATCH BRANCHID,CURRENCYID,BATCHTYPEID,CLEARINGSESSIONID,USERID,VERIFIER,AUTHORIZER,DAY2,CAPTURED,VERIFIED,AUTHORIZED,UPLOADED,COMMISSIONED,BACKUPDATE,SYSTDATE,REC_BATCH VALUES (@paramName)";*/
            string existQuery = "SELECT * FROM BATCH WHERE BATCHO='"+batch.BatchNo+"'";
            SqlCommand existQueryCmd = new SqlCommand(existQuery, conn);
            try
            {
                SqlDataReader reader = existQueryCmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    count = 0;
                }else
                {
                    while (reader.Read())
                    {
                        count++;
                    }
                }
                
                
                /*int i = existQueryCmd.ExecuteNonQuery();
                if (i == 1)
                {
                    inserted = "ok";
                }
                else
                {
                    inserted = "fail";
                }*/
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
            if (count == 0)
            {
                string query =
                "INSERT INTO BATCH (BATCHNO,BRANCHID,CURRENCYID,BATCHTYPEID,CLEARINGSESSIONID,USERID,VERIFIER,AUTHORIZER)" +
                "VALUES" +
                "('" + batch.BatchNo + "','" + batch.BranchId + "','" + batch.CurrencyId + "','" + 1 + "','" + 1 + "','" + batch.UserId + "','" + 0 + "','" + 0 + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        existQuery = "SELECT * FROM OUTCHEQUE WHERE MLINE='" + batch.BatchNo + "'";
                        if (batch.OutChequeList.Count > 0)
                        {
                            List<OutCheque> outChequeList = batch.OutChequeList;
                            foreach (OutCheque oc in outChequeList)
                            {
                                if(existsCheck("OUTCHEQUE", oc.ChequeNo, oc.MLine).Equals("no"))
                                {
                                    String a = insertCheque(oc);
                                    return "batch ok" + a;
                                }
                                else
                                {
                                    inserted = "batch ok but cheque: "+oc.ChequeNo+" :already submitted";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            inserted = "batch ok empty cheque list";
                        }
                    }
                    else
                    {
                        inserted = "batch fail";
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
            }else
            {
                inserted = "batch exists: "+batch.BatchNo+" Count:"+count;
            }
            return inserted;
        }

        public String existsCheck(String tableName,int chequeno,String mline) {
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            SqlCommand sqlComm = new SqlCommand();

            String exists = "";
            int count=0;

            string existQuery = "SELECT * FROM OUTCHEQUE WHERE MLINE='" + mline + "' AND CHEQUENO='"+chequeno+"'";
            SqlCommand existQueryCmd = new SqlCommand(existQuery, conn);
            try
            {
                SqlDataReader reader = existQueryCmd.ExecuteReader();

                while (reader.Read())
                {
                    count++;
                }
                if (count == 0)
                {
                    exists = "no";
                }else if(count>0)
                {
                    exists = "yes";
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                exists = e.Message + "\n" + e.StackTrace;
                //conn.Close();
            }
            finally
            {
                conn.Close();
                //Console.ReadKey();
            }

            return exists;
        }

        public String insertCheque(OutCheque outcheque)
        {
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            SqlCommand sqlComm = new SqlCommand();

            String returnstatement = "";
            int inserted = 0;

            string insertQuery = "INSERT INTO OUTCHEQUE"+
                "(PROCNO,MLINE,USERID,CUSTBRANCH,CUSTACCOUNT,CUSTNAME,BANKID,BRANCHID,ACCOUNTNO,ACCOUNTNAME,CHEQUENO,VOUCHERID,AMOUNT,CLEARINGCODEID,REGIONID,CHECKDIGIT,CURRENCYID,SLIPID,CAPTURED,RETURNED,RETURNREASONID,CHEQUEDATE)"+
                "VALUES"+
                "('"+outcheque.BatchNo+ "','"+outcheque.MLine+ "','"+outcheque.UserId+ "','"+outcheque.CustBranch+ "','"+outcheque.CustAccount+ "','"+outcheque.CustName+ "','"+outcheque.BankId+ "','"+outcheque.BranchId+ "','"+outcheque.AccountNo+"',"+
                "'"+outcheque.AccountName+ "','"+outcheque.ChequeNo+ "','"+5+ "','"+outcheque.Amount+ "','"+1+ "','"+1+ "','"+102+ "','"+1003+ "','"+outcheque.ChequeNo+ "','"+1+ "','"+0+ "','"+4+ "','"+outcheque.ChequeDate+"')";
            SqlCommand insertQueryCmd = new SqlCommand(insertQuery, conn);
            try
            {
                inserted = insertQueryCmd.ExecuteNonQuery();
                if (inserted == 1)
                {
                    returnstatement = "cheque inserted";
                }else
                {
                    returnstatement = "cheque not inserted";
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                returnstatement = e.Message + "\n" + e.StackTrace;
                //conn.Close();
            }
            finally
            {
                conn.Close();
                //Console.ReadKey();
            }

            return returnstatement;
        }
    }
}