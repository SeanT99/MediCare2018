using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auditlog_Auditlog : System.Web.UI.Page
{
    SqlDataReader dataReader;
    SqlCommand cmd;
    String sql = "";

    //Connect database
    string connectionString;
    SqlConnection cnn = null;

    DataTable dtAuditLog = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        connectionString = ConfigurationManager.ConnectionStrings["MedicareContext"].ConnectionString;
        cnn = new SqlConnection(connectionString);

        if (cnn != null && cnn.State == ConnectionState.Closed)
        {
            cnn.Open();
        }


        if (!IsPostBack) {
            BindData();
        }
    }

    public void BindData()
    {
        sql = "SELECT * FROM [AuditLog]";

        cmd = new SqlCommand(sql, cnn);
        dataReader = cmd.ExecuteReader();
        dtAuditLog.Load(dataReader);
        
        Session["dtAuditLog"] = dtAuditLog;
        GridView1.DataSource = dtAuditLog;
        GridView1.DataBind();

        if (dtAuditLog.Rows.Count > 0)
        {
            btDownload.Visible = true;
        }
        else
        {
            btDownload.Visible = false;
        }
    }

    protected void btDownload_Click(object sender, EventArgs e)
    {
        if (Session["dtAuditLog"] != null)
        {
            dtAuditLog = (DataTable)Session["dtAuditLog"];
        }
       
        // creating document object
        iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(PageSize.A4);
        rec.BackgroundColor = new BaseColor(System.Drawing.Color.Olive);
        Document doc = new Document(rec);
        doc.SetPageSize(iTextSharp.text.PageSize.A4);
        PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
        doc.Open();
        //Creating paragraph for header
        BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.BLUE);
        Paragraph prgHeading = new Paragraph();
        prgHeading.Alignment = Element.ALIGN_CENTER;
        prgHeading.Add(new Chunk("Audit Log".ToUpper(), fntHead));
        doc.Add(prgHeading);
        Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
        doc.Add(p);
        //Adding line break
        doc.Add(new Chunk("\n", fntHead));
        //Adding  PdfPTable
        PdfPTable table = new PdfPTable(dtAuditLog.Columns.Count);
        for (int i = 0; i < dtAuditLog.Columns.Count; i++)
        {
            string cellText = Server.HtmlDecode(dtAuditLog.Columns[i].ColumnName);
            PdfPCell cell = new PdfPCell();
            cell.Phrase = new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, 1, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
            cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#00AAFF"));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.PaddingBottom = 5;
            table.AddCell(cell);
        }
        //writing table Data
        for (int i = 0; i < dtAuditLog.Rows.Count; i++)
        {
            for (int j = 0; j < dtAuditLog.Columns.Count; j++)
            {
                table.AddCell(dtAuditLog.Rows[i][j].ToString());
            }
        }
        doc.Add(table);
        doc.Close();
        writer.Close();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;" + "filename=AuditLog.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write(doc);
        Response.End();
    }

    // row edit event
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        BindData();
    }

    // cancel row edit event
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindData();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // find id of edit row
        int auditLogId = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

        // find updated values for update
        TextBox txtAction = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtAction");
        TextBox txtLog = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtLog");

        sql = "UPDATE [AuditLog] SET Action=@Action, Log=@Log WHERE Id=@Id";

        cmd = new SqlCommand(sql, cnn);
        cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Text) { Value = txtAction.Text });
        cmd.Parameters.Add(new SqlParameter("@Log", SqlDbType.Text) { Value = txtLog.Text });
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = auditLogId });

        Int32 rowsAffected = cmd.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            GridView1.EditIndex = -1;
            BindData();
        }

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int auditLogId = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

        //delete audit log
        sql = "Delete From [AuditLog] WHERE Id=@Id";
        cmd = new SqlCommand(sql, cnn);
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = auditLogId });

        Int32 rowsAffected3 = cmd.ExecuteNonQuery();

        BindData();

    }
}