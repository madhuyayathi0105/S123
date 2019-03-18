﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Drawing;
using System.Web.Services;

public partial class BankMaster : System.Web.UI.Page
{

    string usercode = string.Empty;
    string collegecode1 = string.Empty;
    string collegecode = string.Empty;
    string singleuser = string.Empty;
    string group_user = string.Empty;

    static string collegestat = string.Empty;
    static string collegestatpop = string.Empty;
    string selectQuery = "";
    DAccess2 d2 = new DAccess2();
    DataSet ds = new DataSet();
    bool fpbuild_Click = false;
    bool spread1_Click = false;
    bool staff_click = false;
    bool staff1_click = false;
    bool popchq_click = false;
    bool staff2_click = false;


    protected void Page_Load(object sender, EventArgs e)
    {
       
        lblvalidation1.Visible = false;
        usercode = Session["usercode"].ToString();
        collegecode1 = Session["collegecode"].ToString();
        singleuser = Session["single_user"].ToString();
        group_user = Session["group_code"].ToString();
        collegecode1 = Session["collegecode"].ToString();
        if (!IsPostBack)
        {
            setLabelText();
            bindloadcol();
            bindclg();
            loaddistrict();
            loadstate();
            if (ddlcolload.Items.Count > 0)
            {
                collegestat = ddlcolload.SelectedItem.Value.ToString();
            }
            if (ddlpopclg.Items.Count > 0)
            {
                collegestatpop = ddlpopclg.SelectedItem.Value.ToString();
            }
            bindbankname();
            btn_go_Click(sender, e);
        }
        string grouporusercode = "";
        if ((Session["group_code"].ToString().Trim() != "") && (Session["group_code"].ToString().Trim() != "0") && (Session["group_code"].ToString().Trim() != "-1"))
        {
            grouporusercode = " group_code=" + Session["group_code"].ToString().Trim() + "";
            usercode = Session["group_code"].ToString();
        }
        else
        {
            grouporusercode = " usercode=" + Session["usercode"].ToString().Trim() + "";
            usercode = Session["usercode"].ToString();
        }
        if (ddlcolload.Items.Count > 0)
        {
            collegestat = ddlcolload.SelectedItem.Value.ToString();
        }
        if (ddlpopclg.Items.Count > 0)
        {
            collegestatpop = ddlpopclg.SelectedItem.Value.ToString();
        }
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getname(string prefixText)
    {
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> name = new List<string>();
        string query = "select distinct staff_name from staffmaster WHERE  college_code=" + collegestat + " and staff_name like '" + prefixText + "%' ";
        dw = dn.select_method_wo_parameter(query, "Text");
        if (dw.Tables.Count > 0)
        {
            if (dw.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
                {
                    name.Add(dw.Tables[0].Rows[i]["staff_name"].ToString());
                }
            }
        }
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> GetdesCode(string prefixText)
    {
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> name = new List<string>();
        string query = "select distinct staff_code from staffmaster where staff_code like '" + prefixText + "%' and college_code=" + collegestat + " ";
        dw = dn.select_method_wo_parameter(query, "Text");
        if (dw.Tables.Count > 0)
        {
            if (dw.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
                {
                    name.Add(dw.Tables[0].Rows[i]["staff_code"].ToString());
                }
            }
        }
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getstaffname(string prefixText)
    {
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> name = new List<string>();
        string query = "select distinct CONVERT(varchar(10),staff_name)+'-'+CONVERT(varchar(10),staff_code) as staffname from staffmaster where college_code=" + collegestat + " and staff_name like '" + prefixText + "%' ";
        dw = dn.select_method_wo_parameter(query, "Text");
        if (dw.Tables.Count > 0)
        {
            if (dw.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
                {
                    name.Add(dw.Tables[0].Rows[i]["staffname"].ToString());
                }
            }
        }
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getaccname(string prefixText)
    {
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> name = new List<string>();
        string query = "select distinct AccHolderName from FM_FinBankMaster WHERE CollegeCode=" + collegestat + " and AccHolderName like '" + prefixText + "%' ";
        dw = dn.select_method_wo_parameter(query, "Text");
        if (dw.Tables.Count > 0)
        {
            if (dw.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
                {
                    name.Add(dw.Tables[0].Rows[i]["AccHolderName"].ToString());
                }
            }
        }
        return name;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> Getaccnumber(string prefixText)
    {
        DAccess2 dn = new DAccess2();
        DataSet dw = new DataSet();
        List<string> num = new List<string>();
        string query = "select distinct AccNo from FM_FinBankMaster WHERE CollegeCode=" + collegestat + " and AccNo like '" + prefixText + "%' ";
        dw = dn.select_method_wo_parameter(query, "Text");
        if (dw.Tables.Count > 0)
        {
            if (dw.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dw.Tables[0].Rows.Count; i++)
                {
                    num.Add(dw.Tables[0].Rows[i]["AccNo"].ToString());
                }
            }
        }
        return num;
    }

    [WebMethod]
    public static string Checkchequeno(string chqno)
    {
        string returnValue = "1";
        try
        {
            DAccess2 dd = new DAccess2();
            string chq_no = chqno;
            if (chq_no.Trim() != "" && chq_no != null)
            {
                string querychq = dd.GetFunction("select distinct ChequeBookNo,BankFK from FM_BankChqDet where ChequeBookNo='" + chq_no + "' and bankfk = (select bankpk from FM_FinBankMaster where CollegeCode=" + collegestatpop + ") ");
                if (querychq.Trim() == "" || querychq == null || querychq == "0" || querychq == "-1")
                {
                    returnValue = "0";
                }

            }
            else
            {
                returnValue = "2";
            }
        }
        catch (SqlException ex)
        {
            returnValue = "error" + ex.ToString();
        }
        return returnValue;
    }

    protected void lb3_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("BankMaster.aspx", false);
        }
        catch
        {
        }
    }
    protected void ddlcolload_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindbankname();
            btn_go_Click(sender, e);
        }
        catch { }
    }

