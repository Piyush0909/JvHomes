using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JvHomes.Models
{
    public class Wallet : Common
    {
        public string Fk_UserId { get; set; }
        public string LoginId { get; set; }
        public string Amount { get; set; }
        public string Narration { get; set; }
        public string Credit { get; set; }
        public string TransactionDate { get; set; }
        public string PaymentDate { get; set; }
        public string PK_RequestID { get; set; }
        public string Debit { get; set; }
        public List<Wallet> lstDetails { get; set; }
        public DataTable ledgerList { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionNo { get; set; }
        public string BankName { get; set; }
        public string DocumentImg { get; set; }
        public string BankBranch { get; set; }
        public string CreditTo { get; set; }
        public DataTable advancePayment { get; set; }
        public string TransactionNumber { get;  set; }
        public string Fk_BankId { get;  set; }

        public DataSet GetPayoutBalance()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId),
                                      };
            DataSet ds = Connection.ExecuteQuery("GetPayoutBalance", para);
            return ds;
        }
        public DataSet SavePayoutRequest()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Amount", Amount),
                                    new SqlParameter("@AddedBy", AddedBy),
                                      };
            DataSet ds = Connection.ExecuteQuery("PayoutRequest", para);
            return ds;
        }
        public DataSet PayoutLedger()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                       new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", ToDate),

                                     };
            DataSet ds = Connection.ExecuteQuery("GetPayoutLedger", para);
            return ds;
        }
        public DataSet ApprovePayoutRequest()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_UserId", Fk_UserId),
             new SqlParameter("@ApprovedBy", AddedBy),
             new SqlParameter("@PaymentMode", PaymentMode),
             new SqlParameter("@TransactionNumber", TransactionNumber),
             new SqlParameter("@TransactionDate", TransactionDate),
             new SqlParameter("@Amount", Amount),
             new SqlParameter("@BankName", BankName),
             new SqlParameter("@BankBranch", BankBranch),
              new SqlParameter("@Fk_BankId", Fk_BankId),
            };
            DataSet ds = Connection.ExecuteQuery("ApprovePayoutRequest", para);

            return ds;

        }
        public DataSet AdvancePayment()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginID", LoginId),
                                        new SqlParameter("@Amount", Amount),
                new SqlParameter("@Description", Narration),
                                         new SqlParameter("@PaymentMode", PaymentMode),
                                         new SqlParameter("@TransactionNo",TransactionNo),
                                          new SqlParameter("@TransactionDate", TransactionDate),
                                           new SqlParameter("@BankName", BankName),
                                            new SqlParameter("@BankBranch", BankBranch),
                                        new SqlParameter("@AddedBy", AddedBy),
                new SqlParameter("@PaymentDate", PaymentDate),
                 new SqlParameter("@CreditTo", CreditTo)



                                     };
            DataSet ds = Connection.ExecuteQuery("AdvancePayment", para);
            return ds;
        }
        public DataSet RecievePaymentForTeamLedger()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                        new SqlParameter("@Amount", Amount),
                                         new SqlParameter("@PaymentMode", PaymentMode),
                                          new SqlParameter("@PaymentDate", PaymentDate),
                                         new SqlParameter("@Narration", Narration),
                                        new SqlParameter("@BankName", BankName),
                                         new SqlParameter("@TransactionNo",TransactionNo),
                                          new SqlParameter("@TransactionDate", TransactionDate),                                           
                                            new SqlParameter("@BankBranch", BankBranch),
                                             new SqlParameter("@CreditTo", CreditTo),
                                        new SqlParameter("@AddedBy", AddedBy),
              
                



                                     };
            DataSet ds = Connection.ExecuteQuery("RecievePaymentForTeamLedger", para);
            return ds;
        }
        public DataSet ExpensesForTeamLedger()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                        new SqlParameter("@Amount", Amount),
                                         new SqlParameter("@PaymentMode", PaymentMode),
                                          new SqlParameter("@PaymentDate", PaymentDate),
                                         new SqlParameter("@Narration", Narration),
                                        new SqlParameter("@BankName", BankName),
                                         new SqlParameter("@TransactionNo",TransactionNo),
                                          new SqlParameter("@TransactionDate", TransactionDate),
                                            new SqlParameter("@BankBranch", BankBranch),
                                             new SqlParameter("@CreditTo", CreditTo),
                                        new SqlParameter("@AddedBy", AddedBy),





                                     };
            DataSet ds = Connection.ExecuteQuery("ExpensesForTeamLedger", para);
            return ds;
        }
        public DataSet AdvancePaymentReport()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginID", LoginId),
                                     new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate)

                                     };
            DataSet ds = Connection.ExecuteQuery("AdvancePaymentReport", para);
            return ds;
        }
    }
}