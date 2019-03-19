using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class MobileUserBranch:MobileUserBank
    {
        public int Branchid { get; set; }
        //public int Bankid { get; set; }
        public int Branchcode { get; set; }
        public String Branchname { get; set; }
        public int Locationtype { get; set; }
        public int Region { get; set; }
        public int Branchstatus { get; set; }
        public int Headoffice { get; set; }
        public MobileUserBranch Branch { get; set; }

        public MobileUserBranch() { }

        public MobileUserBranch(int Branchid,int Branchcode,String Branchname,int Locationtype,int Region,int Branchstatus/*,int Headoffice*/) {
            this.Branchid = Branchid;
            this.Branchcode = Branchcode;
            this.Branchname = Branchname;
            this.Locationtype = Locationtype;
            this.Region = Region;
            this.Branchstatus = Branchstatus;
            //this.Headoffice = Headoffice;
        }

        public MobileUserBranch(int Branchid, int Branchcode, String Branchname, int Locationtype, int Region, int Branchstatus, /*int Headoffice,*/ int Bankid, int Bankcode, String Bankname, String Bankabbrev, int ClearingBankCode, /*int Clearing,*/ int Status)
        {
            this.Branchid = Branchid;
            this.Branchcode = Branchcode;
            this.Branchname = Branchname;
            this.Locationtype = Locationtype;
            this.Region = Region;
            this.Branchstatus = Branchstatus;
            //this.Headoffice = Headoffice;

            this.Bankid = Bankid;
            this.Bankcode = Bankcode;
            this.Bankname = Bankname;
            this.Bankabbrev = Bankabbrev;
            this.ClearingBankCode = ClearingBankCode;
            //this.Clearing = Clearing;
            this.Status = Status;
        }

        public MobileUserBranch(MobileUserBranch Branch, int Bankid, int Bankcode, String Bankname, String Bankabbrev, int ClearingBankCode, /*int Clearing,*/ int Status)
        {
            this.Branch = Branch;

            this.Bankid = Bankid;
            this.Bankcode = Bankcode;
            this.Bankname = Bankname;
            this.Bankabbrev = Bankabbrev;
            this.ClearingBankCode = ClearingBankCode;
            //this.Clearing = Clearing;
            this.Status = Status;
        }

        public MobileUserBranch GetUserBranch(int BranchCode,int BCode) {
            MobileUserBranch branch = null;
            MobileUserBranch bankbranch = null;

            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();

            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "GetUserBranch";
                com.Parameters.Add("@branchcode", SqlDbType.Int).Value =
                    BranchCode;

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    this.Branchid = Convert.ToInt32(reader["BRANCHID"].ToString());
                    this.Branchcode = Convert.ToInt32(reader["BRANCHCODE"].ToString());
                    this.Branchname = reader["BRANCHNAME"].ToString();
                    this.Locationtype = Convert.ToInt32(reader["LOCATIONTYPEID"].ToString());
                    this.Region = Convert.ToInt32(reader["REGIONID"].ToString());
                    this.Branchstatus = Convert.ToInt32(reader["STATUSID"].ToString());
                    //this.Headoffice = Convert.ToInt32(reader["HEADOFFICE"].ToString());

                    branch = new MobileUserBranch(
                        Convert.ToInt32(reader["BRANCHID"].ToString()),
                        Convert.ToInt32(reader["BRANCHCODE"].ToString()),
                        reader["BRANCHNAME"].ToString(),
                        Convert.ToInt32(reader["LOCATIONTYPEID"].ToString()),
                        Convert.ToInt32(reader["REGIONID"].ToString()),
                        Convert.ToInt32(reader["STATUSID"].ToString())/*,
                        Convert.ToInt32(reader["HEADOFFICE"].ToString())*/
                    );
                }
                reader.Close();
            }

            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "GetUserBankdETAILS";
                com.Parameters.Add("@bankcode", SqlDbType.VarChar).Value =
                    BCode;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    bankbranch = new MobileUserBranch(
                        branch,
                        Convert.ToInt32(reader["BANKID"].ToString()),
                        Convert.ToInt32(reader["BANKCODE"].ToString()),
                        reader["BANKNAME"].ToString(),
                        reader["BANKABBREV"].ToString(),
                        Convert.ToInt32(reader["CLEARINGBANKCODE"].ToString()),
                        /*Convert.ToInt32(reader["CLEARING"].ToString()),*/
                        Convert.ToInt32(reader["STATUSID"].ToString())
                    );
                }

                return bankbranch;
            }
        }

    }
}