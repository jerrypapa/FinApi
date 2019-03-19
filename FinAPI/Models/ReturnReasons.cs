using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class ReturnReasons
    {
        public int RETURNREASONID { get; set; }
        public String RETURNREASONDESC { get; set; }
        public int STATUSID { get; set; }

        public List<ReturnReasons> getReturnResaons()
        {
            List<ReturnReasons> rrList = new List<ReturnReasons>();
            ReturnReasons rr = new ReturnReasons();
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;
            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "GetReturnReasons";
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    rr.RETURNREASONDESC= reader["RETURNREASONDESC"].ToString();
                    rr.RETURNREASONID= Int32.Parse(reader["RETURNREASONID"].ToString());
                    rr.STATUSID = Int32.Parse(reader["STATUSID"].ToString());
                    rrList.Add(rr);
                }
            }
            return rrList;
        }
    }
}