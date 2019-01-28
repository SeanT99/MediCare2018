using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//imports needed for db
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

public partial class Patient_EditProfile_ChangeSecQn : System.Web.UI.Page
{
    string id = "";
    MailUtilities mail = new MailUtilities();
    readonly string _connStr = ConfigurationManager.ConnectionStrings["MediCareContext"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
        {
            if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
            {
                Response.Redirect("../Login/Login.aspx", false);
            }
            else
            {
                id = HttpContext.Current.Session["LoggedIn"].ToString();
            }
        }
        else
        {
            Response.Redirect("../Login/Login.aspx", false);
        }

    }

    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
        //checking otp
        string username_session = Session["LoggedIn"].ToString(); //get current user's id
        DateTime OTPdt = DateTime.Now; //get the current datetime
        string OTP = "0";
        string OTP_ID = "0";
        string OTP_used = "1";
        SqlConnection myConn = new SqlConnection(_connStr);
        myConn.Open();

        string checkOTP = "SELECT * FROM OTP WHERE patientID=@username AND OTP_no=@OTP"; //find if got a otp record that tally with the input
        SqlCommand cmd = new SqlCommand(checkOTP, myConn);
        cmd.Parameters.AddWithValue("@username", username_session);
        cmd.Parameters.AddWithValue("@OTP", otpTB.Text);
        try//try to retrieve any 
        {
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    OTP_ID = rdr["otp_id"].ToString();
                    OTP = rdr["otp_no"].ToString();
                    OTPdt = (DateTime)rdr["OTP_timestamp"];
                    OTP_used = rdr["otp_used"].ToString();
                }
            }
        }
        catch (SqlException ex)
        {
            Debug.Write(ex);
        }


        if (OTP != "0")//if have record
        {
            if ((subtractMinutes(OTPdt, DateTime.Now) > 1) || (OTP_used == "1"))
            {
                //expired otp
                lblError.Text = "Your OTP has expired.";
            }
            else
            {
                //otp success
                string q1, q2, q3;
                q1 = sq1DDL.SelectedItem.Text.ToUpper();
                q2 = sq2DDL.SelectedItem.Text.ToUpper();

                //submit the security questions to database
                SecurityQuestion x = new SecurityQuestion(q1, sqAns1TB.Text.ToUpper(), q2, sqAns2TB.Text.ToUpper());
                int result = x.SecurityQuestionUpdate(id);

                if (result > 0)
                {
                    // retrieve user email /name
                    string[] email = mail.getPatientMailDetails(id);

                    // send email to the user 
                    mail.sendSecQnChanged(email[0], email[1]);

                    //success message
                    Response.Write("<script>alert('Security questions updated successfully');location.href='../Appointment/OnlineAppt.aspx';</script>");
                }

                //mark this otp as used
                setUsed(username_session);
            }
        }
        else
        {
            lblError.Text = "The OTP you've input is incorrect.";
        }

        myConn.Close();
        
        
    }
    public int setUsed(string username)
    {
        int result = 0;
        SqlConnection myConn = new SqlConnection(_connStr);
        myConn.Open();

        string updateOTP = "UPDATE OTP SET OTP_used=1 WHERE patientID=@username";
        SqlCommand cmd = new SqlCommand(updateOTP, myConn);
        cmd.Parameters.AddWithValue("@username", username);
        try
        {
            result = cmd.ExecuteNonQuery();

            myConn.Close();
        }
        catch (SqlException e)
        {
            Debug.Write(e);
        }

        return result;
    }

    public double subtractMinutes(DateTime start, DateTime end)
    {
        return (end - start).TotalMinutes;
    }
}