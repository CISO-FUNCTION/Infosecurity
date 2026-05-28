using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfoSecurity.Models
{
    public class UserMaster
    {
        public UserMaster()
        {
            otherRoles = new OtherRoles();
        }
        public string DomainId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EmpOldID { get; set; }
        public string EmpNewId { get; set; }
        public string MailID { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public char UserType { get; set; }
        public char IsPasswordChanged { get; set; }
        public OtherRoles otherRoles { get; set; }
        public string Photo { get; set; }
        public int DeptID { get; set; }
    }

    public class OtherRoles
    {
        public char IsDH { get; set; }
        public char IsAO { get; set; }
        public char IsApprover { get; set; }
        public char IsBUHead { get; set; }
        public char IsPM { get; set; }
        public char IsHiringManager { get; set; }
        public char IsDelegationAdmin { get; set; }
        public char IsInterviewer { get; set; }
        public char IsTagLeadApprover { get; set; }
        public char IsWMG { get; set; }
        public char IsTAG { get; set; }
        public char IsGDL { get; set; }
        public char IsFinance { get; set; }
        public char IsIJP { get; set; }
        public char IsRenuTeam { get; set; }
        //public char IsRenuTeamAdmin { get; set; }
        public char IsRM { get; set; }
         
        public char IsUSHrRole { get; set; }
    }
    public class projFilter
    {
        public string AccountID { get; set; }
        public string searchText { get; set; }
    }

    public class Employee
    {
        public string UserId { get; set; }
        public string Domain { get; set; }
        public string EMP_TYPE { get; set; }
        public string ProfileImageName { get; set; }

    }
}