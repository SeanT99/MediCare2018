using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login_ChangePasswordPage : System.Web.UI.Page
{
    readonly MailUtilities mail = new MailUtilities();
    readonly PasswordUtility pwUtility = new PasswordUtility();
    readonly ChangePasswordUtility cpu = new ChangePasswordUtility();

    protected void Page_Load(object sender, EventArgs e)
    {
        //check if is patient session
        if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
        {
            if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
            {
                Response.Redirect("../Login/Login.aspx", false);
            }
        }
        else
        {
            Response.Redirect("../Login/Login.aspx", false);
        }

        PasswordIncorrectLabel.Visible = false;
    }

    protected void details_Click(object sender, EventArgs e)
    {
        string id = Session["LoggedIn"].ToString();//TODO add the id details
        string q1, q2;
        q1 = sq1DDL.SelectedItem.Text.ToUpper();
        q2 = sq2DDL.SelectedItem.Text.ToUpper();

        //For Validating password
        PasswordValidator ValidatePass = new PasswordValidator();
        string passwordDoNotMatch = "Password Do Not Match!";
        string passwordMinimum = "Password length must be more than 6";
        string passwordMaximum = "Password length must be less than 16";
        string passwordUpper = "Password must contain at least 1 Uppercase letter";
        string passwordAlpha = "Password be alphanumeric, has no special symbols";

        if(!passAuth(id, OldPasswordTB.Text))
        {
            PasswordIncorrectLabel.Visible = true;
        }

        
        //TODO VALIDATE PASSWORD

        // check if both passwords match
        if ((NewPasswordTB.Text == ConfirmPasswordTB.Text) && ValidatePass.IsValid(NewPasswordTB.Text))
        {
            // validate to make sure no 2 security qn is the same
            //12 13
            //23
            if (q1 == q2)
            {
                Response.Write("<script>alert('Please select different questions!');</script>");
            }
            else
            {

                // hash password and submit to database
                string[] passHash = pwUtility.generateHash(id, ConfirmPasswordTB.Text);
                int result = pwUtility.updatePassword(id, passHash[0], passHash[1]);


                //submit the security questions to database
                SecurityQuestion x = new SecurityQuestion(q1, sqAns1TB.Text, q2, sqAns2TB.Text);
                x.SecurityQuestionUpdate(id);

                String hashPassword = passHash[1].ToString().Trim();
                String OldSalt = passHash[0].Trim();
                
                cpu.PatientInsertOldPassword(id, hashPassword,OldSalt); // Insert into Password DB

                // retrieve user email /name
                string[] email = mail.getPatientMailDetails(id);

                // send email to the user 
                mail.sendPasswordChanged(email[0], email[1]);

                //success message
                Response.Write("<script>alert('Password and security questions updated successfully');location.href='../Appointment/OnlineAppt.aspx';</script>");
            }
        }
        else if((NewPasswordTB.Text == ConfirmPasswordTB.Text) && ValidatePass.IsValid(NewPasswordTB.Text) == false)
        {
           // Response.Write("<script>alert('Passwords do not match! <br/> ');</script>");
            Response.Write("<script>alert('" + "PASSWORD REQUIREMENT" + "\\r\\n" + passwordMinimum + "\\r\\n" + passwordMaximum + "\\r\\n" + passwordUpper + "\\r\\n" + passwordAlpha + "');</script>");
        }
        else
        {
            Response.Write("<script>alert('" + passwordDoNotMatch + "');</script>");
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