using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JvHomes.Models
{
    public class Reports : Common
    {
        public string PLCPerc { get; set; }

        public DataTable documentList { get; set; }
        public string DocumentType { get; set; }
        public string PK_DocumentID { get; set; }
        public string LoginId { get; set; }
        public string Month { get; set; }
        public string UserName { get; set; }
        public string Fk_UserId { get; set; }
        public string LoginIDD { get; set; }
        public DataTable userList { get; set; }
        public string Name { get; set; }
        public string BookingNumber { get; set; }
        public string FromDate { get; set; }
        public string Todate { get; set; }
        public string FromSearchDate { get; set; }
        public string ToSearchDate { get; set; }
        public string Mobile { get; set; }
        public string SponsorId { get; set; }
        public string Leg { get; set; }
        public string PayoutNo { get; set; }
        public string Status { get; set; }
        public DataTable directList { get; set; }
        public string id { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string Fk_BankId { get; set; }
        public string BlockID { get; set; }
        public string PlotNumber { get; set; }
        public string PaymentType { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionNumber { get; set; }
        public string TransactionDate { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public DataTable lstDetails { get; set; }
        public DataTable dtDetails { get; set; }
        public DataTable payoutData { get; set; }
        public DataTable customerLedger { get; set; }
        public string CommissionType { get; set; }
        public string MemberAccNo { get; set; }
        public string MemberBankName { get; set; }
        public string MemberBranch { get; set; }
        public string IFSCCode { get; set; }
        public DataTable associateDetails { get;  set; }
        public DataTable bookingData { get;  set; }
        public DataTable bookingDetails { get;  set; }
        public DataTable advanaceDetails { get;  set; }
        public string BookingNo { get;  set; }
        public string InstallmentNo { get;  set; }
        public string InstallmentDate { get;  set; }
        public string InstAmt { get;  set; }
        public DataTable siteDetails { get; set; }
        public DataTable paymentDetails { get; set; }
        public string Particular { get;  set; }
        public string NetPlotAmount { get;  set; }
        public string PaidAmount { get;  set; }
        public string Balance { get; set; }
        public List<Reports> details { get; set; }
        public string SiteName { get; set; }
        public string TotalArea { get; set; }
        public string PlotRate { get; set; }
        public string PlotAmount { get;  set; }
        public string TotalPlotAmount { get;  set; }
        public string TotalPaidAmount { get;  set; }
        public string TotalBalance { get;  set; }
        public string CreditTo { get; set; }
        public string PK_LedgerID { get;  set; }

        public DataSet GettingUserProfile()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId)};
            DataSet ds = Connection.ExecuteQuery("GetUserProfile", para);
            return ds;
        }
        public DataSet GetDirectBusinessReport()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId)};
            DataSet ds = Connection.ExecuteQuery("GetDirectBusinessReport", para);
            return ds;
        }
        public DataSet GetPendingDocuments()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetAgentListForKYC", para);
            return ds;
        }
        public DataSet GetLevelMemeberList()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_UserId", Fk_UserId),

            };
            DataSet ds = Connection.ExecuteQuery("GetLevelMemeberList", para);
            return ds;
        }
        public DataSet GetKYC()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetKYC", para);
            return ds;
        }
        public DataSet DeleteDayBook()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PK_LedgerID", PK_LedgerID),
                                      new SqlParameter("@DeletedBy", AddedBy),
            };
            DataSet ds = Connection.ExecuteQuery("DeleteDayBook", para);
            return ds;
        }
        public DataSet ApproveKYC()
        {
            SqlParameter[] para = {


                                      new SqlParameter("@PK_DocumentID", PK_DocumentID),
                                      new SqlParameter("@DocumentType", DocumentType),
                                       new SqlParameter("@Status", Status),
                                       new SqlParameter("@UpdatedBy", UpdatedBy),
            };
            DataSet ds = Connection.ExecuteQuery("ApproveKYC", para);
            return ds;
        }
        public DataSet GetPaymentList()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@CustomerId",LoginId),
                                  new SqlParameter("@AssociateId",SponsorId),
                                   new SqlParameter("@BookingNo",BookingNumber)
                            };
            DataSet ds = Connection.ExecuteQuery("GetPaymentListForApproval", para);
            return ds;
        }
        public DataSet ApprovePayment()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@Pk_BookingDetailsId",id),

                                   new SqlParameter("@AddedBy",AddedBy)

                            };
            DataSet ds = Connection.ExecuteQuery("ApprovePayment", para);
            return ds;
        }
        public DataSet RejectPayment()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@Pk_BookingDetailsId",id),

                                   new SqlParameter("@AddedBy",AddedBy)

                            };
            DataSet ds = Connection.ExecuteQuery("RejectPayment", para);
            return ds;
        }
        public DataSet DeletePayment()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@Pk_BookingDetailsId",id),

                                   new SqlParameter("@AddedBy",AddedBy)

                            };
            DataSet ds = Connection.ExecuteQuery("DeletePayment", para);
            return ds;
        }
        public DataSet GetPayoutReport()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PayoutNo", PayoutNo),

                                         new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", Todate),

            };
            DataSet ds = Connection.ExecuteQuery("PayoutReport", para);
            return ds;
        }
        public DataSet GetDircetPayout()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PayoutNo", PayoutNo),

                                         new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", Todate),

            };
            DataSet ds = Connection.ExecuteQuery("PayoutReportForDirect", para);
            return ds;
        }
        public DataSet GetBusinessReport()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                        new SqlParameter("@Month", Month),
                                        new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", Todate)

                                 };
            DataSet ds = Connection.ExecuteQuery("GetDirectBusinessReport", para);
            return ds;
        }

        public DataSet GetClubIncome()
        {
            DataSet ds = Connection.ExecuteQuery("GetClubIncome");
            return ds;
        }
        public DataSet GetDueDetails()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@BookingNo", BookingNo),

            };
            DataSet ds = Connection.ExecuteQuery("GetDueDetails", para);
            return ds;
        }
        public DataSet GetAssociateList()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@AssociateLoginID", LoginId),
                                        new SqlParameter("@SponsorLoginID", SponsorId),
                                        new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", Todate),
                                        new SqlParameter("@Mobile", Mobile),
                                         new SqlParameter("@CommissionType", CommissionType),
                                         new SqlParameter("@Status", Status)
            };
            DataSet ds = Connection.ExecuteQuery("SelectAssociate", para);
            return ds;
        }
        public DataSet GetLedger()
        {
            SqlParameter[] para = {

                                        new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", Todate)

            };
            DataSet ds = Connection.ExecuteQuery("GetLedger", para);
            return ds;
        }
        public DataSet GetSiteLedger()
        {
            SqlParameter[] para = {

                                        new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", Todate),
                                        new SqlParameter("@SiteID", SiteID)

            };
            DataSet ds = Connection.ExecuteQuery("GetSiteLedger", para);
            return ds;
        }
        public DataSet GetDirectList()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@Name", Name),
                                          new SqlParameter("@FromDate", FromDate),
                                           new SqlParameter("@Todate", Todate),
                                            new SqlParameter("@Status", Status),
                                            new SqlParameter("@Leg", Leg)
            };
            DataSet ds = Connection.ExecuteQuery("GetDirectList", para);
            return ds;
        }
        public DataSet GetDownline()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@Name", Name),
                                          new SqlParameter("@FromDate", FromDate),
                                           new SqlParameter("@Todate", Todate),
                                            new SqlParameter("@Status", Status),
                                            new SqlParameter("@Leg", Leg)
            };
            DataSet ds = Connection.ExecuteQuery("DownlineList", para);
            return ds;
        }
        public DataSet GetBookingDetailsList()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@CustomerID", LoginId),
                                      new SqlParameter("@AssociateID", SponsorId),
                                      new SqlParameter("@BookingNo", BookingNumber),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", Todate),
                                      new SqlParameter("@FK_SiteID", SiteID),
                                      new SqlParameter("@FK_SectorID", SectorID),
                                      new SqlParameter("@FK_BlockID", BlockID),
                                      new SqlParameter("@PlotNumber", PlotNumber),
                                  };

            DataSet ds = Connection.ExecuteQuery("GetCancelBooking", para);
            return ds;
        }
        public DataSet GetCollectionReport()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_SiteId", SiteID),
                                      new SqlParameter("@Fk_SectorId", SectorID),
                                      new SqlParameter("@Fk_BlockId", BlockID),
                                      new SqlParameter("@PlotNo", PlotNumber),
                                      new SqlParameter("@AssociateId", SponsorId),
                                      new SqlParameter("@CustomerId", LoginId),
                                      new SqlParameter("@BookingNo", BookingNumber),
                                      new SqlParameter("@PaymentType", PaymentType),
                                      new SqlParameter("@PaymentMode", PaymentMode),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", Todate),
                new SqlParameter("@AddedBy", AddedBy),
                                  };

            DataSet ds = Connection.ExecuteQuery("GetCollectionReport", para);
            return ds;
        }

        public DataSet GetChequeBounceReport()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_SiteId", SiteID),
                                      new SqlParameter("@Fk_SectorId", SectorID),
                                      new SqlParameter("@Fk_BlockId", BlockID),
                                      new SqlParameter("@PlotNo", PlotNumber),
                                      new SqlParameter("@AssociateId", SponsorId),
                                      new SqlParameter("@CustomerId", LoginId),
                                      new SqlParameter("@BookingNo", BookingNumber),
                                      new SqlParameter("@PaymentType", PaymentType),
                                      new SqlParameter("@PaymentMode", PaymentMode),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", Todate),
                                  };

            DataSet ds = Connection.ExecuteQuery("GetChequeBounceReport", para);
            return ds;
        }

        public DataSet DueInstallmentReport()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_SiteId", SiteID),
                                      new SqlParameter("@Fk_SectorId", SectorID),
                                      new SqlParameter("@Fk_BlockId", BlockID),
                                      new SqlParameter("@PlotNo", PlotNumber),
                                      new SqlParameter("@AssociateId", SponsorId),
                                      new SqlParameter("@CustomerId", LoginId),
                                      new SqlParameter("@BookingNo", BookingNumber),
                                      new SqlParameter("@PaymentType", PaymentType),

                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", Todate),
                                  };

            DataSet ds = Connection.ExecuteQuery("DueInstallmentReport", para);
            return ds;
        }
        public DataSet GetUplineDetails()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetUplineAssociateDetails", para);
            return ds;
        }
        public DataSet GetDownlineDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetDownlineAssociateDetails", para);
            return ds;
        }
        public DataSet GetPayPayout()
        {
            DataSet ds = Connection.ExecuteQuery("GetPayPayout");
            return ds;
        }
        public DataSet CustomerLedger()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                        new SqlParameter("@Fk_BookingId", id)
            };
            DataSet ds = Connection.ExecuteQuery("CustomerLedger", para);
            return ds;
        }
        public DataSet GetPayoutRequest()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@FromDate", FromDate),
                                          new SqlParameter("@ToDate", Todate)
            };
            DataSet ds = Connection.ExecuteQuery("GetPayoutRequest", para);
            return ds;
        }
        public DataSet GetPlotAreaReport()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Status", Status),
                                         new SqlParameter("@SiteID", SiteID),
                                            new SqlParameter("@FromDate", FromDate),
                                          new SqlParameter("@ToDate", Todate)

            };
            DataSet ds = Connection.ExecuteQuery("GetPlotAreaReport", para);
            return ds;
        }
        public DataSet GetPlotCount()
        {
            SqlParameter[] para = {
                                  
                                         new SqlParameter("@SiteID", SiteID),
                                       

            };
            DataSet ds = Connection.ExecuteQuery("GetPlotCount", para);
            return ds;
        }
        public DataSet GetEmpLedger()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),

            };
            DataSet ds = Connection.ExecuteQuery("GetEmpLedger", para);
            return ds;
        }
        public DataSet GetAssociateLedger()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),

            };
            DataSet ds = Connection.ExecuteQuery("GetAssociateLedger", para);
            return ds;
        }
      
        public DataSet GlobalSearch()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),

            };
            DataSet ds = Connection.ExecuteQuery("GlobalSearch", para);
            return ds;
        }
        public DataSet GetBankTransaction()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_BankId", Fk_BankId),
                                        new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", Todate),
            };
            DataSet ds = Connection.ExecuteQuery("GetBankTransaction", para);
            return ds;
        }
        public DataSet GetBalanceSheet()
        {
            
            DataSet ds = Connection.ExecuteQuery("GetBalanceSheet");
            return ds;
        }
    }
}