using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using static System.Web.HttpContext;
using System.Diagnostics;

/// <summary>
/// Summary description for PatientAppt
/// </summary>
public class PatientAppt
{
    string _connStr = ConfigurationManager.ConnectionStrings["MediCareContext"].ConnectionString;
    private String patientID;
    private String apptTiming;
    private String apptDate;

    public string PatientID { get => patientID; set => patientID = value; }
    public string ApptTiming { get => apptTiming; set => apptTiming = value; }
    public string ApptDate { get => apptDate; set => apptDate = value; }

    public PatientAppt()
    {
    }

    public PatientAppt(string patientID, string apptTiming, string apptDate)
    {
        this.patientID = patientID;
        this.ApptTiming = apptTiming;
        this.apptDate = apptDate;    
    }

    public PatientAppt(string apptTiming, string apptDate)
    {
        this.ApptTiming = apptTiming;
        this.apptDate = apptDate;
    }

    public PatientAppt getPatient(string patient_ID)
    {
        PatientAppt patientApptDetails = null;
        string patientID, apptTiming, apptDate;
        string queryStr = "SELECT * FROM PatientAppt WHERE patient = @patientID";
        SqlConnection conn = new SqlConnection(_connStr);
        SqlCommand cmd = new SqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@patientID", patient_ID);
        conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        //Check if there are any resultsets
        if (dr.Read())
        {
            patientID = dr["patientID"].ToString();
            apptTiming = dr["apptTiming"].ToString();
            apptDate = dr["apptDate"].ToString();
            patientApptDetails = new PatientAppt(patientID, apptTiming, apptDate);
        }
        else
        {
            patientApptDetails = null;
        }
        conn.Close();
        dr.Close();
        dr.Dispose();
        return patientApptDetails;
    }


    

    public int PatientApptInsert()
    {

        string msg = null;
        int result = 0;
        string queryStr = "INSERT INTO PatientAppt(patient,apptTiming, apptDate)" 
            + "values (@patientID,@apptTiming, @apptDate)";
        try {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@patientID", this.patientID);
            cmd.Parameters.AddWithValue("@apptTiming", this.apptTiming);
            cmd.Parameters.AddWithValue("@apptDate", this.apptDate);
            conn.Open(); 
            result += cmd.ExecuteNonQuery(); // Returns no. of rows affected. Must be > 0
            conn.Close();
            Current.Response.Write("<script>alert('Thanks! Your appointment has been scheduled successfully.'); window.location='/Appointment/OnlineAppt.aspx';</script>");

        }
        catch (SqlException ex)
        {
            Debug.Write(ex);
            Current.Response.Write("<script>alert('Your booking appointment is not successful. Please select the correct date.');</script>");
            return 0;
        }
        return result;

    }//end Insert
    
}