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
    string NotYetChangedPassword = "You Have Not Changed Your First Time Login Password, Please Login And Change Your Password First Before Trying To Change Your Password";
    string CannotChangeDueToLoginAttempt = "You Are Not Allowed To Change Your Password If You Have Failed Login Attempt, Please Login Succesfully And Try Again";
    string YourAccountIsLocked = "Your Account Is Locked, You Are Not Allowed To Change Your Password, Please Contact Our Adminstrator For Assistance";


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

            EmailAddressDoNotExistLabel.Visible = true;
        }
        else if (AllEmailContact[0].Tochangepw.Trim() == "TRUE")
        {

            Response.Write("<script>alert('You Have Not Changed Your First Time Login Password, Please Login And Change Your Password First Before Trying To Change Your Password');location.href='Login.aspx?id=" + "';</script>");


        }
        else if (AllEmailContact[0].Accountstatus.Trim() == "LOCKED" && AllEmailContact[0].Loginattempts > 5)
        {

            Response.Write("<script>alert(' Your Account Is Locked, You Are Not Allowed To Change Your Password, Please Contact Our Adminstrator For Assistance');location.href='Login.aspx?id=" + "';</script>");

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

            string msg = "This is your medicare portal OTP " + otp;
            //send otp and change pw sms
            MailUtilities sendPasswordRequest = new MailUtilities();
            sendPasswordRequest.sendChangePasswordMail(SpecificPatientName.Email, FamilyAndGivenName, otp);
           // sendPasswordRequest.sendOTP(mobile, msg);


            Response.Redirect("ForgetPasswordEmailConfirmation.aspx?EnteredID="+EnteredEmail, false);


        }

    }
}