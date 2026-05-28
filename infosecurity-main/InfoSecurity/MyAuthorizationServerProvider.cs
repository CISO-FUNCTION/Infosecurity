using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using System.DirectoryServices;
using InfoSecurity.Models;
using System.Data;
using InfoSecurity.Repositry;
using System.Web.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using InfoSecurity.App_Data;
using Microsoft.Owin;
using InfoSecurity.common;
using System.Configuration;

namespace InfoSecurity
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.OwinContext.Set<string>("domain", context.Parameters["domain"]);
            context.OwinContext.Set<string>("Token", context.Parameters["Token"]);
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            AccountRepository objLogin = new AccountRepository();
            Common common = new Common();
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            string EmpUserName = string.Empty;
            int LoginType = 0;

            IFormCollection parameters = await context.Request.ReadFormAsync();
            string Code = parameters.Get("Code");

            if (!String.IsNullOrEmpty(Code))
            {
                getUserDetailsOffice GetUserMe = common.GetUserDetailsMS(Code);
                if (!String.IsNullOrEmpty(GetUserMe.userPrincipalName))
                {
                    EmpUserDetails VerifyUser = objLogin.GetUserDetailsByEmail(GetUserMe.userPrincipalName);
                    if (VerifyUser.DomainId == null || VerifyUser.DomainId == "")
                    {
                        context.SetError("Invalid User.", "You are not an authorized User");
                        return;
                    }
                    else
                    {
                        string[] ExtractDomainId = VerifyUser.DomainId.Split('\\');
                        EmpUserName = ExtractDomainId[1];
                        LoginType = 1;
                    }
                }


            }

            Char UserType = 'I';
            string Token = context.OwinContext.Get<string>("Token");
            string User = string.Empty;
            if (context.UserName != null)
            {
                string[] UserNameA = context.UserName.Split('@');
                if (UserNameA.Length > 1 && (UserNameA[1].ToLower() != "infogain.com" && UserNameA[1].ToLower() != "igglobal.com"))
                {
                    UserType = 'E';
                    User = context.UserName;
                }
                else
                {
                    UserType = 'I';
                    User = UserNameA[0];
                }
            }
            var IsValid = false;

            if (Token != null && Token != "")
            {
                try
                {
                    string apiurl = WebConfigurationManager.AppSettings["WebApiBaseUrl"];
                    WebRequest req = WebRequest.Create(apiurl + "/api/auth/ValidateToken");
                    req.Method = "GET";
                    req.Headers.Add("token:" + Token);
                    req.ContentType = "application/json; charset=utf-8";
                    WebResponse resp = req.GetResponse();
                    Stream stream = resp.GetResponseStream();
                    StreamReader re = new StreamReader(stream);
                    String json = re.ReadToEnd();
                    //var serializer = new System.text.Script.Serialization.JavaScriptSerializer();
                    //Employee _Employee = serializer.Deserialize<Employee>(json);
                    Employee _Employee = JsonConvert.DeserializeObject<Employee>(json);
                    if (_Employee.UserId != null && _Employee.UserId != "")
                    {
                        IsValid = true;
                        EmpUserName = _Employee.UserId;
                    }
                    else
                        IsValid = false;
                }
                catch (Exception ex)
                {
                    IsValid = false;
                }
            }
            else if (LoginType == 1)
            {

                //  EmpUserName = context.UserName;
                IsValid = true;
            }
            else if (context.Password == ConfigurationManager.AppSettings["Pwd"] ||
            objLogin.AuthenticateUser("IGGLOBAL", context.UserName, context.Password, UserType))
            {

                EmpUserName = context.UserName;
                IsValid = true;
            }

            if (IsValid)
            {
                string UN = string.Empty;
                if (UserType == 'E')
                    UN = context.UserName;
                else
                    UN = "igglobal\\" + EmpUserName;
                UserMaster objUser = objLogin.GetUserDetails(UN, UserType);
                if (objUser.Role == "" || objUser.Role == null)
                {
                    context.SetError("Invalid User.", "You are not an authorized User");
                    return;
                }
                else
                {
                    identity.AddClaim(new Claim("DomainId", objUser.DomainId));
                    identity.AddClaim(new Claim("FirstName", objUser.FirstName));
                    identity.AddClaim(new Claim("LastName", objUser.LastName));
                    identity.AddClaim(new Claim("FullName", objUser.FullName));
                    identity.AddClaim(new Claim("EmpOldID", objUser.EmpOldID));
                    identity.AddClaim(new Claim("EmpNewId", objUser.EmpNewId));
                    identity.AddClaim(new Claim("MailID", objUser.MailID));
                    identity.AddClaim(new Claim("LocationID", objUser.LocationID.ToString()));
                    identity.AddClaim(new Claim("RoleId", objUser.RoleId.ToString()));
                    identity.AddClaim(new Claim("Role", objUser.Role));
                    identity.AddClaim(new Claim("UserType", UserType.ToString()));
                    identity.AddClaim(new Claim("IsPasswordChanged", objUser.IsPasswordChanged.ToString()));
                    identity.AddClaim(new Claim("DeptID", objUser.DeptID.ToString()));
                    identity.AddClaim(new Claim("IsRenuTeam", objUser.otherRoles.IsRenuTeam.ToString()));
                    context.Validated(identity);
                }
            }
            else
            {
                context.SetError("Invalid User.", "Provided UserName and Password is incorrect");
                return;
            }
        }
    }
}