using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class Slip
    {
        public int SlipId { get; set; }
        public String SlipNo { get; set; }
        //fk
        public int BatchId { get; set; }
        //fk
        public int CustBranchId { get; set; }
        public String CustAccount { get; set; }
        public int ItemCount { get; set; }
        public double ItemSum { get; set; }
        public String Remarks { get; set; }
        public int Bulk { get; set; }
        public int Captured { get; set; }
        public int Verified { get; set; }
        public int Authorized { get; set; }
        public int Uploaded { get; set; }
        public String BackUpDate { get; set; }
        public String SystemDate { get; set; }

        public String InsertSlip(Slip slip)
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

            try{
                string existQuery = "SELECT * FROM SLIP WHERE SLIPNO='" + slip.SlipNo + "'";
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
                inserted = "slipnotexists";
                string query =
                "INSERT INTO SLIP (SLIPNO,BATCHID,CUSTBRANCHID,CUSTACCOUNT,ITEMCOUNT,ITEMSUM,CAPTURED,VERIFIED)" +
                "VALUES" +
                "('" + slip.SlipNo + "','" + slip.BatchId + "','" + slip.CustBranchId + "','" + slip.CustAccount + "','" + slip.ItemCount + "','" + slip.ItemSum + "','" + 1 + "','" + 1 + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        inserted = "slipok";
                    }
                    else
                    {
                        inserted = "slipbad";
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
                inserted = "slipexists" + slip.SlipNo;
                conn.Close();
            }

            return inserted;
        }

        public Slip GetSlip(String SlipNo)
        {
            int count = 0;
            Slip S = new Slip();
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
                string existQuery = "SELECT * FROM SLIP WHERE SLIPNO='" + SlipNo + "'";
                SqlCommand existQueryCmd = new SqlCommand(existQuery, conn);
                SqlDataReader reader = existQueryCmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    S.SlipId = Int32.Parse(reader["SLIPID"].ToString());
                    S.SlipNo = reader["SLIPNO"].ToString();
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
            return S;
        }
    }
}