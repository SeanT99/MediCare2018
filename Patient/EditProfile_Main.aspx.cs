using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;//for string builder(generate pw)

using System.Security.Cryptography;//for hashing

using System.Text.RegularExpressions;//for regex

public partial class Nurse_Registration : System.Web.UI.Page
{
    Boolean valid = true;
    MailUtilities mail = new MailUtilities();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
        string id_Type = idDDL.SelectedItem.Text.ToUpper();

        if (valid)//to check if there are any errors in the registration form
        {
            //int result = 0;

            //variables for the patient
            string id = IDTB.Text;
            string family_Name = FNameTB.Text.ToUpper();
            string given_Name = GNameTB.Text;
            string dob = dobTB.Text;
            string gender = "";

            string email = emailTB.Text;
            string mobileNumber = mobileTB.Text;
            string homeNumber = homeTB.Text;

            string address_blk = blkTB.Text;
            string address_street = streetTB.Text;
            string address_unit = unitTB.Text;
            string address_building = buildingTB.Text;
            string address_postal = postalTB.Text;
            string kin_name = ecNameTB.Text;
            string kin_contact = ecNumberTB.Text;
            string kin_relationship = ecRelationshipTB.Text;
            string medical_allergies = allergyTB.Text;
            string medical_history = medHistTB.Text;

            string sec_qn1 = "";
            string sec_ans1 = "";
            string sec_qn2 = "";
            string sec_ans2 = "";
            string sec_qn3 = "";
            string sec_ans3 = "";

            //set gender
            if (genderRB.SelectedIndex == 0)
            {
                gender = "MALE";
            }
            else
            {
                gender = "FEMALE";
            }

            //GENERATE PASSWORD
            String rawPassword = CreatePassword(10);

            //HASHING
            //hashDetail = new string[] { saltStr, hashStr };
            string[] hashing = generateHash(id, rawPassword);
            //generateHash(id, rawPassword);
            string login_password = hashing[1];

            //execute insertion of patient account details
            PatientInfo patient = new PatientInfo(id, id_Type, family_Name, given_Name, gender, dob, email, mobileNumber, homeNumber, address_blk, address_street, address_unit, address_building, address_postal, kin_name, kin_contact, kin_relationship, medical_allergies, medical_history, login_password, sec_qn1, sec_ans1, sec_qn2, sec_ans2, sec_qn3, sec_ans3, hashing[0]);

            int result = patient.PatientInsert();
            
            if (result > 0)
            {
                //Sending out welcome email
                result = mail.sendWelcomeMail(email, given_Name, rawPassword);
                if(result > 0)
                {
                    //success message
                    Response.Write("<script>alert('New patient added successfully');location.href='PatientManagement_Details.aspx?id="+id+"';</script>");
                }
                else
                {
                    //if mail fail drop user entry in db
                    patient.PatientDelete(id);
                    Response.Write("<script>alert('New patient not added successfully --- no connection to mail');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('New patient not added successfully');</script>");
            }

            

        }

        else
         { return; }
    }



    //---------------------------------------------------VALIDATORS---------------------------------------------------------

    //Validate the dob make sure dob is before todays date
    protected void dobValid_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string enteredDob = dobTB.Text;
      
        DateTime dob = DateTime.Parse(enteredDob);
        DateTime now = DateTime.Now;

        int result = DateTime.Compare(dob, now);

        //result<0 t1 earlier than t2
        //result=0 t1 same as t2
        //result>0 t1 later than t2

        if (result > 0)
        {
            args.IsValid = false;
            valid = false;
            
        }
        else args.IsValid = true;

    }

    //for NRIC validation
    protected void cusCustom_ServerValidateNRIC(object sender, ServerValidateEventArgs e)
    {
        if (idDDL.SelectedItem.Text == "NRIC")
        {
            if (e.Value == null || e.Value == "")
            {
                e.IsValid = false;
                valid = false;
            }

            else if (isValidSgFin(e.Value))
                e.IsValid = true;
        }

        else if (e.Value == null || e.Value == "")
        {
            e.IsValid = false;
            valid = false;
        }

        else
            e.IsValid = true;
    }

    public static bool isValidSgFin(string strValueToCheck)
    {
        strValueToCheck = strValueToCheck.Trim();

        Regex objRegex = new Regex("^(s|t)[0-9]{7}[a-jz]{1}$", RegexOptions.IgnoreCase);

        if (!objRegex.IsMatch(strValueToCheck))
        {
            return false;
        }

        string strNums = strValueToCheck.Substring(1, 7);

        int intSum = 0;
        int checkDigit = 0;
        string checkChar = "";
        intSum = Convert.ToUInt16(strNums.Substring(0, 1)) * 2;
        intSum = intSum + (Convert.ToUInt16(strNums.Substring(1, 1)) * 7);
        intSum = intSum + (Convert.ToUInt16(strNums.Substring(2, 1)) * 6);
        intSum = intSum + (Convert.ToUInt16(strNums.Substring(3, 1)) * 5);
        intSum = intSum + (Convert.ToUInt16(strNums.Substring(4, 1)) * 4);
        intSum = intSum + (Convert.ToUInt16(strNums.Substring(5, 1)) * 3);
        intSum = intSum + (Convert.ToUInt16(strNums.Substring(6, 1)) * 2);

        if (strValueToCheck.Substring(0, 1).ToLower() == "t")
        {
            //prefix T
            intSum = intSum + 4;
        }

        checkDigit = 11 - (intSum % 11);

        checkChar = strValueToCheck.Substring(8, 1).ToLower();

        if (checkDigit == 1 && checkChar == "a")
        {
            return true;
        }
        else if (checkDigit == 2 && checkChar == "b")
        {
            return true;
        }
        else if (checkDigit == 3 && checkChar == "c")
        {
            return true;
        }
        else if (checkDigit == 4 && checkChar == "d")
        {
            return true;
        }
        else if (checkDigit == 5 && checkChar == "e")
        {
            return true;
        }
        else if (checkDigit == 6 && checkChar == "f")
        {
            return true;
        }
        else if (checkDigit == 7 && checkChar == "g")
        {
            return true;
        }
        else if (checkDigit == 8 && checkChar == "h")
        {
            return true;
        }
        else if (checkDigit == 9 && checkChar == "i")
        {
            return true;
        }
        else if (checkDigit == 10 && checkChar == "z")
        {
            return true;
        }
        else if (checkDigit == 11 && checkChar == "j")
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //-------------------------------password hashing-------------------------------
    public string CreatePassword(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }

    public string[] generateHash(string username, string password)
    {
        string[] hashDetail= null;
        string saltStr = "";
        string hashStr = "";

        //add the username to the password then hash the summed string
        string tohash = username+password;
        
        //hash the password

        //1. get the salt
        byte[] salt; //new byte array for salt
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);//generate the salt
        
        //2. concatenate the plaintext to the salt and hash it (using PBKDF2)
        var pbkdf2 = new Rfc2898DeriveBytes(tohash, salt, 10000);
        
        //3. store the hash 
        //place the string in the byte array
        byte[] hash = pbkdf2.GetBytes(20);
        //make new byte array to store the hashed plaintext+salt
        //why 36? cause 20 for hash 16 for salt 
        byte[] hashBytes = new byte[36];
        //place the salt and hash in their respective places
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        //4. convert the byte array to a string
        saltStr = Convert.ToBase64String(salt);
        hashStr = Convert.ToBase64String(hashBytes);

        hashDetail = new string[] { saltStr, hashStr };
        return hashDetail;
    }
    

}