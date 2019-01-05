using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Mail;

/// <summary>
/// Summary description for MailUtilities
/// </summary>
public class MailUtilities
{
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
    

}