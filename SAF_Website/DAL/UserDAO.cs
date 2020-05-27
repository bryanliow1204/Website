using SAF_Website.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SAF_Website.DAL
{
    public class UserDAO
    {

        //insert for registration form
        public int Insert(User reg)
        {
            // Execute NonQuery return an integer value
            int result = 0;
            SqlCommand sqlCmd = new SqlCommand();

            //Step 1 -  Define a connection to the database by getting
            //          the connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["SAF"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "INSERT INTO [User](email,otpcode) " + "VALUES (@email,@otpcode)";
            sqlCmd = new SqlCommand(sqlStmt, myConn);
            sqlCmd.Parameters.AddWithValue("@email", reg.email_addr);
            sqlCmd.Parameters.AddWithValue("@otpcode", reg.otp);
            //sqlCmd.Parameters.AddWithValue("@username", reg.Username);
            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();
            // Step 5 :Close connection
            myConn.Close();

            return result;
        }



        //select the id 
        //public User SelectById(string email)
        //{
        //    //Step 1 -  Define a connection to the database by getting
        //    //          the connection string from web.config
        //    string DBConnect = ConfigurationManager.ConnectionStrings["SAF"].ConnectionString;
        //    SqlConnection myConn = new SqlConnection(DBConnect);

        //    //Step 2 -  Create a DataAdapter to retrieve data from the database table
        //    string sqlStmt = "Select * from [User] where email = @email";
        //    SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);

        //    da.SelectCommand.Parameters.AddWithValue("@email", email);

        //    //Step 3 -  Create a DataSet to store the data to be retrieved
        //    DataSet ds = new DataSet();

        //    //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
        //    da.Fill(ds);

        //    //Step 5 -  Read data from DataSet.
        //    User reg = null;
        //    int rec_cnt = ds.Tables[0].Rows.Count;
        //    if (rec_cnt == 1)
        //    {
        //        DataRow row = ds.Tables[0].Rows[0];  // Sql command returns only one record
        //        string eMail = row["email"].ToString();
        //        string otpcode = row["otpcode"].ToString();
        //        string name = row["username"].ToString();
       


        //        reg = new User(eMail,otpcode,name);
        //    }
        //    else
        //    {
        //        reg = null;
        //    }

        //    return reg;
        //}


        //update the otp everytime they sign in
        public int UpdateOTP(string newOTP, string email)
        {
            int result = 0;
            SqlCommand sqlCmd = new SqlCommand();

            string DBConnect = ConfigurationManager.ConnectionStrings["SAF"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            // Step 2 - Instantiate SqlCommand instance to add record 
            //          with INSERT statement
            string sqlStmt = "UPDATE [User] SET otpcode = @otpcode where email= @email";

            sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            sqlCmd.Parameters.AddWithValue("@otpcode", newOTP);
            sqlCmd.Parameters.AddWithValue("@email", email);


            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        }


    }
}