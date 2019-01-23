using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

/// <summary>
/// Summary description for PatientPayment
/// </summary>
public class PatientPayment
{
    string _connStr = ConfigurationManager.ConnectionStrings["MediCareContext"].ConnectionString;
    private String patientID;
    private String paymentPrice;
    private String cardHolderName;
    private String creditcardNo;
    private String expiryDate;
    private int paymentID;
    private String key;
    private String iv;

    public string PatientID { get => patientID; set => patientID = value; }
    public string PaymentPrice { get => paymentPrice; set => paymentPrice = value; }
    public string CardHolderName { get => cardHolderName; set => cardHolderName = value; }
    public string CreditcardNo { get => creditcardNo; set => creditcardNo = value; }
    public string ExpiryDate { get => expiryDate; set => expiryDate = value; }
    public int PaymentID { get => paymentID; set => paymentID = value; }
    public string Key { get => key; set => key = value; }
    public string Iv { get => iv; set => iv = value; }

    public PatientPayment()
    {
    }

    public PatientPayment(string patientID, string paymentPrice, string cardHolderName, string creditcardNo, string expiryDate, string key, string iv)
    {
        this.patientID = patientID;
        this.PaymentPrice = paymentPrice;
        this.CardHolderName = cardHolderName;
        this.CreditcardNo = creditcardNo;
        this.ExpiryDate = expiryDate;
        this.key = key;
        this.iv = iv;
        //this.paymentID = paymentID;
    }

    public PatientPayment(string paymentPrice, string cardHolderName, string creditcardNo, string expiryDate, string key, string iv)
    {
        this.PaymentPrice = paymentPrice;
        this.CardHolderName = cardHolderName;
        this.CreditcardNo = creditcardNo;
        this.ExpiryDate = expiryDate;
        this.key = key;
        this.iv = iv;
        //this.paymentID = paymentID;
    }

    public PatientPayment getPatientPayment(string patient_ID)
    {
        PatientPayment patientPaymentDetails = null;
        string patientID, paymentPrice, cardHolderName, creditcardNo, expiryDate;
        string queryStr = "SELECT * FROM PatientPayment WHERE patient = @patientID";
        SqlConnection conn = new SqlConnection(_connStr);
        SqlCommand cmd = new SqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@patientID", patient_ID);
        conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        //Check if there are any resultsets
        if (dr.Read())
        {
            patientID = dr["patientID"].ToString();
            paymentPrice = dr["paymentPrice"].ToString();
            cardHolderName = dr["cardHolderName"].ToString();
            creditcardNo = dr["creditcardNo"].ToString();
            expiryDate = dr["expiryDate"].ToString();
            key = dr["strKey"].ToString();
            iv = dr["iv"].ToString();

            patientPaymentDetails = new PatientPayment(patientID, paymentPrice, cardHolderName, creditcardNo, expiryDate, key, iv);
        }
        else
        {
            patientPaymentDetails = null;
        }
        conn.Close();
        dr.Close();
        dr.Dispose();
        return patientPaymentDetails;
    }

    public int PatientPaymentInsert()
    {
        string msg = null;
        int result = 0;
        string queryStr = "INSERT INTO PatientPayment(patientID, paymentPrice, cardHolderName, creditcardNo, expiryDate, paymentID, strKey, iv, paymentDate)"
            + "values (@patientID,@paymentPrice, @cardHolderName, @creditcardNo, @expiryDate, newid(), @key, @iv, @paymentDate)";
        try
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@patientID", this.patientID);
            cmd.Parameters.AddWithValue("@paymentPrice", this.paymentPrice);
            cmd.Parameters.AddWithValue("@cardHolderName", this.cardHolderName);
            cmd.Parameters.AddWithValue("@creditcardNo", this.creditcardNo);
            cmd.Parameters.AddWithValue("@expiryDate", this.expiryDate);
            cmd.Parameters.AddWithValue("@paymentID", this.paymentID);
            cmd.Parameters.AddWithValue("@key", this.key);
            cmd.Parameters.AddWithValue("@iv", this.iv);
            cmd.Parameters.AddWithValue("@paymentDate", DateTime.Now.ToString("d/M/yyyy"));

            conn.Open();
            result += cmd.ExecuteNonQuery(); // Returns no. of rows affected. Must be > 0
            conn.Close();
            return result;
        }
        catch (SqlException ex)
        {
            Debug.Write(ex);
            return 0;
        }
    }//end Insert


}