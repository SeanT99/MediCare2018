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
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

public partial class Login_Login : System.Web.UI.Page
{
    public static int UserLoginAttempts = 1;
    private string LoginNRIC;
    private string Password;
    bool cValidated = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        IncorrectUsernameAndPasswordLabel.Visible = false;
        CaptchaClass.Visible = false;



    }

    public class CaptchaResponse
    {
        public bool success { get; set; }
        public string challenge_ts { get; set; }
        public string hostname { get; set; }
        public string errorCodes { get; set; }
    }

    public class MyObject
    {
        public string success { get; set; }
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
        LoginNRIC = UsernameField.Text.ToUpper();
        Password = PasswordField.Text;
        string hashStr = "";
        string reCaptchaSecret = "[6LdCW4UUAAAAADK9eQFh6LdFvhWaxgji0dv9iyc6]";
        string reCaptchaRequest = "";

        //add the username to the password then hash the summed string
        string ToHashUserLoginInput = LoginNRIC + Password;
        PatientInfo LoginInfo = new PatientInfo();
        PatientInfo UserLoginDetails = LoginInfo.GetLoginDetails(LoginNRIC);
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
            if (hashStr == UserLoginDetails.Login_password && LoginNRIC == UserLoginDetails.Id)
            {
                if (UserLoginDetails.Acctype == "PATIENT   ")
                {
                    Response.Redirect("../Appointment/OnlineAppt.aspx",false);
                }
                else if (UserLoginDetails.Acctype != "PATIENT   ")
                {
                    Response.Redirect("../Nurse/PatientRegistration.aspx",false);
                }
            }
            else
            {
                IncorrectUsernameAndPasswordLabel.Visible = true;
                UserLoginAttempts++;
            }
            //End of Login

            //Regarding Captcha
            // if UserLoginAttempts more than 3, Make sure the login take into account the response of the captcha, so that it does not login the user when the captcha is not completed
            // thats why have to seperate another login validation method

            if (UserLoginAttempts > 3)
            {
                CaptchaClass.Visible = true;

                if (Request.Form["g-recaptcha-response"] != null) // To Check If The Captcha Box Is Clicked
                {                    
                    if (CheckReCaptcha() == true)
                    {
                        Debug.Write("Captcha returned true");

                    }

                    

                }
               

            }                    
            // End Of Regarding Captcha

        }
    }


    //Checking Captcha Success True Or False
    public bool CheckReCaptcha()
    {
        string Response = Request["g-recaptcha-response"];//Getting Response String Append to Post Method
        bool Valid = false;
        //Request to Google Server
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create
        (" https://www.google.com/recaptcha/api/siteverify?secret=6LdCW4UUAAAAAByr9jibv0nrQGEMAGoWUleLC89c &response=" + Response);
        try
        {
            //Google recaptcha Response
            using (WebResponse wResponse = req.GetResponse())
            {

                using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                {
                    string jsonResponse = readStream.ReadToEnd();

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    MyObject data = js.Deserialize<MyObject>(jsonResponse);// Deserialize Json

                    Valid = Convert.ToBoolean(data.success);
                }
            }
            return Valid;
        }
        catch (WebException ex)
        {
            throw ex;
        }
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CheckReCaptcha() == true)
        {
            Debug.Write("Valid Recaptcha");
           
        }

        else
        {
            Debug.Write("Invalid Captcha");
        }



    }

     
    }

















