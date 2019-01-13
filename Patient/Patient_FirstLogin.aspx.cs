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

        // check if both passwords match
        if (NewPasswordTB.Text == ConfirmPasswordTB.Text)
        {
            //validate to make sure no 2 security qn is the same
            
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
                
                cpu.PatientInsertOldPassword(id, passHash[1]);
                
                //retrieve user email /name
                string[] email = mail.getPatientMailDetails(id);

                //send email to the user 
                mail.sendFirstLoginChanged(email[0], email[1]);

                //success message
                Response.Write("<script>alert('Password and security questions updated successfully');location.href='../Appointment/OnlineAppt.aspx';</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('Passwords do not match!');</script>");
        }
    }

    }
