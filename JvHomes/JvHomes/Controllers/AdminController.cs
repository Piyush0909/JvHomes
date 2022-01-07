using JaysSakti;
using JvHomes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static JvHomes.Models.Common;

namespace JvHomes.Controllers
{
    public class AdminController : AdminBaseController
    {
        // GET: Admin
        public ActionResult DashBoard()
        {
            Dashboard obj = new Dashboard();


            DataSet ds = obj.GetAdminDashBoard();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.TotalAssociate = ds.Tables[0].Rows[0]["TotalAssociate"].ToString();
                    ViewBag.TotalCustomer = ds.Tables[0].Rows[0]["TotalCustomer"].ToString();
                    ViewBag.TotalBooking = ds.Tables[0].Rows[0]["TotalBooking"].ToString();
                    ViewBag.TotalBusiness = ds.Tables[0].Rows[0]["TotalBusiness"].ToString();
                    ViewBag.TotalInactiveAssociate = ds.Tables[0].Rows[0]["TotalInactiveAssociate"].ToString();
                    ViewBag.TotalActiveAssociate = ds.Tables[0].Rows[0]["TotalActiveAssociate"].ToString();

                    ViewBag.TotalLevelBusienss = ds.Tables[1].Rows[0]["TotalLevelBusienss"].ToString();
                    ViewBag.TotalDirectBusiness = ds.Tables[2].Rows[0]["TotalDirectBusiness"].ToString();

                    ViewBag.BookedArea = ds.Tables[3].Rows[0]["BookedArea"].ToString();
                    ViewBag.AllotedArea = ds.Tables[4].Rows[0]["AllotedArea"].ToString();
                    ViewBag.RegisteredArea = ds.Tables[5].Rows[0]["RegisteredArea"].ToString();

                    ViewBag.PendingDocument = ds.Tables[6].Rows[0]["PendingDocument"].ToString();



                }

            }
            return View(obj);
        }
        public ActionResult BinaryTree()
        {
            Session["Pk_userId"] = "1";
            Session["LoginId"] = "kdsl1234";
            return View();
        }
        public ActionResult AssociateList(string status)
        {
            Reports obj = new Reports();
            obj.CommissionType = "Level";
            obj.Status = string.IsNullOrEmpty(status) ? null : status;
            DataSet ds = obj.GetAssociateList();
            obj.userList = ds.Tables[0];

            return View(obj);
        }
        [HttpPost]
        public ActionResult AssociateList(Reports obj)
        {
            obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
            obj.CommissionType = "Level";
            obj.Todate = string.IsNullOrEmpty(obj.Todate) ? null : Common.ConvertToSystemDate(obj.Todate, "dd/MM/yyyy");
            DataSet ds = obj.GetAssociateList();
            obj.userList = ds.Tables[0];

            return View(obj);
        }
        public ActionResult DirectAssociateList()
        {
            Reports obj = new Reports();
            obj.CommissionType = "Direct";
            DataSet ds = obj.GetAssociateList();
            obj.userList = ds.Tables[0];

            return View(obj);
        }
        [HttpPost]
        public ActionResult DirectAssociateList(Reports obj)
        {
            obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
            obj.CommissionType = "Direct";
            obj.Todate = string.IsNullOrEmpty(obj.Todate) ? null : Common.ConvertToSystemDate(obj.Todate, "dd/MM/yyyy");
            DataSet ds = obj.GetAssociateList();
            obj.userList = ds.Tables[0];

            return View(obj);
        }
        public ActionResult GetUserList()
        {
            Reports obj = new Reports();
            List<Reports> lst = new List<Reports>();
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.GettingUserProfile();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Reports objList = new Reports();
                    objList.UserName = dr["Fullname"].ToString();
                    objList.LoginIDD = dr["LoginId"].ToString();
                    lst.Add(objList);
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CustomerRegistration(string UserID)
        {
            Customer model = new Customer();
            try
            {
                if (UserID != null)
                {
                    model.UserID = Crypto.Decrypt(UserID);
                    //  model.UserID = UserID;
                    DataSet dsPlotDetails = model.GetList();
                    if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                    {
                        model.UserID = UserID;
                        model.FK_SponsorId = dsPlotDetails.Tables[0].Rows[0]["FK_SponsorId"].ToString();
                        model.sponserId = dsPlotDetails.Tables[0].Rows[0]["SponsorId"].ToString();
                        model.sponsorName = dsPlotDetails.Tables[0].Rows[0]["SponsorName"].ToString();
                        model.FirstName = dsPlotDetails.Tables[0].Rows[0]["FirstName"].ToString();
                        model.LastName = dsPlotDetails.Tables[0].Rows[0]["LastName"].ToString();

                        model.Contact = dsPlotDetails.Tables[0].Rows[0]["Mobile"].ToString();
                        model.Email = dsPlotDetails.Tables[0].Rows[0]["Email"].ToString();
                        model.State = dsPlotDetails.Tables[0].Rows[0]["State"].ToString();
                        model.City = dsPlotDetails.Tables[0].Rows[0]["City"].ToString();
                        model.Address = dsPlotDetails.Tables[0].Rows[0]["Address"].ToString();
                        model.pinCode = dsPlotDetails.Tables[0].Rows[0]["PinCode"].ToString();
                        model.PanNo = dsPlotDetails.Tables[0].Rows[0]["PanNumber"].ToString();
                        model.AssociateID = dsPlotDetails.Tables[0].Rows[0]["AssociateId"].ToString();
                        model.AssociateName = dsPlotDetails.Tables[0].Rows[0]["AssociateName"].ToString();
                        model.JoiningDate = dsPlotDetails.Tables[0].Rows[0]["JoiningDate"].ToString();
                        model.FatherName = dsPlotDetails.Tables[0].Rows[0]["FathersName"].ToString();
                    }
                }

                else
                {


                }
                #region BindRelation
                List<SelectListItem> ddlRelation = Common.BindRelation();
                ViewBag.ddlRelation = ddlRelation;
                #endregion BindRelation
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public ActionResult GetNameForExpense(string LoginId)
        {
            try
            {
                DayBook model = new DayBook();
                model.LoginId = LoginId;


                DataSet dsSponsorName = model.GetUserDetailsForExp();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.sponsorName = dsSponsorName.Tables[0].Rows[0]["Name"].ToString();
                    if (dsSponsorName.Tables[1] != null)
                    {
                        if (dsSponsorName.Tables[1].Rows.Count > 0)
                        {
                            model.Addres = dsSponsorName.Tables[1].Rows[0]["Address"].ToString();
                            model.Mobile = dsSponsorName.Tables[1].Rows[0]["MObile"].ToString();
                            model.EmailId = dsSponsorName.Tables[1].Rows[0]["EMail"].ToString();
                            model.MemberAccNo = dsSponsorName.Tables[1].Rows[0]["MemberAccNo"].ToString();
                            model.MemberBankName = dsSponsorName.Tables[1].Rows[0]["MemberBankName"].ToString();
                            model.MemberBranch = dsSponsorName.Tables[1].Rows[0]["MemberBranch"].ToString();
                            model.IFSCCode = dsSponsorName.Tables[1].Rows[0]["IFSCCode"].ToString();
                            model.ProfilePic = dsSponsorName.Tables[1].Rows[0]["ProfilePic"].ToString();
                            model.AdharNumber = dsSponsorName.Tables[1].Rows[0]["AdharNumber"].ToString();
                            model.AdharImage = dsSponsorName.Tables[1].Rows[0]["AdharImage"].ToString();
                            model.AdharStatus = dsSponsorName.Tables[1].Rows[0]["AdharStatus"].ToString();
                            model.PanNumber = dsSponsorName.Tables[1].Rows[0]["PanNumber"].ToString();
                            model.PanImage = dsSponsorName.Tables[1].Rows[0]["PanImage"].ToString();
                            model.PanStatus = dsSponsorName.Tables[1].Rows[0]["PanStatus"].ToString();
                            model.DocumentImage = dsSponsorName.Tables[1].Rows[0]["DocumentImage"].ToString();
                            model.DocumentStatus = dsSponsorName.Tables[1].Rows[0]["DocumentStatus"].ToString();
                        }
                    }


                    model.Result = "yes";

                }
                else
                {
                    model.sponsorName = "";
                    model.Result = "no";
                }


                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetDircetSponsorName(string SponsorID)
        {
            try
            {
                TraditionalAssociate model = new TraditionalAssociate();
                model.LoginID = SponsorID;

                model.CommissionType = "Direct";
                DataSet dsSponsorName = model.GetAssociateList();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.sponsorName = dsSponsorName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsSponsorName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.SponsorDesignationID = dsSponsorName.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    model.Percentage = dsSponsorName.Tables[0].Rows[0]["Percentage"].ToString();
                    int desgnationCount = 0;

                    DataSet dsdesignation = model.GetDesignationList();
                    List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                    if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow r in dsdesignation.Tables[1].Rows)
                        {
                            if (desgnationCount == 0)
                            {
                                ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                            }
                            ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                            desgnationCount = desgnationCount + 1;
                        }
                    }

                    // ViewBag.ddlDesignation = ddlDesignation;

                    model.ddlDesignation = ddlDesignation;
                    model.Result = "yes";
                }
                else
                {
                    model.sponsorName = "";
                    model.Result = "no";
                }


                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetSponsorName(string SponsorID)
        {
            try
            {
                TraditionalAssociate model = new TraditionalAssociate();
                model.LoginID = SponsorID;
                model.CommissionType = "Level";

                DataSet dsSponsorName = model.GetAssociateList();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.sponsorName = dsSponsorName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsSponsorName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.SponsorDesignationID = dsSponsorName.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    model.Percentage = dsSponsorName.Tables[0].Rows[0]["Percentage"].ToString();
                    int desgnationCount = 0;

                    DataSet dsdesignation = model.GetDesignationList();
                    List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                    if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in dsdesignation.Tables[0].Rows)
                        {
                            if (desgnationCount == 0)
                            {
                                ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                            }
                            ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                            desgnationCount = desgnationCount + 1;
                        }
                    }

                    // ViewBag.ddlDesignation = ddlDesignation;

                    model.ddlDesignation = ddlDesignation;
                    model.Result = "yes";
                }
                else
                {
                    model.sponsorName = "";
                    model.Result = "no";
                }


                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetSponsorNameForPlotBooking(string SponsorID)
        {
            try
            {
                TraditionalAssociate model = new TraditionalAssociate();
                model.LoginID = SponsorID;

                DataSet dsSponsorName = model.GetSponsorForCustomerRegistraton();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.sponsorName = dsSponsorName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsSponsorName.Tables[0].Rows[0]["PK_UserID"].ToString();



                    model.Result = "yes";
                }
                else
                {
                    model.sponsorName = "";
                    model.Result = "no";
                }


                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult CustomerRegistration(Customer model, string btnRegistration)
        {
            #region BindRelation
            List<SelectListItem> ddlRelation = Common.BindRelation();
            ViewBag.ddlRelation = ddlRelation;
            #endregion BindRelation
            string FormName = "";
            string Controller = "";
            try
            {
                if (!string.IsNullOrEmpty(btnRegistration))
                {
                    Random rnd = new Random();
                    string ctrpass = rnd.Next(111111, 999999).ToString();
                    model.Password = Crypto.Encrypt(ctrpass);
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet dsRegistration = model.CustomerRegistration();

                    if (dsRegistration != null && dsRegistration.Tables.Count > 0)
                    {
                        if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            Session["DisplayNameConfirm"] = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                            Session["LoginIDConfirm"] = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["PasswordConfirm"] = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                            string name = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                            string id = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                            string pass = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                            string mob = model.Contact;

                            //string str = BLSMS.CustomerRegistration(name, id, pass);
                            //try
                            //{
                            //      BLSMS.SendSMS(mob, str);
                            //}
                            //catch { }
                        }
                        else
                        {
                            TempData["Registration"] = dsRegistration.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            return View(model);
                        }
                    }
                }
                else
                {
                    model.UserID = Crypto.Decrypt(model.UserID);
                    model.AddedBy = Session["Pk_AdminId"].ToString();

                    DataSet ds = model.UpdateCustomer();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Registration"] = "Customer Updated successfully !";
                            return RedirectToAction("CustomerRegistration");
                        }
                        else
                        {
                            TempData["Registration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            return View(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
                return View(model);
            }
            FormName = "ConfirmRegistration";
            Controller = "Admin";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult ConfirmRegistration()
        {
            return View();
        }
        public ActionResult CustomerList()
        {
            return View();
        }
        [HttpPost]

        public ActionResult CustomerList(Customer model)
        {
            List<Customer> lst = new List<Customer>();
            DataSet ds = model.GetList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Customer obj = new Customer();
                    obj.FK_SponsorId = r["FK_SponsorId"].ToString();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.LoginID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.UserID = r["PK_UserId"].ToString();
                    obj.sponserId = r["SponsorId"].ToString();
                    obj.sponsorName = r["SponsorName"].ToString();
                    obj.isBlocked = r["isBlocked"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.LastName = r["LastName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.City = r["City"].ToString();
                    obj.State = r["State"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_UserId"].ToString());
                    lst.Add(obj);
                }
                model.ListCust = lst;
            }
            return View(model);
        }

        public ActionResult DeleteCustomer(string UserID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Customer obj = new Customer();
                obj.UserID = UserID;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteCustomer();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Registration"] = "Customer deleted successfully";
                        FormName = "CustomerList";
                        Controller = "Admin";
                    }
                    else
                    {
                        TempData["Registration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "CustomerList";
                        Controller = "Admin";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
                FormName = "CustomerList";
                Controller = "Admin";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult PlotBooking(string PK_BookingId)
        {
            Plot model = new Plot();
            model.Discount = "0";
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;
                    model.CustomerID = dsBookingDetails.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsBookingDetails.Tables[0].Rows[0]["CustomerName"].ToString();
                    model.AssociateID = dsBookingDetails.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsBookingDetails.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();

                    model.BookingAmount = dsBookingDetails.Tables[0].Rows[0]["BookingAmt"].ToString();

                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();
                    model.NetPlotAmount = dsBookingDetails.Tables[0].Rows[0]["NetPlotAmount"].ToString();

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
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                    model.BlockID = dsBookingDetails.Tables[0].Rows[0]["PK_BlockID"].ToString();
                    model.PlotRate = dsBookingDetails.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.PaymentDate = dsBookingDetails.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.Discount = dsBookingDetails.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentPlan = dsBookingDetails.Tables[0].Rows[0]["PaymentPlan"].ToString();
                    model.PaymentMode = dsBookingDetails.Tables[0].Rows[0]["PaymentMode"].ToString();
                    model.PlotNumber = dsBookingDetails.Tables[0].Rows[0]["PlotNumber"].ToString();
                    model.PlotAmount = dsBookingDetails.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsBookingDetails.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PayAmount = dsBookingDetails.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.TotalPLC = dsBookingDetails.Tables[0].Rows[0]["PLCCharge"].ToString();
                    model.TransactionNumber = dsBookingDetails.Tables[0].Rows[0]["TransactionNo"].ToString();
                    model.TransactionDate = dsBookingDetails.Tables[0].Rows[0]["TransactionDate"].ToString();
                    model.BankName = dsBookingDetails.Tables[0].Rows[0]["BankName"].ToString();
                    model.BankBranch = dsBookingDetails.Tables[0].Rows[0]["BankBranch"].ToString();
                    model.CreditTo = dsBookingDetails.Tables[0].Rows[0]["CreditTo"].ToString();
                    model.PlotSize = dsBookingDetails.Tables[0].Rows[0]["Totalarea"].ToString();
                    model.DevelopmentCharge = dsBookingDetails.Tables[0].Rows[0]["DevelopmentCharge"].ToString();
                    model.BookingPercent = dsBookingDetails.Tables[0].Rows[0]["BookingPercent"].ToString();
                    // model.TotalPLC = dsBookingDetails.Tables[0].Rows[0]["PLCCharge"].ToString();
                    if (dsBookingDetails.Tables[1] != null)
                    {
                        if (dsBookingDetails.Tables[1].Rows.Count > 0)
                        {
                            model.Dimension = dsBookingDetails.Tables[1].Rows[0]["PLCName"].ToString();

                        }
                    }
                    else
                    {
                        model.Dimension = "N/A";
                        model.TotalPLC = "0";
                    }

                }
            }
            else
            {
                model.BookingDate = DateTime.Now.ToString("dd/MM/yyyy");
                model.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;
            }

            Plot obj = new Plot();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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



            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region bindPaymentPlan
            List<SelectListItem> BindPaymentPlan = Common.BindPaymentPlan();
            ViewBag.BindPaymentPlan = BindPaymentPlan;
            #endregion bindPaymentPlan


            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            DataSet ds = model.PLCList();
            model.dtPLC = ds.Tables[0];

            return View(model);
        }

        public ActionResult GetCustomerNameFromCustomerID(string CustomerID)
        {
            Plot model = new Plot();
            try
            {


                model.LoginId = CustomerID;

                #region GetCustomerName
                DataSet dsCustomerName = model.GetCustomerName();
                if (dsCustomerName != null && dsCustomerName.Tables[0].Rows.Count > 0)
                {
                    model.CustomerName = dsCustomerName.Tables[0].Rows[0]["Name"].ToString();
                    model.LoginId = dsCustomerName.Tables[0].Rows[0]["LoginId"].ToString();
                    model.AssociateID = dsCustomerName.Tables[0].Rows[0]["AssociateLoginId"].ToString();
                    model.AssociateName = dsCustomerName.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.CustomerID = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                model.CustomerID = "";
                model.Result = "no";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAssociateName(string AssociateID)
        {
            Plot model = new Plot();
            try
            {

                model.LoginId = AssociateID;

                #region GetSiteRate
                DataSet dsAssociateName = model.GetSponsorName();
                if (dsAssociateName != null && dsAssociateName.Tables[0].Rows.Count > 0)
                {
                    model.AssociateName = dsAssociateName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsAssociateName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.AssociateName = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                model.AssociateName = "";
                model.Result = "no";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CheckPlot(string SiteID, string SectorID, string BlockID, string PlotNumber)
        {

            Plot model = new Plot();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID;
            model.PlotNumber = PlotNumber;
            DataSet dsblock = model.CheckPlotAvailibility();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Result = "no";
                }
                else
                {
                    model.Result = "yes";
                    model.BookingPercent = dsblock.Tables[0].Rows[0]["BookingPercent"].ToString();
                    model.PlotSize = dsblock.Tables[0].Rows[0]["TotalArea"].ToString();
                    model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmount"].ToString();
                    model.TotalPLC = dsblock.Tables[0].Rows[0]["TotalPLC"].ToString();
                    model.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    model.PlotSize = dsblock.Tables[0].Rows[0]["TotalArea"].ToString();
                    if (dsblock.Tables[1].Rows.Count > 0)
                    {
                        model.Dimension = dsblock.Tables[1].Rows[0]["PLCName"].ToString();
                    }
                    else
                    {
                        model.Dimension = "N/A";
                    }

                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
            //return View();
        }

        [HttpPost]

        public ActionResult PlotBooking(Plot obj, string Save)
        {

            string FormName = "";
            string Controller = "";
            if (!string.IsNullOrEmpty(Save))
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

                    obj.Discount = string.IsNullOrEmpty(obj.Discount) ? "0" : obj.Discount;
                    obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                    obj.BookingDate = string.IsNullOrEmpty(obj.BookingDate) ? null : Common.ConvertToSystemDate(obj.BookingDate, "dd/MM/yyyy");

                    obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                    obj.AddedBy = Session["Pk_AdminId"].ToString();
                    obj.MLMLoginId = "1";
                    DataSet ds = obj.SavePlotBooking();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {

                            TempData["Plot"] = "Plot Booked successfully !";
                            TempData["Booking"] = "Booking ID : " + ds.Tables[0].Rows[0]["BookingNo"].ToString();

                            string Bookno = ds.Tables[0].Rows[0]["BookingNo"].ToString();
                            string Bookamt = ds.Tables[0].Rows[0]["BookingAmt"].ToString();
                            string AsstName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                            string plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                            string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                            if (Request["chksendmsg"] == "on")
                            {
                                try
                                {
                                    string msg = "DEAR CUSTOMER,CONGRATULATION, YOU HAVE BOOKED A PLOT IN " + ds.Tables[0].Rows[0]["SiteName"].ToString() + " with plot no " + plot + ", WE ARE PLEASED TO WELCOME YOU IN KDS ASSOCITATES FAMILY. BEST REGARDS TEAM KDS.Thankyou";
                                    BLSMS.SendSMSNew(mob, msg);


                                }
                                catch { }
                                try
                                {
                                    string msg1 = "DEAR CUSTOMER RECEIVED WITH THANKS FROM AMOUNT RS " + obj.PayAmount + ", AGAINST PLOT No (" + plot + ")" + "Thankyou";
                                    BLSMS.SendSMSNew(mob, msg1);
                                }
                                catch { }
                                mob = "9795710000";
                                try
                                {
                                    string msg = "DEAR CUSTOMER,CONGRATULATION, YOU HAVE BOOKED A PLOT IN " + ds.Tables[0].Rows[0]["SiteName"].ToString() + " with plot no " + plot + ", WE ARE PLEASED TO WELCOME YOU IN KDS ASSOCITATES FAMILY. BEST REGARDS TEAM KDS.Thankyou";
                                    BLSMS.SendSMSNew(mob, msg);


                                }
                                catch { }
                                try
                                {
                                    string msg1 = "DEAR CUSTOMER RECEIVED WITH THANKS FROM AMOUNT RS " + obj.PayAmount + ", AGAINST PLOT No (" + plot + ")" + "Thankyou";
                                    BLSMS.SendSMSNew(mob, msg1);
                                }
                                catch { }
                            }
                        }
                        else
                        {
                            TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            TempData["Booking"] = "";

                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["Plot"] = ex.Message;
                    TempData["Booking"] = "";
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

                    obj.PLCName = plcid;

                    obj.Discount = string.IsNullOrEmpty(obj.Discount) ? "0" : obj.Discount;
                    obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                    obj.BookingDate = string.IsNullOrEmpty(obj.BookingDate) ? null : Common.ConvertToSystemDate(obj.BookingDate, "dd/MM/yyyy");

                    obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                    obj.AddedBy = Session["Pk_AdminId"].ToString();
                    obj.MLMLoginId = "1";
                    DataSet ds = obj.UpdatePlotBooking();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {

                            TempData["Plot"] = "Booking Updated successfully !";


                        }
                        else
                        {
                            TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            TempData["Booking"] = "";

                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["Plot"] = ex.Message;
                    TempData["Booking"] = "";
                }

            }
            FormName = "PlotBooking";
            Controller = "Admin";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult BookingList()
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

            return View(model);
        }
        [HttpPost]

        public ActionResult BookingList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.GetBookingDetailsList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Plot obj = new Plot();
                        obj.PK_BookingId = r["PK_BookingId"].ToString();

                        obj.CustomerID = r["CustomerID"].ToString();
                        obj.CustomerLoginID = r["CustomerLoginID"].ToString();
                        obj.CustomerName = r["CustomerName"].ToString();
                        obj.AssociateID = r["AssociateID"].ToString();
                        obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                        obj.AssociateName = r["AssociateName"].ToString();
                        obj.PlotNumber = r["PlotInfo"].ToString();
                        obj.BookingDate = r["BookingDate"].ToString();
                        obj.BookingAmount = r["BookingAmt"].ToString();
                        obj.PaymentPlan = r["PaymentPlan"].ToString();
                        obj.BookingNumber = r["BookingNo"].ToString();
                        obj.PlotRate = r["PlotRate"].ToString();
                        obj.PLCAmount = r["PLCCharge"].ToString();
                        obj.PlotArea = r["TotalArea"].ToString();
                        obj.PlotAmount = r["ActualPlotAmount"].ToString();
                        obj.IsEMICalculated = r["IsEMICalculated"].ToString();
                        obj.Discount = r["Discount"].ToString();
                        obj.NetPlotAmount = r["NetPlotAmount"].ToString();
                        obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                        obj.EncryptKey = Crypto.Encrypt(r["PK_BookingDetailsId"].ToString());
                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                }
            }
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


            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();

            DataSet dsSector = model.GetSectorList();
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
            DataSet dsblock1 = model.GetBlockList();


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


            return View(model);
        }

        public ActionResult PlotAllotment()
        {
            Plot obj = new Plot();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            return View();
        }
        [HttpPost]
        public ActionResult PlotAllotment(Plot obj, string GetDetails)
        {

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = obj.GetSectorList();

            if (dsSector != null && dsSector.Tables.Count > 0)
            {
                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                }
            }
            ViewBag.ddlSector = ddlSector;
            #endregion

            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();
            Master objmodel = new Master();

            DataSet dsblock = obj.GetBlockList();

            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                }

            }

            ViewBag.ddlBlock = lstBlock;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            if (!string.IsNullOrEmpty(GetDetails))
            {
                dsblock = obj.FillBookedPlotDetails();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {

                    if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        obj.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                        obj.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                        obj.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                        obj.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                        obj.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                        obj.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                        obj.PaymentDate = dsblock.Tables[0].Rows[0]["PaymentDate"].ToString();
                        obj.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                        obj.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                        obj.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                        obj.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                        obj.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                        obj.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                        obj.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                        obj.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                        obj.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                        obj.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                        obj.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                        obj.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();
                        obj.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                        obj.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                        obj.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                        obj.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                        obj.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();
                        obj.hdBookingNo = dsblock.Tables[0].Rows[0]["BookingNo"].ToString();
                        obj.PLCAmount = dsblock.Tables[0].Rows[0]["PLCAmount"].ToString();

                    }
                    else
                    {
                        TempData["PlotAllotment"] = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["PlotAllotment"] = "No record found !";
                }
            }
            else
            {
                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                DataSet ds = obj.SavePlotAllotment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PlotAllotment"] = " Plot Allotted successfully !";
                        string name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string Plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string amt = obj.PaidAmount;

                        //string str = BLSMS.PlotAllotment(name, Plot, amt);
                        try
                        {
                            // BLSMS.SendSMS(mob, str);
                        }
                        catch { }
                        return RedirectToAction("PlotAllotment");
                    }
                    else
                    {
                        TempData["PlotAllotment"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }

            return View(obj);
        }
        public ActionResult PayEMI()
        {
            Plot obj = new Plot();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            return View();
        }
        [HttpPost]
        public ActionResult PayEMI(Plot obj, string GetDetails)
        {

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = obj.GetSectorList();

            if (dsSector != null && dsSector.Tables.Count > 0)
            {
                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                }
            }
            ViewBag.ddlSector = ddlSector;
            #endregion

            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();
            Master objmodel = new Master();

            DataSet dsblock = obj.GetBlockList();

            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                }

            }

            ViewBag.ddlBlock = lstBlock;
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            #endregion
            if (!string.IsNullOrEmpty(GetDetails))
            {
                List<Plot> lst = new List<Plot>();
                dsblock = obj.FillBookedPlotDetailsForEmi();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {

                    if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        obj.Result = "yes";
                        obj.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                        obj.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                        obj.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                        obj.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                        obj.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                        obj.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                        obj.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");
                        obj.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                        obj.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                        obj.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                        obj.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                        obj.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                        obj.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                        obj.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                        obj.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                        obj.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                        obj.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                        obj.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                        obj.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();
                        obj.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                        obj.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                        obj.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                        obj.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();
                        obj.hdBookingNo = dsblock.Tables[0].Rows[0]["BookingNo"].ToString();
                        if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow r in dsblock.Tables[1].Rows)
                            {
                                Plot obj1 = new Plot();
                                obj1.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                                obj1.PK_BookingId = r["Fk_BookingId"].ToString();
                                obj1.InstallmentNo = r["InstallmentNo"].ToString();
                                obj1.InstallmentDate = r["InstallmentDate"].ToString();
                                obj1.PaymentDate = r["PaymentDate"].ToString();
                                obj1.PaidAmount = r["PaidAmount"].ToString();
                                obj1.InstallmentAmount = r["InstAmt"].ToString();
                                obj1.PaymentMode = r["PaymentModeName"].ToString();
                                obj1.DueAmount = r["DueAmount"].ToString();
                                obj1.CssClass = r["CssClass"].ToString();

                                lst.Add(obj1);
                            }
                            obj.lstPlot = lst;
                        }

                    }
                    else
                    {
                        TempData["PayEMI"] = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["PayEMI"] = "No record found !";
                }
            }
            else
            {
                try
                {
                    //obj.TransactionDate = obj.TransactionDate == "" || obj.TransactionDate = null&&(Convert.obj.TransactionDate);

                    obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                    obj.AddedBy = Session["Pk_AdminId"].ToString();
                    obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                    DataSet ds = obj.SaveEMIPayment();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {

                            TempData["PayEMI"] = " EMI Paid !";
                            string name = ds.Tables[0].Rows[0]["Name"].ToString();
                            string Plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                            string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                            string bookno = ds.Tables[0].Rows[0]["BookingNo"].ToString();
                            string instno = ds.Tables[0].Rows[0]["InstallmentNo"].ToString();
                            string amt = obj.PaidAmount;


                            //  string str = BLSMS.EMIPayment(name, Plot, bookno, instno, amt);
                            try
                            {
                                // BLSMS.SendSMS(mob, str);
                            }
                            catch { }
                        }
                        else
                        {
                            TempData["PayEMI"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["PayEMI"] = ex.Message;
                }
            }

            return View(obj);
        }
        public ActionResult Direct()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Direct(Reports obj)
        {

            DataSet ds = obj.GetDirectList();
            obj.directList = ds.Tables[0];
            return View(obj);
        }
        public ActionResult Downline()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Downline(Reports obj)
        {

            DataSet ds = obj.GetDownline();
            obj.directList = ds.Tables[0];
            return View(obj);
        }

        public ActionResult HoldPlot()
        {
            Plot model = new Plot();


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
        public ActionResult HoldPlot(Plot obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SavePlotHold();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PlotHold"] = " Plot is on Hold !";
                    }
                    else
                    {
                        TempData["PlotHold"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["PlotHold"] = ex.Message;
            }
            return RedirectToAction("HoldPlot");
        }
        public ActionResult HoldList()
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

            return View();
        }
        [HttpPost]
        public ActionResult HoldList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            DataSet ds = model.GetPlotHoldList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_PlotHoldID = r["PK_PlotHoldID"].ToString();
                    obj.PlotNumber = r["Plot"].ToString();
                    obj.HoldFrom = r["HoldFrom"].ToString();
                    obj.HoldTo = r["HoldTo"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.HoldType = r["HoldType"].ToString();
                    obj.Amount = r["Amount"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
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

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = model.GetSectorList();
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
            DataSet dsblock = model.GetBlockList();


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
            return View(model);
            #endregion
        }

        public ActionResult DeletePlotHold(string PK_PlotHoldID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();

                model.PK_PlotHoldID = PK_PlotHoldID;
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.DeletePlotHold();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["DelPlot"] = "Deleted successfully !";
                    }
                    else
                    {
                        TempData["DelPlot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DelPlot"] = ex.Message;
            }
            FormName = "HoldList";
            Controller = "Admin";

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult CancelPlot()
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
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank

            return View();
        }
        [HttpPost]
        public ActionResult CancelPlot(Plot model)
        {

            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetBookingDetailsList1();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_BookingId = r["PK_BookingId"].ToString();

                    obj.CustomerID = r["CustomerID"].ToString();
                    obj.CustomerLoginID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateID"].ToString();
                    obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PlotNumber = r["PlotInfo"].ToString();
                    obj.BookingDate = r["BookingDate"].ToString();
                    obj.BookingAmount = r["BookingAmt"].ToString();
                    obj.PaymentPlanID = r["PlanName"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;

            }
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
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            return View(model);
        }

        public ActionResult CancelPlotBooking(string BookingID, string Remark, string PaidAmount, string PaymentMode, string TransactionNumber, string TransactionDate, string BankName, string BankBranch, string CreditTo)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();

                model.PK_BookingId = BookingID;
                model.CancelRemark = Remark;
                model.PaidAmount = PaidAmount;
                model.PaymentMode = PaymentMode;
                model.TransactionNumber = TransactionNumber;
                model.TransactionDate = string.IsNullOrEmpty(TransactionDate) ? null : Common.ConvertToSystemDate(TransactionDate, "dd/MM/yyyy");
                model.BankName = BankName;
                model.BankBranch = BankBranch;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.CreditTo = CreditTo;

                DataSet ds = model.CancelPlotBooking();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["CancelPlot"] = "Plot Booking Cancelled successfully !";
                    }
                    else
                    {
                        TempData["CancelPlot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["CancelPlot"] = ex.Message;
            }
            FormName = "CancelPlot";
            Controller = "Admin";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult PlotRegistry()
        {
            Plot obj = new Plot();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            #endregion
            return View();
        }
        [HttpPost]
        public ActionResult PlotRegistry(Plot obj, string GetDetails)
        {

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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
            DataSet dsSector = obj.GetSectorList();

            if (dsSector != null && dsSector.Tables.Count > 0)
            {
                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                }
            }
            ViewBag.ddlSector = ddlSector;
            #endregion

            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();
            Master objmodel = new Master();

            DataSet dsblock = obj.GetBlockList();

            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                }

            }

            ViewBag.ddlBlock = lstBlock;
            #endregion
            if (!string.IsNullOrEmpty(GetDetails))
            {
                obj.HoldType = "Registry";
                dsblock = obj.FillBookedPlotDetails();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {

                    if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        obj.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                        obj.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                        obj.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                        obj.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                        obj.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                        obj.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                        obj.PaymentDate = dsblock.Tables[0].Rows[0]["PaymentDate"].ToString();
                        obj.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                        obj.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                        obj.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                        obj.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                        obj.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                        obj.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                        obj.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                        obj.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                        obj.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                        obj.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                        obj.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                        obj.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();
                        obj.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                        obj.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                        obj.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                        obj.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                        obj.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();


                    }
                    else
                    {
                        TempData["PlotRegistry"] = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["PlotRegistry"] = "No record found !";
                }
            }
            else
            {
                try
                {

                    obj.RegistrationDate = string.IsNullOrEmpty(obj.RegistrationDate) ? null : Common.ConvertToSystemDate(obj.RegistrationDate, "dd/MM/yyyy");
                    obj.AddedBy = Session["Pk_AdminId"].ToString();


                    DataSet ds = obj.SavePlotRegistry();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["PlotRegistry"] = " Plot Registered successfully !";
                            return RedirectToAction("PlotRegistry");
                        }
                        else
                        {
                            TempData["PlotRegistry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["PlotRegistry"] = ex.Message;
                }
            }

            return View(obj);
        }

        public ActionResult ApprovePayent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ApprovePayent(Reports obj)
        {
            DataSet ds = obj.GetPaymentList();
            obj.userList = ds.Tables[0];
            return View(obj);
        }
        public ActionResult DeletePayment(string id)
        {
            Reports obj = new Reports();
            obj.id = Crypto.Decrypt(id);
            obj.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = obj.DeletePayment();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {
                    return RedirectToAction("ApprovePayent");
                }
                else
                {
                    return RedirectToAction("ApprovePayent");
                }

            }
            return RedirectToAction("ApprovePayent");
        }
        public ActionResult ApprovePendingPayment(string id)
        {
            Reports obj = new Reports();
            obj.id = Crypto.Decrypt(id);
            obj.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = obj.ApprovePayment();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {
                    return RedirectToAction("ApprovePayent");
                }
                else
                {
                    return RedirectToAction("ApprovePayent");
                }

            }
            return RedirectToAction("ApprovePayent");
        }
        public ActionResult RejectPendingPayment(string id)
        {
            Reports obj = new Reports();
            obj.id = Crypto.Decrypt(id);
            obj.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = obj.RejectPayment();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {
                    try
                    {
                        string msg = "YOUR CHEQUE OF RS " + ds.Tables[0].Rows[0]["PaidAmount"].ToString() + "IS BOUNCE BY THE BANK PLEASE CONTACT KDS ASSOCIATES. Kindly login" + SoftwareDetails.Website + " Thankyou";
                        BLSMS.SendSMSNew(ds.Tables[0].Rows[0]["Mobile"].ToString(), msg);
                    }
                    catch { }
                    return RedirectToAction("ApprovePayent");
                }
                else
                {
                    return RedirectToAction("ApprovePayent");
                }

            }
            return RedirectToAction("ApprovePayent");
        }
        public ActionResult CancelPlotList()
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
            return View();
        }
        [HttpPost]
        public ActionResult CancelPlotList(Reports obj)
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
            obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
            obj.Todate = string.IsNullOrEmpty(obj.Todate) ? null : Common.ConvertToSystemDate(obj.Todate, "dd/MM/yyyy");
            obj.SiteID = obj.SiteID == "0" ? null : obj.SiteID;
            obj.SectorID = obj.SectorID == "0" ? null : obj.SectorID;
            obj.BlockID = obj.BlockID == "0" ? null : obj.BlockID;
            DataSet ds = obj.GetBookingDetailsList();
            obj.lstDetails = ds.Tables[0];
            return View(obj);
        }

        public ActionResult CollectionReport()
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
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View();
        }

        [HttpPost]
        public ActionResult CollectionReport(Reports obj)
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


            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            model.SiteID = obj.SiteID;
            DataSet dsSector = model.GetSectorList();
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
            model.SectorID = obj.SectorID;
            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock1 = model.GetBlockList();


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


            #region BindPaymentType
            List<SelectListItem> BindPaymentType = Common.BindPaymentType();
            ViewBag.BindPaymentType = BindPaymentType;
            #endregion BindPaymentType
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
            obj.Todate = string.IsNullOrEmpty(obj.Todate) ? null : Common.ConvertToSystemDate(obj.Todate, "dd/MM/yyyy");
            obj.SiteID = obj.SiteID == "0" ? null : obj.SiteID;
            obj.SectorID = obj.SectorID == "0" ? null : obj.SectorID;
            obj.BlockID = obj.BlockID == "0" ? null : obj.BlockID;
            obj.PaymentType = obj.PaymentType == "0" ? null : obj.PaymentType;
            obj.PaymentMode = obj.PaymentMode == "0" ? null : obj.PaymentMode;
            obj.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = obj.GetCollectionReport();
            obj.lstDetails = ds.Tables[0];
            return View(obj);
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
            DataSet ds = obj.DueInstallmentReport();
            obj.lstDetails = ds.Tables[0];
            return View(obj);
        }


        public ActionResult AssociateRegistration(string UserID)
        {


            TraditionalAssociate model = new TraditionalAssociate();
            try
            {
                if (UserID != null)
                {

                    model.UserID = Crypto.Decrypt(UserID);
                    //model.UserID = UserID;
                    DataSet dsPlotDetails = model.GetList();
                    if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                    {
                        model.Fk_UserId = UserID;
                        model.sponserId = dsPlotDetails.Tables[0].Rows[0]["SponsorId"].ToString();
                        model.sponsorName = dsPlotDetails.Tables[0].Rows[0]["SponsorName"].ToString();
                        model.FirstName = dsPlotDetails.Tables[0].Rows[0]["FirstName"].ToString();
                        model.LastName = dsPlotDetails.Tables[0].Rows[0]["LastName"].ToString();
                        model.DesignationID = dsPlotDetails.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                        model.DesignationName = dsPlotDetails.Tables[0].Rows[0]["DesignationName"].ToString();

                        model.Contact = dsPlotDetails.Tables[0].Rows[0]["Mobile"].ToString();
                        model.Email = dsPlotDetails.Tables[0].Rows[0]["Email"].ToString();
                        model.State = dsPlotDetails.Tables[0].Rows[0]["State"].ToString();
                        model.City = dsPlotDetails.Tables[0].Rows[0]["City"].ToString();
                        model.Address = dsPlotDetails.Tables[0].Rows[0]["Address"].ToString();
                        model.pinCode = dsPlotDetails.Tables[0].Rows[0]["PinCode"].ToString();
                        model.PanNo = dsPlotDetails.Tables[0].Rows[0]["PanNumber"].ToString();

                        model.AdharNumber = dsPlotDetails.Tables[0].Rows[0]["AdharNumber"].ToString();
                        model.BankAccountNo = dsPlotDetails.Tables[0].Rows[0]["MemberAccNo"].ToString();
                        model.BankName = dsPlotDetails.Tables[0].Rows[0]["MemberBankName"].ToString();
                        model.BankBranch = dsPlotDetails.Tables[0].Rows[0]["MemberBranch"].ToString();
                        model.IFSCCode = dsPlotDetails.Tables[0].Rows[0]["IFSCCode"].ToString();
                        model.ProfilePic = dsPlotDetails.Tables[0].Rows[0]["ProfilePic"].ToString();
                        //model.Signature = dsPlotDetails.Tables[0].Rows[0]["Signature"].ToString();
                    }
                }

                else
                {
                    // ViewBag.Disabled = "";

                }


                TraditionalAssociate obj = new TraditionalAssociate();


                #region ddlDesignation

                int desgnationCount = 0;
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                DataSet dsdesignation = obj.GetDesignationList();
                if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsdesignation.Tables[0].Rows)
                    {
                        if (desgnationCount == 0)
                        {
                            ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                        }
                        ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                        desgnationCount = desgnationCount + 1;
                    }
                }

                ViewBag.ddlDesignation = ddlDesignation;

                #endregion

                #region BindCommissionType
                List<SelectListItem> lstcommmissionType = Common.BindCommissionType();
                ViewBag.lstcommmissionType = lstcommmissionType;
                #endregion BindCommissionType
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        [HttpPost]
        public ActionResult AssociateRegistration(TraditionalAssociate model, string btnSave)
        {

            #region ddlDesignation

            int desgnationCount = 0;
            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            DataSet dsdesignation = model.GetDesignationList();
            if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsdesignation.Tables[0].Rows)
                {
                    if (desgnationCount == 0)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                    }
                    ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                    desgnationCount = desgnationCount + 1;
                }
            }

            ViewBag.ddlDesignation = ddlDesignation;

            #endregion
            string FormName = "";
            string Controller = "";
            try
            {
                Random rnd = new Random();
                int ctrPasword = rnd.Next(111111, 999999);
                model.Password = Crypto.Encrypt(ctrPasword.ToString());

                DataSet dsRegistration = new DataSet();
                if (!string.IsNullOrEmpty(btnSave))
                {
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    model.CommissionType = "Level";
                    dsRegistration = model.AssociateRegistration();
                }
                else
                {
                    model.Fk_UserId = Crypto.Decrypt(model.Fk_UserId);
                    model.UpdatedBy = Session["Pk_AdminId"].ToString();

                    dsRegistration = model.UpdateAssociate();
                }


                if (dsRegistration != null && dsRegistration.Tables.Count > 0)
                {
                    if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (!string.IsNullOrEmpty(btnSave))
                        {
                            Session["DisplayNameConfirm"] = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                            Session["LoginIDConfirm"] = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["PasswordConfirm"] = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                            //Session["PKUserID"] = Crypto.Encrypt(dsRegistration.Tables[0].Rows[0]["PKUserID"].ToString());

                            string name = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                            string id = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                            string pass = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                            string mob = model.Contact;
                            try
                            {
                                string msg = "Dear " + name + " Welcome to VNV INFRA STRCUTURE Family.Your Login Id " + id + " and Password is " + pass + ". Kindly login" + SoftwareDetails.Website + " Thankyou";
                                BLSMS.SendSMSNew(model.Contact, msg);
                            }
                            catch { }
                            try
                            {
                                string msg = "Dear " + name + " Welcome to VNV INFRA STRCUTURE Family.Your Login Id " + id + " and Password is " + pass + ". Kindly login" + SoftwareDetails.Website + " Thankyou";
                                BLSMS.SendSMSNew("9795710000", msg);
                            }
                            catch { }
                        }
                        else
                        {
                            TempData["Registration"] = "Associate Updated Successfully";
                            return RedirectToAction("AssociateRegistration");
                        }

                    }
                    else
                    {
                        TempData["Registration"] = dsRegistration.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            FormName = "ConfirmRegistrationAssociate";
            Controller = "Admin";

            return RedirectToAction(FormName, Controller);

        }
        public ActionResult ConfirmRegistrationAssociate()
        {
            return View();
        }

        public ActionResult PrintRecipt()
        {
            Plot model = new Plot();
            Session["PK_BookingDetailsId"] = null;
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;

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

            return View(model);
        }

        [HttpPost]

        public ActionResult PrintRecipt(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.List();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                    obj.PK_BookingId = r["PK_BookingID"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    //obj.TransactionDate = r["TransactionDate"].ToString();
                    //obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PlotNumber = r["PlotInfo"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_BookingDetailsId"].ToString());
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
            DataSet dsblock1 = objmaster.GetBlockList();


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
            return View(model);
        }

        public ActionResult PrintAllotment(string id)
        {
            Plot newdata = new Plot();
            //newdata.PK_BookingId = id;
            if (Session["PK_BookingDetailsId"] != null)
            {
                newdata.PK_BookingDetailsId = Session["PK_BookingDetailsId"].ToString();
            }
            else
            {
                newdata.EncryptKey = Crypto.Decrypt(id);
                newdata.PK_BookingDetailsId = Crypto.Decrypt(id);
            }


            //ViewBag.Name = Session["Name"].ToString();
            DataSet ds = newdata.PrintReceipt();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    newdata.Result = "yes";
                    ViewBag.CustomerID = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                    ViewBag.NetPlotAmountInWords = ds.Tables[0].Rows[0]["NetPlotAmountInWords"].ToString();
                    ViewBag.AmountInWords = ds.Tables[0].Rows[0]["AmountInWords"].ToString();
                    ViewBag.ReceiptNo = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
                    ViewBag.CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                    ViewBag.CustomerContact = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    ViewBag.RefrenceId = ds.Tables[0].Rows[0]["RefrenceId"].ToString();
                    ViewBag.UpadtedById = ds.Tables[0].Rows[0]["UpadtedById"].ToString();
                    ViewBag.ReceiptNo = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
                    ViewBag.ProjectName = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                    ViewBag.PlotNumber = ds.Tables[0].Rows[0]["PlotNumber"].ToString();
                    ViewBag.DepositeDate = ds.Tables[0].Rows[0]["DepositeDate"].ToString();
                    ViewBag.PaymentMode = ds.Tables[0].Rows[0]["PaymentMode"].ToString();
                    ViewBag.TransactionNo = ds.Tables[0].Rows[0]["TransactionNo"].ToString();
                    ViewBag.TransactionDate = ds.Tables[0].Rows[0]["TransactionDate"].ToString();
                    ViewBag.DevelopmentCharge = ds.Tables[0].Rows[0]["DevelopmentCharge"].ToString();
                    ViewBag.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                    ViewBag.PlotArea = ds.Tables[0].Rows[0]["PlotArea"].ToString();
                    ViewBag.SectorName = ds.Tables[0].Rows[0]["SectorName"].ToString();
                    ViewBag.SiteLocation = ds.Tables[0].Rows[0]["SiteLocation"].ToString();
                    ViewBag.BlockName = ds.Tables[0].Rows[0]["BlockName"].ToString();
                    ViewBag.Rate = ds.Tables[0].Rows[0]["Rate"].ToString();
                    ViewBag.PlotAmount = ds.Tables[0].Rows[0]["PlotAmount"].ToString();
                    ViewBag.TotalPLC = ds.Tables[0].Rows[0]["TotalPLC"].ToString();
                    ViewBag.TotalAMount = ds.Tables[0].Rows[0]["TotalAMount"].ToString();
                    ViewBag.PaidAmount = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                    ViewBag.BalanceAmt = ds.Tables[0].Rows[0]["BalanceAmt"].ToString();
                    ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                    ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                    ViewBag.Pincode = ds.Tables[0].Rows[0]["Pincode"].ToString();
                    ViewBag.Relation = ds.Tables[0].Rows[0]["Relation"].ToString();
                    ViewBag.FathersName = ds.Tables[0].Rows[0]["FathersName"].ToString();
                    ViewBag.CurrentStatus = ds.Tables[0].Rows[0]["CurrentStatus"].ToString();
                    ViewBag.PLC = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["PLC"].ToString()) ? "N/A" : ds.Tables[0].Rows[0]["PLC"].ToString();

                    ViewBag.TotalPaidAmount = ds.Tables[0].Rows[0]["TotalPaidAmount"].ToString();

                    ViewBag.NetAMount = ds.Tables[0].Rows[0]["PlotAmount"].ToString();
                    ViewBag.Discount = ds.Tables[0].Rows[0]["Discount"].ToString();
                    ViewBag.BalanceAmt = (decimal.Parse(ds.Tables[0].Rows[0]["BalanceAmt"].ToString()) - decimal.Parse(ds.Tables[0].Rows[0]["Discount"].ToString())).ToString();
                    ViewBag.CompanyName = Common.SoftwareDetails.CompanyName;
                    ViewBag.CompanyAddress = Common.SoftwareDetails.CompanyAddress;
                    ViewBag.Pin1 = Common.SoftwareDetails.Pin1;
                    ViewBag.State1 = Common.SoftwareDetails.State1;
                    ViewBag.City1 = Common.SoftwareDetails.City1;
                    ViewBag.ContactNo = Common.SoftwareDetails.ContactNo;
                    ViewBag.Website = Common.SoftwareDetails.Website;
                    ViewBag.EmailID = Common.SoftwareDetails.EmailID;
                }
            }

            return View(newdata);
        }

        public ActionResult DeleteAssociate(string UserID)
        {
            TraditionalAssociate obj = new TraditionalAssociate();
            obj.UserID = Crypto.Decrypt(UserID);
            obj.AddedBy = Session["PK_AdminId"].ToString();
            DataSet ds = obj.DeleteAssociate();
            return RedirectToAction("AssociateList");
        }

        public ActionResult PayoutReport(Reports obj)
        {
            DataSet ds = obj.GetPayoutReport();
            obj.payoutData = ds.Tables[0];
            return View(obj);
        }

        public ActionResult UnBlockMember(string UserID)
        {
            TraditionalAssociate obj = new TraditionalAssociate();
            obj.Fk_UserId = Crypto.Decrypt(UserID);
            obj.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = obj.BlockUser();
            return RedirectToAction("AssociateList");
        }


        public ActionResult ClubIncomeReport(Reports obj)
        {
            DataSet ds = obj.GetClubIncome();
            obj.payoutData = ds.Tables[0];
            return View(obj);
        }

        public ActionResult CustomerLedger()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustomerLedger(Reports obj, string print)
        {
            if (!string.IsNullOrEmpty(print))
            {
                Session["PK_BookingId"] = obj.id;
                return RedirectToAction("PrintLedger");

            }
            DataSet ds = obj.CustomerLedger();
            obj.lstDetails = ds.Tables[0];
            obj.customerLedger = ds.Tables[1];

            return View(obj);
        }
        public ActionResult PrintLedger()
        {
            Reports obj = new Reports();
            obj.id = Session["PK_BookingId"].ToString();
            DataSet ds = obj.CustomerLedger();
            ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
            ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
            ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
            ViewBag.PlotNumber = ds.Tables[0].Rows[0]["PlotNumber"].ToString();
            ViewBag.TotalArea = ds.Tables[0].Rows[0]["TotalArea"].ToString();
            ViewBag.PlotRate = ds.Tables[0].Rows[0]["PlotRate"].ToString();
            ViewBag.PLCPerc = ds.Tables[0].Rows[0]["PLCPerc"].ToString();
            ViewBag.PlotAmount = ds.Tables[0].Rows[0]["PlotAmount"].ToString();
            ViewBag.NetPlotAmount = ds.Tables[0].Rows[0]["NetPlotAmount"].ToString();
            ViewBag.TotalPlotAmount = ds.Tables[2].Rows[0]["TotalPlotAmount"].ToString();
            ViewBag.TotalPaidAmount = ds.Tables[2].Rows[0]["TotalPaidAmount"].ToString();
            ViewBag.TotalBalance = (Convert.ToDecimal(ViewBag.TotalPlotAmount) - Convert.ToDecimal(ViewBag.TotalPaidAmount)).ToString("0.00");
            obj.paymentDetails = ds.Tables[1];
            return View(obj);
        }
        public ActionResult GetPaymentDetails(string id)
        {
            List<Reports> lst = new List<Reports>();
            Reports obj = new Reports();
            obj.id = id;
            DataSet ds = obj.CustomerLedger();
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Reports objList = new Reports();
                    objList.TransactionDate = dr["TransactionDate"].ToString();
                    objList.Particular = dr["Particular"].ToString();
                    objList.NetPlotAmount = dr["NetPlotAmount"].ToString();
                    objList.PaidAmount = dr["PaidAmount"].ToString();
                    objList.Balance = dr["Balance"].ToString();
                    objList.CreditTo = dr["CreditTo"].ToString();
                    lst.Add(objList);
                }

            }
            obj.Name = ds.Tables[0].Rows[0]["Name"].ToString();
            obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
            obj.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
            obj.PlotNumber = ds.Tables[0].Rows[0]["PlotNumber"].ToString();
            obj.TotalArea = ds.Tables[0].Rows[0]["TotalArea"].ToString();
            obj.PlotRate = ds.Tables[0].Rows[0]["PlotRate"].ToString();
            obj.PLCPerc = ds.Tables[0].Rows[0]["PLCPerc"].ToString();
            obj.PlotAmount = ds.Tables[0].Rows[0]["PlotAmount"].ToString();
            obj.NetPlotAmount = ds.Tables[0].Rows[0]["NetPlotAmount"].ToString();
            obj.TotalPlotAmount = ds.Tables[2].Rows[0]["TotalPlotAmount"].ToString();
            obj.TotalPaidAmount = ds.Tables[2].Rows[0]["TotalPaidAmount"].ToString();
            obj.TotalBalance = (Convert.ToDecimal(obj.TotalPlotAmount) - Convert.ToDecimal(obj.TotalPaidAmount)).ToString("0.00");
            obj.details = lst;
            return Json(obj, JsonRequestBehavior.AllowGet);
            //obj.siteDetails = ds.Tables[2];
            //obj.paymentDetails = ds.Tables[3];
            //return Json(obj,JsonRequestBehavior.AllowGet);
        }
        public ActionResult PayoutRequestList()
        {
            Plot model = new Plot();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            return View();
        }
        [HttpPost]
        public ActionResult PayoutRequestList(Reports obj)
        {
            Plot model = new Plot();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
            obj.Todate = string.IsNullOrEmpty(obj.Todate) ? null : Common.ConvertToSystemDate(obj.Todate, "dd/MM/yyyy");
            DataSet ds = obj.GetPayoutRequest();
            obj.lstDetails = ds.Tables[0];

            return View(obj);
        }
        public ActionResult ApprovePayoutRequest(string id, string PaymentMode, string TransactionNumber, string TransactionDate, string BankName, string BankBranch, string Fk_BankId, string AMount)
        {
            Wallet obj = new Wallet();
            obj.Fk_UserId = id;
            obj.AddedBy = Session["Pk_AdminId"].ToString();
            obj.PaymentMode = PaymentMode;
            obj.TransactionNumber = TransactionNumber;
            obj.TransactionDate = string.IsNullOrEmpty(TransactionDate) ? null : Common.ConvertToSystemDate(TransactionDate, "dd/MM/yyyy");
            obj.BankName = BankName;
            obj.BankBranch = BankBranch;
            obj.Fk_BankId = Fk_BankId;
            obj.Amount = AMount;
            DataSet ds = obj.ApprovePayoutRequest();
            return RedirectToAction("PayoutRequestList");
        }

        public ActionResult ExpenseManagement()
        {
            return View();
        }

        public ActionResult ApproveKYC(string LoginId)
        {
            Reports obj = new Reports();
            obj.LoginId = Crypto.Decrypt(LoginId);
            DataSet ds = obj.GetPendingDocuments();
            obj.documentList = ds.Tables[0];
            return View(obj);

        }
        public ActionResult GetKYC(string LoginId)
        {
            Reports obj = new Reports();
            obj.LoginId = Crypto.Decrypt(LoginId);
            DataSet ds = obj.GetKYC();
            obj.documentList = ds.Tables[0];
            obj.MemberAccNo = ds.Tables[1].Rows[0]["MemberAccNo"].ToString();
            obj.MemberBankName = ds.Tables[1].Rows[0]["MemberBankName"].ToString();
            obj.MemberBranch = ds.Tables[1].Rows[0]["MemberBranch"].ToString();
            obj.IFSCCode = ds.Tables[1].Rows[0]["IFSCCode"].ToString();
            return View(obj);

        }
        public ActionResult UpdateKYCStatus(string id, string DocumentType, string Status)
        {
            Reports obj = new Reports();
            obj.PK_DocumentID = id;
            obj.DocumentType = DocumentType;
            obj.UpdatedBy = Session["Pk_AdminId"].ToString();
            obj.Status = Status;
            DataSet ds = obj.ApproveKYC();
            return RedirectToAction("ApproveKYC");
        }

        public ActionResult UpdateKYCStatusForDashBOard(string id, string DocumentType, string Status)
        {
            Reports obj = new Reports();
            obj.PK_DocumentID = id;
            obj.DocumentType = DocumentType;
            obj.UpdatedBy = Session["Pk_AdminId"].ToString();
            obj.Status = Status;
            DataSet ds = obj.ApproveKYC();
            return RedirectToAction("DashBoard");
        }

        public ActionResult GetPlotAreaReport(string Status, Reports obj1)
        {
            Plot obj = new Plot();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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

            #region ddlPlotStatus
            List<SelectListItem> ddlPlotStatus = Common.BindPlotStatus();
            ViewBag.ddlPlotStatus = ddlPlotStatus;
            #endregion ddlPlotStatus

            obj1.Status = string.IsNullOrEmpty(Status) ? obj1.Status : Status;
            obj1.FromDate = string.IsNullOrEmpty(obj1.FromDate) ? null : Common.ConvertToSystemDate(obj1.FromDate, "dd/MM/yyyy");
            obj1.Todate = string.IsNullOrEmpty(obj1.Todate) ? null : Common.ConvertToSystemDate(obj1.Todate, "dd/MM/yyyy");
            DataSet ds = obj1.GetPlotAreaReport();
            obj1.lstDetails = ds.Tables[0];
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.TotalPaidSqft = double.Parse(ds.Tables[0].Compute("sum(PaidSqft)", "").ToString()).ToString("0.00");
                ViewBag.TotalNetPlotAmount = double.Parse(ds.Tables[0].Compute("sum(NetPlotAmount)", "").ToString()).ToString("0.00");
                ViewBag.TotalPaidAMount = double.Parse(ds.Tables[0].Compute("sum(PaidAMount)", "").ToString()).ToString("0.00");
                ViewBag.TotalDueAmount = double.Parse(ds.Tables[0].Compute("sum(DueAmount)", "").ToString()).ToString("0.00");
            }

            return View(obj1);
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
            List<SelectListItem> ddlPLC = new List<SelectListItem>();
            ds = model.PLCList();
            ddlPLC.Add(new SelectListItem { Text = "Select PLC", Value = "0" });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {



                    ddlPLC.Add(new SelectListItem { Text = r["PLCName"].ToString(), Value = r["PK_PLCId"].ToString() });


                }
            }
            ViewBag.ddlPLC = ddlPLC;

            return View(model);
        }

        [HttpPost]

        public ActionResult PlotList(Master model, string btnupdateLocation)
        {


            List<Master> lst = new List<Master>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.PLCName = model.PLCName == "0" ? null : model.PLCName;
            model.PlotID = null;
            DataSet ds = model.PlotReport();

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

            List<SelectListItem> ddlPLC = new List<SelectListItem>();
            ds = model.PLCList();
            ddlPLC.Add(new SelectListItem { Text = "Select PLC", Value = "0" });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {



                    ddlPLC.Add(new SelectListItem { Text = r["PLCName"].ToString(), Value = r["PK_PLCId"].ToString() });


                }
            }
            ViewBag.ddlPLC = ddlPLC;
            return View(model);
        }


        public ActionResult BounceChequeReport()
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
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View();
        }

        [HttpPost]
        public ActionResult BounceChequeReport(Reports obj)
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
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
            obj.Todate = string.IsNullOrEmpty(obj.Todate) ? null : Common.ConvertToSystemDate(obj.Todate, "dd/MM/yyyy");
            obj.SiteID = obj.SiteID == "0" ? null : obj.SiteID;
            obj.SectorID = obj.SectorID == "0" ? null : obj.SectorID;
            obj.BlockID = obj.BlockID == "0" ? null : obj.BlockID;
            obj.PaymentType = obj.PaymentType == "0" ? null : obj.PaymentType;
            obj.PaymentMode = obj.PaymentMode == "0" ? null : obj.PaymentMode;
            DataSet ds = obj.GetChequeBounceReport();
            obj.lstDetails = ds.Tables[0];
            return View(obj);
        }

        public ActionResult GetLedger()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetLedger(Reports obj)
        {
            obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
            obj.Todate = string.IsNullOrEmpty(obj.Todate) ? null : Common.ConvertToSystemDate(obj.Todate, "dd/MM/yyyy");
            DataSet ds = obj.GetLedger();
            obj.lstDetails = ds.Tables[0];
            return View(obj);
        }


        public ActionResult DayBook()
        {
            Reports obj = new Reports();
            obj.FromDate = Common.ConvertToSystemDate(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy");
            obj.Todate = Common.ConvertToSystemDate(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy");
            DataSet ds = obj.GetLedger();
            obj.lstDetails = ds.Tables[0];
            return View(obj);
        }
        [HttpPost]
        public ActionResult DayBook(Reports obj)
        {
            obj.FromDate = string.IsNullOrEmpty(obj.FromSearchDate) ? null : Common.ConvertToSystemDate(obj.FromSearchDate, "dd/MM/yyyy");
            obj.Todate = string.IsNullOrEmpty(obj.ToSearchDate) ? null : Common.ConvertToSystemDate(obj.ToSearchDate, "dd/MM/yyyy");
            DataSet ds = obj.GetLedger();

            obj.lstDetails = ds.Tables[0];
            return View(obj);
        }

        public ActionResult ExpenseEntry(string id)
        {
            Master obj = new Master();
            #region ddlHead
            // int count1 = 0;
            List<SelectListItem> ddlHead = new List<SelectListItem>();
            DataSet dsSite = obj.GetAccountHead();

            ddlHead.Add(new SelectListItem { Text = "Select Head", Value = "0" });

            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {

                    ddlHead.Add(new SelectListItem { Text = r["SubAccountHead"].ToString(), Value = r["Pk_SubHeadId"].ToString() });
                    //  count1 = count1 + 1;

                }
            }
            ViewBag.ddlHead = ddlHead;
            #endregion


            Plot obj1 = new Plot();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj1.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            DayBook daybook = new DayBook();
            daybook.Pk_LedgerId = Crypto.Decrypt(id);
            DataSet ds = daybook.GetLedger();
            if (ds != null)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    daybook.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    daybook.AccountId = ds.Tables[0].Rows[0]["AccountId"].ToString();
                    daybook.TransactionDate = ds.Tables[0].Rows[0]["TransactionDate"].ToString();
                    daybook.Amount = ds.Tables[0].Rows[0]["CrAmount"].ToString();
                    daybook.Narration = ds.Tables[0].Rows[0]["Narration"].ToString();
                    daybook.PaymentMode = ds.Tables[0].Rows[0]["PK_paymentID"].ToString();
                    daybook.TransactionNumber = ds.Tables[0].Rows[0]["TransactionNo"].ToString();
                    daybook.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                    daybook.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                    daybook.ChequeDate = ds.Tables[0].Rows[0]["ChequeDate"].ToString();
                    daybook.Pk_LedgerId = ds.Tables[0].Rows[0]["Pk_LedgerId"].ToString();
                }
                else
                {

                }
            }
            return View(daybook);
        }
        [HttpPost]

        public ActionResult ExpenseEntry(DayBook obj, string Update)
        {
            Master obj11 = new Master();
            #region ddlHead
            // int count1 = 0;
            List<SelectListItem> ddlHead = new List<SelectListItem>();
            DataSet dsSite = obj11.GetAccountHead();

            ddlHead.Add(new SelectListItem { Text = "Select Head", Value = "0" });

            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {

                    ddlHead.Add(new SelectListItem { Text = r["SubAccountHead"].ToString(), Value = r["Pk_SubHeadId"].ToString() });
                    //  count1 = count1 + 1;

                }
            }
            ViewBag.ddlHead = ddlHead;
            #endregion


            Plot obj1 = new Plot();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj1.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            try
            {
                DataSet ds = new DataSet();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.TransactionDate = Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.ChequeDate = string.IsNullOrEmpty(obj.ChequeDate) ? null : Common.ConvertToSystemDate(obj.ChequeDate, "dd/MM/yyyy");
                if (!string.IsNullOrEmpty(Update))
                {
                    ds = obj.UpdateDayBook();
                }
                else
                {
                    ds = obj.SaveDayBook();
                }

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        if (!string.IsNullOrEmpty(Update))
                        {
                            TempData["DayBook"] = "Data Updated Successfully";
                            return View();
                        }
                        else
                        {
                            TempData["DayBook"] = "Data Save Successfully";
                        }
                        return RedirectToAction("DayBook");
                    }
                    else
                    {
                        TempData["DayBook"] = "Please Contact To Admin.";
                        return View(obj);
                    }
                }

            }
            catch (Exception ex)
            {

                TempData["DayBook"] = ex.Message;
                return View(obj);
            }
            return View(obj);
        }

        #region Salary
        public ActionResult EmployeeSalary(HRManagement model)
        {

            #region ddlUser

            List<SelectListItem> ddlUser = new List<SelectListItem>();
            DataSet ds11 = model.GetEmployeeData();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                { ddlUser.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_AdminId"].ToString() }); }
            }

            ViewBag.ddlUser = ddlUser;
            #endregion

            return View(model);
        }
        [HttpPost]

        public ActionResult EmployeeSalary(HRManagement obj, string Search)
        {

            if (!string.IsNullOrEmpty(Search))
            {
                List<HRManagement> lst = new List<HRManagement>();
                List<HRManagement> lst1 = new List<HRManagement>();

                DataSet ds0 = obj.SalaryHeadList();

                if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds0.Tables[0].Rows)
                    {
                        HRManagement obj1 = new HRManagement();
                        obj1.SalaryHeadID = r["PK_SalaryHeadID"].ToString();
                        obj1.SalaryHeadCode = r["SalaryHeadCode"].ToString();
                        obj1.SalaryHeadName = r["SalaryHeadName"].ToString();
                        obj1.HeadNature = r["HeadNature"].ToString();
                        obj1.IsAmtPer = r["IsAmtPer"].ToString();
                        obj1.PaidAmount = r["Amount"].ToString();
                        obj1.Value = r["Value"].ToString();
                        lst.Add(obj1);
                    }
                    obj.lstList = lst;
                }


                if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds0.Tables[1].Rows)
                    {
                        HRManagement obj1 = new HRManagement();
                        obj1.SalaryHeadID = r["PK_SalaryHeadID"].ToString();
                        obj1.SalaryHeadCode = r["SalaryHeadCode"].ToString();
                        obj1.SalaryHeadName = r["SalaryHeadName"].ToString();
                        obj1.HeadNature = r["HeadNature"].ToString();
                        obj1.IsAmtPer = r["IsAmtPer"].ToString();
                        obj1.PaidAmount = r["Amount"].ToString();
                        obj1.Value = r["Value"].ToString();
                        lst1.Add(obj1);
                    }
                    obj.lstList1 = lst1;
                }



                #region ddlUser

                List<SelectListItem> ddlUser = new List<SelectListItem>();
                obj.EmployeeID = null;
                DataSet ds11 = obj.GetEmployeeData();
                if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds11.Tables[0].Rows)
                    { ddlUser.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_AdminId"].ToString() }); }
                }

                ViewBag.ddlUser = ddlUser;
                #endregion
                return View(obj);
            }
            else
            {
                string FormName = "";
                string Controller = "";


                try
                {

                    string noofrows = Request["hdrows"].ToString();
                    string headid = "";
                    string amt = "";

                    DataTable dtEarning = new DataTable();
                    dtEarning.Columns.Add("FK_SalaryHeadID", typeof(string));
                    dtEarning.Columns.Add("Amount", typeof(string));



                    for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                    {
                        headid = Request["ESalHeadId_ " + i].ToString();
                        amt = Request["txtEarAmt " + i].ToString() == "" ? "0" : Request["txtEarAmt " + i].ToString();

                        dtEarning.Rows.Add(headid, amt);
                    }

                    //=================
                    string noofrows1 = Request["hdrows1"].ToString();
                    string headid1 = "";
                    string amt1 = "";


                    DataTable dtDeduction = new DataTable();
                    dtDeduction.Columns.Add("FK_SalaryHeadID", typeof(string));
                    dtDeduction.Columns.Add("Amount", typeof(string));


                    for (int i = 1; i <= int.Parse(noofrows1) - 1; i++)
                    {
                        headid1 = Request["DSalHeadId_ " + i].ToString();
                        amt1 = Request["txtDedAmt " + i].ToString() == "" ? "0" : Request["txtDedAmt " + i].ToString();


                        dtDeduction.Rows.Add(headid1, amt1);
                    }

                    //  obj.EmployeeID = Session["EmpID"].ToString();
                    obj.AddedBy = Session["Pk_AdminId"].ToString();
                    obj.dtQualification = dtEarning;
                    obj.dtWorkExp = dtDeduction;
                    obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                    DataSet ds = obj.SaveEmployeeSalaryDetails();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["EmpSal"] = " Employee Salary Saved successfully !";

                        }
                        else
                        {
                            TempData["EmpSal"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["EmpSal"] = ex.Message;
                }
                FormName = "EmployeeSalary";
                Controller = "Admin";

                return RedirectToAction(FormName, Controller);
            }
        }


        public ActionResult EmployeeSalarySlip(HRManagement model)
        {
            return View(model);
        }

        [HttpPost]

        public ActionResult EmployeeSalarySlip(HRManagement model, string Search)
        {


            List<HRManagement> lst = new List<HRManagement>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds0 = model.EmployeeSalarySlipBy();

            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[0].Rows)
                {
                    HRManagement obj = new HRManagement();
                    obj.PaymentDate = r["PayDate"].ToString();
                    obj.EmployeeID = r["FK_EmpID"].ToString();
                    obj.EmployeeCode = r["EmployeeCode"].ToString();
                    obj.EmployeeName = r["EmployeeName"].ToString();
                    obj.Basic = r["BasicSalary"].ToString();
                    obj.HRA = r["HouseRent"].ToString();
                    obj.MA = r["Medical"].ToString();
                    obj.PA = r["ProfDevAllowance"].ToString();
                    obj.CA = r["Conveyance"].ToString();
                    obj.PF = r["EmpContribution"].ToString();
                    obj.ExtraWork = r["ExtraWork"].ToString();
                    obj.Incentive = r["Incentice"].ToString();
                    obj.OtherPay = r["OtherPay"].ToString();
                    obj.TotalIncome = r["TotalIncome"].ToString();
                    obj.ContributionTosociety = r["ContributionTosociety"].ToString();
                    obj.Advance = r["Advanced"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.Insurance = r["Insurance"].ToString();
                    obj.Other = r["Other"].ToString();
                    obj.TotalDeduction = r["TotalDeduction"].ToString();
                    obj.MonthName = r["MonthName"].ToString();
                    obj.Year = r["Year"].ToString();
                    obj.NetSalary = r["NetSalary"].ToString();
                    lst.Add(obj);
                }
                model.lstList = lst;
            }
            return View(model);

        }

        public ActionResult PrintSalarySlip(string PaymentDate, String EmployeeID)
        {
            HRManagement model = new HRManagement();

            List<HRManagement> lst = new List<HRManagement>();
            List<HRManagement> lst1 = new List<HRManagement>();
            model.EmployeeID = EmployeeID;
            model.PaymentDate = string.IsNullOrEmpty(PaymentDate) ? null : Common.ConvertToSystemDate(PaymentDate, "dd/MM/yyyy");

            DataSet ds0 = model.EmployeeSalarySlipBy();

            if (ds0 != null && ds0.Tables.Count > 0 && ds0.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds0.Tables[0].Rows)
                {
                    model.EmployeeID = ds0.Tables[0].Rows[0]["FK_EmpID"].ToString();
                    model.EmployeeCode = ds0.Tables[0].Rows[0]["EmployeeCode"].ToString();
                    model.EmployeeName = ds0.Tables[0].Rows[0]["EmployeeName"].ToString();
                    model.BankAccountNo = ds0.Tables[0].Rows[0]["AccNo"].ToString();
                    model.BankName = ds0.Tables[0].Rows[0]["BankName"].ToString();
                    model.IFSCCOde = ds0.Tables[0].Rows[0]["IFSCCOde"].ToString();
                    model.PAN = ds0.Tables[0].Rows[0]["PanNo"].ToString();
                    model.DateOfJoining = ds0.Tables[0].Rows[0]["DOJ"].ToString();
                    model.Fk_DepartmentId = ds0.Tables[0].Rows[0]["UserType"].ToString();
                    model.Address = ds0.Tables[0].Rows[0]["Address"].ToString();

                    model.TotalIncome = ds0.Tables[0].Rows[0]["TotalIncome"].ToString();
                    model.TotalDeduction = ds0.Tables[0].Rows[0]["TotalDeduction"].ToString();
                    model.NetSalary = ds0.Tables[0].Rows[0]["NetSalary"].ToString();
                    model.MonthName = ds0.Tables[0].Rows[0]["MonthName"].ToString();
                    model.Year = ds0.Tables[0].Rows[0]["Year"].ToString();
                    model.AmountInWords = ds0.Tables[0].Rows[0]["AmountInWords"].ToString();

                    model.Basic = ds0.Tables[0].Rows[0]["BasicSalary"].ToString();
                    model.HRA = ds0.Tables[0].Rows[0]["HouseRent"].ToString();
                    model.MA = ds0.Tables[0].Rows[0]["Medical"].ToString();
                    model.PA = ds0.Tables[0].Rows[0]["ProfDevAllowance"].ToString();
                    model.CA = ds0.Tables[0].Rows[0]["Conveyance"].ToString();
                    model.PF = ds0.Tables[0].Rows[0]["ProfDevAllowance"].ToString();
                    model.ExtraWork = ds0.Tables[0].Rows[0]["ExtraWork"].ToString();
                    model.Incentive = ds0.Tables[0].Rows[0]["Incentice"].ToString();
                    model.OtherPay = ds0.Tables[0].Rows[0]["OtherPay"].ToString();
                    model.ContributionTosociety = ds0.Tables[0].Rows[0]["ContributionTosociety"].ToString();
                    model.Advance = ds0.Tables[0].Rows[0]["Advanced"].ToString();
                    model.TDS = ds0.Tables[0].Rows[0]["TDS"].ToString();
                    model.Insurance = ds0.Tables[0].Rows[0]["Insurance"].ToString();
                    model.Other = ds0.Tables[0].Rows[0]["Other"].ToString();


                    ViewBag.AddressLine1 = SoftwareDetails.CompanyAddress;
                    ViewBag.CompanyName = SoftwareDetails.CompanyName;
                    ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
                    ViewBag.Pin1 = SoftwareDetails.Pin1;
                    ViewBag.State1 = SoftwareDetails.State1;
                    ViewBag.City1 = SoftwareDetails.City1;
                    ViewBag.ContactNo = SoftwareDetails.ContactNo;

                    ViewBag.Website = SoftwareDetails.Website;
                    ViewBag.EmailID = SoftwareDetails.EmailID;
                }
            }
            return View(model);
        }

        public ActionResult AdvancePayment()
        {
            Plot obj = new Plot();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlpaymentmode = ddlPaymentMode;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            return View();
        }
        [HttpPost]
        public ActionResult AdvancePayment(Wallet obj)
        {

            Plot obj1 = new Plot();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj1.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlpaymentmode = ddlPaymentMode;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            try
            {
                obj.AddedBy = Session["PK_AdminId"].ToString();
                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                DataSet ds = obj.AdvancePayment();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["AdvancePayment"] = "Advance Payment Saved Successfully";

                    }
                    else
                    {
                        TempData["AdvancePayment"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["AdvancePayment"] = ex.Message;
            }
            return RedirectToAction("AdvancePayment");
        }

        public ActionResult AdvancePaymentList()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdvancePaymentList(Wallet obj)
        {
            obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
            obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/MM/yyyy");
            DataSet ds = obj.AdvancePaymentReport();
            obj.advancePayment = ds.Tables[0];
            return View(obj);

        }
        #endregion

        public ActionResult GetEmpLedger()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetEmpLedger(Reports obj)
        {
            DataSet ds = obj.GetEmpLedger();
            obj.lstDetails = ds.Tables[0];
            return View(obj);
        }
        public ActionResult GetAssociateLedger()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetAssociateLedger(Reports obj)
        {
            DataSet ds = obj.GetAssociateLedger();

            obj.lstDetails = ds.Tables[0];
            obj.customerLedger = ds.Tables[1];
            return View(obj);
        }

        public ActionResult GlobalSearch(string text)
        {
            Reports obj = new Reports();
            obj.LoginId = text;
            DataSet ds = obj.GlobalSearch();
            obj.associateDetails = ds.Tables[0];
            obj.bookingData = ds.Tables[1];
            obj.bookingDetails = ds.Tables[2];
            obj.advanaceDetails = ds.Tables[3];
            return View(obj);
        }

        public ActionResult GetBankTransactions()
        {
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            return View();
        }
        [HttpPost]
        public ActionResult GetBankTransactions(Reports obj)
        {
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            obj.Fk_BankId = obj.Fk_BankId == "0" ? null : obj.Fk_BankId;
            obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
            obj.Todate = string.IsNullOrEmpty(obj.Todate) ? null : Common.ConvertToSystemDate(obj.Todate, "dd/MM/yyyy");
            DataSet ds = obj.GetBankTransaction();
            obj.lstDetails = ds.Tables[0];
            ViewBag.totalCredit = ds.Tables[1].Rows[0]["TotalCredit"].ToString();
            ViewBag.totalDebit = ds.Tables[1].Rows[0]["TotalDebit"].ToString();
            ViewBag.totalBalance = ds.Tables[1].Rows[0]["Balance"].ToString();
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
                obj.OldPassword = Crypto.Encrypt(obj.OldPassword);
                obj.NewPassword = Crypto.Encrypt(obj.NewPassword);
                obj.LoginId = Session["LoginId"].ToString();
                DataSet ds = obj.UpdateAdminPassword();
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

        public ActionResult CreateEMI(string PK_BookingId, string noofEMI)
        {
            Plot obj = new Plot();
            obj.PK_BookingId = PK_BookingId;
            obj.NoofEMI = noofEMI;
            obj.AddedBy = Session["PK_AdminId"].ToString();
            DataSet ds = obj.CreateEMI();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BalanceSheet(Reports obj)
        {
            DataSet ds = obj.GetBalanceSheet();
            obj.lstDetails = ds.Tables[0];
            obj.dtDetails = ds.Tables[1];
            return View(obj);
        }



        public ActionResult GetDircetPayout(Reports obj)
        {
            DataSet ds = obj.GetDircetPayout();
            obj.payoutData = ds.Tables[0];
            return View(obj);
        }



        public ActionResult AssociateRegistrationDirect(string UserID)
        {


            TraditionalAssociate model = new TraditionalAssociate();
            try
            {
                if (UserID != null)
                {

                    model.UserID = Crypto.Decrypt(UserID);
                    //model.UserID = UserID;
                    DataSet dsPlotDetails = model.GetList();
                    if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                    {
                        model.Fk_UserId = UserID;
                        model.sponserId = dsPlotDetails.Tables[0].Rows[0]["SponsorId"].ToString();
                        model.sponsorName = dsPlotDetails.Tables[0].Rows[0]["SponsorName"].ToString();
                        model.FirstName = dsPlotDetails.Tables[0].Rows[0]["FirstName"].ToString();
                        model.LastName = dsPlotDetails.Tables[0].Rows[0]["LastName"].ToString();
                        model.DesignationID = dsPlotDetails.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                        model.DesignationName = dsPlotDetails.Tables[0].Rows[0]["DesignationName"].ToString();

                        model.Contact = dsPlotDetails.Tables[0].Rows[0]["Mobile"].ToString();
                        model.Email = dsPlotDetails.Tables[0].Rows[0]["Email"].ToString();
                        model.State = dsPlotDetails.Tables[0].Rows[0]["State"].ToString();
                        model.City = dsPlotDetails.Tables[0].Rows[0]["City"].ToString();
                        model.Address = dsPlotDetails.Tables[0].Rows[0]["Address"].ToString();
                        model.pinCode = dsPlotDetails.Tables[0].Rows[0]["PinCode"].ToString();
                        model.PanNo = dsPlotDetails.Tables[0].Rows[0]["PanNumber"].ToString();

                        model.AdharNumber = dsPlotDetails.Tables[0].Rows[0]["AdharNumber"].ToString();
                        model.BankAccountNo = dsPlotDetails.Tables[0].Rows[0]["MemberAccNo"].ToString();
                        model.BankName = dsPlotDetails.Tables[0].Rows[0]["MemberBankName"].ToString();
                        model.BankBranch = dsPlotDetails.Tables[0].Rows[0]["MemberBranch"].ToString();
                        model.IFSCCode = dsPlotDetails.Tables[0].Rows[0]["IFSCCode"].ToString();
                        model.ProfilePic = dsPlotDetails.Tables[0].Rows[0]["ProfilePic"].ToString();
                        //model.Signature = dsPlotDetails.Tables[0].Rows[0]["Signature"].ToString();
                    }
                }

                else
                {
                    // ViewBag.Disabled = "";

                }


                TraditionalAssociate obj = new TraditionalAssociate();


                #region ddlDesignation

                int desgnationCount = 0;
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                DataSet dsdesignation = obj.GetDesignationList();
                if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in dsdesignation.Tables[1].Rows)
                    {
                        if (desgnationCount == 0)
                        {
                            ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                        }
                        ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                        desgnationCount = desgnationCount + 1;
                    }
                }

                ViewBag.ddlDesignation = ddlDesignation;

                #endregion

                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        [HttpPost]
        public ActionResult AssociateRegistrationDirect(TraditionalAssociate model, string btnSave)
        {

            #region ddlDesignation

            int desgnationCount = 0;
            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            DataSet dsdesignation = model.GetDesignationList();
            if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in dsdesignation.Tables[1].Rows)
                {
                    if (desgnationCount == 0)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                    }
                    ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                    desgnationCount = desgnationCount + 1;
                }
            }

            ViewBag.ddlDesignation = ddlDesignation;

            #endregion
            string FormName = "";
            string Controller = "";
            try
            {
                Random rnd = new Random();
                int ctrPasword = rnd.Next(111111, 999999);
                model.Password = Crypto.Encrypt(ctrPasword.ToString());

                DataSet dsRegistration = new DataSet();
                if (!string.IsNullOrEmpty(btnSave))
                {
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    model.CommissionType = "Direct";
                    dsRegistration = model.AssociateRegistration();
                }
                else
                {
                    model.Fk_UserId = Crypto.Decrypt(model.Fk_UserId);
                    model.UpdatedBy = Session["Pk_AdminId"].ToString();

                    dsRegistration = model.UpdateAssociate();
                }


                if (dsRegistration != null && dsRegistration.Tables.Count > 0)
                {
                    if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (!string.IsNullOrEmpty(btnSave))
                        {
                            Session["DisplayNameConfirm"] = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                            Session["LoginIDConfirm"] = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["PasswordConfirm"] = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                            //Session["PKUserID"] = Crypto.Encrypt(dsRegistration.Tables[0].Rows[0]["PKUserID"].ToString());

                            string name = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                            string id = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                            string pass = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                            string mob = model.Contact;
                            try
                            {
                                string msg = "Dear " + name + " Welcome to VNV INFRA STRCUTURE Family.Your Login Id " + id + " and Password is " + pass + ". Kindly login" + SoftwareDetails.Website + " Thankyou";
                                BLSMS.SendSMSNew(model.Contact, msg);
                            }
                            catch { }
                            try
                            {
                                string msg = "Dear " + name + " Welcome to VNV INFRA STRCUTURE Family.Your Login Id " + id + " and Password is " + pass + ". Kindly login" + SoftwareDetails.Website + " Thankyou";
                                BLSMS.SendSMSNew("9795710000", msg);
                            }
                            catch { }
                        }
                        else
                        {
                            TempData["Registration"] = "Associate Updated Successfully";
                            return RedirectToAction("AssociateRegistrationDirect");
                        }

                    }
                    else
                    {
                        TempData["Registration"] = dsRegistration.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            FormName = "ConfirmRegistrationAssociate";
            Controller = "Admin";

            return RedirectToAction(FormName, Controller);

        }

        public ActionResult BusinessReport(string loginid)
        {

            Reports obj = new Reports();
            if (!string.IsNullOrEmpty(loginid))
            {
                obj.LoginId = loginid;
                obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
                obj.Todate = string.IsNullOrEmpty(obj.Todate) ? null : Common.ConvertToSystemDate(obj.Todate, "dd/MM/yyyy");
                DataSet ds11 = obj.GetBusinessReport();
                obj.lstDetails = ds11.Tables[0];
                return View(obj);
            }
            return View();

        }
        [HttpPost]
        public ActionResult BusinessReport(Reports obj)
        {

            obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
            obj.Todate = string.IsNullOrEmpty(obj.Todate) ? null : Common.ConvertToSystemDate(obj.Todate, "dd/MM/yyyy");
            DataSet ds11 = obj.GetBusinessReport();
            obj.lstDetails = ds11.Tables[0];
            return View(obj);

        }

        public ActionResult PayPayout()
        {
            Plot model = new Plot();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            Reports obj = new Reports();
            DataSet ds = obj.GetPayPayout();
            obj.lstDetails = ds.Tables[0];
            return View(obj);
        }

        public ActionResult PlotCountReport(Reports obj1)
        {
            Plot obj = new Plot();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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
            obj1.SiteID = obj1.SiteID == "0" ? null : obj1.SiteID;
            DataSet ds = obj1.GetPlotCount();
            obj1.lstDetails = ds.Tables[0];

            return View(obj1);

        }

        public ActionResult RecievePayment()
        {
            Plot obj = new Plot();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            return View();
        }
        [HttpPost]
        public ActionResult RecievePayment(SiteManagement siteManagement)
        {
            Plot obj = new Plot();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            try
            {
                siteManagement.TransactionDate = Common.ConvertToSystemDate(siteManagement.TransactionDate, "dd/MM/yyyy");
                siteManagement.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = siteManagement.RecievePaymentOnSite();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["RecievePayment"] = "Payment Recieved Successfully";
                        return RedirectToAction("RecievePayment");
                    }
                    else
                    {
                        TempData["RecievePayment"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(siteManagement);
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["RecievePayment"] = ex.Message;
                return View(siteManagement);
            }
            return RedirectToAction("RecievePayment");
        }

        public ActionResult SiteExpense()
        {
            Plot obj = new Plot();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            return View();
        }
        [HttpPost]
        public ActionResult SiteExpense(SiteManagement siteManagement)
        {
            Plot obj = new Plot();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            try
            {
                siteManagement.TransactionDate = Common.ConvertToSystemDate(siteManagement.TransactionDate, "dd/MM/yyyy");
                siteManagement.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = siteManagement.ExpenseOnSite();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["SiteExpense"] = "Expense Saved Successfully";
                        return RedirectToAction("SiteExpense");
                    }
                    else
                    {
                        TempData["SiteExpense"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(siteManagement);
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["SiteExpense"] = ex.Message;
                return View(siteManagement);
            }
            return RedirectToAction("SiteExpense");
        }


        public ActionResult SiteManagementreport()
        {
            Plot obj = new Plot();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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
            return View();
        }
        [HttpPost]
        public ActionResult SiteManagementreport(Reports model)
        {
            Plot obj = new Plot();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.Todate = string.IsNullOrEmpty(model.Todate) ? null : Common.ConvertToSystemDate(model.Todate, "dd/MM/yyyy");
            DataSet ds = model.GetSiteLedger();
            model.lstDetails = ds.Tables[0];
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.TotalCRAmount = double.Parse(ds.Tables[0].Compute("sum(CRAmount)", "").ToString()).ToString("0.00");
                ViewBag.TotalDRAmount = double.Parse(ds.Tables[0].Compute("sum(DRAmount)", "").ToString()).ToString("0.00");
                ViewBag.TotalBalance = (double.Parse(ViewBag.TotalCRAmount) - double.Parse(ViewBag.TotalDRAmount)).ToString("0.00");
                // ViewBag.TotalDueAmount = double.Parse(ds.Tables[0].Compute("sum(DueAmount)", "").ToString()).ToString("0.00");
            }
            return View(model);
        }

        public ActionResult LevelMemberRegistration(string UserID)
        {
            Customer customer = new Customer();
            #region BindRelation
            List<SelectListItem> ddlRelation = Common.BindRelation();
            ViewBag.ddlRelation = ddlRelation;
            #endregion BindRelation
            if (!string.IsNullOrEmpty(UserID))
            {
                customer.Fk_UserId = Crypto.Decrypt(UserID);
                DataSet dataSet = customer.GetLevelMemeberList();
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        customer.FirstName = dataSet.Tables[0].Rows[0]["Name"].ToString();
                        customer.Relation = dataSet.Tables[0].Rows[0]["Relation"].ToString();
                        customer.FatherName = dataSet.Tables[0].Rows[0]["RalationName"].ToString();
                        customer.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
                        customer.Contact = dataSet.Tables[0].Rows[0]["MobileNo"].ToString();
                        customer.UserID = dataSet.Tables[0].Rows[0]["PK_MemberId"].ToString();
                    }
                }
            }
            return View(customer);
        }
        [HttpPost]
        public ActionResult LevelMemberRegistration(Customer customer, string btnSave)
        {
            #region BindRelation
            List<SelectListItem> ddlRelation = Common.BindRelation();
            ViewBag.ddlRelation = ddlRelation;
            #endregion BindRelation
            Random rnd = new Random();
            int ctrPasword = rnd.Next(111111, 999999);
            customer.Password = Crypto.Encrypt(ctrPasword.ToString());
            DataSet dataSet = new DataSet();

            if (!string.IsNullOrEmpty(btnSave))
            {
                customer.AddedBy = Session["Pk_AdminId"].ToString();

                dataSet = customer.LevelMemberRegistration();
            }
            else
            {
                customer.UserID = (customer.UserID);
                customer.UpdatedBy = Session["Pk_AdminId"].ToString();

                dataSet = customer.UpdateLevelMemberRegistration();
            }
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                if (dataSet.Tables[0].Rows[0][0].ToString() == "1")
                {
                    if (!string.IsNullOrEmpty(btnSave))
                    {
                        TempData["Registration"] = "Level Member Registration Completed";

                    }
                    else
                    {
                        TempData["Registration"] = "Level Member Updated";
                    }
                }
                else
                {
                    TempData["Registration"] = dataSet.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return View(customer);
                }
            }
            return RedirectToAction("LevelMemberRegistration");
        }

        public ActionResult LevelMemberRegistrationList()
        {
            Reports reports = new Reports();
            DataSet dataSet = reports.GetLevelMemeberList();
            reports.userList = dataSet.Tables[0];
            return View(reports);
        }
        public ActionResult DeleteLevelMember(string UserID)
        {
            TraditionalAssociate obj = new TraditionalAssociate();
            obj.UserID = Crypto.Decrypt(UserID);
            obj.AddedBy = Session["PK_AdminId"].ToString();
            DataSet ds = obj.DeleteLevelmember();
            return RedirectToAction("LevelMemberRegistrationList");
        }

        public ActionResult RecievAmount()
        {
            Plot obj = new Plot();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlpaymentmode = ddlPaymentMode;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            return View();
        }
        [HttpPost]
        public ActionResult RecievAmount(Wallet obj)
        {

            Plot obj1 = new Plot();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj1.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlpaymentmode = ddlPaymentMode;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            try
            {
                obj.AddedBy = Session["PK_AdminId"].ToString();
                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                DataSet ds = obj.RecievePaymentForTeamLedger();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["RecieveAmount"] = "Payment Recieve Successfully";

                    }
                    else
                    {
                        TempData["RecieveAmount"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["RecieveAmount"] = ex.Message;
            }
            return RedirectToAction("RecievAmount");
        }

        public ActionResult GetNameForLevelExpense(string LoginId)
        {
            try
            {
                DayBook model = new DayBook();
                model.LoginId = LoginId;


                DataSet dsSponsorName = model.GetNameForLevelExpense();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.sponsorName = dsSponsorName.Tables[0].Rows[0]["Name"].ToString();



                    model.Result = "yes";

                }
                else
                {
                    model.sponsorName = "";
                    model.Result = "no";
                }


                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult DeletedayBook(string id)
        {
            Reports reports = new Reports();
            reports.PK_LedgerID = id;
            reports.AddedBy = Session["PK_AdminId"].ToString();
            DataSet ds = reports.DeleteDayBook();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {

                    //TempData["DeletedayBook"] = "Expenses Saved Successfully";

                }
                else
                {
                    //  TempData["DeletedayBook"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                }

            }
            return RedirectToAction("DayBook");
        }

        public ActionResult Expenses()
        {
            Plot obj = new Plot();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlpaymentmode = ddlPaymentMode;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            return View();
        }
        [HttpPost]
        public ActionResult Expenses(Wallet obj)
        {

            Plot obj1 = new Plot();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj1.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlpaymentmode = ddlPaymentMode;
            #endregion
            #region BindBank

            Master objbank = new Master();
            List<SelectListItem> ddlBank = new List<SelectListItem>();
            ddlBank.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
            DataSet dsBank = objbank.GetBankList();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlBank.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });

                }
            }

            ViewBag.ddlBank = ddlBank;

            #endregion BindBank
            try
            {
                obj.AddedBy = Session["PK_AdminId"].ToString();
                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                DataSet ds = obj.ExpensesForTeamLedger();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["Expenses"] = "Expenses Saved Successfully";

                    }
                    else
                    {
                        TempData["Expenses"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Expenses"] = ex.Message;
            }
            return RedirectToAction("Expenses");
        }

    }
}