using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfoSecurity.Models
{
    public class AuthModel
    {
    }

    public class getOffice365AccessToken
    {
        public string access_token { get; set; }
        public string id_token { get; set; }
    }

    public class getUserDetailsOffice
    {
        public string displayName { get; set; }
        public string givenName { get; set; }
        public string jobTitle { get; set; }
        public string mail { get; set; }
        public string surname { get; set; }
        public string userPrincipalName { get; set; }
    }

    public class EmpUserDetails
    {
        public string DomainId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EmpOldID { get; set; }
        public string EmpNewId { get; set; }
        public string MailID { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
    }
}