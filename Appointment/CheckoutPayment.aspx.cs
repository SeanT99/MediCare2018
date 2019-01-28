using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using static System.Web.HttpContext;
using System.Text.RegularExpressions;

public partial class Appointment_CheckoutPayment : System.Web.UI.Page
{
    AesCryptoServiceProvider crypt_provider = new AesCryptoServiceProvider();
    PatientPayment patient = new PatientPayment();
    String creditcarddetails = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            confirmationTiming.Text = Session["apptTiming"].ToString();
            confirmationDate.Text = Session["apptDate"].ToString();

            patient = patient.GetPatientCreditCardDetails(Session["LoggedIn"].ToString());

        if (patient != null)
            {
                cardholdername_tb.Text = patient.CardHolderName;
                creditNo_tb.Text = decrypt(patient.CreditcardNo, patient.Key, patient.Iv);
                //creditNo_tb.Text = string.Format("************{0}", creditNo_tb.Text.Trim().Substring(12, 4));

            }  

        }



    }

    protected void buttonApptCancel_Click(object sender, EventArgs e)
    {

        Response.Redirect("OnlineAppt.aspx");

        //PatientPayment patientPayment = new PatientPayment();

        //PatientPayment patient = new PatientPayment();

        //patient = patientPayment.getPatientPayment("mfjenfed");

        //decrypt(patient.CreditcardNo, patient.Key, patient.Iv);

        //Current.Response.Write("<script>alert('" + decrypt(patient.CreditcardNo, patient.Key, patient.Iv) + "');</script>");

    }



    protected void buttonPaymentConfirm_Click(object sender, EventArgs e)
    {
        string patientID = HttpContext.Current.Session["LoggedIn"].ToString();
        string apptTiming = confirmationTiming.Text;
        string apptDate = confirmationDate.Text;
        string paymentPrice = fee_lbl.Text;
        string cardHolderName = cardholdername_tb.Text;
        string creditcardNo = creditNo_tb.Text;
        string expiryMonth = expiryDateMM_tb.Text;
        string expiryYear = expiryDateYY_tb.Text;
        string expiryDate = expiryMonth + "/" + expiryYear;
        int result = 0;
        crypt_provider.BlockSize = 128;
        crypt_provider.KeySize = 256;
        crypt_provider.GenerateIV();
        crypt_provider.GenerateKey();
        crypt_provider.Mode = CipherMode.CBC;
        crypt_provider.Padding = PaddingMode.PKCS7;

        string strKey = Convert.ToBase64String(crypt_provider.Key);

        string strIV = Convert.ToBase64String(crypt_provider.IV);

        string encrypted = encrypt(creditcardNo, strKey, strIV);


        //Current.Response.Write("<script>alert('" + encrypt(creditcardNo, strKey, strIV) + "');</script>");

        //string decrypted = decrypt(encrypted);
        //Current.Response.Write("<script>alert('" + decrypted + "');</script>");

        PatientPayment payment = new PatientPayment(patientID, paymentPrice, cardHolderName, encrypted, expiryDate, strKey, strIV);
        PatientAppt appt = new PatientAppt(patientID, apptTiming, apptDate);

        if (Mod10Check(creditcardNo) == true)
        {
            result = appt.PatientApptInsert();
            if (result == 1)
            {
                payment.PatientPaymentInsert();
            }
        }

        else
        {
            lblResult.Text = "Credit card is invalid";
        }

    }

    public String encrypt(String clear_text, String key, String iv)
    {

        crypt_provider.BlockSize = 128;
        crypt_provider.KeySize = 256;
        crypt_provider.Mode = CipherMode.CBC;
        crypt_provider.Padding = PaddingMode.PKCS7;

        crypt_provider.Key = Convert.FromBase64String(key);

        crypt_provider.IV = Convert.FromBase64String(iv);

        ICryptoTransform transform = crypt_provider.CreateEncryptor();

        byte[] encrypted_bytes = transform.TransformFinalBlock(ASCIIEncoding.ASCII.GetBytes(clear_text), 0, clear_text.Length);

        //encrypted bytes to string
        string str = Convert.ToBase64String(encrypted_bytes);

        return str;
    }

    public String decrypt(String cipher_text, string key, string iv)
    {
        crypt_provider.BlockSize = 128;
        crypt_provider.KeySize = 256;
        crypt_provider.Mode = CipherMode.CBC;
        crypt_provider.Padding = PaddingMode.PKCS7;

        crypt_provider.Key = Convert.FromBase64String(key);

        crypt_provider.IV = Convert.FromBase64String(iv);

        ICryptoTransform transform = crypt_provider.CreateDecryptor();

        byte[] enc_bytes = Convert.FromBase64String(cipher_text);

        byte[] decrypted_bytes = transform.TransformFinalBlock(enc_bytes, 0, enc_bytes.Length);

        string str = ASCIIEncoding.ASCII.GetString(decrypted_bytes);

        return str;
    }

    

    public static bool Mod10Check(string creditcardNo)
    {

        //// check whether input string is null or empty
        if (string.IsNullOrEmpty(creditcardNo))
        {
            return false;
        }

        //// 1.	Starting with the check digit double the value of every other digit 
        //// 2.	If doubling of a number results in a two digits number, add up
        ///   the digits to get a single digit number. This will results in eight single digit numbers                    
        //// 3. Get the sum of the digits
        int sumOfDigits = creditcardNo.Where((e) => e >= '0' && e <= '9')
                        .Reverse()
                        .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                        .Sum((e) => e / 10 + e % 10);


        //// If the final sum is divisible by 10, then the credit card number
        //   is valid. If it is not divisible by 10, the number is invalid.            
        return sumOfDigits % 10 == 0;

    }

}