using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JvHomes.Models
{
    public class ChangePassword:Common
    {
        public string LoginId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public DataSet UpdatePassword()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@OldPassword", OldPassword),
                                       new SqlParameter("@NewPassword", NewPassword),
            };
            DataSet ds = Connection.ExecuteQuery("ChangePassword", para);
            return ds;
        }
        public DataSet UpdateAdminPassword()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@OldPassword", OldPassword),
                                       new SqlParameter("@NewPassword", NewPassword),
            };
            DataSet ds = Connection.ExecuteQuery("UpdateAdminPassword", para);
            return ds;
        }
    }
}