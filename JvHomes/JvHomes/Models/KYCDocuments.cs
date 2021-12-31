using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JvHomes.Models
{
    public class KYCDocuments:UpdateProfile
    {
        public string PKUserID { get; set; }
        public string AdharNumber { get; set; }
        public string AdharImage { get; set; }
        public string AdharStatus { get; set; }
        public string PanNumber { get; set; }
        public string PanImage { get; set; }
        public string PanStatus { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentImage { get; set; }
        public string DocumentStatus { get; set; }
        public string Signature { get; set; }

        public DataSet UploadKYCDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID",PKUserID ) ,
                                      new SqlParameter("@AdharNumber", AdharNumber) ,
                                      new SqlParameter("@AdharImage", AdharImage) ,
                                      new SqlParameter("@PanNumber", PanNumber),
                                      new SqlParameter("@PanImage", PanImage) ,
                                      new SqlParameter("@DocumentNumber", DocumentNumber) ,
                                      new SqlParameter("@DocumentImage", DocumentImage),
                                       new SqlParameter("@Email", Email),
                                      new SqlParameter("@Mobile", Mobile),
                                      new SqlParameter("@FirstName", FirstName),
                                      new SqlParameter("@LastName", LastName),
                                       new SqlParameter("@AccountNo", AccountNo),
                                      new SqlParameter("@BankName", BankName),
                                      new SqlParameter("@BankBranch", BankBranch),
                                      new SqlParameter("@IFSC", IFSC),
                                      new SqlParameter("@Signature", Signature),
                new SqlParameter("@ProfilePic", ProfilePic),
                new SqlParameter("@Address", Address),


                                      new SqlParameter("@Gender", Gender),
                                  };
            DataSet ds = Connection.ExecuteQuery("UploadKYC", para);
            return ds;
        }

        public DataSet GetKYCDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID", PKUserID) };
            DataSet ds = Connection.ExecuteQuery("GetKYCDocuments", para);
            return ds;
        }

    }
}