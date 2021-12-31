﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JvHomes.Models
{
    public class Customer : Common
    {
        #region Properties  

        public string Fk_UserId { get; set; }
        public string FatherName { get; set; }
        public string Relation { get; set; }
        public string AssociateID { get; set; }
        public string JoiningFromDate { get; set; }
        public string JoiningToDate { get; set; }
        public string AssociateName { get; set; }
        public string isBlocked { get; set; }
        public string CustomerLoginID { get; set; }
        public string AssociateLoginID { get; set; }
        public string CustomerName { get; set; }
        public string FK_SponsorId { get; set; }

        public string LoginID { get; set; }
        public string Password { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
      
        public string BranchID { get; set; }
        public string Address { get; set; }
        public string PanNo { get; set; }
        public string Pin { get; set; }
        public string BranchName { get; set; }
        public List<Customer> ListCust { get; set; }

        #endregion

        public DataSet GetAssociateList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID) };
            DataSet ds = Connection.ExecuteQuery("AssociateListTraditional", para);
            return ds;
        }

        #region CustomerRegistration

        public DataSet CustomerRegistration()

        {
            SqlParameter[] para = { 
                                  new SqlParameter("@SponsorID", UserID) ,
                                  new SqlParameter("@RoleID", 3) ,
                                  new SqlParameter("@FirstName", FirstName) ,
                                  new SqlParameter("@LastName", LastName) ,
                                  new SqlParameter("@Contact", Contact) ,
                                  new SqlParameter("@Email", Email) ,
                                new SqlParameter("@Relation", Relation) ,
                                  new SqlParameter("@Address", Address) ,
                                  new SqlParameter("@PanNo", PanNo) ,
                                  new SqlParameter("@AddedBy", AddedBy) ,
                                  new SqlParameter("@Password", Password) ,
                                  new SqlParameter("@FatherName", FatherName) ,
                                  };
            DataSet ds = Connection.ExecuteQuery("CustomerRegistration", para);
            return ds;
        }

        public DataSet LevelMemberRegistration()

        {
            SqlParameter[] para = {
                                
                                  new SqlParameter("@Name", FirstName) ,
                                 new SqlParameter("@Relation", Relation) ,
                                 new SqlParameter("@RelationName", FatherName) ,
                                    new SqlParameter("@Address", Address) ,
                                  new SqlParameter("@MobileNo", Contact) ,
                                  new SqlParameter("@AddedBy", AddedBy) ,
                                  new SqlParameter("@Password", Password) ,
                                  
                                  };
            DataSet ds = Connection.ExecuteQuery("LevelMemberRegistration", para);
            return ds;
        }
        public DataSet GetLevelMemeberList()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_UserId", Fk_UserId),

            };
            DataSet ds = Connection.ExecuteQuery("GetLevelMemeberList", para);
            return ds;
        }
        public DataSet UpdateLevelMemberRegistration()

        {
            SqlParameter[] para = {

                                  new SqlParameter("@Name", FirstName) ,
                                 new SqlParameter("@Relation", Relation) ,
                                 new SqlParameter("@RelationName", FatherName) ,
                                    new SqlParameter("@Address", Address) ,
                                  new SqlParameter("@MobileNo", Contact) ,
                                  new SqlParameter("@AddedBy", UpdatedBy) ,
                                  new SqlParameter("@UserId", UserID) ,

                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateLevelMemberRegistration", para);
            return ds;
        }
        public DataSet GetList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", UserID),
                                  new SqlParameter("@CustomerLoginID", CustomerLoginID),
                                  new SqlParameter("@CustomerName", CustomerName),
                                  new SqlParameter("@AssociateLoginID", AssociateLoginID),
                                  new SqlParameter("@AssociateName", AssociateName),
                                  new SqlParameter("@FromDate", JoiningFromDate),
                                  new SqlParameter("@ToDate", JoiningToDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("SelectCustomer", para);
            return ds;
        }
        public DataSet UpdateCustomer()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", UserID) ,
                                 
                                  new SqlParameter("@FirstName", FirstName) ,
                                  new SqlParameter("@LastName", LastName) ,
                                  new SqlParameter("@Mobile", Contact) ,
                                  new SqlParameter("@Email", Email) ,
                                 
                                  new SqlParameter("@Address", Address) ,
                                  new SqlParameter("@PanNumber", PanNo) ,
                                  new SqlParameter("@UpdatedBy", AddedBy)  ,
                                   new SqlParameter("@FatherName", FatherName) ,
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateCustomerRegistrationDetails", para);
            return ds;
        }

        public DataSet DeleteCustomer()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@PK_UserId", UserID) ,
                                      new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteCustomerRegistration", para);
            return ds;
        }
        public DataSet GetSponsorName()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID) };
            DataSet ds = Connection.ExecuteQuery("GetSponsorForCustomerRegistraton", para);
            return ds;
        }

        #endregion

        public DataSet BlockUser()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID),
                                      new SqlParameter("@FK_UserID", Fk_UserId),
                                   new SqlParameter("@BlockedBy", UpdatedBy)};

            DataSet ds = Connection.ExecuteQuery("BlockAssociate", para);
            return ds;
        }

        public DataSet UnblockUser()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID),
                                      new SqlParameter("@FK_UserID", Fk_UserId),
                                   new SqlParameter("@BlockedBy", UpdatedBy)};
            DataSet ds = Connection.ExecuteQuery("UnblockAssociate", para);
            return ds;
        }



        public string JoiningDate { get; set; }

        public string EncryptKey { get; set; }
    }
}