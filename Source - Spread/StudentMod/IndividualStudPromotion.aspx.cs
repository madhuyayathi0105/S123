﻿using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections.Generic;

public partial class IndividualStudPromotion : System.Web.UI.Page
{
    int collegeCode = 0;
    int userCode = 0;
    DAccess2 DA = new DAccess2();
    ReuasableMethods rs = new ReuasableMethods();

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
                setLabelText();
                bindCollege();
                updateClgCode();
                bindType();
                bindbatch();
                binddegree();
                bindbranch();
                bindsem();
                bindsec();
            }
            lbl_validation.Visible = false;
            updateClgCode();
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
    public void bindType()
    {
        try
        {
            lbl_stream.Text = useStreamShift();
            cbl_stream.Items.Clear();
            string query = "select Distinct ISNULL( type,'') as type  from Registration r,Degree d,Department dt,Course c where r.degree_code =d.Degree_Code and d.Dept_Code =dt.Dept_Code and d.Course_Id =c.Course_Id  and type<>''  and r.college_code='" + collegeCode + "'  order by type asc";

            DataSet ds = DA.select_method_wo_parameter(query, "Text");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                cbl_stream.DataSource = ds;
                cbl_stream.DataTextField = "type";
                cbl_stream.DataBind();
                if (cbl_stream.Items.Count > 0)
                {
                    for (int i = 0; i < cbl_stream.Items.Count; i++)
                    {
                        cbl_stream.Items[i].Selected = true;
                    }
                    txt_stream.Text = lbl_stream.Text + "(" + cbl_stream.Items.Count + ")";
                    cb_stream.Checked = true;
                    txt_stream.Enabled = true;
                    txt_stream.Enabled = true;
                }
                else
                {
                    txt_stream.Text = "--Select--";
                    cb_stream.Checked = false;
                    txt_stream.Enabled = false;
                }
            }
            else
            {
                txt_stream.Text = "--Select--";
                txt_stream.Enabled = false;
            }
        }
        catch (Exception ex) { }
    }
    public void bindbatch()
    {
        try
        {
            cbl_batch.Items.Clear();
            string sqlyear = "select distinct batch_year from Registration where batch_year<>'-1' and batch_year<>'' and cc=0 and delflag=0 and exam_flag<>'debar' order by batch_year desc";
            DataSet ds = DA.select_method_wo_parameter(sqlyear, "Text");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                cbl_batch.DataSource = ds;
                cbl_batch.DataTextField = "batch_year";
                cbl_batch.DataValueField = "batch_year";
                cbl_batch.DataBind();
                //ddl_batch1.SelectedIndex = 3;
                if (cbl_batch.Items.Count > 0)
                {
                    for (int i = 0; i < cbl_batch.Items.Count; i++)
                    {
                        cbl_batch.Items[0].Selected = true;
                    }
                    // txt_batch.Text = "Batch(" + cbl_batch.Items.Count + ")";
                    txt_batch.Text = "Batch(" + 1 + ")";
                    //cb_batch.Checked = true;
                }
                else
                {
                    txt_batch.Text = "--Select--";
                    cb_batch.Checked = false;
                }
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
            stream = cbl_stream.Items.Count > 0 ? cbl_stream.SelectedValue : "";

            string query = "select distinct degree.course_id,course.course_name from degree,course,deptprivilages  where course.course_id=degree.course_id and course.college_code = degree.college_code  and degree.college_code in (" + collegeCode + ") and deptprivilages.Degree_code=degree.Degree_code and   user_code=" + userCode + " ";
            if (cbl_stream.Enabled)
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
                CallCheckBoxChangedEvent(cbl_degree, cb_degree, txt_degree, lbl_degree.Text);
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
            degree = GetSelectedItemsValueAsString(cbl_degree);//ddl_degree.Items.Count > 0 ? ddl_degree.SelectedValue : "";


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
                //ddl_branch.DataValueField = "dept_code";
                //ddl_branch.DataBind();
                cbl_branch.DataSource = ds;
                cbl_branch.DataTextField = "dept_name";
                cbl_branch.DataValueField = "degree_code";
                cbl_branch.DataBind();
                CallCheckBoxChangedEvent(cbl_branch, cb_branch, txt_branch, lbl_branch.Text);
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

            branch = GetSelectedItemsValueAsString(cbl_branch);//Convert.ToString(ddl_branch.SelectedItem.Value);

            batch = GetSelectedItemsValue(cbl_branch);

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
                    CallCheckBoxChangedEvent(cbl_sem, cb_sem, txt_sem, lbl_Sem.Text);
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
            batch = GetSelectedItemsValue(cbl_batch);
            string branch = "";
            branch = GetSelectedItemsValue(cbl_branch);//ddl_branch.Items.Count > 0 ? ddl_branch.SelectedValue : "0";
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
                CallCheckBoxChangedEvent(cbl_sec, cb_sec, txt_sec, "Section");
            }
        }
        catch (Exception ex) { }
    }
    protected void btn_alertclose_Click(object sender, EventArgs e)
    {
        imgAlert.Visible = false;
    }
    protected void lb_LogOut_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx", false);
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
        //  btn_go_Click(sender, e);
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
        // btn_go_Click(sender, e);
    }
    protected void ddl_degree_OnIndexChange(object sender, EventArgs e)
    {
        bindbranch();
        bindsem();
        bindsec();
        // btn_go_Click(sender, e);
    }
    protected void cbl_degree_SelectedIndexChanged(object sender, EventArgs e)
    {
        CallCheckBoxListChangedEvent(cbl_degree, cb_degree, txt_degree, lbl_degree.Text);
        bindbranch();
        bindsem();
        bindsec();
        // btn_go_Click(sender, e);
    }
    protected void cb_degree_ChekedChange(object sender, EventArgs e)
    {
        CallCheckBoxChangedEvent(cbl_degree, cb_degree, txt_degree, lbl_degree.Text);
        bindbranch();
        bindsem();
        bindsec();
        // btn_go_Click(sender, e);
    }
    protected void ddl_branch_OnIndexChange(object sender, EventArgs e)
    {
        bindsem();
        bindsec();
        // btn_go_Click(sender, e);
    }
    protected void cbl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        CallCheckBoxListChangedEvent(cbl_branch, cb_branch, txt_branch, lbl_branch.Text);
        bindsem();
        bindsec();
        // btn_go_Click(sender, e);
    }
    protected void cb_branch_ChekedChange(object sender, EventArgs e)
    {
        CallCheckBoxChangedEvent(cbl_branch, cb_branch, txt_branch, lbl_branch.Text);
        bindsem();
        bindsec();
        //btn_go_Click(sender, e);
    }
    protected void ddl_sem_OnIndexChange(object sender, EventArgs e)
    {
        bindsec();
        // btn_go_Click(sender, e);
    }
    protected void cb_sem_CheckedChanged(object sender, EventArgs e)
    {
        CallCheckBoxChangedEvent(cbl_sem, cb_sem, txt_sem, lbl_Sem.Text);
        bindsec();
        // btn_go_Click(sender, e);
    }
    protected void cbl_sem_SelectedIndexChanged(object sender, EventArgs e)
    {
        CallCheckBoxListChangedEvent(cbl_sem, cb_sem, txt_sem, lbl_Sem.Text);
        bindsec();
        // btn_go_Click(sender, e);
    }
    protected void ddl_sec_OnIndexChange(object sender, EventArgs e)
    {
        //  btn_go_Click(sender, e);
    }
    protected void cb_sec_ChekedChange(object sender, EventArgs e)
    {
        CallCheckBoxChangedEvent(cbl_sec, cb_sec, txt_sec, "Section");
        //  btn_go_Click(sender, e);
    }
    protected void cbl_sec_SelectedIndexChanged(object sender, EventArgs e)
    {
        CallCheckBoxListChangedEvent(cbl_sec, cb_sec, txt_sec, "Section");
        // btn_go_Click(sender, e);
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
        try
        {
            lbl_errormsg.Visible = false;
            lbl_Total.Visible = false;
            Printcontrol.Visible = false;
            rptprint.Visible = false;

            string selectquery;

            string branch = GetSelectedItemsValueAsString(cbl_branch);//ddl_branch.Items.Count > 0 ? ddl_branch.SelectedValue : "";

            string degCode = GetSelectedItemsValueAsString(cbl_degree);//ddl_degree.Items.Count > 0 ? ddl_degree.SelectedValue : "";

            string stream = cbl_stream.Enabled ? cbl_stream.Items.Count > 0 ? cbl_stream.SelectedItem.Text.Trim() : "" : "";

            string section = GetSelectedItemsText(cbl_sec);//ddl_sec.Items.Count > 0 ? ddl_sec.SelectedItem.Text.Trim() : "";

            string batch_year = GetSelectedItemsValue(cbl_batch);

            string cusem = GetSelectedItemsText(cbl_sem);// ddl_sem.Items.Count > 0 ? ddl_sem.SelectedItem.Text : "";

            DataSet ds = new DataSet();
            if (batch_year != string.Empty && degCode != string.Empty && branch != string.Empty && cusem != string.Empty)
            {
                if (stream != string.Empty)
                {
                    stream = " and c.type in ('" + stream + "')";
                }
                selectquery = "select r.Current_Semester,Roll_No,Roll_Admit,smart_serial_no,Stud_Name,d.Degree_Code,(C.Course_Name +' - '+ dt.Dept_Name) as Department,Reg_No,r.App_No,c.type,isnull(r.Sections,'') as Sections   from Registration r,Degree d,Department dt,Course c where r.degree_code =d.Degree_Code and dt.Dept_Code =d.Dept_Code and c.Course_Id =d.Course_Id and CC=0 and DelFlag =0 and Exam_Flag <>'DEBAR' and Batch_Year IN(" + batch_year + ") and r.degree_code in ('" + branch + "')  and isnull(r.Sections,'') in ('" + section + "')  and r.current_semester in('" + cusem + "')  " + stream + " order by Department,r.Sections asc";
                ds = DA.select_method_wo_parameter(selectquery, "Text");
            }
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                spreadStudList.Sheets[0].RowCount = 1;
                spreadStudList.Sheets[0].ColumnCount = 0;
                spreadStudList.Sheets[0].ColumnHeader.RowCount = 1;
                spreadStudList.CommandBar.Visible = false;
                spreadStudList.Sheets[0].ColumnCount = 10;

                spreadStudList.Sheets[0].RowHeader.Visible = false;
                spreadStudList.Sheets[0].AutoPostBack = false;

                FarPoint.Web.Spread.StyleInfo darkstyle = new FarPoint.Web.Spread.StyleInfo();
                darkstyle.BackColor = ColorTranslator.FromHtml("#0CA6CA");
                darkstyle.ForeColor = Color.Black;
                spreadStudList.ActiveSheetView.ColumnHeader.DefaultStyle = darkstyle;

                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 0].Text = " S.No";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 0].Font.Bold = true;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 0].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 0].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].Columns[0].Locked = true;
                spreadStudList.Sheets[0].Columns[0].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].Columns[0].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].Columns[0].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Columns[0].Width = 50;

                FarPoint.Web.Spread.CheckBoxCellType chkall = new FarPoint.Web.Spread.CheckBoxCellType();
                chkall.AutoPostBack = true;

                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 1].Text = "Select";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 1].Font.Bold = true;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 1].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 1].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 1].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].Columns[1].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].Columns[1].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].Columns[1].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].Columns[1].Width = 60;
                spreadStudList.Sheets[0].Columns[1].Locked = false;
                spreadStudList.Sheets[0].Cells[0, 1].CellType = chkall;
                spreadStudList.Sheets[0].Columns[1].Visible = false;

                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 2].Text = "Admission Number";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 2].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 2].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 2].Font.Bold = true;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 2].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].Columns[2].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].Columns[2].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].Columns[2].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].Columns[2].Locked = true;
                spreadStudList.Columns[2].Width = 150;

                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 3].Text = "Roll No";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 3].Font.Bold = true;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 3].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 3].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 3].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].Columns[3].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].Columns[3].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].Columns[3].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].Columns[3].Locked = true;
                spreadStudList.Columns[3].Width = 100;

                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 4].Text = "Reg No";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 4].Font.Bold = true;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 4].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 4].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 4].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].Columns[4].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].Columns[4].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].Columns[4].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].Columns[4].Locked = true;
                spreadStudList.Columns[4].Width = 100;

                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 5].Text = "Smartcard No";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 5].Font.Bold = true;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 5].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 5].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 5].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].Columns[5].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].Columns[5].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].Columns[5].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].Columns[5].Locked = true;
                spreadStudList.Columns[5].Width = 100;
                spreadStudList.Sheets[0].Columns[5].Visible = false;

                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 6].Text = "Student Name";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 6].Font.Bold = true;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 6].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 6].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 6].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].Columns[6].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].Columns[6].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].Columns[6].HorizontalAlign = HorizontalAlign.Left;
                spreadStudList.Sheets[0].Columns[6].Locked = true;
                spreadStudList.Columns[6].Width = 300;

                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 7].Text = lbl_degree.Text + "/" + lbl_branch.Text;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 7].Font.Bold = true;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 7].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 7].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 7].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].Columns[7].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].Columns[7].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].Columns[7].HorizontalAlign = HorizontalAlign.Left;
                spreadStudList.Sheets[0].Columns[7].VerticalAlign = VerticalAlign.Middle;
                spreadStudList.Sheets[0].Columns[7].Locked = true;
                spreadStudList.Sheets[0].SetColumnMerge(7, FarPoint.Web.Spread.Model.MergePolicy.Always);
                spreadStudList.Columns[7].Width = 300;
                //spreadStudList.Sheets[0].Columns[7].Visible = false;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 8].Text = lbl_Sem.Text;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 8].Font.Bold = true;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 8].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 8].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 8].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].Columns[8].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].Columns[8].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].Columns[8].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].Columns[8].Locked = true;
                spreadStudList.Columns[8].Width = 60;

                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 9].Text = "Section";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 9].Font.Bold = true;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 9].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 9].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].ColumnHeader.Cells[0, 9].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].Columns[9].Font.Name = "Book Antiqua";
                spreadStudList.Sheets[0].Columns[9].Font.Size = FontUnit.Medium;
                spreadStudList.Sheets[0].Columns[9].HorizontalAlign = HorizontalAlign.Center;
                spreadStudList.Sheets[0].Columns[9].Locked = true;
                spreadStudList.Columns[8].Width = 60;

                FarPoint.Web.Spread.TextCellType txtRollno = new FarPoint.Web.Spread.TextCellType();
                FarPoint.Web.Spread.TextCellType txtRegno = new FarPoint.Web.Spread.TextCellType();
                FarPoint.Web.Spread.TextCellType txtRollAd = new FarPoint.Web.Spread.TextCellType();
                FarPoint.Web.Spread.TextCellType txtAppno = new FarPoint.Web.Spread.TextCellType();
                FarPoint.Web.Spread.TextCellType txtSmartno = new FarPoint.Web.Spread.TextCellType();

                for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                {

                    spreadStudList.Sheets[0].RowCount++;
                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 0].Text = Convert.ToString(row + 1);
                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 0].Tag = Convert.ToString(ds.Tables[0].Rows[row]["App_No"]);

                    FarPoint.Web.Spread.CheckBoxCellType check = new FarPoint.Web.Spread.CheckBoxCellType();
                    check.AutoPostBack = false;
                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 1].CellType = check;

                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 2].CellType = txtRollAd;
                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 2].Text = Convert.ToString(ds.Tables[0].Rows[row]["Roll_Admit"]);

                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 3].CellType = txtRollno;
                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 3].Text = Convert.ToString(ds.Tables[0].Rows[row]["Roll_No"]);

                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 4].CellType = txtRegno;
                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 4].Text = Convert.ToString(ds.Tables[0].Rows[row]["Reg_No"]);

                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 5].CellType = txtSmartno;
                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 5].Text = Convert.ToString(ds.Tables[0].Rows[row]["smart_serial_no"]);

                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 6].Text = Convert.ToString(ds.Tables[0].Rows[row]["Stud_Name"]);

                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 7].Tag = Convert.ToString(ds.Tables[0].Rows[row]["Degree_Code"]);
                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 7].Text = Convert.ToString(ds.Tables[0].Rows[row]["Department"]);

                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 8].Text = Convert.ToString(ds.Tables[0].Rows[row]["Current_Semester"]);
                    spreadStudList.Sheets[0].Cells[spreadStudList.Sheets[0].RowCount - 1, 9].Text = Convert.ToString(ds.Tables[0].Rows[row]["Sections"]);
                }
                spreadStudList.Visible = true;
                spreadStudList.Sheets[0].PageSize = spreadStudList.Sheets[0].RowCount;
                spreadStudList.Height = 320;
                spreadStudList.SaveChanges();
                rptprint.Visible = true;
                lbl_Total.Visible = true;
                lbl_Total.Text = "Total Number Of Students : " + (spreadStudList.Sheets[0].RowCount - 1);
            }
            else
            {
                spreadStudList.Visible = false;
                lbl_errormsg.Visible = true;
                lbl_errormsg.Text = "No Records Found";
            }
        }
        catch (Exception ex)
        {
            spreadStudList.Visible = false;
            lbl_errormsg.Visible = true;
            lbl_errormsg.Text = "No Records Found"; DA.sendErrorMail(ex, collegeCode.ToString(), "SectionAllocation");
        }
    }
    protected void btn_printmaster_Click(object sender, EventArgs e)
    {
        try
        {
            string degreedetails = "Section Allocation Report";
            string pagename = "SectionAllocation.aspx";
            Printcontrol.loadspreaddetails(spreadStudList, pagename, degreedetails);
            Printcontrol.Visible = true;
        }
        catch (Exception ex) { }
    }
    protected void btn_excel_Click(object sender, EventArgs e)
    {
        try
        {
            string reportname = txt_excelname.Text;
            if (reportname.ToString().Trim() != "")
            {
                DA.printexcelreport(spreadStudList, reportname);
                lbl_validation.Visible = false;
            }
            else
            {
                lbl_validation.Text = "Please Enter Your Report Name";
                lbl_validation.Visible = true;
                txt_excelname.Focus();
            }
        }
        catch (Exception ex) { }

    }
    private string GetSelectedItemsValue(CheckBoxList cblSelected)
    {
        System.Text.StringBuilder sbSelected = new System.Text.StringBuilder();
        try
        {
            for (int sel = 0; sel < cblSelected.Items.Count; sel++)
            {
                if (cblSelected.Items[sel].Selected == true)
                {
                    if (sbSelected.Length == 0)
                    {
                        sbSelected.Append(Convert.ToString(cblSelected.Items[sel].Value));
                    }
                    else
                    {
                        sbSelected.Append("," + Convert.ToString(cblSelected.Items[sel].Value));
                    }
                }
            }
        }
        catch (Exception ex) { sbSelected.Clear(); }
        return sbSelected.ToString();
    }
    private string GetSelectedItemsValueAsString(CheckBoxList cblSelected)
    {
        System.Text.StringBuilder sbSelected = new System.Text.StringBuilder();
        try
        {
            for (int sel = 0; sel < cblSelected.Items.Count; sel++)
            {
                if (cblSelected.Items[sel].Selected == true)
                {
                    if (sbSelected.Length == 0)
                    {
                        sbSelected.Append(Convert.ToString(cblSelected.Items[sel].Value));
                    }
                    else
                    {
                        sbSelected.Append("','" + Convert.ToString(cblSelected.Items[sel].Value));
                    }
                }
            }
        }
        catch { sbSelected.Clear(); }
        return sbSelected.ToString();
    }
    private string GetSelectedItemsText(CheckBoxList cblSelected)
    {
        System.Text.StringBuilder sbSelected = new System.Text.StringBuilder();
        try
        {
            for (int sel = 0; sel < cblSelected.Items.Count; sel++)
            {
                if (cblSelected.Items[sel].Selected == true)
                {
                    if (sbSelected.Length == 0)
                    {
                        sbSelected.Append(Convert.ToString(cblSelected.Items[sel].Text));
                    }
                    else
                    {
                        sbSelected.Append("','" + Convert.ToString(cblSelected.Items[sel].Text));
                    }
                }
            }
        }
        catch { sbSelected.Clear(); }
        return sbSelected.ToString();
    }
    private List<string> GetSelectedItemsValueList(CheckBoxList cblSelected)
    {
        System.Collections.Generic.List<string> lsSelected = new System.Collections.Generic.List<string>();
        try
        {
            for (int list = 0; list < cblSelected.Items.Count; list++)
            {
                if (cblSelected.Items[list].Selected)
                {
                    lsSelected.Add(cblSelected.Items[list].Value);
                }
            }
        }
        catch { lsSelected.Clear(); }
        return lsSelected;
    }
    private List<string> GetSelectedItemsTextList(CheckBoxList cblSelected)
    {
        System.Collections.Generic.List<string> lsSelected = new System.Collections.Generic.List<string>();
        try
        {
            for (int list = 0; list < cblSelected.Items.Count; list++)
            {
                if (cblSelected.Items[list].Selected)
                {
                    lsSelected.Add(cblSelected.Items[list].Text);
                }
            }
        }
        catch { lsSelected.Clear(); }
        return lsSelected;
    }
    private List<string> GetItemsValueList(CheckBoxList cblItems)
    {
        System.Collections.Generic.List<string> lsItems = new System.Collections.Generic.List<string>();
        try
        {
            for (int list = 0; list < cblItems.Items.Count; list++)
            {
                lsItems.Add(cblItems.Items[list].Value);
            }
        }
        catch { lsItems.Clear(); }
        return lsItems;
    }
    private void CallCheckBoxChangedEvent(CheckBoxList cbl, CheckBox cb, TextBox tb, string dispString)
    {
        try
        {
            tb.Text = dispString;
            if (cb.Checked)
            {
                for (int i = 0; i < cbl.Items.Count; i++)
                {
                    cbl.Items[i].Selected = true;
                }
                tb.Text = dispString + "(" + cbl.Items.Count + ")";
            }
            else
            {
                for (int i = 0; i < cbl.Items.Count; i++)
                {
                    cbl.Items[i].Selected = false;
                }
            }
        }
        catch { }
    }
    private void CallCheckBoxListChangedEvent(CheckBoxList cbl, CheckBox cb, TextBox tb, string dispString)
    {
        try
        {
            cb.Checked = false;
            tb.Text = dispString;
            int count = 0;
            for (int i = 0; i < cbl.Items.Count; i++)
            {
                if (cbl.Items[i].Selected == true)
                {
                    count++;
                }
            }
            tb.Text = dispString + "(" + count + ")";
            if (count == cbl.Items.Count)
            {
                cb.Checked = true;
            }
        }
        catch { }
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        Printcontrol.Visible = false;
        popwindow.Visible = true;
        bindclg1();
        bindType1();
        bindbatch1();
        binddegree1();
        bindbranch1();
        bindsem1();
        bindsemdest1();
        bindsec1();
        btn_Save.Visible = false;
        btn_exit.Visible = false;
        spreadStudAdd.Visible = false;
        lbl_Total1.Visible = false;
        btn_go1_Click(sender, e);
    }
    protected void imagebtnpopclose_Click(object sender, EventArgs e)
    {
        popwindow.Visible = false;
    }
    public void bindType1()
    {
        try
        {
            lbl_stream1.Text = useStreamShift();
            ddl_strm1.Items.Clear();
            string query = "select Distinct ISNULL( type,'') as type  from Registration r,Degree d,Department dt,Course c where r.degree_code =d.Degree_Code and d.Dept_Code =dt.Dept_Code and d.Course_Id =c.Course_Id  and type<>''  and r.college_code='" + Convert.ToString(ddl_college1.SelectedValue) + "'  order by type asc";

            DataSet ds = DA.select_method_wo_parameter(query, "Text");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddl_strm1.DataSource = ds;
                ddl_strm1.DataTextField = "type";
                ddl_strm1.DataValueField = "type";
                ddl_strm1.DataBind();
                ddl_strm1.Enabled = true;
            }
            else
            {
                ddl_strm1.Enabled = false;
            }
        }
        catch (Exception ex) { }
    }
    public void bindbatch1()
    {
        try
        {
            ddl_batch1.Items.Clear();
            string sqlyear = "select distinct batch_year from Registration where batch_year<>'-1' and batch_year<>'' and cc=0 and delflag=0 and exam_flag<>'debar' order by batch_year desc";
            DataSet ds = DA.select_method_wo_parameter(sqlyear, "Text");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddl_batch1.DataSource = ds;
                ddl_batch1.DataTextField = "batch_year";
                ddl_batch1.DataValueField = "batch_year";
                ddl_batch1.DataBind();
            }
        }
        catch (Exception ex) { }
    }
    public void binddegree1()
    {
        try
        {
            ddl_degree1.Items.Clear();
            string stream = "";
            stream = ddl_strm1.Items.Count > 0 ? ddl_strm1.SelectedValue : "";

            string query = "select distinct degree.course_id,course.course_name from degree,course,deptprivilages  where course.course_id=degree.course_id and course.college_code = degree.college_code  and degree.college_code in (" + Convert.ToString(ddl_college1.SelectedValue) + ") and deptprivilages.Degree_code=degree.Degree_code and   user_code=" + userCode + " ";
            if (ddl_strm1.Enabled)
            {
                query += " and course.type in ('" + stream + "')";
            }
            DataSet ds = DA.select_method_wo_parameter(query, "Text");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddl_degree1.DataSource = ds;
                ddl_degree1.DataTextField = "course_name";
                ddl_degree1.DataValueField = "course_id";
                ddl_degree1.DataBind();
            }
        }
        catch (Exception ex) { }
    }
    public void bindbranch1()
    {
        try
        {
            cbl_branch1.Items.Clear();
            string degree = "";
            degree = ddl_degree1.Items.Count > 0 ? ddl_degree1.SelectedValue : "";


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
                cbl_branch1.DataSource = ds;
                cbl_branch1.DataTextField = "dept_name";
                cbl_branch1.DataValueField = "degree_code";
                cbl_branch1.DataBind();
                if (cbl_branch1.Items.Count > 0)
                {
                    for (int row = 0; row < cbl_branch1.Items.Count; row++)
                    {
                        cbl_branch1.Items[row].Selected = true;
                        cb_branch1.Checked = true;
                    }
                    txt_branch1.Text = lbl_branch1.Text + "(" + cbl_branch1.Items.Count + ")";
                }
                else
                {
                    txt_branch1.Text = "--Select--";
                }
            }
        }
        catch (Exception ex) { }
    }
    public void bindsem1()
    {
        try
        {
            ddl_sem1.Items.Clear();

            int duration = 0;
            int i = 0;

            string batch = "";
            batch = ddl_batch1.Items.Count > 0 ? ddl_batch1.SelectedValue : "0";
            string branch = "";
            branch = cbl_branch1.Items.Count > 0 ? cbl_branch1.SelectedValue : "0";

            if (branch.Trim() != "" && batch.Trim() != "")
            {
                string strsql1 = "select distinct duration,first_year_nonsemester  from degree where degree_code in ('" + branch + "') and college_code='" + Convert.ToString(ddl_college1.SelectedValue) + "'";
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
                        ddl_sem1.Items.Add(Convert.ToString(i));
                    }
                }
            }
        }
        catch { }
    }
    public void bindsemdest1()
    {
        try
        {
            ddl_semdest.Items.Clear();

            int duration = 0;
            int i = 0;

            string batch = "";
            batch = ddl_batch1.Items.Count > 0 ? ddl_batch1.SelectedValue : "0";
            string branch = "";
            branch = cbl_branch1.Items.Count > 0 ? cbl_branch1.SelectedValue : "0";

            if (branch.Trim() != "" && batch.Trim() != "")
            {
                string strsql1 = "select distinct duration,first_year_nonsemester  from degree where degree_code in ('" + branch + "') and college_code='" + Convert.ToString(ddl_college1.SelectedValue) + "'";
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
                        string cur_sem = Convert.ToString(ddl_sem1.SelectedItem.Value);
                        if (Convert.ToInt32(cur_sem) < i)
                        {
                            ddl_semdest.Items.Add(Convert.ToString(i));
                        }
                    }
                }
            }
        }
        catch { }
    }
    public void bindsec1()
    {
        try
        {
            cbl_sec.Items.Clear();
            string batch = "";
            string branch = "";
            int i = 0;
            if (cbl_branch.Items.Count > 0)
            {
                for (i = 0; i < cbl_branch.Items.Count; i++)
                {

                    if (cbl_branch.Items[i].Selected == true)
                    {
                        string build = cbl_branch.Items[i].Value.ToString();
                        if (branch == "")
                        {
                            branch = build;
                        }
                        else
                        {
                            branch = branch + "','" + build;

                        }
                    }
                }
            }
            batch = ddl_batch1.SelectedItem.Value;


            string sqlquery = "select distinct sections from registration where batch_year in('" + batch + "') and degree_code in('" + branch + "') and sections<>'-1' and sections<>' ' and delflag=0 and exam_flag<>'Debar' order by sections";

            DataSet ds = new DataSet();
            ds = DA.select_method_wo_parameter(sqlquery, "text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_sec1.DataSource = ds;
                ddl_sec1.DataTextField = "sections";
                ddl_sec1.DataValueField = "sections";
                ddl_sec1.DataBind();

            }

        }
        catch
        {
        }
    }

    public void bindclg1()
    {
        try
        {
            ddl_college1.Items.Clear();
            string selectQuery = "select cp.college_code,cf.collname from collegeprivilages cp,collinfo cf where user_code=" + userCode + " and cp.college_code=cf.college_code";
            DataSet ds = DA.select_method_wo_parameter(selectQuery, "Text");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddl_college1.DataSource = ds;
                ddl_college1.DataTextField = "collname";
                ddl_college1.DataValueField = "college_code";
                ddl_college1.DataBind();
            }
        }
        catch (Exception ex) { }
    }
    protected void ddl_college1_OnIndexChange(object sender, EventArgs e)
    {
        bindType1();
        bindbatch1();
        binddegree1();
        bindbranch1();
        bindsem1();
        bindsemdest1();
        bindsec1();
        btn_go1_Click(sender, e);
    }
    protected void ddl_batch1_OnIndexChange(object sender, EventArgs e)
    {
        bindType1();
        binddegree1();
        bindbranch1();
        bindsem1();
        bindsemdest1();
        bindsec1();
        btn_go1_Click(sender, e);
    }
    protected void ddl_strm1_OnIndexChange(object sender, EventArgs e)
    {
        binddegree1();
        bindbranch1();
        bindsem1();
        bindsemdest1();
        bindsec1();
        btn_go1_Click(sender, e);
    }
    protected void ddl_degree1_OnIndexChange(object sender, EventArgs e)
    {
        bindbranch1();
        bindsem1();
        bindsemdest1();
        bindsec1();
        btn_go1_Click(sender, e);
    }
    protected void ddl_branch1_OnIndexChange(object sender, EventArgs e)
    {
        bindsem1();
        bindsemdest1();
        bindsec1();
        btn_go1_Click(sender, e);
    }
    protected void ddl_sem1_OnIndexChange(object sender, EventArgs e)
    {
        bindsec1();
        bindsemdest1();
        btn_go1_Click(sender, e);
    }
    protected void ddl_sec1_OnIndexChange(object sender, EventArgs e)
    {
        btn_go1_Click(sender, e);
    }
    protected void btn_go1_Click(object sender, EventArgs e)
    {
        try
        {
            btn_Save.Visible = false;
            btn_exit.Visible = false;
            lbl_errormsg1.Visible = false;
            lbl_Total1.Visible = false;

            string selectquery;

            string branch = cbl_branch1.Items.Count > 0 ? cbl_branch1.SelectedValue : "";

            string degCode = ddl_degree1.Items.Count > 0 ? ddl_degree1.SelectedValue : "";

            string stream = ddl_strm1.Enabled ? ddl_strm1.Items.Count > 0 ? ddl_strm1.SelectedItem.Text.Trim() : "" : "";

            string section = ddl_sec1.Items.Count > 0 ? ddl_sec1.SelectedItem.Text.Trim() : "";

            string batch_year = ddl_batch1.Items.Count > 0 ? ddl_batch1.SelectedItem.Text : "";

            string cusem = ddl_sem1.Items.Count > 0 ? ddl_sem1.SelectedItem.Text : "";

            DataSet ds = new DataSet();
            if (batch_year != string.Empty && degCode != string.Empty && branch != string.Empty && cusem != string.Empty)
            {
                if (stream != string.Empty)
                {
                    stream = " and c.type in ('" + stream + "')";
                }
                selectquery = "select Current_Semester,Roll_No,Roll_Admit,smart_serial_no,Stud_Name,d.Degree_Code,(C.Course_Name +' - '+ dt.Dept_Name) as Department,Reg_No,r.App_No,c.type,isnull(r.Sections,'') as Sections   from Registration r,Degree d,Department dt,Course c where r.degree_code =d.Degree_Code and dt.Dept_Code =d.Dept_Code and c.Course_Id =d.Course_Id and CC=0 and DelFlag =0 and Exam_Flag <>'DEBAR' and Batch_Year =" + batch_year + " and r.degree_code in ('" + branch + "')  and isnull(r.Sections,'') in ('" + section + "') and r.current_semester in('" + cusem + "')  " + stream + " ";
                ds = DA.select_method_wo_parameter(selectquery, "Text");
            }
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                spreadStudAdd.Sheets[0].RowCount = 1;
                spreadStudAdd.Sheets[0].ColumnCount = 0;
                spreadStudAdd.Sheets[0].ColumnHeader.RowCount = 1;
                spreadStudAdd.CommandBar.Visible = false;
                spreadStudAdd.Sheets[0].ColumnCount = 10;

                spreadStudAdd.Sheets[0].RowHeader.Visible = false;
                spreadStudAdd.Sheets[0].AutoPostBack = false;

                FarPoint.Web.Spread.StyleInfo darkstyle = new FarPoint.Web.Spread.StyleInfo();
                darkstyle.BackColor = ColorTranslator.FromHtml("#0CA6CA");
                darkstyle.ForeColor = Color.Black;
                spreadStudAdd.ActiveSheetView.ColumnHeader.DefaultStyle = darkstyle;

                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 0].Text = " S.No";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 0].Font.Bold = true;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 0].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 0].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].Columns[0].Locked = true;
                spreadStudAdd.Sheets[0].Columns[0].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].Columns[0].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].Columns[0].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Columns[0].Width = 50;

                FarPoint.Web.Spread.CheckBoxCellType chkall = new FarPoint.Web.Spread.CheckBoxCellType();
                chkall.AutoPostBack = true;

                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 1].Text = "Select";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 1].Font.Bold = true;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 1].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 1].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 1].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].Columns[1].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].Columns[1].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].Columns[1].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].Columns[1].Width = 60;
                spreadStudAdd.Sheets[0].Columns[1].Locked = false;
                spreadStudAdd.Sheets[0].Cells[0, 1].CellType = chkall;

                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 2].Text = "Admission Number";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 2].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 2].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 2].Font.Bold = true;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 2].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].Columns[2].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].Columns[2].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].Columns[2].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].Columns[2].Locked = true;
                spreadStudAdd.Columns[2].Width = 150;

                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 3].Text = "Roll No";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 3].Font.Bold = true;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 3].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 3].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 3].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].Columns[3].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].Columns[3].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].Columns[3].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].Columns[3].Locked = true;
                spreadStudAdd.Columns[3].Width = 100;

                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 4].Text = "Reg No";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 4].Font.Bold = true;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 4].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 4].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 4].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].Columns[4].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].Columns[4].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].Columns[4].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].Columns[4].Locked = true;
                spreadStudAdd.Columns[4].Width = 100;

                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 5].Text = "Smartcard No";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 5].Font.Bold = true;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 5].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 5].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 5].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].Columns[5].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].Columns[5].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].Columns[5].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].Columns[5].Locked = true;
                spreadStudAdd.Columns[5].Width = 100;
                spreadStudAdd.Sheets[0].Columns[5].Visible = false;

                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 6].Text = "Student Name";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 6].Font.Bold = true;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 6].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 6].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 6].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].Columns[6].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].Columns[6].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].Columns[6].HorizontalAlign = HorizontalAlign.Left;
                spreadStudAdd.Sheets[0].Columns[6].Locked = true;
                spreadStudAdd.Columns[6].Width = 260;

                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 7].Text = lbl_degree1.Text + "/" + lbl_branch1.Text;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 7].Font.Bold = true;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 7].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 7].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 7].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].Columns[7].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].Columns[7].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].Columns[7].HorizontalAlign = HorizontalAlign.Left;
                spreadStudAdd.Sheets[0].Columns[7].Locked = true;
                spreadStudAdd.Sheets[0].Columns[7].Visible = false;


                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 8].Text = lbl_sem1.Text;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 8].Font.Bold = true;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 8].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 8].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 8].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].Columns[8].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].Columns[8].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].Columns[8].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].Columns[8].Locked = true;
                spreadStudAdd.Columns[8].Width = 70;

                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 9].Text = "Section";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 9].Font.Bold = true;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 9].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 9].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].ColumnHeader.Cells[0, 9].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].Columns[9].Font.Name = "Book Antiqua";
                spreadStudAdd.Sheets[0].Columns[9].Font.Size = FontUnit.Medium;
                spreadStudAdd.Sheets[0].Columns[9].HorizontalAlign = HorizontalAlign.Center;
                spreadStudAdd.Sheets[0].Columns[9].Locked = true;
                spreadStudAdd.Columns[9].Width = 50;

                FarPoint.Web.Spread.TextCellType txtRollno = new FarPoint.Web.Spread.TextCellType();
                FarPoint.Web.Spread.TextCellType txtRegno = new FarPoint.Web.Spread.TextCellType();
                FarPoint.Web.Spread.TextCellType txtRollAd = new FarPoint.Web.Spread.TextCellType();
                FarPoint.Web.Spread.TextCellType txtAppno = new FarPoint.Web.Spread.TextCellType();
                FarPoint.Web.Spread.TextCellType txtSmartno = new FarPoint.Web.Spread.TextCellType();

                for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                {
                    spreadStudAdd.Sheets[0].RowCount++;
                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 0].Text = Convert.ToString(row + 1);
                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 0].Tag = Convert.ToString(ds.Tables[0].Rows[row]["App_No"]);

                    FarPoint.Web.Spread.CheckBoxCellType check = new FarPoint.Web.Spread.CheckBoxCellType();
                    check.AutoPostBack = false;
                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 1].CellType = check;

                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 2].CellType = txtRollAd;
                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 2].Text = Convert.ToString(ds.Tables[0].Rows[row]["Roll_Admit"]);

                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 3].CellType = txtRollno;
                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 3].Text = Convert.ToString(ds.Tables[0].Rows[row]["Roll_No"]);

                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 4].CellType = txtRegno;
                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 4].Text = Convert.ToString(ds.Tables[0].Rows[row]["Reg_No"]);

                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 5].CellType = txtSmartno;
                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 5].Text = Convert.ToString(ds.Tables[0].Rows[row]["smart_serial_no"]);

                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 6].Text = Convert.ToString(ds.Tables[0].Rows[row]["Stud_Name"]);

                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 7].Tag = Convert.ToString(ds.Tables[0].Rows[row]["Degree_Code"]);
                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 7].Text = Convert.ToString(ds.Tables[0].Rows[row]["Department"]);

                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 8].Text = Convert.ToString(ds.Tables[0].Rows[row]["Current_Semester"]);

                    spreadStudAdd.Sheets[0].Cells[spreadStudAdd.Sheets[0].RowCount - 1, 9].Text = Convert.ToString(ds.Tables[0].Rows[row]["Sections"]);
                }
                spreadStudAdd.Visible = true;
                spreadStudAdd.Sheets[0].PageSize = spreadStudAdd.Sheets[0].RowCount;

                spreadStudAdd.Height = 300;
                spreadStudAdd.SaveChanges();
                btn_Save.Visible = true;
                btn_exit.Visible = true;
                lbl_Total1.Visible = true;
                lbl_Total1.Text = "Total Number Of Students : " + (spreadStudAdd.Sheets[0].RowCount - 1);
            }
            else
            {
                spreadStudAdd.Visible = false;
                lbl_errormsg1.Visible = true;
                lbl_errormsg1.Text = "No Records Found";
            }
        }
        catch (Exception ex)
        {
            spreadStudAdd.Visible = false;
            lbl_errormsg1.Visible = true;
            lbl_errormsg1.Text = "No Records Found"; DA.sendErrorMail(ex, Convert.ToString(ddl_college1.SelectedValue), "SectionAllocation");
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            int saved = 0;
            int notsaved = 0;
            int upOk = 0;
            if (ddl_semdest.Items.Count > 0)
            {
                string desSem = ddl_semdest.SelectedItem.Text.Trim();
                string srcSem = ddl_sem1.SelectedItem.Text.Trim();

                string batchYr = ddl_batch1.Items.Count > 0 ? ddl_batch1.SelectedItem.Value : "0";
                string degreeCode = cbl_branch1.Items.Count > 0 ? cbl_branch1.SelectedItem.Value : "0";

                if (desSem != srcSem)
                {
                    if (true)
                    {
                        List<string> appNoList;
                        List<string> rollnoList;
                        if (checkedOK(out appNoList, out rollnoList))
                        {

                            for (int apI = 0; apI < appNoList.Count; apI++)
                            {
                                string appNo = appNoList[apI];
                                string rollNo = rollnoList[apI];

                                if (rdo_sem.Checked == true)
                                {
                                    string upQ = "update Registration set Current_Semester='" + desSem + "' where app_no='" + appNo + "'";
                                    upOk = DA.update_method_wo_parameter(upQ, "Text");
                                    saved++;
                                }
                                else
                                {
                                    notsaved++;
                                }
                                //if (upOk > 0)
                                //{
                                //    string upSubChooser = "update subjectChooser set staffcode ='' where roll_no ='" + rollNo + "'";
                                //    DA.update_method_wo_parameter(upSubChooser, "Text");

                                //   // updateMarks(appNo, rollNo, degreeCode, batchYr, srcSem, desSem);


                                //}

                            }
                            imgAlert.Visible = true;
                            lbl_alert.Text = String.Format("Saved : " + saved + ". \n\n Not Saved : " + notsaved);
                        }
                        else
                        {
                            imgAlert.Visible = true;
                            lbl_alert.Text = "Please Select Students";
                        }
                    }
                    else
                    {
                        imgAlert.Visible = true;
                        lbl_alert.Text = "Please Select Batch And Degree";
                    }
                }
                else
                {
                    imgAlert.Visible = true;
                    lbl_alert.Text = "Destination Section Should Not Be Same As Section";
                }
            }
            else
            {
                imgAlert.Visible = true;
                lbl_alert.Text = "Destination Sections Not Available";
            }
        }
        catch (Exception ex) { DA.sendErrorMail(ex, Convert.ToString(ddl_college1.SelectedValue), "SectionAllocation.aspx"); }
        btn_go1_Click(sender, e);
    }

    public bool checkedOK(out List<string> appNoList, out List<string> rollnoList)
    {
        bool Ok = false;
        appNoList = new List<string>();
        rollnoList = new List<string>();
        spreadStudAdd.SaveChanges();
        for (int i = 1; i < spreadStudAdd.Sheets[0].Rows.Count; i++)
        {
            byte check = Convert.ToByte(spreadStudAdd.Sheets[0].Cells[i, 1].Value);
            if (check == 1)
            {
                Ok = true;
                appNoList.Add(Convert.ToString(spreadStudAdd.Sheets[0].Cells[i, 0].Tag));
                rollnoList.Add(Convert.ToString(spreadStudAdd.Sheets[0].Cells[i, 3].Text));
            }
        }
        return Ok;
    }
    protected void spreadStudAdd_Command(object sender, FarPoint.Web.Spread.SpreadCommandEventArgs e)
    {
        try
        {
            string actrow = spreadStudAdd.Sheets[0].ActiveRow.ToString();
            string actcol = spreadStudAdd.Sheets[0].ActiveColumn.ToString();
            if (actrow.Trim() == "0" && actcol.Trim() == "1")
            {
                if (spreadStudAdd.Sheets[0].RowCount > 0)
                {
                    int checkval = Convert.ToInt32(spreadStudAdd.Sheets[0].Cells[0, 1].Value);
                    if (checkval == 0)
                    {
                        for (int i = 1; i < spreadStudAdd.Sheets[0].RowCount; i++)
                        {
                            spreadStudAdd.Sheets[0].Cells[i, 1].Value = 1;
                        }
                    }
                    if (checkval == 1)
                    {
                        for (int i = 1; i < spreadStudAdd.Sheets[0].RowCount; i++)
                        {
                            spreadStudAdd.Sheets[0].Cells[i, 1].Value = 0;
                        }
                    }
                }
            }
        }
        catch (Exception ex) { }
    }
    public void cb_branch1_checkedchange(object sender, EventArgs e)
    {
        try
        {

            if (cb_branch1.Checked == true)
            {
                for (int i = 0; i < cbl_branch1.Items.Count; i++)
                {
                    cbl_branch1.Items[i].Selected = true;
                }
                txt_branch1.Text = lbl_branch1.Text + "(" + (cbl_branch1.Items.Count) + ")";
            }
            else
            {
                for (int i = 0; i < cbl_branch1.Items.Count; i++)
                {
                    cbl_branch1.Items[i].Selected = false;
                }
                txt_branch1.Text = "--Select--";
            }

        }
        catch
        {
        }
    }
    public void cbl_branch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            int commcount = 0;
            txt_branch1.Text = "--Select--";
            cb_branch1.Checked = false;
            for (int i = 0; i < cbl_branch1.Items.Count; i++)
            {
                if (cbl_branch1.Items[i].Selected == true)
                {
                    commcount = commcount + 1;
                }
            }
            if (commcount == cbl_branch1.Items.Count)
            {
                txt_branch1.Text = lbl_branch1.Text + "(" + commcount.ToString() + ")";
                cb_branch1.Checked = true;
            }

        }
        catch
        {
        }
    }
    public void cb_stream_checkedchange(object sender, EventArgs e)
    {
        try
        {

            if (cb_stream.Checked == true)
            {
                for (int i = 0; i < cbl_stream.Items.Count; i++)
                {
                    cbl_stream.Items[i].Selected = true;
                }
                txt_stream.Text = lbl_stream.Text + "(" + (cbl_stream.Items.Count) + ")";
            }
            else
            {
                for (int i = 0; i < cbl_stream.Items.Count; i++)
                {
                    cbl_stream.Items[i].Selected = false;
                }
                txt_stream.Text = "--Select--";
            }
            binddegree();

        }
        catch
        {
        }
    }
    public void cbl_stream_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            int commcount = 0;
            txt_stream.Text = "--Select--";
            cb_stream.Checked = false;
            for (int i = 0; i < cbl_stream.Items.Count; i++)
            {
                if (cbl_stream.Items[i].Selected == true)
                {
                    commcount = commcount + 1;
                }
            }
            binddegree();

            if (commcount == cbl_stream.Items.Count)
            {
                txt_stream.Text = lbl_stream.Text + "(" + commcount.ToString() + ")";
                cb_stream.Checked = true;
            }
            else if (commcount == 0)
            {
                txt_stream.Text = "--Select--";
            }
            else
            {
                txt_stream.Text = lbl_stream.Text + "(" + commcount.ToString() + ")";
            }

        }
        catch
        {
        }
    }
    public void cb_batch_checkedchange(object sender, EventArgs e)
    {
        try
        {

            string buildvalue1 = "";
            string build1 = "";

            if (cb_batch.Checked == true)
            {
                for (int i = 0; i < cbl_batch.Items.Count; i++)
                {

                    if (cb_batch.Checked == true)
                    {
                        cbl_batch.Items[i].Selected = true;
                        txt_batch.Text = "Batch(" + (cbl_batch.Items.Count) + ")";
                        build1 = cbl_batch.Items[i].Value.ToString();
                        if (buildvalue1 == "")
                        {
                            buildvalue1 = build1;
                        }
                        else
                        {
                            buildvalue1 = buildvalue1 + "'" + "," + "'" + build1;

                        }

                    }
                }


            }
            else
            {
                for (int i = 0; i < cbl_batch.Items.Count; i++)
                {
                    cbl_batch.Items[i].Selected = false;
                    txt_batch.Text = "--Select--";

                }
            }

            bindsem();
            bindsec();

        }
        catch (Exception ex)
        {
        }
    }
    public void cbl_batch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            int seatcount = 0;

            cb_batch.Checked = false;

            string buildvalue = "";
            string build = "";
            for (int i = 0; i < cbl_batch.Items.Count; i++)
            {
                if (cbl_batch.Items[i].Selected == true)
                {
                    seatcount = seatcount + 1;
                    txt_batch.Text = "--Select--";
                    build = cbl_batch.Items[i].Value.ToString();
                    if (buildvalue == "")
                    {
                        buildvalue = build;
                    }
                    else
                    {
                        buildvalue = buildvalue + "'" + "," + "'" + build;

                    }
                }
            }

            if (seatcount == cbl_batch.Items.Count)
            {
                txt_batch.Text = "Batch(" + seatcount.ToString() + ")";
                cb_batch.Checked = true;
            }
            else if (seatcount == 0)
            {
                txt_batch.Text = "--Select--";
                cb_batch.Text = "--Select--";
            }
            else
            {
                txt_batch.Text = "Batch(" + seatcount.ToString() + ")";
            }
            bindsem();
            bindsec();
        }
        catch (Exception ex)
        {
        }
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

        lbl.Add(lblCollege1);
        fields.Add(0);

        lbl.Add(lbl_stream1);
        fields.Add(1);

        lbl.Add(lbl_degree1);
        fields.Add(2);

        lbl.Add(lbl_branch1);
        fields.Add(3);

        lbl.Add(lbl_sem1);
        fields.Add(4);

        lbl_semdestination.Text = "To " + lbl_Sem.Text;
        rdo_sem.Text = lbl_Sem.Text;
        rdo_nonsem.Text = "Non-" + lbl_Sem.Text;

        //Name -0, Stream - 1 ,Degree - 2, Branch - 3, Term - 4
        new HeaderLabelText().setLabels(grouporusercode, ref lbl, fields);
    }
}