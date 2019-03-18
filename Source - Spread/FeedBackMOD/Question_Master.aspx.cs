﻿using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Configuration;

public partial class Question_Master : System.Web.UI.Page
{
    bool cellclick = false;
    string usercode = string.Empty;
    string collegecode1 = string.Empty;
    string collegecode = string.Empty;
    string singleuser = string.Empty;
    string group_user = string.Empty;
    static string clgcode1 = string.Empty;
    string selectQuery = "";
    DAccess2 d2 = new DAccess2();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();

    static int gridview1_selectedrow = -1, gridview1_selectedcol = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["collegecode"] == null)
        //{
        //    Response.Redirect("~/Default.aspx");
        //}
        try
        {
            if (Session["collegecode"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!Request.FilePath.Contains("Feedbackhome"))
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/FeedBackMOD/Feedbackhome.aspx");
                    return;
                }
            }

            usercode = Session["usercode"].ToString();
            collegecode1 = Session["collegecode"].ToString();
            clgcode1 = collegecode1;
            singleuser = Session["single_user"].ToString();
            group_user = Session["group_code"].ToString();

            if (!IsPostBack)
            {
                bindclg();
                bindclg1();
                loaddesc();
                bindheader();
                BindType();
                /// questions();
                rb_Acad1.Checked = true;
                rb_Gend1.Checked = false;

                rdb_obj.Visible = true;
                rdb_desc.Visible = true;

                //btn_search_Click(object sender, EventArgs e)

            }

        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }

    }



    public void Cb_college_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int cout = 0;
            Txt_college.Text = "--Select--";
            if (Cb_college.Checked == true)
            {
                cout++;
                for (int i = 0; i < Cbl_college.Items.Count; i++)
                {
                    Cbl_college.Items[i].Selected = true;
                }
                Txt_college.Text = "College(" + (Cbl_college.Items.Count) + ")";
            }
            else
            {
                for (int i = 0; i < Cbl_college.Items.Count; i++)
                {
                    Cbl_college.Items[i].Selected = false;
                }
                Txt_college.Text = "--Select--";
            }
            bindheader();
        }

        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }

    }
    public void Cbl_college_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            int commcount = 0;
            Txt_college.Text = "--Select--";
            Cb_college.Checked = false;

            for (int i = 0; i < Cbl_college.Items.Count; i++)
            {
                if (Cbl_college.Items[i].Selected == true)
                {
                    commcount = commcount + 1;
                    Cb_college.Checked = false;
                }
            }
            if (commcount > 0)
            {
                if (commcount == Cbl_college.Items.Count)
                {

                    Cb_college.Checked = true;
                }
                Txt_college.Text = "College(" + commcount.ToString() + ")";

            }
            bindheader();


        }

        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }

    }
    public void bindclg()
    {
        try
        {
            ds.Clear();
            Cbl_college.Items.Clear();
            string clgname = "select cp.college_code,cf.collname from collegeprivilages cp,collinfo cf where user_code=" + Session["usercode"] + " and cp.college_code=cf.college_code";
            ds = d2.select_method_wo_parameter(clgname, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Cbl_college.DataSource = ds;
                Cbl_college.DataTextField = "collname";
                Cbl_college.DataValueField = "college_code";
                Cbl_college.DataBind();
            }
            if (Cbl_college.Items.Count > 0)
            {
                for (int row = 0; row < Cbl_college.Items.Count; row++)
                {
                    Cbl_college.Items[row].Selected = true;
                    Cb_college.Checked = true;
                }
                Txt_college.Text = "College(" + Cbl_college.Items.Count + ")";


            }

            else
            {

                Txt_college.Text = "--Select--";
            }
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }

    public void Cb_college1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int cout = 0;
            Txt_college1.Text = "--Select--";
            if (Cb_college1.Checked == true)
            {
                cout++;
                for (int i = 0; i < Cbl_college1.Items.Count; i++)
                {
                    Cbl_college1.Items[i].Selected = true;
                }
                Txt_college1.Text = "College(" + (Cbl_college1.Items.Count) + ")";
            }
            else
            {
                for (int i = 0; i < Cbl_college1.Items.Count; i++)
                {
                    Cbl_college1.Items[i].Selected = false;
                }
                Txt_college1.Text = "--Select--";
            }

            string college = "";
            for (int row = 0; row < Cbl_college1.Items.Count; row++)
            {
                if (Cbl_college1.Items[row].Selected == true)
                {
                    if (college == "")
                    {
                        college = Cbl_college1.Items[row].Value;
                    }
                    else
                    {
                        college = college + "'" + "," + "'" + Cbl_college1.Items[row].Value;
                    }
                }
            }

            clgcode1 = college;
        }


        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }
    public void Cbl_college1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            int commcount = 0;
            Txt_college1.Text = "--Select--";
            Cb_college1.Checked = false;

            for (int i = 0; i < Cbl_college1.Items.Count; i++)
            {
                if (Cbl_college1.Items[i].Selected == true)
                {
                    commcount = commcount + 1;
                    Cb_college1.Checked = false;
                }
            }
            if (commcount > 0)
            {
                if (commcount == Cbl_college1.Items.Count)
                {

                    Cb_college1.Checked = true;
                }
                Txt_college1.Text = "College(" + commcount.ToString() + ")";

            }
            //bindhostelname();
            string college = "";
            for (int row = 0; row < Cbl_college1.Items.Count; row++)
            {
                if (Cbl_college1.Items[row].Selected == true)
                {
                    if (college == "")
                    {
                        college = Cbl_college1.Items[row].Value;
                    }
                    else
                    {
                        college = college + "'" + "," + "'" + Cbl_college1.Items[row].Value;
                    }
                }
            }

            clgcode1 = college;

        }

        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }
    public void bindclg1()
    {
        try
        {
            ds.Clear();
            Cbl_college1.Items.Clear();
            string clgname = "select cp.college_code,cf.collname from collegeprivilages cp,collinfo cf where user_code=" + Session["usercode"] + " and cp.college_code=cf.college_code";
            ds = d2.select_method_wo_parameter(clgname, "Text");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Cbl_college1.DataSource = ds;
                Cbl_college1.DataTextField = "collname";
                Cbl_college1.DataValueField = "college_code";
                Cbl_college1.DataBind();
            }
            if (Cbl_college1.Items.Count > 0)
            {
                for (int row = 0; row < Cbl_college1.Items.Count; row++)
                {
                    Cbl_college1.Items[row].Selected = true;
                    Cb_college1.Checked = true;
                }
                Txt_college1.Text = "College(" + Cbl_college1.Items.Count + ")";
                string college = "";
                for (int row = 0; row < Cbl_college1.Items.Count; row++)
                {
                    if (Cbl_college1.Items[row].Selected == true)
                    {
                        if (college == "")
                        {
                            college = Cbl_college1.Items[row].Value;
                        }
                        else
                        {
                            college = college + "'" + "," + "'" + Cbl_college1.Items[row].Value;
                        }
                    }
                }

                clgcode1 = college;
            }

            else
            {

                Txt_college1.Text = "--Select--";
            }
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }



    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {
            rptprint1.Visible = true;
            div1.Visible = true;
            questions();
            Printcontrol1.Visible = false;
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }

    }
    protected void rb_Acad1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rb_Acad1.Checked == true)
            {
                rdb_obj.Visible = true;
                rdb_desc.Visible = true;
            }
            bindheader();

        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }

    }
    protected void rb_Gend1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rb_Gend1.Checked == true)
            {
                rdb_obj.Visible = true;
                rdb_desc.Visible = true;
                rdb_obj.Checked = true;
                rdb_desc.Checked = false;



            }
            bindheader();
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }

    }

    protected void rb_Acad2_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            txt_qstn.Text = "";

            bindclg1();

            loaddesc();
            rb_Acad2.Checked = true;
            rb_Gend2.Checked = false;
            //rb_easy.Checked = true;
            //rb_Medium.Checked = false;
            //rdb_Difficult.Checked = false;
            //rdb_VeryDifficult.Checked = false;
            if (rb_Acad2.Checked == true)
            {
                rdbobjective.Visible = true;
                rdbdescriptive.Visible = true;

                //lbl_tough.Visible = true;
                //rb_easy.Visible = true;
                //rb_Medium.Visible = true;
                //rdb_Difficult.Visible = true;
                //rdb_VeryDifficult.Visible = true;
                lbloption.Visible = true;
                cb_option.Visible = true;
                cbl_option.Visible = true;
            }

        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }
    protected void rb_Gend2_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            txt_qstn.Text = "";

            bindclg1();

            loaddesc();
            rb_Acad2.Checked = false;
            rb_Gend2.Checked = true;
            //rb_easy.Checked = true;
            //rb_Medium.Checked = false;
            //rdb_Difficult.Checked = false;
            //rdb_VeryDifficult.Checked = false;
            if (rb_Gend2.Checked == true)
            {
                rdbobjective.Visible = true;
                rdbdescriptive.Visible = true;
                rdbobjective.Checked = true;
                rdbdescriptive.Checked = false;

                //lbl_tough.Visible = true;
                //rb_easy.Visible = true;
                //rb_Medium.Visible = true;
                //rdb_Difficult.Visible = true;
                //rdb_VeryDifficult.Visible = true;
                lbloption.Visible = true;
                cb_option.Visible = true;
                cbl_option.Visible = true;
            }

        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }

    protected void btnplus_Click(object sender, EventArgs e)
    {
        try
        {
            txt_header.Text = "";
            imgdiv5.Visible = true;
            panel_header.Visible = true;
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }

    }
    protected void btnminus_Click(object sender, EventArgs e)
    {
        try
        {
            imgdiv4.Visible = true;
            lbl_warningmsghed.Visible = true;
            lbl_warningmsghed.Text = "Are You Sure You Want Delete?";

        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }


    }
    public void loaddesc()
    {
        try
        {
            ddl_group.Items.Clear();
            ds.Tables.Clear();
            ddl_group.Items.Insert(0, new ListItem("--Select--", "0"));

            string sql = "select TextCode,TextVal from TextValTable where TextCriteria ='QHead' and college_code ='" + collegecode1 + "'";
            ds = d2.select_method_wo_parameter(sql, "TEXT");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_group.DataSource = ds;
                ddl_group.DataTextField = "TextVal";
                ddl_group.DataValueField = "TextCode";
                ddl_group.DataBind();
                ddl_group.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                //ddl_group.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }

    }

    protected void btn_addheader_Click(object sender, EventArgs e)
    {
        try
        {
            //string header = txt_header.Text.ToString();

            txt_header.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txt_header.Text);

            if (txt_header.Text != "")
            {


                string sql = "if exists ( select * from TextValTable where TextVal ='" + txt_header.Text + "' and TextCriteria ='QHead' and college_code ='" + collegecode1 + "') update TextValTable set TextVal ='" + txt_header.Text + "' where TextVal ='" + txt_header.Text + "' and TextCriteria ='QHead' and college_code ='" + collegecode1 + "' else insert into TextValTable (TextVal,TextCriteria,college_code) values ('" + txt_header.Text + "','QHead','" + collegecode1 + "')";
                int insert = d2.update_method_wo_parameter(sql, "TEXT");
                if (insert != 0)
                {
                    imgdiv2.Visible = true;
                    lbl_erroralert.Text = "Saved Successfully";
                    txt_header.Text = "";

                    panel_header.Visible = false;
                    imgdiv5.Visible = false;
                }
                loaddesc();
            }
            else
            {
                imgdiv2.Visible = true;
                lbl_erroralert.Text = "Enter the HeaderName";
            }
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }

    protected void btn_exitheader_Click(object sender, EventArgs e)
    {
        try
        {
            imgdiv2.Visible = false;
            panel_header.Visible = false;
            imgdiv5.Visible = false;
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }

    }


    public void btn_warningmsghed_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_group.Items.Count > 0)
            {
                imgdiv4.Visible = false;
                string college = "";
                for (int row = 0; row < Cbl_college1.Items.Count; row++)
                {
                    if (Cbl_college1.Items[row].Selected == true)
                    {
                        if (college == "")
                        {
                            college = Cbl_college1.Items[row].Value;
                        }
                        else
                        {
                            college = college + "','" + Cbl_college1.Items[row].Value;
                        }
                    }
                }

                string sql = "delete from textvaltable where TextCode='" + ddl_group.SelectedItem.Value.ToString() + "' and TextCriteria='QHead' and college_code in ('" + college + "') ";
                int delete = d2.update_method_wo_parameter(sql, "TEXT");
                if (delete != 0)
                {
                    imgdiv2.Visible = true;
                    lbl_erroralert.Text = "Deleted Successfully";
                }

                loaddesc();
            }

            else
            {
                imgdiv2.Visible = true;
                lbl_erroralert.Text = "No records found";
            }
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }
    public void btn_warningmsghed_exit_Click(object sender, EventArgs e)
    {
        imgdiv4.Visible = false;

    }


    public void btndel_Click(object sender, EventArgs e)
    {
        try
        {
            imgdiv3.Visible = true;
            lbl_warning_alert.Visible = true;
            lbl_warning_alert.Text = "Are You Sure You Want Delete?";

            // lbl_erroralert.Visible = false;
            //imgdiv2.Visible = false;
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }

    }

    public void btn_warningmsg_Click(object sender, EventArgs e)
    {
        try
        {
            imgdiv3.Visible = false;
            lbl_warning_alert.Visible = false;
            int savecc = 0;
            string question = txt_qstn.Text;
            string activerow = gridview1_selectedrow.ToString();



            string[] tagvalues = gridview1.Rows[Convert.ToInt32(activerow)].Cells[gridview1.HeaderRow.Cells.Count - 1].Text.Split('^');

            string pkval = tagvalues[0];



            string colege = tagvalues[4];






            if ((activerow != "") && (activerow != "-1"))
            {

                string questionpk = d2.GetFunction("select distinct QuestionMasterFK from CO_FeedBackQuestions where QuestionMasterFK ='" + pkval + "'");

                if (questionpk != pkval)
                {
                    string sql = "delete  from CO_QuestionMaster where  QuestionMasterPK = '" + pkval + "' and collegecode='" + colege + "'";
                    int qry = d2.update_method_wo_parameter(sql, "Text");

                    savecc++;
                    if (savecc > 0)
                    {
                        lbl_erroralert.Text = "Deleted Successfully";
                        lbl_erroralert.Visible = true;
                        imgdiv2.Visible = true;


                        bindheader();
                        questions();
                    }
                }
                else
                {
                    lbl_erroralert.Text = "Sorry This Question Added in FeedBack  ";
                    lbl_erroralert.Visible = true;
                    imgdiv2.Visible = true;
                }
            }

            addnew.Visible = false;
            txt_qstn.Text = "";
            loaddesc();

        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }

    }

    public void btn_warning_exit_Click(object sender, EventArgs e)
    {
        imgdiv3.Visible = false;
    }
    protected void btnerrclose_Click(object sender, EventArgs e)
    {
        imgdiv2.Visible = false;

    }
    //public void cb_batch_CheckedChanged(object sender, EventArgs e)
    //{abbreviation = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(abbreviation);

    //    try
    //    {
    //        int count = 0;
    //        txt_batch.Text = "--Select--";
    //        if (cb_batch.Checked == true)
    //        {
    //            count++;
    //            for (int i = 0; i < cbl_batch.Items.Count; i++)
    //            {
    //                cbl_batch.Items[i].Selected = true;
    //            }
    //            txt_batch.Text = "Batch(" + (cbl_batch.Items.Count) + ")";


    //        }
    //        else
    //        {
    //            for (int i = 0; i < cbl_batch.Items.Count; i++)
    //            {
    //                cbl_batch.Items[i].Selected = false;
    //            }
    //            txt_batch.Text = "--Select--";
    //        }
    //        BindDegree();
    //        bindbranch();
    //        bindsem();
    //        bindsec();

    //    }
    //    catch (Exception ex)
    //    {

    //    }

    //}
    //public void cbl_batch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int commcount = 0;
    //        string buildvalue = "";
    //        string build = "";
    //        cb_batch.Checked = false;
    //        txt_batch.Text = "--Select--";


    //        for (int i = 0; i < cbl_batch.Items.Count; i++)
    //        {
    //            if (cbl_batch.Items[i].Selected == true)
    //            {
    //                commcount = commcount + 1;
    //                //cb_batch.Checked = false;
    //                build = cbl_batch.Items[i].Value.ToString();
    //                if (buildvalue == "")
    //                {
    //                    buildvalue = build;
    //                }
    //                else
    //                {
    //                    buildvalue = buildvalue + "'" + "," + "'" + build;
    //                }

    //            }

    //        }


    //        if (commcount > 0)
    //        {
    //            txt_batch.Text = "Batch(" + commcount.ToString() + ")";
    //            if (commcount == cbl_batch.Items.Count)
    //            {
    //                cb_batch.Checked = true;
    //            }
    //            txt_batch.Text = "Batch(" + commcount.ToString() + ")";
    //        }
    //        BindDegree();
    //        bindbranch();
    //        bindsem();
    //        bindsec();

    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    // public void BindBatch()
    //{
    //    try
    //    {
    //        cbl_batch.Items.Clear();

    //        ds = d2.BindBatch();

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            cbl_batch.DataSource = ds;
    //            cbl_batch.DataTextField = "batch_year";
    //            cbl_batch.DataValueField = "batch_year";
    //            cbl_batch.DataBind();
    //        }
    //        if (cbl_batch.Items.Count > 0)
    //        {
    //            for (int row = 0; row < cbl_batch.Items.Count; row++)
    //            {
    //                cbl_batch.Items[row].Selected = true;
    //                cb_batch.Checked = true;
    //            }
    //            txt_batch.Text = "Batch(" + cbl_batch.Items.Count + ")";
    //        }

    //        else
    //        {

    //            txt_batch.Text = "--Select--";
    //        }
    //        BindDegree();
    //    }
    //    catch
    //    {
    //    }

    //}
    // public void cb_degree_CheckedChanged(object sender, EventArgs e)
    // {
    //     try
    //     {
    //         int count = 0;
    //         txt_degree.Text = "--Select--";

    //         if (cb_degree.Checked == true)
    //         {
    //             count++;
    //             for (int i = 0; i < cbl_degree.Items.Count; i++)
    //             {
    //                 cbl_degree.Items[i].Selected = true;
    //             }
    //             txt_degree.Text = "Degree(" + (cbl_degree.Items.Count) + ")";

    //         }


    //         else
    //         {
    //             for (int i = 0; i < cbl_degree.Items.Count; i++)
    //             {
    //                 cbl_degree.Items[i].Selected = false;
    //                 //txt_degree.Text = "--Select--";
    //                 //txtbranch.Text = "--Select--";
    //                 //chklstbranch.ClearSelection();
    //                 //chkbranch.Checked = false;
    //             }
    //             txt_degree.Text = "--Select--";
    //         }


    //         bindbranch();
    //         bindsem();
    //         bindsec();
    //         // bindhostelname();

    //     }
    //     catch (Exception ex)
    //     {
    //     }

    // }
    // public void cbl_degree_SelectedIndexChanged(object sender, EventArgs e)
    // {

    //     try
    //     {
    //         int i = 0;
    //         int commcount = 0;
    //         cb_degree.Checked = false;
    //         txt_degree.Text = "--Select--";
    //         for (i = 0; i < cbl_degree.Items.Count; i++)
    //         {
    //             if (cbl_degree.Items[i].Selected == true)
    //             {
    //                 commcount = commcount + 1;
    //             }
    //         }
    //         if (commcount > 0)
    //         {
    //             if (commcount == cbl_degree.Items.Count)
    //             {
    //                 cb_degree.Checked = true;
    //             }
    //             txt_degree.Text = "Degree (" + commcount.ToString() + ")";
    //         }


    //         bindbranch();
    //         bindsem();
    //         bindsec();

    //     }
    //     catch (Exception ex)
    //     {

    //     }

    // }
    // public void BindDegree()
    // {
    //     try
    //     {
    //         cbl_degree.Items.Clear();
    //         string build = "";
    //         if (cbl_batch.Items.Count > 0)
    //         {
    //             for (int i = 0; i < cbl_batch.Items.Count; i++)
    //             {
    //                 if (cbl_batch.Items[i].Selected == true)
    //                 {
    //                     if (build == "")
    //                     {
    //                         build = Convert.ToString(cbl_batch.Items[i].Value);
    //                     }
    //                     else
    //                     {
    //                         build = build + "'" + "," + "'" + Convert.ToString(cbl_batch.Items[i].Value);
    //                     }
    //                 }
    //             }
    //         }
    //         if (build != "")
    //         {
    //             ds = d2.BindDegree(singleuser, group_user, collegecode1, usercode);
    //             int count1 = ds.Tables[0].Rows.Count;
    //             if (count1 > 0)
    //             {
    //                 cbl_degree.DataSource = ds;
    //                 cbl_degree.DataTextField = "course_name";
    //                 cbl_degree.DataValueField = "course_id";
    //                 cbl_degree.DataBind();
    //                 if (cbl_degree.Items.Count > 0)
    //                 {
    //                     for (int row = 0; row < cbl_degree.Items.Count; row++)
    //                     {
    //                         cbl_degree.Items[row].Selected = true;
    //                     }
    //                     cb_degree.Checked = true;
    //                     txt_degree.Text = "Degree(" + cbl_degree.Items.Count + ")";
    //                 }

    //             }
    //         }
    //         else
    //         {
    //             cb_degree.Checked = false;
    //             txt_degree.Text = "--Select--";
    //         }

    //     }
    //     catch (Exception ex)
    //     {
    //     }
    // }

    //public void cb_branch_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int cout = 0;
    //        txt_branch.Text = "--Select--";
    //        if (cb_branch.Checked == true)
    //        {
    //            cout++;
    //            for (int i = 0; i < cbl_branch.Items.Count; i++)
    //            {
    //                cbl_branch.Items[i].Selected = true;
    //            }
    //            txt_branch.Text = "Branch(" + (cbl_branch.Items.Count) + ")";
    //        }
    //        else
    //        {
    //            for (int i = 0; i < cbl_branch.Items.Count; i++)
    //            {
    //                cbl_branch.Items[i].Selected = false;
    //            }
    //            txt_branch.Text = "--Select--";
    //        }


    //       bindsem();
    //       bindsec();

    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    //public void cbl_branch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        cbl_sem.Items.Clear();

    //        int commcount = 0;
    //        cb_branch.Checked = false;
    //        txt_branch.Text = "--Select--";
    //        int commcount1 = 0;

    //        for (int i = 0; i < cbl_branch.Items.Count; i++)
    //        {
    //            if (cbl_branch.Items[i].Selected == true)
    //            {
    //                commcount = commcount + 1;

    //            }
    //        }
    //        if (commcount > 0)
    //        {
    //            if (commcount == cbl_branch.Items.Count)
    //            {

    //                cb_branch.Checked = true;
    //            }
    //            txt_branch.Text = "Branch(" + commcount.ToString() + ")";

    //        }

    //       bindsem();
    //        bindsec();

    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    //public void bindbranch()
    //{
    //    try
    //    {

    //        cbl_branch.Items.Clear();
    //        string course_id = "";
    //        if (cbl_degree.Items.Count > 0)
    //        {
    //            for (int row = 0; row < cbl_degree.Items.Count; row++)
    //            {
    //                if (cbl_degree.Items[row].Selected == true)
    //                {
    //                    if (course_id == "")
    //                    {
    //                        course_id = Convert.ToString(cbl_degree.Items[row].Value);
    //                    }
    //                    else
    //                    {
    //                        course_id = course_id + "," + Convert.ToString(cbl_degree.Items[row].Value);
    //                    }
    //                }
    //            }

    //        }
    //        if (course_id != "")
    //        {
    //            ds = d2.BindBranchMultiple(singleuser, group_user, course_id, collegecode1, usercode);
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                cbl_branch.DataSource = ds;
    //                cbl_branch.DataTextField = "dept_name";
    //                cbl_branch.DataValueField = "degree_code";
    //                cbl_branch.DataBind();
    //                if (cbl_branch.Items.Count > 0)
    //                {
    //                    for (int row = 0; row < cbl_branch.Items.Count; row++)
    //                    {
    //                        cbl_branch.Items[row].Selected = true;
    //                    }
    //                    cb_branch.Checked = true;
    //                    txt_branch.Text = "Branch(" + cbl_branch.Items.Count + ")";
    //                }

    //            }
    //        }
    //        else
    //        {
    //            cb_branch.Checked = false;
    //            txt_branch.Text = "--Select--";
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //    }
    //}

    //public void cb_sem_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int cout = 0;
    //        txt_sem.Text = "--Select--";
    //        if (cb_sem.Checked == true)
    //        {
    //            cout++;
    //            for (int i = 0; i < cbl_sem.Items.Count; i++)
    //            {
    //                cbl_sem.Items[i].Selected = true;
    //            }
    //            txt_sem.Text = "Semester(" + (cbl_sem.Items.Count) + ")";
    //        }
    //        else
    //        {
    //            for (int i = 0; i < cbl_sem.Items.Count; i++)
    //            {
    //                cbl_sem.Items[i].Selected = false;
    //            }
    //        }
    //       bindsec();

    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    //public void cbl_sem_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        cb_sem.Checked = false;
    //        int commcount = 0;
    //        txt_sem.Text = "--Select--";

    //        for (int i = 0; i < cbl_sem.Items.Count; i++)
    //        {
    //            if (cbl_sem.Items[i].Selected == true)
    //            {
    //                commcount = commcount + 1;
    //                cb_sem.Checked = false;

    //            }
    //        }
    //        if (commcount > 0)
    //        {
    //            if (commcount == cbl_sem.Items.Count)
    //            {

    //                cb_sem.Checked = true;
    //            }
    //            txt_sem.Text = "Semester(" + commcount.ToString() + ")";

    //        }

    //        bindsec();

    //    }
    //    catch (Exception ex)
    //    {

    //    }

    //}
    //public void bindsem()
    //{
    //    cbl_sem.Items.Clear();
    //    txt_sem.Text = "--Select--";
    //    Boolean first_year;
    //    first_year = false;
    //    int duration = 0;
    //    int i = 0;
    //    ds.Clear();
    //    string branch = "";
    //    string build = "";
    //    string batch = "";
    //    if (cbl_branch.Items.Count > 0)
    //    {
    //        for (i = 0; i < cbl_branch.Items.Count; i++)
    //        {

    //            if (cbl_branch.Items[i].Selected == true)
    //            {
    //                build = cbl_branch.Items[i].Value.ToString();
    //                if (branch == "")
    //                {
    //                    branch = build;
    //                }
    //                else
    //                {
    //                    branch = branch + "," + build;

    //                }
    //            }
    //        }
    //    }
    //    build = "";
    //    if (cbl_batch.Items.Count > 0)
    //    {
    //        for (i = 0; i < cbl_batch.Items.Count; i++)
    //        {

    //            if (cbl_batch.Items[i].Selected == true)
    //            {
    //                build = cbl_batch.Items[i].Value.ToString();
    //                if (batch == "")
    //                {
    //                    batch = build;
    //                }
    //                else
    //                {
    //                    batch = batch + "," + build;

    //                }

    //            }
    //        }

    //    }
    //    if (branch.Trim() != "" && batch.Trim() != "")
    //    {
    //        ds = d2.BindSem(branch, batch, collegecode1);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
    //            {
    //                string dur = Convert.ToString(ds.Tables[0].Rows[i][0]);
    //                if (dur.Trim() != "")
    //                {
    //                    if (duration < Convert.ToInt32(dur))
    //                    {
    //                        duration = Convert.ToInt32(dur);
    //                    }
    //                }
    //            }
    //        }
    //        if (duration != 0)
    //        {
    //            for (i = 1; i <= duration; i++)
    //            {
    //                cbl_sem.Items.Add(Convert.ToString(i));
    //            }
    //            if (cbl_sem.Items.Count > 0)
    //            {
    //                for (int row = 0; row < cbl_sem.Items.Count; row++)
    //                {
    //                    cbl_sem.Items[row].Selected = true;
    //                    cb_sem.Checked = true;
    //                }
    //                txt_sem.Text = "Sem(" + cbl_sem.Items.Count + ")";
    //            }
    //        }
    //    }



    //}

    public void cb_header_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int cout = 0;
            txt_headersearch.Text = "--Select--";
            if (cb_header.Checked == true)
            {
                cout++;
                for (int i = 0; i < cbl_header.Items.Count; i++)
                {
                    cbl_header.Items[i].Selected = true;
                }
                txt_headersearch.Text = "Header(" + (cbl_header.Items.Count) + ")";
            }
            else
            {
                for (int i = 0; i < cbl_header.Items.Count; i++)
                {
                    cbl_header.Items[i].Selected = false;
                }
                txt_headersearch.Text = "--Select--";
            }

        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }
    public void cbl_header_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            int commcount = 0;
            txt_headersearch.Text = "--Select--";
            cb_header.Checked = false;

            for (int i = 0; i < cbl_header.Items.Count; i++)
            {
                if (cbl_header.Items[i].Selected == true)
                {
                    commcount = commcount + 1;
                    cb_header.Checked = false;
                }
            }
            if (commcount > 0)
            {
                if (commcount == cbl_header.Items.Count)
                {

                    cb_header.Checked = true;
                }
                txt_headersearch.Text = "Header(" + commcount.ToString() + ")";

            }


        }

        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }
    public void bindheader()
    {
        try
        {

            string type = "";
            if (rb_Acad1.Checked == true)
            {
                type = "1";
            }
            else
            {
                type = "2";
            }

            string college_cd = "";
            if (Cbl_college.Items.Count > 0)
            {
                for (int i = 0; i < Cbl_college.Items.Count; i++)
                {
                    if (Cbl_college.Items[i].Selected == true)
                    {
                        if (college_cd == "")
                        {
                            college_cd = "" + Cbl_college.Items[i].Value.ToString() + "";
                        }
                        else
                        {
                            college_cd = college_cd + "','" + Convert.ToString(Cbl_college.Items[i].Value);
                        }
                    }
                }
            }

            string selqry = "  SELECT distinct (select TextVal from TextValTable where TextCode= HeaderCode) as HeaderName,HeaderCode FROM CO_QuestionMaster where CollegeCode in ('" + college_cd + "') and QuestType='" + type + "'";


            ds = d2.select_method_wo_parameter(selqry, "Text");

            cbl_header.Items.Clear();
            txt_headersearch.Text = "---Select---";
            cb_header.Checked = false;

            if (ds.Tables[0].Rows.Count > 0)
            {

                cbl_header.DataSource = ds;
                cbl_header.DataTextField = "HeaderName";
                cbl_header.DataValueField = "HeaderCode";
                cbl_header.DataBind();
                if (cbl_header.Items.Count > 0)
                {
                    for (int row = 0; row < cbl_header.Items.Count; row++)
                    {
                        cbl_header.Items[row].Selected = true;
                        cb_header.Checked = true;
                    }
                    txt_headersearch.Text = "Header(" + cbl_header.Items.Count + ")";
                }

            }

            else
            {
                cb_header.Checked = false;
                txt_headersearch.Text = "--Select--";
            }
        }

        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }

    public void questions()
    {
        try
        {
            DataTable dtl = new DataTable();
            DataRow dtrow = null;
            string tagval = "";
            string itemheadercode = "";
            int itemheader = 0;
            string optdes = string.Empty;
            for (int i = 0; i < cbl_header.Items.Count; i++)
            {
                if (cbl_header.Items[i].Selected == true)
                {
                    if (itemheadercode == "")
                    {
                        itemheadercode = "" + cbl_header.Items[i].Value.ToString() + "";
                        itemheader = 1;
                    }
                    else
                    {
                        itemheadercode = itemheadercode + "','" + cbl_header.Items[i].Value.ToString() + "";
                    }
                }
            }


            string college = "";
            if (Cbl_college.Items.Count > 0)
            {
                for (int i = 0; i < Cbl_college.Items.Count; i++)
                {
                    if (Cbl_college.Items[i].Selected == true)
                    {
                        if (college == "")
                        {
                            college = Convert.ToString(Cbl_college.Items[i].Value);
                        }
                        else
                        {
                            college = college + "','" + Convert.ToString(Cbl_college.Items[i].Value);
                        }
                    }
                }
            }

            string type = "";
            if (rb_Acad1.Checked == true)
            {
                type = "1";
                if (rdb_obj.Checked == true)
                {
                    optdes = "1";
                }
                if (rdb_desc.Checked == true)
                {
                    optdes = "2";
                }
            }
            else if (rb_Gend1.Checked == true)
            {
                type = "2";
                if (rdb_obj.Checked == true)
                {
                    optdes = "1";
                }
                if (rdb_desc.Checked == true)
                {
                    optdes = "2";
                }
            }


            dtrow = dtl.NewRow();
            dtl.Rows.Add(dtrow);



            for (int col = 0; col < 7; col++)
            {
                dtl.Columns.Add("", typeof(string));

            }






            dtl.Rows[0][0] = "S.No";




            dtl.Rows[0][1] = "Header";



            dtl.Rows[0][2] = "Questions";





            dtl.Rows[0][3] = "QuestionRangeName";





            dtl.Rows[0][4] = "QuestionType";



            dtl.Rows[0][5] = "Question Acr";

            dtl.Rows[0][6] = "Tag values";




            ////ds.Clear();

            string selqry = "";

            if (itemheader > 0)
            {
                if (optdes != "2")
                {

                    selqry = " SELECT QuestionMasterPK,CollegeCode, QuestType,objdes,case when QuestType=1 then 'Academic' when QuestType=2 then 'General'end as QuestionType,(select TextVal from TextValTable where TextCode= HeaderCode) as HeaderName,HeaderCode,Question,questionacr,case when QuestRange=1 then 'Easy' when QuestRange=2 then 'Medium' when QuestRange=3 then 'Difficult' when QuestRange=4 then 'Very Difficult' end as QuestRangeName,QuestRange FROM CO_QuestionMaster  where CollegeCode in ('" + college + "') and QuestType='" + type + "' and HeaderCode in('" + itemheadercode + "') and isnull(objdes,'')!='2' ";
                    ds = d2.select_method_wo_parameter(selqry, "Text");
                }
                if (optdes == "2")
                {
                    selqry = " SELECT QuestionMasterPK,CollegeCode, QuestType,objdes,case when QuestType=1 then 'Academic' when QuestType=2 then 'General'end as QuestionType,(select TextVal from TextValTable where TextCode= HeaderCode) as HeaderName,HeaderCode,Question,questionacr,case when QuestRange=1 then 'Easy' when QuestRange=2 then 'Medium' when QuestRange=3 then 'Difficult' when QuestRange=4 then 'Very Difficult' end as QuestRangeName,QuestRange FROM CO_QuestionMaster  where CollegeCode in ('" + college + "') and QuestType='" + type + "' and HeaderCode in('" + itemheadercode + "') and objdes='2' ";
                    ds = d2.select_method_wo_parameter(selqry, "Text");
                }

            }
            else
            {
                if (optdes != "2")
                {
                    selqry = " SELECT QuestionMasterPK,CollegeCode, QuestType,objdes,case when QuestType=1 then 'Academic' when QuestType=2 then 'General'end as QuestionType,(select TextVal from TextValTable where TextCode= HeaderCode) as HeaderName,HeaderCode,Question,questionacr,case when QuestRange=1 then 'Easy' when QuestRange=2 then 'Medium' when QuestRange=3 then 'Difficult' when QuestRange=4 then 'Very Difficult' end as QuestRangeName,QuestRange FROM CO_QuestionMaster  where CollegeCode  in ('" + collegecode1 + "') and QuestType='" + type + "' and isnull(objdes,'')!='2' ";
                }
                if (optdes != "2")
                {
                    selqry = " SELECT QuestionMasterPK,CollegeCode, QuestType,objdes,case when QuestType=1 then 'Academic' when QuestType=2 then 'General'end as QuestionType,(select TextVal from TextValTable where TextCode= HeaderCode) as HeaderName,HeaderCode,Question,questionacr,case when QuestRange=1 then 'Easy' when QuestRange=2 then 'Medium' when QuestRange=3 then 'Difficult' when QuestRange=4 then 'Very Difficult' end as QuestRangeName,QuestRange FROM CO_QuestionMaster  where CollegeCode  in ('" + collegecode1 + "') and QuestType='" + type + "' objdes='2' ";
                }
                ds = d2.select_method_wo_parameter(selqry, "Text");

            }

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataView dv = new DataView();

                    for (int i = 0; i < Cbl_college.Items.Count; i++)
                    {
                        if (Cbl_college.Items[i].Selected == true)
                        {
                            ds.Tables[0].DefaultView.RowFilter = "CollegeCode='" + Cbl_college.Items[i].Value + "'";
                            dv = ds.Tables[0].DefaultView;
                            if (dv.Count > 0)
                            {


                                dtrow = dtl.NewRow();
                                dtl.Rows.Add(dtrow);

                                dtl.Rows[dtl.Rows.Count - 1][0] = Convert.ToString(Cbl_college.Items[i].Text);



                                int sno = 1;

                                for (int ik = 0; ik < dv.Count; ik++)
                                {


                                    dtrow = dtl.NewRow();
                                    dtl.Rows.Add(dtrow);



                                    dtl.Rows[dtl.Rows.Count - 1][0] = Convert.ToString(sno++);
                                    tagval = dv[ik]["QuestionMasterPK"].ToString();


                                    dtl.Rows[dtl.Rows.Count - 1][1] = dv[ik]["HeaderName"].ToString();
                                    tagval += "^" + dv[ik]["HeaderCode"].ToString();



                                    dtl.Rows[dtl.Rows.Count - 1][2] = dv[ik]["Question"].ToString();
                                    tagval += "^" + dv[ik]["QuestRange"].ToString();



                                    dtl.Rows[dtl.Rows.Count - 1][3] = dv[ik]["QuestRangeName"].ToString();
                                    tagval += "^" + dv[ik]["QuestType"].ToString();


                                    dtl.Rows[dtl.Rows.Count - 1][4] = dv[ik]["QuestionType"].ToString();
                                    tagval += "^" + dv[ik]["CollegeCode"].ToString();



                                    dtl.Rows[dtl.Rows.Count - 1][5] = dv[ik]["questionacr"].ToString();
                                    tagval += "^" + dv[ik]["objdes"].ToString();

                                    dtl.Rows[dtl.Rows.Count - 1][6] = tagval;
                                }
                            }
                        }
                    }


                    if (dtl.Rows.Count > 1)
                    {
                        gridview1.DataSource = dtl;
                        gridview1.DataBind();
                        gridview1.Visible = true;




                        for (int i = 0; i < gridview1.Rows.Count; i++)
                        {
                            gridview1.Rows[i].Cells[0].Width = 80;

                            gridview1.Rows[i].Cells[1].Width = 200;

                            gridview1.Rows[i].Cells[2].Width = 400;

                            gridview1.Rows[i].Cells[4].Width = 100;

                            gridview1.Rows[i].Cells[5].Width = 100;



                            for (int j = 0; j < gridview1.HeaderRow.Cells.Count; j++)
                            {

                                if (j == 0 || j == 5)
                                {
                                    gridview1.Rows[i].Cells[j].HorizontalAlign = HorizontalAlign.Center;


                                }
                                else if (j == 6 || j == 3 || j == 4)
                                {
                                    gridview1.Rows[i].Cells[j].Visible = false;
                                }
                                gridview1.Rows[i].Cells[j].Font.Name = "Book Antiqua";
                                gridview1.Rows[i].Cells[j].Font.Size = FontUnit.Medium;

                                if (i == 0)
                                {
                                    gridview1.Rows[i].Cells[j].HorizontalAlign = HorizontalAlign.Center;
                                    gridview1.Rows[i].Cells[j].BackColor = ColorTranslator.FromHtml("#0CA6CA");
                                    gridview1.Rows[i].Cells[j].BorderColor = Color.Black;
                                    gridview1.Rows[i].Cells[j].Font.Bold = true;
                                    gridview1.Rows[i].Cells[j].ForeColor = Color.White;

                                }
                                else
                                {
                                    int colspan = 1;
                                    if (j == 0)
                                    {

                                        while (gridview1.Rows[i].Cells[j].Text != "&nbsp;" && gridview1.Rows[i].Cells[j + colspan].Text == "&nbsp;")
                                        {
                                            colspan++;
                                            if (gridview1.HeaderRow.Cells.Count - 1 == j + colspan)
                                                break;

                                        }
                                    }

                                    if (colspan != 1)
                                    {
                                        gridview1.Rows[i].Cells[j].HorizontalAlign = HorizontalAlign.Center;

                                        gridview1.Rows[i].Cells[j].ColumnSpan = colspan;
                                        for (int a = j + 1; a < j + colspan; a++)
                                            gridview1.Rows[i].Cells[a].Visible = false;
                                    }

                                }
                            }
                        }
                    }


                }
                else
                {
                    imgdiv2.Visible = true;
                    lbl_erroralert.Text = "No Records Found";
                    gridview1.Visible = false;
                    div1.Visible = false;
                    rptprint1.Visible = false;

                }
            }
            else
            {

                imgdiv2.Visible = true;
                lbl_erroralert.Text = "No Records Found";
                gridview1.Visible = false;
                div1.Visible = false;
                rptprint1.Visible = false;

            }

        }

        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
        
        //  FpSpread1.SaveChanges();
    }


    protected void lb3_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx", false);

        }

        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }

    }

  

    protected void gridview1OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex != 0)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            TableCell cell = e.Row.Cells[i];
                            cell.Attributes["onmouseover"] = "this.style.cursor='pointer';";
                            cell.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                            cell.Attributes["onclick"] = string.Format("document.getElementById('{0}').value = {1}; {2}"
                               , SelectedGridCellIndex.ClientID, i
                               , Page.ClientScript.GetPostBackClientHyperlink((GridView)sender, string.Format("Select${0}", e.Row.RowIndex)));
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }
    protected void gridview1_OnSelectedIndexChanged(Object sender, EventArgs e)
    {
        try
        {
            int cnt = gridview1.HeaderRow.Cells.Count;
            var grid = (GridView)sender;
            GridViewRow selectedRow = grid.SelectedRow;
            int rowIndex = grid.SelectedIndex;
            int selectedCellIndex = int.Parse(this.SelectedGridCellIndex.Value);

            gridview1_selectedrow = rowIndex;
            gridview1_selectedcol = selectedCellIndex;

            if (rowIndex != -1 && selectedCellIndex != -1)
            {



                string activerow = rowIndex.ToString();
                string activecol = selectedCellIndex.ToString();


                try
                {




                    string[] tagvalues = gridview1.Rows[Convert.ToInt32(activerow)].Cells[gridview1.HeaderRow.Cells.Count - 1].Text.Split('^');

                    string question = gridview1.Rows[Convert.ToInt32(activerow)].Cells[2].Text.ToString();

                    string quesacr = gridview1.Rows[Convert.ToInt32(activerow)].Cells[5].Text.ToString();

                    //   string rangquestion = FpSpread1.Sheets[0].Cells[Convert.ToInt32(activerow), 1].Text.ToString();
                    // txt_point.Text = rangquestion;

                    if (tagvalues[0] != "&nbsp;")
                    {
                        rb_Acad2.Checked = true;
                        rb_Gend2.Checked = false;
                        addnew.Visible = true;

                        btn_saveqtion.Text = "Update";
                        btndel.Visible = true;
                        Txt_college1.Enabled = false;

                        if (question != "&nbsp;")
                            txt_qstn.Text = question;
                        else
                            txt_qstn.Text = "";
                        if (quesacr != "&nbsp;")
                            txtacr.Text = quesacr;
                        else
                            txtacr.Text = "";
                        string type = tagvalues[3];
                        string objdesc = tagvalues[5];
                        if (type == "1")
                        {
                            rb_Acad2.Checked = true;
                            rb_Gend2.Enabled = false;
                            rdbobjective.Visible = true;
                            rdbdescriptive.Visible = true;

                            //lbl_tough.Visible = true;
                            //rb_easy.Visible = true;
                            //rb_Medium.Visible = true;
                            //rdb_Difficult.Visible = true;
                            //rdb_VeryDifficult.Visible = true;
                            lbloption.Visible = true;
                            cb_option.Visible = true;
                            cbl_option.Visible = true;

                            if (objdesc == "1")
                            {
                                rdbobjective.Checked = true;
                                rdbdescriptive.Checked = false;

                                //lbl_tough.Visible = true;
                                //rb_easy.Visible = true;
                                //rb_Medium.Visible = true;
                                //rdb_Difficult.Visible = true;
                                //rdb_VeryDifficult.Visible = true;
                                lbloption.Visible = true;
                                cb_option.Visible = true;
                                cbl_option.Visible = true;
                            }
                            if (objdesc == "2")
                            {
                                rdbobjective.Checked = false;
                                rdbdescriptive.Checked = true;

                                //lbl_tough.Visible = false;
                                //rb_easy.Visible = false;
                                //rb_Medium.Visible = false;
                                //rdb_Difficult.Visible = false;
                                //rdb_VeryDifficult.Visible = false;
                                lbloption.Visible = false;
                                cb_option.Visible = false;
                                cbl_option.Visible = false;
                            }

                        }
                        else
                        {
                            rb_Gend2.Checked = true;
                            rb_Acad2.Enabled = false;
                            rdbobjective.Visible = true;
                            rdbdescriptive.Visible = true;
                            if (objdesc == "1")
                            {
                                rdbobjective.Checked = true;
                                rdbdescriptive.Checked = false;

                                //lbl_tough.Visible = true;
                                //rb_easy.Visible = true;
                                //rb_Medium.Visible = true;
                                //rdb_Difficult.Visible = true;
                                //rdb_VeryDifficult.Visible = true;
                                lbloption.Visible = true;
                                cb_option.Visible = true;
                                cbl_option.Visible = true;
                            }
                            if (objdesc == "2")
                            {
                                rdbobjective.Checked = false;
                                rdbdescriptive.Checked = true;

                                //lbl_tough.Visible = false;
                                //rb_easy.Visible = false;
                                //rb_Medium.Visible = false;
                                //rdb_Difficult.Visible = false;
                                //rdb_VeryDifficult.Visible = false;
                                lbloption.Visible = false;
                                cb_option.Visible = false;
                                cbl_option.Visible = false;
                            }

                        }

                        string college = tagvalues[4];


                        int con = 0;

                        for (int i = 0; i < Cbl_college1.Items.Count; i++)
                        {
                            Cbl_college1.Items[i].Selected = false;
                        }
                        for (int i = 0; i < Cbl_college1.Items.Count; i++)
                        {
                            if (Cbl_college1.Items[i].Value.ToString() == college)
                            {
                                Cbl_college1.Items[i].Selected = true;
                                Cb_college1.Checked = false;
                                con = con + 1;

                            }
                            Txt_college1.Text = "College(" + con.ToString() + ")";

                        }

                        string value = tagvalues[2];
                        if (value == "1")
                        {
                            //rb_easy.Checked = true;
                        }
                        else if (value == "2")
                        {
                            //rb_Medium.Checked = true;
                        }
                        else if (value == "3")
                        {
                            //rdb_Difficult.Checked = true;
                        }
                        else if (value == "4")
                        {
                            //rdb_VeryDifficult.Checked = true;
                        }

                        string header = tagvalues[1];
                        for (int i = 0; i < ddl_group.Items.Count; i++)
                        {
                            if (ddl_group.Items[i].Value.ToString().Trim() == header.Trim())
                            {
                                ddl_group.SelectedIndex = i;
                            }
                        }
                    }
                }



                catch
                {

                }

            }
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }

    }


    protected void btnAdd1_Click(object sender, EventArgs e)
    {
        //BindType();
        try
        {
            cbl_option.ClearSelection();//delsi2301
            btn_saveqtion.Text = "Save";
            Txt_college1.Enabled = true;
            rb_Acad2.Enabled = true;
            rb_Gend2.Enabled = true;
            txt_qstn.Text = "";
            txtacr.Text = "";
            addnew.Visible = true;
            btndel.Visible = false;
            Printcontrol1.Visible = false;
            bindclg1();
            bindheader();
            rb_Acad2.Checked = true;
            rb_Gend2.Checked = false;
            //rb_easy.Checked = true;
            //rb_Medium.Checked = false;
            //rdb_Difficult.Checked = false;
            //rdb_VeryDifficult.Checked = false;
            rdbobjective.Visible = true;
            rdbdescriptive.Visible = true;

            //lbl_tough.Visible = true;
            //rb_easy.Visible = true;
            //rb_Medium.Visible = true;
            //rdb_Difficult.Visible = true;
            //rdb_VeryDifficult.Visible = true;
            lbloption.Visible = true;
            cb_option.Visible = true;
            cbl_option.Visible = true;


        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }

    }
    protected void btnSave2_Click(object sender, EventArgs e)
    {

    }

    protected void imagebtnpopclose1_Click(object sender, EventArgs e)
    {
        try
        {
            addnew.Visible = false;
            rb_Acad1.Checked = true;
            rb_Gend1.Checked = false;
            rdb_obj.Visible = true;
            rdb_desc.Visible = true;//delsiref
            div1.Visible = false;
            gridview1.Visible = false;
            bindheader();
            rptprint1.Visible = false;
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }

    }
    protected void rb_easy_CheckedChanged(object sender, EventArgs e)
    {
    }
    protected void rb_Medium_CheckedChanged(object sender, EventArgs e)
    {
    }
    protected void rdb_Difficult_CheckedChanged(object sender, EventArgs e)
    {
    }
    protected void rdb_VeryDifficult_CheckedChanged(object sender, EventArgs e)
    {
    }
    protected void btn_savequstion_Click(object sender, EventArgs e)
    {
        try
        {

            string Question = txt_qstn.Text;
            string Questionacr = txtacr.Text;
            string Header = ddl_group.SelectedItem.Value;
            Question = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Question);
            Questionacr = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Questionacr);
            string type = "";
            string objectivedesc = string.Empty;
            //if (rb_Gend2.Checked == true)//delsi1311
            //{
                if (rdbobjective.Checked == true)
                {
                    objectivedesc = "1";
                }
                else if (rdbdescriptive.Checked == true)
                {
                    objectivedesc = "2";
                }
            //}
            if (rb_Acad2.Checked == true)
            {
                type = "1";
            }
            else if (rb_Gend2.Checked == true)
            {
                type = "2";
            }
            //string QuestnRange = "";
            //if (rb_easy.Checked == true)
            //{
            //    QuestnRange = "1";
            //}
            //else if (rb_Medium.Checked == true)
            //{
            //    QuestnRange = "2";
            //}
            //else if (rdb_Difficult.Checked == true)
            //{
            //    QuestnRange = "3";
            //}
            //else if (rdb_VeryDifficult.Checked == true)
            //{
            //    QuestnRange = "4";
            //}
            if (btn_saveqtion.Text.Trim().ToUpper() == "SAVE")
            {


                if (txt_qstn.Text != "")
                {
                    if (Txt_college1.Text != "--Select--")
                    {
                        if (Header != "0")
                        {

                            for (int a = 0; a < Cbl_college1.Items.Count; a++)
                            {
                                if (Cbl_college1.Items[a].Selected == true)
                                {
                                    string insert = string.Empty;
                                    //if (rb_Acad2.Checked == true)
                                    //{
                                    //     insert = "insert into CO_QuestionMaster (HeaderCode, QuestType,Question,CollegeCode,questionacr) values ('" + Header + "','" + type + "','" + Question + "','" + Cbl_college1.Items[a].Value + "','" + Questionacr + "')";
                                    //}
                                    //if (rb_Gend2.Checked == true)
                                    //{
                                        if (rdbobjective.Checked == true)
                                        {
                                            insert = "insert into CO_QuestionMaster (HeaderCode, QuestType,Question,CollegeCode,questionacr,objdes) values ('" + Header + "','" + type + "','" + Question + "','" + Cbl_college1.Items[a].Value + "','" + Questionacr + "','" + objectivedesc + "')";
                                        }
                                        if (rdbdescriptive.Checked == true)
                                        {
                                            insert = "insert into CO_QuestionMaster (HeaderCode, QuestType,Question,CollegeCode,questionacr,objdes) values ('" + Header + "','" + type + "','" + Question + "','" + Cbl_college1.Items[a].Value + "','" + Questionacr + "','" + objectivedesc + "')";
                                        }
                                    //}

                                    int insertvalue2 = d2.update_method_wo_parameter(insert, "Text");
                                    if (insertvalue2 != 0)
                                    {
                                        if (objectivedesc !="2")
                                        {
                                            for (int k = 0; k < cbl_option.Items.Count; k++)//delsi2301
                                            {
                                                if (cbl_option.Items[k].Selected == true)
                                                {
                                                    string Questionpk = d2.GetFunction("select QuestionMasterPK from CO_QuestionMaster where HeaderCode='" + Header + "'and QuestType='" + type + "'and Question='" + Question + "' and CollegeCode='" + Cbl_college1.Items[a].Value + "'");
                                                    if (Questionpk != "0")
                                                    {
                                                        string q1 = " if not exists( select QuestionmasterFK from Co_Question_Type where QuestionmasterFK='" + Questionpk + "' and MarkMasterFK='" + cbl_option.Items[k].Value + "' and HeaderCode='" + Header + "')insert into Co_Question_Type (QuestionmasterFK, MarkMasterFK,HeaderCode)values ('" + Questionpk + "','" + cbl_option.Items[k].Value + "','" + Header + "')";
                                                        insertvalue2 = d2.update_method_wo_parameter(q1, "text");
                                                    }

                                                }
                                            }
                                        }

                                    }


                                }
                            }
                            imgdiv2.Visible = true;
                            lbl_erroralert.Visible = true;

                            lbl_erroralert.Text = "Saved Successfully";
                            bindheader();
                            txt_qstn.Text = "";
                            txtacr.Text = "";

                            cbl_option.ClearSelection();
                            bindclg1();
                            //BindType();
                            loaddesc();
                            //rb_Acad2.Checked = true;
                            //rb_Gend2.Checked = false;
                            //rb_easy.Checked = true;
                            //rb_Medium.Checked = false;
                            //rdb_Difficult.Checked = false;
                            //rdb_VeryDifficult.Checked = false;
                            // addnew.Visible = false;
                            btn_search_Click(sender, e);

                        }
                        else
                        {
                            imgdiv2.Visible = true;
                            lbl_erroralert.Visible = true;

                            lbl_erroralert.Text = "please select Header";
                            addnew.Visible = true;

                        }

                    }
                    else
                    {
                        imgdiv2.Visible = true;
                        lbl_erroralert.Visible = true;

                        lbl_erroralert.Text = "please select college";
                        addnew.Visible = true;

                    }

                }
                //else
                //{
                //    imgdiv2.Visible = true;
                //    lbl_erroralert.Visible = true;
                //    lbl_erroralert.Text = "Pleace Enter Question";
                //}

            }
            else if (btn_saveqtion.Text.Trim().ToUpper() == "UPDATE")
            {
                string college_cd = "";
                if (Cbl_college1.Items.Count > 0)
                {

                    for (int a = 0; a < Cbl_college1.Items.Count; a++)
                    {
                        if (Cbl_college1.Items[a].Selected == true)
                        {
                            if (college_cd == "")
                            {
                                college_cd = Convert.ToString(Cbl_college1.Items[a].Value);
                            }
                            else
                            {
                                college_cd = college_cd + "','" + Convert.ToString(Cbl_college1.Items[a].Value);
                            }

                        }
                    }
                }
                string uptype = "";

                if (rb_Acad1.Checked == true)
                {
                    uptype = "1";

                }
                else if (rb_Gend1.Checked == true)
                {
                    uptype = "2";
                }

                string activerow = gridview1_selectedrow.ToString();
                string activecol = gridview1_selectedcol.ToString();

                
                

                 string[] tagvalues = gridview1.Rows[Convert.ToInt32(activerow)].Cells[gridview1.HeaderRow.Cells.Count - 1].Text.Split('^');

                 int value = Convert.ToInt32(tagvalues[0]);
                

                //update CO_QuestionMaster set HeaderCode='Header',Question='Question',QuestRange='QuestnRange' where QuestionMasterPK=''"+value+"'' and collegecode='" + collegecode1 + "'
                if (Header != "0")
                {
                }
                else
                {
                    imgdiv2.Visible = true;
                    lbl_erroralert.Visible = true;

                    lbl_erroralert.Text = "please select Header";
                    addnew.Visible = true;
                    return;


                }

                string updqry = "update CO_QuestionMaster set HeaderCode='" + Header + "',QuestType='" + uptype + "',Question='" + Question + "', questionacr='" + Questionacr + "',objdes='" + objectivedesc + "' where QuestionMasterPK='" + value + "' and collegecode in('" + college_cd + "')";
                int qtn = d2.update_method_wo_parameter(updqry, "Text");
            }

            if (btn_saveqtion.Text.ToUpper().Trim() == "UPDATE")
            {
                lbl_erroralert.Text = "Updated successfully";
                imgdiv2.Visible = true;
                addnew.Visible = false;
                bindheader();
                questions();

            }


            // loaddesc();  




        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }


    }

    protected void btn_exit_Click(object sender, EventArgs e)
    {
        try
        {
            addnew.Visible = false;
            questions();
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }

    protected void btnExcel1_Click(object sender, EventArgs e)
    {
        try
        {
            string reportname = txtexcelname1.Text;
            if (reportname.ToString().Trim() != "")
            {
                if (gridview1.Visible == true)
                {
                    
                    d2.printexcelreportgrid(gridview1, reportname);

                }
                lbl_norec1.Visible = false;
            }
            else
            {
                lbl_norec1.Text = "Please Enter Your Report Name";
                lbl_norec1.Visible = true;
                txtexcelname1.Focus();
            }
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }
    protected void btnprintmaster1_Click(object sender, EventArgs e)
    {
        try
        {
            string dptname = "Question Master Report";
            string pagename = "Question_Master.aspx";

            if (gridview1.Visible == true)
            {
                
                string ss = null;
                string degreedetails = "";
                Printcontrol1.loadspreaddetails(gridview1, pagename, degreedetails, 0, ss);
                Printcontrol1.Visible = true;
            }
            
            Printcontrol1.Visible = true;
            lbl_norec1.Visible = false;
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }

    [System.Web.Services.WebMethod]
    public static string CheckUserName(string StoreName)
    {
        string returnValue = "1";
        try
        {
            DAccess2 dd = new DAccess2();
            string user_name = StoreName;
            if (user_name.Trim() != "" && user_name != null)
            {
                string query = dd.GetFunction("select distinct Question,QuestionMasterPK from CO_QuestionMaster  where CollegeCode in ('" + clgcode1 + "') and Question ='" + user_name + "'");


                if (query.Trim() == "" || query == null || query == "0" || query == "-1")
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


    [System.Web.Services.WebMethod]
    public static string CheckUserName1(string StoreName)
    {
        string returnValue = "1";
        try
        {
            DAccess2 dd = new DAccess2();
            string user_name = StoreName;
            if (user_name.Trim() != "" && user_name != null)
            {
                string query = dd.GetFunction("select distinct questionacr,QuestionMasterPK from CO_QuestionMaster  where CollegeCode in ('" + clgcode1 + "') and questionacr ='" + user_name + "'");


                if (query.Trim() == "" || query == null || query == "0" || query == "-1")
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


    protected void cb_option_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //rs.CallCheckBoxChangedEvent(cbl_option, cb_option, txt_option, "Option");
            int count = 0;
            if (cb_option.Checked)
            {
                for (int i = 0; i < cbl_option.Items.Count; i++)
                {
                    cbl_option.Items[i].Selected = true;
                    if (cbl_option.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < cbl_option.Items.Count; i++)
                {
                    cbl_option.Items[i].Selected = false;
                }
            }
            if (count == cbl_option.Items.Count)
            {
                cb_option.Checked = true;
            }
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }
    public void BindType()
    {
        try
        {
           
            cbl_option.Items.Clear();
            cb_option.Checked = false;
            if (Cbl_college.Items.Count > 0)
            {
                
                string selqry = " SELECT  distinct (MarkType),MarkMasterPK FROM CO_MarkMaster WHERE  CollegeCode in (" + Convert.ToString(Cbl_college.SelectedItem.Value) + ")";
                
                ds = d2.select_method_wo_parameter(selqry, "Text");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbl_option.DataSource = ds;
                    cbl_option.DataTextField = "MarkType";
                    cbl_option.DataValueField = "MarkMasterPK";
                    cbl_option.DataBind();
                }
                if (cbl_option.Items.Count > 0)
                {
                    for (int row = 0; row < cbl_option.Items.Count; row++)
                    {
                        cbl_option.Items[row].Selected = true;
                        cb_option.Checked = true;
                    }
                    
                }
                else
                {
                   
                }
            }
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }
    protected void rdbobjective_checkedChange(object sender, EventArgs e)
    {
        try
        {
            if (rdbobjective.Checked == true)
            {
                rdbdescriptive.Checked = false;

                //lbl_tough.Visible = true;
                //rb_easy.Visible = true;
                //rb_Medium.Visible = true;
                //rdb_Difficult.Visible = true;
                //rdb_VeryDifficult.Visible = true;
                lbloption.Visible = true;
                cb_option.Visible = true;
                cbl_option.Visible = true;
            }
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }
    protected void rdbdescriptive_checkedChange(object sender, EventArgs e)
    {
        try
        {
            if (rdbdescriptive.Checked == true)
            {
                rdbobjective.Checked = false;
                //lbl_tough.Visible = false;
                //rb_easy.Visible = false;
                //rb_Medium.Visible = false;
                //rdb_Difficult.Visible = false;
                //rdb_VeryDifficult.Visible = false;
                lbloption.Visible = false;
                cb_option.Visible = false;
                cbl_option.Visible = false;
            }
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }
    protected void rdb_obj_checkedChange(object sender, EventArgs e)
    {
        try
        {
            if (rdb_obj.Checked == true)
            {
                rdb_desc.Checked = false;
            }
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }
    protected void rdb_desc_checkedChange(object sender, EventArgs e)
    {
        try
        {

            if (rdb_desc.Checked == true)
            {
                rdb_obj.Checked = false;
            }
        }
        catch (Exception ex)
        {
            d2.sendErrorMail(ex, collegecode1, "Question_Master");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    { }
}