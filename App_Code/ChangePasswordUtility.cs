using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//imports needed for db
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

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
    string old_salt;

    public ChangePasswordUtility()
    {

    }

    public string Id { get => id; set => id = value; }
    public string Old_password { get => old_password; set => old_password = value; }
    public string Time_Password_changed { get => time_Password_changed; set => time_Password_changed = value; }
    public string Date_password_changed { get => date_password_changed; set => date_password_changed = value; }
    public string Old_salt { get => old_salt; set => old_salt = value; }

    public ChangePasswordUtility(string id, string old_password, string time_Password_changed, string date_password_changed, string oldsalt)
    {
        this.id = id;
        this.old_password = old_password;
        this.time_Password_changed = time_Password_changed;
        this.date_password_changed = date_password_changed;
        this.old_salt = oldsalt;
        
    }


    //DB Method
    public int PatientInsertOldPassword(string username, string oldpassword, string oldsalt)
    {
        int result = 0;

        //create the query "template" string
        string queryStr = "INSERT INTO Password(Id,Old_Password,Time_Password_Changed,Date_Password_Changed,Old_Salt)" +
            "VALUES (@Id,@Old_Password,@Time_Password_Changed,@Date_Password_Changed,@Old_Salt)";

        SqlConnection conn = new SqlConnection(_connStr);
        SqlCommand cmd = new SqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@Id", username);
        cmd.Parameters.AddWithValue("@Old_Password", oldpassword);
        cmd.Parameters.AddWithValue("@Time_Password_Changed", "1/11/2019");
        cmd.Parameters.AddWithValue("@Date_Password_Changed", "1/11/2019");
        cmd.Parameters.AddWithValue("@Old_Salt", oldsalt);



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


    public List<ChangePasswordUtility> GetOldPasswordDetails(string LoginNRIC)
    {

        List<ChangePasswordUtility> SpecificUserDetails = new List<ChangePasswordUtility>();
        //strings for the object creation

        //query string
        string queryStr = "SELECT Id,Old_Password,Time_Password_Changed,Date_Password_Changed,Old_Salt FROM Password WHERE Id = @LoginNRIC";

        SqlConnection conn = new SqlConnection(_connStr);
        SqlCommand cmd = new SqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@LoginNRIC", LoginNRIC);
        try
        {
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                id = dr["Id"].ToString();
                string Old_Password = dr["Old_Password"].ToString();
                string Time_Password_Changed = dr["Time_Password_Changed"].ToString();
                string Date_Password_Changed = dr["Date_Password_Changed"].ToString();
                string OldSalt = dr["Old_Salt"].ToString();

                ChangePasswordUtility x = new ChangePasswordUtility(id, Old_Password, Time_Password_Changed, Date_Password_Changed,OldSalt);
                SpecificUserDetails.Add(x);
            }

            conn.Close();
            dr.Close();
            dr.Dispose();
        }
        catch (SqlException e)
        {
            Debug.Write(e);
        }
        return SpecificUserDetails;
    }

    public int updateAccountPassword(string LoginNRIC, string NewPasswordHash, string salt)
    {
        int result = 0;


        //id, email, mobileNumber, homeNumber, address_blk, address_street, address_unit, address_building, address_postal, kin_name, kin_contact, kin_relationship, medical_allergies, medical_history

        string queryStr = "UPDATE PatientInfo SET login_password = @login_password,salt = @salt WHERE id=@LoginNRIC";

        //open connections
        SqlConnection conn = new SqlConnection(_connStr);
        SqlCommand cmd = new SqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@LoginNRIC", LoginNRIC);
        cmd.Parameters.AddWithValue("@login_password", NewPasswordHash);
        cmd.Parameters.AddWithValue("@salt", salt);

        try
        {
            conn.Open();
            result += cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception e)
        {
            Debug.Write(e);
        }

        return result;
    }

    public int UpdatePasswordTableWithNewDataAfterUserChangedPassword(string username, string currentpassword, string salt)
    {
        int result = 0;

        //create the query "template" string
        string queryStr = "INSERT INTO Password(Id,Old_Password,Time_Password_Changed,Date_Password_Changed,Old_Salt)" +
            "VALUES (@Id,@Old_Password,@Time_Password_Changed,@Date_Password_Changed,@Old_Salt)";

        SqlConnection conn = new SqlConnection(_connStr);
        SqlCommand cmd = new SqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@Id", username);
        cmd.Parameters.AddWithValue("@Old_Password", currentpassword);
        cmd.Parameters.AddWithValue("@Time_Password_Changed", "1/11/2019");
        cmd.Parameters.AddWithValue("@Date_Password_Changed", "1/11/2019");
        cmd.Parameters.AddWithValue("@Old_Salt", salt);


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

    public string generateHashWithOrignalSalt(string username, string password, string salt)
    {
        string NewPasswordWithOrignalSalt = "";
        string OldSaltPlusNewPassword = username + password;
        byte[] array = Convert.FromBase64String(salt);
        //2. concatenate the plaintext to the salt and hash it (using PBKDF2)
        var pbkdf2 = new Rfc2898DeriveBytes(OldSaltPlusNewPassword, array, 10000);

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
        NewPasswordWithOrignalSalt = Convert.ToBase64String(hashBytes);
        return NewPasswordWithOrignalSalt;
    }


  









}

