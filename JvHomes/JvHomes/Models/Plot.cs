using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JvHomes.Models
{
    public class Plot : Common
    {
        public bool IsDownline { get; set; }
        public string Downline { get; set; }
        public string CalculatedWith { get; set; }
        public string MLMLoginId { get; set; }
        public string ApprovePaymentMode { get; set; }
        public string CreditTo { get; set; }

        public List<Plot> lstCalculationWith { get; set; }
        public List<Plot> lstCalculation { get; set; }

        #region Properties
        public string EncryptKey { get; set; }
        public string hdBookingNo { get; set; }
        public string ReceiptNo { get; set; }
        public string Amount { get; set; }
        public string PK_BookingId { get; set; }
        public string NetPlotAmount { get; set; }
        public string CssClass { get; set; }
        public string ApprovedDate { get; set; }
        public string RejectedDate { get; set; }
        public string PlotSize { get; set; }
        public string Dimension { get; set; }
        public string BookingPercent { get; set; }
        public string UserID { get; set; }
        public string BranchID { get; set; }
        public string BranchName { get; set; }
        public string PlotID { get; set; }
        public string PlotNumber { get; set; }
        public string CustomerID { get; set; }
        public string CustomerLoginID { get; set; }
        public string CustomerName { get; set; }
        public string AssociateID { get; set; }
        public string AssociateLoginID { get; set; }
        public string AssociateName { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string PlotAmount { get; set; }
        public string PlotRate { get; set; }
        public string PLCAmount { get; set; }
        public string PaymentPlanID { get; set; }
        public string BookingAmount { get; set; }
        public string PayAmount { get; set; }
        public string Discount { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string NoofEMI { get; set; }
        public string PaymentPlan { get; set; }
        public string TransactionNumber { get; set; }
        public string TransactionDate { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string Remark { get; set; }
        public string TotalPLC { get; set; }
        public string LoginId { get; set; }
        public List<SelectListItem> lstBlock { get; set; }
        public List<SelectListItem> ddlSector { get; set; }
        public string BookingDate { get; set; }
        public string ActualPlotRate { get; set; }
        public string DevelopmentCharge { get; set; }
        public List<Plot> lstPlot { get; set; }
        public string BookingStatus { get; set; }
        public string CancelRemark { get; set; }
        public string CancelDate { get; set; }

        public string KhadraNo { get; set; }
        public string RegistreeNo { get; set; }
        public string RegistrationDate { get; set; }
        #endregion

        #region PlotBooking
        public DataSet PLCList()
        {

            DataSet ds = Connection.ExecuteQuery("PLCList");
            return ds;
        }
        public DataSet GetBranchList()
        {
            DataSet ds = Connection.ExecuteQuery("GetBranchList");
            return ds;
        }

        public DataSet GetSiteList()
        {
            DataSet ds = Connection.ExecuteQuery("SiteList");
            return ds;
        }

        public DataSet GetCustomerName()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetCustomerDetailsForBooking", para);
            return ds;
        }

        public DataSet GetAssociateList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("AssociateListTraditional", para);
            return ds;
        }

        public DataSet GetSectorList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("GetSectorList", para);
            return ds;
        }

        public DataSet GetBlockList()
        {
            SqlParameter[] para ={ new SqlParameter("@SiteID",SiteID),
                                     new SqlParameter("@SectorID",SectorID),
                                     new SqlParameter("@BlockID",BlockID),
                                 };
            DataSet ds = Connection.ExecuteQuery("GetBlockList", para);
            return ds;
        }

        public DataSet GetPaymentPlanList()
        {
            DataSet ds = Connection.ExecuteQuery("GetPaymentPlan");
            return ds;
        }

        public DataSet CheckPlotAvailibility()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotNumber",PlotNumber)
                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotStatus", para);
            return ds;
        }

        public DataSet GetPaymentModeList()
        {

            DataSet ds = Connection.ExecuteQuery("GetPaymentModeList");
            return ds;
        }

        public DataSet SavePlotBooking()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@CustomerId ",CustomerID),
                                        new SqlParameter("@AssociateId" , AssociateID),
                                        new SqlParameter("@Fk_BranchId" , BranchID),
                                        new SqlParameter("@Fk_PlotId"  , PlotID),
                                        new SqlParameter("@Fk_PlanId" ,PaymentPlanID),
                                        new SqlParameter("@BookingDate"  ,BookingDate),
                                        new SqlParameter("@PlotAmount" ,PlotAmount),
                                        new SqlParameter("@Discount", Discount),
                                        new SqlParameter("@DevelopmentCharge", DevelopmentCharge),
                                        new SqlParameter("@ActualPlotRate"  , ActualPlotRate),
                                        new SqlParameter("@PlotRate"  , PlotRate),
                                        new SqlParameter("@BookingAmt"  , BookingAmount),
                                        new SqlParameter("@PaidAmount"  , PayAmount),
                                        new SqlParameter("@PaymentDate"  , PaymentDate),
                                        new SqlParameter("@PLCCharge"  , TotalPLC),
                                        new SqlParameter("@PaymentMode"  , PaymentMode),
                                        new SqlParameter("@TransactionNo"  , TransactionNumber),
                                        new SqlParameter("@TransactionDate"  , TransactionDate),
                                        new SqlParameter("@BankName"  , BankName),
                                        new SqlParameter("@BankBranch"   , BankBranch),
                                        new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@PLCName",PLCName),
                new SqlParameter("@CreditTo",CreditTo),
                                        new SqlParameter("@MLMLoginId",MLMLoginId),
                                          new SqlParameter("@PaymentPlan",PaymentPlan),
                                            new SqlParameter("@noOfEMI",NoofEMI)

                            };
            DataSet ds = Connection.ExecuteQuery("PlotBooking", para);
            return ds;
        }

        public DataSet UpdatePlotBooking()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@CustomerId ",CustomerID),
                                        new SqlParameter("@AssociateId" , AssociateID),
                                        new SqlParameter("@Fk_BranchId" , BranchID),
                                        new SqlParameter("@Fk_PlotId"  , PlotID),
                                        new SqlParameter("@Fk_PlanId" ,PaymentPlanID),
                                        new SqlParameter("@BookingDate"  ,BookingDate),
                                        new SqlParameter("@PlotAmount" ,PlotAmount),
                                        new SqlParameter("@Discount", Discount),
                                        new SqlParameter("@DevelopmentCharge", DevelopmentCharge),
                                        new SqlParameter("@ActualPlotRate"  , ActualPlotRate),
                                        new SqlParameter("@PlotRate"  , PlotRate),
                                        new SqlParameter("@BookingAmt"  , BookingAmount),
                                        new SqlParameter("@PaidAmount"  , PayAmount),
                                        new SqlParameter("@PaymentDate"  , PaymentDate),
                                        new SqlParameter("@PLCCharge"  , TotalPLC),
                                        new SqlParameter("@PaymentMode"  , PaymentMode),
                                        new SqlParameter("@TransactionNo"  , TransactionNumber),
                                        new SqlParameter("@TransactionDate"  , TransactionDate),
                                        new SqlParameter("@BankName"  , BankName),
                                        new SqlParameter("@BankBranch"   , BankBranch),
                                        new SqlParameter("@UpdatedBy",AddedBy),
                                        new SqlParameter("@PLCName",PLCName),
                                        new SqlParameter("@CreditTo",CreditTo),
                                        new SqlParameter("@PK_BookingId",PK_BookingId),
                                        new SqlParameter("@MLMLoginId",MLMLoginId),
                                          new SqlParameter("@PaymentPlan",PaymentPlan),
                                            new SqlParameter("@noOfEMI",NoofEMI)

                            };
            DataSet ds = Connection.ExecuteQuery("UpdatePlotBooking", para);
            return ds;
        }

        public DataSet GetBookingDetailsList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingId", PK_BookingId),
                                      new SqlParameter("@CustomerID", CustomerID),
                                      new SqlParameter("@AssociateID", AssociateID),
                                      new SqlParameter("@BookingNo", BookingNumber),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@FK_SiteID", SiteID),
                                      new SqlParameter("@FK_SectorID", SectorID),
                                      new SqlParameter("@FK_BlockID", BlockID),
                                      new SqlParameter("@PlotNumber", PlotNumber),
                new SqlParameter("@AddedBy", AddedBy),
                                  };

            DataSet ds = Connection.ExecuteQuery("GetPlotBooking", para);
            return ds;
        }
        public DataSet GetBookingDetailsList1()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingId", PK_BookingId),
                                      new SqlParameter("@CustomerID", CustomerID),
                                      new SqlParameter("@AssociateID", AssociateID),
                                      new SqlParameter("@BookingNo", BookingNumber),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@FK_SiteID", SiteID),
                                      new SqlParameter("@FK_SectorID", SectorID),
                                      new SqlParameter("@FK_BlockID", BlockID),
                                      new SqlParameter("@PlotNumber", PlotNumber),
                                  };

            DataSet ds = Connection.ExecuteQuery("GetPlotBookingForCancelList", para);
            return ds;
        }
       
        public DataSet CancelPlotBooking()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@PK_BookingId ",PK_BookingId),
                                        new SqlParameter("@CancelledBy",AddedBy),
                                        new SqlParameter("@CancelRemark", CancelRemark),
                                        new SqlParameter("@ReturnAmount", PaidAmount),
                                        new SqlParameter("@PaymentMode", PaymentMode),
                                        new SqlParameter("@TransactionNo", TransactionNumber),
                                        new SqlParameter("@TransactionDate", TransactionDate),
                                        new SqlParameter("@BankName", BankName),
                                        new SqlParameter("@BankBranch", BankBranch),
                new SqlParameter("@CreditTo", CreditTo),

                            };
            DataSet ds = Connection.ExecuteQuery("CancelPlotBooking", para);
            return ds;
        }

        public DataSet GetCancelledBookingDetailsList()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_BookingId", PK_BookingId),
                                         new SqlParameter("@CustomerID", CustomerID),
                                          new SqlParameter("@AssociateID", AssociateID),
                                  new SqlParameter("@BookingNo",BookingNumber)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetCancelledBooking", para);
            return ds;
        }
        #endregion

        #region HoldPlot
        public string HoldFrom { get; set; }
        public string HoldTo { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string PK_PlotHoldID { get; set; }
        public string HoldType { get; set; }


        public DataSet SavePlotHold()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@Fk_PlotId ",PlotID),
                                        new SqlParameter("@FK_SiteID ",SiteID),
                                        new SqlParameter("@FK_SectorID" , SectorID),
                                        new SqlParameter("@FK_BlockID" , BlockID),
                                        new SqlParameter("@PlotNumber"  , PlotNumber),
                                        new SqlParameter("@HoldFrom" ,HoldFrom),
                                        new SqlParameter("@HoldTo" ,HoldTo),
                                        new SqlParameter("@Name" ,Name),
                                        new SqlParameter("@Mobile" ,Mobile),
                                        new SqlParameter("@AddedBy",AddedBy)  ,
                                        new SqlParameter("@Remark1",Remark),
                                        new SqlParameter("@Amount",Amount)
                            };
            DataSet ds = Connection.ExecuteQuery("PlotHold", para);
            return ds;
        }
        public DataSet GetPlotHoldList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_PlotHoldID", PK_PlotHoldID),

                                   new SqlParameter("@FK_SiteID" ,SiteID),
                                        new SqlParameter("@FK_SectorID" ,SectorID),
                                        new SqlParameter("@FK_BlockID" ,BlockID),
                                        new SqlParameter("@PlotNumber" ,PlotNumber)


                                  };


            DataSet ds = Connection.ExecuteQuery("getPlotHoldList", para);
            return ds;
        }
        public DataSet DeletePlotHold()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@PK_PlotHoldID ",PK_PlotHoldID),
                                        new SqlParameter("@DeletedBy ",AddedBy)

                            };
            DataSet ds = Connection.ExecuteQuery("DeleteHoldPlot", para);
            return ds;
        }
        #endregion

        #region Plot Allotment
        public string PaidAmount { get; set; }
        public string PlanName { get; set; }
        public DataSet FillBookedPlotDetails()
        {
            SqlParameter[] para =
                            {

                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotNumber",PlotNumber) ,
                                new SqlParameter("@Type",HoldType)


                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotDetailsForAllotment", para);
            return ds;
        }
        public DataSet SavePlotAllotment()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@BookingNo ",hdBookingNo),
                                        new SqlParameter("@PaymentDate" , PaymentDate),
                                        new SqlParameter("@PaidAmount"  , PaidAmount),
                                        new SqlParameter("@PaymentMode" ,PaymentMode),
                                        new SqlParameter("@TransactionNo"  ,TransactionNumber),
                                        new SqlParameter("@TransactionDate" ,TransactionDate),
                                        new SqlParameter("@BankBranch", BankBranch),
                                        new SqlParameter("@BankName"  , BankName),
                                        new SqlParameter("@AddedBy",AddedBy),
                                        new SqlParameter("@CreditTo",CreditTo)
                            };
            DataSet ds = Connection.ExecuteQuery("PlotAllotment", para);
            return ds;
        }
        public string TotalAllotmentAmount { get; set; }
        public string PaidAllotmentAmount { get; set; }
        public string BalanceAllotmentAmount { get; set; }
        public DataSet GetSponsorName()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetSponsorForCustomerRegistraton", para);
            return ds;
        }
        #endregion

        #region EMI Payment

        public string TotalInstallment { get; set; }
        public string InstallmentAmount { get; set; }
        public string PK_BookingDetailsId { get; set; }
        public string InstallmentNo { get; set; }
        public string InstallmentDate { get; set; }
        public string BookingNumber { get; set; }
        public string PlotArea { get; set; }
        public string Balance { get; set; }
        public string DueAmount { get; set; }

        public DataSet FillBookedPlotDetailsForEmi()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotNumber",PlotNumber),
                                 new SqlParameter("@BookingNo",BookingNumber)
                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotDetailsForEMIPayment", para);
            return ds;
        }

        public DataSet SaveEMIPayment()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@BookingNo",hdBookingNo),
                                        new SqlParameter("@PaymentDate" , PaymentDate),
                                        new SqlParameter("@PaidAmount"  , PaidAmount),
                                        new SqlParameter("@PaymentMode" ,PaymentMode),
                                        new SqlParameter("@TransactionNo"  ,TransactionNumber),
                                        new SqlParameter("@TransactionDate" ,TransactionDate),
                                        new SqlParameter("@BankBranch", BankBranch),
                                        new SqlParameter("@BankName"  , BankName),
                                        new SqlParameter("@UpdatedBy",AddedBy)  ,
                                        new SqlParameter("@ReceiptNoManual",ReceiptNo),
                                           new SqlParameter("@CreditTo",CreditTo)

                            };
            DataSet ds = Connection.ExecuteQuery("PayEMI", para);
            return ds;
        }

        #endregion

        #region Customer Ledger Report

        public DataSet FillDetails()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@BookingNo",BookingNumber),

                                  new SqlParameter("@FK_SiteID",SiteID),
                                   new SqlParameter("@FK_SectorID",SectorID),
                                    new SqlParameter("@FK_BlockID",BlockID),
                                     new SqlParameter("@PlotNumber",PlotNumber)
                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotDetailsForCustomerLedger", para);
            return ds;
        }

        #endregion

        #region  DueInstallmentReport

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public DataSet FillDueInstDetails()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@BookingNo",BookingNumber),
                                  new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate),
                                   new SqlParameter("@FK_SiteID",SiteID),
                                   new SqlParameter("@FK_SectorID",SectorID),
                                   new SqlParameter("@FK_BlockID",BlockID),
                                   new SqlParameter("@PlotNumber",PlotNumber),

                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotDetailsForDueInstallment", para);
            return ds;
        }
        #endregion

        #region Cheque/neft/cashpayment


        public string PaymentStatus { get; set; }

        public string Description { get; set; }



        public DataSet RejectPayment()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@PK_BookingDetailsId",UserID),
                                  new SqlParameter("@Description",Description),
                                   new SqlParameter("@UpdatedBy",AddedBy),
                                     new SqlParameter("@ApprovedDate",null)
                            };
            DataSet ds = Connection.ExecuteQuery("RejectPayment", para);
            return ds;
        }

        #endregion

        #region PaymentReport

        public DataSet GetPaymentReportList()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@CustomerLoginID",CustomerID),
                                 new SqlParameter("@PaymentStatus",PaymentStatus),
                                  new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate)
                            };
            DataSet ds = Connection.ExecuteQuery("GetDeatilsForPaymentReport", para);
            return ds;
        }

        public string ApproveDescription { get; set; }
        public string RejectDescription { get; set; }

        #endregion

        #region ApproveRejectedPayment

        public DataSet GetList()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@PaymentMode",PaymentMode),
                                  new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate)
                            };
            DataSet ds = Connection.ExecuteQuery("GetDetailsOfRejectedPayment", para);
            return ds;
        }

        public DataSet ApproveRejectPayment()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingDetailsId",UserID),
                                     new SqlParameter("@Description",Description),
                                     new SqlParameter("@UpdatedBy",AddedBy),
                                       new SqlParameter("@ApprovedDate",ApprovedDate),
                                       new SqlParameter("@PaymentMode",PaymentMode),
                                       new SqlParameter("@TransactionNumber",TransactionNumber),
                                       new SqlParameter("@TransactionDate",TransactionDate),
                                       new SqlParameter("@BankName",BankName),
                                       new SqlParameter("@BankBranch",BankBranch)
                                 };
            DataSet ds = Connection.ExecuteQuery("ApproveRejectedPayment", para);
            return ds;
        }

        #endregion

        #region RejectPaymentApproveReport

        public DataSet GetPaymentRejAppReport()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@CustomerLoginID",CustomerID),
                                 new SqlParameter("@PaymentMode ",PaymentMode ),
                                  new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate)
                            };
            DataSet ds = Connection.ExecuteQuery("GetDeatilsForApprovedRejectPaymentReport", para);
            return ds;
        }

        #endregion

        #region AllotmentReport
        public DataSet List()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@PK_BookingId",PK_BookingId),
                                 new SqlParameter("@CustomerID",CustomerID ),
                                 new SqlParameter("@AssociateID",AssociateID ),
                                 new SqlParameter("@FromDate",FromDate),
                                 new SqlParameter("@ToDate",ToDate),
                                  new SqlParameter("@PK_SiteID",SiteID),
                                   new SqlParameter("@PK_SectorID",SectorID),
                                    new SqlParameter("@PK_BlockID",BlockID),
                                     new SqlParameter("@PlotNumber",PlotNumber),
                                       new SqlParameter("@BookingNo",BookingNumber),
                                         new SqlParameter("@PK_BookingDetailsId",PK_BookingDetailsId),


                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotAllotmentReport", para);
            return ds;
        }
        public DataSet PrintReceipt()
        {
            SqlParameter[] para =
                          {
                 new SqlParameter("@Pk_bookingDeatilsId",PK_BookingDetailsId),

                            };
            DataSet ds = Connection.ExecuteQuery("PrintRecipt", para);
            return ds;
        }

        #endregion

        #region SummaryReport

        public DataSet GetSummaryList()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@PK_BookingId",PK_BookingId),
                                 new SqlParameter("@CustomerID",CustomerID ),
                                 new SqlParameter("@AssociateID",AssociateID ),
                                 new SqlParameter("@FromDate",FromDate),
                                 new SqlParameter("@ToDate",ToDate),
                                 new SqlParameter("@CustomerName",CustomerName),
                                 new SqlParameter("@Mobile",Mobile),
                                 new SqlParameter("@PlotNumber",PlotNumber),
                                 new SqlParameter("@BookingNo",BookingNumber),
                                new SqlParameter("@PK_SiteID",SiteID),
                                new SqlParameter("@PK_SectorID",SectorID),
                                new SqlParameter("@PK_BlockID",BlockID),
                                new SqlParameter("@AssociateName",AssociateName),
                                new SqlParameter("@IsDownline",Downline)
                            };

            DataSet ds = Connection.ExecuteQuery("GetDetailsForSummaryReport", para);
            return ds;
        }

        #endregion

        #region PlotTransfer

        public string SiteID1 { get; set; }
        public string SectorID1 { get; set; }
        public string BlockID1 { get; set; }
        public string PlotNumber1 { get; set; }
        public DataTable dtPLC { get; set; }
        public string PLCName { get; set; }
        public string IsEMICalculated { get; set; }



        #endregion


        public DataSet SavePlotRegistry()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@Fk_BookingId ",PK_BookingId),
                                        new SqlParameter("@RegistrationDate" , RegistrationDate),
                                        new SqlParameter("@KhadraNo"  , KhadraNo),
                                        new SqlParameter("@RegistreeNo" ,RegistreeNo),
                                        new SqlParameter("@AddedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("PlotRegistry", para);
            return ds;
        }
        public DataSet UpdateMLMIDDetails()
        {
            SqlParameter[] para = {   new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotNumber",PlotNumber),
                                 new SqlParameter("@BookingNo",BookingNumber)
                                  };

            DataSet ds = Connection.ExecuteQuery("GetPlotDetailsForUpdatingMLMID", para);
            return ds;
        }

        public DataSet SaveUpdateMLMID()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_BookingId ",PK_BookingId),
                                        new SqlParameter("@MLMLoginId"  , MLMLoginId),
                                        new SqlParameter("@AddedBy",AddedBy)
                                  };

            DataSet ds = Connection.ExecuteQuery("UpdateMLMID", para);
            return ds;
        }
        public DataSet CreateEMI()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_BookingId",PK_BookingId),
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@noofEMI",NoofEMI),
            };
            DataSet ds = Connection.ExecuteQuery("CalculateEMI", para);
            return ds;
        }
        public DataSet ListCalculationWith()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@PaymentMode",PaymentMode),
                                  new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate)
                            };
            DataSet ds = Connection.ExecuteQuery("CalculationWith", para);
            return ds;
        }


        public DataSet ListGetCalculatedReport()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@PaymentMode",PaymentMode),
                                  new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate)
                            };
            DataSet ds = Connection.ExecuteQuery("CalculatedWithReport", para);
            return ds;
        }

        public DataSet PaymentCalculatedWith()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@PK_BookingDetailsId",PK_BookingDetailsId),
                                  new SqlParameter("@CalculatedWith",CalculatedWith),
                                   new SqlParameter("@UpdatedBy",AddedBy),

                            };
            DataSet ds = Connection.ExecuteQuery("PaymentCalculatedWith", para);
            return ds;
        }
        public DataSet GetplotRegistryList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingId", PK_BookingId),
                                      new SqlParameter("@CustomerID", CustomerID),
                                      new SqlParameter("@AssociateID", AssociateID),
                                      new SqlParameter("@BookingNo", BookingNumber),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@FK_SiteID", SiteID),
                                      new SqlParameter("@FK_SectorID", SectorID),
                                      new SqlParameter("@FK_BlockID", BlockID),
                                      new SqlParameter("@PlotNumber", PlotNumber),
                                      new SqlParameter("@RegistreeNo",RegistreeNo)
                                  };

            DataSet ds = Connection.ExecuteQuery("GetplotRegistryList", para);
            return ds;
        }
    }
}