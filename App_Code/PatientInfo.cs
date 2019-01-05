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
/// Class for the patient's information
/// </summary>
public class PatientInfo
{
    //database connection string
    string _connStr = ConfigurationManager.ConnectionStrings["MediCareContext"].ConnectionString;

    //variables for the patient (for the registration)
    private string id;
    private string id_Type;
    private string family_Name;
    private string given_Name;
    private string gender;
    private string dob;
    private string email;
    private string mobileNumber;
    private string homeNumber;
    private string address_blk;
    private string address_street;
    private string address_unit;
    private string address_building;
    private string address_postal;
    private string kin_name;
    private string kin_contact;
    private string kin_relationship;
    private string medical_allergies;
    private string medical_history;
    private string login_password;
    private string sec_qn1;
    private string sec_ans1;
    private string sec_qn2;
    private string sec_ans2;
    private string sec_qn3;
    private string sec_ans3;
    private string acctype;//jj
    private string salt;//jj

    //variables for patient listing table
    private string emergency_contact;

    public string Id { get => id; set => id = value; }
    public string Id_Type { get => id_Type; set => id_Type = value; }
    public string Family_Name { get => family_Name; set => family_Name = value; }
    public string Given_Name { get => given_Name; set => given_Name = value; }
    public string Gender { get => gender; set => gender = value; }
    public string Dob { get => dob; set => dob = value; }
    public string MobileNumber { get => mobileNumber; set => mobileNumber = value; }
    public string HomeNumber { get => homeNumber; set => homeNumber = value; }
    public string Address_blk { get => address_blk; set => address_blk = value; }
    public string Address_street { get => address_street; set => address_street = value; }
    public string Address_unit { get => address_unit; set => address_unit = value; }
    public string Address_building { get => address_building; set => address_building = value; }
    public string Address_postal { get => address_postal; set => address_postal = value; }
    public string Kin_name { get => kin_name; set => kin_name = value; }
    public string Kin_contact { get => kin_contact; set => kin_contact = value; }
    public string Kin_relationship { get => kin_relationship; set => kin_relationship = value; }
    public string Medical_allergies { get => medical_allergies; set => medical_allergies = value; }
    public string Medical_history { get => medical_history; set => medical_history = value; }
    public string Login_password { get => login_password; set => login_password = value; }
    public string Sec_qn1 { get => sec_qn1; set => sec_qn1 = value; }
    public string Sec_ans1 { get => sec_ans1; set => sec_ans1 = value; }
    public string Sec_qn2 { get => sec_qn2; set => sec_qn2 = value; }
    public string Sec_ans2 { get => sec_ans2; set => sec_ans2 = value; }
    public string Sec_qn3 { get => sec_qn3; set => sec_qn3 = value; }
    public string Sec_ans3 { get => sec_ans3; set => sec_ans3 = value; }
    public string Email { get => email; set => email = value; }
    public string Acctype { get => acctype; set => acctype = value; }
    public string Salt { get => salt; set => salt = value; }

    public PatientInfo() {}

    public PatientInfo(string id, string id_Type, string family_Name, string given_Name, string gender, string dob, string email, string mobileNumber, string homeNumber, string address_blk, string address_street, string address_unit, string address_building, string address_postal, string kin_name, string kin_contact, string kin_relationship, string medical_allergies, string medical_history, string login_password, string sec_qn1, string sec_ans1, string sec_qn2, string sec_ans2, string sec_qn3, string sec_ans3, string salt)
    {
        this.id = id;
        this.id_Type = id_Type;
        this.family_Name = family_Name;
        this.given_Name = given_Name;
        this.gender = gender;
        this.dob = dob;
        this.email = email;
        this.mobileNumber = mobileNumber;
        this.homeNumber = homeNumber;
        this.address_blk = address_blk;
        this.address_street = address_street;
        this.address_unit = address_unit;
        this.address_building = address_building;
        this.address_postal = address_postal;
        this.kin_name = kin_name;
        this.kin_contact = kin_contact;
        this.kin_relationship = kin_relationship;
        this.medical_allergies = medical_allergies;
        this.medical_history = medical_history;
        this.login_password = login_password;
        this.sec_qn1 = sec_qn1;
        this.sec_ans1 = sec_ans1;
        this.sec_qn2 = sec_qn2;
        this.sec_ans2 = sec_ans2;
        this.sec_qn3 = sec_qn3;
        this.sec_ans3 = sec_ans3;
        this.salt = salt;
    }

