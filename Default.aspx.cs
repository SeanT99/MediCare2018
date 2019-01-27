using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //session ANSELM TEOH
        Session["LoggedIn"] = "Admin";
        Session["Acctype"] = "ADMIN";
        //create a new GUID and save into session
        string guid = Guid.NewGuid().ToString();
        Session["AuthToken"] = guid;

        // Create cookie with this guid value
        Response.Cookies.Add(new HttpCookie("AuthToken", guid));
    }
}