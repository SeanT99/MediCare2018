using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_Patient_FirstLogin : System.Web.UI.Page
{
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


    //TODO check if both passwords match

    //TODO validate to make sure no 2 security qn is the same

    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
        string id = "ADMIN";//TODO add the id details

        // check if both passwords match
        if ( newPWTB.Text == conPWTB.Text)
        {
            //TODO validate to make sure no 2 security qn is the same

            // hash password and submit to database
            string[] passHash = pwUtility.generateHash(id, newPWTB.Text);
            int result = pwUtility.updatePassword(id, passHash[0], passHash[1]);


            //submit the security questions to database
            SecurityQuestion x = new SecurityQuestion(sq1DDL.SelectedItem.Text.ToUpper(), sqAns1TB.Text, sq2DDL.SelectedItem.Text.ToUpper(), sqAns2TB.Text, sq3DDL.SelectedItem.Text.ToUpper(), sqAns3TB.Text);
            x.SecurityQuestionUpdate(id);

            //TODO send email to the user 

        }
        else
        {
            Response.Write("<script>alert('Passwords do not match!');</script>");
        }
    }


    //TODO method to hash password and submit to database
}