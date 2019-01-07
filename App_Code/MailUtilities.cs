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
            result = 0;
        }

        return result;

    }

    //for notice of account deletion
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