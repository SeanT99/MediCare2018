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
        //retrieve the security qns and answer
        q = q.SecurityQuestionGet("ADMIN"); //TODO change to the current user
        //generate 2 random numbers
        ran = ranNum();

        //set the security questions to the labels
        if (ran[0] == "1")
        {
            question1 = q.Sec_qn1;
        }
        else if (ran[0] == "2")
        {
            question1 = q.Sec_qn2;
        }
        else if (ran[0] == "3")
        {
            question1 = q.Sec_qn3;
        }

        if (ran[1] == "1")
        {
            question2 = q.Sec_qn1;
        }
        else if (ran[1] == "2")
        {
            question2 = q.Sec_qn2;
        }
        else if (ran[1] == "3")
        {
            question2 = q.Sec_qn3;
        }

        
        Q1Lbl.Text = question1;
        Q2Lbl.Text = question2;

    }

    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
        //bool pass = false;

        //set the security answers to the strings
        if (ran[0] == "1")
        {
            ans1 = q.Sec_ans1;
        }
        else if (ran[0] == "2")
        {
            ans1 = q.Sec_ans2;
        }
        else if (ran[0] == "3")
        {
            ans1 = q.Sec_ans3;
        }

        if (ran[1] == "1")
        {
            ans2 = q.Sec_ans1;
        }
        else if (ran[1] == "2")
        {
            ans2 = q.Sec_ans2;
        }
        else if (ran[1] == "3")
        {
            ans2 = q.Sec_ans3;
        }

        //check the ans
        if (Ans1TB.Text == ans1 && Ans2TB.Text == ans2)
            //pass = true;
            //link to edit page
            Response.Redirect("http://www.yahoo.com"); //TODO change to the edit profile page

        //TODO prompt ans wrong alert
        else
            Response.Write("<script>alert('One/more of the answer(s) is wrong');</script>");
    }

    public string[] ranNum()
    {
        //get a value
        string valid = "123";
        string a = random(valid);
        //remove a from valid
        if (a == "1")
            valid = "23";
        else if (a == "2")
            valid = "13";
        else if (a == "3")
            valid = "12";
        //get b value
        string b = random(valid);
        
        
        string[] result = new string[] { a, b };
        return result;
    }

    public string random(string valid)
    {
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        
        res.Append(valid[rnd.Next(valid.Length)]);
        
        return res.ToString();
    }
}