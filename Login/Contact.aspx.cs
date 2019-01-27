using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login_ConfirmChangedPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BackToLoginPageButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

    protected void details_Click(object sender, EventArgs e)
    {
        string name = NameTB.Text.Trim();
        string email = emailTB.Text.Trim();
        string dob = DobTB.Text.Trim();

        PatientInfo y = new PatientInfo();
        PatientInfo CheckUserExist = y.GetLoginDetails(name);
        MailUtilities x = new MailUtilities();
        

        Label1.Text = "";

        if (CheckUserExist == null)
        {
            Label1.Text = "User / Email Address does not exist";
        }
        else
        {
            x.NotifyMedicareEmail(email,name,dob);
            Response.Redirect("Login.aspx",false);
        }




    }
}