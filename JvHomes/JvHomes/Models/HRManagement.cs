using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JvHomes.Models
{
    public class HRManagement : Common
    {
        #region Properties
        public List<SelectListItem> lstListACCHD { get; set; }
        public List<HRManagement> lstQualification { get; set; }
        public List<HRManagement> lstWorkExperience { get; set; }
        public List<HRManagement> lstEarning { get; set; }
        public List<HRManagement> lstDeduction { get; set; }
        public List<HRManagement> lstLeave { get; set; }
        public string EmployeeID { get; set; }
        public string PaymentMode { get; set; }
        public DataTable dtQualification { get; set; }
        public DataTable dtWorkExp { get; set; }
        public List<HRManagement> lstList { get; set; }
        public List<SelectListItem> lstListEmp { get; set; }
        public List<HRManagement> lstList1 { get; set; }
        public List<SelectListItem> ddlDesignation { get; set; }
        public List<SelectListItem> lstListDes { get; set; }
        public string Value { get; set; }
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationID { get; set; }
        public string DesignationName { get; set; }
        public string PaidSalID { get; set; }
        public string EmployeeName { get; set; }
        public string FatherName { get; set; }
        public string DOB { get; set; }
        //public string Gender { get; set; }
        public string CertificateSubmitted { get; set; }
        public string SalaryHeadID { get; set; }
        public string SalaryAmount { get; set; }
        public string SalaryHeadType { get; set; }
        public string SalaryHeadCode { get; set; }
        public string SalaryHeadName { get; set; }
        public string HeadNature { get; set; }
        public string IsAmtPer { get; set; }
        public string EmployeeCode { get; set; }
        public string LeaveID { get; set; }
        public string LeaveName { get; set; }
        public string LeaveLimit { get; set; }
        public string UsedLeave { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string LeaveApplicationID { get; set; }
        public string HolidayID { get; set; }
        public string HolidayName { get; set; }
        public string HolidayDate { get; set; }
        public string Pk_DepartmentId { get; set; }
        public List<HRManagement> listDepartment { get; set; }
        public string Fk_DepartmentId { get; set; }
        public List<HRManagement> listDesignation { get; set; }
        public string Pk_DesignationID { get; set; }
        public string LeaveType { get; set; }
        public string Pk_LeaveTypeId { get; set; }
        public List<HRManagement> listLeaveType { get; set; }
        public string AmountPersent { get; set; }
        public string RemarkSalaryHead { get; set; }
        public string PK_SalaryHeadID { get; set; }
        public List<HRManagement> listSalaryHead { get; set; }
        public string SkillCategoryCode { get; set; }
        public string SkillCategoryName { get; set; }
        public string RemarkSkillCategory { get; set; }
        public string PK_SkillCategoryID { get; set; }
        public List<HRManagement> listSkillCategory { get; set; }
        public string SalaryDate { get; set; }
        public string TDS { get; set; }
        public string MonthName { get; set; }
        public string Year { get; set; }
        public string Basic { get; set; }
        public string HRA { get; set; }
        public string Other { get; set; }
        public string PF { get; set; }
        public string MA { get; set; }
        public string PA { get; set; }
        public string CA { get; set; }
        public string ExtraWork { get; set; }
        public string Incentive { get; set; }
        public string TotalDeduction { get; set; }
        public string OtherPay { get; set; }
        public string TotalIncome { get; set; }
        public string NetSalary { get; set; }
        public string ContributionTosociety { get; set; }
        public string Insurance { get; set; }
        public string Advance { get; set; }
        public string Balance { get; set; }
        public string PaidAmount { get; set; }
        public string PaymentDate { get; set; }
        public string Password { get; set; }
        public string QualificationID { get; set; }
        public string WorkID { get; set; }
        public string EmployeeType { get; set; }
        public string EmployeeGender { get; set; }
        public string Qualification { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string PAN { get; set; }
        public string BankAccountNo { get; set; }
        public string BankName { get; set; }
        public string BankBranchName { get; set; }
        public string IFSCCOde { get; set; }
        public string PerAddress { get; set; }
        public string PerPinCode { get; set; }
        public string PerState { get; set; }
        public string PerCity { get; set; }
        public string LocAddress { get; set; }
        public string LocPinCode { get; set; }
        public string LocState { get; set; }
        public string LocCity { get; set; }
        public string Branch { get; set; }
        public string DateOfJoining { get; set; }
        public string PFNumber { get; set; }
        public string ProfilePic { get; set; }
        public List<HRManagement> lstEmployeeRegister { get; set; }
        public List<HRManagement> lstDesignation { get; set; }
        public string PublishDate { get; set; }
        public string PK_AccountHead { get; set; }
        public string AccountHead { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeDate { get; set; }
        public string Pk_PaidSalId { get; set; }
        public string AttendanceDate { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
         public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string AmountInWords { get;  set; }


        #endregion




        public DataSet SaveEmployeeSalaryDetails()
        {
            SqlParameter[] para =
                            {
                 new SqlParameter("@FK_EmpID",EmployeeID),
                     new SqlParameter("@dtEmployeeEarning",dtQualification),
                         new SqlParameter("@dtEmployeeDeduction",dtWorkExp),
                             new SqlParameter("@AddedBy",AddedBy),
                                     new SqlParameter("@PayDate",PaymentDate),
                                    
                            };
            DataSet ds = Connection.ExecuteQuery("AddEmployeeSalary", para);
            return ds;
        }
        public DataSet SalaryHeadList()
        {
            SqlParameter[] para = {

                   
                    new SqlParameter("@EmployeeId",EmployeeID),

            };
            DataSet ds = Connection.ExecuteQuery("GetDataForSalary", para);
            return ds;
        }
         
        public DataSet SaveEmployeeSalary()
        {
            SqlParameter[] para =
                            {
             new SqlParameter("@SalaryDate", SalaryDate),
             new SqlParameter("@PaymentDate", PaymentDate),
             new SqlParameter("@dtSalary", dtQualification),
             new SqlParameter("@AddedBy", AddedBy),
                            };
            DataSet ds = Connection.ExecuteQuery("PostEmployeeSalary", para);
            return ds;
        }
        public DataSet GetEmployeeByBranch()
        {
            SqlParameter[] para =
                            {
                     new SqlParameter("@FK_BranchID",Branch ),
                       

                            };
            DataSet ds = Connection.ExecuteQuery("TeacherList", para);
                return ds;
        }
        public DataSet PublishSalaryBy()
        {
            SqlParameter[] para = {

                    new SqlParameter("@Date",PaymentDate),

            };
            DataSet ds = Connection.ExecuteQuery("GetDataForPublishSalary", para);
            return ds;
        }
        public DataSet SavePublishSalary()
        {
            SqlParameter[] para = {

                    new SqlParameter("@PublishDate",PublishDate),
                     new SqlParameter("@Fk_EmpId",EmployeeID),
                      new SqlParameter("@AddedBy",AddedBy),

            };
            DataSet ds = Connection.ExecuteQuery("PublishSalaryData", para);
            return ds;
        }
        public DataSet SalaryPaymentBy()
        {
            SqlParameter[] para = {

                    new SqlParameter("@Date",PaymentDate),
                       new SqlParameter("@PK_EmployeeID",EmployeeID),

            };
            DataSet ds = Connection.ExecuteQuery("GetDataForPaidSalary", para);
            return ds;
        }
        public DataSet GetAccountHeadByBranch()
        {
            SqlParameter[] para ={
                                       new SqlParameter ("@Fk_BranchId",Branch),
                                         new SqlParameter ("@Pk_HeadId",PK_AccountHead),
            };
            DataSet ds = Connection.ExecuteQuery("GetAccountHeadByBranch", para);
            return ds;
        }
        
        public DataSet GetPaymentModeList()
        { 
            DataSet ds = Connection.ExecuteQuery("GetPaymentMode" );
            return ds;
        }
        public DataSet SaveSalaryPayment()
        {
            SqlParameter[] para ={
                                       new SqlParameter ("@FK_EmpID",EmployeeID),
                                       new SqlParameter ("@HeadID",AccountHead),
                                       new SqlParameter ("@AddedBy",AddedBy),
                                       new SqlParameter ("@PayDate",PublishDate),
                                       new SqlParameter ("@PaymentMode", PaymentMode),
                                       new SqlParameter ("@TransactionNo",ChequeNo),
                                       new SqlParameter ("@TransactionDate",ChequeDate),
                                     
            };
            DataSet ds = Connection.ExecuteQuery("SavePaidSalary", para);
            return ds;
        }

        public DataSet EmployeeSalarySlipBy()
        {
            SqlParameter[] para ={
                                       new SqlParameter ("@FromDate",FromDate),
                                       new SqlParameter ("@ToDate",ToDate),
                                       new SqlParameter ("@Pk_PaidSalId",Pk_PaidSalId),
                                        new SqlParameter ("@FK_EmpID",EmployeeID),
                                           new SqlParameter ("@PayDate",PaymentDate),
            };
            DataSet ds = Connection.ExecuteQuery("GetDataForSalarySlip", para);
            return ds;
        }


        public DataSet GetEmployeeData()
        {
            SqlParameter[] para = {
            new SqlParameter("@PK_AdminId",EmployeeID),
            new SqlParameter("@Name", EmployeeName),
            new SqlParameter("@LoginId", LoginId),
            };
            DataSet ds = Connection.ExecuteQuery("GetEmployeeDetails", para);
            return ds;
        }

    }

}