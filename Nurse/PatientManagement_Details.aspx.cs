using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Nurse_PatientManagement_Details : System.Web.UI.Page
{
    PatientInfo x = new PatientInfo();
    PasswordUtility passUtil = new PasswordUtility();
    string id = "";
    MailUtilities mail = new MailUtilities();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Call the patient retrieval method
        id = Request.QueryString["ID"].ToString();
        x = x.PatientInfoGet(id);

        given_NameLBL.Text = x.Given_Name;
        family_NameLBL.Text = x.Family_Name;
        dobLBL.Text = x.Dob;
        genderLBL.Text = x.Gender;
        idTypeLBL.Text = x.Id_Type;
        idLBL.Text = x.Id;
        emailLBL.Text = x.Email;
        mobileLBL.Text = x.MobileNumber;
        homeLBL.Text = x.HomeNumber;
        blkLBL.Text = x.Address_blk;
        streetLBL.Text = x.Address_street;
        unitLBL.Text = x.Address_unit;
        buildingLBL.Text = x.Address_building;
        postalLBL.Text = x.Address_postal;
        ecNameLBL.Text = x.Kin_name;
        ecContactLBL.Text = x.Kin_contact;
        ecRelationshipLBL.Text = x.Kin_relationship;
        allergyLBL.Text = x.Medical_allergies;
        historyLBL.Text = x.Medical_history;

    }

    protected void PwResetBtn_Click(object sender, EventArgs e)
    {
        // generate the password
        int length = 10;
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        string newPw =  res.ToString();

        // hash the password
        string[] passHash =  passUtil.generateHash(id, newPw);
        //hashDetail = new string[] { saltStr, hashStr };

        //Retrieve account status
        PatientInfo LoginInfo = new PatientInfo();
        PatientInfo GetUserAccountStatus = LoginInfo.GetLoginDetails(x.Id);

        String accountstatus = GetUserAccountStatus.Accountstatus;
        String toChangePw = GetUserAccountStatus.Tochangepw;
        String change = "";

        if (GetUserAccountStatus.Tochangepw.Trim() == "TRUE")
        {
            change = "TRUE";
        }
        else
        {
            change = "MCP";
        }
        // update database
        int result = passUtil.ResetPassword(id, passHash[0], passHash[1], change);
        if (result > 0)
        {
            if (GetUserAccountStatus.Accountstatus == "LOCKED    ")
            {
                result = mail.sendUnblockEmail(x.Email, x.Given_Name, newPw);
            }
            // send the email
            else
            {
                result = mail.sendNurseReset(x.Email, x.Given_Name, newPw);
            }
           
            if (!(result > 0))
            {
                //alert mail failed check connection and try again (need to reset again )
                Response.Write("<script>alert('Please check your connection and try again. The password has to be reset again before patient can login again.');</script>");

            }
            else if (result > 0)
            {
                //success message
                Response.Write("<script>alert('Password reset successful');</script>");
            }
        }
        else
        {
            //Fail message
            Response.Write("<script>alert('Password reset NOT successful');</script>");
        }



    }

    protected void DeleteBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("PatientManagement_List_DeleteAuth.aspx?id=" + id);
    }

    protected void EditBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("PatientManagement_List_EditPatient.aspx?id=" + id);
    }
}