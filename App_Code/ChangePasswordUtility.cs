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
/// Summary description for ChangePasswordUtility
/// </summary>
public class ChangePasswordUtility
{
    string _connStr = ConfigurationManager.ConnectionStrings["MediCareContext"].ConnectionString;

    string id;
    string old_password;
    string time_Password_changed;
    string date_password_changed;

    public ChangePasswordUtility()
    {

    }

    public string Id { get => id; set => id = value; }
    public string Old_password { get => old_password; set => old_password = value; }
    public string Time_Password_changed { get => time_Password_changed; set => time_Password_changed = value; }
    public string Date_password_changed { get => date_password_changed; set => date_password_changed = value; }


    public ChangePasswordUtility(string id, string old_password, string time_Password_changed, string date_password_changed)
    {
        this.id = id;
        this.old_password = old_password;
        this.time_Password_changed = time_Password_changed;
        this.date_password_changed = date_password_changed;
    }




    //DB Method
    public int PatientInsertOldPassword(string username, string oldpassword)
    {
        int result = 0;

        //create the query "template" string
        string queryStr = "INSERT INTO PatientInfo(Id,Old_Password,Time_Password_Changed,Date_Password_Changed )" +
            " VALUES (@Id,@Old_Password,@Time_Password_Changed,@Date_Password_Changed)";

        // @sec_qn1, @sec_ans1, @sec_qn2, @sec_ans2, @sec_qn3, @sec_ans3

        //open connections
        SqlConnection conn = new SqlConnection(_connStr);
        SqlCommand cmd = new SqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@Id", username);
        cmd.Parameters.AddWithValue("@Old_Password", oldpassword);
        cmd.Parameters.AddWithValue("@Time_Password_Changed","1/11/2019");
        cmd.Parameters.AddWithValue("@Date_Password_Changed", "1/11/2019");


        try
        {
            conn.Open();
            result += cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch (SqlException e)
        {
            Debug.Write(e);
        }

        return result;
    }










}

