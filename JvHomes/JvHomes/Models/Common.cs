using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using SD = System.Drawing;

namespace JvHomes.Models
{
    public class Common
    {
       
        public string AddedBy { get; set; }
        public string Fk_UserId { get; set; }
        public string UpdatedBy { get; set; }
        public string sponserId { get; set; }
        public string sponsorName { get; set; }
        public string pinCode { get; set; }
        public string DisplayName { get;  set; }
        public string Result { get;  set; }
        public string State { get;  set; }
        public string City { get;  set; }
        public static string GenerateRandom()
        {
            Random r = new Random();
            string s = "";
            for (int i = 0; i < 6; i++)
            {
                s = string.Concat(s, r.Next(10).ToString());
            }
            return s;
        }
        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string DateString = "";
            DateTime Dt;
            string[] DatePart = (InputDate).Split(new string[] { "-", @"/" }, StringSplitOptions.None);
            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];
                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Month + "/" + Day + "/" + Year;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }
            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }
        }
        public DataSet BindUserTypeForRegistration()
        {

            DataSet ds = Connection.ExecuteQuery("GetUserTypeForRegistration");

            return ds;

        }
        public static List<SelectListItem> BindCommissionType()
        {
            List<SelectListItem> BindCommissionType = new List<SelectListItem>();
            BindCommissionType.Add(new SelectListItem { Text = "Select", Value = "0" });
            BindCommissionType.Add(new SelectListItem { Text = "Direct", Value = "Direct" });
            BindCommissionType.Add(new SelectListItem { Text = "Level", Value = "Level" });

            return BindCommissionType;
        }
        public static List<SelectListItem> BindGender()
        {
            List<SelectListItem> BindGender = new List<SelectListItem>();
            BindGender.Add(new SelectListItem { Text = "Select", Value = "0" });
            BindGender.Add(new SelectListItem { Text = "Male", Value = "M" });
            BindGender.Add(new SelectListItem { Text = "Female", Value = "F" });

            return BindGender;
        }
        public static List<SelectListItem> BindRelation()
        {
            List<SelectListItem> BindGender = new List<SelectListItem>();
            BindGender.Add(new SelectListItem { Text = "Select", Value = "0" });
            BindGender.Add(new SelectListItem { Text = "S/O", Value = "S/O" });
            BindGender.Add(new SelectListItem { Text = "F/O", Value = "F/O" });
            BindGender.Add(new SelectListItem { Text = "W/O", Value = "W/O" });
            BindGender.Add(new SelectListItem { Text = "D/O", Value = "D/O" });

            return BindGender;
        }
       
        public static List<SelectListItem> BindHeadType()
        {
            List<SelectListItem> BindHeadType = new List<SelectListItem>();
            BindHeadType.Add(new SelectListItem { Text = "Select", Value = "0" });
            BindHeadType.Add(new SelectListItem { Text = "Direct", Value = "Direct" });
            BindHeadType.Add(new SelectListItem { Text = "InDirect", Value = "InDirect" });
            

            return BindHeadType;
        }
        public static List<SelectListItem> BindPlotStatus()
        {
            List<SelectListItem> BindPlotStatus = new List<SelectListItem>();
            BindPlotStatus.Add(new SelectListItem { Text = "Select", Value = "0" });
            BindPlotStatus.Add(new SelectListItem { Text = "Booked", Value = "Booked" });
            BindPlotStatus.Add(new SelectListItem { Text = "Alloted", Value = "Alloted" });
            BindPlotStatus.Add(new SelectListItem { Text = "Registered", Value = "Registered" });

            return BindPlotStatus;
        }
        public static List<SelectListItem> BindPaymentPlan()
        {
            List<SelectListItem> BindPaymentPlan = new List<SelectListItem>();
            BindPaymentPlan.Add(new SelectListItem { Text = "Select", Value = "0" });
            BindPaymentPlan.Add(new SelectListItem { Text = "EMI Plan", Value = "EMI PLAN" });
            BindPaymentPlan.Add(new SelectListItem { Text = "Full Payment Plan", Value = "Full Payment Plan" });

            return BindPaymentPlan;
        }
        public static List<SelectListItem> BindPaymentType()
        {
            List<SelectListItem> BindPaymentType = new List<SelectListItem>();
            BindPaymentType.Add(new SelectListItem { Text = "Select", Value = "0" });
            BindPaymentType.Add(new SelectListItem { Text = "Booking", Value = "Booking" });
            BindPaymentType.Add(new SelectListItem { Text = "Allotment", Value = "Allotment" });
            BindPaymentType.Add(new SelectListItem { Text = "EMI", Value = "EMI" });
            return BindPaymentType;
        }
        public DataSet GetMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", sponserId),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetMemberName", para);

            return ds;
        }
        
       
        public DataSet GetStateCity()
        {
            SqlParameter[] para = { new SqlParameter("@Pincode", pinCode) };
            DataSet ds = Connection.ExecuteQuery("GetStateCity", para);
            return ds;
        }

        public DataSet BindFormMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = Connection.ExecuteQuery("FormMasterManage", para);

            return ds;

        }
        public DataSet GetMonth()
        {
            
            DataSet ds = Connection.ExecuteQuery("GetMonth");

            return ds;

        }
        public DataSet BindFormTypeMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = Connection.ExecuteQuery("FormTypeMasterManage", para);

            return ds;

        }

        public DataSet FormPermissions(string FormName, string AdminId)
        {
            try
            {
                SqlParameter[] para = {
                                          new SqlParameter("@FormName", FormName) ,
                                          new SqlParameter("@AdminId", AdminId)
                                      };

                DataSet ds = Connection.ExecuteQuery("PermissionsOfForm", para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public class SoftwareDetails
        {
            public static string CompanyName = "KDS Associates";
            public static string CompanyAddress = "Medical College Road, Rapti Nagar, Near Arogya Mandir, Infront of S.S. Tower";
            public static string Pin1 = "273004";
            public static string State1 = "Uttar Pradesh";
            public static string City1 = "Gorakhpur";
            public static string ContactNo = "6390486000";
            public static string Website = "www.JvHomes.com";
            public static string EmailID = "kdsassociates8@gmail.com";
        }


    }
   


}