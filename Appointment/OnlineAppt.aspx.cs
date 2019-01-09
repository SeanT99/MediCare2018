using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Web.HttpContext;
using System.Security.Cryptography;
using System.Text;
using System.IO;


public partial class Appointment_OnlineAppt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tbPatientID.Text = Session["LoggedIn"].ToString();
    }


    protected void buttonApptConfirm_Click(object sender, EventArgs e)
    {
        string apptTiming = ddlApptTime.Text;
        string apptDate = ddlApptDate.Text;
        Response.Redirect("CheckoutPayment.aspx?Parameter=" + apptTiming + "&Parameter2=" + apptDate);



        //    PatientPayment patientPayment = new PatientPayment();

        //    PatientPayment patient = patientPayment.getPatientPayment("mfjenfed");

        //using (AesManaged myAes = new AesManaged())
        //{
        //    string roundtrip = DecryptStringFromBytes_Aes(patient.CreditcardNo, myAes.Key, myAes.IV);
        //    Current.Response.Write("<script>alert('" + roundtrip + "');</script>");
        //}

    }

   
}