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
        string Option = "";

        Label1.Text = "";
        PatientInfo y = new PatientInfo();
        PatientInfo CheckUserExist = y.GetLoginDetails(name);
        MailUtilities x = new MailUtilities();

        if (OptionRadio.SelectedIndex == 0)
        {
            Option = "Unblock Of Account";
        }
        else
        {
            Option = "Reset Password";
        }
        

        if (CheckUserExist == null)
        {
            Label1.Text = "User / Email Address does not exist";
        }
        else if (Option == "Unblock Of Account")
        {
            x.NotifyMedicareEmail(email, name, dob);
            Response.Redirect("Login.aspx", false);
        }
        else if (Option == "Reset Password")
        {
            x.UserRequestChangePasswordEmail(email,name,dob);
            Response.Redirect("Login.aspx", false);
        }
       




    }
}