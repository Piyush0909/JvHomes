using JvHomes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JvHomes.Controllers
{
    public class MastersController : AdminBaseController
    {
        // GET: Masters
        #region SiteMaster

        public ActionResult SiteMaster(string SiteID)
        {
            Master model = new Master();


            if (SiteID != null)
            {
                model.SiteID = Crypto.Decrypt(SiteID);
                List<Master> lst = new List<Master>();
                DataSet dsSite = model.GetSiteList();
                if (dsSite != null && dsSite.Tables.Count > 0)
                {

                    model.SiteName = dsSite.Tables[0].Rows[0]["SiteName"].ToString();
                    model.Location = dsSite.Tables[0].Rows[0]["Location"].ToString();
                    model.Rate = dsSite.Tables[0].Rows[0]["SiteRate"].ToString();
                }


            }

            return View(model);
        }

        [HttpPost]
        public ActionResult SiteMaster(Master obj, string btnSave)
        {
            try
            {
                if (!string.IsNullOrEmpty(btnSave))
                {
                    obj.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = new DataSet();
                    ds = obj.SaveSite();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["SiteMaster"] = "Site saved successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["SiteMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        TempData["SiteMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = new DataSet();
                    ds = obj.UpdateSite();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["SiteMaster"] = "Site updated successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["SiteMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        TempData["SiteMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["SiteMaster"] = ex.Message;
            }
            return RedirectToAction("SiteMaster", "Masters");
        }



        public ActionResult SiteList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetSiteList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.SiteID = r["PK_SiteID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_SiteID"].ToString());
                    obj.SiteName = r["SiteName"].ToString();
                    obj.Location = r["Location"].ToString();

                    lst.Add(obj);
                }
                model.lstSite = lst;
            }
            return View(model);
        }

        public ActionResult DeleteSite(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.SiteID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteSite();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Site"] = "Site deleted successfully";
                        FormName = "SiteList";
                        Controller = "Masters";
                    }
                    else
                    {
                        TempData["Site"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "SiteList";
                        Controller = "Masters";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Site"] = ex.Message;
                FormName = "SiteList";
                Controller = "Masters";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region SectorMaster

        public ActionResult SectorMaster(string SectorID)
        {
            Master model = new Master();
            #region ddlSite
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet ds1 = obj.GetSiteList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlSite = ddlSite;

            #endregion

            if (SectorID != null)
            {
                model.SectorID = Crypto.Decrypt(SectorID);
                List<Master> lst = new List<Master>();
                DataSet dsSite = model.GetSector();
                if (dsSite != null && dsSite.Tables.Count > 0)
                {
                    model.SiteName = dsSite.Tables[0].Rows[0]["SiteName"].ToString();
                    model.SiteID = dsSite.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    model.SectorID = dsSite.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    model.SectorName = dsSite.Tables[0].Rows[0]["SectorName"].ToString();
                }
            }

            return View(model);
        }

        [HttpPost]

        public ActionResult SectorMaster(Master obj, string btnSave)
        {
            try
            {
                if (!string.IsNullOrEmpty(btnSave))
                {
                    obj.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = new DataSet();
                    ds = obj.SaveSector();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["SectorMaster"] = "Sector saved successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["SectorMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        TempData["SectorMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = new DataSet();
                    ds = obj.UpdateSector();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["SectorMaster"] = "Sector updated successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["SectorMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        TempData["SectorMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }


            }
            catch (Exception ex)
            {
                TempData["SectorMaster"] = ex.Message;
            }
            return RedirectToAction("SectorMaster", "Masters");
        }



        public ActionResult SectorList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetSector();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorID = r["PK_SectorID"].ToString();
                    obj.SiteID = r["FK_SiteID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_SectorID"].ToString());
                    obj.SectorName = r["SectorName"].ToString();

                    lst.Add(obj);
                }
                model.lstSector = lst;
            }
            return View(model);
        }

        public ActionResult DeleteSector(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.SectorID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteSector();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SectorMaster"] = "Sector deleted successfully";
                        FormName = "SectorList";
                        Controller = "Masters";
                    }
                    else
                    {
                        TempData["SectorMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "SectorList";
                        Controller = "Masters";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SectorMaster"] = ex.Message;
                FormName = "SectorList";
                Controller = "Masters";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion 

        #region BlockMaster
        public ActionResult BlockMaster(string BlockID)
        {
            Master model = new Master();
            if (BlockID != null)
            {
                model.BlockID = Crypto.Decrypt(BlockID);

                DataSet dsSite = model.GetBlockList();
                if (dsSite != null && dsSite.Tables.Count > 0)
                {
                    model.SiteName = dsSite.Tables[0].Rows[0]["SiteName"].ToString();
                    model.SiteID = dsSite.Tables[0].Rows[0]["PK_SiteID"].ToString();
                    model.SectorID = dsSite.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    model.SectorName = dsSite.Tables[0].Rows[0]["SectorName"].ToString();
                    model.BlockName = dsSite.Tables[0].Rows[0]["BlockName"].ToString();
                    model.BlockID = dsSite.Tables[0].Rows[0]["PK_BlockID"].ToString();



                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsSite.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsSite.Tables[0].Rows[0]["PK_SiteID"].ToString();


                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                }
            }
            else
            {

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;


            }

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet Site = model.GetSiteList();
            if (Site != null && Site.Tables.Count > 0 && Site.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in Site.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            return View(model);

        }

        [HttpPost]

        public ActionResult BlockMaster(Master obj, string btnSave)
        {
            try
            {
                if (!string.IsNullOrEmpty(btnSave))
                {
                    obj.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = new DataSet();
                    ds = obj.SaveBlock();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["BlockMaster"] = "Block saved successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["BlockMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        TempData["BlockMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = new DataSet();
                    ds = obj.UpdateBlock();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["BlockMaster"] = "Block updated successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["BlockMaster"] = ds.Tables[0].Rows[0][0].ToString();
                        }
                    }
                    else
                    {
                        TempData["BlockMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["BlockMaster"] = ex.Message;
            }
            return RedirectToAction("BlockMaster", "Masters");
        }

        public ActionResult GetSiteDetails(string SiteID)
        {
            try
            {
                Master model = new Master();
                model.SiteID = SiteID;


                #region GetSectors
                List<SelectListItem> ddlSector = new List<SelectListItem>();
                DataSet dsSector = model.GetSectorList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                    }
                    model.Result = "yes";
                }
                model.ddlSector = ddlSector;
                #endregion

                #region SitePLCCharge
                List<Master> lstPlcCharge = new List<Master>();
                DataSet dsPlcCharge = model.GetPLCChargeList();

                if (dsPlcCharge != null && dsPlcCharge.Tables.Count > 0)
                {
                    foreach (DataRow r in dsPlcCharge.Tables[0].Rows)
                    {
                        Master obj = new Master();

                        obj.PLCName = r["PLCName"].ToString();
                        obj.PLCCharge = "0";
                        obj.PLCID = r["PK_PLCID"].ToString();

                        lstPlcCharge.Add(obj);
                    }
                    model.lstPLC = lstPlcCharge;
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult BlockList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetBlockList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorID = r["PK_SectorID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_SectorID"].ToString());
                    obj.SiteID = r["PK_SiteID"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockID = r["PK_BlockID"].ToString();
                    obj.BlockName = r["BlockName"].ToString();

                    lst.Add(obj);
                }
                model.lstBlock1 = lst;
            }
            return View(model);
        }

        public ActionResult DeleteBlock(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.BlockID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteBlock();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockMaster"] = "Block deleted successfully";
                        FormName = "BlockList";
                        Controller = "Masters";
                    }
                    else
                    {
                        TempData["BlockMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "BlockList";
                        Controller = "Masters";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockMaster"] = ex.Message;
                FormName = "BlockList";
                Controller = "Masters";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion

        public ActionResult PlotMaster(string PlotID)
        {
            Master model = new Master();
            #region ddlSites
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet ds1 = obj.GetSiteList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlSite = ddlSite;

            #endregion

            #region ddlPlotSize
            int countplot = 0;
            List<SelectListItem> ddlPlotSize = new List<SelectListItem>();
            DataSet dsPlotSize = obj.GetPlotSize();

            if (dsPlotSize != null && dsPlotSize.Tables.Count > 0 && dsPlotSize.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPlotSize.Tables[0].Rows)
                {
                    if (countplot == 0)
                    {
                        ddlPlotSize.Add(new SelectListItem { Text = "Select Plot Size", Value = "0" });
                    }
                    ddlPlotSize.Add(new SelectListItem { Text = r["PlotSize"].ToString(), Value = r["PK_PlotSizeMaster"].ToString() });
                    countplot = countplot + 1;
                }
            }

            ViewBag.ddlPlotSize = ddlPlotSize;

            #endregion


            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = obj.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    if (sectorcount == 0)
                    {
                        ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                    }
                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = 1;
                }
            }

            ViewBag.ddlSector = ddlSector;
            #endregion

            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock1 = obj.GetBlockList();


            if (dsblock1 != null && dsblock1.Tables.Count > 0 && dsblock1.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock1.Tables[0].Rows)
                {
                    if (blockcount == 0)
                    {
                        lstBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                    }
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                    blockcount = 1;
                }

            }


            ViewBag.ddlBlock = lstBlock;
            #endregion

            if (!string.IsNullOrEmpty(PlotID))
            {
                model.PlotID = Crypto.Decrypt(PlotID);
                DataSet ds = model.GetPlotList();
                model.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
                model.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
                model.BlockID = ds.Tables[0].Rows[0]["PK_BlockID"].ToString();
                model.FromPlot = ds.Tables[0].Rows[0]["PlotNumber"].ToString();
                model.ToPlot = ds.Tables[0].Rows[0]["PlotNumber"].ToString();
                model.PlotAmount = ds.Tables[0].Rows[0]["PlotAmount"].ToString();
                model.PlotRate = ds.Tables[0].Rows[0]["PlotRate"].ToString();
                model.PlotSizeID = ds.Tables[0].Rows[0]["FK_PlotSizeID"].ToString();
                if(model.PlotSizeID=="0")
                {
                    ViewBag.RegularPlot = "0";
                }
                else
                {
                    ViewBag.RegularPlot = "1";
                }
                model.PlotArea = ds.Tables[0].Rows[0]["TotalArea"].ToString();
                model.BookingPercent = ds.Tables[0].Rows[0]["BookingPercent"].ToString();
                model.AllottmentPercent = ds.Tables[0].Rows[0]["AllottmentPercent"].ToString();
                model.PlotID = ds.Tables[0].Rows[0]["PK_PlotID"].ToString(); 
                model.dtPLC = ds.Tables[1];

            }
            else
            {
                ViewBag.RegularPlot = "1";
                DataSet ds = model.PLCList();
                model.dtPLC = ds.Tables[0];
            }
            model.BookingPercent = "21000";
            return View(model);

        }
        public ActionResult GetBlockList(string SiteID, string SectorID)
        {
            List<SelectListItem> lstBlock = new List<SelectListItem>();
            Master model = new Master();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            DataSet dsblock = model.GetBlockList();

            #region ddlBlock
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                }

            }

                model.lstBlock = lstBlock;
            #endregion

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PlotMaster(Master obj, string btnSave)
        {
            string FormName = "";
            string Controller = "";
            if (!string.IsNullOrEmpty(btnSave))
            {
                try
                {

                    string hdrows = Request["hdRows"].ToString();
                    string plcid = "";
                    for (int i = 1; i < int.Parse(hdrows); i++)
                    {
                        if (Request["chk_" + i] == "on")
                        {
                            plcid = plcid + "," + Request["plcid_" + i];
                        }

                    }

                    obj.PLCName = plcid;

                    obj.AddedBy = Session["Pk_AdminId"].ToString();

                    DataSet ds = obj.SavePlot();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Plot"] = "Plot saved successfully !";
                        }
                        else
                        {
                            TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["Plot"] = ex.Message;
                }
            }
            else
            {
                try
                {

                    string hdrows = Request["hdRows"].ToString();
                    string plcid = "";
                    for (int i = 1; i < int.Parse(hdrows); i++)
                    {
                        if (Request["chk_" + i] == "on")
                        {
                            plcid = plcid + "," + Request["plcid_" + i];
                        }

                    }
                    if(obj.PlotStatus== "NonRegularPlot")
                    {
                        obj.PlotSizeID = "0";
                    }
                    obj.PLCName = plcid;
                    obj.PlotID = Crypto.Decrypt(obj.PlotID);
                    obj.AddedBy = Session["Pk_AdminId"].ToString();

                    DataSet ds = obj.UpdatePlot();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Plot"] = "Plot updated successfully !";
                        }
                        else
                        {
                            TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["Plot"] = ex.Message;
                }
            }

            FormName = "PlotMaster";
            Controller = "Masters";

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult AllotPlot(string PlotID)
        {
            Master model = new Master();
            model.PlotID = Crypto.Decrypt(PlotID);
            DataSet ds = model.AllotPlot();
            return RedirectToAction("PlotList");
        }
        public ActionResult PlotList()
        {
            Master model = new Master();
            DataSet ds = model.GetPlotList();


            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;


            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion




            return View(model);
        }


        [HttpPost]

        public ActionResult PlotList(Master model, string btnupdateLocation)
        {
            if (!string.IsNullOrEmpty(btnupdateLocation))
            {
                string hdrows = Request["hdRows"].ToString();
                string plcid = "";
                for (int i = 1; i < int.Parse(hdrows); i++)
                {
                    if (Request["chk_" + i] == "on")
                    {
                        plcid = plcid + "," + Request["plcid_" + i];
                    }

                }
                model.PLCName = plcid;
                model.PlotID = Crypto.Decrypt(model.PlotID);
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.isPlotUpdate = "1";
                DataSet ds111 = model.UpdatePlot();
                //obj.PLCName = plcid;
            }

            List<Master> lst = new List<Master>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.PlotID = null;
            DataSet ds = model.GetPlotList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PlotID = r["PK_PlotID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_PlotID"].ToString());
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PlotArea = r["TotalArea"].ToString();
                    obj.PlotRate = r["PlotRate"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.PLCCharge = r["PLCCharge"].ToString();
                    obj.BookingPercent = r["BookingPercent"].ToString();
                    obj.AllottmentPercent = (r["AllottmentPercent"].ToString());
                    obj.PlotStatus = (r["PlotStatus"].ToString());
                    obj.ColorCSS = (r["StatusColor"].ToString());
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }


            #region ddlSite
            int count1 = 0;
            Master objmaster = new Master();
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = objmaster.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = objmaster.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    if (sectorcount == 0)
                    {
                        ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                    }
                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = 1;
                }
            }

            ViewBag.ddlSector = ddlSector;
            #endregion

            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock = objmaster.GetBlockList();


            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    if (blockcount == 0)
                    {
                        lstBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                    }
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                    blockcount = 1;
                }

            }


            ViewBag.ddlBlock = lstBlock;
            #endregion


            DataSet dqs = model.GetPlotList();

            model.dtPLC = dqs.Tables[1];
            return View(model);
        }

        public ActionResult DeletePlot(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.PlotID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeletePlot();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Plot deleted successfully";
                        FormName = "PlotList";
                        Controller = "Masters";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PlotList";
                        Controller = "Masters";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
                FormName = "PlotList";
                Controller = "Masters";
            }
            return RedirectToAction(FormName, Controller);
        }


        #region PlotSizeMaster

        public ActionResult PlotSizeList()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetPlotSizeList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PlotSizeID = r["PK_PlotSizeMaster"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_PlotSizeMaster"].ToString());
                    obj.WidthFeet = r["WidthFeet"].ToString();
                    obj.WidthInch = r["WidthInch"].ToString();
                    obj.HeightFeet = r["HeightFeet"].ToString();
                    obj.HeightInch = (r["HeightInch"].ToString());
                    obj.PlotArea = (r["PlotArea"].ToString());

                    lst.Add(obj);
                }
                model.lstSite = lst;
            }

            return View(model);
        }

        public ActionResult PlotSizeMaster(string plotSizeID)
        {
            Master model = new Master();
            #region ddlUnits
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds1 = obj.GetUnitList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlUnit = ddlUnit;

            #endregion

            if (plotSizeID != null)
            {
                model.PlotSizeID = Crypto.Decrypt(plotSizeID);
                DataSet ds = new DataSet();
                ds = model.GetPlotSizeList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.PlotSizeID = ds.Tables[0].Rows[0]["PK_PlotSizeMaster"].ToString();
                    model.WidthFeet = ds.Tables[0].Rows[0]["WidthFeet"].ToString();
                    model.WidthInch = ds.Tables[0].Rows[0]["WidthInch"].ToString();
                    model.HeightFeet = ds.Tables[0].Rows[0]["HeightFeet"].ToString();
                    model.HeightInch = ds.Tables[0].Rows[0]["HeightInch"].ToString();
                    model.PlotArea = ds.Tables[0].Rows[0]["PlotArea"].ToString();
                }
            }
            model.UnitID = "1";
            return View(model);
        }

        [HttpPost]

        public ActionResult PlotSizeMaster(Master obj, string Save)
        {
            if (!string.IsNullOrEmpty(Save))
            {
                try
                {
                    obj.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = new DataSet();
                    ds = obj.SavePlotSize();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["PlotSize"] = "Plot Size saved successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["PlotSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        TempData["PlotSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                catch (Exception ex)
                {
                    TempData["PlotSize"] = ex.Message;
                }
            }
            else
            {
                try
                {

                    obj.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = new DataSet();
                    ds = obj.UpdatePlotSize();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["PlotSize"] = "Plot Size updated successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["PlotSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        TempData["PlotSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                catch (Exception ex)
                {
                    TempData["PlotSize"] = ex.Message;
                }
            }

            return RedirectToAction("PlotSizeMaster", "Masters");
        }


        public ActionResult DeletePlotSize(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.PlotSizeID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeletePlotSize();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PlotSize"] = "Plot Size deleted successfully";
                        FormName = "PlotSizeList";
                        Controller = "Masters";
                    }
                    else
                    {
                        TempData["PlotSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PlotSizeList";
                        Controller = "Masters";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["PlotSize"] = ex.Message;
                FormName = "PlotSizeList";
                Controller = "Masters";
            }
            return RedirectToAction(FormName, Controller);
        }


        #endregion




        public ActionResult PLCMaster(string plcid)
        {
            Master obj = new Master();
            if (!string.IsNullOrEmpty(plcid))
            {
                obj.PLCID = plcid;
                DataSet ds = obj.PLCList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    obj.PLCID = ds.Tables[0].Rows[0]["PK_PLCID"].ToString();
                    obj.PLCName = ds.Tables[0].Rows[0]["PLCName"].ToString();
                    obj.PLCCharge = ds.Tables[0].Rows[0]["PLCCharge"].ToString();
                }
            }
            return View(obj);
        }
        [HttpPost]
        public ActionResult PLCMaster(Master obj, string btnSave)
        {
            if (!string.IsNullOrEmpty(btnSave))
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.SavePLC();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PLCMaster"] = "PLC saved successfully !";
                    }
                    else
                    {
                        TempData["PLCMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(obj);
                    }
                }
            }
            else
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdatePLC();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PLCMaster"] = "PLC updated successfully !";
                    }
                    else
                    {
                        TempData["PLCMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(obj);
                    }
                }
            }

            return RedirectToAction("PLCMaster");
        }

        public ActionResult PLCList(Master obj)
        {
            DataSet ds = obj.PLCList();
            obj.dtPLC = ds.Tables[0];
            return View(obj);
        }
        public ActionResult DeletePLC(string id)
        {
            Master obj = new Master();
            obj.AddedBy = Session["Pk_AdminId"].ToString();
            obj.PLCID = id;
            DataSet ds = obj.DeletePLC();
            return RedirectToAction("PLCList");
        }


        public ActionResult AddBank()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBank(Master obj)
        {
            obj.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = obj.AddBank();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["AddBank"] = "Bank added successfully !";
                }
                else
                {
                    TempData["AddBank"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(obj);
                }
            }
            return RedirectToAction("AddBank");
        }
        public ActionResult BankList()
        {
            Master obj = new Master();
            DataSet ds = obj.GetBankList();
            obj.bankList = ds.Tables[0];
            return View(obj);
        }
        public ActionResult DeleteBank(string id)
        {
            Master obj = new Master();
            obj.AddedBy = Session["Pk_AdminId"].ToString();
            obj.BankId = id;
            DataSet ds = obj.DeleteBank();
            return RedirectToAction("BankList");

        }

        public ActionResult AccountSubHeadMaster()
        {
            #region ddlHeadtTyppe
            List<SelectListItem> ddlHeadtTyppe = Common.BindHeadType();
            ViewBag.ddlHeadtTyppe = ddlHeadtTyppe;
            #endregion ddlHeadtTyppe

            return View();
        }
        [HttpPost]
        public ActionResult AccountSubHeadMaster(Master obj)
        {
            #region ddlHeadtTyppe
            List<SelectListItem> ddlHeadtTyppe = Common.BindHeadType();
            ViewBag.ddlHeadtTyppe = ddlHeadtTyppe;
            #endregion ddlHeadtTyppe
            obj.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = obj.SaveSubHead();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["AccountHeadMaster"] = "Head added successfully !";
                }
                else
                {
                    TempData["AccountHeadMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(obj);
                }
            }
            return RedirectToAction("AccountSubHeadMaster");
        }
        public ActionResult AccountHeadList()
        {
            Master obj = new Master();
            DataSet ds = obj.GetAccountHead();
            obj.bankList = ds.Tables[0];
            return View(obj);
        }
        public ActionResult DeleteAccountHead(string id)
        {
            Master obj = new Master();
            obj.AddedBy = Session["Pk_AdminId"].ToString();
            obj.AccountId = id;
            DataSet ds = obj.DeleteAccountHead();
            return RedirectToAction("AccountHeadList");

        }

    }
}