using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JvHomes.Models
{
    public class Dashboard:Common
    {
        public string Fk_UserId { get; set; }
        public DataTable investmentDetails { get;  set; }
        public DataTable associateDetails { get;  set; }
        public DataTable documentList { get; set; }
        public DataTable kycDetails { get;  set; }
        public DataTable bageDetails { get;  set; }

        public DataSet GetAssociateDashboard()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                     
                                  };
            DataSet ds = Connection.ExecuteQuery("GetDashBoardDetailsForAssociate", para);

            return ds;
        }
        public DataSet GetCustomerDashBoard()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetCustomerDashBoard", para);

            return ds;
        }
        public DataSet GetDirectAssociateDashboard()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetDirectAssociateDashboard", para);

            return ds;
        }
        public DataSet GetAdminDashBoard()
        {
            
            DataSet ds = Connection.ExecuteQuery("GetAdminDashBoard");

            return ds;
        }
    }
}