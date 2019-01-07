using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_EditProfile_Edit : System.Web.UI.Page
{
    PatientInfo x = new PatientInfo();
    string id = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //Call the patient retrieval method
        id = Request.QueryString["ID"].ToString();
        x = x.PatientInfoGet(id);

        given_NameLBL.Text = x.Given_Name.TrimEnd();
        family_NameLBL.Text = x.Family_Name.TrimEnd();
        dobLBL.Text = x.Dob.TrimEnd();
        genderLBL.Text = x.Gender.TrimEnd();
        idTypeLBL.Text = x.Id_Type.TrimEnd();
        idLBL.Text = x.Id.TrimEnd();
        emailTB.Text = x.Email.TrimEnd();
        mobileTB.Text = x.MobileNumber.TrimEnd();
        homeTB.Text = x.HomeNumber.TrimEnd();
        blkTB.Text = x.Address_blk.TrimEnd();
        streetTB.Text = x.Address_street.TrimEnd();
        unitTB.Text = x.Address_unit.TrimEnd();
        buildingTB.Text = x.Address_building.TrimEnd();
        postalTB.Text = x.Address_postal.TrimEnd();
        ecNameTB.Text = x.Kin_name.TrimEnd();
        ecNumberTB.Text = x.Kin_contact.TrimEnd();
        ecRelationshipTB.Text = x.Kin_relationship.TrimEnd();
        allergyTB.Text = x.Medical_allergies.TrimEnd();
        medHistTB.Text = x.Medical_history.TrimEnd();
    }



    protected void SaveBtn_Click(object sender, EventArgs e)
    {

    }
}