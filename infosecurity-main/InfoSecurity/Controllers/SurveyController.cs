using InfoSecurity.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using Swashbuckle.Swagger;

namespace InfoSecurity.Controllers
{
    [AuthorizeAttribute]
    [RoutePrefix("api/survey")] 
    public class SurveyController : ApiController
    {
        [Route("GetRegularUsersDetails")]
        
        [HttpGet]
        public IHttpActionResult GetRegularUsersDetails()
        {
            HttpClient httpClientX = new HttpClient();
            httpClientX.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");

            try
            {
                SurveyRepository objEU = new SurveyRepository();
            
                return Ok(objEU.GetRegularUsersDetails());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetRegularUsersDetailsWithInOneHour")]

        [HttpGet]
        public IHttpActionResult GetRegularUsersDetailsWithInOneHour()
        {
            HttpClient httpClientX = new HttpClient();
            httpClientX.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");

            try
            {
                SurveyRepository objEU = new SurveyRepository();

                return Ok(objEU.GetRegularUsersDetailsWithInOneHour());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("GetDontPageQuestions")]
        [HttpGet]
        public IHttpActionResult GetDontPageQuestions()
        {

            try
            {
                SurveyRepository objEU = new SurveyRepository();

                return Ok(objEU.GetDontPageQuestions());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [Route("GetDoPageQuestions")]
        [HttpGet]
        public IHttpActionResult GetDoPageQuestions()
        {

            try
            {
                SurveyRepository objEU = new SurveyRepository();

                return Ok(objEU.GetDoPageQuestions());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("GetEmployeeDetails")]
        [HttpGet]
        public IHttpActionResult GetEmployeeDetails(String username)
        {



            try
            {
                SurveyRepository objEU = new SurveyRepository();



                return Ok(objEU.GetEmployeeDetails(username));



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("saveUserDetails")]
        [HttpGet]
        public IHttpActionResult saveUserDetails(String Emp_id)
        {



            try
            {
                SurveyRepository objEU = new SurveyRepository();



                return Ok(objEU.saveUserDetails(Emp_id));



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("saveUserFeedbackDetails")]
        [HttpGet]
        public IHttpActionResult saveUserFeedbackDetails(String Emp_id)
        {
            try
            {
                SurveyRepository objEU = new SurveyRepository();

                return Ok(objEU.saveUserFeedbackDetails(Emp_id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Route("createAndSavePdf")]
        [HttpGet]
        public IHttpActionResult createAndSavePdf(string empName,string empId)
        {


                Dictionary<string, string> resp = new Dictionary<string, string>();
                resp.Clear();
                resp.Add("status", "0");
                resp.Add("msg", "Something went wrong, while saving pdf..");
                String pathToAcceptance = "";
                string pdfTemplatePath = HttpContext.Current.Server.MapPath("~/Files/merged_v15.pdf");
                PdfStamper stamper = null;
                BaseFont bf = null;
                BaseFont cp = null;

                pathToAcceptance = "D://policySecurity//" + empId + ".pdf";

                string stringDate = DateTime.Now.ToString("dd-MM-yyyy");
                PdfReader reader = null;


                try
                {
                    reader = new PdfReader(pdfTemplatePath);
                }
                catch (IOException e)
                {
                 Console.WriteLine(e.Message);
                 return BadRequest(e.Message);
                }
                try
                {
                    stamper = new PdfStamper(reader, new FileStream(pathToAcceptance, FileMode.Create));
                    bf = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1257, BaseFont.CACHED);
                    cp = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1257, BaseFont.CACHED);
                }
                catch(Exception e)
                {
                Console.WriteLine(e.Message);
                return Json(resp);
                }
                //loop on pages (1-based)
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfContentByte over = stamper.GetOverContent(i);
                    if (i == reader.NumberOfPages)
                    {
                        over.BeginText();
                        over.SetFontAndSize(bf, 12.0F);
                        over.SetTextMatrix(327.0F, 185.0F);

                        over.ShowText(": " + empName);
                        over.EndText();

                        over.BeginText();
                        over.SetFontAndSize(bf, 12.0F);
                        over.SetTextMatrix(327.0F, 165.0F);

                        over.ShowText(": " + empId);
                        over.EndText();

                        over.BeginText();
                        over.SetFontAndSize(bf, 12.0F);
                        over.SetTextMatrix(327.0F, 145.0F);
                        over.ShowText(": " + stringDate);
                        over.EndText();
                    }

                    over.BeginText();
                    over.SetFontAndSize(cp, 10.0F);
                    over.SetTextMatrix(40.0F, 20.0F);

                    over.ShowText("© Infogain Corporation All Rights Reserved( page " + i + " of " + reader.NumberOfPages + ")");
                    over.EndText();
                }

                try
                {
                    stamper.Close();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    return Json(resp);
                }

                resp.Clear();
                resp.Add("status", "1");
                resp.Add("msg", "File is successfully saved..");
                return Ok(resp);
        }

                    
        

    }
}
