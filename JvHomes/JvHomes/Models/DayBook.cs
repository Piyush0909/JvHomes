using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JvHomes.Models
{
    public class DayBook:Common
    {
        public List<DayBook> lstdaybook { get; set; }

        public string Amount { get; set; }
        public string Narration { get; set; }
        public string TransactionDate { get; set; }
        public string ChequeDate { get; set; }
        public string Credit { get; set; }
        public string AccountId { get; set; }
        public string LoginId { get; set; }
        public string TransactionNumber { get; set; }
        public string BankBranch { get; set; }
        public string BankName { get; set; }
        public string PaymentMode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Addres { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public string MemberAccNo { get; set; }
        public string MemberBankName { get; set; }
        public string MemberBranch { get;  set; }
        public string IFSCCode { get;  set; }
        public string ProfilePic { get;  set; }
        public string AdharNumber { get;  set; }
        public string AdharImage { get;  set; }
        public string AdharStatus { get;  set; }
        public string PanNumber { get;  set; }
        public string PanImage { get;  set; }
        public string PanStatus { get;  set; }
        public string DocumentImage { get;  set; }
        public string DocumentStatus { get;  set; }

        public DataSet SaveDayBook()
        {
            SqlParameter[] param = {
                                        new SqlParameter("@Amount", Amount),
                                        new SqlParameter("@Narration", Narration),
                                         new SqlParameter("@TransactionDate", TransactionDate),
                                          new SqlParameter("@AddedBy", AddedBy),
                                          new SqlParameter("@PaymentMode", PaymentMode),
                                          new SqlParameter("@LoginId", LoginId),
                                           new SqlParameter("@TransactionNo"  , TransactionNumber),
                                        new SqlParameter("@ChequeDate"  , TransactionDate),
                                        new SqlParameter("@BankName"  , BankName),
                                        new SqlParameter("@BankBranch"   , BankBranch),
                                          new SqlParameter("@AccountId", AccountId)
            };
            DataSet ds = Connection.ExecuteQuery("SaveDayBook", param);
            return ds;
        }
        public DataSet GetUserDetailsForExp()
        {
            SqlParameter[] param = {
                                        new SqlParameter("@LoginId", LoginId),
                                       
            };
            DataSet ds = Connection.ExecuteQuery("GetUserDetailsForExp", param);
            return ds;
        }
        public DataSet GetNameForLevelExpense()
        {
            SqlParameter[] param = {
                                        new SqlParameter("@LoginId", LoginId),
                                       
            };
            DataSet ds = Connection.ExecuteQuery("GetNameForLevelExpense", param);
            return ds;
        }
        public DataSet GetDayBook()
        {
            SqlParameter[] param = {
                                        new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@AddedBy", AddedBy),
            };
            DataSet ds = Connection.ExecuteQuery("GetDayBook", param);
            return ds;
        }
        public DataSet ExpenseList()
        {
            SqlParameter[] param = {
                                        new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", ToDate),
                                        new SqlParameter("@AddedBy", AddedBy),
                                     
            };
            DataSet ds = Connection.ExecuteQuery("GetExpenseList", param);
            return ds;
        }
    }
}