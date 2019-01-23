using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

    }

    protected void details_Click(object sender, EventArgs e)
    {

        string id = Session["LoggedIn"].ToString();//TODO add the id details
        string q1, q2, q3;
        q1 = sq1DDL.SelectedItem.Text.ToUpper();
        q2 = sq2DDL.SelectedItem.Text.ToUpper();
        q3 = sq3DDL.SelectedItem.Text.ToUpper();
        //For Validating password
        PasswordValidator ValidatePass = new PasswordValidator();
        string passwordDoNotMatch = "Password Do Not Match!";
        string passwordMinimum = "Password length must be more than 6";
        string passwordMaximum = "Password length must be less than 16";
        string passwordUpper = "Password must contain at least 1 Uppercase letter";
        string passwordAlpha = "Password be alphanumeric, has no special symbols";



        //TODO VALIDATE PASSWORD

        // check if both passwords match
        if ((NewPasswordTB.Text == ConfirmPasswordTB.Text) && ValidatePass.IsValid(NewPasswordTB.Text) == true)
        {
            //TODO validate to make sure no 2 security qn is the same
            //12 13
            //23
            if (q1 == q2 || q1 == q3 || q2 == q3)
            {
                Response.Write("<script>alert('Please select different questions!');</script>");
            }
            else
            {

                // hash password and submit to database
                string[] passHash = pwUtility.generateHash(id, ConfirmPasswordTB.Text);
                int result = pwUtility.updatePassword(id, passHash[0], passHash[1]);


                //submit the security questions to database
                SecurityQuestion x = new SecurityQuestion(q1, sqAns1TB.Text, q2, sqAns2TB.Text, q3, sqAns3TB.Text);
                x.SecurityQuestionUpdate(id);

                String hashPassword = passHash[1].ToString().Trim();
                String OldSalt = passHash[0].Trim();
                
                cpu.PatientInsertOldPassword(id, hashPassword,OldSalt); // Insert into Password DB

                //TODO retrieve user email /name
                string[] email = mail.getPatientMailDetails(id);

                //TODO send email to the user 
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

}