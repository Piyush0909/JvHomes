using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JvHomes.Models
{
    public class Master : Common
    {
        #region Properties
        public string EncryptKey { get; set; }

        public string ColorCSS { get; set; }
        public string SiteID { get; set; }
        public string PLCID { get; set; }
        public string PLCName { get; set; }
        public string SiteName { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string PK_DesignationID { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string Rate { get; set; }
        public string PLCCharge { get; set; }
        public string DevelopmentCharge { get; set; }

        public string PlotSizeID { get; set; }
        public string PlotID { get; set; }
        public string PlotNumber { get; set; }
        public string PlotStatus { get; set; }
        public string PlotRate { get; set; }
        public string PlotAmount { get; set; }
        public string PlotPrefix { get; set; }
        public string PlotPostfix { get; set; }
        public string FromPlot { get; set; }
        public string ToPlot { get; set; }
        public string BookingPercent { get; set; }
        public string AllottmentPercent { get; set; }
        public string WidthFeet { get; set; }
        public string WidthInch { get; set; }
        public string HeightFeet { get; set; }
        public string HeightInch { get; set; }
        public string PlotArea { get; set; }

        public string SectorID { get; set; }
        public string SectorName { get; set; }
        public string BlockID { get; set; }
        public string BlockName { get; set; }
        public string LoginId { get; set; }
        public DataTable dtPLCCharge { get; set; }
        public List<Master> lstPLC { get; set; }
        public List<Master> lstSite { get; set; }
        public List<Master> lstPlot { get; set; }
        public List<SelectListItem> lstBlock { get; set; }
        public List<SelectListItem> ddlSector { get; set; }
        public List<SelectListItem> ddlSite { get; set; }
        #endregion
        public string AccountHead { get; set; }
        public string HeadType { get; set; }
        public string AccountId { get; set; }
        #region SiteMaster

        public DataSet SaveSite()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@SiteName", SiteName),
                                      new SqlParameter("@Location", Location),
new SqlParameter("@SiteRate", Rate),
                                      new SqlParameter("@AddedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("SiteMaster", para);
            return ds;
        }

        public DataSet UpdateSite()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@SiteID", SiteID),
                                      new SqlParameter("@SiteName", SiteName),
                                      new SqlParameter("@Location", Location),
                                      new SqlParameter("@SiteRate", Rate),
                                      new SqlParameter("@UpdatedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("UpdateSite", para);
            return ds;
        }

        public DataSet GetSiteList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("SiteList", para);
            return ds;
        }

        public DataSet GetUnitList()
        {
            DataSet ds = Connection.ExecuteQuery("GetUnitList");
            return ds;
        }

        public DataSet GetSitePlcChargeList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("SitePlcChargeList", para);
            return ds;
        }

        public DataSet DeleteSite()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteSite", para);
            return ds;
        }

        #endregion

        #region  Sector

        public DataSet SaveSector()
        {
            SqlParameter[] para =
                            {

                                new SqlParameter("@FK_SiteID",SiteID),
                                new SqlParameter("@SectorName",SectorName),
                                new SqlParameter("@AddedBy",AddedBy)

                            };
            DataSet ds = Connection.ExecuteQuery("SectorMaster", para);
            return ds;
        }


        public DataSet UpdateSector()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@PK_SectorID",SectorID),
                                new SqlParameter("@FK_SiteID",SiteID),
                                new SqlParameter("@SectorName",SectorName),
                                new SqlParameter("@UpdatedBy",AddedBy)

                            };
            DataSet ds = Connection.ExecuteQuery("UpdateSector", para);
            return ds;
        }


        public List<Master> lstSector { get; set; }
        public List<Master> lstBlock1 { get; set; }
        public DataTable dtPLC { get; set; }
        public string isPlotUpdate { get; set; }

        public DataSet GetSector()
        {
            SqlParameter[] para =
             {
                                new SqlParameter("@PK_SectorID",SectorID)

                            };
            DataSet ds = Connection.ExecuteQuery("SelectSector", para);
            return ds;
        }


        public DataSet DeleteSector()
        {
            SqlParameter[] para = { new SqlParameter("@PK_SectorID", SectorID),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteSector", para);
            return ds;
        }

        #endregion
        #region BlockMaster

        public DataSet DeleteBlock()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BlockID", BlockID),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteBlock", para);
            return ds;
        }


        public DataSet SaveBlock()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_SiteID", SiteID),
                                      new SqlParameter("@BlockName", BlockName),
                                      new SqlParameter("@FK_SectorID", SectorID),
                                      new SqlParameter("@AddedBy", AddedBy)

                                  };
            DataSet ds = Connection.ExecuteQuery("BlockMaster", para);
            return ds;
        }

        public DataSet UpdateBlock()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_BlockID", BlockID),
                                      new SqlParameter("@FK_SiteID ", SiteID ),
                                      new SqlParameter("@FK_SectorID", SectorID),
                                        new SqlParameter("@BlockName", BlockName),
                                      new SqlParameter("@UpdatedBy", AddedBy)

                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateBlockMaster", para);
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
        public DataSet GetPLCChargeList()
        {
            SqlParameter[] para = { new SqlParameter("@FK_SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("GetPlotPLCCharge", para);
            return ds;

        }
        public DataSet GetSectorList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("GetSectorList", para);
            return ds;
        }
        #endregion

        #region PlotMaster-List-Update-Delete
        public DataSet GetPlotSize()
        {
            DataSet ds = Connection.ExecuteQuery("GetPlotSize");
            return ds;
        }
        public DataSet SavePlot()
        {
            SqlParameter[] para =
                            {

                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotPrefix",PlotPrefix),
                                new SqlParameter("@FromPlot",FromPlot),
                                new SqlParameter("@ToPlot",ToPlot),
                                new SqlParameter("@PlotSizeID",PlotSizeID),
                                new SqlParameter("@PlotRate",PlotRate),
                                new SqlParameter("@PlotAmount",PlotAmount),
                new SqlParameter("@PLCName",PLCName),
                new SqlParameter("@PlotArea",PlotArea),
                new SqlParameter("@PlotPostfix",PlotPostfix),
                                new SqlParameter("@BookingPercent",BookingPercent),
                                new SqlParameter("@AllottmentPercent",AllottmentPercent),
                                new SqlParameter("@AddedBy",AddedBy)


                            };
            DataSet ds = Connection.ExecuteQuery("PlotMaster", para);
            return ds;
        }
        public DataSet GetPlotList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID),
                                  new SqlParameter("@SectorID", SectorID),
                                  new SqlParameter("@BlockID", BlockID),
                                  new SqlParameter("@PlotID", PlotID),
                                  new SqlParameter("@PlotNumber", PlotNumber),
                                  };
            DataSet ds = Connection.ExecuteQuery("PlotList", para);
            return ds;
        }
        public DataSet AllotPlot()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@PlotID", PlotID),
                                  
                                  };
            DataSet ds = Connection.ExecuteQuery("AllotPlot", para);
            return ds;
        }
        public DataSet PlotReport()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID),
                                  new SqlParameter("@SectorID", SectorID),
                                  new SqlParameter("@BlockID", BlockID),
                                  new SqlParameter("@PlotID", PlotID),
                                  new SqlParameter("@PlotNumber", PlotNumber),
                                  new SqlParameter("@PLCName", PLCName),
                                  };
            DataSet ds = Connection.ExecuteQuery("PlotStatus", para);
            return ds;
        }
        public DataSet UpdatePlot()
        {
            SqlParameter[] para =
                            {
                               
                                new SqlParameter("@PK_PlotID",PlotID),
                                   new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                              
                                new SqlParameter("@PlotAmount",PlotAmount),
                                new SqlParameter("@PlotNumber",ToPlot),
                                new SqlParameter("@PlotSizeID",PlotSizeID),
                                new SqlParameter("@PlotRate",PlotRate),
                                 new SqlParameter("@isPlotUpdate",isPlotUpdate),
                                  new SqlParameter("@PLCName",PLCName),
                                   new SqlParameter("@PlotArea",PlotArea),
                                new SqlParameter("@BookingPercent",BookingPercent),
                                new SqlParameter("@AllottmentPercent",AllottmentPercent),
                                new SqlParameter("@AddedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("UpdatePlot", para);
            return ds;
        }

        public DataSet DeletePlot()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@PK_PlotID",PlotID),
                                new SqlParameter("@DeletedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("DeletePlotEntry", para);
            return ds;
        }

        public DataSet SavePLC()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@PLCName",PLCName),
                                new SqlParameter("@PLCCharge",PLCCharge),
                                new SqlParameter("@AddedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("SavePLC", para);
            return ds;
        }
        public DataSet UpdatePLC()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@PLCName",PLCName),
                                new SqlParameter("@PLCCharge",PLCCharge),
                                new SqlParameter("@UpdatedBy",UpdatedBy),
                                 new SqlParameter("@PLCID",PLCID)
                            };
            DataSet ds = Connection.ExecuteQuery("UpdatePLC", para);
            return ds;
        }
        public DataSet PLCList()
        {
            SqlParameter[] para =
                            {

                                 new SqlParameter("@PLCID",PLCID)
                            };
            DataSet ds = Connection.ExecuteQuery("PLCList", para);
            return ds;
        }
        #endregion

        public DataSet SavePlotSize()
        {
            SqlParameter[] para = { new SqlParameter("@WidthFeet", WidthFeet),
                                      new SqlParameter("@WidthInch", WidthInch),
                                      new SqlParameter("@HeightFeet", HeightFeet),
                                      new SqlParameter("@HeightInch", HeightInch),
                                      new SqlParameter("@TotalArea", PlotArea),
                                      new SqlParameter("@UnitID", UnitID),
                                      new SqlParameter("@AddedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("SavePlotSize", para);
            return ds;
        }
        public DataSet DeletePlotSize()
        {
            SqlParameter[] para = { new SqlParameter("@PlotSizeID", PlotSizeID),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeletePlotSize", para);
            return ds;
        }
        public DataSet DeletePLC()
        {
            SqlParameter[] para = { new SqlParameter("@PLCID", PLCID),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeletePLC", para);
            return ds;
        }
        public DataSet UpdatePlotSize()
        {
            SqlParameter[] para = { new SqlParameter("@PK_PlotSizeMasterID", PlotSizeID),
                                      new SqlParameter("@WidthFeet", WidthFeet),
                                      new SqlParameter("@WidthInch", WidthInch),
                                      new SqlParameter("@HeightFeet", HeightFeet),
                                      new SqlParameter("@HeightInch", HeightInch),
                                      new SqlParameter("@TotalArea", PlotArea),
                                      new SqlParameter("@UnitID", UnitID),
                                      new SqlParameter("@UpdatedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("UpdatePlotSize", para);
            return ds;
        }
        public DataSet GetPlotSizeList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_PlotSizeMasterID", PlotSizeID) };
            DataSet ds = Connection.ExecuteQuery("PlotSizeMasterList", para);
            return ds;
        }
        public string BankName { get; set; }
        public DataTable bankList { get;  set; }
        public string BankId { get;  set; }

        public DataSet AddBank()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@BankName", BankName),
                                      new SqlParameter("@AddedBy ", AddedBy ),

                                  };
            DataSet ds = Connection.ExecuteQuery("AddBank", para);
            return ds;
        }
        public DataSet SaveSubHead()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@SubAccountHead", AccountHead),
                                      new SqlParameter("@AddedBy ", AddedBy ),
                                       new SqlParameter("@HeadType", HeadType ),

                                  };
            DataSet ds = Connection.ExecuteQuery("SaveSubHead", para);
            return ds;
        }
        public DataSet GetBankList()
        {

            DataSet ds = Connection.ExecuteQuery("GetBankList");
            return ds;
        }
        public DataSet GetAccountHead()
        {

            DataSet ds = Connection.ExecuteQuery("GetAccountHead");
            return ds;
        }
        public DataSet DeleteBank()
        {
            SqlParameter[] para = { new SqlParameter("@BankId", BankId),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("Deletebank", para);
            return ds;
        }
        public DataSet DeleteAccountHead()
        {
            SqlParameter[] para = { new SqlParameter("@AccountId", AccountId),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteAccountHead", para);
            return ds;
        }
    }
}