    //the listing constructor
    public PatientInfo(string id, string family_Name, string given_Name, string gender, string mobileNumber, string medical_allergies,string emergency_contact)
    {
        this.id = id;
        this.family_Name = family_Name;
        this.given_Name = given_Name;
        this.gender = gender;
        this.mobileNumber = mobileNumber;
        this.medical_allergies = medical_allergies;
        this.kin_contact = emergency_contact;
    }
    //details constructor
    public PatientInfo(string id, string id_Type, string family_Name, string given_Name, string gender, string dob, string email, string mobileNumber, string homeNumber, string address_blk, string address_street, string address_unit, string address_building, string address_postal, string kin_name, string kin_contact, string kin_relationship, string medical_allergies, string medical_history)
    {
        this.id = id;
        this.id_Type = id_Type;
        this.family_Name = family_Name;
        this.given_Name = given_Name;
        this.gender = gender;
        this.dob = dob;
        this.email = email;
        this.mobileNumber = mobileNumber;
        this.homeNumber = homeNumber;
        this.address_blk = address_blk;
        this.address_street = address_street;
        this.address_unit = address_unit;
        this.address_building = address_building;
        this.address_postal = address_postal;
        this.kin_name = kin_name;
        this.kin_contact = kin_contact;
        this.kin_relationship = kin_relationship;
        this.medical_allergies = medical_allergies;
        this.medical_history = medical_history;
    }

    //login constructor -- jj
    public PatientInfo(string id, string login_password, string salt, string acctype)
    {
        this.id = id;
        this.login_password = login_password;
        this.salt = salt;
        this.acctype = acctype;
    }
    
