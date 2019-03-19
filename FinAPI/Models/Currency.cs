using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public String CurrencyCode { get; set; }
        public String CurrencyName { get; set; }
        public String UploadCode { get; set; }
        public String ISOCode { get; set; }
        public double Valuecap { get; set; }
        public double ExchangeRate { get; set; }
        public int RoundCents { get; set; }
        public int StatusId { get; set; }

        public List<Currency> GetCurrencies()
        {
            int count = 0;
            
            List<Currency> currencyList = new List<Currency>();
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
                string existQuery = "SELECT * FROM CURRENCY";
                SqlCommand existQueryCmd = new SqlCommand(existQuery, conn);
                SqlDataReader reader = existQueryCmd.ExecuteReader();
                while (reader.Read())
                {
                    Currency c = new Currency();
                    count++;
                    c.CurrencyId = Int32.Parse(reader["CURRENCYID"].ToString());
                    c.CurrencyCode = reader["CURRENCYCODE"].ToString();
                    c.CurrencyName = reader["CURRENCYNAME"].ToString();
                    c.UploadCode = reader["UPLOADCODE"].ToString();
                    c.ISOCode = reader["ISOCODE"].ToString();
                    c.Valuecap = Double.Parse(reader["VALUECAP"].ToString());
                    c.ExchangeRate = Double.Parse(reader["EXCHANGERATE"].ToString());
                    c.RoundCents = int.Parse(reader.GetOrdinal("ROUNDCENTS").ToString());
                    c.StatusId = int.Parse(reader["STATUSID"].ToString());

                    currencyList.Add(c);
                }
                reader.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
            return currencyList;
        }
    }
}