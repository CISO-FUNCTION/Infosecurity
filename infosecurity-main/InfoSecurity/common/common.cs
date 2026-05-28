using InfoSecurity.App_Data;
using InfoSecurity.Models;
using InfoSecurity.Repositry;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.Exchange.WebServices.Data;
using Newtonsoft.Json;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using TimeZoneConverter;
using TimeZoneNames;

namespace InfoSecurity.common
{
    public class Common
    {
        public string ConvertDMYtoYMD(string date)
        {
            string[] strDate = date.Split('/');
            return strDate[2] + "/" + strDate[0] + "/" + strDate[1];
        }

        public DateTime ConvertUTCToTZ(DateTime date, string timezone)
        {
            var tzm = TZConvert.IanaToWindows(timezone);
            TimeZoneInfo serverZone = TimeZoneInfo.FindSystemTimeZoneById(tzm);
            DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(date, serverZone);
            return currentDateTime;
        }

        public string getAccessToen(string code)
        {

            HttpResponseMessage servicerequest = null;
            try
            {
                var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", "8e9af8aa-9f68-47eb-b2e5-dabb5803dfbe"),
              //  new KeyValuePair<string, string>("scope", "user.read%20mail.read"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", "http://localhost:4200/login"),
                //new KeyValuePair<string, string>("redirect_uri", "https://infosecurity.infogain.com/login"),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("client_secret", "F4p8Q~OGIgGCizYJgL8OxJKot.p2fDRlLWW4HcLx"),
                // ...
            });
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpClient httpClient = new HttpClient();
                string _url = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                servicerequest = httpClient.PostAsync(_url, requestContent).Result;
                string response = servicerequest.Content.ReadAsStringAsync().Result;
                servicerequest.EnsureSuccessStatusCode();
                getOffice365AccessToken jsonObj = JsonConvert.DeserializeObject<getOffice365AccessToken>(response);
                return jsonObj.access_token;
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex, "Common", "getAccessToken");
                return "";
            }

        }

        public getUserDetailsOffice GetUserDetailsMS(string code)
        {
            HttpResponseMessage servicerequest = null;
            try
            {
                string token = getAccessToen(code);
                //  string jsonBodySend = JsonConvert.SerializeObject(InviteSendBody);
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("authorization", "Bearer " + token + "");
                //  var contentSend = new StringContent(jsonBodySend);
                //   contentSend.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                string _urlSend = "https://graph.microsoft.com/v1.0/me";
                servicerequest = httpClient.GetAsync(_urlSend).Result;
                string response = servicerequest.Content.ReadAsStringAsync().Result;
                servicerequest.EnsureSuccessStatusCode();
                getUserDetailsOffice jsonObj = JsonConvert.DeserializeObject<getUserDetailsOffice>(response);
                return jsonObj;
            }
            catch (HttpRequestException ex)
            {
                ExceptionLogging.SendExcepToDB(ex, "Common", "GETMEOFFICE");
                //   getUserDetailsOffice jsonObj = JsonConvert.DeserializeObject<getUserDetailsOffice>(response);

                return null;
            }
        }

        public string Encrypt(string inputText, string encryptionkey)
        {
            //string encryptionkey = "SAUW193BX628TD57";
            byte[] keybytes = Encoding.ASCII.GetBytes(encryptionkey.Length.ToString());
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] plainText = Encoding.Unicode.GetBytes(inputText);
            PasswordDeriveBytes pwdbytes = new PasswordDeriveBytes(encryptionkey, keybytes);
            using (ICryptoTransform encryptrans = rijndaelCipher.CreateEncryptor(pwdbytes.GetBytes(32), pwdbytes.GetBytes(16)))
            {
                using (MemoryStream mstrm = new MemoryStream())
                {
                    using (CryptoStream cryptstm = new CryptoStream(mstrm, encryptrans, CryptoStreamMode.Write))
                    {
                        cryptstm.Write(plainText, 0, plainText.Length);
                        cryptstm.Close();
                        return Convert.ToBase64String(mstrm.ToArray());
                    }
                }
            }
        }

      

    }
}