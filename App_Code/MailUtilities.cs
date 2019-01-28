using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Mail;

//imports needed for db
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Collections.Specialized;
using System.Text;

/// <summary>
/// Summary description for MailUtilities
/// </summary>
public class MailUtilities
{
    //database connection string
    readonly string _connStr = ConfigurationManager.ConnectionStrings["MediCareContext"].ConnectionString;


    public MailUtilities(){}

    //for custom mailing text
    public void sendCustomMail(string email, string subject, string body)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mail = new MailMessage();

        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        mail.To.Add(new MailAddress(email));

        //to set the contents of the email
        mail.Subject =  subject;
        mail.Body = body;

        mail.BodyEncoding = System.Text.Encoding.UTF8;

        smtpClient.Send(mail);
    }

    //for new patient welcome
    public int sendWelcomeMail(string email, string name, string password)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        int result = 1;
        
        string body ="Hi " + name + ",<br/>Welcome to your new MediCare account.<br/><br/>" +"This is your new MediCare account password:<br/><b>" + password + "</b><br/><br/>" + "Please login at the link below and change the password on your first login.<br/>" + " <a href=\"http://localhost:49947/Login/Login\">Login Here</a> ";

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mail = new MailMessage();

        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        mail.To.Add(new MailAddress(email));
        

        //to set the contents of the email
        mail.Subject = "Welcome To MediCare Portal!!!";
        mail.Body = body;
        mail.IsBodyHtml = true;

        mail.BodyEncoding = System.Text.Encoding.UTF8;
        
        try
        {
            smtpClient.Send(mail);

        }
        catch (SmtpException ex)
        {
            Debug.Write(ex);
            result = 0;
        }

        return result;

        }

    //for notice of account deletion
    public int sendDeletedMail(string email, string name)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        int result = 1;
        
        string body = "Hi " + name + ",<br/><br/><b>Your MediCare account has been successfully deleted.</b><br/><br/>" + "Please contact the clinic at 6458 9900 if your account has been deleted wrongly.<br/>";

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mail = new MailMessage();

        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        mail.To.Add(new MailAddress(email));


        //to set the contents of the email
        mail.Subject = "Notice of MediCare account deletion";
        mail.Body = body;
        mail.IsBodyHtml = true;

        mail.BodyEncoding = System.Text.Encoding.UTF8;

        try
        {
            smtpClient.Send(mail);

        }
        catch (SmtpException ex)
        {
            Debug.Write(ex);
            result = 0;
        }

        return result;
    }

    //for reset of password (by nurse)
    public int sendNurseReset(string email, string name, string password)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        int result = 1;

        string body = "Hi " + name + ",<br/>Your password has been reset.<br/><br/>" + "This is your new MediCare account password:<br/><b>" + password + "</b><br/><br/>" + "Please login at the link below and change the password on your first login.<br/>" + " <a href=\"http://localhost:49947/Login/Login\">Login Here</a> ";

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mail = new MailMessage();

        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        mail.To.Add(new MailAddress(email));


        //to set the contents of the email
        mail.Subject = "Reset of MediCare password";
        mail.Body = body;
        mail.IsBodyHtml = true;

        mail.BodyEncoding = System.Text.Encoding.UTF8;

        try
        {
            smtpClient.Send(mail);

        }
        catch (SmtpException ex)
        {
            Debug.Write(ex);
            result = 0;
        }

        return result;

    }
    // For JJ Unblock account
    public int sendUnblockEmail(string email, string name, string password)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        int result = 1;

        string body = "Hi " + name + ",<br/>Your password has been unblocked<br/><br/>" + "This is your new MediCare account password:<br/><b>" + password + "</b><br/><br/>" + "Please login with this new password and reset your first time password to start using your account again.<br/>" + " <a href=\"http://localhost:65233/Login/Login.aspx\">Login Here</a> ";

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mail = new MailMessage();

        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        mail.To.Add(new MailAddress(email));


        //to set the contents of the email
        mail.Subject = "Your Medicare Account Has Been Unblocked";
        mail.Body = body;
        mail.IsBodyHtml = true;

        mail.BodyEncoding = System.Text.Encoding.UTF8;

        try
        {
            smtpClient.Send(mail);

        }
        catch (SmtpException ex)
        {
            Debug.Write(ex);
            result = 0;
        }

        return result;

    }


    public int NotifyMedicareEmail(string email,string name,string dob)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        int result = 1;

        string body = "This patient account has been blocked, please verify the user: <br/><br/>" + "Patient ID: " + name + "<br/> DOB: " + dob + "<br/> Email: " + email;

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mail = new MailMessage();

        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        mail.To.Add(new MailAddress("aspmedicare2018@gmail.com"));


        //to set the contents of the email
        mail.Subject = "Request To Unlock Account";
        mail.Body = body;
        mail.IsBodyHtml = true;

        mail.BodyEncoding = System.Text.Encoding.UTF8;

        try
        {
            smtpClient.Send(mail);

        }
        catch (SmtpException ex)
        {
            Debug.Write(ex);
            result = 0;
        }

        return result;

    }

    public int sendAccountBlockedEmail(string email, string name)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        int result = 1;

        string body = "Hi " + name + ",<br/>Your password has been blocked due to exceeding the amount of login attempts<br/><br/>" + "<br/><b>" + "</b><br/><br/>" + "Please Click On The Link Below To Contact Our Adminstrator For Assistance <br/>" + " <a href=\"http://localhost:65233/Login/Contact.aspx\">Contact Us</a> ";

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mail = new MailMessage();

        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        mail.To.Add(new MailAddress("yap_junjiang12@hotmail.com"));


        //to set the contents of the email
        mail.Subject = "Your Medicare Account Has Been blocked";
        mail.Body = body;
        mail.IsBodyHtml = true;

        mail.BodyEncoding = System.Text.Encoding.UTF8;

        try
        {
            smtpClient.Send(mail);

        }
        catch (SmtpException ex)
        {
            Debug.Write(ex);
            result = 0;
        }

        return result;

    }





    //for notice of password changed
    public int sendPasswordChanged(string email, string name)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        int result = 1;

        string body = "Hi " + name + ",<br/><br/><b>Your MediCare account password has been changed.</b><br/><br/>" + "Please contact the clinic at 6458 9900 if your account has not been changed by you.<br/>";

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mail = new MailMessage();

        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        mail.To.Add(new MailAddress(email));


        //to set the contents of the email
        mail.Subject = "MediCare Account Security Alert";
        mail.Body = body;
        mail.IsBodyHtml = true;

        mail.BodyEncoding = System.Text.Encoding.UTF8;

        try
        {
            smtpClient.Send(mail);

        }
        catch (SmtpException ex)
        {
            Debug.Write(ex);
            result = 0;
        }

        return result;
    }

    public string[] getPatientMailDetails (string id)
    {
        string[] x = null;

        //query string
        string queryStr = "SELECT given_Name,email FROM PatientInfo WHERE id = @id";

        //open connections, insert param and execute query
        SqlConnection conn = new SqlConnection(_connStr);
        SqlCommand cmd = new SqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            //store the data into object
            string name = dr["given_Name"].ToString();
            string email = dr["email"].ToString();
            x = new string[] {email, name};
        }

        //close connecetions
        conn.Close();
        dr.Close();
        dr.Dispose();
        


        return x;
    }

    //JJ
    public int sendChangePasswordMail(string email, string name, string otp)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        MailMessage mail = new MailMessage();
        String body = "";
        int result = 1;

        //sandra added otp parameter
        if (otp == "")
        {
            body = "Hi " + name + ",<br/>Your password has been changed successfully<br/><br/>" + "Click on the link below to change your password<br/><b>" + " " + "</b><br/><br/>" + " <a href=\"http://localhost:50581/Login/ChangePasswordPage.aspx\" >Click Here To Change Your Password</a> ";
            mail.Subject = "MediCare Account Security Alert";
        }
        else {
            body = "Hi " + name + ",<br/>You Requested To Change Your Password. The OTP is as follows: " + otp + "<br/><br/>" + "Click on the link below to change your password<br/><b>" + " " + "</b><br/><br/>" + " <a href=\"http://localhost:50581/Login/ChangePasswordPage.aspx\" > Login Here</a> ";
            mail.Subject = "MediCare Portal Change Password Request";
        }
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        

        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        mail.To.Add(new MailAddress(email));


        //to set the contents of the email
        mail.Subject = "MediCare Portal Change Password Request";
        mail.Body = body;
        mail.IsBodyHtml = true;

        mail.BodyEncoding = System.Text.Encoding.UTF8;

        try
        {
            smtpClient.Send(mail);

        }
        catch (SmtpException ex)
        {
            Debug.Write(ex);
            result = 0;
        }

        return result;

    }

    //for notice of security qn change
    public int sendSecQnChanged(string email, string name)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        int result = 1;

        string body = "Hi " + name + ",<br/><br/><b>Your MediCare account security questions has been changed.</b><br/><br/>" + "Please contact the clinic at 6458 9900 if your account has not been changed by you.<br/>";

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mail = new MailMessage();

        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        mail.To.Add(new MailAddress(email));


        //to set the contents of the email
        mail.Subject = "MediCare Account Security Alert";
        mail.Body = body;
        mail.IsBodyHtml = true;

        mail.BodyEncoding = System.Text.Encoding.UTF8;

        try
        {
            smtpClient.Send(mail);

        }
        catch (SmtpException ex)
        {
            Debug.Write(ex);
            result = 0;
        }

        return result;
    }

    //for notice of security qn/password firstlogin change
    public int sendFirstLoginChanged(string email, string name)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        int result = 1;

        string body = "Hi " + name + ",<br/><br/><b>Your MediCare account password and security questions has been changed.</b><br/><br/>" + "Please contact the clinic at 6458 9900 if your account has not been changed by you.<br/>";

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mail = new MailMessage();

        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        mail.To.Add(new MailAddress(email));


        //to set the contents of the email
        mail.Subject = "MediCare Account Security Alert";
        mail.Body = body;
        mail.IsBodyHtml = true;

        mail.BodyEncoding = System.Text.Encoding.UTF8;

        try
        {
            smtpClient.Send(mail);

        }
        catch (SmtpException ex)
        {
            Debug.Write(ex);
            result = 0;
        }

        return result;
    }


    //for notice of security qn change
    public int sendProfileChanged(string email, string name)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        int result = 1;

        string body = "Hi " + name + ",<br/><br/><b>Your MediCare account details has been changed.</b><br/><br/>" + "Please contact the clinic at 6458 9900 if your account has not been changed by you.<br/>";

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mail = new MailMessage();

        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        mail.To.Add(new MailAddress(email));


        //to set the contents of the email
        mail.Subject = "MediCare Account Security Alert";
        mail.Body = body;
        mail.IsBodyHtml = true;

        mail.BodyEncoding = System.Text.Encoding.UTF8;

        try
        {
            smtpClient.Send(mail);

        }
        catch (SmtpException ex)
        {
            Debug.Write(ex);
            result = 0;
        }

        return result;
    }


    //for patient otp
    public int sendOTP(string name, string email, string otp)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        int result = 1;

        string body = "Hi " + name + ",<br/><br/>" + "This is your otp:<br/><b>" + otp + "</b><br/><br/>";

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mail = new MailMessage();

        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        Debug.Write("MAIL EMAIL:" + email + "\n");
        mail.To.Add(new MailAddress(email));


        //to set the contents of the email
        mail.Subject = "Medicare OTP";
        mail.Body = body;
        mail.IsBodyHtml = true;

        mail.BodyEncoding = System.Text.Encoding.UTF8;

        try
        {
            smtpClient.Send(mail);

        }
        catch (SmtpException ex)
        {
            Debug.Write(ex);
            result = 0;
        }

        return result;

    }

    public int sendResendOTPMail(string email, string name, string otp)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        MailMessage mail = new MailMessage();
        String body = "";
        int result = 1;

        //sandra added otp parameter
        if (otp == "")
        {
            body = "Hi " + name + ",<br/>Your resend otp has been reset successfully<br/><br/>" + "Click on the link below to change your password<br/><b>" + " " + "</b><br/><br/>" + " <a href=\"http://localhost:50581/Login/ChangePasswordPage.aspx\" > Login Here</a> ";
            mail.Subject = "MediCare Account Security Alert";
        }
        else
        {
            body = "Hi " + name + ",<br/>You Requested to resend your otp. The OTP is as follows: " + otp + "<br/><br/>" + "Click on the link below to change your password<br/><b>" + " " + "</b><br/><br/>" + " <a href=\"http://localhost:50581/Login/ChangePasswordPage.aspx\" > Login Here</a> ";
            mail.Subject = "MediCare Portal Resend OTP Request";
        }
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;


        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        mail.To.Add(new MailAddress(email));


        //to set the contents of the email
        mail.Subject = "MediCare Portal Change Password Request";
        mail.Body = body;
        mail.IsBodyHtml = true;

        mail.BodyEncoding = System.Text.Encoding.UTF8;

        try
        {
            smtpClient.Send(mail);

        }
        catch (SmtpException ex)
        {
            Debug.Write(ex);
            result = 0;
        }

        return result;

    }

    //for notice of security qn change
    public int sendAccModded(string email, string name)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        int result = 1;

        string body = "Hi " + name + ",<br/><br/><b>Your MediCare account details has been modified.</b><br/><br/>" + "Please contact the clinic at 6458 9900 if your account has not been changed by you.<br/>";

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mail = new MailMessage();

        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        mail.To.Add(new MailAddress(email));


        //to set the contents of the email
        mail.Subject = "MediCare Account Security Alert";
        mail.Body = body;
        mail.IsBodyHtml = true;

        mail.BodyEncoding = System.Text.Encoding.UTF8;

        try
        {
            smtpClient.Send(mail);

        }
        catch (SmtpException ex)
        {
            Debug.Write(ex);
            result = 0;
        }

        return result;
    }


    public string sendOTP(string mobile, string message)
    {
        using (var wb = new WebClient())
        {
            var data = new NameValueCollection();
            data["SMSAccount"] = "A3";
            data["Pwd"] = "955460";
            data["Mobile"] = mobile;
            data["Message"] = message;

            var response = wb.UploadValues("http://sms.sit.nyp.edu.sg/SMSWebService/sms.asmx/sendMessage", "POST", data);
            string responseInString = Encoding.UTF8.GetString(response);

            return responseInString;
        }
    }



    /*
    //for sending reminder to patient to login to portal
    public int sendLoginReminder(string email, string name, string password)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        int result = 1;

        string body = "Hi " + name + ",<br/>This is a reminder to login to your <br/><br/>" + "This is your new MediCare account password:<br/><b>" + password + "</b><br/><br/>" + "Please login at the link below and change the password on your first login.<br/>" + " <a href=\"http://localhost:49947/Login/Login\">Login Here</a> ";

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("aspmedicare2018@gmail.com", "Exact123");
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mail = new MailMessage();

        //Setting From , To and CC
        mail.From = new MailAddress("aspmedicare2018@gmail.com", "MediCare Portal");
        mail.To.Add(new MailAddress(email));


        //to set the contents of the email
        mail.Subject = "Reset of MediCare password";
        mail.Body = body;
        mail.IsBodyHtml = true;

        mail.BodyEncoding = System.Text.Encoding.UTF8;

        try
        {
            smtpClient.Send(mail);

        }
        catch (SmtpException ex)
        {
            result = 0;
        }

        return result;

    }
    */
}