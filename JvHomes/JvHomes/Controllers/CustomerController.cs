using JvHomes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JvHomes.Controllers
{
    public class CustomerController : CustomerBaseController
    {
        // GET: Customer
        public ActionResult DashBoard()
        {
            Dashboard obj = new Dashboard();
            obj.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = obj.GetCustomerDashBoard();
            obj.bageDetails = ds.Tables[0];
            return View(obj);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePassword obj)
        {
            try
            {
                obj.OldPassword = JvHomes.Crypto.Encrypt(obj.OldPassword);
                obj.NewPassword = JvHomes.Crypto.Encrypt(obj.NewPassword);
                obj.LoginId = Session["LoginId"].ToString();
                DataSet ds = obj.UpdatePassword();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["Login"] = "Password Changed Successfully";
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(obj);
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                return View(obj);
            }
            return RedirectToAction("ChangePassword");
        }

        public ActionResult UpdateProfile()
        {
            UpdateProfile obj = new UpdateProfile();
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.GetCustomerList();
            obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
            obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
            obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
            obj.Email = ds.Tables[0].Rows[0]["Email"].ToString();
            obj.AccountNo = ds.Tables[0].Rows[0]["AccountNo"].ToString();
            obj.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
            obj.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
            obj.IFSC = ds.Tables[0].Rows[0]["IFSC"].ToString();
            obj.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
            return View(obj);
        }
        [HttpPost]
        public ActionResult UpdateProfile(UpdateProfile model, IEnumerable<HttpPostedFileBase> postedFile)
        {
            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        model.ProfilePic = "../UserImage/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(model.ProfilePic)));
                    }

                }
                model.LoginId = Session["LoginId"].ToString();
                DataSet ds = model.UpdateAssociateProfile();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        Session["ProfilePic"] = ds.Tables[0].Rows[0]["UserImage"].ToString();
                        TempData["Profile"] = "Profile Updated Successfully";
                        return RedirectToAction("UpdateProfile");
                    }
                    else
                    {
                        TempData["Profile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Profile"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("UpdateProfile");
        }

        public ActionResult DueInstallmentReport()
        {
            Reports obj = new Reports();
            Plot model = new Plot();
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;


            #region BindPaymentType
            List<SelectListItem> BindPaymentType = Common.BindPaymentType();
            ViewBag.BindPaymentType = BindPaymentType;
            #endregion BindPaymentType

            return View();
        }

        [HttpPost]
        public ActionResult DueInstallmentReport(Reports obj)
        {
            Plot model = new Plot();
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;


            #region BindPaymentType
            List<SelectListItem> BindPaymentType = Common.BindPaymentType();
            ViewBag.BindPaymentType = BindPaymentType;
            #endregion BindPaymentType

            obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
            obj.Todate = string.IsNullOrEmpty(obj.Todate) ? null : Common.ConvertToSystemDate(obj.Todate, "dd/MM/yyyy");
            obj.SiteID = obj.SiteID == "0" ? null : obj.SiteID;
            obj.SectorID = obj.SectorID == "0" ? null : obj.SectorID;
            obj.BlockID = obj.BlockID == "0" ? null : obj.BlockID;
            obj.PaymentType = obj.PaymentType == "0" ? null : obj.PaymentType;
            obj.PaymentMode = obj.PaymentMode == "0" ? null : obj.PaymentMode;
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.DueInstallmentReport();
            obj.lstDetails = ds.Tables[0];
            return View(obj);
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
        public ActionResult GetDueDetails(string BookingNo)
        {
            Reports obj = new Reports();
            List<Reports> lst = new List<Reports>();
            obj.BookingNo = BookingNo;
            DataSet ds = obj.GetDueDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Reports objList = new Reports();
                    objList.InstallmentNo = dr["InstallmentNo"].ToString();
                    objList.InstallmentDate = dr["InstallmentDate"].ToString();
                    objList.InstAmt = dr["InstAmt"].ToString();
                    lst.Add(objList);
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);



        }
        public ActionResult PlotAvailbilty()
        {
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View();
        }
       
      
        [HttpPost]
        public ActionResult PlotAvailbilty(Master model)
        {
            List<Master> lst = new List<Master>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;

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
            return View(model);
        }

    }
}