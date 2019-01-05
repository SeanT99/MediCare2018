using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_Patient_FirstLogin : System.Web.UI.Page
{
    PasswordUtility pwUtility = new PasswordUtility();
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
        if( newPWTB.Text == conPWTB.Text)
        {
            //TODO check if both passwords match

            //TODO validate to make sure no 2 security qn is the same

            //TODO hash password and submit to database
            string[] hashDetails = pwUtility.generateHash("", newPWTB.Text);//TODO add the id details
        }
    }


    //TODO method to hash password and submit to database
}