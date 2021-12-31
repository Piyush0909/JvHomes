using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JvHomes.Models
{
    public class UpdateProfile : Common
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string AccountNo { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string IFSC { get; set; }
        public string ProfilePic { get; set; }
        public string LoginId { get; set; }
        public string Gender { get; set; }
         public string Address { get; set; }

        public DataSet UpdateAssociateProfile()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Email", Email),
                                      new SqlParameter("@Mobile", Mobile),
                                      new SqlParameter("@FirstName", FirstName),
                                      new SqlParameter("@LastName", LastName),
                                       new SqlParameter("@AccountNo", AccountNo),
                                      new SqlParameter("@BankName", BankName),
                                      new SqlParameter("@BankBranch", BankBranch),
                                      new SqlParameter("@IFSC", IFSC),
                                      new SqlParameter("@ProfilePic", ProfilePic),
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Gender", Gender),
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateProfile", para);

            return ds;
        }

        public DataSet GetProfile()
        {
            SqlParameter[] para = {

                                   
                                      new SqlParameter("@LoginId", LoginId),
                                     
                                  };
            DataSet ds = Connection.ExecuteQuery("GetAssociateList", para);

            return ds;
        }
        public DataSet GetCustomerList()
        {
            SqlParameter[] para = {


                                      new SqlParameter("@LoginId", LoginId),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetCustomerList", para);

            return ds;
        }
    }
}