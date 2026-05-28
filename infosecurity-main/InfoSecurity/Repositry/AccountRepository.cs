using ASTAPI.Mapper;
using InfoSecurity.App_Data;
using InfoSecurity.common;
using InfoSecurity.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;

namespace InfoSecurity.Repositry
{
    public class AccountRepository : Connection
    {
        SqlCommand cmdObj;
        string sectionName = "AccountRepository";
        DataUtility du;

        public AccountRepository()
        {
            du = new DataUtility();
        }
        //  [AuthorizeAttribute]
        public UserMaster GetUserDetails(string DomainID, char UserType)
        {
            UserMaster ud = new UserMaster();
            try
            {
                OpeneConnection();
                string _sql = "GetUserDetails";
                cmdObj = new SqlCommand(_sql, ConCampus);
                cmdObj.CommandType = CommandType.StoredProcedure;
                cmdObj.Parameters
                .Add(new SqlParameter("@DomainID", SqlDbType.NVarChar))
                .Value = DomainID;
                cmdObj.Parameters
               .Add(new SqlParameter("@UserType", SqlDbType.Char))
               .Value = UserType;
                DataSet ds = du.GetDataSetWithProc(cmdObj);
                ud = (UserMaster)RepositryMapper.getMap<UserMaster>(ds);
                CloseConnection();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex, sectionName, "GetUserDetails");
            }
            return ud;
        }

        public EmpUserDetails GetUserDetailsByEmail(string Email)
        {
            EmpUserDetails ud = new EmpUserDetails();
            try
            {
                OpeneConnection();
                string _sql = "GetUserDetailsByEmail";
                cmdObj = new SqlCommand(_sql, ConCampus);
                cmdObj.CommandType = CommandType.StoredProcedure;
                cmdObj.Parameters
               .Add(new SqlParameter("@EmailID", SqlDbType.VarChar))
               .Value = Email;
                DataSet ds = du.GetDataSetWithProc(cmdObj);
                ud = (EmpUserDetails)RepositryMapper.getMap<EmpUserDetails>(ds);
                CloseConnection();
            }
            catch (Exception ex)     
            {
                ExceptionLogging.SendExcepToDB(ex, sectionName, "GetUserDetails");
            }
            return ud;
        }

        public bool AuthenticateUser(string domain, string username, string password, char UserType)
        {
           
            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("Username & Password must be passed");
            }
            bool authenticated = false;
            int IsAuthorized = 0;
            if (UserType == 'I')
            {
                using (var context = new PrincipalContext(ContextType.Domain, domain, domain + "\\" + username, password))
                {
                    authenticated = context.ValidateCredentials(username, password);
                }
            }
            else
            {
                authenticated = ValidateExternalUser(username, password);
            }

            IsAuthorized = authenticated == true ? 1 : 0;
            AddEmpLoginDetails(username, UserType, IsAuthorized);
            return authenticated;
        }
        private bool ValidateExternalUser(string UserName, string Password)
        {
            Common common = new Common();
            DataSet salt = GetSalt(UserName);
            if (salt != null && salt.Tables.Count == 1 && salt.Tables[0].Rows.Count > 0)
            {
                string encPass = common.Encrypt(Password, salt.Tables[0].Rows[0]["salt"].ToString());
                if (salt.Tables[0].Rows[0]["Password"].ToString() == encPass)
                {
                    return true;
                }
                return false;
            }
            else { return false; }
        }

        public int ChangePassword(string UserId, string NewPassword, string salt, string pwdTxt)
        {
            int result = 0;
            try
            {
                OpeneConnection();
                string _sql = "ChangePassword";
                cmdObj = new SqlCommand(_sql, ConCampus);
                cmdObj.CommandType = CommandType.StoredProcedure;
                cmdObj.Parameters
                .Add(new SqlParameter("@UserId", SqlDbType.VarChar))
                .Value = UserId;
                cmdObj.Parameters
                .Add(new SqlParameter("@NewPassword", SqlDbType.NVarChar))
                .Value = NewPassword;
                cmdObj.Parameters
                .Add(new SqlParameter("@pwdTxt", SqlDbType.NVarChar))
                .Value = pwdTxt;
                cmdObj.Parameters
                .Add(new SqlParameter("@salt", SqlDbType.Char))
                .Value = salt;
                cmdObj.Parameters
                .Add(new SqlParameter("@ChangedBy", SqlDbType.VarChar))
                .Value = UserId;
                cmdObj.Parameters
                .Add(new SqlParameter("@result", SqlDbType.Int))
                .Direction = ParameterDirection.Output;
                du.ExecuteSqlProcedure(cmdObj);
                result = Convert.ToInt32(cmdObj.Parameters["@result"].Value);
                CloseConnection();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex, sectionName, "ChangePassword");
            }
            return result;
        }

        private DataSet GetSalt(string UserName)
        {
            DataSet ds = null;
            try
            {
                OpeneConnection();
                string _sql = "getSaltForExternalUser";
                cmdObj = new SqlCommand(_sql, ConCampus);
                cmdObj.CommandType = CommandType.StoredProcedure;
                cmdObj.Parameters
                 .Add(new SqlParameter("@UserName", SqlDbType.NVarChar))
                 .Value = UserName;
                ds = du.GetDataSetWithProc(cmdObj);
                CloseConnection();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex, sectionName, "GetSalt");
            }
            return ds;
        }

        public int AddEmpLoginDetails(string UserName, char UserType, int IsAuthorized)
        {
            int result = 0;
            try
            {
                OpeneConnection();
                string _sql = "addempLoginDetails";
                cmdObj = new SqlCommand(_sql, ConCampus);
                cmdObj.CommandType = CommandType.StoredProcedure;
                cmdObj.Parameters
                .Add(new SqlParameter("@UserName", SqlDbType.VarChar))
                .Value = UserName;
                cmdObj.Parameters
                .Add(new SqlParameter("@Usertype", SqlDbType.NVarChar))
                .Value = UserType;
                cmdObj.Parameters
                .Add(new SqlParameter("@IsAuthorized", SqlDbType.Int))
                .Value = IsAuthorized;
                cmdObj.Parameters
                .Add(new SqlParameter("@result", SqlDbType.Int))
                .Direction = ParameterDirection.Output;
                du.ExecuteSqlProcedure(cmdObj);
                result = Convert.ToInt32(cmdObj.Parameters["@result"].Value);
                CloseConnection();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex, sectionName, "addempLoginDetails");
                result = -1;
            }
            return result;
        }
    }
}