using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using InsproDataAccess;
using System.Drawing;
using System.Globalization;
using System.Configuration;

public partial class CoeMod_StaffPerformanceComparisonReport : System.Web.UI.Page
{
    string collegecode = string.Empty;
    string singleuser = string.Empty;
    string group_user = string.Empty;
    string sessstream = string.Empty;
    string selectQuery = string.Empty;
    DAccess2 d2 = new DAccess2();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    DataSet dsprint = new DataSet();
    ArrayList colord = new ArrayList();
    DAccess2 queryObject = new DAccess2();
    DAccess2 da = new DAccess2();
    DataView dvhead = new DataView();
    DataSet dscol = new DataSet();
    Hashtable ht = new Hashtable();
    DataTable dtCommon = new DataTable();
    InsproStoreAccess storeAcc = new InsproStoreAccess();
    Dictionary<string, string> dicSQLParameter = new Dictionary<string, string>();
    Dictionary<string, string> dicQueryParameter = new Dictionary<string, string>();
    static byte roll = 0;

    string userCode = string.Empty;
    string userCollegeCode = string.Empty;
    string singleUser = string.Empty;
    string groupUserCode = string.Empty;
    string qryUserOrGroupCode = string.Empty;
    string collegeCode = string.Empty;
    string batchYear = string.Empty;
    string courseId = string.Empty;
    string degreeCode = string.Empty;
    string semester = string.Empty;
    string orderBy = string.Empty;
    string orderBySetting = string.Empty;
    string qry = string.Empty;
    string qryCollegeCode = string.Empty;
    string qryCollegeCode1 = string.Empty;
    string qryBatchYear = string.Empty;
    string qryDegreeCode = string.Empty;
    string qrySemester = string.Empty;
    string examYear = string.Empty;
    string qryExamYear = string.Empty;
    string examMonth = string.Empty;
    string qryExamMonth = string.Empty;

