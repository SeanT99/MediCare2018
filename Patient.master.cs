﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //check if is patient session
        if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
        {
            if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
            {
                Response.Redirect("../Login/Login.aspx", false);
            }
            // wrong account
            else if (Session["Acctype"].ToString().Trim() != "PATIENT")
            {
                Response.Redirect("../Unauthorized.aspx");
            }
        }
        else
        {
            Response.Redirect("../Login/Login.aspx", false);
        }

        //Response.AddHeader("Refresh", "60");
    }

    protected void Logout(object sender, EventArgs e)
    {
        //clear
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();

        Response.Redirect("../Login/Login.aspx", false);

        if (Request.Cookies["ASP.NET_SessionId"] != null)
        {
            Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
        }

        if (Request.Cookies["AuthToken"] != null)
        {
            Response.Cookies["AuthToken"].Value = string.Empty;
            Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
        }

    }

}
