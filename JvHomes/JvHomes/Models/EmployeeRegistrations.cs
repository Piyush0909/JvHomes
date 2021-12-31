using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JvHomes.Models
{
    public class EmployeeRegistrations
    {
        [Required]
        [Display(Name = "User Type")]
        public string UserType { get; set; }

        [Required]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Required]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Display(Name = "DOB")]
        public string DOB { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Father's Name")]
        public string FathersName { get; set; }

        [Display(Name = "EducationQualification")]
        public string EducationQualififcation { get; set; }
        public string DOJ { get; set; }

        public string Fk_UserTypeId { get; set; }
        public string Fk_BranchId { get; set; }
        public string CreatedBy { get; set; }
        public string Message { get; set; }

        public string PK_UserID { get; set; }
        public string Task { get; set; }

        [NotMapped]
        public List<EmployeeRegistrations> lstemp { get; set; }
        public string LoginId { get; set; }
       
        public string Password { get; set; }
        public string ContactPerson { get; set; }
        public string ExpiryDate { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentType { get; set; }
        public string DocumentImage { get; set; }
        public string ContactNo { get; set; }
        public string BloodGroup { get; set; }
        public string ProfilePic { get; set; }
        public string Adharno { get; set; }
        public string PanNo { get; set; }
        public string AccNo { get; set; }
        public string BankName { get; set; }
        public string IFSCCOde { get; set; }
        public DataSet SaveEmpoyeeData()
        {
            SqlParameter[] para = {
            new SqlParameter("@Name", Name),
            new SqlParameter("@Mobile", Mobile),
            new SqlParameter("@Email", Email),
            new SqlParameter("@Address", Address),
            new SqlParameter("@DOJ", DOJ),
            new SqlParameter("@Qualification", EducationQualififcation),
            new SqlParameter("@DOB", DOB),
            new SqlParameter("@Fk_UserTypeId", Fk_UserTypeId),
            new SqlParameter("@ContactPerson", ContactPerson),
              new SqlParameter("@ContactNo", ContactNo),
                new SqlParameter("@BloodGroup", BloodGroup),
                  new SqlParameter("@DocumentType", DocumentType),
                    new SqlParameter("@DocumentNo", DocumentNo),
                      new SqlParameter("@ExpiryDate", ExpiryDate),
            new SqlParameter("@CreatedBy", CreatedBy),
            new SqlParameter("@PanNo", PanNo),
            new SqlParameter("@Adharno", Adharno),
            new SqlParameter("@ProfilePic", ProfilePic),
             new SqlParameter("@BankName", BankName),
              new SqlParameter("@IFSCCOde", IFSCCOde),
               new SqlParameter("@AccNo", AccNo),
                new SqlParameter("@DocumentImage", DocumentImage),
                 new SqlParameter("@LoginId", LoginId),
                 new SqlParameter("@Password",Password),
                  new SqlParameter("@Fk_BranchId", Fk_BranchId),
            };
            DataSet ds = Connection.ExecuteQuery("EmployeeRegistration", para);
            return ds;
        }
        public DataSet GetEmployeeData()
        {
            SqlParameter[] para = {
            new SqlParameter("@PK_AdminId", PK_UserID),
            new SqlParameter("@Name", Name),
            new SqlParameter("@LoginId", LoginId),
            };
            DataSet ds = Connection.ExecuteQuery("GetEmployeeDetails", para);
            return ds;
        }
        public DataSet DeleteEmployee()
        {
            SqlParameter[] para = {

            new SqlParameter("@EmpID", PK_UserID),
            new SqlParameter("@DeletedBy", CreatedBy),
            };
            DataSet ds = Connection.ExecuteQuery("DeleteEmployee", para);
            return ds;
        }

        public DataSet UpdateEmployee()
        {
            SqlParameter[] para = {
            new SqlParameter("@FirstName", Name),
            new SqlParameter("@ContactNo", Mobile),
            new SqlParameter("@EmailId", Email),
            new SqlParameter("@Address", Address),
            new SqlParameter("@Qualification", EducationQualififcation),
            new SqlParameter("@UpdatedBy", CreatedBy),
             new SqlParameter("@PKid", PK_UserID),
              new SqlParameter("@DOJ", DOJ),
          
            new SqlParameter("@DOB", DOB),
            new SqlParameter("@Fk_UserTypeId", Fk_UserTypeId),
            new SqlParameter("@ContactPerson", ContactPerson),
              new SqlParameter("@ContactPersonNo", ContactNo),
                new SqlParameter("@BloodGroup", BloodGroup),
                  new SqlParameter("@DocumentType", DocumentType),
                    new SqlParameter("@DocumentNo", DocumentNo),
                      new SqlParameter("@PanNo", PanNo),
            new SqlParameter("@Adharno", Adharno),
            new SqlParameter("@ProfilePic", ProfilePic),
                      new SqlParameter("@ExpiryDate", ExpiryDate),
                      new SqlParameter("@BankName", BankName),
              new SqlParameter("@IFSCCOde", IFSCCOde),
               new SqlParameter("@AccNo", AccNo),
               new SqlParameter("@Fk_BranchId", Fk_BranchId),
            };
            DataSet ds = Connection.ExecuteQuery("UpdateEmployee", para);
            return ds;
        }
        
        
    }
}