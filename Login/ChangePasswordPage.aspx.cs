using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

public partial class Login_ChangePasswordPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ChangePassUserErrorLabel.Visible = false;
    }

    protected void details_Click(object sender, EventArgs e)
    {
        String Username = ChangePassUsernameField.Text;
        String NewPassword = ChangePasswordField.Text;
        String VerifyNewPassword = VerifyPasswordTextBox.Text;
        PatientInfo getUserInfo = new PatientInfo();
        PatientInfo InfoNeededForUserToChangePassword = getUserInfo.GetLoginDetails(Username);


        if (InfoNeededForUserToChangePassword.Id == Username )
        {
            
        }






      
        Response.Redirect("ConfirmChangedPassword.aspx", false);
    }
}