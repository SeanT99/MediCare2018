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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;

public partial class Login_Login : System.Web.UI.Page
{
    private static int NonAccountAttempt = 0;

    public static int UserLoginAttempts = 0;
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

        LoginNRIC = UsernameField.Text.ToUpper().Trim();
        Password = PasswordField.Text;
        string hashStr = "";

        //add the username to the password then hash the summed string
        string ToHashUserLoginInput = LoginNRIC + Password;
        PatientInfo LoginInfo = new PatientInfo();
        PatientInfo UserLoginDetails = LoginInfo.GetLoginDetails(LoginNRIC);
        MailUtilities c = new MailUtilities();


        if (UserLoginDetails != null && UserLoginDetails.Acctype == "PATIENT   " && UserLoginDetails.Accountstatus == "AVAILABLE ")
        {
            //----------------ALL FOR PATIENTS----------------------------
            Debug.WriteLine("PATIENT ACCOUNT TEST");
            PatientInfo GetPatientLoginAttemptAndStatus = LoginInfo.GetPatientLoginAttemptAndAccountStatus(LoginNRIC);
            int UserCurrentLoginAttempts = GetPatientLoginAttemptAndStatus.Loginattempts; //User login attempt
            Debug.WriteLine(UserCurrentLoginAttempts);


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

            //CHANGE IT TO IF LESS THAN OR = 3 *MAKE IT STAND ALONE CAUSE IF LESS THAN 3 CAN JUST LOGIN, IF FAIL UPDATE COUNTER*
            if (UserCurrentLoginAttempts <= 2) // ONLY REACHES THIS LOOP IS THERE IS NO FAIL LOGIN MORE THAN 3 TIMES
            {
                if (hashStr != UserLoginDetails.Login_password || LoginNRIC != UserLoginDetails.Id) // If User Or Password Wrong And Have Captcha, show only IncorrectUserAndPassLabel
                {
                    IncorrectUsernameAndPasswordLabel.Visible = true;
                    UserCurrentLoginAttempts++;
                    Debug.WriteLine(UserCurrentLoginAttempts);
                    LoginInfo.updatePatientLoginAttempt(LoginNRIC, UserCurrentLoginAttempts);
                    
                }
                else if ((hashStr == UserLoginDetails.Login_password) && LoginNRIC == UserLoginDetails.Id) // After captcha returned True if is completed, check If Username, Password and Captcha is all completed then allow login for user
                {
                    if (UserLoginDetails.Acctype.Trim() == "PATIENT" && UserLoginDetails.Tochangepw.Trim() == "TRUE")
                    {
                        //session ANSELM TEOH
                        Session["LoggedIn"] = UsernameField.Text.Trim().ToUpper();
                        Session["Acctype"] = UserLoginDetails.Acctype;
                        //create a new GUID and save into session
                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;

                        // Create cookie with this guid value
                        Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                        NonAccountAttempt = 0;
                        LoginInfo.updatePatientLoginAttempt(LoginNRIC);
                        Response.Redirect("../Login/Patient_FirstLogin.aspx", false);
                    }
                    else if (UserLoginDetails.Acctype.Trim() == "PATIENT" && UserLoginDetails.Tochangepw.Trim() == "MCP")
                    {
                        //session ANSELM TEOH
                        Session["LoggedIn"] = UsernameField.Text.Trim().ToUpper();
                        Session["Acctype"] = UserLoginDetails.Acctype;
                        //create a new GUID and save into session
                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;

                        // Create cookie with this guid value
                        Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                        NonAccountAttempt = 0;
                        LoginInfo.updatePatientLoginAttempt(LoginNRIC);
                        Response.Redirect("../Login/MandatoryChangePasswordPage.aspx", false);
                    }

                    else if (UserLoginDetails.Acctype.Trim() == "PATIENT" && UserLoginDetails.Tochangepw.Trim() == "FALSE")
                    {
                        //session ANSELM TEOH
                        Session["LoggedIn"] = UsernameField.Text.Trim().ToUpper();
                        Session["Acctype"] = UserLoginDetails.Acctype;
                        //create a new GUID and save into session
                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;

                        // Create cookie with this guid value
                        Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                        NonAccountAttempt = 0;
                        LoginInfo.updatePatientLoginAttempt(LoginNRIC);
                        Response.Redirect("../Appointment/OnlineAppt.aspx", false);
                    }
                   
                }
            }

            if (UserCurrentLoginAttempts >= 3 && UserCurrentLoginAttempts < 6) // NEED TO SET MROE THAN 3 AND LESS THAN 6
            {
                CaptchaClass.Visible = true; // Set the captcha visible first
                if (Request.Form["g-recaptcha-response"] != "") // To Check If The Captcha Box Is Clicked
                {
                    if (CheckReCaptcha() == true)
                    {
                        if (hashStr != UserLoginDetails.Login_password) // If User Or Password Wrong And Have Captcha, show only IncorrectUserAndPassLabel
                        {
                            CaptchaNotCompletedLabel.Visible = false;
                            IncorrectUsernameAndPasswordLabel.Visible = true;
                            UserCurrentLoginAttempts++;
                            LoginInfo.updatePatientLoginAttempt(LoginNRIC, UserCurrentLoginAttempts);

                        }
                        else if ((hashStr == UserLoginDetails.Login_password)) // After captcha returned True if is completed, check If Username, Password and Captcha is all completed then allow login for user
                        {
                            if (UserLoginDetails.Acctype.Trim() == "PATIENT" && UserLoginDetails.Tochangepw.Trim() == "TRUE")
                            {
                                //session ANSELM TEOH
                                Session["LoggedIn"] = UsernameField.Text.Trim().ToUpper();

                                Session["Acctype"] = UserLoginDetails.Acctype;

                                //create a new GUID and save into session
                                string guid = Guid.NewGuid().ToString();
                                Session["AuthToken"] = guid;
                                // Create cookie with this guid value
                                Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                                NonAccountAttempt = 0;
                                LoginInfo.updatePatientLoginAttempt(LoginNRIC);
                                Response.Redirect("../Login/Patient_FirstLogin.aspx", false);
                            }
                            else if (UserLoginDetails.Acctype.Trim() == "PATIENT" && UserLoginDetails.Tochangepw.Trim() == "MCP")
                            {
                                //session ANSELM TEOH
                                Session["LoggedIn"] = UsernameField.Text.Trim().ToUpper();

                                Session["Acctype"] = UserLoginDetails.Acctype;

                                //create a new GUID and save into session
                                string guid = Guid.NewGuid().ToString();
                                Session["AuthToken"] = guid;
                                // Create cookie with this guid value
                                Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                                NonAccountAttempt = 0;
                                LoginInfo.updatePatientLoginAttempt(LoginNRIC);
                                Response.Redirect("../Login/MandatoryChangePasswordPage.aspx", false);
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
                                NonAccountAttempt = 0;
                                LoginInfo.updatePatientLoginAttempt(LoginNRIC);
                                Response.Redirect("../Appointment/OnlineAppt.aspx", false);
                            }
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
            } // end of UserLoginAttempt > 3 IF Loop
            else if (UserCurrentLoginAttempts > 5)
            {
                if (UserLoginDetails.Accountstatus == "AVAILABLE ")
                {
                    LoginInfo.updatePatientAccountStatus(LoginNRIC, "LOCKED");
                    c.sendAccountBlockedEmail(UserLoginDetails.Email,UserLoginDetails.Given_Name);
                    Response.Write("<script>alert('" + "*** PLEASE TAKE NOTE *** " + "\\r\\n" + "YOUR ACCOUNT HAS BEEN LOCKED DUE TO TOO MANY ATTEMPTS, PLEASE CONTACT ADMINSTRATOR FOR ASSISTANCE" + "\\r\\n" + "');</script>");                   
                    //update status to blocked
                }        
            }
            // IF LOGIN ATTEMPT MORE THAN 5, BLOCK ACCOUNT
        } // END OF USER NOT NULL IF CONDITION        
        else if (UserLoginDetails.Accountstatus != "AVAILABLE ") 
        {
            Response.Write("<script>alert('" + "*** PLEASE TAKE NOTE *** " + "\\r\\n" + "YOUR ACCOUNT HAS BEEN LOCKED, PLEASE CONTACT ADMINSTRATOR FOR ASSISTANCE" + "\\r\\n" + "');</script>");
            //Create another page for email and redirect them
        }
        //------------------------ADMIN--------------------------------
        else if (UserLoginDetails != null && UserLoginDetails.Acctype == "ADMIN     ") //Admin account with catpcha validation
        {
            Debug.WriteLine("ADMIN ACCOUNT TEST");
            PatientInfo GetPatientLoginAttemptAndStatus = LoginInfo.GetPatientLoginAttemptAndAccountStatus(LoginNRIC);
            int UserCurrentLoginAttempts = GetPatientLoginAttemptAndStatus.Loginattempts; //User login attempt
            Debug.WriteLine(UserCurrentLoginAttempts);

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

            //CHANGE IT TO IF LESS THAN OR = 3 *MAKE IT STAND ALONE CAUSE IF LESS THAN 3 CAN JUST LOGIN, IF FAIL UPDATE COUNTER*
            if (UserCurrentLoginAttempts <= 3) // ONLY REACHES THIS LOOP IS THERE IS NO FAIL LOGIN MORE THAN 3 TIMES
            {
                if (hashStr != UserLoginDetails.Login_password || LoginNRIC != UserLoginDetails.Id) // If User Or Password Wrong And Have Captcha, show only IncorrectUserAndPassLabel
                {
                    IncorrectUsernameAndPasswordLabel.Visible = true;
                    UserCurrentLoginAttempts++;
                    Debug.WriteLine(UserCurrentLoginAttempts);
                    LoginInfo.updatePatientLoginAttempt(LoginNRIC, UserCurrentLoginAttempts);


                }
                else if ((hashStr == UserLoginDetails.Login_password) && LoginNRIC == UserLoginDetails.Id) // After captcha returned True if is completed, check If Username, Password and Captcha is all completed then allow login for user
                {
                    //Straight login to admin page
                    if (UserLoginDetails.Acctype != "PATIENT   ")
                    {

                        //session ANSELM TEOH
                        Session["LoggedIn"] = UsernameField.Text.Trim().ToUpper();
                        Session["Acctype"] = UserLoginDetails.Acctype;
                        //create a new GUID and save into session
                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;

                        // Create cookie with this guid value
                        Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                        NonAccountAttempt = 0;


                        #region  Create auditlog for login
                        //Connect database
                        string connectionString;
                        SqlConnection cnn = null;

                        connectionString = ConfigurationManager.ConnectionStrings["MedicareContext"].ConnectionString;
                        cnn = new SqlConnection(connectionString);

                        if (cnn != null && cnn.State == ConnectionState.Closed)
                        {
                            cnn.Open();
                        }

                        string action = "LOGIN";
                        string log = "User login " + LoginNRIC;
                        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                                        CultureInfo.InvariantCulture);

                        SqlCommand cmd;
                        String sql = "";

                        sql = "INSERT [AuditLog] (Action, Log, Timestamp) VALUES (@Action, @Log, @Timestamp)";

                        cmd = new SqlCommand(sql, cnn);
                        cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Text) { Value = action });
                        cmd.Parameters.Add(new SqlParameter("@Log", SqlDbType.Text) { Value = log });
                        cmd.Parameters.Add(new SqlParameter("@Timestamp", SqlDbType.DateTime) { Value = timestamp });

                        Int32 rowsAffected = cmd.ExecuteNonQuery();
                        #endregion
                        LoginInfo.updatePatientLoginAttempt(LoginNRIC);
                        Response.Redirect("../Nurse/PatientRegistration.aspx", false);
                    }
                }
            }

            if (UserCurrentLoginAttempts > 3) // NEED TO SET MROE THAN 3 AND LESS THAN 6
            {
                CaptchaClass.Visible = true; // Set the captcha visible first
                if (Request.Form["g-recaptcha-response"] != "") // To Check If The Captcha Box Is Clicked
                {
                    CheckReCaptcha(); //Check if the Captcha Procedure is completed.                   
                    if (hashStr != UserLoginDetails.Login_password) // If User Or Password Wrong And Have Captcha, show only IncorrectUserAndPassLabel
                    {
                        CaptchaNotCompletedLabel.Visible = false;
                        IncorrectUsernameAndPasswordLabel.Visible = true;
                        UserCurrentLoginAttempts++;
                        Debug.WriteLine(UserCurrentLoginAttempts);
                        LoginInfo.updatePatientLoginAttempt(LoginNRIC, UserCurrentLoginAttempts);

                    }
                    else if ((hashStr == UserLoginDetails.Login_password)) // After captcha returned True if is completed, check If Username, Password and Captcha is all completed then allow login for user
                    {

                        if (UserLoginDetails.Acctype != "PATIENT   ")
                        {
                            //session ANSELM TEOH
                            Session["LoggedIn"] = UsernameField.Text.Trim().ToUpper();
                            Session["Acctype"] = UserLoginDetails.Acctype;
                            //create a new GUID and save into session
                            string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;

                            // Create cookie with this guid value
                            Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                            NonAccountAttempt = 0;

                            #region  Create auditlog for login
                            //Connect database
                            string connectionString;
                            SqlConnection cnn = null;

                            connectionString = ConfigurationManager.ConnectionStrings["MedicareContext"].ConnectionString;
                            cnn = new SqlConnection(connectionString);

                            if (cnn != null && cnn.State == ConnectionState.Closed)
                            {
                                cnn.Open();
                            }

                            string action = "LOGIN";
                            string log = "User login " + LoginNRIC;
                            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                                            CultureInfo.InvariantCulture);

                            SqlCommand cmd;
                            String sql = "";

                            sql = "INSERT [AuditLog] (Action, Log, Timestamp) VALUES (@Action, @Log, @Timestamp)";

                            cmd = new SqlCommand(sql, cnn);
                            cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Text) { Value = action });
                            cmd.Parameters.Add(new SqlParameter("@Log", SqlDbType.Text) { Value = log });
                            cmd.Parameters.Add(new SqlParameter("@Timestamp", SqlDbType.DateTime) { Value = timestamp });

                            Int32 rowsAffected = cmd.ExecuteNonQuery();
                            #endregion


                            LoginInfo.updatePatientLoginAttempt(LoginNRIC);
                            Response.Redirect("../Nurse/PatientRegistration.aspx", false);

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
            } // end of UserLoginAttempt > 3 IF Loop
        } // end of admin loop
        //Else loop for acocunt does not exist one
        else
        {
            //dd
            Debug.WriteLine("Non Exsiting ACCOUNT TEST");
            //CHANGE IT TO IF LESS THAN OR = 3 *MAKE IT STAND ALONE CAUSE IF LESS THAN 3 CAN JUST LOGIN, IF FAIL UPDATE COUNTER*
            if (NonAccountAttempt <= 3) // ONLY REACHES THIS LOOP IS THERE IS NO FAIL LOGIN MORE THAN 3 TIMES
            {
                IncorrectUsernameAndPasswordLabel.Visible = true;
                NonAccountAttempt++;
            }
            if (NonAccountAttempt > 3) // NEED TO SET MROE THAN 3 AND LESS THAN 6
            {
                CaptchaClass.Visible = true; // Set the captcha visible first
            } // end of else loop
            
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



    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Contact.aspx", false);
    }
}

















