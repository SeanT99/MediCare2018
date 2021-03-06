﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_EditProfile_ChangePassword : System.Web.UI.Page
{
    readonly MailUtilities mail = new MailUtilities();
    readonly PasswordUtility pwUtility = new PasswordUtility();

    protected void Page_Load(object sender, EventArgs e)
    {
        PasswordDoesNotMatchLabel.Visible = false;
        PasswordIncorrectLabel.Visible = false;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //get the patient id from the session
        string id = Session["LoggedIn"].ToString();
        //check the old password and both new match
        bool pass = passAuth(id, old_tb.Text);
        //if pass
        if (!pass)
        {
            PasswordIncorrectLabel.Visible = true;
        }
        else if (pass && (ChangePasswordField.Text == VerifyPasswordTextBox.Text))
        {
            // hash password and submit to database
            string[] passHash = pwUtility.generateHash(id, ChangePasswordField.Text);
            int result = pwUtility.ResetPassword(id, passHash[0], passHash[1], "FALSE");


            //send security alert
            string[] email = mail.getPatientMailDetails(id);
            mail.sendPasswordChanged(email[0], email[1]);

            //show success message
            Response.Write("<script>alert('Password updated successfully');location.href='../Appointment/OnlineAppt.aspx';</script>");
        }
        else if(!(ChangePasswordField.Text == VerifyPasswordTextBox.Text))
        {
            PasswordDoesNotMatchLabel.Visible = true;
        }

    }
    protected bool passAuth(string id, string Password)
    {
        bool pass = false;

        string hashStr = "";

        //add the username to the password then hash the summed string
        string ToHashUserLoginInput = id + Password;

        PatientInfo LoginInfo = new PatientInfo();
        PatientInfo UserLoginDetails = LoginInfo.GetLoginDetails(id);

        if (UserLoginDetails != null)
        {
            string originalSaltValue = UserLoginDetails.Salt;
            byte[] array = Convert.FromBase64String(originalSaltValue);


            //2. concatenate the plaintext to the salt and hash it (using PBKDF2)
            var pbkdf2 = new Rfc2898DeriveBytes(ToHashUserLoginInput, array, 10000);

            //3. store the hash 
            //place the string in the byte array
            byte[] hash = pbkdf2.GetBytes(20);

            //make new byte array to store the hashed plaintext+salt
            //why 36? cause 20 for hash 16 for salt 
            byte[] hashBytes = new byte[36];
            ////place the salt and hash in their respective places
            Array.Copy(array, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            ////4. convert the byte array to a string
            hashStr = Convert.ToBase64String(hashBytes);


            if (hashStr == UserLoginDetails.Login_password)
            {
                pass = true;
            }
        }


        return pass;
    }

}