    //the patientListRetrieve method
    public List<PatientInfo> PatientListGet()
    {
        List<PatientInfo> patients = new List<PatientInfo>();

        string id, family_Name, given_Name, gender, mobileNumber, medical_allergies, kin_contact;
    
        string queryStr = "SELECT * FROM PatientInfo WHERE acctype = \'PATIENT   \' ORDER BY family_Name";

        SqlConnection conn = new SqlConnection(_connStr);
        SqlCommand cmd = new SqlCommand(queryStr, conn);
        try
        {
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                id = dr["id"].ToString();
                family_Name = dr["family_Name"].ToString();
                given_Name = dr["given_Name"].ToString();
                gender = dr["gender"].ToString();
                mobileNumber = dr["mobileNumber"].ToString();
                medical_allergies = dr["medical_allergies"].ToString();
                kin_contact = dr["kin_name"].ToString() + " - " + dr
                    ["kin_contact"].ToString();
                PatientInfo a = new PatientInfo(id, family_Name, given_Name, gender, mobileNumber, medical_allergies, kin_contact);
                patients.Add(a);
            }

            conn.Close();
            dr.Close();
            dr.Dispose();
        }
        catch (SqlException e) {
            Debug.Write(e);
        }
        return patients;
    }
  
    //the retrieval of a specific patient's details
    public PatientInfo PatientInfoGet(string qid)
    {
        PatientInfo x = null;

        //strings for the object creation
        string id, id_Type, family_Name, given_Name, gender, dob, email, mobileNumber, homeNumber, address_blk, address_street, address_unit, address_building, address_postal, kin_name, kin_contact, kin_relationship, medical_allergies, medical_history;

        //query string
        string queryStr = "SELECT id,id_Type,family_Name,given_Name,gender,dob,email,mobileNumber,homeNumber,address_blk,address_street,address_unit,address_building,address_postal,kin_name,kin_contact, kin_relationship,medical_allergies,medical_history FROM PatientInfo WHERE id = @id";

        //open connections, insert param and execute query
        SqlConnection conn = new SqlConnection(_connStr);
        SqlCommand cmd = new SqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@id", qid);
        try
        {
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                //store the data into object
                id = dr["id"].ToString();
                id_Type = dr["id_Type"].ToString();
                family_Name = dr["family_Name"].ToString();
                given_Name = dr["given_Name"].ToString();
                gender = dr["gender"].ToString();
                dob = dr["dob"].ToString();
                email = dr["email"].ToString();
                mobileNumber = dr["mobileNumber"].ToString();
                homeNumber = dr["homeNumber"].ToString();
                address_blk = dr["address_blk"].ToString();
                address_street = dr["address_street"].ToString();
                address_unit = dr["address_unit"].ToString();
                address_building = dr["address_building"].ToString();
                address_postal = dr["address_postal"].ToString();
                kin_name = dr["kin_name"].ToString();
                kin_contact = dr["kin_contact"].ToString();
                kin_relationship = dr["kin_relationship"].ToString();
                medical_allergies = dr["medical_allergies"].ToString();
                medical_history = dr["medical_history"].ToString();

                x = new PatientInfo(id, id_Type, family_Name, given_Name, gender, dob, email, mobileNumber, homeNumber, address_blk, address_street, address_unit, address_building, address_postal, kin_name, kin_contact, kin_relationship, medical_allergies, medical_history);
            }

            //close connecetions
            conn.Close();
            dr.Close();
            dr.Dispose();
        }
        catch(SqlException e)
        {
            Debug.Write(e);
        }


        return x;
    }

    //method to add new patient to database
    public int PatientInsert()
    {
        int result = 0;

        //create the query "template" string
        string queryStr = "INSERT INTO PatientInfo(id, id_Type, acctype, family_Name, given_Name, gender, dob, email, mobileNumber, homeNumber, address_blk, address_street, address_unit, address_building, address_postal, kin_name, kin_contact, kin_relationship, medical_allergies, medical_history, salt, login_password, sec_qn1, sec_ans1, sec_qn2, sec_ans2, sec_qn3, sec_ans3, registerDate, toChangePw)"+
            " VALUES (@id, @id_Type, @acctype, @family_Name, @given_Name, @gender, @dob, @email, @mobileNumber, @homeNumber, @address_blk, @address_street, @address_unit, @address_building, @address_postal, @kin_name, @kin_contact, @kin_relationship, @medical_allergies, @medical_history, @salt, @login_password, @sec_qn1, @sec_ans1, @sec_qn2, @sec_ans2, @sec_qn3, @sec_ans3, @registerDate, @toChangePw)";

        // @sec_qn1, @sec_ans1, @sec_qn2, @sec_ans2, @sec_qn3, @sec_ans3

        //open connections
        SqlConnection conn =  new SqlConnection(_connStr);
        SqlCommand cmd = new SqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@id", this.id);
        cmd.Parameters.AddWithValue("@id_Type",this.id_Type);
        cmd.Parameters.AddWithValue("@family_Name",this.family_Name);
        cmd.Parameters.AddWithValue("@given_Name",this.given_Name);
        cmd.Parameters.AddWithValue("@gender",this.gender);
        cmd.Parameters.AddWithValue("@dob",this.dob);
        cmd.Parameters.AddWithValue("@email", this.email);
        cmd.Parameters.AddWithValue("@mobileNumber",this.mobileNumber);
        cmd.Parameters.AddWithValue("@homeNumber",this.homeNumber);
        cmd.Parameters.AddWithValue("@address_blk",this.address_blk);
        cmd.Parameters.AddWithValue("@address_street",this.address_street);
        cmd.Parameters.AddWithValue("@address_unit",this.address_unit);
        cmd.Parameters.AddWithValue("@address_building", this.address_unit);
        cmd.Parameters.AddWithValue("@address_postal",this.address_postal);
        cmd.Parameters.AddWithValue("@kin_name",this.kin_name);
        cmd.Parameters.AddWithValue("@kin_contact",this.kin_contact);
        cmd.Parameters.AddWithValue("@kin_relationship",this.kin_relationship);
        cmd.Parameters.AddWithValue("@medical_allergies",this.medical_allergies);
        cmd.Parameters.AddWithValue("@medical_history",this.medical_history);
        cmd.Parameters.AddWithValue("@login_password",this.login_password);
        cmd.Parameters.AddWithValue("@sec_qn1",this.sec_qn1);
        cmd.Parameters.AddWithValue("@sec_ans1",this.sec_ans1);
        cmd.Parameters.AddWithValue("@sec_qn2", this.sec_qn2);
        cmd.Parameters.AddWithValue("@sec_ans2", this.sec_ans2);
        cmd.Parameters.AddWithValue("@sec_qn3", this.sec_qn3);
        cmd.Parameters.AddWithValue("@sec_ans3", this.sec_ans3);
        cmd.Parameters.AddWithValue("@accType", "PATIENT");
        cmd.Parameters.AddWithValue("@salt", this.salt);
        cmd.Parameters.AddWithValue("@toChangePw", "TRUE");
        cmd.Parameters.AddWithValue("@registerDate", DateTime.Now.ToString("d/M/yyyy"));

        try { 
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

    // patient deletion method
    public int PatientDelete(String id)
    {
        int result = 0;

        string queryStr = "DELETE FROM PatientInfo WHERE id = @id";

        //open connections
        SqlConnection conn = new SqlConnection(_connStr);
        SqlCommand cmd = new SqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@id", id);

        conn.Open();
        result += cmd.ExecuteNonQuery();
        conn.Close();

        return result;
    }

    //JJ get login account
    public PatientInfo GetLoginDetails(string LoginNRIC)
    {
        PatientInfo x = null;

        //strings for the object creation

        //query string
        string queryStr = "SELECT id,login_password,salt,acctype FROM PatientInfo WHERE id = @LoginNRIC";

        //open connections, insert param and execute query
        SqlConnection conn = new SqlConnection(_connStr);
        SqlCommand cmd = new SqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@LoginNRIC", LoginNRIC);
        conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            //store the data into object
            id = dr["id"].ToString();
            login_password = dr["login_password"].ToString();
            salt = dr["salt"].ToString();
            acctype = dr["acctype"].ToString();

            x = new PatientInfo(id, login_password, salt, acctype);
        }

        //close connecetions
        conn.Close();
        dr.Close();
        dr.Dispose();


        return x;
    }
    
    public PatientInfo PatientInfoGetAll(string qid)
    {
        PatientInfo x = null;

        //strings for the object creation
        string id, id_Type, family_Name, given_Name, gender, dob, email, mobileNumber, homeNumber, address_blk, address_street, address_unit, address_building, address_postal, kin_name, kin_contact, kin_relationship, medical_allergies, medical_history, login_password, sec_qn1, sec_ans1, sec_qn2, sec_ans2, sec_qn3, sec_ans3, salt;

        //query string
        string queryStr = "SELECT * FROM PatientInfo WHERE id = @id";

        //open connections, insert param and execute query
        SqlConnection conn = new SqlConnection(_connStr);
        SqlCommand cmd = new SqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@id", qid);

        try
        {
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                //store the data into object
                id = dr["id"].ToString();
                id_Type = dr["id_Type"].ToString();
                family_Name = dr["family_Name"].ToString();
                given_Name = dr["given_Name"].ToString();
                gender = dr["gender"].ToString();
                dob = dr["dob"].ToString();
                email = dr["email"].ToString();
                mobileNumber = dr["mobileNumber"].ToString();
                homeNumber = dr["homeNumber"].ToString();
                address_blk = dr["address_blk"].ToString();
                address_street = dr["address_street"].ToString();
                address_unit = dr["address_unit"].ToString();
                address_building = dr["address_building"].ToString();
                address_postal = dr["address_postal"].ToString();
                kin_name = dr["kin_name"].ToString();
                kin_contact = dr["kin_contact"].ToString();
                kin_relationship = dr["kin_relationship"].ToString();
                medical_allergies = dr["medical_allergies"].ToString();
                medical_history = dr["medical_history"].ToString();
                login_password = dr["login_password"].ToString();
                salt = dr["salt"].ToString();
                sec_qn1 = dr["sec_qn1"].ToString();
                sec_ans1 = dr["sec_ans1"].ToString();
                sec_qn2 = dr["sec_qn2"].ToString();
                sec_ans2 = dr["sec_ans2"].ToString();
                sec_qn3 = dr["sec_qn3"].ToString();
                sec_ans3 = dr["sec_ans3"].ToString();


                x = new PatientInfo(id, id_Type, family_Name, given_Name, gender, dob, email, mobileNumber, homeNumber, address_blk, address_street, address_unit, address_building, address_postal, kin_name, kin_contact, kin_relationship, medical_allergies, medical_history, login_password, sec_qn1, sec_ans1, sec_qn2, sec_ans2, sec_qn3, sec_ans3, salt);
            }

            //close connecetions
            conn.Close();
            dr.Close();
            dr.Dispose();
        }
        catch(SqlException e)
        {
            Debug.Write(e);
        }

        return x;
    }

} 