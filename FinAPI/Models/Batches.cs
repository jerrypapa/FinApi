using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class Batches
    {
        public int BatchId { get; set; }
        public String BatchNo { get; set; }
        public int BranchId { get; set; }
        public int CurrencyId { get; set; }
        public int BatchType { get; set; }
        public int ClearingSessionid { get; set; }
        public int UserId { get; set; }
        public int Verifier { get; set; }
        public int Authorizer { get; set; }
        public int Day2 { get; set; }
        public int Captured { get; set; }
        public int Verified { get; set; }
        public int Authorized { get; set; }
        public Slip slip { get; set; }
        public List<Slip> SlipList { get; set; }
        public List<OutCheques> OutChequeList { get; set; }
        public List<ChequeImage> ChequeImagesList { get; set; }

        public String InsertBatch(Batches batch)
        {
            String inserted = "";
            int count = 0;
            DBConnect dbConnect;
            SqlConnection conn=null;
            try
            {
                dbConnect = new DBConnect();
                conn = dbConnect.OpenDBConn();
            }catch(Exception e)
            {
                inserted = "sqlexception-" + e.Message;
            }
            
            try
            {
                string existQuery = "SELECT * FROM BATCH WHERE BATCHNO='" + batch.BatchNo + "'";
                SqlCommand existQueryCmd = new SqlCommand(existQuery, conn);
                SqlDataReader reader = existQueryCmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                }
                reader.Close();
                //inserted = count.ToString();
                /*if (!reader.HasRows)
                {
                    count = 0;
                }
                else
                {
                    
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
                //Console.ReadKey();
            }
            if (count == 0)
            {
                inserted = "batchnotexists";
                string query =
                "INSERT INTO BATCH (BATCHNO,BRANCHID,CURRENCYID,BATCHTYPEID,CLEARINGSESSIONID,USERID,VERIFIER,AUTHORIZER,CAPTURED,VERIFIED)" +
                "VALUES" +
                "('" + batch.BatchNo + "','" + batch.BranchId + "','" + batch.CurrencyId + "','" + 1 + "','" + 1 + "','" + /*batch.UserId*/12 + "','" + 0 + "','" + 0 + "','" + 1 + "','" + 1 + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        inserted = "batchok";
                    }
                    else
                    {
                        inserted = "batchbad";
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
                inserted = "batchexists"+batch.BatchNo;
                conn.Close();
            }
            return inserted;
        }

        public Batches GetBatch(String BatchNo)
        {
            int count = 0;
            Batches b = new Batches();
            DBConnect dbConnect;
            SqlConnection conn = null;
            try
            {
                dbConnect = new DBConnect();
                conn = dbConnect.OpenDBConn();
            }
            catch (Exception e)
            {
                //inserted = "sqlexception-" + e.Message;
            }

            try
            {
                string existQuery = "SELECT * FROM BATCH WHERE BATCHNO='" + BatchNo + "'";
                SqlCommand existQueryCmd = new SqlCommand(existQuery, conn);
                SqlDataReader reader = existQueryCmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    b.BatchId = Int32.Parse(reader["BATCHID"].ToString());
                    b.BatchNo = reader["BATCHNO"].ToString();
                }
                reader.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                //inserted = e.Message + "\n" + e.StackTrace;
                //conn.Close();
            }
            finally
            {
                //Console.ReadKey();
            }
            return b;
        }
    }
}