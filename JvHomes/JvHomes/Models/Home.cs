using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JvHomes.Models
{
    public class Home
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string Result { get; set; }
        public string MenuId { get; set; }
        public string Pk_AdminId { get; set; }
        public string UserType { get; set; }
        public string MenuName { get; set; }
        public string Url { get; set; }
        public string Icons { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuId { get; set; }
        public List<Home> lstMenu { get; set; }
        public List<Home> lstsubmenu { get; set; }
        public DataTable lstDetails { get; set; }

        public DataSet loadHeaderMenu()
        {
            SqlParameter[] para = {
                                new SqlParameter("@PK_AdminId", Pk_AdminId),
                                 new SqlParameter("@UserType", UserType)
            };

            DataSet ds = Connection.ExecuteQuery("GetMenuUserWise", para);
            return ds;
        }
        public DataSet GetEmployeeData()
        {
            SqlParameter[] para = {

            new SqlParameter("@LoginId", LoginId),
            };
            DataSet ds = Connection.ExecuteQuery("GetEmployeeDetails", para);
            return ds;
        }
        public DataSet GetTermsCondition()
        {
           
            DataSet ds = Connection.ExecuteQuery("GetTermsCondition");
            return ds;
        }
        public DataSet GetPassword()
        {
            SqlParameter[] para = {

            new SqlParameter("@LoginId", LoginId),
            };
            DataSet ds = Connection.ExecuteQuery("GetPassword", para);
            return ds;
        }
        public static Home GetMenus(string Pk_AdminId, string UserType)
        {
            Home model = new Home();
            List<Home> lstmenu = new List<Home>();
            List<Home> lstsubmenu = new List<Home>();

            model.Pk_AdminId = Pk_AdminId;
            model.UserType = UserType;
            DataSet dsHeader = model.loadHeaderMenu();
            if (dsHeader != null && dsHeader.Tables.Count > 0)
            {
                if (dsHeader.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsHeader.Tables[0].Rows)
                    {
                        Home obj = new Home();

                        obj.MenuId = r["PK_FormTypeId"].ToString();
                        obj.MenuName = r["FormType"].ToString();
                        obj.Url = r["Url"].ToString();
                        obj.Icons = r["Icon"].ToString();
                        lstmenu.Add(obj);
                    }

                    model.lstMenu = lstmenu;
                    foreach (DataRow r in dsHeader.Tables[1].Rows)
                    {
                        Home obj = new Home();
                        obj.Url = r["Url"].ToString();
                        obj.MenuId = r["FK_FormTypeId"].ToString();
                        obj.SubMenuId = r["PK_FormId"].ToString();
                        obj.SubMenuName = r["FormName"].ToString();
                        lstsubmenu.Add(obj);
                    }

                    model.lstsubmenu = lstsubmenu;

                }


            }
            return model;

        }
    }
}