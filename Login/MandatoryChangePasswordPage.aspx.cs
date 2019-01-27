using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login_MandatoryChangePasswordPage : System.Web.UI.Page
{
    readonly MailUtilities mail = new MailUtilities();
    readonly PasswordUtility pwUtility = new PasswordUtility();
    PasswordValidator pwValidator = new PasswordValidator();
    string passwordDoNotMatch = "- Password Do Not Match!";
    string passwordMinimum = "- Password Length Must Be More Than 6";
    string passwordMaximum = "- Password Length Must Be Less Than 16";
    string passwordUpper = "- Password Must Contain At Least 1 Uppercase Letter";
    string passwordAlpha = "- Password Be Alphanumeric, Has No Special Symbols";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
        {
            if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
            {
                Response.Redirect("Login.aspx", false);
            }
        }
        else
        {
            Response.Redirect("../Login/Login.aspx", false);
        }

        PasswordUsedPreviouslyLabel.Visible = false;
        NewPasswordDoesNotMatchLabel.Visible = false;
        AlphaNumericLabel.Visible = false;
        PasswordIncorrectLabel.Visible = false;
    }

    protected void details_Click(object sender, EventArgs e)
    {
        //get the patient id from the session
        string id = Session["LoggedIn"].ToString();
        //check the old password and both new match
        bool pass = passAuth(id, old_tb.Text);
        string newPassword = ChangePasswordField.Text;
        string confirmNewPassword = VerifyPasswordTextBox.Text;


        if (!pass)
        {
            PasswordIncorrectLabel.Visible = true;
        }
        //if pass
        if (pass && (ChangePasswordField.Text == VerifyPasswordTextBox.Text) )
        {
            if (pwValidator.IsValid(newPassword) == true)
            {
                // hash password and submit to database
                string[] passHash = pwUtility.generateHash(id, ChangePasswordField.Text);
                int result = pwUtility.updatePassword(id, passHash[0], passHash[1]);


                //send security alert
                string[] email = mail.getPatientMailDetails(id);
                mail.sendPasswordChanged(email[0], email[1]);

                //show success message
                Response.Write("<script>alert('Password updated successfully');location.href='../Appointment/OnlineAppt.aspx';</script>");
            }          
            else if (pwValidator.IsValid(newPassword) == false && newPassword.Equals(confirmNewPassword))
            {
                Response.Write("<script>alert('" + "*** PLEASE TAKE NOTE *** " + "\\r\\n" + "PASSWORD MATCHES, BUT DOES NOT MEET THE PASSWORD REQURIEMENT" + "\\r\\n" + "PASSWORD REQUIREMENT" + "\\r\\n" + passwordMinimum + "\\r\\n" + passwordMaximum + "\\r\\n" + passwordUpper + "\\r\\n" + passwordAlpha + "');</script>");
                //AlphaNumericLabel.Visible = true;
                Debug.WriteLine("Password matches, but not valid");
            }
        }
        else if (!(ChangePasswordField.Text == VerifyPasswordTextBox.Text))
        {
            NewPasswordDoesNotMatchLabel.Visible = true;
        }
    }

    protected bool passAuth(string id, string Password)
    {
        bool pass = false;

        string hashStr = "";

        //add the username to the password then hash the summed string
        string ToHashUserLoginInput = id + Password;

        PatientInfo LoginInfo = new PatientInfo();
        PatientInfo UserLoginDetails = LoginInfo.GetLoginDetails(id);

        if (UserLoginDetails != null)
        {
            string originalSaltValue = UserLoginDetails.Salt;
            byte[] array = Convert.FromBase64String(originalSaltValue);


            //2. concatenate the plaintext to the salt and hash it (using PBKDF2)
            var pbkdf2 = new Rfc2898DeriveBytes(ToHashUserLoginInput, array, 10000);

            //3. store the hash 
            //place the string in the byte array
            byte[] hash = pbkdf2.GetBytes(20);

            //make new byte array to store the hashed plaintext+salt
            //why 36? cause 20 for hash 16 for salt 
            byte[] hashBytes = new byte[36];
            ////place the salt and hash in their respective places
            Array.Copy(array, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            ////4. convert the byte array to a string
            hashStr = Convert.ToBase64String(hashBytes);


            if (hashStr == UserLoginDetails.Login_password && UserLoginDetails.Acctype != "PATIENT   ")
            {
                pass = true;
            }
        }


        return pass;
    }
}