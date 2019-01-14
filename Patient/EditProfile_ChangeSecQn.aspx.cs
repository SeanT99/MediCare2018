using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_EditProfile_ChangeSecQn : System.Web.UI.Page
{
    string id = "";
    MailUtilities mail = new MailUtilities();

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
                id = HttpContext.Current.Session["LoggedIn"].ToString();
            }
        }
        else
        {
            Response.Redirect("../Login/Login.aspx", false);
        }

    }

    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
        string q1, q2, q3;
        q1 = sq1DDL.SelectedItem.Text.ToUpper();
        q2 = sq2DDL.SelectedItem.Text.ToUpper();
        q3 = sq3DDL.SelectedItem.Text.ToUpper();

        //submit the security questions to database
        SecurityQuestion x = new SecurityQuestion(q1, sqAns1TB.Text.ToUpper(), q2, sqAns2TB.Text.ToUpper(), q3, sqAns3TB.Text.ToUpper());
        int result = x.SecurityQuestionUpdate(id);

        if (result > 0)
        {
            // retrieve user email /name
            string[] email = mail.getPatientMailDetails(id);

            // send email to the user 
            mail.sendSecQnChanged(email[0], email[1]);

            //success message
            Response.Write("<script>alert('Security questions updated successfully');location.href='../Appointment/OnlineAppt.aspx';</script>");
        }
    }
}