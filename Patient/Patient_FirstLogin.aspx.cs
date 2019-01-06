using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_Patient_FirstLogin : System.Web.UI.Page
{
    readonly MailUtilities mail = new MailUtilities();
    readonly PasswordUtility pwUtility = new PasswordUtility();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //TODO password rule checkers (check with jj)
    //RULES:
    // upper/lowercase
    // alphanumeric
    // length>6
    //Password will be match to be at least 4 characters, no more than 8 characters, and must include at least one upper case letter, one lower case letter, and one numeric digit. 



    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
        string id = "ADMIN";//TODO add the id details
        string q1, q2, q3;
        q1 = sq1DDL.SelectedItem.Text.ToUpper();
        q2 = sq2DDL.SelectedItem.Text.ToUpper();
        q3 = sq3DDL.SelectedItem.Text.ToUpper();

        // check if both passwords match
        if (newPWTB.Text == conPWTB.Text)
        {
            //TODO validate to make sure no 2 security qn is the same
            //12 13
            //23
            if (q1 == q2 || q1 == q3 || q2 == q3)
            {
                Response.Write("<script>alert('Please select different questions!');</script>");
            }
            else {

                // hash password and submit to database
                string[] passHash = pwUtility.generateHash(id, newPWTB.Text);
                int result = pwUtility.updatePassword(id, passHash[0], passHash[1]);


                //submit the security questions to database
                SecurityQuestion x = new SecurityQuestion(q1, sqAns1TB.Text, q2, sqAns2TB.Text, q3, sqAns3TB.Text);
                x.SecurityQuestionUpdate(id);

                //TODO retrieve user email /name
                string[] email = mail.getPatientMailDetails(id);
                //TODO send email to the user 
                mail.sendPasswordChanged(email[0], email[1]);

                //success message
                Response.Write("<script>alert('Password and security questions updated successfully');location.href='../Appointment/OnlineAppt.aspx';</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('Passwords do not match!');</script>");
        }
    }


    //TODO method to hash password and submit to database
}