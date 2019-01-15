using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

//imports needed for db
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Text;

/// <summary>
/// Summary description for OTP
/// </summary>
public class OTP
{
    string patientID;
    string otp;

    //database connection string
    readonly string _connStr = ConfigurationManager.ConnectionStrings["MediCareContext"].ConnectionString;

    public string PatientID { get => patientID; set => patientID = value; }
    public string Otp { get => otp; set => otp = value; }


    public OTP()
    {
    }

    public OTP(string patientID, string otp)
    {
        this.patientID = patientID;
        this.otp = otp;
    }

    public int insertOTP()
    {
        int result = 0;

        SqlConnection myConn = new SqlConnection(_connStr);
        myConn.Open();

        try
        {
            result = setUsed(this.patientID); // ensure that all OTPs are set to used first.

            string insertOTP = "INSERT INTO OTP (otp_id, patientID, otp_no, otp_timestamp, otp_used) VALUES (newid(), @username, @otpno, GETDATE(), 0)";
            SqlCommand cmd = new SqlCommand(insertOTP, myConn);
            cmd.Parameters.AddWithValue("@username", this.patientID);
            cmd.Parameters.AddWithValue("@otpno", this.otp);
            result = cmd.ExecuteNonQuery();

            myConn.Close();
        }
        catch(SqlException e)
        {
            Debug.Write(e);
        }
        return result;
    }

    public int setUsed(string username)
    {
        int result = 0;
        SqlConnection myConn = new SqlConnection(_connStr);
        myConn.Open();

        string updateOTP = "UPDATE OTP SET OTP_used=1 WHERE patientID=@username";
        SqlCommand cmd = new SqlCommand(updateOTP, myConn);
        cmd.Parameters.AddWithValue("@username", username);
        try
        {
            result = cmd.ExecuteNonQuery();

            myConn.Close();
        }
        catch (SqlException e)
        {
            Debug.Write(e);
        }

        return result;
    }

    public string genOTP()
    {
        int length = 6;
        const string valid = "1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }



}



/*
 * CREATE TABLE [dbo].[OTP] (
    [otp_id]        INT          NOT NULL,
    [patientID]     VARCHAR (20) NOT NULL,
    [otp_no]        NCHAR (10)   NOT NULL,
    [otp_timestamp] NCHAR (10)   NOT NULL,
    [otp_used]      NCHAR (10)   NOT NULL,
    PRIMARY KEY CLUSTERED ([otp_id] ASC)
);

*/