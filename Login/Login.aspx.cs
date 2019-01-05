using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;

public partial class Login_Login : System.Web.UI.Page
{
    public static int UserLoginAttempts = 1;
    private string LoginNRIC;
    private string Password;


    protected void Page_Load(object sender, EventArgs e)
    {
        IncorrectUsernameAndPasswordLabel.Visible = false;
        SecurityTable.Visible = false;

    }



    protected void forgotPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("ForgetPasswordPage.aspx");

    }

    protected void ChangePasswordButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangePasswordPage.aspx");
    }

    protected void Login_Method(object sender, EventArgs e)
    {
        LoginNRIC = UsernameField.Text;
        Password = PasswordField.Text;

        string hashStr = "";
        //add the username to the password then hash the summed string
        string ToHashUserLoginInput = LoginNRIC + Password;

        Debug.Write(ToHashUserLoginInput);
        PatientInfo LoginInfo = new PatientInfo();
        PatientInfo UserLoginDetails = LoginInfo.GetLoginDetails(LoginNRIC);


        if (UserLoginDetails != null)
        {

            string originalSaltValue = UserLoginDetails.Salt;
            byte[] array = Convert.FromBase64String(originalSaltValue);
            Debug.Write("Original Salt Value" + " " + originalSaltValue);


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

            Debug.Write("Final hash value" + " " + hashStr);

            if (hashStr == UserLoginDetails.Login_password)
            {
                if (UserLoginDetails.Acctype == "PATIENT   ")
                {
                    //login as patient
                    Response.Redirect("../Appointment/OnlineAppt.aspx");
                }
                else
                {
                    //login as nurse
                    Response.Redirect("../Nurse/PatientRegistration.aspx");
                }
            }
            else
            {
                IncorrectUsernameAndPasswordLabel.Visible = true;

            }
        }
    }
    
}