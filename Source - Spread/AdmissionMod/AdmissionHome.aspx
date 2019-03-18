﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AttendanceMOD/AttendanceSubSiteMaster.master" AutoEventWireup="true"
    CodeFile="AdmissionHome.aspx.cs" Inherits="AdmissionHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <style type="text/css">
        
        .grid-view
        {
            padding: 0;
            margin: 0;
            border: 1px solid #333;
            font-family: "Verdana, Arial, Helvetica, sans-serif, Trebuchet MS";
            font-size: 0.9em;
        }
        
        .grid-view tr.header
        {
            color: white;
            background-color: #0CA6CA;
            height: 30px;
            vertical-align: middle;
            text-align: center;
            font-weight: bold;
            font-size: 20px;
        }
        
        .grid-view tr.normal
        {
            color: black;
            background-color: #FDC64E;
            height: 25px;
            vertical-align: middle;
            text-align: center;
        }
        
        .grid-view tr.alternate
        {
            color: black;
            background-color: #D59200;
            height: 25px;
            vertical-align: middle;
            text-align: center;
        }
        
        .grid-view tr.normal:hover, .grid-view tr.alternate:hover
        {
            background-color: white;
            color: black;
            font-weight: bold;
        }
        
        .grid_view_lnk_button
        {
            color: Black;
            text-decoration: none;
            font-size: large;
        }
        .lbl
        {
            font-family: Book Antiqua;
            font-size: 30px;
            font-weight: bold;
            color: Green;
            text-align: center;
            font-style: italic;
        }
        .hdtxt
        {
            font-family: Book Antiqua;
            font-size: large;
            font-weight: bold;
        }
        .FixedHeader
        {
            position: absolute;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <div style="width: 700px;">
            <table>
                <tr>
                    <td>
                    </td>
                    <td>
                        <center>
                            <asp:Label ID="Label1" CssClass="lbl" runat="server" Text="Admission"></asp:Label>
                        </center>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:GridView ID="gridMenu" BackColor="WhiteSmoke" runat="server" CssClass="grid-view" AutoGenerateColumns="False"
                            GridLines="Both" OnRowDataBound="gridMenu_OnRowDataBound"
                            OnDataBound="gridMenu_OnDataBound" Width="700px">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <center>
                                            <asp:Label ID="lblSno" runat="server"  CssClass="grid_view_lnk_button" Text='<%#Container.DisplayIndex+1 %>'></asp:Label>
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Header Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHdrName" runat="server" CssClass="grid_view_lnk_button" Text='<%#Eval("HeaderName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                    <center>
                                        <asp:Label ID="lblReportId" runat="server"  CssClass="grid_view_lnk_button" Text='<%#Eval("ReportId") %>'></asp:Label>
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Menu">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbPagelink" runat="server" CssClass="grid_view_lnk_button"  Text='<%#Eval("ReportName") %>' PostBackUrl='<%#Eval("PageName") %>'
                                            Style="text-decoration: none;"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Help" Visible="false">
                                    <ItemTemplate>
                                        <center>
                                            <%--<asp:LinkButton ID="lbHelplink" runat="server" Text="Help" PostBackUrl='<%#Eval("HelpURL") %>'
                                                Style="text-decoration: none;"></asp:LinkButton>--%>
                                                <a id="lbHelplink" runat="server" target="_blank" href='<%#Eval("HelpURL") %>'>Help</a>
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </center>
</asp:Content>
