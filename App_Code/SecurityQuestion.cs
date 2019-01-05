using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//imports needed for db
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

/// <summary>
/// Summary description for SecurityQuestion
/// </summary>
public class SecurityQuestion
{
    //database connection string
    string _connStr = ConfigurationManager.ConnectionStrings["MediCareContext"].ConnectionString;

    string sec_qn1 = "";
    string sec_ans1 = "";
    string sec_qn2 = "";
    string sec_ans2 = "";
    string sec_qn3 = "";
    string sec_ans3 = "";

    public string Sec_qn1 { get => sec_qn1; set => sec_qn1 = value; }
    public string Sec_ans1 { get => sec_ans1; set => sec_ans1 = value; }
    public string Sec_qn2 { get => sec_qn2; set => sec_qn2 = value; }
    public string Sec_ans2 { get => sec_ans2; set => sec_ans2 = value; }
    public string Sec_qn3 { get => sec_qn3; set => sec_qn3 = value; }
    public string Sec_ans3 { get => sec_ans3; set => sec_ans3 = value; }

    public SecurityQuestion()
    {
    }

    public SecurityQuestion(string sec_qn1, string sec_ans1, string sec_qn2, string sec_ans2, string sec_qn3, string sec_ans3)
    {
        this.sec_qn1 = sec_qn1;
        this.sec_ans1 = sec_ans1;
        this.sec_qn2 = sec_qn2;
        this.sec_ans2 = sec_ans2;
        this.sec_qn3 = sec_qn3;
        this.sec_ans3 = sec_ans3;
    }

    //retrieve user's security questions and ans
    public SecurityQuestion SecurityQuestionGet(string qid)
    {
        SecurityQuestion x = null;

        //strings for the object creation
        string sec_qn1, sec_ans1, sec_qn2, sec_ans2, sec_qn3, sec_ans3;

        //query string
        string queryStr = "SELECT sec_qn1, sec_ans1, sec_qn2, sec_ans2, sec_qn3, sec_ans3 FROM PatientInfo WHERE id = @id";

        //open connections, insert param and execute query
        SqlConnection conn = new SqlConnection(_connStr);
        SqlCommand cmd = new SqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@id", qid);

        try
        {
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                //store the data into object
                
                sec_qn1 = dr["sec_qn1"].ToString();
                sec_ans1 = dr["sec_ans1"].ToString();
                sec_qn2 = dr["sec_qn2"].ToString();
                sec_ans2 = dr["sec_ans2"].ToString();
                sec_qn3 = dr["sec_qn3"].ToString();
                sec_ans3 = dr["sec_ans3"].ToString();
                

                x = new SecurityQuestion(sec_qn1, sec_ans1, sec_qn2, sec_ans2, sec_qn3, sec_ans3);
            }

            //close connecetions
            conn.Close();
            dr.Close();
            dr.Dispose();
        }
        catch (SqlException e)
        {
            Debug.Write(e);
        }

        return x;
    }
}