    protected void ddlpopclg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txt_saname1.Text = "";
            txt_saname2.Text = "";
        }
        catch
        {

        }
    }

    protected void txt_accrdate_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime currdate = DateTime.Now;
            string date = txt_accrdate.Text.Trim();
            string[] split = new string[2];
            split = date.Split('/');
            DateTime dt = Convert.ToDateTime(split[1] + "/" + split[0] + "/" + split[2]);
            if (dt > currdate)
            {
                imgdiv2.Visible = true;
                lbl_alert.Visible = true;
                lbl_alert.Text = "Please select a Valid Date!";
                txt_accrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        catch
        {

        }
    }

    protected void cb_bank_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int count = 0;

            txt_bankname.Text = "--Select--";
            if (cb_bank.Checked == true)
            {
                count++;
                for (int i = 0; i < cbl_bank.Items.Count; i++)
                {
                    cbl_bank.Items[i].Selected = true;
                }
                txt_bankname.Text = "Bank Name(" + cbl_bank.Items.Count + ")";
            }
            else
            {
                for (int i = 0; i < cbl_bank.Items.Count; i++)
                {
                    cbl_bank.Items[i].Selected = false;
                }
                txt_bankname.Text = "--Select--";
            }
        }
        catch
        {
        }
    }
    protected void cbl_bank_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            int i = 0;
            cb_bank.Checked = false;
            int commcount = 0;

            txt_bankname.Text = "--Select--";
            for (i = 0; i < cbl_bank.Items.Count; i++)
            {
                if (cbl_bank.Items[i].Selected == true)
                {
                    commcount = commcount + 1;

                }
            }
            if (commcount > 0)
            {
                if (commcount == cbl_bank.Items.Count)
                {

                    cb_bank.Checked = true;
                }
                txt_bankname.Text = "Bank Name(" + commcount.ToString() + ")";

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void bindloadcol()
    {
        try
        {
            string strUser = d2.getUserCode(Convert.ToString(Session["group_code"]), Convert.ToString(Session["usercode"]), 1);
            ds.Clear();
            ddlcolload.Items.Clear();
            string query = "select cp.college_code,cf.collname from collegeprivilages cp,collinfo cf where " + strUser + " and cp.college_code=cf.college_code";
            ds = d2.select_method_wo_parameter(query, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcolload.DataSource = ds;
                ddlcolload.DataTextField = "collname";
                ddlcolload.DataValueField = "college_code";
                ddlcolload.DataBind();
            }
        }
        catch
        {

        }
    }
    protected void bindclg()
    {
        try
        {
            string strUser = d2.getUserCode(Convert.ToString(Session["group_code"]), Convert.ToString(Session["usercode"]), 1);
            ds.Clear();
            ddlpopclg.Items.Clear();
            string query = "select cp.college_code,cf.collname from collegeprivilages cp,collinfo cf where " + strUser + " and cp.college_code=cf.college_code";
            ds = d2.select_method_wo_parameter(query, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlpopclg.DataSource = ds;
                ddlpopclg.DataTextField = "collname";
                ddlpopclg.DataValueField = "college_code";
                ddlpopclg.DataBind();
            }
        }
        catch
        {

        }
    }
    protected void bindbankname()
    {

        try
        {
            cbl_bank.Items.Clear();
            string query = "select distinct BankPK,BankName from FM_FinBankMaster where CollegeCode=" + collegestat + "";
            ds.Clear();
            ds = d2.select_method_wo_parameter(query, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                cbl_bank.DataSource = ds;
                cbl_bank.DataTextField = "BankName";
                cbl_bank.DataValueField = "BankPK";
                cbl_bank.DataBind();

                if (cbl_bank.Items.Count > 0)
                {
                    for (int i = 0; i < cbl_bank.Items.Count; i++)
                    {
                        cbl_bank.Items[i].Selected = true;
                    }
                    cb_bank.Checked = true;
                    txt_bankname.Text = "Bank Name(" + cbl_bank.Items.Count + ")";
                }
            }
            else
            {
                txt_bankname.Text = "--Select--";
            }
        }
        catch
        {

        }
    }

    protected void btn_go_Click(object sender, EventArgs e)
    {
        try
        {
            if (collegecode1 != null)
            {
                string headercode = "";
                for (int i = 0; i < cbl_bank.Items.Count; i++)
                {
                    if (cbl_bank.Items[i].Selected == true)
                    {
                        if (headercode == "")
                        {
                            headercode = "" + cbl_bank.Items[i].Value.ToString() + "";
                        }
                        else
                        {
                            headercode = headercode + "'" + "," + "'" + cbl_bank.Items[i].Value.ToString() + "";
                        }
                    }

                }
                if (headercode.Trim() != "")
                {
                    string selq = "";
                    if (txt_acname.Text.Trim() != "" && txt_acno.Text.Trim() != "")
                    {
                        selq = "select BankPK,BankCode,BankName,BankBranch,AccHolderName,AccNo,AccType,(CONVERT(varchar(10), AccStartDate,103)) as acctcreate,SignAuthorityStaff1,CollegeCode from FM_FinBankMaster where CollegeCode='" + collegestat + "' and AccHolderName='" + Convert.ToString(txt_acname.Text) + "' and AccNo='" + Convert.ToString(txt_acno.Text) + "' and BankPK in('" + headercode + "') order by acctcreate";
                    }
                    else if (txt_acno.Text.Trim() != "")
                    {
                        selq = "select BankPK,BankCode,BankName,BankBranch,AccHolderName,AccNo,AccType,(CONVERT(varchar(10), AccStartDate,103)) as acctcreate,SignAuthorityStaff1,CollegeCode from FM_FinBankMaster where CollegeCode='" + collegestat + "' and AccNo='" + Convert.ToString(txt_acno.Text) + "' and BankPK in('" + headercode + "') order by acctcreate";
                    }
                    else if (txt_acname.Text.Trim() != "")
                    {
                        selq = "select BankPK,BankCode,BankName,BankBranch,AccHolderName,AccNo,AccType,(CONVERT(varchar(10), AccStartDate,103)) as acctcreate,SignAuthorityStaff1,CollegeCode from FM_FinBankMaster where CollegeCode='" + collegestat + "' and AccHolderName='" + Convert.ToString(txt_acname.Text) + "' and BankPK in('" + headercode + "') order by acctcreate";
                    }
                    else
                    {
                        selq = "select BankPK,BankCode,BankName,BankBranch,AccHolderName,AccNo,AccType,(CONVERT(varchar(10), AccStartDate,103)) as acctcreate,SignAuthorityStaff1,CollegeCode from FM_FinBankMaster where CollegeCode ='" + collegestat + "' and BankPK in('" + headercode + "') order by acctcreate";
                    }
                    ds.Clear();
                    ds = d2.select_method_wo_parameter(selq, "Text");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Fpspread1.Sheets[0].RowCount = 0;
                        Fpspread1.Sheets[0].ColumnCount = 0;
                        Fpspread1.CommandBar.Visible = false;
                        Fpspread1.Sheets[0].AutoPostBack = true;
                        Fpspread1.Sheets[0].ColumnHeader.RowCount = 1;
                        Fpspread1.Sheets[0].RowHeader.Visible = false;
                        Fpspread1.Sheets[0].ColumnCount = 6;

                        FarPoint.Web.Spread.StyleInfo darkstyle = new FarPoint.Web.Spread.StyleInfo();
                        darkstyle.BackColor = ColorTranslator.FromHtml("#0CA6CA");
                        darkstyle.ForeColor = Color.White;
                        Fpspread1.ActiveSheetView.ColumnHeader.DefaultStyle = darkstyle;

                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 0].Text = "S.No";
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 0].Font.Bold = true;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 0].Border.BorderColor = System.Drawing.Color.Black;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 0].Font.Name = "Book Antiqua";
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 0].Font.Size = FontUnit.Medium;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 0].HorizontalAlign = HorizontalAlign.Center;

                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 1].Text = "Bank Name";
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 1].Font.Bold = true;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 1].Border.BorderColor = System.Drawing.Color.Black;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 1].Font.Name = "Book Antiqua";
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 1].Font.Size = FontUnit.Medium;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 1].HorizontalAlign = HorizontalAlign.Center;

                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 2].Text = "Account Name";
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 2].Font.Bold = true;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 2].Border.BorderColor = System.Drawing.Color.Black;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 2].Font.Name = "Book Antiqua";
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 2].Font.Size = FontUnit.Medium;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 2].HorizontalAlign = HorizontalAlign.Center;

                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 3].Text = "Account Number";
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 3].Font.Bold = true;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 3].Border.BorderColor = System.Drawing.Color.Black;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 3].Font.Name = "Book Antiqua";
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 3].Font.Size = FontUnit.Medium;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 3].HorizontalAlign = HorizontalAlign.Center;

                        FarPoint.Web.Spread.TextCellType txtall = new FarPoint.Web.Spread.TextCellType();

                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 4].Text = "Account Creation Date";
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 4].Font.Bold = true;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 4].Border.BorderColor = System.Drawing.Color.Black;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 4].Font.Name = "Book Antiqua";
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 4].Font.Size = FontUnit.Medium;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 4].HorizontalAlign = HorizontalAlign.Center;

                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 5].Text = "Signing Authority";
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 5].Font.Bold = true;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 5].Border.BorderColor = System.Drawing.Color.Black;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 5].Font.Name = "Book Antiqua";
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 5].Font.Size = FontUnit.Medium;
                        Fpspread1.Sheets[0].ColumnHeader.Cells[0, 5].HorizontalAlign = HorizontalAlign.Center;

                        for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                        {
                            Fpspread1.Sheets[0].RowCount++;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 0].Text = Convert.ToString(row + 1);
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Center;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 0].Border.BorderColor = System.Drawing.Color.Black;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 0].Font.Size = FontUnit.Medium;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 0].Font.Name = "Book Antiqua";

                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 1].Text = Convert.ToString(ds.Tables[0].Rows[row]["BankName"]);
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 1].Tag = Convert.ToString(ds.Tables[0].Rows[row]["BankCode"]);
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 1].HorizontalAlign = HorizontalAlign.Left;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 1].Border.BorderColor = System.Drawing.Color.Black;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 1].Font.Size = FontUnit.Medium;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 1].Font.Name = "Book Antiqua";

                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 2].Text = Convert.ToString(ds.Tables[0].Rows[row]["AccHolderName"]);
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 2].Tag = Convert.ToString(ds.Tables[0].Rows[row]["BankPK"]);
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 2].HorizontalAlign = HorizontalAlign.Left;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 2].Border.BorderColor = System.Drawing.Color.Black;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 2].Font.Size = FontUnit.Medium;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 2].Font.Name = "Book Antiqua";

                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 3].CellType = txtall;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 3].Text = Convert.ToString(ds.Tables[0].Rows[row]["AccNo"]);
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 3].Tag = Convert.ToString(ds.Tables[0].Rows[row]["AccType"]);
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 3].HorizontalAlign = HorizontalAlign.Left;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 3].Border.BorderColor = System.Drawing.Color.Black;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 3].Font.Size = FontUnit.Medium;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 3].Font.Name = "Book Antiqua";

                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 4].Text = Convert.ToString(ds.Tables[0].Rows[row]["acctcreate"]);
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 4].Tag = Convert.ToString(ds.Tables[0].Rows[row]["CollegeCode"]);
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 4].HorizontalAlign = HorizontalAlign.Left;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 4].Border.BorderColor = System.Drawing.Color.Black;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 4].Font.Size = FontUnit.Medium;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 4].Font.Name = "Book Antiqua";

                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 5].Tag = Convert.ToString(ds.Tables[0].Rows[row]["SignAuthorityStaff1"]);
                            string staffcode = Convert.ToString(Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 5].Tag);
                            string selstaff = "select staff_name from staffmaster s,staff_appl_master a where s.appl_no=a.appl_no and a.appl_id='" + staffcode + "' and s.college_code='" + collegestat + "'";
                            string staffname = d2.GetFunction(selstaff);

                            if (staffname.Trim() != "" && staffname.Trim() != "0")
                            {
                                Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 5].Text = Convert.ToString(staffname);
                            }
                            else
                            {
                                Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 5].Text = "";
                            }
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 5].HorizontalAlign = HorizontalAlign.Left;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 5].Border.BorderColor = System.Drawing.Color.Black;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 5].Font.Size = FontUnit.Medium;
                            Fpspread1.Sheets[0].Cells[Fpspread1.Sheets[0].RowCount - 1, 5].Font.Name = "Book Antiqua";

                        }
                        Fpspread1.Visible = true;
                        div2.Visible = true;
                        lbl_error.Visible = false;
                        rptprint.Visible = true;
                        Fpspread1.Sheets[0].PageSize = Fpspread1.Sheets[0].RowCount;
                    }
                    else
                    {
                        Fpspread1.Visible = false;
                        div2.Visible = false;
                        lbl_error.Visible = true;
                        rptprint.Visible = false;
                        lbl_error.Text = "Please Search The Corresponding Values";
                    }
                }
                else
                {
                    Fpspread1.Visible = false;
                    div2.Visible = false;
                    lbl_error.Visible = true;
                    rptprint.Visible = false;
                    lbl_error.Text = "No Records Found";
                }
            }
        }
        catch
        {
        }
    }
    protected void btn_addnew_Click(object sender, EventArgs e)
    {
        clear();
        bindclg();
        loaddistrict();
        loadstate();
        popwindow.Visible = true;
        btn_save.Visible = true;
        btn_update.Visible = false;
        btn_delete.Visible = false;
        div01.Visible = true;
        lnkadd.Visible = false;
        //string selquery = "select top 1 BankCode from FM_FinBankMaster order by BankCode desc";
        //string bankcode = d2.GetFunction(selquery);
        //string codebank = bankcode.Replace("BC", " ");
        //int code = Convert.ToInt32(codebank);
        //string newbankcode = (code + 1).ToString();
        //string gencode = "BC" + newbankcode;
        //txt_bankcode.Text = gencode;
    }

    protected void btn_saname1_Click(object sender, EventArgs e)
    {
        try
        {
            popupsscode1.Visible = true;
            btn_go2_Click(sender, e);
            ddlsearch1.SelectedIndex = 0;
            Label1.Text = "Search By Name";
            txtsearch1.Visible = true;
            txtsearch1c.Visible = false;
            txtsearch1.Text = "";
        }
        catch
        {
        }
    }

    protected void btn_saname2_Click(object sender, EventArgs e)
    {
        try
        {
            popupsscode2.Visible = true;
            btn_go3_Click(sender, e);
            ddlsearch2.SelectedIndex = 0;
            Label2.Text = "Search By Name";
            txtsearch2.Visible = true;
            txtsearch2c.Visible = false;
            txtsearch2.Text = "";
        }
        catch
        {
        }
    }

    protected void btn_errorclose_Click(object sender, EventArgs e)
    {
        imgdiv2.Visible = false;
    }

    protected void lbadd_click(object sender, EventArgs e)
    {
        try
        {
            string acctype = "";
            if (txt_bkname.Text != "")
            {
                popchq.Visible = true;
                btn_savechq.Visible = true;
                txtbankname.Text = txt_bkname.Text;
                txtbankcode.Text = txt_bankcode.Text;
                string bankcode = txtbankcode.Text;
                string bankname = txtbankname.Text;
                if (rdo_cac.Checked)
                {
                    acctype = "Current Account";
                }
                if (rdo_sac.Checked)
                {
                    acctype = "Savings Account";
                }
                int bankfk = Convert.ToInt32(Convert.ToString(ViewState["bankid"]));

                txtchqacc.Text = acctype.ToString();
                string selquery = "select b.BankName,c.BankFK,c.ChqStartNo,c.ChqEndNo,c.NoOfLeaf,convert(varchar(10),ChqReceivedDate,103) as date,c.ChequeBookNo from FM_BankChqDet c,FM_FinBankMaster b where c.BankFK=b.BankPK and c.BankFK='" + bankfk + "' and b.BankName='" + bankname + "' and b.CollegeCode='" + collegestatpop + "'";
                ds.Clear();
                ds = d2.select_method_wo_parameter(selquery, "Text");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Fpspread3.Sheets[0].RowCount = 0;
                    Fpspread3.Sheets[0].ColumnCount = 0;
                    Fpspread3.CommandBar.Visible = false;
                    Fpspread3.Sheets[0].AutoPostBack = true;
                    Fpspread3.Sheets[0].ColumnHeader.RowCount = 1;
                    Fpspread3.Sheets[0].RowHeader.Visible = false;
                    Fpspread3.Sheets[0].ColumnCount = 6;

                    FarPoint.Web.Spread.TextCellType txtcell = new FarPoint.Web.Spread.TextCellType();

                    FarPoint.Web.Spread.StyleInfo darkstyle = new FarPoint.Web.Spread.StyleInfo();
                    darkstyle.BackColor = ColorTranslator.FromHtml("#0CA6CA");
                    darkstyle.ForeColor = Color.White;
                    Fpspread3.ActiveSheetView.ColumnHeader.DefaultStyle = darkstyle;

                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 0].Text = "S.No";
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 0].Font.Bold = true;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 0].Font.Name = "Book Antiqua";
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 0].ForeColor = System.Drawing.Color.Black;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 0].Font.Size = FontUnit.Medium;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 0].HorizontalAlign = HorizontalAlign.Center;
                    Fpspread3.Columns[0].Width = 65;

                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 1].Text = "Cheque Book No";
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 1].Font.Bold = true;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 1].Font.Name = "Book Antiqua";
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 1].ForeColor = System.Drawing.Color.Black;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 1].Font.Size = FontUnit.Medium;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 1].HorizontalAlign = HorizontalAlign.Center;
                    Fpspread3.Columns[1].Width = 200;

                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 2].Text = "Start Number";
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 2].Font.Bold = true;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 2].Font.Name = "Book Antiqua";
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 2].ForeColor = System.Drawing.Color.Black;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 2].Font.Size = FontUnit.Medium;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 2].HorizontalAlign = HorizontalAlign.Center;
                    Fpspread3.Columns[2].Width = 100;

                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 3].Text = "End Number";
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 3].Font.Bold = true;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 3].Column.Width = 150;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 3].Font.Name = "Book Antiqua";
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 3].ForeColor = System.Drawing.Color.Black;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 3].Font.Size = FontUnit.Medium;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 3].HorizontalAlign = HorizontalAlign.Center;
                    Fpspread3.Columns[3].Width = 100;

                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 4].Text = "No of Leaf";
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 4].Font.Bold = true;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 4].Font.Name = "Book Antiqua";
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 4].ForeColor = System.Drawing.Color.Black;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 4].Font.Size = FontUnit.Medium;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 4].HorizontalAlign = HorizontalAlign.Center;
                    Fpspread3.Columns[4].Width = 75;

                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 5].Text = "Received Date";
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 5].Font.Bold = true;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 5].Font.Name = "Book Antiqua";
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 5].ForeColor = System.Drawing.Color.Black;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 5].Font.Size = FontUnit.Medium;
                    Fpspread3.Sheets[0].ColumnHeader.Cells[0, 5].HorizontalAlign = HorizontalAlign.Center;
                    Fpspread3.Columns[5].Width = 210;

                    for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                    {
                        Fpspread3.Sheets[0].RowCount++;
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 0].Text = Convert.ToString(row + 1);
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Center;
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 0].Font.Size = FontUnit.Medium;
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 0].Font.Name = "Book Antiqua";

                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 1].Text = Convert.ToString(ds.Tables[0].Rows[row]["ChequeBookNo"]);
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 1].Tag = Convert.ToString(ds.Tables[0].Rows[row]["BankFK"]);
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 1].HorizontalAlign = HorizontalAlign.Left;
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 1].Font.Size = FontUnit.Medium;
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 1].Font.Name = "Book Antiqua";

                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 2].Text = Convert.ToString(ds.Tables[0].Rows[row]["ChqStartNo"]);
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 2].HorizontalAlign = HorizontalAlign.Left;
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 2].Font.Size = FontUnit.Medium;
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 2].Font.Name = "Book Antiqua";

                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 3].Text = Convert.ToString(ds.Tables[0].Rows[row]["ChqEndNo"]);
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 3].HorizontalAlign = HorizontalAlign.Center;
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 3].Font.Size = FontUnit.Medium;
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 3].Font.Name = "Book Antiqua";

                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 4].Text = Convert.ToString(ds.Tables[0].Rows[row]["NoOfLeaf"]);
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 4].HorizontalAlign = HorizontalAlign.Center;
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 4].Font.Size = FontUnit.Medium;
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 4].Font.Name = "Book Antiqua";

                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 5].CellType = txtcell;
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 5].Text = Convert.ToString(ds.Tables[0].Rows[row]["date"]);
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 5].HorizontalAlign = HorizontalAlign.Center;
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 5].Font.Size = FontUnit.Medium;
                        Fpspread3.Sheets[0].Cells[Fpspread3.Sheets[0].RowCount - 1, 5].Font.Name = "Book Antiqua";

                    }
                    Fpspread3.Visible = true;
                    rptprint.Visible = true;
                    div9.Visible = true;
                    div8.Visible = true;
                    lblchqerr.Visible = false;
                    Fpspread3.Sheets[0].PageSize = Fpspread3.Sheets[0].RowCount;
                    Fpspread3.Width = 750;
                    Fpspread3.Height = 300;
                }
                else
                {
                    Fpspread3.Visible = false;
                    div9.Visible = true;
                    div8.Visible = true;
                    lblchqerr.Visible = true;
                    lblchqerr.Text = "No Records Found";
                }
            }
            else
            {
                imgdiv2.Visible = true;
                lbl_alert.Visible = true;
                lbl_alert.Text = "Please Enter the Bank Name";
            }
        }
        catch
        {

        }
    }

    protected void imagebtnpopclosechq_Click(object sender, EventArgs e)
    {
        popchq.Visible = false;
    }

    protected void imagebtnpopclosechqdet_Click(object sender, EventArgs e)
    {
        popchqdet.Visible = false;
    }

    protected void imagebtnpopclosestaff_Click(object sender, EventArgs e)
    {
        popchqstaff.Visible = false;
    }

    protected void btn_savechq_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtbankname.Text != "")
            {
                popchqdet.Visible = true;
                btn_savechqdet.Visible = true;
                btn_updatechqdet.Visible = false;
                btn_deletechqdet.Visible = false;
                chqdetclear();
                txtbkname.Text = txtbankname.Text;
                txtbnkchqcode.Text = txtbankcode.Text;
            }
        }
        catch
        {

        }
    }

    protected void btn_updatechq_Click(object sender, EventArgs e)
    {

    }

    protected void btn_deletechq_Click(object sender, EventArgs e)
    {

    }

    protected void btn_exitchq_Click(object sender, EventArgs e)
    {
        popchq.Visible = false;
    }

    protected void btnexitchq_click(object sender, EventArgs e)
    {
        popchqstaff.Visible = false;
    }

    protected void txtstartno_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            int startno = Convert.ToInt32(txtstartno.Text);
            int noofleaf = Convert.ToInt32(txtleaf.Text);
            if (startno != 0)
            {
                txtendno.Text = ((startno + noofleaf) - 1).ToString();
            }
            string endno = txtendno.Text;
        }
        catch
        {

        }
    }

    protected void txtleaf_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            int startno = Convert.ToInt32(txtstartno.Text);
            int noofleaf = Convert.ToInt32(txtleaf.Text);
            if (startno != 0)
            {
                txtendno.Text = ((startno + noofleaf) - 1).ToString();
            }
            string endno = txtendno.Text;
        }
        catch
        {

        }
    }

    protected void btn_staff_Click(object sender, EventArgs e)
    {
        try
        {
            popchqstaff.Visible = true;
            txtsearch3.Text = "";
            if (collegecode1 != null)
            {
                string selq = "";
                selq = "select staff_code,staff_name from staffmaster s,staff_appl_master a where s.appl_no=a.appl_no and s.college_code='" + collegestatpop + "' order by PrintPriority";
                ds.Clear();
                ds = d2.select_method_wo_parameter(selq, "Text");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    FpSpread4.Sheets[0].RowCount = 0;
                    FpSpread4.Sheets[0].ColumnCount = 0;
                    FpSpread4.CommandBar.Visible = false;
                    FpSpread4.Sheets[0].AutoPostBack = true;
                    FpSpread4.Sheets[0].ColumnHeader.RowCount = 1;
                    FpSpread4.Sheets[0].RowHeader.Visible = false;
                    FpSpread4.Sheets[0].ColumnCount = 3;

                    FarPoint.Web.Spread.StyleInfo darkstyle = new FarPoint.Web.Spread.StyleInfo();
                    darkstyle.BackColor = ColorTranslator.FromHtml("#0CA6CA");
                    darkstyle.ForeColor = Color.White;
                    FpSpread4.ActiveSheetView.ColumnHeader.DefaultStyle = darkstyle;

                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 0].Text = "S.No";
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 0].Font.Bold = true;
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 0].Font.Name = "Book Antiqua";
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 0].Font.Size = FontUnit.Medium;
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 0].HorizontalAlign = HorizontalAlign.Center;

                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 1].Text = "Staff Code";
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 1].Font.Bold = true;
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 1].Font.Name = "Book Antiqua";
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 1].Font.Size = FontUnit.Medium;
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 1].HorizontalAlign = HorizontalAlign.Center;

                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 2].Text = "Staff Name";
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 2].Font.Bold = true;
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 2].Font.Name = "Book Antiqua";
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 2].Font.Size = FontUnit.Medium;
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 2].HorizontalAlign = HorizontalAlign.Center;

                    for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                    {
                        FpSpread4.Sheets[0].RowCount++;
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 0].Text = Convert.ToString(row + 1);
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Center;
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 0].Font.Size = FontUnit.Medium;
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 0].Font.Name = "Book Antiqua";

                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 1].Text = Convert.ToString(ds.Tables[0].Rows[row]["staff_code"]);
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 1].HorizontalAlign = HorizontalAlign.Left;
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 1].Font.Size = FontUnit.Medium;
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 1].Font.Name = "Book Antiqua";

                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 2].Text = Convert.ToString(ds.Tables[0].Rows[row]["staff_name"]);
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 2].HorizontalAlign = HorizontalAlign.Left;
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 2].Font.Size = FontUnit.Medium;
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 2].Font.Name = "Book Antiqua";

                    }
                    FpSpread4.Visible = true;
                    div2.Visible = true;
                    lblerr1.Visible = false;
                    FpSpread4.Sheets[0].PageSize = FpSpread4.Sheets[0].RowCount;
                }
            }
        }
        catch
        {

        }
    }

    protected void btn_savechqdet_Click(object sender, EventArgs e)
    {
        try
        {
            string actrow = Convert.ToString(FpSpread4.ActiveSheetView.ActiveRow);
            string actcol = Convert.ToString(FpSpread4.ActiveSheetView.ActiveColumn);
            string bankname = txtbkname.Text;
            string chkno = txtchqno.Text;
            string nooleaf = txtleaf.Text;
            string startno = txtstartno.Text;
            string endno = txtendno.Text;
            string bankcode = txtbnkchqcode.Text;
            string staffcode = txtstaff.Text;
            string genstaff = "";
            string[] splitcode = new string[2];
            if (staffcode.Trim() != "")
            {
                if (staffcode.Contains('-'))
                {
                    splitcode = staffcode.Split('-');
                    genstaff = splitcode[1];
                    string selquery = "select s.staff_name from staffmaster s ,staff_appl_master a where s.appl_no=a.appl_no and s.staff_code='" + genstaff + "' and s.college_code='" + collegestatpop + "'";
                    ds.Clear();
                    ds = d2.select_method_wo_parameter(selquery, "Text");
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Visible = true;
                            lbl_alert.Text = "Please select valid Staff Name!";
                            txtstaff.Text = "";
                            return;
                        }
                    }
                }
                else
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Visible = true;
                    lbl_alert.Text = "Please select valid Staff Name!";
                    txtstaff.Text = "";
                    return;
                }
            }
            else
            {
                genstaff = "";
            }

            string revddate = txtrevddate.Text;
            string[] split = revddate.Split('/');
            DateTime dt = Convert.ToDateTime(split[1] + "/" + split[0] + "/" + split[2]);

            string newbankcode = Convert.ToString(ViewState["bankid"]);
            string chk = "select ChequeBookNo from FM_BankChqDet where BankFK='" + newbankcode + "' and ChequeBookNo='" + chkno + "'";
            ds.Clear();
            ds = d2.select_method_wo_parameter(chk, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                imgdiv2.Visible = true;
                lbl_alert.Visible = true;
                lbl_alert.Text = "Cheque Book No already available!";
            }
            else
            {
                string insquery = "insert into FM_BankChqDet (BankFK,ChqStartNo,ChqEndNo,NoOfLeaf,RecStaffCode,ChqReceivedDate,ChequeBookNo) values ('" + newbankcode + "','" + startno + "','" + endno + "','" + nooleaf + "','" + genstaff + "','" + dt.ToString("MM/dd/yyyy") + "','" + chkno + "')";
                int inscount = d2.update_method_wo_parameter(insquery, "Text");
                if (inscount > 0)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Visible = true;
                    popchq.Visible = true;
                    popchqdet.Visible = false;
                    lbadd_click(sender, e);
                    chqdetclear();
                    lbl_alert.Text = "Saved Successfully";
                }
            }
        }
        catch
        {

        }
    }

    protected void btn_updatechqdet_Click(object sender, EventArgs e)
    {
        try
        {
            string currrow = Fpspread3.ActiveSheetView.ActiveRow.ToString();
            string currcol = Fpspread3.ActiveSheetView.ActiveColumn.ToString();
            string actrow = FpSpread4.ActiveSheetView.ActiveRow.ToString();
            string actcol = FpSpread4.ActiveSheetView.ActiveColumn.ToString();
            string chkbookno = txtchqno.Text;
            string noofleaf = txtleaf.Text;
            string startno = txtstartno.Text;
            string endno = txtendno.Text;
            string revddate = txtrevddate.Text;
            string staffcode = txtstaff.Text;
            string genstaff = "";
            string[] splitcode = new string[2];
            if (staffcode.Trim() != "")
            {
                if (staffcode.Contains('-'))
                {
                    splitcode = staffcode.Split('-');
                    genstaff = splitcode[1];
                    string selquery = "select s.staff_name from staffmaster s ,staff_appl_master a where s.appl_no=a.appl_no and s.staff_code='" + genstaff + "' and s.college_code='" + collegestatpop + "'";
                    ds.Clear();
                    ds = d2.select_method_wo_parameter(selquery, "Text");
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Visible = true;
                            lbl_alert.Text = "Please select valid Staff Name!";
                            txtstaff.Text = "";
                            return;
                        }
                    }
                }
                else
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Visible = true;
                    lbl_alert.Text = "Please select valid Staff Name!";
                    txtstaff.Text = "";
                    return;
                }
            }
            else
            {
                genstaff = "";
            }

            string[] split = revddate.Split('/');
            DateTime dt = Convert.ToDateTime(split[1] + "/" + split[0] + "/" + split[2]);

            if (currrow.Trim() != "")
            {
                string bankcode = Convert.ToString(Fpspread3.Sheets[0].Cells[Convert.ToInt32(currrow), 1].Tag);
                string chkbkno = Convert.ToString(Fpspread3.Sheets[0].Cells[Convert.ToInt32(currrow), 1].Text);

                string selq = "select ChequeBookNo from FM_BankChqDet where BankFK not in('" + bankcode + "') and ChequeBookNo='" + chkbookno + "'";
                ds.Clear();
                ds = d2.select_method_wo_parameter(selq, "Text");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        imgdiv2.Visible = true;
                        lbl_alert.Visible = true;
                        lbl_alert.Text = "Cheque Book No already available!";
                    }
                    else
                    {
                        string update = "Update FM_BankChqDet set ChequeBookNo='" + chkbookno + "',ChqStartNo='" + startno + "',ChqEndNo='" + endno + "',NoOfLeaf='" + noofleaf + "',RecStaffCode='" + genstaff + "',ChqReceivedDate='" + dt.ToString("MM/dd/yyyy") + "' where BankFK='" + bankcode + "' and ChequeBookNo='" + chkbkno + "'";
                        int upcount = d2.update_method_wo_parameter(update, "Text");
                        if (upcount > 0)
                        {
                            imgdiv2.Visible = true;
                            lbl_alert.Visible = true;
                            popchq.Visible = true;
                            popchqdet.Visible = false;
                            lbadd_click(sender, e);
                            chqdetclear();
                            lbl_alert.Text = "Updated Successfully";
                        }
                    }
                }
            }
        }
        catch
        {

        }
    }

    protected void btn_deletechqdet_Click(object sender, EventArgs e)
    {
        try
        {
            imgDiv0.Visible = true;
            lblchqerrdet.Visible = true;
            lblchqerrdet.Text = "Do you want to delete this Record?";
        }
        catch
        {

        }
    }

    protected void btnyeschq_Click(object sender, EventArgs e)
    {
        try
        {
            string currrow = Fpspread3.ActiveSheetView.ActiveRow.ToString();
            string currcol = Fpspread3.ActiveSheetView.ActiveColumn.ToString();

            if (currrow.Trim() != "")
            {
                string bankcode = Convert.ToString(Fpspread3.Sheets[0].Cells[Convert.ToInt32(currrow), 1].Tag);
                string chkbkno = Convert.ToString(Fpspread3.Sheets[0].Cells[Convert.ToInt32(currrow), 1].Text);
                string delete = "delete from FM_BankChqDet where BankFK='" + bankcode + "' and ChequeBookNo='" + chkbkno + "'";
                int delcount = d2.update_method_wo_parameter(delete, "Text");
                if (delcount > 0)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Visible = true;
                    popchq.Visible = true;
                    popchqdet.Visible = false;
                    lbadd_click(sender, e);
                    imgDiv0.Visible = false;
                    lblchqerrdet.Visible = false;
                    chqdetclear();
                    lbl_alert.Text = "Deleted Successfully";
                }
            }
        }
        catch
        {

        }
    }

    protected void btnnochq_Click(object sender, EventArgs e)
    {
        imgDiv0.Visible = false;
        lblchqerrdet.Visible = false;
    }

    protected void btn_exitchqdet_Click(object sender, EventArgs e)
    {
        popchqdet.Visible = false;
    }

    protected void Cellchq_Click(object sender, EventArgs e)
    {
        try
        {
            popchq_click = true;
        }
        catch
        {

        }
    }

    protected void Fpspread3_render(object sender, EventArgs e)
    {
        try
        {
            if (popchq_click == true)
            {
                popchqdet.Visible = true;
                btn_savechqdet.Visible = false;
                btn_updatechqdet.Visible = true;
                btn_deletechqdet.Visible = true;
                string activerow = Fpspread3.ActiveSheetView.ActiveRow.ToString();
                string activecol = Fpspread3.ActiveSheetView.ActiveColumn.ToString();
                if (activerow.Trim() != "")
                {
                    string bankcode = Convert.ToString(Fpspread3.Sheets[0].Cells[Convert.ToInt32(activerow), 1].Tag);
                    string chkbkno = Convert.ToString(Fpspread3.Sheets[0].Cells[Convert.ToInt32(activerow), 1].Text);
                    string selquery = "select b.BankName,c.BankFK,c.ChequeBookNo,c.ChqStartNo,c.ChqEndNo,c.NoOfLeaf,convert(varchar(10),c.ChqReceivedDate,103) as date,c.RecStaffCode from FM_BankChqDet c,FM_FinBankMaster b where c.BankFK=b.BankPK and c.BankFK='" + bankcode + "' and c.ChequeBookNo='" + chkbkno + "' and b.CollegeCode='" + collegestatpop + "'";
                    ds.Clear();
                    ds = d2.select_method_wo_parameter(selquery, "Text");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtbkname.Text = ds.Tables[0].Rows[0]["BankName"].ToString();
                        txtchqno.Text = ds.Tables[0].Rows[0]["ChequeBookNo"].ToString();
                        txtleaf.Text = ds.Tables[0].Rows[0]["NoOfLeaf"].ToString();
                        txtstartno.Text = ds.Tables[0].Rows[0]["ChqStartNo"].ToString();
                        txtendno.Text = ds.Tables[0].Rows[0]["ChqEndNo"].ToString();
                        txtrevddate.Text = ds.Tables[0].Rows[0]["date"].ToString();
                        string staffcode = ds.Tables[0].Rows[0]["RecStaffCode"].ToString();
                        string selq = "select Convert(varchar(10),staff_name)+'-'+Convert(varchar(10),staff_code) as staffname from staffmaster where staff_code='" + staffcode + "' and college_code='" + collegestatpop + "'";
                        string staffname = d2.GetFunction(selq);
                        if (staffname.Trim() != "")
                        {
                            txtstaff.Text = staffname;
                        }
                        else
                        {
                            txtstaff.Text = "";
                        }
                    }
                }
            }
        }
        catch
        {

        }
    }

    public void chqdetclear()
    {
        txtchqno.Text = "";
        txtleaf.Text = "";
        txtstartno.Text = "";
        txtendno.Text = "";
        txtrevddate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtstaff.Text = "";
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            string acctype = "";
            string actrow = Fpstaff.ActiveSheetView.ActiveRow.ToString();
            string actcol = Fpstaff.ActiveSheetView.ActiveColumn.ToString();

            string currrow = FpSpread2.ActiveSheetView.ActiveRow.ToString();
            string currcol = FpSpread2.ActiveSheetView.ActiveColumn.ToString();

            string bankcode = txt_bankcode.Text.ToString();
            string bankname = txt_bkname.Text.ToString();
            bankname = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(bankname);
            string street = txt_str.Text.ToString();
            street = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(street);
            string area = Convert.ToString(ddl_dis.SelectedItem.Value);
            area = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(area);
            string city = txt_city.Text.ToString();
            string state = Convert.ToString(ddl_state.SelectedItem.Value);
            string pin = txt_pincode.Text.ToString();
            string ifsc = txt_ifsc.Text.ToString();
            string sign1 = txt_saname1.Text.ToString();
            string sign2 = txt_saname2.Text.ToString();
            string branch = txt_branch.Text.ToString();
            string staffsign1 = "";
            string staffsign2 = "";
            string appl_id1 = "";
            string appl_id2 = "";
            try
            {
                if (txt_saname1.Text.Trim() != "")
                {
                    staffsign1 = Convert.ToString(Fpstaff.Sheets[0].Cells[Convert.ToInt32(actrow), 1].Text);
                    string selq = "select appl_id  from staff_appl_master a,staffmaster s where a.appl_no =s.appl_no and s.staff_code in('" + staffsign1 + "') and s.college_code='" + collegestatpop + "'";
                    appl_id1 = d2.GetFunction(selq);
                }
                else
                {
                    appl_id1 = "";
                }
                if (txt_saname2.Text.Trim() != "")
                {
                    staffsign2 = Convert.ToString(FpSpread2.Sheets[0].Cells[Convert.ToInt32(currrow), 1].Text);
                    string selq1 = "select appl_id  from staff_appl_master a,staffmaster s where a.appl_no =s.appl_no and s.staff_code in('" + staffsign2 + "') and s.college_code='" + collegestatpop + "'";
                    appl_id2 = d2.GetFunction(selq1);
                }
                else
                {
                    appl_id2 = "";
                }

                //appl_id1 = Convert.ToString(ViewState["staffapplid"]);
                //appl_id2 = Convert.ToString(ViewState["applid2"]);
            }
            catch
            {

            }
            string rtgs = txt_rtgs.Text.ToString();

            string accdate = txt_accrdate.Text;
            DateTime dt = new DateTime();
            string createdate = Convert.ToString(accdate);
            string[] split = createdate.Split('/');
            dt = Convert.ToDateTime(split[1] + "/" + split[0] + "/" + split[2]);

            if (rdo_cac.Checked == true)
            {
                acctype = "1";
            }
            if (rdo_sac.Checked == true)
            {
                acctype = "2";
            }
            string accholder = txt_acholdrname.Text.ToString();
            string accno = txt_acnumbr.Text.ToString();
            //string contact = txt_contper.Text.ToString();
            string mobile = txt_mblno.Text.ToString();
            string offphone = txt_ofcno.Text.ToString();
            //string sadesig1 = txt_sadesg1.Text.ToString();
            //string sadesig2 = txt_sadesg2.Text.ToString();
            string Micr = txt_micr.Text.ToString();
            string selquery = "select AccNo from FM_FinBankMaster where AccNo='" + accno + "' and CollegeCode='" + collegestatpop + "'";
            ds.Clear();
            ds = d2.select_method_wo_parameter(selquery, "Text");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    imgdiv2.Visible = true;
                    lbl_alert.Visible = true;
                    lbl_alert.Text = "Account Number already Exist!";
                }
                else
                {
                    string insertq = "";
                    insertq = "Insert into FM_FinBankMaster(BankCode,BankName,BankBranch,Street,District,City,State,PinCode,IFSCCode,SignAuthorityStaff1,SignAuthorityStaff2,RTGSCode,AccStartDate,AccType,AccHolderName,AccNo,MobileNo,PhoneNo,MCRCode,CollegeCode)";
                    insertq = insertq + "values('" + bankcode + "','" + bankname + "','" + branch + "','" + street + "','" + area + "','" + city + "','" + state + "','" + pin + "','" + ifsc + "','" + appl_id1.Trim() + "','" + appl_id2.Trim() + "','" + rtgs + "','" + dt.ToString("MM/dd/yyyy") + "','" + acctype + "','" + accholder + "','" + accno + "','" + mobile + "','" + offphone + "','" + Micr + "','" + collegestatpop + "')";
                    int inscount = d2.update_method_wo_parameter(insertq, "Text");
                    bindbankname();
                    btn_go_Click(sender, e);
                    clear();
                    popwindow.Visible = true;
                    imgdiv2.Visible = true;
                    lbl_alert.Visible = true;
                    lbl_alert.Text = "Saved Successfully";
                    ViewState["staffapplid"] = "";
                    ViewState["applid2"] = "";
                }
            }
        }
        catch
        {

        }
    }
    protected void btnsave_click(object sender, EventArgs e)
    {

    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        imgdiv1.Visible = true;
        lblalert.Visible = true;
        lblalert.Text = "Do you want to delete this record?";
    }
    protected void btnyes_Click(object sender, EventArgs e)
    {
        string activerow = Fpspread1.ActiveSheetView.ActiveRow.ToString();
        string activecol = Fpspread1.ActiveSheetView.ActiveColumn.ToString();

        string nameofbank = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(activerow), 1].Text);
        string codebank = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(activerow), 2].Tag);
        string numacc = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(activerow), 3].Text);
        string acctype = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(activerow), 3].Tag);

        string selquery = "select * from FM_BankChqDet where BankFK='" + codebank + "'";
        ds.Clear();
        ds = d2.select_method_wo_parameter(selquery, "Text");
        if (ds.Tables[0].Rows.Count > 0)
        {
            imgdiv1.Visible = false;
            lblalert.Visible = false;
            imgdiv2.Visible = true;
            lbl_alert.Visible = true;
            lbl_alert.Text = "You can't delete this record";
        }
        else
        {
            string delquery = "delete from FM_FinBankMaster where BankPk='" + codebank + "' and CollegeCode='" + collegestatpop + "'";
            int delcount = d2.update_method_wo_parameter(delquery, "Text");
            bindbankname();
            btn_go_Click(sender, e);
            popwindow.Visible = false;
            imgdiv1.Visible = false;
            imgdiv2.Visible = true;
            lblalert.Visible = false;
            lbl_alert.Visible = true;
            lbl_alert.Text = "Deleted Successfully";
        }
    }
    protected void btnno_Click(object sender, EventArgs e)
    {
        imgdiv1.Visible = false;
        lblalert.Visible = false;
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        string acc = "";
        string signauth1 = "";
        string signauth2 = "";
        string activerow = Fpspread1.ActiveSheetView.ActiveRow.ToString();
        string activecol = Fpspread1.ActiveSheetView.ActiveColumn.ToString();

        string actrow = Convert.ToString(Fpstaff.ActiveSheetView.ActiveRow);
        string actcol = Convert.ToString(Fpstaff.ActiveSheetView.ActiveColumn);

        string currrow = Convert.ToString(FpSpread2.ActiveSheetView.ActiveRow);
        string currcol = Convert.ToString(FpSpread2.ActiveSheetView.ActiveColumn);

        string nameofbank = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(activerow), 1].Text);
        string codebank = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(activerow), 1].Tag);
        string bankid = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(activerow), 2].Tag);
        string numacc = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(activerow), 3].Text);
        string acctype = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(activerow), 3].Tag);
        string clgid = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(activerow), 4].Tag);

        signauth1 = Convert.ToString(ViewState["staffapplid"]);
        signauth2 = Convert.ToString(ViewState["applid2"]);

        string bankcode = txt_bankcode.Text.ToString();
        string bankname = txt_bkname.Text.ToString();
        bankname = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(bankname);
        string street = txt_str.Text.ToString();
        street = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(street);
        string area = Convert.ToString(ddl_dis.SelectedItem.Value);
        area = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(area);
        string city = txt_city.Text.ToString();
        string state = Convert.ToString(ddl_state.SelectedItem.Value);
        string pin = txt_pincode.Text.ToString();
        string ifsc = txt_ifsc.Text.ToString();
        string branch = txt_branch.Text.ToString();
        string sign1 = txt_saname1.Text.ToString();
        string sign2 = txt_saname2.Text.ToString();
        //string[] splcode = new string[2];
        //string staffcode1 = "";
        //string staffcode2 = "";
        //string signcode1 = txt_saname1.Text.ToString();
        //if (signcode1.Trim() != "")
        //{
        //    string selq = "select appl_id from staff_appl_master a,staffmaster s where a.appl_no =s.appl_no and s.staff_code in('" + signauth1 + "') and s.college_code='" + collegestatpop + "'";
        //    staffcode1 = d2.GetFunction(selq);
        //}
        //else
        //{
        //    staffcode1 = "";
        //}

        //string signcode2 = txt_saname2.Text.ToString();
        //if (signcode2.Trim() != "")
        //{
        //    string selq = "select appl_id from staff_appl_master a,staffmaster s where a.appl_no =s.appl_no and s.staff_code in('" + signauth2 + "') and s.college_code='" + collegestatpop + "'";
        //    staffcode2 = d2.GetFunction(selq);
        //}
        //else
        //{
        //    staffcode2 = "";
        //}
        string rtgs = txt_rtgs.Text.ToString();

        string accdate = txt_accrdate.Text;
        DateTime dt = new DateTime();
        string createdate = Convert.ToString(accdate);
        string[] split = createdate.Split('/');
        dt = Convert.ToDateTime(split[1] + "/" + split[0] + "/" + split[2]);

        if (rdo_cac.Checked == true)
        {
            acc = "1";
        }
        if (rdo_sac.Checked == true)
        {
            acc = "2";
        }
        string accholder = txt_acholdrname.Text.ToString();
        string accno = txt_acnumbr.Text.ToString();
        //string contact = txt_contper.Text.ToString();
        string mobile = txt_mblno.Text.ToString();
        string offphone = txt_ofcno.Text.ToString();
        //string sadesig1 = txt_sadesg1.Text.ToString();
        //string sadesig2 = txt_sadesg2.Text.ToString();
        string Micr = txt_micr.Text.ToString();
        string selqu = "select AccNo from FM_FinBankMaster where AccNo='" + accno + "' and CollegeCode='" + collegestatpop + "' and BankPK not in('" + bankid + "')";
        ds.Clear();
        ds = d2.select_method_wo_parameter(selqu, "Text");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                imgdiv2.Visible = true;
                lbl_alert.Visible = true;
                lbl_alert.Text = "Account Number already Exist!";
            }
            else
            {
                string updatequery = "if exists (select * from FM_FinBankMaster where BankPK ='" + bankid + "' and CollegeCode='" + collegestatpop + "') update FM_FinBankMaster set BankCode='" + bankcode + "',BankName='" + bankname + "',BankBranch='" + branch + "',Street='" + street + "',District='" + area + "',City='" + city + "',State='" + state + "',PinCode='" + pin + "',IFSCCode='" + ifsc + "',SignAuthorityStaff1='" + signauth1 + "',SignAuthorityStaff2='" + signauth2 + "',RTGSCode='" + rtgs + "',AccStartDate='" + dt.ToString("MM/dd/yyyy") + "',AccType='" + acc + "',AccHolderName='" + accholder + "',AccNo='" + accno + "',MobileNo='" + mobile + "',PhoneNo='" + offphone + "',MCRCode='" + Micr + "' where BankPK='" + bankid + "' and CollegeCode='" + clgid + "' else Insert into FM_FinBankMaster(BankCode,BankName,BankBranch,Street,District,City,State,PinCode,IFSCCode,SignAuthorityStaff1,SignAuthorityStaff2,RTGSCode,AccStartDate,AccType,AccHolderName,AccNo,MobileNo,PhoneNo,MCRCode,CollegeCode) values('" + bankcode + "','" + bankname + "','" + branch + "','" + street + "','" + area + "','" + city + "','" + state + "','" + pin + "','" + ifsc + "','" + signauth1.Trim() + "','" + signauth2.Trim() + "','" + rtgs + "','" + dt.ToString("MM/dd/yyyy") + "','" + acctype + "','" + accholder + "','" + accno + "','" + mobile + "','" + offphone + "','" + Micr + "','" + collegestatpop + "')";
                int count = d2.update_method_wo_parameter(updatequery, "Text");
                bindbankname();
                btn_go_Click(sender, e);
                popwindow.Visible = false;
                imgdiv2.Visible = true;
                lbl_alert.Visible = true;
                lbl_alert.Text = "Updated Successfully";
                ViewState["staffapplid"] = "";
                ViewState["applid2"] = "";
            }
        }
    }
    protected void btn_exit_Click(object sender, EventArgs e)
    {
        popwindow.Visible = false;
    }
    protected void btnexit_click(object sender, EventArgs e)
    {
        popupsscode2.Visible = false;
    }
    protected void btn_okbnk_Click(object sender, EventArgs e)
    {

    }
    protected void btn_oksa_Click(object sender, EventArgs e)
    {

    }
    protected void btn_exitsa_Click(object sender, EventArgs e)
    {

    }
    protected void rdo_staff_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void rdo_staff1_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void rdo_both_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void rdo_both1_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void rdo_managmt_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void rdo_managmt1_CheckedChanged(object sender, EventArgs e)
    {

    }

    //staff selection
    protected void imagebtnpopclose3_Click(object sender, EventArgs e)
    {
        popupsscode2.Visible = false;
    }
    protected void imagebtnpopclose2_Click(object sender, EventArgs e)
    {
        popupsscode1.Visible = false;
    }
    protected void imagebtnpopclose_Click(object sender, EventArgs e)
    {
        popwindow.Visible = false;
    }
    protected void btn_save1_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    //string dept = "";
        //    //string desg = "";
        //    //string wardencode = "";           
        //    string activerow = "";
        //    string activecol = "";
        //    //if (Fpstaff.Sheets[0].RowCount != 0)
        //    //{
        //    //    activerow = Fpstaff.ActiveSheetView.ActiveRow.ToString();
        //    //    activecol = Fpstaff.ActiveSheetView.ActiveColumn.ToString();
        //        if (activerow != Convert.ToString(-1))
        //        {
        //            //if (checkvalue == "wardenvalue")
        //            //{
        //                //if (txt_searchby.Text == "" || txt_searchby.Text != "")
        //                //{
        //                //    string name = Fpstaff.Sheets[0].Cells[Convert.ToInt32(activerow), 2].Text;
        //                //    txt_warden.Text = name;
        //                //    dept = Fpstaff.Sheets[0].Cells[Convert.ToInt32(activerow), 3].Text;
        //                //    txt_department.Text = dept;
        //                //    desg = Fpstaff.Sheets[0].Cells[Convert.ToInt32(activerow), 4].Text;
        //                //    txt_designation.Text = desg;
        //                //    wardencode = Convert.ToString(Fpstaff.Sheets[0].Cells[Convert.ToInt32(activerow), 1].Text);
        //                //}
        //                //ViewState["WardenCode"] = Convert.ToString(wardencode);
        //            //}                   

        //            popupsscode1.Visible = false;
        //        }
        //        else
        //        {
        //            lbl_errorsearch.Visible = true;
        //            lbl_errorsearch.Text = "Please Select Any One Staff";
        //        }

        //    else
        //    {
        //        lbl_errorsearch1.Visible = true;
        //        lbl_errorsearch1.Text = "No Records Found";
        //        //Fpstaff.Visible = false;
        //    }
        //}
        //catch (Exception ex)
        //{

        //}
    }
    protected void btn_exit2_Click(object sender, EventArgs e)
    {
        try
        {
            popupsscode1.Visible = false;
        }
        catch
        {
        }
    }

    protected void Cell_Click(object sender, EventArgs e)
    {
        try
        {
            spread1_Click = true;
        }
        catch
        {

        }
    }

    protected void Fpspread1_render(object sender, EventArgs e)
    {
        try
        {
            if (spread1_Click == true)
            {
                lnkadd.Visible = true;
                popwindow.Visible = true;
                btn_delete.Visible = true;
                btn_update.Visible = true;
                btn_save.Visible = false;
                div01.Visible = true;

                string mainrow = Fpspread1.ActiveSheetView.ActiveRow.ToString();
                string maincol = Fpspread1.ActiveSheetView.ActiveColumn.ToString();

                if (mainrow.Trim() != "")
                {
                    string nameofbank = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(mainrow), 1].Text);
                    string bankid = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(mainrow), 2].Tag);
                    ViewState["bankid"] = Convert.ToString(bankid);
                    string codebank = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(mainrow), 1].Tag);
                    string numacc = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(mainrow), 3].Text);
                    string acctype = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(mainrow), 3].Tag);
                    string clgid = Convert.ToString(Fpspread1.Sheets[0].Cells[Convert.ToInt32(mainrow), 4].Tag);
                    string selquery = "select BankCode,BankName,BankBranch,Street,District,City,State,PinCode,IFSCCode,SignAuthorityStaff1,SignAuthorityStaff2,RTGSCode,(CONVERT(varchar(10), AccStartDate,103)) as acctcreate,AccType,AccHolderName,AccNo,MobileNo,PhoneNo,MCRCode from FM_FinBankMaster where BankPK='" + bankid + "' and CollegeCode='" + clgid + "'";
                    ds.Clear();
                    ds = d2.select_method_wo_parameter(selquery, "Text");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlpopclg.SelectedValue = clgid;
                        txt_bankcode.Text = codebank;
                        txt_bkname.Text = nameofbank;
                        txt_branch.Text = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                        txt_str.Text = ds.Tables[0].Rows[0]["Street"].ToString();
                        string dis = ds.Tables[0].Rows[0]["District"].ToString();
                        if (dis.Trim() != "")
                        {
                            loaddistrict();
                            try
                            {
                                ddl_dis.SelectedValue = ds.Tables[0].Rows[0]["District"].ToString();
                            }
                            catch { ddl_dis.SelectedIndex = 0; }
                            loadstate();
                        }
                        else
                        {
                            loaddistrict();
                            ddl_dis.SelectedIndex = 0;
                            loadstate();
                        }
                        txt_city.Text = ds.Tables[0].Rows[0]["City"].ToString();
                        //string state = ds.Tables[0].Rows[0]["State"].ToString();
                        //if (state.Trim() != "")
                        //{
                        //    ddl_state.SelectedValue = ds.Tables[0].Rows[0]["State"].ToString();
                        //}
                        //else
                        //{
                        //    ddl_state.SelectedIndex = 0;
                        //}
                        txt_pincode.Text = ds.Tables[0].Rows[0]["PinCode"].ToString();
                        txt_ifsc.Text = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                        string sig1 = ds.Tables[0].Rows[0]["SignAuthorityStaff1"].ToString();
                        ViewState["staffapplid"] = Convert.ToString(sig1);
                        string selq1 = "select staff_name from staffmaster s,staff_appl_master a where s.appl_no=a.appl_no and a.appl_id='" + sig1 + "' and s.college_code='" + clgid + "'";
                        string staffname = d2.GetFunction(selq1);
                        if (staffname.Trim() != "" && staffname.Trim() != "0")
                        {
                            txt_saname1.Text = staffname;
                        }
                        else
                        {
                            txt_saname1.Text = "";
                        }

                        string sig2 = ds.Tables[0].Rows[0]["SignAuthorityStaff2"].ToString();
                        ViewState["applid2"] = Convert.ToString(sig2);
                        string selq2 = "select staff_name from staffmaster s,staff_appl_master a where s.appl_no=a.appl_no and a.appl_id='" + sig2 + "' and s.college_code='" + clgid + "'";
                        string staffname1 = d2.GetFunction(selq2);
                        if (staffname1.Trim() != "" && staffname1.Trim() != "0")
                        {
                            txt_saname2.Text = staffname1;
                        }
                        else
                        {
                            txt_saname2.Text = "";
                        }

                        txt_rtgs.Text = ds.Tables[0].Rows[0]["RTGSCode"].ToString();
                        txt_accrdate.Text = ds.Tables[0].Rows[0]["acctcreate"].ToString();
                        string acc = ds.Tables[0].Rows[0]["AccType"].ToString();
                        if (acc.Trim() == "1")
                        {
                            rdo_cac.Checked = true;
                            rdo_sac.Checked = false;
                        }
                        if (acc.Trim() == "2")
                        {
                            rdo_sac.Checked = true;
                            rdo_cac.Checked = false;
                        }
                        txt_acholdrname.Text = ds.Tables[0].Rows[0]["AccHolderName"].ToString();
                        txt_acnumbr.Text = ds.Tables[0].Rows[0]["AccNo"].ToString();
                        //txt_contper.Text = ds.Tables[0].Rows[0]["contactperson"].ToString();
                        txt_mblno.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        txt_ofcno.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                        //txt_sadesg1.Text = ds.Tables[0].Rows[0]["sign_auth_deg1"].ToString();
                        //txt_sadesg2.Text = ds.Tables[0].Rows[0]["sign_auth_deg2"].ToString();
                        txt_micr.Text = ds.Tables[0].Rows[0]["MCRCode"].ToString();
                        ddlpopclg.SelectedIndex = ddlpopclg.Items.IndexOf(ddlpopclg.Items.FindByValue(clgid));
                        ddl_dis.SelectedIndex = ddl_dis.Items.IndexOf(ddl_dis.Items.FindByValue(ddl_dis.SelectedValue));
                        ddl_state.SelectedIndex = ddl_state.Items.IndexOf(ddl_state.Items.FindByValue(ddl_state.SelectedValue));
                    }
                }
            }
        }
        catch
        {

        }
    }

    protected void staffcell4_Click(object sender, EventArgs e)
    {
        try
        {
            staff2_click = true;
        }
        catch
        {

        }
    }

    protected void staffcell_Click(object sender, EventArgs e)
    {
        try
        {
            staff_click = true;
        }
        catch
        {

        }
    }

    protected void Fpstaff4_render(object sender, EventArgs e)
    {
        try
        {
            if (staff2_click == true)
            {
                popchqdet.Visible = true;
                string actrow = "";
                string actcol = "";
                actrow = FpSpread4.ActiveSheetView.ActiveRow.ToString();
                actcol = FpSpread4.ActiveSheetView.ActiveColumn.ToString();
                if (actrow.Trim() != "")
                {
                    string staff = Convert.ToString(FpSpread4.Sheets[0].Cells[Convert.ToInt32(actrow), 2].Text) + "-" + Convert.ToString(FpSpread4.Sheets[0].Cells[Convert.ToInt32(actrow), 1].Text);
                    txtstaff.Text = staff;
                    popchqstaff.Visible = false;
                }
            }
        }
        catch
        {

        }
    }

    protected void Fpstaff_render(object sender, EventArgs e)
    {
        try
        {
            if (staff_click == true)
            {
                popwindow.Visible = true;
                string actrow = "";
                string actcol = "";
                actrow = Fpstaff.ActiveSheetView.ActiveRow.ToString();
                actcol = Fpstaff.ActiveSheetView.ActiveColumn.ToString();
                if (actrow.Trim() != "")
                {
                    string staff = Convert.ToString(Fpstaff.Sheets[0].Cells[Convert.ToInt32(actrow), 2].Text);
                    string staffcode = Convert.ToString(Fpstaff.Sheets[0].Cells[Convert.ToInt32(actrow), 1].Tag);
                    ViewState["staffapplid"] = Convert.ToString(Fpstaff.Sheets[0].Cells[Convert.ToInt32(actrow), 1].Tag);
                    txt_saname1.Text = staff;
                    popupsscode1.Visible = false;
                }
            }
        }
        catch
        {

        }
    }
    protected void staffcell1_Click(object sender, EventArgs e)
    {
        try
        {
            staff1_click = true;
        }
        catch
        {

        }
    }
    protected void Fpstaff1_render(object sender, EventArgs e)
    {
        try
        {
            if (staff1_click == true)
            {
                popwindow.Visible = true;
                string actrow = "";
                string actcol = "";
                actrow = FpSpread2.ActiveSheetView.ActiveRow.ToString();
                actcol = FpSpread2.ActiveSheetView.ActiveColumn.ToString();
                if (actrow.Trim() != "")
                {
                    string staff = Convert.ToString(FpSpread2.Sheets[0].Cells[Convert.ToInt32(actrow), 2].Text);
                    ViewState["applid2"] = Convert.ToString(FpSpread2.Sheets[0].Cells[Convert.ToInt32(actrow), 1].Tag);
                    txt_saname2.Text = staff;
                    popupsscode2.Visible = false;
                }
            }
        }
        catch
        {

        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string reportname = txtexcelname.Text;
            if (reportname.ToString().Trim() != "")
            {
                d2.printexcelreport(Fpspread1, reportname);
                lblvalidation1.Visible = false;
            }
            else
            {
                lblvalidation1.Text = "Please Enter Your Report Name";
                lblvalidation1.Visible = true;
                txtexcelname.Focus();
            }
        }
        catch
        {

        }
    }

    protected void btnprintmaster_Click(object sender, EventArgs e)
    {
        try
        {
            string degreedetails = "Bank Report";
            string pagename = "BankMaster.aspx";
            Printcontrol.loadspreaddetails(Fpspread1, pagename, degreedetails);
            Printcontrol.Visible = true;
        }
        catch
        {

        }
    }

    protected void btngostaff_Click(object sender, EventArgs e)
    {
        try
        {
            popchqstaff.Visible = true;
            if (collegecode1 != null)
            {

                string selq = "";
                if (txtsearch3.Text.Trim() != "")
                {
                    selq = "select staff_code,staff_name from staffmaster s,staff_appl_master a where s.appl_no=a.appl_no and s.college_code='" + collegestatpop + "' and staff_name='" + Convert.ToString(txtsearch3.Text) + "'";
                }
                else
                {
                    selq = "select staff_code,staff_name from staffmaster s,staff_appl_master a where s.appl_no=a.appl_no and s.college_code ='" + collegestatpop + "' order by PrintPriority";
                }
                ds.Clear();
                ds = d2.select_method_wo_parameter(selq, "Text");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    FpSpread4.Sheets[0].RowCount = 0;
                    FpSpread4.Sheets[0].ColumnCount = 0;
                    FpSpread4.CommandBar.Visible = false;
                    FpSpread4.Sheets[0].AutoPostBack = true;
                    FpSpread4.Sheets[0].ColumnHeader.RowCount = 1;
                    FpSpread4.Sheets[0].RowHeader.Visible = false;
                    FpSpread4.Sheets[0].ColumnCount = 3;

                    FarPoint.Web.Spread.StyleInfo darkstyle = new FarPoint.Web.Spread.StyleInfo();
                    darkstyle.BackColor = ColorTranslator.FromHtml("#0CA6CA");
                    darkstyle.ForeColor = Color.White;
                    FpSpread4.ActiveSheetView.ColumnHeader.DefaultStyle = darkstyle;

                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 0].Text = "S.No";
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 0].Font.Bold = true;
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 0].Font.Name = "Book Antiqua";
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 0].Font.Size = FontUnit.Medium;
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 0].HorizontalAlign = HorizontalAlign.Center;

                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 1].Text = "Staff Code";
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 1].Font.Bold = true;
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 1].Font.Name = "Book Antiqua";
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 1].Font.Size = FontUnit.Medium;
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 1].HorizontalAlign = HorizontalAlign.Center;

                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 2].Text = "Staff Name";
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 2].Font.Bold = true;
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 2].Font.Name = "Book Antiqua";
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 2].Font.Size = FontUnit.Medium;
                    FpSpread4.Sheets[0].ColumnHeader.Cells[0, 2].HorizontalAlign = HorizontalAlign.Center;

                    for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                    {
                        FpSpread4.Sheets[0].RowCount++;
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 0].Text = Convert.ToString(row + 1);
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Center;
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 0].Font.Size = FontUnit.Medium;
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 0].Font.Name = "Book Antiqua";

                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 1].Text = Convert.ToString(ds.Tables[0].Rows[row]["staff_code"]);
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 1].HorizontalAlign = HorizontalAlign.Left;
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 1].Font.Size = FontUnit.Medium;
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 1].Font.Name = "Book Antiqua";

                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 2].Text = Convert.ToString(ds.Tables[0].Rows[row]["staff_name"]);
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 2].HorizontalAlign = HorizontalAlign.Left;
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 2].Font.Size = FontUnit.Medium;
                        FpSpread4.Sheets[0].Cells[FpSpread4.Sheets[0].RowCount - 1, 2].Font.Name = "Book Antiqua";

                    }
                    FpSpread4.Visible = true;
                    div2.Visible = true;
                    lblerr1.Visible = false;
                    FpSpread4.Sheets[0].PageSize = FpSpread4.Sheets[0].RowCount;
                }
            }
        }
        catch
        {
        }
    }

    protected void btn_go2_Click(object sender, EventArgs e)
    {
        try
        {
            popupsscode1.Visible = true;
            if (collegecode1 != null)
            {
                string selq = "";
                if (txtsearch1.Text.Trim() != "")
                {
                    selq = "select staff_code,staff_name,a.appl_id from staffmaster s,staff_appl_master a where s.appl_no=a.appl_no and s.college_code='" + collegestatpop + "' and staff_name='" + Convert.ToString(txtsearch1.Text) + "'";
                }
                else if (txtsearch1c.Text.Trim() != "")
                {
                    selq = "select staff_code,staff_name,a.appl_id from staffmaster s,staff_appl_master a where s.appl_no=a.appl_no and s.college_code='" + collegestatpop + "' and staff_code='" + Convert.ToString(txtsearch1c.Text) + "'";
                }
                else
                {
                    selq = "select staff_code,staff_name,a.appl_id from staffmaster s,staff_appl_master a where s.appl_no=a.appl_no and s.college_code='" + collegestatpop + "' order by PrintPriority";
                }
                ds.Clear();
                ds = d2.select_method_wo_parameter(selq, "Text");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Fpstaff.Sheets[0].RowCount = 0;
                    Fpstaff.Sheets[0].ColumnCount = 0;
                    Fpstaff.CommandBar.Visible = false;
                    Fpstaff.Sheets[0].AutoPostBack = true;
                    Fpstaff.Sheets[0].ColumnHeader.RowCount = 1;
                    Fpstaff.Sheets[0].RowHeader.Visible = false;
                    Fpstaff.Sheets[0].ColumnCount = 3;

                    FarPoint.Web.Spread.StyleInfo darkstyle = new FarPoint.Web.Spread.StyleInfo();
                    darkstyle.BackColor = ColorTranslator.FromHtml("#0CA6CA");
                    darkstyle.ForeColor = Color.White;
                    Fpstaff.ActiveSheetView.ColumnHeader.DefaultStyle = darkstyle;

                    Fpstaff.Sheets[0].ColumnHeader.Cells[0, 0].Text = "S.No";
                    Fpstaff.Sheets[0].ColumnHeader.Cells[0, 0].Font.Bold = true;
                    Fpstaff.Sheets[0].ColumnHeader.Cells[0, 0].Font.Name = "Book Antiqua";
                    Fpstaff.Sheets[0].ColumnHeader.Cells[0, 0].Font.Size = FontUnit.Medium;
                    Fpstaff.Sheets[0].ColumnHeader.Cells[0, 0].HorizontalAlign = HorizontalAlign.Center;

                    Fpstaff.Sheets[0].ColumnHeader.Cells[0, 1].Text = "Staff Code";
                    Fpstaff.Sheets[0].ColumnHeader.Cells[0, 1].Font.Bold = true;
                    Fpstaff.Sheets[0].ColumnHeader.Cells[0, 1].Font.Name = "Book Antiqua";
                    Fpstaff.Sheets[0].ColumnHeader.Cells[0, 1].Font.Size = FontUnit.Medium;
                    Fpstaff.Sheets[0].ColumnHeader.Cells[0, 1].HorizontalAlign = HorizontalAlign.Center;

                    Fpstaff.Sheets[0].ColumnHeader.Cells[0, 2].Text = "Staff Name";
                    Fpstaff.Sheets[0].ColumnHeader.Cells[0, 2].Font.Bold = true;
                    Fpstaff.Sheets[0].ColumnHeader.Cells[0, 2].Font.Name = "Book Antiqua";
                    Fpstaff.Sheets[0].ColumnHeader.Cells[0, 2].Font.Size = FontUnit.Medium;
                    Fpstaff.Sheets[0].ColumnHeader.Cells[0, 2].HorizontalAlign = HorizontalAlign.Center;

                    for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                    {
                        Fpstaff.Sheets[0].RowCount++;
                        Fpstaff.Sheets[0].Cells[Fpstaff.Sheets[0].RowCount - 1, 0].Text = Convert.ToString(row + 1);
                        Fpstaff.Sheets[0].Cells[Fpstaff.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Center;
                        Fpstaff.Sheets[0].Cells[Fpstaff.Sheets[0].RowCount - 1, 0].Font.Size = FontUnit.Medium;
                        Fpstaff.Sheets[0].Cells[Fpstaff.Sheets[0].RowCount - 1, 0].Font.Name = "Book Antiqua";

                        Fpstaff.Sheets[0].Cells[Fpstaff.Sheets[0].RowCount - 1, 1].Text = Convert.ToString(ds.Tables[0].Rows[row]["staff_code"]);
                        Fpstaff.Sheets[0].Cells[Fpstaff.Sheets[0].RowCount - 1, 1].Tag = Convert.ToString(ds.Tables[0].Rows[row]["appl_id"]);
                        Fpstaff.Sheets[0].Cells[Fpstaff.Sheets[0].RowCount - 1, 1].HorizontalAlign = HorizontalAlign.Left;
                        Fpstaff.Sheets[0].Cells[Fpstaff.Sheets[0].RowCount - 1, 1].Font.Size = FontUnit.Medium;
                        Fpstaff.Sheets[0].Cells[Fpstaff.Sheets[0].RowCount - 1, 1].Font.Name = "Book Antiqua";

                        Fpstaff.Sheets[0].Cells[Fpstaff.Sheets[0].RowCount - 1, 2].Text = Convert.ToString(ds.Tables[0].Rows[row]["staff_name"]);
                        Fpstaff.Sheets[0].Cells[Fpstaff.Sheets[0].RowCount - 1, 2].HorizontalAlign = HorizontalAlign.Left;
                        Fpstaff.Sheets[0].Cells[Fpstaff.Sheets[0].RowCount - 1, 2].Font.Size = FontUnit.Medium;
                        Fpstaff.Sheets[0].Cells[Fpstaff.Sheets[0].RowCount - 1, 2].Font.Name = "Book Antiqua";

                    }
                    Fpstaff.Visible = true;
                    div2.Visible = true;
                    lblerr1.Visible = false;
                    Fpstaff.Sheets[0].PageSize = Fpstaff.Sheets[0].RowCount;
                }
            }
        }
        catch
        {
        }
    }


    protected void btn_go3_Click(object sender, EventArgs e)
    {
        try
        {
            popupsscode2.Visible = true;
            if (collegecode1 != null)
            {

                string selq = "";
                if (txtsearch2.Text.Trim() != "")
                {
                    selq = "select staff_code,staff_name,a.appl_id from staffmaster s,staff_appl_master a where s.appl_no=a.appl_no and s.college_code='" + collegestatpop + "' and staff_name='" + Convert.ToString(txtsearch2.Text) + "'";
                }
                else if (txtsearch2c.Text.Trim() != "")
                {
                    selq = "select staff_code,staff_name,a.appl_id from staffmaster s,staff_appl_master a where s.appl_no=a.appl_no and s.college_code='" + collegestatpop + "' and staff_code='" + Convert.ToString(txtsearch2c.Text) + "'";
                }
                else
                {
                    selq = "select staff_code,staff_name,a.appl_id from staffmaster s,staff_appl_master a where s.appl_no=a.appl_no and s.college_code='" + collegestatpop + "' order by PrintPriority";
                }
                ds.Clear();
                ds = d2.select_method_wo_parameter(selq, "Text");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    FpSpread2.Sheets[0].RowCount = 0;
                    FpSpread2.Sheets[0].ColumnCount = 0;
                    FpSpread2.CommandBar.Visible = false;
                    FpSpread2.Sheets[0].AutoPostBack = true;
                    FpSpread2.Sheets[0].ColumnHeader.RowCount = 1;
                    FpSpread2.Sheets[0].RowHeader.Visible = false;
                    FpSpread2.Sheets[0].ColumnCount = 3;

                    FarPoint.Web.Spread.StyleInfo darkstyle = new FarPoint.Web.Spread.StyleInfo();
                    darkstyle.BackColor = ColorTranslator.FromHtml("#0CA6CA");
                    darkstyle.ForeColor = Color.White;
                    FpSpread2.ActiveSheetView.ColumnHeader.DefaultStyle = darkstyle;

                    FpSpread2.Sheets[0].ColumnHeader.Cells[0, 0].Text = "S.No";
                    FpSpread2.Sheets[0].ColumnHeader.Cells[0, 0].Font.Bold = true;
                    FpSpread2.Sheets[0].ColumnHeader.Cells[0, 0].Font.Name = "Book Antiqua";
                    FpSpread2.Sheets[0].ColumnHeader.Cells[0, 0].Font.Size = FontUnit.Medium;
                    FpSpread2.Sheets[0].ColumnHeader.Cells[0, 0].HorizontalAlign = HorizontalAlign.Center;

                    FpSpread2.Sheets[0].ColumnHeader.Cells[0, 1].Text = "Staff Code";
                    FpSpread2.Sheets[0].ColumnHeader.Cells[0, 1].Font.Bold = true;
                    FpSpread2.Sheets[0].ColumnHeader.Cells[0, 1].Font.Name = "Book Antiqua";
                    FpSpread2.Sheets[0].ColumnHeader.Cells[0, 1].Font.Size = FontUnit.Medium;
                    FpSpread2.Sheets[0].ColumnHeader.Cells[0, 1].HorizontalAlign = HorizontalAlign.Center;

                    FpSpread2.Sheets[0].ColumnHeader.Cells[0, 2].Text = "Staff Name";
                    FpSpread2.Sheets[0].ColumnHeader.Cells[0, 2].Font.Bold = true;
                    FpSpread2.Sheets[0].ColumnHeader.Cells[0, 2].Font.Name = "Book Antiqua";
                    FpSpread2.Sheets[0].ColumnHeader.Cells[0, 2].Font.Size = FontUnit.Medium;
                    FpSpread2.Sheets[0].ColumnHeader.Cells[0, 2].HorizontalAlign = HorizontalAlign.Center;

                    for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                    {
                        FpSpread2.Sheets[0].RowCount++;
                        FpSpread2.Sheets[0].Cells[FpSpread2.Sheets[0].RowCount - 1, 0].Text = Convert.ToString(row + 1);
                        FpSpread2.Sheets[0].Cells[FpSpread2.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Center;
                        FpSpread2.Sheets[0].Cells[FpSpread2.Sheets[0].RowCount - 1, 0].Font.Size = FontUnit.Medium;
                        FpSpread2.Sheets[0].Cells[FpSpread2.Sheets[0].RowCount - 1, 0].Font.Name = "Book Antiqua";

                        FpSpread2.Sheets[0].Cells[FpSpread2.Sheets[0].RowCount - 1, 1].Text = Convert.ToString(ds.Tables[0].Rows[row]["staff_code"]);
                        FpSpread2.Sheets[0].Cells[FpSpread2.Sheets[0].RowCount - 1, 1].Tag = Convert.ToString(ds.Tables[0].Rows[row]["appl_id"]);
                        FpSpread2.Sheets[0].Cells[FpSpread2.Sheets[0].RowCount - 1, 1].HorizontalAlign = HorizontalAlign.Left;
                        FpSpread2.Sheets[0].Cells[FpSpread2.Sheets[0].RowCount - 1, 1].Font.Size = FontUnit.Medium;
                        FpSpread2.Sheets[0].Cells[FpSpread2.Sheets[0].RowCount - 1, 1].Font.Name = "Book Antiqua";

                        FpSpread2.Sheets[0].Cells[FpSpread2.Sheets[0].RowCount - 1, 2].Text = Convert.ToString(ds.Tables[0].Rows[row]["staff_name"]);
                        FpSpread2.Sheets[0].Cells[FpSpread2.Sheets[0].RowCount - 1, 2].HorizontalAlign = HorizontalAlign.Left;
                        FpSpread2.Sheets[0].Cells[FpSpread2.Sheets[0].RowCount - 1, 2].Font.Size = FontUnit.Medium;
                        FpSpread2.Sheets[0].Cells[FpSpread2.Sheets[0].RowCount - 1, 2].Font.Name = "Book Antiqua";

                    }
                    FpSpread2.Visible = true;
                    div2.Visible = true;
                    lblerr1.Visible = false;
                    FpSpread2.Sheets[0].PageSize = FpSpread2.Sheets[0].RowCount;
                }
            }
        }
        catch
        {
        }
    }

    protected void ddl_dis_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_dis.SelectedIndex != 0 && ddl_dis.SelectedItem.Text != "Select")
            {
                loadstate();
            }
        }
        catch
        {

        }
    }

    protected void ddlsearch1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsearch1.SelectedIndex == 0)
        {
            txtsearch1.Visible = true;
            Label1.Text = "Search By Name";
            txtsearch1c.Visible = false;
        }
        else
        {
            txtsearch1c.Visible = true;
            Label1.Text = "Search By Code";
            txtsearch1.Visible = false;
        }
    }

    protected void ddlsearch2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsearch2.SelectedIndex == 0)
        {
            txtsearch2.Visible = true;
            Label2.Text = "Search By Name";
            txtsearch2c.Visible = false;
        }
        else
        {
            txtsearch2c.Visible = true;
            Label2.Text = "Search By Code";
            txtsearch2.Visible = false;
        }
    }

    public void loaddistrict()
    {
        try
        {
            string selq = "select MasterCode,MasterValue from CO_MasterValues where MasterCriteria ='District' and CollegeCode ='" + collegestatpop + "'";
            DataSet ds1 = new DataSet();
            ds1.Clear();
            ds1 = d2.select_method_wo_parameter(selq, "Text");
            if (ds1.Tables.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddl_dis.DataSource = ds1;
                    ddl_dis.DataTextField = "MasterValue";
                    ddl_dis.DataValueField = "MasterCode";
                    ddl_dis.DataBind();
                }
            }
        }
        catch
        {

        }
    }

    public void loadstate()
    {
        try
        {
            string selq = "select MasterCode,MasterCriteriaValue2 from CO_MasterValues where MasterCriteria ='District' and MasterCode ='" + ddl_dis.SelectedItem.Value + "' and CollegeCode ='" + collegestatpop + "'";
            DataSet ds1 = new DataSet();
            ds1.Clear();
            ds1 = d2.select_method_wo_parameter(selq, "Text");
            if (ds1.Tables.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddl_state.DataSource = ds1;
                    ddl_state.DataTextField = "MasterCriteriaValue2";
                    ddl_state.DataValueField = "MasterCode";
                    ddl_state.DataBind();
                }
            }
        }
        catch
        {

        }
    }

    protected void clear()
    {
        txt_bankcode.Text = "";
        txt_bkname.Text = "";
        txt_branch.Text = "";
        txt_str.Text = "";
        txt_city.Text = "";
        txt_pincode.Text = "";
        txt_saname1.Text = "";
        txt_saname2.Text = "";
        txt_rtgs.Text = "";
        txt_accrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txt_acholdrname.Text = "";
        txt_acnumbr.Text = "";
        //txt_contper.Text = "";
        txt_mblno.Text = "";
        txt_ofcno.Text = "";
        //txt_sadesg1.Text = "";
        //txt_sadesg2.Text = "";
        txt_micr.Text = "";
        txt_ifsc.Text = "";
    }

    private void setLabelText()
    {
        string grouporusercode = string.Empty;
        if (Session["group_code"] != null && (Convert.ToString(Session["group_code"]).Trim() != "") && (Convert.ToString(Session["group_code"]).Trim() != "0") && (Convert.ToString(Session["group_code"]).Trim() != "-1"))
        {
            grouporusercode = " group_code=" + Convert.ToString(Session["group_code"]).Trim() + "";
        }
        else if (Session["usercode"] != null)
        {
            grouporusercode = " usercode=" + Convert.ToString(Session["usercode"]).Trim() + "";
        }
        List<Label> lbl = new List<Label>();
        List<byte> fields = new List<byte>();

        lbl.Add(lblcol);
        fields.Add(0);
        lbl.Add(lblcollege);
        fields.Add(0);

        new HeaderLabelText().setLabels(grouporusercode, ref lbl, fields);
    }
    // last modified 04-10-2016 sudhagar
}