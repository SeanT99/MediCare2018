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
        if (!IsPostBack)
        {
            //check session (remove code if cookie error)
            if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
               
                //check for token
                if (Session["AuthToken"].ToString() == (Request.Cookies["AuthToken"].Value.ToString()))
                {
                    //-----------redirect to correct homepage--------------------------
                    //-----------patient to online apt,nurse to patient reg------------   

                    //if account is patient
                    //-----------retrieve and check for acc type-----------------------


                    string Acctype = HttpContext.Current.Session["Acctype"].ToString();
                    if (Acctype.TrimEnd() == "PATIENT")
                    {
                        Response.Redirect("../Appointment/OnlineAppt.aspx", false);
                    }
                    //else nurse
                    else
                    {
                        Response.Redirect("../Nurse/PatientRegistration.aspx", false);
                    }
                }

                else
                {
                    Response.Redirect("../Login/Login.aspx", false);
                }

            }
        }

        IncorrectUsernameAndPasswordLabel.Visible = false;
        CaptchaClass.Visible = false;
        CaptchaNotCompletedLabel.Visible = true;

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



            if (UserLoginAttempts > 3)
            {
                CaptchaClass.Visible = true; // Set the captcha visible first
                if (Request.Form["g-recaptcha-response"] != "") // To Check If The Captcha Box Is Clicked
                {
                    CheckReCaptcha(); //Check if the Captcha Procedure is completed.                   
                
                    if (hashStr != UserLoginDetails.Login_password ) // If User Or Password Wrong And Have Captcha, show only IncorrectUserAndPassLabel
                    {
                        CaptchaNotCompletedLabel.Visible = false;
                        IncorrectUsernameAndPasswordLabel.Visible = true;
                    
                    }
                    else if ((hashStr == UserLoginDetails.Login_password)) // After captcha returned True if is completed, check If Username, Password and Captcha is all completed then allow login for user
                    {
                        if (UserLoginDetails.Acctype == "PATIENT   " && UserLoginDetails.Tochangepw == "TRUE      ")
                        {
                            //session ANSELM TEOH
                            Session["LoggedIn"] = UsernameField.Text.Trim().ToUpper();

                            Session["Acctype"] = UserLoginDetails.Acctype;

                            //create a new GUID and save into session
                            string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;

                            // Create cookie with this guid value
                            Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                            Response.Redirect("../Patient/Patient_FirstLogin.aspx", false);
                        }
                      
                        else if (UserLoginDetails.Acctype != "PATIENT   ")
                        {
                            //session ANSELM TEOH
                            Session["LoggedIn"] = UsernameField.Text.Trim().ToUpper();
                            Session["Acctype"] = UserLoginDetails.Acctype;
                            //create a new GUID and save into session
                            string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;

                            // Create cookie with this guid value
                            Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                            Response.Redirect("../Nurse/PatientRegistration.aspx", false);

                        }
                        else
                        {
                            //session ANSELM TEOH
                            Session["LoggedIn"] = UsernameField.Text.Trim().ToUpper();
                            Session["Acctype"] = UserLoginDetails.Acctype;
                            //create a new GUID and save into session
                            string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;

                            // Create cookie with this guid value
                            Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                            Response.Redirect("../Appointment/OnlineAppt.aspx", false);
                        }
                       
                    }
                }
                else
                {
                    if (hashStr != UserLoginDetails.Login_password) // If User Or Password Wrong and with no Captcha, Show Both Label
                    {
                        CaptchaNotCompletedLabel.Visible = true;
                        IncorrectUsernameAndPasswordLabel.Visible = true;
                       
                    }
                    else if (hashStr == UserLoginDetails.Login_password) //IF user and Password correct but no captcha, Show IncompleteCaptchaLabel
                    {
                        CaptchaNotCompletedLabel.Visible = true;
                        IncorrectUsernameAndPasswordLabel.Visible = false;
                       
                    }
                }
            } // end of UserLoginAttempt IF Loop
            else if (UserLoginAttempts <= 3)
            {
                if (hashStr != UserLoginDetails.Login_password || LoginNRIC != UserLoginDetails.Id ) // If User Or Password Wrong And Have Captcha, show only IncorrectUserAndPassLabel
                {
                    IncorrectUsernameAndPasswordLabel.Visible = true;
                    UserLoginAttempts++;

                }
                else if ((hashStr == UserLoginDetails.Login_password) && LoginNRIC == UserLoginDetails.Id) // After captcha returned True if is completed, check If Username, Password and Captcha is all completed then allow login for user
                {
                    if (UserLoginDetails.Acctype == "PATIENT   " && UserLoginDetails.Tochangepw == "TRUE      ")
                    {
                        //session ANSELM TEOH
                        Session["LoggedIn"] = UsernameField.Text.Trim().ToUpper();
                        Session["Acctype"] = UserLoginDetails.Acctype;
                        //create a new GUID and save into session
                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;

                        // Create cookie with this guid value
                        Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                        Response.Redirect("../Patient/Patient_FirstLogin.aspx", false);
                    }
                    else if (UserLoginDetails.Acctype == "PATIENT   " && UserLoginDetails.Tochangepw == "FALSE     ")
                    {
                        //session ANSELM TEOH
                        Session["LoggedIn"] = UsernameField.Text.Trim().ToUpper();
                        Session["Acctype"] = UserLoginDetails.Acctype;
                        //create a new GUID and save into session
                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;

                        // Create cookie with this guid value
                        Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                        Response.Redirect("../Appointment/OnlineAppt.aspx", false);
                    }

                    else if (UserLoginDetails.Acctype != "PATIENT   ")
                    {
                        //session ANSELM TEOH
                        Session["LoggedIn"] = UsernameField.Text.Trim().ToUpper();
                        Session["Acctype"] = UserLoginDetails.Acctype;
                        //create a new GUID and save into session
                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;

                        // Create cookie with this guid value
                        Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                        Response.Redirect("../Nurse/PatientRegistration.aspx", false);

                    }
                }             
            }          
        }
        else
        {
            IncorrectUsernameAndPasswordLabel.Visible = true;
        }
    }


    //Checking Captcha Success True Or False
    public bool CheckReCaptcha()
    {
        string Response = Request["g-recaptcha-response"];//Getting Response String Append to Post Method
        Debug.Write(Response);
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
}

