    InsproDirectAccess dirAcc = new InsproDirectAccess();

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            //****************************************************//
            if (Session["collegecode"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!Request.FilePath.Contains("CoeHome"))
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/CoeMod/CoeHome.aspx");
                    return;
                }
            }
            //****************************************************//
            else
            {
                userCollegeCode = (Session["collegecode"] != null) ? Convert.ToString(Session["collegecode"]).Trim() : "";
                userCode = (Session["usercode"] != null) ? Convert.ToString(Session["usercode"]).Trim() : "";
                singleUser = (Session["single_user"] != null) ? Convert.ToString(Session["single_user"]).Trim() : "";
                groupUserCode = (Session["group_code"] != null) ? Convert.ToString(Session["group_code"]).Trim() : "";
            }
            if (!IsPostBack)
            {
                //ShowReport.Visible = false;
                Bindcollege();
                bindBtch();
                BindDegree();
                bindbranch();
                //BindRightsBasedSectionDetail();
                BindExamYear();
                BindExamMonth();
            }
        }
        catch (Exception ex)
        {
        }

    }

    #region college

    public void Bindcollege()
    {
        try
        {
            ddlCollege.Items.Clear();
            dtCommon.Clear();
            ddlCollege.Enabled = false;
            DataSet dsprint = new DataSet();
            dsprint.Clear();
            string qryUserCodeOrGroupCode = string.Empty;
            string group_user = ((Session["group_code"] != null) ? Convert.ToString(Session["group_code"]) : string.Empty);
            if (group_user.Contains(';'))
            {
                string[] group_semi = group_user.Split(';');
                group_user = Convert.ToString(group_semi[0]);
            }
            if ((Convert.ToString(group_user).Trim() != "") && Session["group_code"] != null && Session["single_user"] != null && Convert.ToString(Session["single_user"]).Trim() != "1" && Convert.ToString(Session["single_user"]).Trim().ToLower() != "true")
            {
                qryUserCodeOrGroupCode = " and group_code='" + group_user + "'";
            }
            else if (Session["usercode"] != null && !string.IsNullOrEmpty(Convert.ToString(Session["usercode"]).Trim()))
            {
                qryUserCodeOrGroupCode = " and user_code='" + Convert.ToString(Session["usercode"]).Trim() + "'";
            }
            if (!string.IsNullOrEmpty(qryUserCodeOrGroupCode))
            {
                dicQueryParameter.Clear();
                dicQueryParameter.Add("column_field", Convert.ToString(qryUserCodeOrGroupCode));
                dtCommon = storeAcc.selectDataTable("bind_college", dicQueryParameter);
            }
            if (dtCommon.Rows.Count > 0)
            {
                ddlCollege.DataSource = dtCommon;
                ddlCollege.DataTextField = "collname";
                ddlCollege.DataValueField = "college_code";
                ddlCollege.DataBind();
                ddlCollege.SelectedIndex = 0;
                ddlCollege.Enabled = true;
            }
        }
        catch (Exception ex)
        {
        }
    }

    #endregion

    #region batch
    public void bindBtch()
    {
        try
        {

            cbl_batch.Items.Clear();
            cb_batch.Checked = false;
            txt_batch.Text = "---Select---";
            ds.Clear();
            ds = d2.BindBatch();
            if (ds.Tables[0].Rows.Count > 0)
            {
                cbl_batch.DataSource = ds;
                cbl_batch.DataTextField = "batch_year";
                cbl_batch.DataValueField = "batch_year";
                cbl_batch.DataBind();
                if (cbl_batch.Items.Count > 0)
                {
                    for (int i = 0; i < cbl_batch.Items.Count; i++)
                    {
                        cbl_batch.Items[i].Selected = true;
                    }
                    txt_batch.Text = "Batch(" + cbl_batch.Items.Count + ")";
                    cb_batch.Checked = true;
                }
            }
        }
        catch { }
    }

    #endregion


    #region degree

    public void BindDegree()
    {
        string college_code = Convert.ToString(ddlCollege.SelectedValue).Trim();
        string query = string.Empty;
        ddlDegree.Items.Clear();
        string usercode = Convert.ToString(Session["usercode"]).Trim();
        string singleuser = Convert.ToString(Session["single_user"]).Trim();
        string group_user = Convert.ToString(Session["group_code"]).Trim();
        if (group_user.Contains(";"))
        {
            string[] group_semi = group_user.Split(';');
            group_user = group_semi[0].ToString();
        }
        if ((Convert.ToString(group_user).Trim() != "") && (Convert.ToString(group_user).Trim() != "0") && (Convert.ToString(group_user).Trim() != "-1"))
        {
            query = "select distinct degree.course_id,course.course_name from degree,course,deptprivilages where course.course_id=degree.course_id and course.college_code = degree.college_code and degree.college_code='" + college_code + "' and deptprivilages.Degree_code=degree.Degree_code and group_code='" + group_user + "' ";
        }
        else
        {
            query = "select distinct degree.course_id,course.course_name from degree,course,deptprivilages where course.course_id=degree.course_id and course.college_code = degree.college_code and degree.college_code='" + college_code + "' and deptprivilages.Degree_code=degree.Degree_code and user_code='" + usercode + "' ";
        }
        DataSet ds = new DataSet();
        ds.Clear();
        ds = da.select_method_wo_parameter(query, "Text");
        // DataSet ds = ClsAttendanceAccess.GetDegreeDetail(collegecode.ToString());
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlDegree.DataSource = ds;
            ddlDegree.DataValueField = "Course_Id";
            ddlDegree.DataTextField = "Course_Name";
            ddlDegree.DataBind();
            // ddlDegree.Items.Insert(0, new ListItem("--Select--", "-1"));
        }
    }

    #endregion

    #region Branch

    public void bindbranch()
    {
        try
        {
            DataSet ds = new DataSet();
            ds.Clear();
            ddlBranch.Items.Clear();
            ht.Clear();
            string usercode = Convert.ToString(Session["usercode"]).Trim();
            string collegecode = Convert.ToString(ddlCollege.SelectedValue).Trim();
            string singleuser = Convert.ToString(Session["single_user"]).Trim();
            string group_user = Convert.ToString(Session["group_code"]).Trim();
            if (group_user.Contains(";"))
            {
                string[] group_semi = group_user.Split(';');
                group_user = Convert.ToString(group_semi[0]).Trim();
            }
            string course_id = string.Empty;// ddlDegree.SelectedValue.ToString();
            if (ddlDegree.Items.Count > 0)
            {
                course_id = Convert.ToString(ddlDegree.SelectedValue).Trim();
                string query = string.Empty;
                if ((Convert.ToString(group_user).Trim() != "") && (Convert.ToString(group_user).Trim() != "0") && (Convert.ToString(group_user).Trim() != "-1"))
                {
                    query = "select distinct degree.degree_code,department.dept_name from degree,department,course,deptprivilages where course.course_id=degree.course_id and department.dept_code=degree.dept_code and course.college_code = degree.college_code and department.college_code = degree.college_code and degree.course_id='" + course_id + "' and degree.college_code='" + collegecode + "' and deptprivilages.Degree_code=degree.Degree_code and group_code='" + group_user + "'";
                }
                else
                {
                    query = "select distinct degree.degree_code,department.dept_name from degree,department,course,deptprivilages where course.course_id=degree.course_id and department.dept_code=degree.dept_code and course.college_code = degree.college_code and department.college_code = degree.college_code and degree.course_id='" + course_id + "' and degree.college_code='" + collegecode + "' and deptprivilages.Degree_code=degree.Degree_code and user_code='" + usercode + "' ";
                }
                ds = da.select_method_wo_parameter(query, "Text");
            }
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int count2 = ds.Tables[0].Rows.Count;
                if (count2 > 0)
                {
                    ddlBranch.DataSource = ds;
                    ddlBranch.DataTextField = "dept_name";
                    ddlBranch.DataValueField = "degree_code";
                    ddlBranch.DataBind();
                }
            }
        }
        catch
        {
        }
    }

    #endregion

    #region ExamYear

    public void BindExamYear()
    {
        try
        {
            ddlExamYear.Items.Clear();
            ds.Clear();
            collegeCode = string.Empty;
            batchYear = string.Empty;
            degreeCode = string.Empty;
            qryCollegeCode = string.Empty;
            qryDegreeCode = string.Empty;
            qryBatchYear = string.Empty;
            if (ddlCollege.Items.Count > 0)
            {
                collegeCode = Convert.ToString(ddlCollege.SelectedValue).Trim();
                if (!string.IsNullOrEmpty(collegeCode))
                {
                    qryCollegeCode = " and dg.college_code in (" + collegeCode + ")";
                }
            }
            batchYear = "";
            for ( int i = 0; i < cbl_batch.Items.Count; i++)
            {
                if (cbl_batch.Items[i].Selected == true)
                {
                    if (batchYear == "")
                    {
                        batchYear = Convert.ToString(cbl_batch.Items[i].Text);
                    }
                    else
                    {
                        batchYear += "," + Convert.ToString(cbl_batch.Items[i].Text);
                    }
                }

            }
            
                if (!string.IsNullOrEmpty(batchYear))
                {
                    qryBatchYear = " and ed.batch_year in(" + batchYear + ")";
                }
            
            if (ddlBranch.Items.Count > 0)
            {
                degreeCode = Convert.ToString(ddlBranch.SelectedValue).Trim();
                if (!string.IsNullOrEmpty(degreeCode))
                {
                    qryDegreeCode = " and ed.degree_code in(" + degreeCode + ")";
                }
            }
            if (!string.IsNullOrEmpty(qryCollegeCode) && !string.IsNullOrEmpty(qryBatchYear) && !string.IsNullOrEmpty(qryDegreeCode))
            {
                string qry = "select distinct ed.Exam_year from exam_details ed,Degree dg,Course c,Department dt where dg.Degree_Code=ed.degree_code and  c.Course_Id=dg.Course_Id and dg.Dept_Code=dt.Dept_Code and ed.Exam_year<>'0' " + qryCollegeCode + qryDegreeCode + qryBatchYear + " order by ed.Exam_year desc";
                ds.Clear();
                ds.Reset();
                ds.Dispose();
                ds = da.select_method_wo_parameter(qry, "text");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlExamYear.DataSource = ds;
                    ddlExamYear.DataTextField = "Exam_year";
                    ddlExamYear.DataValueField = "Exam_year";
                    ddlExamYear.DataBind();
                    ddlExamYear.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    #endregion

    #region ExamMonth

    private void BindExamMonth()
    {
        try
        {
            string collegeCode = string.Empty;
            string batchYear = string.Empty;
            string degreeCode = string.Empty;
            ddlExamMonth.Items.Clear();
            ds.Clear();
            collegeCode = string.Empty;
            batchYear = string.Empty;
            degreeCode = string.Empty;
            qryCollegeCode = string.Empty;
            qryDegreeCode = string.Empty;
            qryBatchYear = string.Empty;
            if (ddlCollege.Items.Count > 0)
            {
                collegeCode = Convert.ToString(ddlCollege.SelectedValue).Trim();
                if (!string.IsNullOrEmpty(collegeCode))
                {
                    qryCollegeCode = " and dg.college_code in (" + collegeCode + ")";
                }
            }

            batchYear = "";
            for (int i = 0; i < cbl_batch.Items.Count; i++)
            {
                if (cbl_batch.Items[i].Selected == true)
                {
                    if (batchYear == "")
                    {
                        batchYear = Convert.ToString(cbl_batch.Items[i].Text);
                    }
                    else
                    {
                        batchYear += "," + Convert.ToString(cbl_batch.Items[i].Text);
                    }
                }

            }

            if (!string.IsNullOrEmpty(batchYear))
            {
                qryBatchYear = " and ed.batch_year in(" + batchYear + ")";
            }
            
            if (ddlBranch.Items.Count > 0)
            {
                degreeCode = Convert.ToString(ddlBranch.SelectedValue).Trim();
                if (!string.IsNullOrEmpty(degreeCode))
                {
                    qryDegreeCode = " and ed.degree_code in(" + degreeCode + ")";
                }
            }
            examYear = string.Empty;
            qryExamYear = string.Empty;
            if (ddlExamYear.Items.Count > 0)
            {
                foreach (System.Web.UI.WebControls.ListItem li in ddlExamYear.Items)
                {
                    if (li.Selected)
                    {
                        if (string.IsNullOrEmpty(examYear))
                        {
                            examYear = "'" + li.Value + "'";
                        }
                        else
                        {
                            examYear += ",'" + li.Value + "'";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(examYear))
                {
                    qryExamYear = " and Exam_year in (" + examYear + ")";
                }
            }
            if (!string.IsNullOrEmpty(qryExamYear) && !string.IsNullOrEmpty(qryCollegeCode) && !string.IsNullOrEmpty(qryBatchYear) && !string.IsNullOrEmpty(qryDegreeCode))
            {
                string qry = "select distinct ed.Exam_Month,upper(convert(varchar(3),DateAdd(month,ed.Exam_Month,-1))) as Month_Name from exam_details ed,Degree dg,Course c,Department dt where dg.Degree_Code=ed.degree_code and  c.Course_Id=dg.Course_Id and dg.Dept_Code=dt.Dept_Code and ed.Exam_Month<>'0' " + qryCollegeCode + qryDegreeCode + qryBatchYear + qryExamYear + " order by Exam_Month";
                ds.Clear();
                ds.Reset();
                ds.Dispose();
                ds = da.select_method_wo_parameter(qry, "text");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlExamMonth.DataSource = ds;
                    ddlExamMonth.DataTextField = "Month_Name";
                    ddlExamMonth.DataValueField = "Exam_Month";
                    ddlExamMonth.DataBind();
                    ddlExamMonth.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    #endregion


    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindBtch();
            BindDegree();
            bindbranch();
            //BindRightsBasedSectionDetail();
            BindExamYear();
            BindExamMonth();
            //ShowReport.Visible = false;

            
        }
        catch (Exception ex)
        {
        }
    }


    protected void cb_batch_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            txt_batch.Text = "--Select--";
            if (cb_batch.Checked == true)
            {

                for (int i = 0; i < cbl_batch.Items.Count; i++)
                {
                    cbl_batch.Items[i].Selected = true;
                }
                txt_batch.Text = "Batch(" + (cbl_batch.Items.Count) + ")";
            }
            else
            {
                for (int i = 0; i < cbl_batch.Items.Count; i++)
                {
                    cbl_batch.Items[i].Selected = false;
                }
            }
            BindDegree();
            bindbranch();
            //BindRightsBasedSectionDetail();
            BindExamYear();
            BindExamMonth();
            //ShowReport.Visible = false;

        }
        catch { }
    }
    protected void cbl_batch_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            cb_batch.Checked = false;
            int commcount = 0;
            txt_batch.Text = "--Select--";
            for (i = 0; i < cbl_batch.Items.Count; i++)
            {
                if (cbl_batch.Items[i].Selected == true)
                {
                    commcount = commcount + 1;
                }
            }
            if (commcount > 0)
            {
                if (commcount == cbl_batch.Items.Count)
                {
                    cb_batch.Checked = true;
                }
                txt_batch.Text = "Batch(" + commcount.ToString() + ")";
            }
            BindDegree();
            bindbranch();
            //BindRightsBasedSectionDetail();
            BindExamYear();
            BindExamMonth();
            //ShowReport.Visible = false;
        }
        catch { }
    }

    protected void ddlDegree_SelectedIndexChanged(Object sender, EventArgs e)
    {
        try
        {
            bindbranch();
            //BindRightsBasedSectionDetail();
            BindExamYear();
            BindExamMonth();
            //ShowReport.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlBranch_SelectedIndexChanged(Object sender, EventArgs e)
    {
        try
        {
            //BindRightsBasedSectionDetail();
            BindExamYear();
            BindExamMonth();
            //ShowReport.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlExamYear_SelectedIndexChanged(Object sender, EventArgs e)
    {
        try
        {
            BindExamMonth();
            //ShowReport.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlExamMonth_SelectedIndexChanged(Object sender, EventArgs e)
    {
        try
        {
            //ShowReport.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
    }


    #region PopupAlert

    protected void btnPopAlertClose_Click(object sender, EventArgs e)
    {
        try
        {

            lblAlertMsg.Text = string.Empty;
            divPopAlert.Visible = false;
        }
        catch
        {


        }
    }

    #endregion
}