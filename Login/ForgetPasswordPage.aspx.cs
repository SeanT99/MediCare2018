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
        string EnteredEmail = EmailAddressField.Text;

        PatientInfo EmailInfo = new PatientInfo();
        List<PatientInfo> AllEmailContact = EmailInfo.GetPatientsEmail();

        PatientInfo SpecificPatientName;

        if (!EmailInfo.GetSpecificPatient(EnteredEmail).Email.Equals(EnteredEmail))
        {
            Debug.Write("No Existing Email");
        }
        else if (EmailInfo.GetSpecificPatient(EnteredEmail).Email.Equals(EnteredEmail))
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

            
            MailUtilities sendPasswordRequest = new MailUtilities();
            //send otp sms
            //sendPasswordRequest.sendOTP("98257046", "TEST"+otp);//sandra #
            sendPasswordRequest.sendOTP("93868983", "TEST"+otp); //sean

            /*
            //send otp and change pw email
            
            sendPasswordRequest.sendChangePasswordMail(SpecificPatientName.Email, FamilyAndGivenName, otp);


    */
        }

    }
}