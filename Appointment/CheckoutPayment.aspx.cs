using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Appointment_CheckoutPayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void buttonApptCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("OnlineAppt.aspx");
    }

    protected void buttonPaymentConfirm_Click(object sender, EventArgs e)
    {

    }
}