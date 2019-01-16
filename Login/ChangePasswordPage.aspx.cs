using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

public partial class Login_ChangePasswordPage : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

        ChangePassUserErrorLabel.Visible = false;
        PasswordUsedPreviouslyLabel.Visible = false;
        NewPasswordDoesNotMatchLabel.Visible = false;
        AlphaNumericLabel.Visible = false;



    }

    protected void details_Click(object sender, EventArgs e)
    {
        String Username = ChangePassUsernameField.Text;
        String NewPassword = ChangePasswordField.Text;
        String VerifyNewPassword = VerifyPasswordTextBox.Text;

        PatientInfo getUserInfo = new PatientInfo();
        PasswordUtility CheckPassword = new PasswordUtility();
        ChangePasswordUtility InsertOldPassword = new ChangePasswordUtility();
        ChangePasswordUtility UpdateNewPasswordToPatientTable = new ChangePasswordUtility();
        ChangePasswordUtility UpdateNewDataToPasswordTable = new ChangePasswordUtility();
        PasswordValidator ValidatePassword = new PasswordValidator();
        List<ChangePasswordUtility> InfoNeededForUserToChangePassword = InsertOldPassword.GetOldPasswordDetails(Username);


        if (InfoNeededForUserToChangePassword.Count > 0 && ValidatePassword.IsValid(NewPassword) == true) // Check if Username exist in database, if does not, then password also cannot change
        {
            // To get new password + old salt       
            if (NewPassword.Equals(VerifyNewPassword)) //if Username exist then continue to check new password and verify password if is the same
            {
                Boolean PasswordExist = false;
                for (int i = 0; i < InfoNeededForUserToChangePassword.Count; i++) // To check if old password used
                {
                    string AllExistingOldSalt = InfoNeededForUserToChangePassword[i].Old_salt.Trim();
                    string NewPasswordHashCheckWithAllExistingOldSalt = InsertOldPassword.generateHashWithOrignalSalt(Username, NewPassword, AllExistingOldSalt.Trim());
                    string AllOldPassword = InfoNeededForUserToChangePassword[i].Old_password.Trim();
                    //Help me to get the Old Password with old salt

                    if (NewPasswordHashCheckWithAllExistingOldSalt.Trim().Equals(AllOldPassword.Trim())) // Check if NewPass + old salt = OldPass + Old Salt (Check if password is used before)
                    {
                        PasswordUsedPreviouslyLabel.Visible = true;
                        PasswordExist = true;

                        Debug.WriteLine("Password used");
                    }
                }
                if (PasswordExist == false) //If Password does not exist
                {
                    //If is new password, update patientInfo salt and login password
                    string[] GenerateNewPasswordHash = CheckPassword.generateHash(Username, NewPassword);
                    string NewSalt = GenerateNewPasswordHash[0];
                    string NewLoginPassword = GenerateNewPasswordHash[1];
                    UpdateNewPasswordToPatientTable.updateAccountPassword(Username, NewLoginPassword, NewSalt);
                    //Retrieve updated salt and password 
                    PatientInfo getUpdatedPasswordAndSalt = getUserInfo.GetLoginDetails(Username);
                    string UpdatedSalt = getUpdatedPasswordAndSalt.Salt;
                    string UpdatedPassword = getUpdatedPasswordAndSalt.Login_password;
                    UpdateNewDataToPasswordTable.UpdatePasswordTableWithNewDataAfterUserChangedPassword(Username, UpdatedPassword, UpdatedSalt);
                    Debug.WriteLine("Password not used");
                }
                PasswordExist = false;
            }
        }
        else if (ValidatePassword.IsValid(NewPassword) == false && !(NewPassword.Equals(VerifyNewPassword)))
        {
            AlphaNumericLabel.Visible = true;
            NewPasswordDoesNotMatchLabel.Visible = true;

        }
        else if (ValidatePassword.IsValid(NewPassword) == false && NewPassword.Equals(VerifyNewPassword))
        {
            AlphaNumericLabel.Visible = true;
        }
        
        if (InfoNeededForUserToChangePassword == null)//The place to show error message
        {
            ChangePassUserErrorLabel.Visible = true;

        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        String NewPassword = ChangePasswordField.Text;

        PasswordValidator x = new PasswordValidator();
        Debug.WriteLine(x.IsValid(NewPassword));

    



    }
}









