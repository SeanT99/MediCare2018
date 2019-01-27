using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login_ForgetPasswordEmailConfirmation : System.Web.UI.Page
{

    string email = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        email = Request.QueryString["EnteredID"].ToString();
    }

    protected void BackToLoginPageButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Forget_ChangePasswordPage.aspx?EnteredID="+email);
    }
}