using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JvHomes.Models
{
    public class Registration : Common
    {

        public string leg { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailId { get; set; }
        public string mobileNo { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string panCard { get; set; }

        public DataSet SaveRegistration()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@SponsorId", sponserId),
                                      new SqlParameter("@Email", emailId),
                                      new SqlParameter("@MobileNo", mobileNo),
                                      new SqlParameter("@FirstName", firstName),
                                      new SqlParameter("@LastName", lastName),
                                       new SqlParameter("@password", password),
                                      new SqlParameter("@Gender", gender),
                                      new SqlParameter("@PanCard", panCard),
                                      new SqlParameter("@Address", address),
                                      new SqlParameter("@PinCode", pinCode),
                                      new SqlParameter("@Leg", leg),
                                  };
            DataSet ds = Connection.ExecuteQuery("Registration", para);

            return ds;
        }

    }

}