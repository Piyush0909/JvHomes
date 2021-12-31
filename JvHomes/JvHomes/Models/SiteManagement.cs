using System.Data;
using System.Data.SqlClient;

namespace JvHomes.Models
{
    public class SiteManagement:Common
    {
        public string Fk_SiteId { get; set; }
        public string Name { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string Narration { get; set; }
        public string Amount { get; set; }
        public string CreditTo { get; set; }

        public DataSet RecievePaymentOnSite()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_SiteId", Fk_SiteId),
                                        new SqlParameter("@Name", Name),
                                        new SqlParameter("@PaymentMode", PaymentMode),
                                        new SqlParameter("@TransactionNo", TransactionNo),
                                        new SqlParameter("@TransactionDate", TransactionDate),
                                        new SqlParameter("@BankName", BankName),
                                        new SqlParameter("@BankBranch", BankBranch),
                                        new SqlParameter("@Narration", Narration),
                                        new SqlParameter("@Amount", Amount),
                                        new SqlParameter("@AddedBy", AddedBy),
                                        new SqlParameter("@CreditTo", CreditTo),
            };
            DataSet ds = Connection.ExecuteQuery("RecievePaymentOnSite", para);
            return ds;
        }
        public DataSet ExpenseOnSite()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_SiteId", Fk_SiteId),
                                        new SqlParameter("@Name", Name),
                                        new SqlParameter("@PaymentMode", PaymentMode),
                                        new SqlParameter("@TransactionNo", TransactionNo),
                                        new SqlParameter("@TransactionDate", TransactionDate),
                                        new SqlParameter("@BankName", BankName),
                                        new SqlParameter("@BankBranch", BankBranch),
                                        new SqlParameter("@Narration", Narration),
                                        new SqlParameter("@Amount", Amount),
                                        new SqlParameter("@AddedBy", AddedBy),
                                        new SqlParameter("@CreditTo", CreditTo),
            };
            DataSet ds = Connection.ExecuteQuery("ExpenseOnSite", para);
            return ds;
        }
    }
}