using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

/// <summary>
/// Summary description for PasswordUtility
/// </summary>
public class PasswordUtility
{
    public PasswordUtility()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string[] generateHash(string username, string password)
    {
        string[] hashDetail = null;
        string saltStr = "";
        string hashStr = "";

        //add the username to the password then hash the summed string
        string tohash = username + password;

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