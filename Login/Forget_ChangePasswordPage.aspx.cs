using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
//imports needed for db
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Login_ChangePasswordPage : System.Web.UI.Page
{
    string id = "";
    MailUtilities mail = new MailUtilities();
    OTP o = new OTP();
    readonly PatientInfo pat = new PatientInfo();

    readonly string _connStr = ConfigurationManager.ConnectionStrings["MediCareContext"].ConnectionString;
    string passwordDoNotMatch = "- Password Do Not Match!";
    string passwordMinimum = "- Password Length Must Be More Than 6";
    string passwordMaximum = "- Password Length Must Be Less Than 16";
    string passwordUpper = "- Password Must Contain At Least 1 Uppercase Letter";
    string passwordAlpha = "- Password Be Alphanumeric, Has No Special Symbols";
    string UsernameDonExist = "- Username Do Not Exist";
    string NewAndVerifyPass = "- New Password Does Not Match With Each Other";


    protected void Page_Load(object sender, EventArgs e)
    {

        ChangePassUserErrorLabel.Visible = false;
        PasswordUsedPreviouslyLabel.Visible = false;
        NewPasswordDoesNotMatchLabel.Visible = false;
        AlphaNumericLabel.Visible = false;



    }

    protected void details_Click(object sender, EventArgs e)
    {

        String Username = ChangePassUsernameField.Text.ToUpper();
        String NewPassword = ChangePasswordField.Text;
        String VerifyNewPassword = VerifyPasswordTextBox.Text;

        PatientInfo getUserInfo = new PatientInfo();
        PatientInfo LoginDetails = getUserInfo.GetLoginDetails(Username); // Check if Username exist in db
        Debug.WriteLine(LoginDetails.Id + "Test"); // pass
        PasswordUtility CheckPassword = new PasswordUtility();
        ChangePasswordUtility InsertOldPassword = new ChangePasswordUtility();
        ChangePasswordUtility UpdateNewPasswordToPatientTable = new ChangePasswordUtility();
        ChangePasswordUtility UpdateNewDataToPasswordTable = new ChangePasswordUtility();
        PasswordValidator ValidatePassword = new PasswordValidator();
        MailUtilities NotifyPasswordChanged = new MailUtilities();

        //if otp correct then allow the below

        if (LoginDetails != null) // Check if Username exist in db / Valid NRIC Format
        {

            //checking otp
            string username_session = ChangePassUsernameField.Text; //get current user's id
            DateTime OTPdt = DateTime.Now; //get the current datetime
            string OTP = "0";
            string OTP_ID = "0";
            string OTP_used = "1";
            SqlConnection myConn = new SqlConnection(_connStr);
            myConn.Open();

            string checkOTP = "SELECT * FROM OTP WHERE patientID=@username AND otp_no=@OTP"; //find if got a otp record that tally with the input
            SqlCommand cmd = new SqlCommand(checkOTP, myConn);
            cmd.Parameters.AddWithValue("@username", username_session);
            cmd.Parameters.AddWithValue("@OTP", otp_tb.Text);
            try//try to retrieve any 
            {
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        OTP_ID = rdr["otp_id"].ToString();
                        OTP = rdr["otp_no"].ToString();
                        OTPdt = (DateTime)rdr["otp_timestamp"];
                        OTP_used = rdr["otp_used"].ToString();
                    }
                }

                myConn.Close();
            }
            catch (SqlException ex)
            {
                Debug.Write(ex);
            }

            if (OTP != "0")//if have record
            {
                Debug.WriteLine("OTP has record");
                double XTIME = subtractMinutes(OTPdt, DateTime.Now);
                Debug.Write(XTIME);
                Debug.Write(XTIME + OTP_used);
                if ((XTIME > 5) || (OTP_used == "1 "))
                {
                    //expired otp
                    lblError.Text = "Your OTP has expired.";
                }
                

                else
                {
                    Debug.WriteLine("STARTING TO INSERT PASSWORD");
                    //otp success
                    List<ChangePasswordUtility> InfoNeededForUserToChangePassword = InsertOldPassword.GetOldPasswordDetails(Username);
                    //Debug.WriteLine("Infoneeded Username" + " " + InfoNeededForUserToChangePassword[0].Id);
                    if (InfoNeededForUserToChangePassword.Count > 0 && ValidatePassword.IsValid(NewPassword) == true) // Check if Username exist in database, if does not, then password also cannot change
                    {                                              // To get new password + old salt       
                        if (NewPassword.Equals(VerifyNewPassword)) //if Username exist then continue to check new password and verify password if is the same
                        {

                            Debug.WriteLine("New pass = Verify pass PASS");//TODO remove
                            Boolean PasswordExist = false;
                            for (int i = 0; i < InfoNeededForUserToChangePassword.Count; i++) // To check if old password used
                            {
                                Debug.WriteLine("PASS1");//TODO remove
                                string AllExistingOldSalt = InfoNeededForUserToChangePassword[i].Old_salt.Trim();
                                string NewPasswordHashCheckWithAllExistingOldSalt = InsertOldPassword.generateHashWithOrignalSalt(Username, NewPassword, AllExistingOldSalt.Trim());
                                string AllOldPassword = InfoNeededForUserToChangePassword[i].Old_password.Trim();
                                //Help me to get the Old Password with old salt
                                if (NewPasswordHashCheckWithAllExistingOldSalt.Trim().Equals(AllOldPassword.Trim())) // Check if NewPass + old salt = OldPass + Old Salt (Check if password is used before)
                                {
                                    PasswordUsedPreviouslyLabel.Visible = true;
                                    PasswordExist = true;

                                    Debug.WriteLine("Password used");
                                }
                            }
                            if (PasswordExist == false) //If Password does not exist
                            {
                                //If is new password, update patientInfo salt and login password
                                string[] GenerateNewPasswordHash = CheckPassword.generateHash(Username, NewPassword);
                                string NewSalt = GenerateNewPasswordHash[0];
                                string NewLoginPassword = GenerateNewPasswordHash[1];
                                UpdateNewPasswordToPatientTable.updateAccountPassword(Username, NewLoginPassword, NewSalt);
                                //Retrieve updated salt and password 
                                PatientInfo getUpdatedPasswordAndSalt = getUserInfo.GetLoginDetails(Username);
                                string UpdatedSalt = getUpdatedPasswordAndSalt.Salt;
                                string UpdatedPassword = getUpdatedPasswordAndSalt.Login_password;
                                UpdateNewDataToPasswordTable.UpdatePasswordTableWithNewDataAfterUserChangedPassword(Username, UpdatedPassword, UpdatedSalt);

                                // Send email after password is changed
                                string FullName = LoginDetails.Family_Name + " " + LoginDetails.Given_Name;
                                NotifyPasswordChanged.sendChangePasswordMail(LoginDetails.Email, FullName, "");
                                Debug.WriteLine("Password not used");

                                //mark this otp as used
                                setUsed(username_session);
                                Response.Write("<script>alert('Your Password Has Been Changed Successfully');location.href='Login.aspx?id=" + "';</script>");
                                Response.Redirect("Forget_ConfirmChangedPassword.aspx", false);

                            }
                            PasswordExist = false;
                        }
                    }
                    //else if (!(LoginDetails.Id.Equals(Username)) && ValidatePassword.IsValid(NewPassword) == false)//The place to show error message
                    //{
                    //    Response.Write("<script>alert('" + "*** PLEASE TAKE NOTE *** " + "\\r\\n" + UsernameDonExist + "\\r\\n" + "PASSWORD REQUIREMENT" + "\\r\\n" + passwordMinimum + "\\r\\n" + passwordMaximum + "\\r\\n" + passwordUpper + "\\r\\n" + passwordAlpha + "');</script>");

                    //    //ChangePassUserErrorLabel.Visible = true;
                    //    //AlphaNumericLabel.Visible = true;
                    //    lblError.Text = "";

                    //    Debug.WriteLine("Password not valid, Username Invalid");
                    //}
                    else if (!(NewPassword.Equals(VerifyNewPassword)))
                    {
                        Response.Write("<script>alert('" + "*** PLEASE TAKE NOTE *** " + "\\r\\n" + "PASSWORD Does not match" + "\\r\\n" + "');</script>");
                        //AlphaNumericLabel.Visible = true;
                        Debug.WriteLine("Password Does not match");
                    }
                    else if (ValidatePassword.IsValid(NewPassword) == false && NewPassword.Equals(VerifyNewPassword))
                    {
                        Response.Write("<script>alert('" + "*** PLEASE TAKE NOTE *** " + "\\r\\n" + "PASSWORD MATCHES, BUT DOES NOT MEET THE PASSWORD REQURIEMENT" + "\\r\\n" + "PASSWORD REQUIREMENT" + "\\r\\n" + passwordMinimum + "\\r\\n" + passwordMaximum + "\\r\\n" + passwordUpper + "\\r\\n" + passwordAlpha + "');</script>");
                        //AlphaNumericLabel.Visible = true;
                        Debug.WriteLine("Password matches, but not valid");
                    }
              
                } //Insert Else statement ends here

            }
            else if (OTP == "0" || OTP_used == "1 ")
            {
                lblError.Text = "Your OTP is incorrect";
            }
        }// End of Login != null IF Loop 





        else if(LoginDetails == null)
        {
            ChangePassUserErrorLabel.Visible = true;
          
        }


    }

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //        if otp correct then allow the below

    //        //checking otp
    //    string username_session = Session["LoggedIn"].ToString(); //get current user's id
    //    DateTime OTPdt = DateTime.Now; //get the current datetime
    //    string OTP = "0";
    //    string OTP_ID = "0";
    //    string OTP_used = "1";
    //    SqlConnection myConn = new SqlConnection(_connStr);
    //    myConn.Open();

    //        string checkOTP = "SELECT * FROM OTP WHERE patientID=@username AND otp_no=@OTP"; //find if got a otp record that tally with the input
    //    SqlCommand cmd = new SqlCommand(checkOTP, myConn);
    //    cmd.Parameters.AddWithValue("@username", username_session);
    //        cmd.Parameters.AddWithValue("@OTP", otp_tb.Text);
    //        try//try to retrieve any 
    //        {
    //            using (SqlDataReader rdr = cmd.ExecuteReader())
    //            {
    //                while (rdr.Read())
    //                {
    //                    OTP_ID = rdr["otp_id"].ToString();
    //    OTP = rdr["otp_no"].ToString();
    //    OTPdt = (DateTime) rdr["OTP_timestamp"];
    //    OTP_used = rdr["otp_used"].ToString();
    //}
    //            }
    //        }
    //        catch (SqlException ex)
    //        {
    //            Debug.Write(ex);
    //        }


    //        if (OTP != "0")//if have record
    //        {
    //            if ((subtractMinutes(OTPdt, DateTime.Now) > 1) || (OTP_used == "1"))
    //            {
    //                //expired otp
    //                lblError.Text = "Your OTP has expired.";
    //            }
    //            else
    //            {
    //                //otp success
    //                String NewPassword = ChangePasswordField.Text;

    //PasswordValidator x = new PasswordValidator();
    //Debug.WriteLine(x.IsValid(NewPassword));

    //                //mark this otp as used
    //                setUsed(username_session);
    //            }
    //        }
    //        else
    //        {
    //            lblError.Text = "The OTP you've input is incorrect.";
    //        }

    //        myConn.Close();

    //}


    public int setUsed(string username)
    {
        int result = 0;
        SqlConnection myConn = new SqlConnection(_connStr);
        myConn.Open();

        string updateOTP = "UPDATE OTP SET otp_used=1 WHERE patientID=@username";
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


    protected void Button1_Click(object sender, EventArgs e)
    {
        String NewPassword = ChangePasswordField.Text;

        PasswordValidator x = new PasswordValidator();
        Debug.WriteLine(x.IsValid(NewPassword));

    }


    protected void Button1_Click1(object sender, EventArgs e)
    {

        Debug.WriteLine("STARTING TO INSERT PASSWORD");
        //otp success
        String Username = ChangePassUsernameField.Text;
        String NewPassword = ChangePasswordField.Text;
        String VerifyNewPassword = VerifyPasswordTextBox.Text;

        PatientInfo getUserInfo = new PatientInfo();
        PatientInfo LoginDetails = getUserInfo.GetLoginDetails(Username); // Check if Username exist in db
        Debug.WriteLine(LoginDetails.Id + "Test"); // pass
        PasswordUtility CheckPassword = new PasswordUtility();
        ChangePasswordUtility InsertOldPassword = new ChangePasswordUtility();
        ChangePasswordUtility UpdateNewPasswordToPatientTable = new ChangePasswordUtility();
        ChangePasswordUtility UpdateNewDataToPasswordTable = new ChangePasswordUtility();
        PasswordValidator ValidatePassword = new PasswordValidator();
        MailUtilities NotifyPasswordChanged = new MailUtilities();
        List<ChangePasswordUtility> InfoNeededForUserToChangePassword = InsertOldPassword.GetOldPasswordDetails(Username);
        Debug.WriteLine("Infoneeded Username" + " " + InfoNeededForUserToChangePassword[0].Id);

        if (LoginDetails != null) // Check if Username exist in db / Valid NRIC Format
        {
            if (InfoNeededForUserToChangePassword.Count > 0 && ValidatePassword.IsValid(NewPassword) == true) // Check if Username exist in database, if does not, then password also cannot change
            {
                Debug.WriteLine("Breaks here");// To get new password + old salt   
                Debug.WriteLine(NewPassword);
                Debug.WriteLine(VerifyNewPassword);
                if (NewPassword.Equals(VerifyNewPassword)) //if Username exist then continue to check new password and verify password if is the same
                {
                    Debug.WriteLine("New pass = Verify pass PASS");//TODO remove
                    Boolean PasswordExist = false;
                    for (int i = 0; i < InfoNeededForUserToChangePassword.Count; i++) // To check if old password used
                    {
                        Debug.WriteLine("PASS");//TODO remove
                        string AllExistingOldSalt = InfoNeededForUserToChangePassword[i].Old_salt.Trim();
                        string NewPasswordHashCheckWithAllExistingOldSalt = InsertOldPassword.generateHashWithOrignalSalt(Username, NewPassword, AllExistingOldSalt.Trim());
                        string AllOldPassword = InfoNeededForUserToChangePassword[i].Old_password.Trim();
                        //Help me to get the Old Password with old salt

                        if (NewPasswordHashCheckWithAllExistingOldSalt.Trim().Equals(AllOldPassword.Trim())) // Check if NewPass + old salt = OldPass + Old Salt (Check if password is used before)
                        {
                            PasswordUsedPreviouslyLabel.Visible = true;
                            PasswordExist = true;

                            Debug.WriteLine("Password used");
                        }
                    }
                    if (PasswordExist == false) //If Password does not exist
                    {
                        //If is new password, update patientInfo salt and login password
                        //string[] GenerateNewPasswordHash = CheckPassword.generateHash(Username, NewPassword);
                        //string NewSalt = GenerateNewPasswordHash[0];
                        //string NewLoginPassword = GenerateNewPasswordHash[1];
                        //UpdateNewPasswordToPatientTable.updateAccountPassword(Username, NewLoginPassword, NewSalt);
                        ////Retrieve updated salt and password 
                        //PatientInfo getUpdatedPasswordAndSalt = getUserInfo.GetLoginDetails(Username);
                        //string UpdatedSalt = getUpdatedPasswordAndSalt.Salt;
                        //string UpdatedPassword = getUpdatedPasswordAndSalt.Login_password;
                        //UpdateNewDataToPasswordTable.UpdatePasswordTableWithNewDataAfterUserChangedPassword(Username, UpdatedPassword, UpdatedSalt);

                        //// Send email after password is changed
                        //string FullName = LoginDetails.Family_Name + " " + LoginDetails.Given_Name;
                        //NotifyPasswordChanged.sendChangePasswordMail(LoginDetails.Email, FullName, "");

                        Response.Redirect("ConfirmChangedPassword.aspx", false);


                        Debug.WriteLine("Password not used");
                    }
                    PasswordExist = false;
                }
            }
        }
        else if (LoginDetails == null && ValidatePassword.IsValid(NewPassword) == false)//The place to show error message
        {
            ChangePassUserErrorLabel.Visible = true;
            AlphaNumericLabel.Visible = true;

        }
        else if (ValidatePassword.IsValid(NewPassword) == false && !(NewPassword.Equals(VerifyNewPassword)))
        {
            AlphaNumericLabel.Visible = true;
            NewPasswordDoesNotMatchLabel.Visible = true;

        }
        else if (ValidatePassword.IsValid(NewPassword) == false && NewPassword.Equals(VerifyNewPassword))
        {
            AlphaNumericLabel.Visible = true;
        }
        //mark this otp as used
    }






    protected void resend_btn_Click(object sender, EventArgs e)
    {

            string EnteredEmail = Request.QueryString["EnteredID"].ToString();
            PatientInfo EmailInfo = new PatientInfo();
            List<PatientInfo> AllEmailContact = EmailInfo.GetPatientsEmail(EnteredEmail);
            PatientInfo SpecificPatientName;

            SpecificPatientName = EmailInfo.GetSpecificPatientByEmail(EnteredEmail);
            String FamilyAndGivenName = SpecificPatientName.Given_Name + SpecificPatientName.Family_Name;
            id = pat.GetPatientIDByEmail(EnteredEmail);

            //TODO generate otp
            string otp = o.genOTP();
            //TODO create the otp object
            o = new OTP(id, otp);
            //TODO send otp to table
            int result = o.insertOTP();
            Debug.Write("-------" + result);

            string mobile = pat.GetPatientsMobile(id);

            string msg = "This is your medicare portal OTP " + otp;

            //send otp and send resend otp sms
            MailUtilities sendPasswordRequest = new MailUtilities();
            sendPasswordRequest.sendResendOTPMail(SpecificPatientName.Email, FamilyAndGivenName, otp);
            //sendPasswordRequest.sendOTP(mobile, msg);

    }
}









