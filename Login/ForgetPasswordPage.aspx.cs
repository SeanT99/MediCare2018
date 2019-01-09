using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

public partial class Login_ForgetPasswordPage : System.Web.UI.Page
{
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
            MailUtilities sendPasswordRequest = new MailUtilities();
            sendPasswordRequest.sendChangePasswordMail(SpecificPatientName.Email, FamilyAndGivenName);


        }

    }
}