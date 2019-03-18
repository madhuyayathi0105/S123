﻿/*
  Code Started by Mohamed Idhris Sheik Dawood on 02/03/2017
*/
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections.Generic;
using InsproDataAccess;
using System.Web.UI;

public partial class FinanceMod_PartPayStudsettings : System.Web.UI.Page
{
    int collegeCode = 0;
    int userCode = 0;
    DataSet dsload = new DataSet();
    DAccess2 DA = new DAccess2();
    InsproDirectAccess dirAcc = new InsproDirectAccess();
    ReuasableMethods reUse = new ReuasableMethods();
    bool flag_true = false;
    string usercode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["collegecode"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                collegeCode = Convert.ToInt32(Convert.ToString(Session["collegecode"]));
                userCode = Convert.ToInt32(Convert.ToString(Session["usercode"]));
                usercode = Convert.ToString(Convert.ToString(Session["usercode"]));
                setLabelText();
                bindCollege();
                updateClgCode();
                bindType();
                bindbatch();
                binddegree();
                bindbranch();
                string ledger = Convert.ToString(getCblSelectedText(chkl_studled));
                if (string.IsNullOrEmpty(ledger))
                {
                    bindsem();
                    single.Visible = true;
                    multiple.Visible = false;
                }
                else
                {
                    bindsemledger();
                    single.Visible = false;
                    multiple.Visible = true;
                }

                bindsec();
                bindledger();
                ddlType_OnIndexChange(sender, e);
            }
            updateClgCode();
            usercode = Convert.ToString(Convert.ToString(Session["usercode"]));
        }
        catch { Response.Redirect("~/Default.aspx"); }
    }
    public void bindCollege()
    {
        try
        {
            DataSet ds = new DataSet();
            ds.Clear();
            ddl_college.Items.Clear();
            string selectQuery = "select cp.college_code,cf.collname from collegeprivilages cp,collinfo cf where user_code=" + userCode + " and cp.college_code=cf.college_code";
            ds = DA.select_method_wo_parameter(selectQuery, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_college.DataSource = ds;
                ddl_college.DataTextField = "collname";
                ddl_college.DataValueField = "college_code";
                ddl_college.DataBind();
            }
        }
        catch (Exception ex) { ddl_college.Items.Clear(); }
    }
    private void updateClgCode()
    {
        try
        {
            if (ddl_college.Items.Count > 0)
            {
                collegeCode = Convert.ToInt32(ddl_college.SelectedItem.Value);
            }
            else
            {
                collegeCode = 13;
            }
            userCode = Convert.ToInt32(Convert.ToString(Session["usercode"]));

        }
        catch { }
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
        lbl.Add(lblCollege);
        fields.Add(0);

        lbl.Add(lbl_stream);
        fields.Add(1);

        lbl.Add(lbl_degree);
        fields.Add(2);

        lbl.Add(lbl_branch);
        fields.Add(3);

        lbl.Add(lbl_Sem);
        fields.Add(4);

        //Name -0, Stream - 1 ,Degree - 2, Branch - 3, Term - 4
        new HeaderLabelText().setLabels(grouporusercode, ref lbl, fields);
    }
    public void bindType()
    {
        try
        {
            lbl_stream.Text = useStreamShift();
            ddl_strm.Items.Clear();
            string query = "select Distinct ISNULL( type,'') as type  from Registration r,Degree d,Department dt,Course c where r.degree_code =d.Degree_Code and d.Dept_Code =dt.Dept_Code and d.Course_Id =c.Course_Id  and type<>''  and r.college_code='" + collegeCode + "'  order by type asc";

            DataSet ds = DA.select_method_wo_parameter(query, "Text");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddl_strm.DataSource = ds;
                ddl_strm.DataTextField = "type";
                ddl_strm.DataValueField = "type";
                ddl_strm.DataBind();
                ddl_strm.Enabled = true;
            }
            else
            {
                ddl_strm.Enabled = false;
            }
        }
        catch (Exception ex) { }
    }
    public void bindbatch()
    {
        try
        {
            ddl_batch.Items.Clear();
            string sqlyear = "select distinct batch_year from Registration where batch_year<>'-1' and batch_year<>'' and cc=0 and delflag=0 and exam_flag<>'debar' order by batch_year desc";
            DataSet ds = DA.select_method_wo_parameter(sqlyear, "Text");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddl_batch.DataSource = ds;
                ddl_batch.DataTextField = "batch_year";
                ddl_batch.DataValueField = "batch_year";
                ddl_batch.DataBind();
            }
        }
        catch (Exception ex) { }
    }
    public void binddegree()
    {
        try
        {
            //ddl_degree.Items.Clear();
            cbl_degree.Items.Clear();
            txt_degree.Text = lbl_degree.Text;
            cb_degree.Checked = true;
            string stream = "";
            stream = ddl_strm.Items.Count > 0 ? ddl_strm.SelectedValue : "";

            string query = "select distinct degree.course_id,course.course_name from degree,course,deptprivilages  where course.course_id=degree.course_id and course.college_code = degree.college_code  and degree.college_code in (" + collegeCode + ") and deptprivilages.Degree_code=degree.Degree_code and   user_code=" + userCode + " ";
            if (ddl_strm.Enabled)
            {
                query += " and course.type in ('" + stream + "')";
            }
            DataSet ds = DA.select_method_wo_parameter(query, "Text");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //ddl_degree.DataSource = ds;
                //ddl_degree.DataTextField = "course_name";
                //ddl_degree.DataValueField = "course_id";
                //ddl_degree.DataBind();

                cbl_degree.DataSource = ds;
                cbl_degree.DataTextField = "course_name";
                cbl_degree.DataValueField = "course_id";
                cbl_degree.DataBind();
                reUse.CallCheckBoxChangedEvent(cbl_degree, cb_degree, txt_degree, lbl_degree.Text);
            }
        }
        catch (Exception ex) { }
    }
    public void bindbranch()
    {
        try
        {
            //ddl_branch.Items.Clear();
            cbl_branch.Items.Clear();
            txt_branch.Text = lbl_branch.Text;
            cb_branch.Checked = true;
            string degree = "";
            degree = reUse.GetSelectedItemsValueAsString(cbl_degree);//ddl_degree.Items.Count > 0 ? ddl_degree.SelectedValue : "";


            string commname = "";
            if (degree != "")
            {
                commname = "select distinct degree.degree_code,department.dept_name,degree.Acronym,department.dept_code  from degree,department,course,deptprivilages where course.course_id=degree.course_id  and department.dept_code=degree.dept_code and course.college_code = degree.college_code and department.college_code = degree.college_code and degree.course_id in('" + degree + "') and deptprivilages.Degree_code=degree.Degree_code ";
            }
            else
            {
                commname = " select distinct degree.degree_code,department.dept_name,degree.Acronym,department.dept_code  from degree,department,course,deptprivilages where course.course_id=degree.course_id  and department.dept_code=degree.dept_code and course.college_code = degree.college_code and department.college_code = degree.college_code and deptprivilages.Degree_code=degree.Degree_code";
            }

            DataSet ds = DA.select_method_wo_parameter(commname, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ddl_branch.DataSource = ds;
                //ddl_branch.DataTextField = "dept_name";
                //ddl_branch.DataValueField = "degree_code";
                //ddl_branch.DataBind();
                cbl_branch.DataSource = ds;
                cbl_branch.DataTextField = "dept_name";
                cbl_branch.DataValueField = "degree_code";
                cbl_branch.DataBind();
                reUse.CallCheckBoxChangedEvent(cbl_branch, cb_branch, txt_branch, lbl_branch.Text);
            }
        }
        catch (Exception ex) { }
    }
    public void bindsem()
    {
        try
        {
            //ddl_sem.Items.Clear();
            cbl_sem.Items.Clear();
            cb_sem.Checked = true;
            txt_sem.Text = lbl_Sem.Text;

            int duration = 0;
            int i = 0;

            string branch = "";
            string batch = "";

            branch = reUse.GetSelectedItemsValueAsString(cbl_branch);//Convert.ToString(ddl_branch.SelectedItem.Value);

            batch = Convert.ToString(ddl_batch.SelectedItem.Value);

            if (branch.Trim() != "" && batch.Trim() != "")
            {
                string strsql1 = "select distinct duration,first_year_nonsemester  from degree where degree_code in ('" + branch + "') and college_code='" + collegeCode + "'";
                DataSet ds = DA.select_method_wo_parameter(strsql1, "text");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string dur = Convert.ToString(ds.Tables[0].Rows[i][0]);
                        if (dur.Trim() != "")
                        {
                            if (duration < Convert.ToInt32(dur))
                            {
                                duration = Convert.ToInt32(dur);
                            }
                        }
                    }
                }
                if (duration != 0)
                {
                    for (i = 1; i <= duration; i++)
                    {
                        //ddl_sem.Items.Add(Convert.ToString(i));
                        cbl_sem.Items.Add(Convert.ToString(i));
                    }
                    reUse.CallCheckBoxChangedEvent(cbl_sem, cb_sem, txt_sem, lbl_Sem.Text);
                }
            }
        }
        catch { }
    }
    public void bindsec()
    {
        try
        {
            //ddl_sec.Items.Clear();
            cbl_sec.Items.Clear();
            cb_sec.Checked = true;
            txt_sec.Text = "Section";

            ListItem item = new ListItem("Empty", " ");

            string batch = "";
            batch = ddl_batch.Items.Count > 0 ? ddl_batch.SelectedValue : "0";
            string branch = "";
            branch = reUse.GetSelectedItemsValue(cbl_branch);//ddl_branch.Items.Count > 0 ? ddl_branch.SelectedValue : "0";
            DataSet dsSec = DA.BindSectionDetail(batch, branch);
            if (dsSec.Tables.Count > 0 && dsSec.Tables[0].Rows.Count > 0)
            {
                //ddl_sec.DataSource = dsSec;
                //ddl_sec.DataTextField = "sections";
                //ddl_sec.DataValueField = "sections";
                //ddl_sec.DataBind();

                cbl_sec.DataSource = dsSec;
                cbl_sec.DataTextField = "sections";
                cbl_sec.DataValueField = "sections";
                cbl_sec.DataBind();
                reUse.CallCheckBoxChangedEvent(cbl_sec, cb_sec, txt_sec, "Section");
            }
        }
        catch (Exception ex) { }
    }
    protected void ddl_college_OnIndexChange(object sender, EventArgs e)
    {
        bindType();
        bindbatch();
        binddegree();
        bindbranch();
        bindsem();
        bindsec();
        //btn_go_Click(sender, e);
    }
    protected void ddl_batch_OnIndexChange(object sender, EventArgs e)
    {
        bindType();
        binddegree();
        bindbranch();
        bindsem();
        bindsec();
        //btn_go_Click(sender, e);
    }
    protected void ddl_strm_OnIndexChange(object sender, EventArgs e)
    {
        binddegree();
        bindbranch();
        bindsem();
        bindsec();
        //btn_go_Click(sender, e);
    }
    protected void ddl_degree_OnIndexChange(object sender, EventArgs e)
    {
        bindbranch();
        bindsem();
        bindsec();
        //btn_go_Click(sender, e);
    }
    protected void cbl_degree_SelectedIndexChanged(object sender, EventArgs e)
    {
        reUse.CallCheckBoxListChangedEvent(cbl_degree, cb_degree, txt_degree, lbl_degree.Text);
        bindbranch();
        bindsem();
        bindsec();
        //btn_go_Click(sender, e);
    }
    protected void cb_degree_ChekedChange(object sender, EventArgs e)
    {
        reUse.CallCheckBoxChangedEvent(cbl_degree, cb_degree, txt_degree, lbl_degree.Text);
        bindbranch();
        bindsem();
        bindsec();
        //btn_go_Click(sender, e);
    }
    protected void ddl_branch_OnIndexChange(object sender, EventArgs e)
    {
        bindsem();
        bindsec();
        //btn_go_Click(sender, e);
    }
    protected void cbl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        reUse.CallCheckBoxListChangedEvent(cbl_branch, cb_branch, txt_branch, lbl_branch.Text);
        bindsem();
        bindsec();
        //btn_go_Click(sender, e);
    }
    protected void cb_branch_ChekedChange(object sender, EventArgs e)
    {
        reUse.CallCheckBoxChangedEvent(cbl_branch, cb_branch, txt_branch, lbl_branch.Text);
        bindsem();
        bindsec();
        //btn_go_Click(sender, e);
    }
    protected void ddl_sem_OnIndexChange(object sender, EventArgs e)
    {
        bindsec();
        //btn_go_Click(sender, e);
    }
    protected void cb_sem_CheckedChanged(object sender, EventArgs e)
    {
        reUse.CallCheckBoxChangedEvent(cbl_sem, cb_sem, txt_sem, lbl_Sem.Text);
        bindsec();
        //btn_go_Click(sender, e);
    }
    protected void cbl_sem_SelectedIndexChanged(object sender, EventArgs e)
    {
        reUse.CallCheckBoxListChangedEvent(cbl_sem, cb_sem, txt_sem, lbl_Sem.Text);
        bindsec();
        //btn_go_Click(sender, e);
    }
    protected void ddl_sec_OnIndexChange(object sender, EventArgs e)
    {
        //btn_go_Click(sender, e);
    }
    protected void cb_sec_ChekedChange(object sender, EventArgs e)
    {
        reUse.CallCheckBoxChangedEvent(cbl_sec, cb_sec, txt_sec, "Section");
        //btn_go_Click(sender, e);
    }
    protected void cbl_sec_SelectedIndexChanged(object sender, EventArgs e)
    {
        reUse.CallCheckBoxListChangedEvent(cbl_sec, cb_sec, txt_sec, "Section");
        //btn_go_Click(sender, e);
    }
    private string useStreamShift()
    {
        string useStrShft = "Stream";
        string streamcode = DA.GetFunction("select value from Master_Settings where settings='Stream/Shift Rights' and usercode='" + userCode + "'").Trim();

        if (streamcode == "" || streamcode == "0")
        {
            useStrShft = "Stream";
        }
        if (streamcode.Trim() == "1")
        {
            useStrShft = "Shift";
        }
        if (streamcode.Trim() == "2")
        {
            useStrShft = "Stream";
        }
        return useStrShft;
    }
    protected void btn_go_Click(object sender, EventArgs e)
    {
        string ledger = Convert.ToString(getCblSelectedText(chkl_studled));
        if (string.IsNullOrEmpty(ledger))
        {
            try
            {
                grid.Visible = true;
                lbl_errormsg.Visible = false;

                string selectquery;

                string branch = reUse.GetSelectedItemsValueAsString(cbl_branch);//ddl_branch.Items.Count > 0 ? ddl_branch.SelectedValue : "";

                string degCode = reUse.GetSelectedItemsValueAsString(cbl_degree);//ddl_degree.Items.Count > 0 ? ddl_degree.SelectedValue : "";

                string stream = ddl_strm.Enabled ? ddl_strm.Items.Count > 0 ? ddl_strm.SelectedItem.Text.Trim() : "" : "";

                string section = reUse.GetSelectedItemsText(cbl_sec);//ddl_sec.Items.Count > 0 ? ddl_sec.SelectedItem.Text.Trim() : "";

                string batch_year = ddl_batch.Items.Count > 0 ? ddl_batch.SelectedItem.Text : "";

                string cusem = reUse.GetSelectedItemsText(cbl_sem);// ddl_sem.Items.Count > 0 ? ddl_sem.SelectedItem.Text : "";

                DataTable dtStud = new DataTable();
                if (batch_year != string.Empty && degCode != string.Empty && branch != string.Empty && cusem != string.Empty)
                {
                    if (stream != string.Empty)
                    {
                        stream = " and c.type in ('" + stream + "')";
                    }
                    string rptType = string.Empty;
                    if (ddlType.SelectedItem.Text == "Part Payment")
                        rptType = ",isPartAmount";
                    else
                        rptType = ",isPartAmount,isPartHold";

                    selectquery = "select r.Roll_No,r.Roll_Admit,r.smart_serial_no,r.Stud_Name,d.Degree_Code,(C.Course_Name +' - '+ dt.Dept_Name) as Department,r.Reg_No,r.App_No,c.type,isnull(r.Sections,'') as Sections, case when a.sex=0 then 'Male' when a.sex=1 then 'Female' when a.sex=2 then 'Transgender' else 'N/A' end as Gender,a.IsFinPartPay" + rptType + "  from Registration r,applyn a,Degree d,Department dt,Course c where r.app_no=a.app_no and r.degree_code =d.Degree_Code and dt.Dept_Code =d.Dept_Code and c.Course_Id =d.Course_Id and CC=0 and DelFlag =0 and Exam_Flag <>'DEBAR' and r.Batch_Year =" + batch_year + " and r.degree_code in ('" + branch + "')  and isnull(r.Sections,'') in ('" + section + "')  and r.current_semester in('" + cusem + "')  and isnull(a.admission_status,'0')='1'   " + stream + "  order by d.Degree_Code,isnull(r.Sections,''),ltrim (r.Roll_No) asc ,ltrim (r.stud_name) asc ";

                    dtStud = dirAcc.selectDataTable(selectquery);
                }
                if (dtStud.Rows.Count > 0)
                {
                    //for (int row = 0; row < dtStud.Rows.Count; row++)
                    //{

                    //}
                    gridStudList.DataSource = dtStud;
                    gridStudList.DataBind();
                    gridStudList.Visible = true;
                    btnSave.Visible = true;
                }
                else
                {
                    gridStudList.DataSource = null;
                    gridStudList.DataBind();
                    gridStudList.Visible = false;
                    btnSave.Visible = false;
                    lbl_errormsg.Visible = true;
                    lbl_errormsg.Text = "No Records Found";
                }
            }


            catch
            {
                gridStudList.DataSource = null;
                gridStudList.DataBind();
                gridStudList.Visible = false;
                btnSave.Visible = false;
                lbl_errormsg.Visible = true;
                lbl_errormsg.Text = "No Records Found";
            }
        }
        else
        {
            bindsemledger();
            dsload = dsvalue();

            single.Visible = false;
            multiple.Visible = true;
            try
            {
                ledgersave.Visible = true;
                Cancelhold.Visible = true;
                grid.Visible = false;
                #region design
                FpSpread1.Visible = true;
                FpSpread1.Sheets[0].RowCount = 1;
                FpSpread1.Sheets[0].ColumnHeader.RowCount = 1;
                FpSpread1.CommandBar.Visible = false;
                FpSpread1.Sheets[0].ColumnCount = 7;
                FpSpread1.Sheets[0].RowHeader.Visible = false;
                FarPoint.Web.Spread.StyleInfo darkstyle = new FarPoint.Web.Spread.StyleInfo();
                darkstyle.BackColor = ColorTranslator.FromHtml("#0CA6CA");
                darkstyle.ForeColor = Color.White;
                FpSpread1.ActiveSheetView.ColumnHeader.DefaultStyle = darkstyle;

                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 0].Text = "S.No";
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 0].ForeColor = Color.Black;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 0].Font.Bold = true;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 0].Font.Name = "Book Antiqua";
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 0].Locked = true;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 0].HorizontalAlign = HorizontalAlign.Center;
                FpSpread1.Columns[0].Width = 50;

                FarPoint.Web.Spread.CheckBoxCellType chkall = new FarPoint.Web.Spread.CheckBoxCellType();
                chkall.AutoPostBack = true;

                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 1].Text = "Select";
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 1].ForeColor = Color.Black;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 1].Font.Bold = true;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 1].Font.Name = "Book Antiqua";
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 1].HorizontalAlign = HorizontalAlign.Center;
                FpSpread1.Columns[1].Width = 50;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 1].Locked = true;
                FpSpread1.Sheets[0].Columns[1].Locked = false;
                FpSpread1.Sheets[0].Columns[1].Visible = true;
                FpSpread1.Sheets[0].Cells[0, 1].CellType = chkall;
                FpSpread1.Sheets[0].Cells[0, 1].HorizontalAlign = HorizontalAlign.Center;

                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 2].Text = "Roll No";
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 2].ForeColor = Color.Black;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 2].Font.Bold = true;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 2].Font.Name = "Book Antiqua";
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 2].HorizontalAlign = HorizontalAlign.Center;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 2].Locked = true;
                FpSpread1.Columns[2].Width = 150;

                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 3].Text = "Name";
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 3].ForeColor = Color.Black;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 3].Font.Bold = true;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 3].Font.Name = "Book Antiqua";
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 3].HorizontalAlign = HorizontalAlign.Center;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 3].Locked = true;
                FpSpread1.Columns[3].Width = 250;

                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 4].Text = "Department";
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 4].ForeColor = Color.Black;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 4].Font.Bold = true;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 4].Font.Name = "Book Antiqua";
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 4].HorizontalAlign = HorizontalAlign.Center;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 4].Locked = true;
                FpSpread1.Columns[4].Width = 250;

                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 5].Text = "Semester";
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 5].ForeColor = Color.Black;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 5].Font.Bold = true;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 5].Font.Name = "Book Antiqua";
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 5].HorizontalAlign = HorizontalAlign.Center;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 5].Locked = true;
                FpSpread1.Columns[5].Width = 100;

                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 6].Text = "Ledger";
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 6].ForeColor = Color.Black;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 6].Font.Bold = true;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 6].Font.Name = "Book Antiqua";
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 6].HorizontalAlign = HorizontalAlign.Center;
                FpSpread1.Sheets[0].ColumnHeader.Cells[0, 6].Locked = true;
                FpSpread1.Columns[6].Width = 150;
                //   loaddesc1();
                string[] Finearray = new string[chkl_studled.Items.Count + 1];
                Finearray[0] = "Select";
                if (chkl_studled.Items.Count > 0)
                {
                    for (int fine = 0; fine < chkl_studled.Items.Count; fine++)
                    {
                        Finearray[fine + 1] = Convert.ToString(chkl_studled.Items[fine].Text);
                    }
                }
                FarPoint.Web.Spread.ComboBoxCellType cb1 = new FarPoint.Web.Spread.ComboBoxCellType(Finearray);
                cb1.UseValue = true;
                cb1.ShowButton = true;
                cb1.AutoPostBack = true;

                FpSpread1.Sheets[0].Columns[6].Locked = false;
                FpSpread1.Sheets[0].Columns[6].Visible = true;
                FpSpread1.Sheets[0].Cells[0, 6].CellType = cb1;
                FpSpread1.Sheets[0].Cells[0, 6].HorizontalAlign = HorizontalAlign.Center;
                FpSpread1.Sheets[0].Cells[0, 6].BackColor = Color.SkyBlue;
                #endregion

                #region value
                int sno = 0;

                for (int i = 0; i < dsload.Tables[0].Rows.Count; i++)
                {
                    FpSpread1.Sheets[0].RowCount++;
                    sno++;
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 0].Text = Convert.ToString(sno);
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 0].HorizontalAlign = HorizontalAlign.Center;
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 0].Font.Name = "Book Antiqua";
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 0].Font.Size = FontUnit.Medium;

                    FarPoint.Web.Spread.CheckBoxCellType check = new FarPoint.Web.Spread.CheckBoxCellType();
                    check.AutoPostBack = false;
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 1].CellType = check;
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 1].HorizontalAlign = HorizontalAlign.Center;


                    FarPoint.Web.Spread.TextCellType txt = new FarPoint.Web.Spread.TextCellType();
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 2].Text = Convert.ToString(dsload.Tables[0].Rows[i]["Roll_No"]);
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 2].CellType = txt;
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 2].HorizontalAlign = HorizontalAlign.Center;
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 2].Font.Name = "Book Antiqua";
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 2].Font.Size = FontUnit.Medium;

                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 3].Text = Convert.ToString(dsload.Tables[0].Rows[i]["Stud_Name"]);
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 3].HorizontalAlign = HorizontalAlign.Left;
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 3].Font.Name = "Book Antiqua";
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 3].Font.Size = FontUnit.Medium;

                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 4].Text = Convert.ToString(dsload.Tables[0].Rows[i]["Department"]);
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 4].HorizontalAlign = HorizontalAlign.Left;
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 4].Font.Name = "Book Antiqua";
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 4].Font.Size = FontUnit.Medium;

                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 5].Text = Convert.ToString(dsload.Tables[0].Rows[i]["TextVal"]);
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 5].HorizontalAlign = HorizontalAlign.Center;
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 5].Font.Name = "Book Antiqua";
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 5].Font.Size = FontUnit.Medium;

                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 6].CellType = cb1;
                    //FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 5].BackColor = ColorTranslator.FromHtml("lightyellow");
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 6].HorizontalAlign = HorizontalAlign.Right;
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 6].Font.Name = "Book Antiqua";
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 6].Font.Size = FontUnit.Medium;

                }
                #endregion

                FpSpread1.Sheets[0].PageSize = FpSpread1.Sheets[0].RowCount;
                FpSpread1.Height = 1000;
                FpSpread1.Width = 1000;
                FpSpread1.SaveChanges();
                FpSpread1.Visible = true;
                divspread.Visible = true;
                //imgdiv2.Visible = false;
                //lbl_alert.Text = "";
                //lblvalidation1.Text = "";
                //for (int i = 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                //{
                //    string rollno = Convert.ToString(FpSpread1.Sheets[0].Cells[i, 2].Text);
                //    string semester = Convert.ToString(FpSpread1.Sheets[0].Cells[i, 5].Text);
                //    string feecat = DA.GetFunction(" select textcode from TextValTable where textval='" + semester + "'");
                //    if (rollno != "")
                //    {
                //        string confirmFine = " select distinct roll_no from FT_FineCancelSetting where roll_no='" + rollno.Trim() + "' and feecategory in('" + feecat + "')";
                //        DataSet dsFine = new DataSet();
                //        dsFine = DA.select_method_wo_parameter(confirmFine, "Text");
                //        if (dsFine.Tables.Count > 0 && dsFine.Tables[0].Rows.Count > 0)
                //        {
                //            for (int k = 0; k < FpSpread1.Columns.Count; k++)
                //            {
                //                FpSpread1.Sheets[0].Cells[i, k].BackColor = Color.LightGreen;
                //            }
                //        }
                //        else
                //        {
                //            string DelChk = " select distinct roll_no from FT_FineCancelSetting where roll_no='" + rollno.Trim() + "' and feecategory in('" + feecat + "')";
                //            DataSet dsDel = new DataSet();
                //            dsDel = DA.select_method_wo_parameter(DelChk, "Text");
                //            if (dsDel.Tables.Count > 0 && dsDel.Tables[0].Rows.Count > 0)
                //            {
                //                for (int k = 0; k < FpSpread1.Columns.Count; k++)
                //                {
                //                    FpSpread1.Sheets[0].Cells[i, k].BackColor = ColorTranslator.FromHtml("#FF3333");
                //                }
                //            }
                //        }
                //    }
                //}
            }
            catch
            {
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gRow in gridStudList.Rows)
            {
                double amT = 0;
                Label lbl_appNo = (Label)gRow.FindControl("lbl_appNo");
                CheckBox cb_selectgrid = (CheckBox)gRow.FindControl("cb_selectgrid");
                TextBox txtAmt = (TextBox)gRow.FindControl("txtAmt");
                int checkVal = cb_selectgrid.Checked ? 1 : 0;
                if (ddlType.SelectedItem.Text.Trim() == "Part Payment")
                {
                    if (checkVal == 1)
                        double.TryParse(Convert.ToString(txtAmt.Text), out amT);

                    dirAcc.updateData("update applyn set IsFinPartPay='" + (cb_selectgrid.Checked ? 1 : 0) + "',isPartAmount='" + amT + "' where app_no='" + lbl_appNo.Text.Trim() + "'");
                }
                else
                {
                    dirAcc.updateData("update applyn set IsFinPartPay='" + (cb_selectgrid.Checked ? 1 : 0) + "',isPartHold='" + (cb_selectgrid.Checked ? 1 : 0) + "' where app_no='" + lbl_appNo.Text.Trim() + "'");
                }

            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Saved Successfully')", true);
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Please try later')", true);
        }
    }


    protected void btnSaveledgerhold_Click(object sender, EventArgs e)
    {
        try
        {
            for (int rowStud = 1; rowStud < FpSpread1.Sheets[0].RowCount; rowStud++)
            {
                int checkval = Convert.ToInt32(FpSpread1.Sheets[0].Cells[rowStud, 1].Value);
                if (checkval == 1)
                {
                    //string headerfk = Convert.ToString(ddlheader.SelectedValue);
                    //string ledgerfk = Convert.ToString(ddlLedger.SelectedValue);

                    string roll_no = string.Empty;
                    roll_no = Convert.ToString(FpSpread1.Sheets[0].Cells[rowStud, 2].Text);
                    string name = string.Empty;
                    name = Convert.ToString(FpSpread1.Sheets[0].Cells[rowStud, 3].Text);
                    string dept = string.Empty;
                    dept = Convert.ToString(FpSpread1.Sheets[0].Cells[rowStud, 4].Text);
                    string ledger = string.Empty;
                    ledger = Convert.ToString(FpSpread1.Sheets[0].Cells[rowStud, 6].Value);
                    string ledgerfk = DA.GetFunction("select ledgerpk from fm_ledgermaster where ledgername='" + ledger + "'");
                    string semester = Convert.ToString(FpSpread1.Sheets[0].Cells[rowStud, 5].Value);
                    string feecat = DA.GetFunction(" select textcode from TextValTable where textval='" + semester + "'");



                    string appNo = string.Empty;
                    string queryRollApp = "select r.app_no from Registration r where r.college_code='" + collegeCode + "'  and r.Roll_No='" + roll_no + "'";
                    DataSet dsRollApp = new DataSet();
                    dsRollApp = DA.select_method_wo_parameter(queryRollApp, "Text");
                    if (dsRollApp.Tables.Count > 0)
                    {
                        if (dsRollApp.Tables[0].Rows.Count > 0)
                        {
                            appNo = Convert.ToString(dsRollApp.Tables[0].Rows[0]["app_no"]);
                        }
                    }
                    string insertqry = string.Empty;
                    insertqry = "update ft_feeallot set isPartHold='1' where app_no='" + appNo + "' and ledgerfk='" + ledgerfk + "' and feecategory='" + feecat + "'";
                    DA.update_method_wo_parameter(insertqry, "Text");

                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Part Hold SuccessFully Added')", true);
                }
            }
        }
        catch
        {
        }
    }
    //added by sudhagar 03.10.2017
    protected void ddlType_OnIndexChange(object sender, EventArgs e)
    {
        tdAmt.Visible = false;
        if (ddlType.SelectedItem.Text.Trim() == "Part Payment")
        {
            tdAmt.Visible = true;
        }

    }
    protected void gridStudList_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ddlType.SelectedIndex == 0)
            {
                e.Row.Cells[8].Visible = true;
            }
            else
            {
                e.Row.Cells[8].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ddlType.SelectedIndex == 0)
            {
                e.Row.Cells[8].Visible = true;
                TextBox txtAmt = (TextBox)e.Row.Cells[8].FindControl("txtAmt");
                double oldAmt = 0;
                double.TryParse(Convert.ToString(txtAmt.Text), out oldAmt);
                double Amt = 0;
                double.TryParse(Convert.ToString(txtPayment.Text), out Amt);
                if (oldAmt == 0 && Amt != 0)
                    txtAmt.Text = Convert.ToString(Amt);
            }
            else
            {
                e.Row.Cells[8].Visible = false;
            }

        }
    }
    public void bindledger()//abarna
    {
        try
        {
            string headercode;

            string collegecode = ddl_college.SelectedItem.Value;
            // headercode = Convert.ToString(getCblSelectedValue(chkl_studhed));
            chkl_studled.Items.Clear();
            txt_studled.Text = "--Select--";
            chk_studled.Checked = false;
            DataSet ds = new DataSet();
            if (Convert.ToString(collegecode) != "")
            {
                string query = " select distinct ledgername,ledgerpk from FM_LedgerMaster l,FM_HeaderMaster h,FS_LedgerPrivilage P where l.HeaderFK =h.HeaderPK   and L.LedgerPK = P.LedgerFK and l.CollegeCode in('" + collegecode + "' )";

                ds = DA.select_method_wo_parameter(query, "Text");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkl_studled.DataSource = ds;
                    chkl_studled.DataTextField = "ledgername";
                    chkl_studled.DataValueField = "ledgerpk";
                    chkl_studled.DataBind();
                    for (int i = 0; i < chkl_studled.Items.Count; i++)
                    {
                        chkl_studled.Items[i].Selected = true;
                    }
                    txt_studled.Text = lbl_ledger.Text + "(" + chkl_studled.Items.Count + ")";
                    chk_studled.Checked = true;
                }
            }
        }
        catch
        {
        }
    }
    public void chk_studled_OnCheckedChanged(object sender, EventArgs e)
    {
        reUse.CallCheckboxChange(chk_studled, chkl_studled, txt_studled, lbl_ledger.Text, "--Select--");
    }

    public void chkl_studled_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        reUse.CallCheckboxListChange(chk_studled, chkl_studled, txt_studled, lbl_ledger.Text, "--Select--");
    }
    private string getCblSelectedText(CheckBoxList cblSelected)
    {
        System.Text.StringBuilder selectedText = new System.Text.StringBuilder();
        try
        {
            for (int sel = 0; sel < cblSelected.Items.Count; sel++)
            {
                if (cblSelected.Items[sel].Selected == true)
                {
                    if (selectedText.Length == 0)
                    {
                        selectedText.Append(Convert.ToString(cblSelected.Items[sel].Text));
                    }
                    else
                    {
                        selectedText.Append("','" + Convert.ToString(cblSelected.Items[sel].Text));
                    }
                }
            }
        }
        catch { cblSelected.Items.Clear(); }
        return selectedText.ToString();
    }
    private string getCblSelectedValue(CheckBoxList cblSelected)
    {
        System.Text.StringBuilder selectedvalue = new System.Text.StringBuilder();
        try
        {
            for (int sel = 0; sel < cblSelected.Items.Count; sel++)
            {
                if (cblSelected.Items[sel].Selected == true)
                {
                    if (selectedvalue.Length == 0)
                    {
                        selectedvalue.Append(Convert.ToString(cblSelected.Items[sel].Value));
                    }
                    else
                    {
                        selectedvalue.Append("','" + Convert.ToString(cblSelected.Items[sel].Value));
                    }
                }
            }
        }
        catch { cblSelected.Items.Clear(); }
        return selectedvalue.ToString();
    }
    protected DataSet dsvalue()
    {

        try
        {
            string selectquery;

            string branch = reUse.GetSelectedItemsValueAsString(cbl_branch);//ddl_branch.Items.Count > 0 ? ddl_branch.SelectedValue : "";

            string degCode = reUse.GetSelectedItemsValueAsString(cbl_degree);//ddl_degree.Items.Count > 0 ? ddl_degree.SelectedValue : "";

            string stream = ddl_strm.Enabled ? ddl_strm.Items.Count > 0 ? ddl_strm.SelectedItem.Text.Trim() : "" : "";

            string section = reUse.GetSelectedItemsText(cbl_sec);//ddl_sec.Items.Count > 0 ? ddl_sec.SelectedItem.Text.Trim() : "";

            string batch_year = ddl_batch.Items.Count > 0 ? ddl_batch.SelectedItem.Text : "";

            string cusem = reUse.GetSelectedItemsText(cbl_sem);// ddl_sem.Items.Count > 0 ? ddl_sem.SelectedItem.Text : "";
            string sem = reUse.getCblSelectedValue(cbl_seml);
            DataTable dtStud = new DataTable();
            if (batch_year != string.Empty && degCode != string.Empty && branch != string.Empty && cusem != string.Empty)
            {
                if (stream != string.Empty)
                {
                    stream = " and c.type in ('" + stream + "')";
                }
                string rptType = string.Empty;
                if (ddlType.SelectedItem.Text == "Part Payment")
                    rptType = ",isPartAmount";
                else
                    rptType = ",isPartAmount,isPartHold";
                string college_Code = Convert.ToString(ddl_college.SelectedValue);
                string ledgid = getCblSelectedValue(chkl_studled);
                //selectquery = "select r.Roll_No,r.Roll_Admit,r.smart_serial_no,r.Stud_Name,d.Degree_Code,(C.Course_Name +' - '+ dt.Dept_Name) as Department,r.Reg_No,r.App_No,c.type,isnull(r.Sections,'') as Sections, case when a.sex=0 then 'Male' when a.sex=1 then 'Female' when a.sex=2 then 'Transgender' else 'N/A' end as Gender,a.IsFinPartPay" + rptType + "  from Registration r,applyn a,Degree d,Department dt,Course c where r.app_no=a.app_no and r.degree_code =d.Degree_Code and dt.Dept_Code =d.Dept_Code and c.Course_Id =d.Course_Id and CC=0 and DelFlag =0 and Exam_Flag <>'DEBAR' and r.Batch_Year =" + batch_year + " and r.degree_code in ('" + branch + "')  and isnull(r.Sections,'') in ('" + section + "')  and r.current_semester in('" + sem + "')  and isnull(a.admission_status,'0')='1'   " + stream + "  order by d.Degree_Code,isnull(r.Sections,''),ltrim (r.Roll_No) asc ,ltrim (r.stud_name) asc ";
                selectquery = " select distinct Roll_No ,Stud_Name,Course_Name+'-'+Dept_Name as Department,r.degree_code,A.HeaderFK,HeaderName,A.LedgerFK,(LedgerName) as LedgerName,isnull(FeeAmount,0) as FeeAmount,isnull(TotalAmount,0) as TotalAmount,isnull(BalAmount,'0') as balamount,TextVal,TextCode,f.DueDate from Registration r,FM_FineMaster f,Degree d,course c,Department dt,FT_FeeAllot A,FM_HeaderMaster H,FM_LedgerMaster L,TextValTable T,FS_HeaderPrivilage P where r.Batch_Year in('" + batch_year + "') and a.App_No=r.App_No and r.degree_code in ('" + branch + "') and r.college_code in('" + college_Code + "') and A.FeeCategory in('" + sem + "') and a.LedgerFK in('" + ledgid + "') and c.Course_Id in('" + degCode + "') and d.Degree_Code=r.degree_code and d.Course_Id=c.Course_Id and dt.Dept_Code=d.Dept_Code and A.HeaderFK = H.HeaderPK AND A.LedgerFK = L.LedgerPK  and P.HeaderFK = H.HeaderPK and P.HeaderFK = L.HeaderFK and a.HeaderFK=p.HeaderFK  AND H.HeaderPK = L.HeaderFK AND A.FeeCategory = T.TextCode and BalAmount>0  order by r.degree_code";//and c.Course_Id in('" + degCode + "')// and  c.Course_Id in('" + degCode + "')  

                dsload.Clear();
                dsload = DA.select_method_wo_parameter(selectquery, "Text");
            }

        }
        catch (Exception ex)
        {
            //DA.sendErrorMail(ex, collegeCode, "Finesetting.aspx");
        }
        return dsload;
    }
    protected void FpSpread1_OnUpdateCommand(object sender, FarPoint.Web.Spread.SpreadCommandEventArgs e)
    {
        try
        {
            string buttonok = String.Empty;
            string spread = string.Empty;
            string controlatt = string.Empty;
            Control control = null;
            string ctrlname = Page.Request.Params["__EVENTTARGET"];
            if (ctrlname != null && ctrlname != String.Empty)
            {
                control = Page.FindControl(ctrlname);
                spread = ctrlname.ToString();
            }
            else
            {
                string ctrlStr = String.Empty;
                Control c = null;
                foreach (string ctl in Page.Request.Form)
                {
                    if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                    {
                        ctrlStr = ctl.Substring(0, ctl.Length - 2);
                        c = Page.FindControl(ctrlStr);
                    }
                    else
                    {
                        c = Page.FindControl(ctl);
                        buttonok = ctl;
                    }
                    if (c is System.Web.UI.WebControls.Button ||
                             c is System.Web.UI.WebControls.ImageButton)
                    {
                        control = c;
                        break;
                    }
                }
            }


            string spreadname = string.Empty;
            if (spread != "")
            {
                string[] spiltspreadname = spread.Split('$');
                spreadname = spiltspreadname[2].ToString().Trim();
                controlatt = spreadname;
            }

            string actrow = FpSpread1.Sheets[0].ActiveRow.ToString();
            string actcol = FpSpread1.Sheets[0].ActiveColumn.ToString();
            if (actrow.Trim() == "0" && actcol.Trim() == "1")
            {
                if (FpSpread1.Sheets[0].RowCount > 0)
                {
                    int checkval = Convert.ToInt32(FpSpread1.Sheets[0].Cells[0, 1].Value);
                    if (checkval == 0)
                    {
                        for (int i = 1; i < FpSpread1.Sheets[0].RowCount; i++)
                        {
                            FpSpread1.Sheets[0].Cells[i, 1].Value = 1;
                        }
                    }
                    if (checkval == 1)
                    {
                        for (int i = 1; i < FpSpread1.Sheets[0].RowCount; i++)
                        {
                            FpSpread1.Sheets[0].Cells[i, 1].Value = 0;
                        }
                    }
                }
            }
            if (spreadname.ToString().Trim().ToLower() == "fpspread1")
            {
                actrow = FpSpread1.Sheets[0].ActiveRow.ToString();
                actcol = FpSpread1.Sheets[0].ActiveColumn.ToString();
                string last = e.CommandArgument.ToString();
                if (actrow == "0")
                {
                    if (last == "0")
                    {
                        flag_true = false;
                    }
                    else
                    {
                        flag_true = true;
                    }
                }
                if (actcol == "0")
                {
                    if (actrow == last)
                    {
                        flag_true = false;
                    }
                    else
                    {
                        flag_true = true;
                    }
                }

                if (flag_true == false && actrow == "0" && actcol.Trim() == "6")
                {
                    string seltext = string.Empty;
                    for (int j = 1; j < FpSpread1.Sheets[0].RowCount; j++)
                    {
                        actcol = e.SheetView.ActiveColumn.ToString();
                        string value = e.EditValues[0].ToString();
                        e.Handled = true;
                        seltext = e.EditValues[Convert.ToInt32(actcol)].ToString();
                        cbl_sem.SelectedItem.Text = seltext;
                        if (seltext != "System.Object")
                        {
                            if (FpSpread1.Sheets[0].Cells[j, 6].Locked == false)
                            {
                                FpSpread1.Sheets[0].Cells[j, 6].Text = seltext;
                            }
                        }
                        else
                        {
                            if (FpSpread1.Sheets[0].Cells[j, 6].Locked == false)
                            {
                                FpSpread1.Sheets[0].Cells[j, 6].Text = seltext;
                            }
                        }
                    }
                    flag_true = true;
                }
            }
        }
        catch (Exception ex)
        {
            // d2.sendErrorMail(ex, collegecode, "Finesetting.aspx");
        }
    }
    #region sem
    protected void cb_sem_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            reUse.CallCheckboxChange(cb_seml, cbl_seml, txt_seml, "Semester", "--Select--");
            //bindsec();
        }
        catch (Exception ex)
        { }
    }
    protected void cbl_sem_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            reUse.CallCheckboxListChange(cb_seml, cbl_seml, txt_seml, "Semester", "--Select--");
            //bindsec();
        }
        catch (Exception ex)
        { }

    }

    protected void bindsemledger()
    {
        try
        {
            cbl_seml.Items.Clear();
            cb_seml.Checked = false;
            txt_seml.Text = "--Select--";
            dsload.Clear();
            string linkName = string.Empty;
            string cbltext = string.Empty;
            dsload = DA.loadFeecategory(Convert.ToString(ddl_college.SelectedItem.Value), usercode, ref linkName);
            if (dsload.Tables.Count > 0 && dsload.Tables[0].Rows.Count > 0)
            {
                cbl_seml.DataSource = dsload;
                cbl_seml.DataTextField = "TextVal";
                cbl_seml.DataValueField = "TextCode";
                cbl_seml.DataBind();

                if (cbl_seml.Items.Count > 0)
                {
                    for (int i = 0; i < cbl_seml.Items.Count; i++)
                    {
                        cbl_seml.Items[i].Selected = true;
                        cbltext = Convert.ToString(cbl_seml.Items[i].Text);
                    }
                    if (cbl_seml.Items.Count == 1)
                        txt_seml.Text = "" + linkName + "(" + cbltext + ")";
                    else
                        txt_sem.Text = "" + linkName + "(" + cbl_sem.Items.Count + ")";
                    cb_seml.Checked = true;
                }
            }
        }
        catch { }
    }



    #endregion


    protected void btnCancelLedgerHold_Click(object sender, EventArgs e)
    {
        try
        {
            for (int rowStud = 1; rowStud < FpSpread1.Sheets[0].RowCount; rowStud++)
            {
                int checkval = Convert.ToInt32(FpSpread1.Sheets[0].Cells[rowStud, 1].Value);
                if (checkval == 1)
                {
                    //string headerfk = Convert.ToString(ddlheader.SelectedValue);
                    //string ledgerfk = Convert.ToString(ddlLedger.SelectedValue);

                    string roll_no = string.Empty;
                    roll_no = Convert.ToString(FpSpread1.Sheets[0].Cells[rowStud, 2].Text);
                    string name = string.Empty;
                    name = Convert.ToString(FpSpread1.Sheets[0].Cells[rowStud, 3].Text);
                    string dept = string.Empty;
                    dept = Convert.ToString(FpSpread1.Sheets[0].Cells[rowStud, 4].Text);
                    string ledger = string.Empty;
                    ledger = Convert.ToString(FpSpread1.Sheets[0].Cells[rowStud, 6].Value);
                    string ledgerfk = DA.GetFunction("select ledgerpk from fm_ledgermaster where ledgername='" + ledger + "'");
                    string semester = Convert.ToString(FpSpread1.Sheets[0].Cells[rowStud, 5].Value);
                    string feecat = DA.GetFunction(" select textcode from TextValTable where textval='" + semester + "'");



                    string appNo = string.Empty;
                    string queryRollApp = "select r.app_no from Registration r where r.college_code='" + collegeCode + "'  and r.Roll_No='" + roll_no + "'";
                    DataSet dsRollApp = new DataSet();
                    dsRollApp = DA.select_method_wo_parameter(queryRollApp, "Text");
                    if (dsRollApp.Tables.Count > 0)
                    {
                        if (dsRollApp.Tables[0].Rows.Count > 0)
                        {
                            appNo = Convert.ToString(dsRollApp.Tables[0].Rows[0]["app_no"]);
                        }
                    }
                    string insertqry = string.Empty;
                    insertqry = "update ft_feeallot set isPartHold='0' where app_no='" + appNo + "' and ledgerfk='" + ledgerfk + "' and feecategory='" + feecat + "'";
                    DA.update_method_wo_parameter(insertqry, "Text");

                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Part Hold Cancel SuccessFully')", true);
                }
            }
        }
        catch
        {
        }
    }
}
//Last Modified by Idhris 02-03-2017