using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Web;

namespace InfoSecurity.App_Data
{
    public static class ExceptionLogging
    {
        static SqlConnection con;
        private static void connecttion()
        {
            string constr = ConfigurationManager.ConnectionStrings["ATSConnection"].ToString();
            con = new SqlConnection(constr);
            con.Open();
        }
        public static void SendExcepToDB(Exception exdb,string sectionName,string methodName)
        {

            try
            { 
            connecttion();
            SqlCommand com = new SqlCommand("ExceptionLogging", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@sectionName", sectionName);
            com.Parameters.AddWithValue("@methodName", methodName);
            com.Parameters.AddWithValue("@exceptionMsg", exdb.Message.ToString());
            com.Parameters.AddWithValue("@exceptionType", exdb.GetType().Name.ToString());
            com.Parameters.AddWithValue("@exceptionSource", "Application");
            com.Parameters.AddWithValue("@exceptionDetails", exdb.StackTrace.ToString());
            com.Parameters.AddWithValue("@createdOn", DateTime.Now);
            com.Parameters.AddWithValue("@createdBy", "");
            com.ExecuteNonQuery();
            con.Close();

            }
            catch(Exception ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry("Unable to create the log using ExceptionLogging class. Exact error is :  " + ex.Message.ToString() + " Stack Trace: " + ex.StackTrace.ToString() , EventLogEntryType.Error, 101, 1);
                }
            }
        }
    }
}
