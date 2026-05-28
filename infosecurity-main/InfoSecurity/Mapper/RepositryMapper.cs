using InfoSecurity.App_Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Mvc;
using InfoSecurity.Models;

namespace ASTAPI.Mapper
{
    public static class RepositryMapper
    {
        public static object getMap<T>(DataSet ds)
        {
            object objlist = new object();
            string sectionName = "RepositryMapper";
            try
            {
                if (typeof(T) == typeof(UserMaster))
                {
                    UserMaster ud = new UserMaster();
                    if (ds != null && ds.Tables.Count == 1)
                    {
                        ud.DomainId = Convert.ToString(ds.Tables[0].Rows[0]["emp_domainid"]);
                        ud.FirstName = Convert.ToString(ds.Tables[0].Rows[0]["emp_firstname"]);
                        ud.LastName = Convert.ToString(ds.Tables[0].Rows[0]["emp_lastName"]);
                        ud.FullName = Convert.ToString(ds.Tables[0].Rows[0]["FullName"]);
                        ud.EmpOldID = Convert.ToString(ds.Tables[0].Rows[0]["emp_staffid"]);
                        ud.EmpNewId = Convert.ToString(ds.Tables[0].Rows[0]["Emp_newid"]);
                        ud.MailID = Convert.ToString(ds.Tables[0].Rows[0]["emp_mailid"]);
                        ud.LocationID = Convert.ToInt16(ds.Tables[0].Rows[0]["LocationId"]);
                        ud.LocationName = Convert.ToString(ds.Tables[0].Rows[0]["LocationName"]);
                        ud.RoleId = Convert.ToInt16(ds.Tables[0].Rows[0]["RoleId"]);
                        ud.Role = Convert.ToString(ds.Tables[0].Rows[0]["Role"]);
                        ud.UserType = Convert.ToChar(ds.Tables[0].Rows[0]["UserType"]);
                        ud.IsPasswordChanged = Convert.ToChar(ds.Tables[0].Rows[0]["IsPasswordChanged"]);
                        ud.otherRoles.IsDH = Convert.ToChar(ds.Tables[0].Rows[0]["IsDH"]);
                        ud.otherRoles.IsAO = Convert.ToChar(ds.Tables[0].Rows[0]["IsAO"]);
                        ud.otherRoles.IsApprover = Convert.ToChar(ds.Tables[0].Rows[0]["IsApprover"]);
                        ud.otherRoles.IsBUHead = Convert.ToChar(ds.Tables[0].Rows[0]["IsBUHead"]);
                        ud.otherRoles.IsPM = Convert.ToChar(ds.Tables[0].Rows[0]["IsPM"]);
                        ud.otherRoles.IsHiringManager = Convert.ToChar(ds.Tables[0].Rows[0]["IsHiringManager"]);
                        ud.otherRoles.IsDelegationAdmin = Convert.ToChar(ds.Tables[0].Rows[0]["IsDelegationAdmin"]);
                        ud.otherRoles.IsInterviewer = Convert.ToChar(ds.Tables[0].Rows[0]["IsInterviewer"]);
                        ud.otherRoles.IsTagLeadApprover = Convert.ToChar(ds.Tables[0].Rows[0]["IsTagLeadApprover"]);
                        ud.otherRoles.IsWMG = Convert.ToChar(ds.Tables[0].Rows[0]["IsWMG"]);
                        ud.otherRoles.IsTAG = Convert.ToChar(ds.Tables[0].Rows[0]["IsTAG"]);
                        ud.otherRoles.IsGDL = Convert.ToChar(ds.Tables[0].Rows[0]["IsGDL"]);
                        ud.otherRoles.IsFinance = Convert.ToChar(ds.Tables[0].Rows[0]["IsFinance"]);
                        ud.otherRoles.IsIJP = Convert.ToChar(ds.Tables[0].Rows[0]["IsIJP"]);
                        ud.otherRoles.IsRM = Convert.ToChar(ds.Tables[0].Rows[0]["IsRM"]);
                        ud.otherRoles.IsUSHrRole = Convert.ToChar(ds.Tables[0].Rows[0]["IsUSHrRole"]);

                        ud.otherRoles.IsFinance = Convert.ToChar(ds.Tables[0].Rows[0]["IsFinance"]);
                        ud.DeptID = Convert.ToInt16(ds.Tables[0].Rows[0]["DeptID"]);
                        ud.otherRoles.IsRenuTeam = Convert.ToChar(ds.Tables[0].Rows[0]["IsRenuTeam"]);
                        //ud.otherRoles.IsRenuTeamAdmin = Convert.ToChar(ds.Tables[0].Rows[0]["IsRenuTeamAdmin"]);
                        ud.Photo = ds.Tables[0].Rows[0]["Photo"].ToString();
                    }
                    objlist = ud;
                }

                else if (typeof(T) == typeof(EmpUserDetails))
                {
                    EmpUserDetails ud = new EmpUserDetails();
                    if (ds != null && ds.Tables.Count == 1)
                    {
                        ud.DomainId = Convert.ToString(ds.Tables[0].Rows[0]["emp_domainid"]);
                        ud.FirstName = Convert.ToString(ds.Tables[0].Rows[0]["emp_firstname"]);
                        ud.LastName = Convert.ToString(ds.Tables[0].Rows[0]["emp_lastName"]);
                        ud.FullName = Convert.ToString(ds.Tables[0].Rows[0]["FullName"]);
                        ud.EmpOldID = Convert.ToString(ds.Tables[0].Rows[0]["emp_staffid"]);
                        ud.EmpNewId = Convert.ToString(ds.Tables[0].Rows[0]["Emp_newid"]);
                        ud.MailID = Convert.ToString(ds.Tables[0].Rows[0]["emp_mailid"]);
                        ud.LocationID = Convert.ToInt16(ds.Tables[0].Rows[0]["LocationId"]);
                        ud.LocationName = Convert.ToString(ds.Tables[0].Rows[0]["LocationName"]);
                    }
                    objlist = ud;
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex, sectionName, "getMap");
            }

            return objlist;

        }
    }
}