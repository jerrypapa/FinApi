using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class Account
    {
        public int CustomerNo { get; set; }
        public String AccountNo { get; set; }
        public String AccountName { get; set; }
        public int CurrencyId { get; set; }
        public int BranchId { get; set; }
        public int StatusId { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public int UploadMethodId { get; set; }
        public int AllowCredit { get; set; }
        public int AllowDebit { get; set; }
        public int ClientCode { get; set; }
        public int Active { get; set; }
        public String Password { get; set; }
        public int MobileUser { get; set; }

        public Account Login(Account acc)
        {
            int count = 0;
            Account account = new Account();
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
                string existQuery = "SELECT * FROM ACCOUNT WHERE EMAIL='" + acc.Email + "' AND PASSWORD='" + acc.Password + "'";
                //string existQuery = "SELECT * FROM [USER]";
                SqlCommand existQueryCmd = new SqlCommand(existQuery, conn);
                SqlDataReader reader = existQueryCmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    account.CustomerNo = Int32.Parse(reader["CUSTOMERNO"].ToString());
                    account.AccountNo = reader["ACCOUNTNO"].ToString();
                    account.AccountName = reader["ACCOUNTNAME"].ToString();
                    account.CurrencyId = int.Parse(reader["CURRENCYID"].ToString());
                    account.BranchId = int.Parse(reader["BRANCHID"].ToString());
                    account.StatusId = int.Parse(reader["STATUSID"].ToString());
                    account.Address = reader["ADDRESS"].ToString();
                    account.Email = reader["EMAIL"].ToString();
                    account.PhoneNumber = reader["PHONENUMBER"].ToString();
                    account.UploadMethodId = int.Parse(reader["UPLOADMETHODID"].ToString());
                    account.AllowCredit = int.Parse(reader.GetOrdinal("ALLOWCREDIT").ToString());
                    account.AllowDebit = int.Parse(reader.GetOrdinal("ALLOWDEBIT").ToString());
                    account.ClientCode = int.Parse(reader.GetOrdinal("CLIENTCODE").ToString());
                    account.Active = int.Parse(reader["ACTIVE"].ToString());
                    account.Password = reader["PASSWORD"].ToString();
                    account.MobileUser = int.Parse(reader.GetOrdinal("MOBILEUSER").ToString());
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
            return account;
        }

        public Account NewLogin(Account acc)
        {
            int count = 0;
            Account account = new Account();
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
                string existQuery = "SELECT * FROM ACCOUNT,PASSWORDLIST WHERE ACCOUNT.EMAIL='" + acc.Email + "' AND PASSWORDLIST.PASSWORD='" + acc.Password + "'";
                //string existQuery = "SELECT * FROM ACCOUNT WHERE EMAIL='" + acc.Email + "'";
                SqlCommand existQueryCmd = new SqlCommand(existQuery, conn);
                SqlDataReader reader = existQueryCmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    account.CustomerNo = Int32.Parse(reader["CUSTOMERNO"].ToString());
                    account.AccountNo = reader["ACCOUNTNO"].ToString();
                    account.AccountName = reader["ACCOUNTNAME"].ToString();
                    account.CurrencyId = int.Parse(reader["CURRENCYID"].ToString());
                    account.BranchId = int.Parse(reader["BRANCHID"].ToString());
                    account.StatusId = int.Parse(reader["STATUSID"].ToString());
                    account.Address = reader["ADDRESS"].ToString();
                    account.Email = reader["EMAIL"].ToString();
                    account.PhoneNumber = reader["PHONENUMBER"].ToString();
                    account.UploadMethodId = int.Parse(reader["UPLOADMETHODID"].ToString());
                    account.AllowCredit = int.Parse(reader.GetOrdinal("ALLOWCREDIT").ToString());
                    account.AllowDebit = int.Parse(reader.GetOrdinal("ALLOWDEBIT").ToString());
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
            return account;
        }
    }
}