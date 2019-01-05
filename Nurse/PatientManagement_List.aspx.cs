using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Nurse_PatientInfoListing : System.Web.UI.Page
{   
    PatientInfo a = new PatientInfo();
    MailUtilities mail = new MailUtilities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
    }
    
    protected void bind()
    {
        List<PatientInfo> patients = new List<PatientInfo>();
        patients = a.PatientListGet();
        gvPatient.DataSource = patients;
        gvPatient.DataBind();

        if (patients.Count == 0)
            notFoundLbl.Visible = true;
       
    }

    protected void gvPatient_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Get the currently selected row.
        GridViewRow row = gvPatient.SelectedRow;
        // Get patient ID from the selected row, which is the
        // first row, i.e. index 0.
        string id = row.Cells[0].Text;
        // Redirect to next page, with the patient Id added to the URL,
        // e.g. ProductDetails.aspx?ProdID=1
        Response.Redirect("PatientManagement_Details.aspx?ID=" + id);
    }


    protected void gvPatient_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = gvPatient.DataKeys[e.RowIndex].Value.ToString();
        Response.Redirect("PatientManagement_List_DeleteAuth.aspx?id=" + id);
        
    }
}