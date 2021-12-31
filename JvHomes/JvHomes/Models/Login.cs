using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JvHomes.Models
{
    public class Login
    {
        public List<Login> lstMenu { get; set; }
        public List<Login> lstsubmenu { get; set; }

        public string loginId { get; set; }
        public string password { get; set; }
        public string Pk_AdminId { get;  set; }
        public string UserType { get;  set; }
        public string MenuId { get;  set; }
        public string SubMenuId { get;  set; }
        public string SubMenuName { get;  set; }
        public string MenuName { get;  set; }
        public string Url { get;  set; }
        public string Icons { get;  set; }

        public DataSet CheckLogin()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@loginId", loginId),
                                      new SqlParameter("@password", password)
                                     
                                  };
            DataSet ds = Connection.ExecuteQuery("Login", para);

            return ds;
        }
        public DataSet loadHeaderMenu()
        {
            SqlParameter[] para = {
                                new SqlParameter("@PK_AdminId", Pk_AdminId),
                                 new SqlParameter("@UserType", UserType)
            };

            DataSet ds = Connection.ExecuteQuery("GetMenuUserWise", para);
            return ds;
        }
        public static Login GetMenus(string Pk_AdminId, string UserType)
        {
            Login model = new Login();
            List<Login> lstmenu = new List<Login>();
            List<Login> lstsubmenu = new List<Login>();

            model.Pk_AdminId = Pk_AdminId;
            model.UserType = UserType;
            DataSet dsHeader = model.loadHeaderMenu();
            if (dsHeader != null && dsHeader.Tables.Count > 0)
            {
                if (dsHeader.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsHeader.Tables[0].Rows)
                    {
                        Login obj = new Login();

                        obj.MenuId = r["PK_FormTypeId"].ToString();
                        obj.MenuName = r["FormType"].ToString();
                        obj.Url = r["Url"].ToString();
                        obj.Icons = r["Icon"].ToString();
                        lstmenu.Add(obj);
                    }

                    model.lstMenu = lstmenu;
                    foreach (DataRow r in dsHeader.Tables[1].Rows)
                    {
                        Login obj = new Login();
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