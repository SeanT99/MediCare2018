using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_EditProfile_View : System.Web.UI.Page
{
    PatientInfo x = new PatientInfo();
    string id = "";

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

    protected void EditBtn_Click(object sender, EventArgs e)
    {
        //TODO redirect to editprofile_auth
    }
}