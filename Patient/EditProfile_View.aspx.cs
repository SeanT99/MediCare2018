using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_EditProfile_View : System.Web.UI.Page
{
    PatientInfo x = new PatientInfo();
    string id = "";
    OTP o = new OTP();
    readonly MailUtilities mail = new MailUtilities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
        {
            if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
            {
                Response.Redirect("../Login/Login.aspx", false);
            }
            else
            {
                //Call the patient retrieval method
                id = HttpContext.Current.Session["LoggedIn"].ToString();
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
        }
        else
        {
            Response.Redirect("../Login/Login.aspx", false);
        }
    }

    protected void EditBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Patient/EditProfile_Auth.aspx", false);
    }

    protected void ChangeQnBtn_Click(object sender, EventArgs e)
    {
        //TODO generate the otp
        string otp = o.genOTP();
        //TODO create the otp object
        o = new OTP(id, otp);
        //TODO send otp to table
        int result = o.insertOTP();

        if (result > 0)
        {
            
            //retrieve user email /name
            string[] email = mail.getPatientMailDetails(id);
            // remove
            Debug.Write(email[0]+"\n");
            string l = email[0];
            // send otp to user
            mail.sendOTP(id,l,otp);
            // redirect to change qn pg
            Response.Redirect("/Patient/EditProfile_ChangeSecQn.aspx", false);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Patient/EditProfile_ChangePassword.aspx",false);
    }
}