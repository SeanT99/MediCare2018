using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_EditProfile_Auth : System.Web.UI.Page
{
    SecurityQuestion q = new SecurityQuestion();
    String question1, question2, ans1, ans2;
    string[] ran;

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
                if (ans1 != null || ans1 != "")
                {
                    //retrieve the security qns and answer
                    q = q.SecurityQuestionGet(HttpContext.Current.Session["LoggedIn"].ToString());
                    Q1Lbl.Text = q.Sec_qn1;
                    Q2Lbl.Text = q.Sec_qn2;
                    ans1 = q.Sec_ans1.ToUpper();
                    ans2 = q.Sec_ans2.ToUpper();
                }
            }
        }
        else
        {
            Response.Redirect("../Login/Login.aspx", false);
        }
    }

    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
        //bool pass = false;


        //check the ans
        if (Ans1TB.Text.ToUpper() == ans1 && Ans2TB.Text.ToUpper() == ans2)
            //pass = true;
            //link to edit page
            Response.Redirect("/Patient/EditProfile_Edit.aspx");

        //prompt ans wrong alert
        else
            Response.Write("<script>alert('One/more of the answer(s) is wrong');</script>");
    }

}