
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
    public class PermissionController : AdminBaseController
    {
        //
        // GET: /Permission/

        public ActionResult SetPermission(Permisssions model)
        {

            DataSet ds1 = new DataSet();

            #region ddlformtype
            Common obj = new Common();
            List<SelectListItem> ddlformtype = new List<SelectListItem>();
            ds1 = obj.BindFormTypeMaster();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                { ddlformtype.Add(new SelectListItem { Text = r["FormType"].ToString(), Value = r["PK_FormTypeId"].ToString() }); }
            }

            ViewBag.ddlformtype = ddlformtype;

            #endregion
            #region ddluser

            List<SelectListItem> ddluser = new List<SelectListItem>();
            EmployeeRegistrations emp = new EmployeeRegistrations();
            ds1 = emp.GetEmployeeData();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (r["UserType"].ToString()!="Admin")
                    {
                        ddluser.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_AdminId"].ToString() });
                    }
                      
                }
            }
            else
            {
                ddluser.Add(new SelectListItem { Text = "Select User", Value = "0" });
            }

            ViewBag.ddluser = ddluser;

            #endregion

            return View();
        }
        [HttpPost]

        public ActionResult SetPermission(Permisssions obj, string GetDetails)
        {
            if (!string.IsNullOrEmpty(GetDetails))
            {
                Permisssions model = new Permisssions();
                List<Permisssions> lst = new List<Permisssions>();
                DataSet ds = obj.GetFormPermission();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Permisssions ob = new Permisssions();
                        ob.FormName = dr["FormName"].ToString();
                        ob.IsSelectValue = Convert.ToBoolean(dr["FormDelete"].ToString());
                        if (ob.IsSelectValue == false)
                        {
                            ob.SelectedValue = "";
                        }
                        else
                        {
                            ob.SelectedValue = "checked";
                        }
                        ob.IsSaveValue = Convert.ToBoolean(dr["FormSave"].ToString());
                        ob.IsUpdateValue = Convert.ToBoolean(dr["FormUpdate"].ToString());
                        ob.IsDeleteValue = Convert.ToBoolean(dr["FormDelete"].ToString());
                        ob.Fk_FormId = dr["PK_FormId"].ToString();
                        ob.Fk_FormTypeId = dr["pk_formtypeid"].ToString();
                        ob.Fk_UserId = dr["Fk_UserId"].ToString();
                        lst.Add(ob);
                    }
                    model.lstpermission = lst;
                }
                DataSet ds1 = new DataSet();

                #region ddlformtype
                Common obj1 = new Common();
                List<SelectListItem> ddlformtype = new List<SelectListItem>();
                ds1 = obj1.BindFormTypeMaster();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    { ddlformtype.Add(new SelectListItem { Text = r["FormType"].ToString(), Value = r["PK_FormTypeId"].ToString() }); }
                }

                ViewBag.ddlformtype = ddlformtype;

                #endregion
                #region ddluser

                List<SelectListItem> ddluser = new List<SelectListItem>();
                EmployeeRegistrations emp = new EmployeeRegistrations();
                ds1 = emp.GetEmployeeData();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    { ddluser.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_AdminId"].ToString() }); }
                }

                ViewBag.ddluser = ddluser;

                #endregion
                return View(model);
            }
            else
            {
                DataSet ds1 = new DataSet();
                #region ddlformtype
                Common obj1 = new Common();
                List<SelectListItem> ddlformtype = new List<SelectListItem>();
                ds1 = obj1.BindFormTypeMaster();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    { ddlformtype.Add(new SelectListItem { Text = r["FormType"].ToString(), Value = r["PK_FormTypeId"].ToString() }); }
                }

                ViewBag.ddlformtype = ddlformtype;

                #endregion
                #region ddluser

                List<SelectListItem> ddluser = new List<SelectListItem>();
                EmployeeRegistrations emp = new EmployeeRegistrations();
                ds1 = emp.GetEmployeeData();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    { ddluser.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_AdminId"].ToString() }); }
                }

                ViewBag.ddluser = ddluser;

                #endregion
                string hdrows = Request["hdRows"].ToString();
                string chkSave = "";
                string chkupdate = "";
                string chkdelete = "";
                string chkselect = "";
                string hdfformtypeid = "";
                string hdfformid = "";
                string hdfloginid = "";
                DataTable dtpermission = new DataTable();

                dtpermission.Columns.Add("Fk_FormTypeId");
                dtpermission.Columns.Add("Fk_FormId");
                dtpermission.Columns.Add("IsSave");
                dtpermission.Columns.Add("IsUpdate");
                dtpermission.Columns.Add("IsDelete");
                dtpermission.Columns.Add("IsSelect");
                for (int i = 1; i < int.Parse(hdrows); i++)
                {

                    try
                    {
                        chkselect = Request["chkSelect_ " + i].ToString();
                    }
                    catch { chkselect = "0"; }
                    hdfformtypeid = Request["hdFormtypeId_ " + i].ToString();
                    hdfformid = Request["hdFormId_ " + i].ToString();
                    hdfloginid = Request["hdLoginid_ " + i].ToString();

                    dtpermission.Rows.Add(hdfformtypeid, hdfformid, "0", "0", "0", chkselect == "on" ? "1" : "0");

                }

                obj.UserTypeFormPermisssion = dtpermission;
                obj.CreatedBy = Session["Pk_AdminId"].ToString();
                obj.Fk_UserId = hdfloginid;
                obj.Fk_FormTypeId = hdfformtypeid;
                DataSet ds = obj.SavePermisssion();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Permission"] = "Permission set successfully.";
                    }
                    else
                    {
                        TempData["Permission"] = ds.Tables[0].Rows[0]["Remark"].ToString();
                    }
                }

                return View(obj);
            }

        }

        #region Registration

        public ActionResult EmployeeRegistration(string PK_UserID)
        {
            #region ddlUserType
            Common obj1 = new Common();
            List<SelectListItem> ddlUserType = new List<SelectListItem>();
            DataSet ds11 = obj1.BindUserTypeForRegistration();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                { ddlUserType.Add(new SelectListItem { Text = r["UserType"].ToString(), Value = r["PK_UserTypeId"].ToString() }); }
            }

            ViewBag.ddlUserType = ddlUserType;
            #endregion
            
            EmployeeRegistrations obj = new EmployeeRegistrations();
            if (PK_UserID != null)
            {

                try
                {
                    obj.PK_UserID = PK_UserID;
                    DataSet ds = obj.GetEmployeeData();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {


                        obj.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                        obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        obj.Mobile = ds.Tables[0].Rows[0]["Contact"].ToString();
                        obj.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        obj.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                        obj.EducationQualififcation = ds.Tables[0].Rows[0]["EducationQualifiacation"].ToString();
                        obj.PK_UserID = ds.Tables[0].Rows[0]["PK_AdminId"].ToString();
                        obj.DOJ = ds.Tables[0].Rows[0]["DOJ"].ToString();
                        obj.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                        obj.ContactPerson = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
                        obj.ContactNo = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                        obj.BloodGroup = ds.Tables[0].Rows[0]["BloodGroup"].ToString();
                        obj.DocumentType = ds.Tables[0].Rows[0]["DocumentType"].ToString();
                        obj.DocumentNo = ds.Tables[0].Rows[0]["DocumentNo"].ToString();
                        obj.ExpiryDate = ds.Tables[0].Rows[0]["ExpiryDate"].ToString();
                        obj.ProfilePic = ds.Tables[0].Rows[0]["UserImage"].ToString();
                        obj.UserType = ds.Tables[0].Rows[0]["UserType"].ToString();
                        obj.Fk_UserTypeId = ds.Tables[0].Rows[0]["UserType"].ToString();
                        obj.Fk_BranchId = ds.Tables[0].Rows[0]["Fk_BranchId"].ToString();


                    }
                }
                catch (Exception ex)
                {
                    TempData["Category"] = ex.Message;
                }

            }
            else
            {
                obj.LoginId ="KDCE"+ Common.GenerateRandom();
            }


            return View(obj);
        }
        public ActionResult EmployeeDetails()
        {

            List<EmployeeRegistrations> lst = new List<EmployeeRegistrations>();
            EmployeeRegistrations emp = new EmployeeRegistrations();
            DataSet ds = emp.GetEmployeeData();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    EmployeeRegistrations Objload = new EmployeeRegistrations();
                    Objload.Name = dr["Name"].ToString();
                    Objload.LoginId = dr["LoginId"].ToString();
                    Objload.Mobile = dr["Contact"].ToString();
                    Objload.Email = dr["Email"].ToString();
                    Objload.Password = dr["Password"].ToString();
                    Objload.EducationQualififcation = dr["EducationQualifiacation"].ToString();
                    Objload.PK_UserID = dr["PK_AdminId"].ToString();
                    Objload.DOJ = dr["DOJ"].ToString();
                    Objload.DOB = dr["DOB"].ToString();
                    Objload.ContactPerson = dr["ContactPerson"].ToString();
                    Objload.ContactNo = dr["ContactNo"].ToString();
                    Objload.BloodGroup = dr["BloodGroup"].ToString();
                    Objload.DocumentType = dr["DocumentType"].ToString();
                    Objload.DocumentNo = dr["DocumentNo"].ToString();
                    Objload.ExpiryDate = dr["ExpiryDate"].ToString();

                    lst.Add(Objload);
                }
                emp.lstemp = lst;
            }
            return View(emp);
        }
        [HttpPost]
       
        public ActionResult EmployeeRegistration(HttpPostedFileBase fileProfilePicture, EmployeeRegistrations objregi, HttpPostedFileBase flpdocument,string Save)
        {
            if(!string.IsNullOrEmpty(Save))
            {
                #region ddlUserType
                Common obj = new Common();
                List<SelectListItem> ddlUserType = new List<SelectListItem>();
                DataSet ds11 = obj.BindUserTypeForRegistration();
                if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds11.Tables[0].Rows)
                    { ddlUserType.Add(new SelectListItem { Text = r["UserType"].ToString(), Value = r["PK_UserTypeId"].ToString() }); }
                }

                ViewBag.ddlUserType = ddlUserType;
                #endregion

                try
                {

                    objregi.DOB = string.IsNullOrEmpty(objregi.DOB) ? null : Common.ConvertToSystemDate(objregi.DOB, "dd/MM/yyyy");
                    objregi.DOJ = string.IsNullOrEmpty(objregi.DOJ) ? null : Common.ConvertToSystemDate(objregi.DOJ, "dd/MM/yyyy");
                    objregi.ExpiryDate = string.IsNullOrEmpty(objregi.ExpiryDate) ? null : Common.ConvertToSystemDate(objregi.ExpiryDate, "dd/MM/yyyy");
                    objregi.CreatedBy = Session["Pk_AdminId"].ToString();
                    string pass = Common.GenerateRandom();
                    objregi.Password = Crypto.Encrypt(pass);
                    if (fileProfilePicture != null)
                    {
                        objregi.ProfilePic = "/UserImage/" + Guid.NewGuid() + Path.GetExtension(fileProfilePicture.FileName);
                        fileProfilePicture.SaveAs(Path.Combine(Server.MapPath(objregi.ProfilePic)));
                    }
                    if (flpdocument != null)
                    {
                        objregi.DocumentImage = "/UserImage/" + Guid.NewGuid() + Path.GetExtension(flpdocument.FileName);
                        flpdocument.SaveAs(Path.Combine(Server.MapPath(objregi.DocumentImage)));
                    }
                    DataSet ds = objregi.SaveEmpoyeeData();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                        {
                            TempData["EmployeeRegistration"] = "Employee Registration successfully";
                        }
                        else
                        {
                            TempData["EmployeeRegistration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["EmployeeRegistration"] = ex.Message;
                }
                return RedirectToAction("EmployeeRegistration");
            }
            else
            {
                #region ddlUserType
                Common objcomm = new Common();
                List<SelectListItem> ddlUserType = new List<SelectListItem>();
                DataSet ds11 = objcomm.BindUserTypeForRegistration();
                if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds11.Tables[0].Rows)
                    { ddlUserType.Add(new SelectListItem { Text = r["UserType"].ToString(), Value = r["PK_UserTypeId"].ToString() }); }
                }

                ViewBag.ddlUserType = ddlUserType;
                #endregion


                try
                {
                    objregi.DOB = string.IsNullOrEmpty(objregi.DOB) ? null : Common.ConvertToSystemDate(objregi.DOB, "dd/MM/yyyy");
                    objregi.DOJ = string.IsNullOrEmpty(objregi.DOJ) ? null : Common.ConvertToSystemDate(objregi.DOJ, "dd/MM/yyyy");
                    objregi.ExpiryDate = string.IsNullOrEmpty(objregi.ExpiryDate) ? null : Common.ConvertToSystemDate(objregi.ExpiryDate, "dd/MM/yyyy");
                    objregi.CreatedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = objregi.UpdateEmployee();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {

                            TempData["EmployeeRegistration"] = "Employee is Successfully Updated!";
                        }
                        else
                        {

                            TempData["EmployeeRegistration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            return View(objregi);
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["EmployeeRegistration"] = ex.Message;
                    return View(objregi);
                }
                return RedirectToAction("EmployeeDetails");
            }
           
        }
      
        public ActionResult DeleteEmployee(string PK_UserID)
        {

            try
            {
                EmployeeRegistrations model = new EmployeeRegistrations();
                model.PK_UserID = PK_UserID;
                model.CreatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteEmployee();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Employee"] = "Employee is Successfully Deleted";
                    }
                    else
                    {
                        TempData["Employee"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Employee"] = ex.Message;
            }
            return RedirectToAction("EmployeeDetails");
        }

        #endregion Registration

    }
}
