﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Web.HttpContext;
using System.Security.Cryptography;
using System.Text;
using System.IO;


public partial class Appointment_OnlineAppt : System.Web.UI.Page
{
    Boolean valid = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        //check if is patient session
        if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
        {
            if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
            {
                Response.Redirect("../Login/Login.aspx", false);
            }
            else
            {
                tbPatientID.Text = Session["LoggedIn"].ToString();
            }
        }
        else
        {
            Response.Redirect("../Login/Login.aspx", false);
        }

        
    }


    protected void buttonApptConfirm_Click(object sender, EventArgs e)
    {
        string apptTiming = ddlApptTime.Text;
        string apptDate = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
        //string apptDate = apptDate_tb.Text;

        string userId = Session["LoggedIn"].ToString();
        //Current.Response.Write("<script>alert('" + Session["LoggedIn"].ToString() + "');</script>");

        PatientAppt patientApptCheck = new PatientAppt();

        if (apptDate == "01/01/0001")
        {
            Current.Response.Write("<script>alert('You have not select a booking appointment date. Please select a date');</script>");
        }

        else if (patientApptCheck.checkPatientDate(userId, apptDate) != null)
        {
            Current.Response.Write("<script>alert('You have a booking appointment on this date. Please select another date');</script>");
        }

        else
        {
            Session["apptTiming"] = apptTiming;
            Session["apptDate"] = apptDate;

            Response.Redirect("CheckoutPayment.aspx");


        }



        //    PatientPayment patientPayment = new PatientPayment();

        //    PatientPayment patient = patientPayment.getPatientPayment("mfjenfed");

        //using (AesManaged myAes = new AesManaged())
        //{
        //    string roundtrip = DecryptStringFromBytes_Aes(patient.CreditcardNo, myAes.Key, myAes.IV);
        //    Current.Response.Write("<script>alert('" + roundtrip + "');</script>");
        //}

    }

    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        if (e.Day.Date.CompareTo(DateTime.Today) < 0)
        {
            e.Day.IsSelectable = false;
        }
    }


}