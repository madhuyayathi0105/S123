﻿<%@ Page Title="" Language="C#" MasterPageFile="~/FinanceMod/FinanceSubSiteMaster.master"
    AutoEventWireup="true" CodeFile="PartPayStudsettings.aspx.cs" Inherits="FinanceMod_PartPayStudsettings" %>

<%@ Register Src="~/Usercontrols/PrintMaster.ascx" TagName="printmaster" TagPrefix="Insproplus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
        var checkedId = false;
        function OnGridHeaderSelected() {
            var id = document.getElementById("<%=gridStudList.ClientID %>");
            var len = id.rows.length;
            var i = 0;
            var checkedId = id.rows[0].getElementsByTagName("input")[0].checked;
            for (var ak = 1; ak < len; ak++) {
                if (id.rows[ak].getElementsByTagName("input")[i].type == "checkbox") {
                    if (checkedId == true) {
                        id.rows[ak].getElementsByTagName("input")[i].checked = true;
                    } else {
                        id.rows[ak].getElementsByTagName("input")[i].checked = false;
                    }
                }
            }
        }
    </script>
    <asp:ScriptManager ID="scrptMgr" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <div>
                <span class="fontstyleheader" style="color: Green">Part Payment Student Settings</span>
            </div>
        </center>
    </div>
    <center>
        <div class="maindivstyle" style="width: 970px; height: 500px;">
            <center>
                <table class="maintablestyle">
                    <tr>
                        <td>
                            <asp:Label ID="lblCollege" runat="server" Text="College"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_college" Width="200px" Height="30px" runat="server" CssClass="textbox ddlheight"
                                AutoPostBack="true" OnSelectedIndexChanged="ddl_college_OnIndexChange">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lbl_stream" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_strm" Width="80px" Height="30px" runat="server" CssClass="textbox ddlheight"
                                AutoPostBack="true" OnSelectedIndexChanged="ddl_strm_OnIndexChange">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lbl_batch" runat="server" Text="Batch"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_batch" Width="80px" Height="30px" runat="server" CssClass="textbox ddlheight"
                                AutoPostBack="true" OnSelectedIndexChanged="ddl_batch_OnIndexChange">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lbl_degree" runat="server" Text="Degree"></asp:Label>
                        </td>
                        <td>
                            <%-- <asp:DropDownList ID="ddl_degree" Width="100px" Height="30px" runat="server" CssClass="textbox ddlheight"
                                AutoPostBack="true" OnSelectedIndexChanged="ddl_degree_OnIndexChange">
                            </asp:DropDownList>--%>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_degree" runat="server" ReadOnly="true" Height="20px" CssClass="textbox txtheight">Degree</asp:TextBox>
                                    <asp:Panel ID="pdegree" runat="server" Width="150px" Height="170px" CssClass="multxtpanel">
                                        <asp:CheckBox ID="cb_degree" runat="server" OnCheckedChanged="cb_degree_ChekedChange"
                                            Text="Select All" AutoPostBack="True" />
                                        <asp:CheckBoxList ID="cbl_degree" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbl_degree_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                    <asp:PopupControlExtender ID="PopupControlExtender8" runat="server" TargetControlID="txt_degree"
                                        PopupControlID="pdegree" Position="Bottom">
                                    </asp:PopupControlExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:Label ID="lbl_branch" runat="server" Text="Branch"></asp:Label>
                        </td>
                        <td>
                            <%--  <asp:DropDownList ID="ddl_branch" Width="200px" Height="30px" runat="server" CssClass="textbox ddlheight"
                                AutoPostBack="true" OnSelectedIndexChanged="ddl_branch_OnIndexChange">
                            </asp:DropDownList>--%>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_branch" runat="server" ReadOnly="true" Height="20px" CssClass="textbox textbox1 txtheight">Branch</asp:TextBox>
                                    <asp:Panel ID="pbranch" runat="server" Width="250px" Height="200px" CssClass="multxtpanel">
                                        <asp:CheckBox ID="cb_branch" runat="server" OnCheckedChanged="cb_branch_ChekedChange"
                                            Font-Names="Book Antiqua" Font-Size="Medium" Text="Select All" AutoPostBack="True" />
                                        <asp:CheckBoxList ID="cbl_branch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbl_branch_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                    <asp:PopupControlExtender ID="PopupControlExtender9" runat="server" TargetControlID="txt_branch"
                                        PopupControlID="pbranch" Position="Bottom">
                                    </asp:PopupControlExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_Sem" runat="server" Text="Semester"></asp:Label>
                        </td>
                        <td id="single" runat="server" visible="false">
                            <%--  <asp:DropDownList ID="ddl_sem" Width="100px" Height="30px" runat="server" CssClass="textbox ddlheight"
                                AutoPostBack="true" OnSelectedIndexChanged="ddl_sem_OnIndexChange">
                            </asp:DropDownList>--%>
                            <asp:UpdatePanel ID="UPpanel_sem" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_sem" runat="server" CssClass="textbox txtheight2" Width="100px"
                                        ReadOnly="true" placeholder="Semester" onfocus="return myFunction1(this)"></asp:TextBox>
                                    <asp:Panel ID="panel_sem" runat="server" CssClass="multxtpanel">
                                        <asp:CheckBox ID="cb_sem" runat="server" Width="100px" Text="Select All" AutoPostBack="True"
                                            OnCheckedChanged="cb_sem_CheckedChanged" />
                                        <asp:CheckBoxList ID="cbl_sem" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbl_sem_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                    <asp:PopupControlExtender ID="popupce_sem" runat="server" TargetControlID="txt_sem"
                                        PopupControlID="panel_sem" Position="Bottom">
                                    </asp:PopupControlExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td id="multiple" runat="server" visible="false">
                            <asp:UpdatePanel ID="Updp_sem" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_seml" runat="server" Style="height: 20px; width: 124px;" ReadOnly="true">--Select--</asp:TextBox>
                                    <asp:Panel ID="panel_sem1" runat="server" CssClass="multxtpanel" Style="width: 124px;
                                        height: 172px;">
                                        <asp:CheckBox ID="cb_seml" runat="server" Width="124px" Text="Select All" AutoPostBack="True"
                                            OnCheckedChanged="cb_sem_OnCheckedChanged" />
                                        <asp:CheckBoxList ID="cbl_seml" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbl_sem_OnSelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                    <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txt_seml"
                                        PopupControlID="panel_sem1" Position="Bottom">
                                    </asp:PopupControlExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:Label ID="lbl_sec" runat="server" Text="Section"></asp:Label>
                        </td>
                        <td>
                            <%--  <asp:DropDownList ID="ddl_sec" Width="80px" Height="30px" runat="server" CssClass="textbox ddlheight"
                                AutoPostBack="true" OnSelectedIndexChanged="ddl_sec_OnIndexChange">
                            </asp:DropDownList>--%>
                            <asp:UpdatePanel ID="UpdatePanel8sec" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_sec" runat="server" ReadOnly="true" Height="20px" CssClass="textbox textbox1 txtheight"
                                        Width="70px">Section</asp:TextBox>
                                    <asp:Panel ID="pnlsec" runat="server" Width="120px" Height="80px" CssClass="multxtpanel">
                                        <asp:CheckBox ID="cb_sec" runat="server" OnCheckedChanged="cb_sec_ChekedChange" Font-Names="Book Antiqua"
                                            Font-Size="Medium" Text="Select All" AutoPostBack="True" />
                                        <asp:CheckBoxList ID="cbl_sec" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbl_sec_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                    <asp:PopupControlExtender ID="PopupControlExtender5" runat="server" TargetControlID="txt_sec"
                                        PopupControlID="pnlsec" Position="Bottom">
                                    </asp:PopupControlExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Type"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlType" Width="80px" Height="30px" runat="server" CssClass="textbox ddlheight"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlType_OnIndexChange">
                                <asp:ListItem Text="Part Payment"></asp:ListItem>
                                <asp:ListItem Text="Hold"></asp:ListItem>
                            </asp:DropDownList>
                            <%--AutoPostBack="true" OnSelectedIndexChanged="ddl_strm_OnIndexChange"--%>
                        </td>
                        <td colspan="2" id="tdAmt" runat="server" visible="false">
                            <asp:Label ID="Label2" runat="server" Text="Amount"></asp:Label>
                            <%-- </td>
                        <td>--%>
                            <asp:TextBox ID="txtPayment" runat="server" Height="20px" CssClass="textbox textbox1 txtheight"
                                Width="70px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btn_go" Text="Go" OnClick="btn_go_Click" CssClass="textbox btn1 textbox1"
                                runat="server" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="textbox btn1 textbox1"
                                Width="60px" BackColor="#4CB267" runat="server" Visible="false" />
                        </td>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_ledger" runat="server" Text="Ledger" Style="width: 50px;"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_studled" runat="server" Style="height: 20px; width: 100px;"
                                            ReadOnly="true">--Select--</asp:TextBox>
                                        <asp:Panel ID="pnl_studled" runat="server" CssClass="multxtpanel multxtpanleheight"
                                            Style="width: 300px; height: 180px;">
                                            <asp:CheckBox ID="chk_studled" runat="server" Width="100px" Text="Select All" AutoPostBack="True"
                                                OnCheckedChanged="chk_studled_OnCheckedChanged" />
                                            <asp:CheckBoxList ID="chkl_studled" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkl_studled_OnSelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                        <asp:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="txt_studled"
                                            PopupControlID="pnl_studled" Position="Bottom">
                                        </asp:PopupControlExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:Button ID="ledgersave" Text="Save" OnClick="btnSaveledgerhold_Click" CssClass="textbox btn1 textbox1"
                                    Width="60px" BackColor="#4CB267" runat="server" Visible="false" />
                                    <asp:Button ID="Cancelhold" Text="Cancel" OnClick="btnCancelLedgerHold_Click" CssClass="textbox btn1 textbox1"
                                    Width="60px" BackColor="#4CB267" runat="server" Visible="false" />
                            </td>
                        </tr>
                    </tr>
                </table>
                <br />
                <div>
                    <asp:Label ID="lbl_errormsg" Visible="false" runat="server" Font-Bold="true" Text=""
                        ForeColor="Red"></asp:Label>
                </div>
                <div id="divspread" runat="server" visible="false" style="width: 1000px; height: auto">
                    <FarPoint:FpSpread ID="FpSpread1" runat="server" Height="250px" Width="400px" EnableClientScript="true"
                        ActiveSheetViewIndex="0" DesignString="&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;&lt;Spread /&gt;"
                        CssClass="cursorptr" BorderColor="Black" BorderWidth="0.5" OnUpdateCommand="FpSpread1_OnUpdateCommand"
                        VerticalScrollBarPolicy="Never" HorizontalScrollBarPolicy="Never" Style="margin: 0px;
                        margin-top: 15px; margin-bottom: 15px; position: relative;">
                        <Sheets>
                            <FarPoint:SheetView SheetName="Sheet1">
                            </FarPoint:SheetView>
                        </Sheets>
                    </FarPoint:FpSpread>
                </div>
                <div id="grid" runat="server" visible="false" style="height: 320px; overflow: auto;">
                    <%--OnRowDataBound="grid_Details_OnRowDataBound" OnDataBound="grid_Details_DataBound"--%>
                    <asp:GridView ID="gridStudList" runat="server" AutoGenerateColumns="false" GridLines="Both"
                        OnRowDataBound="gridStudList_OnRowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="#0CA6CA" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_serial" runat="server" Text='<%#Container.DisplayIndex+1 %>'></asp:Label>
                                    <asp:Label ID="lbl_appNo" runat="server" Text='<%#Eval("app_no") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select" HeaderStyle-BackColor="#0CA6CA" HeaderStyle-HorizontalAlign="center">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cb_selectHead" runat="server" onchange="return OnGridHeaderSelected()">
                                    </asp:CheckBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb_selectgrid" runat="server" Checked='<%# Eval("IsFinPartPay") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Roll No" HeaderStyle-BackColor="#0CA6CA" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <center>
                                        <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Eval("Roll_No") %>' Width="80px"></asp:Label>
                                    </center>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reg No" HeaderStyle-BackColor="#0CA6CA" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <center>
                                        <asp:Label ID="lbl_reg_no" runat="server" Text='<%#Eval("Reg_No") %>'></asp:Label>
                                    </center>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Admission No" HeaderStyle-BackColor="#0CA6CA" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <center>
                                        <asp:Label ID="lbl_roll_admit" runat="server" Text='<%#Eval("roll_admit") %>'></asp:Label>
                                    </center>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name" HeaderStyle-BackColor="#0CA6CA" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="300px">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_stud_name" runat="server" Text='<%#Eval("Stud_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department" HeaderStyle-BackColor="#0CA6CA" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="300px">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_DegCode" runat="server" Text='<%#Eval("Degree_Code") %>'></asp:Label>
                                    <asp:Label ID="lbl_dept_name" runat="server" Text='<%#Eval("Department") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section" HeaderStyle-BackColor="#0CA6CA" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="80px">
                                <ItemTemplate>
                                    <center>
                                        <asp:Label ID="lbl_sec" runat="server" Text='<%#Eval("Sections") %>'></asp:Label>
                                    </center>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" HeaderStyle-BackColor="#0CA6CA" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="80px">
                                <ItemTemplate>
                                    <center>
                                        <asp:TextBox ID="txtAmt" runat="server" Width="60px" Text='<%#Eval("isPartAmount") %>'></asp:TextBox><%--Text='<%#Eval("Sections") %>'--%>
                                    </center>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </center>
        </div>
    </center>
</asp:Content>
