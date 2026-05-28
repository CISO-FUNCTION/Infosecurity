using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;



namespace InfoSecurity.Repository
{
    public class SurveyRepository
    {

        public DataSet GetRegularUsersDetails()
        {
             DataSet result = null;

            //string email = null;
            SqlConnection con = null;
            string connectionStrings = ConfigurationManager.ConnectionStrings["Survey"].ConnectionString;
            try
            {
              con   = new SqlConnection(connectionStrings);
                con.Open();
                string sqlQuery = "InfoSecurity_GetRegularUsers";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, con);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                //  sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@emp_id", email);
                result=new DataSet();
               
                sqlDataAdapter.Fill(result);
                result.Tables[0].TableName = "data";
                /* string sql = "select top 1 * from Employee";
                 SqlCommand cmdn= new SqlCommand(sql, con);
                SqlDataReader read =cmdn.ExecuteReader();
                 while (read.Read())
                 {
                     email = (string)read["email"];
                 }*/
                // Console.ReadLine();

            }
            catch (Exception e)
            {
            Console.WriteLine (e.Message);
            }
            finally
            {
             con.Close();
            }
            return result;
        }

        public DataSet GetRegularUsersDetailsWithInOneHour()
        {
            DataSet result = null;

            //string email = null;
            SqlConnection con = null;
            string connectionStrings = ConfigurationManager.ConnectionStrings["Survey"].ConnectionString;
            try
            {
                con = new SqlConnection(connectionStrings);
                con.Open();
                string sqlQuery = "infoSecu_GetActiveUsersWithInOneHour";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, con);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                //  sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@emp_id", email);
                result = new DataSet();

                sqlDataAdapter.Fill(result);
                result.Tables[0].TableName = "data";
                /* string sql = "select top 1 * from Employee";
                 SqlCommand cmdn= new SqlCommand(sql, con);
                SqlDataReader read =cmdn.ExecuteReader();
                 while (read.Read())
                 {
                     email = (string)read["email"];
                 }*/
                // Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public DataSet GetDontPageQuestions()
        {
            DataSet result = null;

            //string email = null;
            SqlConnection con = null;
            string connectionStrings = ConfigurationManager.ConnectionStrings["Survey"].ConnectionString;
            try
            {
                con = new SqlConnection(connectionStrings);
                con.Open();
                string sqlQuery = "InfoSecurity_GetDONT_Page";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, con);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                //  sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@emp_id", email);
                result = new DataSet();

                sqlDataAdapter.Fill(result);
                result.Tables[0].TableName = "data";


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }



        public DataSet GetDoPageQuestions()
        {
            DataSet result = null;

            //string email = null;
            SqlConnection con = null;
            string connectionStrings = ConfigurationManager.ConnectionStrings["Survey"].ConnectionString;
            try
            {
                con = new SqlConnection(connectionStrings);
                con.Open();
                string sqlQuery = "InfoSecurity_GetDO_Questions";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, con);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                //  sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@emp_id", email);
                result = new DataSet();

                sqlDataAdapter.Fill(result);
                result.Tables[0].TableName = "data";


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public DataTable GetEmployeeDetails(string username)
        {
            DataTable result = null;

           // DataRow datarow = null;

            //string username = null;
            SqlConnection con = null;
            string connectionStrings = ConfigurationManager.ConnectionStrings["Survey"].ConnectionString;
            try
            {
                con = new SqlConnection(connectionStrings);
                con.Open();
                string sqlQuery = "Get_UserDetails";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, con);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@username", username);
                result = new DataTable();



                sqlDataAdapter.Fill(result);
                





            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public int saveUserDetails(string Emp_id)
        {


            DataTable result = null;
            int rows = 0;
            // DataRow datarow = null;

            //string username = null;
            SqlConnection con = null;
            string connectionStrings = ConfigurationManager.ConnectionStrings["Survey"].ConnectionString;
            try
            {
                con = new SqlConnection(connectionStrings);
                con.Open();
                string sqlQuery = "Survey_Save_User_Visit_Count";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, con);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@EMP_ID", Emp_id);
                 result = new DataTable();
                    sqlDataAdapter.Fill(result);
                rows= (int)result.Rows[0]["ROWS"];

}
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            return rows;
        }


        public int saveUserFeedbackDetails(string Emp_id)
        {


            DataTable result = null;
            int status = 0;
            // DataRow datarow = null;

            //string username = null;
            SqlConnection con = null;
            string connectionStrings = ConfigurationManager.ConnectionStrings["Survey"].ConnectionString;
            try
            {
                con = new SqlConnection(connectionStrings);
                con.Open();
                string sqlQuery = "INFOSECURITY_SAVE_FEEDBACKS";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, con);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@EMP_ID", Emp_id);
                result = new DataTable();
                sqlDataAdapter.Fill(result);
                status = (int)result.Rows[0]["STATUS"];

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            return status;
        }




    }
}