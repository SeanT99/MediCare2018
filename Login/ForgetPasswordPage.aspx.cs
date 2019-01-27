using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

public partial class Login_ForgetPasswordPage : System.Web.UI.Page
{
    string id = "";
    OTP o = new OTP();
    readonly PatientInfo pat = new PatientInfo();

    readonly MailUtilities mail = new MailUtilities();

    protected void Page_Load(object sender, EventArgs e)
    {
        EmailAddressDoNotExistLabel.Visible = false;
    }



    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string EnteredEmail = EmailAddressField.Text.ToLower();

        PatientInfo EmailInfo = new PatientInfo();
        List<PatientInfo> AllEmailContact = EmailInfo.GetPatientsEmail(EnteredEmail);

        PatientInfo SpecificPatientName;

        if (AllEmailContact.Count <= 0)
        {
            Debug.Write("No Existing Email");
            EmailAddressDoNotExistLabel.Visible = true;
        }
        else if (AllEmailContact[0].Tochangepw.Trim() == "TRUE")
        {
            Response.Write("<script>alert('" + "*** PLEASE TAKE NOTE *** " + "\\r\\n" + "You Have Not Changed Your First Time Login Password, Please Login And Change Your Password First Before Trying To Change Your Password" + "\\r\\n" + "');</script>");
            Response.Redirect("Login.aspx", false);
        }
        else
        {
            SpecificPatientName = EmailInfo.GetSpecificPatientByEmail(EnteredEmail);
            String FamilyAndGivenName = SpecificPatientName.Given_Name + SpecificPatientName.Family_Name;

            id = pat.GetPatientIDByEmail(EnteredEmail);
            //TODO generate otp
            string otp = o.genOTP();
            //TODO create the otp object
            o = new OTP(id, otp);
            //TODO send otp to table
            int result = o.insertOTP();
            Debug.Write("-------" + result);

            string mobile = pat.GetPatientsMobile(id);

            string msg = "This is your medicare portal OTP " + otp ;
            //send otp and change pw email
            MailUtilities sendPasswordRequest = new MailUtilities();
            sendPasswordRequest.sendChangePasswordMail(SpecificPatientName.Email, FamilyAndGivenName, otp);
            //sendPasswordRequest.sendOTP(mobile, msg);


            Response.Redirect("ForgetPasswordEmailConfirmation.aspx", false);


        }

    }
}