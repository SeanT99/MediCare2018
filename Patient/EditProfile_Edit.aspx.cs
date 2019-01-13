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
        if (!IsPostBack)
        {
            //Call the patient retrieval method
            id = HttpContext.Current.Session["LoggedIn"].ToString();
            x = x.PatientInfoGet(id);

            //non-editable
            given_NameLBL.Text = x.Given_Name.TrimEnd();
            family_NameLBL.Text = x.Family_Name.TrimEnd();
            dobLBL.Text = x.Dob.TrimEnd();
            genderLBL.Text = x.Gender.TrimEnd();
            idTypeLBL.Text = x.Id_Type.TrimEnd();
            idLBL.Text = x.Id.TrimEnd();

            //editable
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
    }



    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        //create the patient object
        string email = emailTB.Text;
        string mobileNumber = mobileTB.Text;
        string homeNumber = homeTB.Text;

        string address_blk = blkTB.Text;
        string address_street = streetTB.Text;
        string address_unit = unitTB.Text;
        string address_building = buildingTB.Text;
        string address_postal = postalTB.Text;
        string kin_name = ecNameTB.Text;
        string kin_contact = ecNumberTB.Text;
        string kin_relationship = ecRelationshipTB.Text;
        string medical_allergies = allergyTB.Text;
        string medical_history = medHistTB.Text;

        PatientInfo y = new PatientInfo(id, email, mobileNumber, homeNumber, address_blk, address_street, address_unit, address_building, address_postal, kin_name, kin_contact, kin_relationship, medical_allergies, medical_history);

        //UPDATE DB
        int result = y.updatePatientInfo();
        if (result > 0)
        {
            Response.Redirect("../Patient/EditProfile_View.aspx", false);//TODO change to the patient display pg
        }

    }



}