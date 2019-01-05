using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Appointment_OnlineAppt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void buttonApptConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("CheckoutPayment.aspx");
    }
}