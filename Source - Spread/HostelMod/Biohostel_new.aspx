﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Hostelmod/hostelsite.master" AutoEventWireup="true"
    CodeFile="Biohostel_new.aspx.cs" Inherits="Biohostel_new" %>

<%@ Register Assembly="FarPoint.Web.Spread" Namespace="FarPoint.Web.Spread" TagPrefix="FarPoint" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Usercontrols/PrintMaster.ascx" TagName="PRINTPDF" TagPrefix="Insproplus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
    <html>
    <style type="text/css">
        .style7
        {
            width: 23px;
            height: 28px;
        }
        .style11
        {
            height: 28px;
            width: 205px;
        }
        .style14
        {
            height: 28px;
        }
        .style15
        {
            width: 47px;
            height: 10px;
        }
        .style23
        {
            width: 11px;
        }
        .style27
        {
            width: 15px;
        }
        .cpHeader
        {
            color: white;
            background-color: #719DDB;
            font-size: 12px;
            cursor: pointer;
            padding: 4px;
            font-style: normal;
            font-variant: normal;
            font-weight: bold;
            line-height: normal;
            font-family: "auto Trebuchet MS" , Verdana;
        }
        .cpBody
        {
            background-color: #DCE4F9;
            font: normal 11px auto Verdana, Arial;
            border: 1px gray;
            padding-top: 7px;
            padding-left: 4px;
            padding-right: 4px;
            padding-bottom: 4px;
            width: 1000px;
        }
        
        .cpimage
        {
            float: right;
            vertical-align: middle;
            background-color: transparent;
        }
        .style33
        {
            height: 10px;
            width: 15px;
        }
        .style34
        {
            height: 10px;
            width: 97px;
        }
        .style35
        {
            height: 10px;
        }
        .style37
        {
            width: 108px;
        }
        .style38
        {
            width: 3px;
        }
        .style39
        {
            height: 10px;
            width: 8px;
        }
        .style40
        {
            font-family: Arial;
        }
    </style>
    <body>
        <div style="height: 183px">
            <asp:Panel ID="Panel3" runat="server" Height="121px" BackImageUrl="~/bioimage/Biomatric_New.jpg">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </asp:Panel>
            <br />
            <table style="margin-left: 0px; height: 27px; width: 381px; margin-bottom: 3px;">
                <tr>
                    <td class="style27">
                    </td>
                    <td class="style37">
                        <asp:Image ID="Image9" runat="server" ImageUrl="~/bioimage/Date.jpg" />
                    </td>
                    <td class="style38">
                        <asp:TextBox ID="Txtentryfrom" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Width="75px" CssClass="style40" Height="24px" Font-Size="Medium"></asp:TextBox>
                        <asp:CalendarExtender ID="Txtentryfrom_CalendarExtender" runat="server" TargetControlID="Txtentryfrom"
                            Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="To:" Font-Names="Book Antiqua"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txtentryto" runat="server" OnTextChanged="Txtentryto_TextChanged"
                            Font-Bold="True" Font-Names="Book Antiqua" Width="76px" CssClass="style40" Height="25px"
                            Font-Size="Medium"></asp:TextBox>
                        <asp:CalendarExtender ID="Txtentryto_CalendarExtender" runat="server" TargetControlID="Txtentryto"
                            Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                        <asp:Label ID="lbldate" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
                        </asp:ToolkitScriptManager>
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoinandout" runat="server" Width="16px" Checked="True" GroupName="s"
                            AutoPostBack="true" OnCheckedChanged="rdchecked" />
                    </td>
                    <td>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/bioimage/In out.gif" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoinonly" runat="server" Width="16px" GroupName="s" AutoPostBack="true"
                            OnCheckedChanged="rdchecked" />
                    </td>
                    <td>
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/bioimage/In Only.jpg" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdooutonly" runat="server" GroupName="s" Width="16px" AutoPostBack="true"
                            OnCheckedChanged="rdchecked" />
                    </td>
                    <td>
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/bioimage/Out Only.jpg" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdounreg" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" GroupName="s" Text="Unregister Students" Width="172px" AutoPostBack="true"
                            OnCheckedChanged="rdchecked" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoboth" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" GroupName="s" Text="Both" Width="71px" AutoPostBack="true"
                            OnCheckedChanged="rdchecked" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rbdailylog" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" GroupName="s" Text="DailyLog" Style="margin-left: -17px;"
                            Width="94px" AutoPostBack="true" OnCheckedChanged="rdchecked" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlstud" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium">
                            <asp:ListItem Text="Students"></asp:ListItem>
                            <asp:ListItem Text="Guest"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:CheckBox ID="Chktimein" runat="server" Font-Bold="True" OnCheckedChanged="Chktimein_CheckedChanged"
                            Height="24px" AutoPostBack="true" Width="19px" />
                    </td>
                    <td class="style34">
                        <asp:Image ID="Image5" runat="server" ImageUrl="~/bioimage/In Time.jpg" />
                    </td>
                    <td class="style15">
                        <asp:DropDownList ID="cbo_hrtin" runat="server" Width="40px" Font-Bold="False" Height="20px"
                            CssClass="style40" Font-Names="Arial" Enabled="False">
                            <asp:ListItem>Hours</asp:ListItem>
                            <asp:ListItem> 1</asp:ListItem>
                            <asp:ListItem> 2</asp:ListItem>
                            <asp:ListItem> 3</asp:ListItem>
                            <asp:ListItem> 4</asp:ListItem>
                            <asp:ListItem> 5</asp:ListItem>
                            <asp:ListItem> 6</asp:ListItem>
                            <asp:ListItem> 7</asp:ListItem>
                            <asp:ListItem> 8</asp:ListItem>
                            <asp:ListItem> 9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style35">
                        <asp:DropDownList ID="cbo_mintimein" runat="server" Width="40px" Font-Bold="False"
                            Font-Names="Arial" Height="20px" CssClass="style40" Enabled="False">
                            <asp:ListItem>Min</asp:ListItem>
                            <asp:ListItem>00</asp:ListItem>
                            <asp:ListItem>01</asp:ListItem>
                            <asp:ListItem>02</asp:ListItem>
                            <asp:ListItem>03</asp:ListItem>
                            <asp:ListItem>04</asp:ListItem>
                            <asp:ListItem>05</asp:ListItem>
                            <asp:ListItem>06</asp:ListItem>
                            <asp:ListItem>07</asp:ListItem>
                            <asp:ListItem>08</asp:ListItem>
                            <asp:ListItem>09</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>17</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                            <asp:ListItem>19</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>21</asp:ListItem>
                            <asp:ListItem>22</asp:ListItem>
                            <asp:ListItem>23</asp:ListItem>
                            <asp:ListItem>24</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>26</asp:ListItem>
                            <asp:ListItem>27</asp:ListItem>
                            <asp:ListItem>28</asp:ListItem>
                            <asp:ListItem>29</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>31</asp:ListItem>
                            <asp:ListItem>32</asp:ListItem>
                            <asp:ListItem>33</asp:ListItem>
                            <asp:ListItem>34</asp:ListItem>
                            <asp:ListItem>35</asp:ListItem>
                            <asp:ListItem>36</asp:ListItem>
                            <asp:ListItem>37</asp:ListItem>
                            <asp:ListItem>38</asp:ListItem>
                            <asp:ListItem>39</asp:ListItem>
                            <asp:ListItem>40</asp:ListItem>
                            <asp:ListItem>41</asp:ListItem>
                            <asp:ListItem>42</asp:ListItem>
                            <asp:ListItem>43</asp:ListItem>
                            <asp:ListItem>44</asp:ListItem>
                            <asp:ListItem>45</asp:ListItem>
                            <asp:ListItem>46</asp:ListItem>
                            <asp:ListItem>47</asp:ListItem>
                            <asp:ListItem>48</asp:ListItem>
                            <asp:ListItem>49</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>51</asp:ListItem>
                            <asp:ListItem>52</asp:ListItem>
                            <asp:ListItem>53</asp:ListItem>
                            <asp:ListItem>54</asp:ListItem>
                            <asp:ListItem>55</asp:ListItem>
                            <asp:ListItem>56</asp:ListItem>
                            <asp:ListItem>57</asp:ListItem>
                            <asp:ListItem>58</asp:ListItem>
                            <asp:ListItem>59</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style23">
                        <asp:DropDownList ID="cbo_in" runat="server" Width="40px" Font-Names="Arial" Font-Bold="False"
                            Height="20px" CssClass="style40" Enabled="False">
                            <asp:ListItem>AM</asp:ListItem>
                            <asp:ListItem>PM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblto" runat="server" Font-Bold="True" Text="To" Font-Names="Book Antiqua"></asp:Label>
                    </td>
                    <td class="style35">
                        <asp:DropDownList ID="cbo_hrinto" runat="server" Style="margin-top: 0px" Width="40px"
                            Font-Bold="False" Font-Names="Arial" Height="20px" CssClass="style40" Enabled="False">
                            <asp:ListItem>Hours:</asp:ListItem>
                            <asp:ListItem> 1</asp:ListItem>
                            <asp:ListItem> 2</asp:ListItem>
                            <asp:ListItem> 3</asp:ListItem>
                            <asp:ListItem> 4</asp:ListItem>
                            <asp:ListItem> 5</asp:ListItem>
                            <asp:ListItem> 6</asp:ListItem>
                            <asp:ListItem> 7</asp:ListItem>
                            <asp:ListItem> 8</asp:ListItem>
                            <asp:ListItem> 9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style35">
                        <asp:DropDownList ID="cbo_mininto" runat="server" Width="40px" Font-Names="Arial"
                            Font-Bold="False" OnSelectedIndexChanged="cbo_mininto_SelectedIndexChanged" Height="20px"
                            CssClass="style40" Enabled="False">
                            <asp:ListItem>Min</asp:ListItem>
                            <asp:ListItem>00</asp:ListItem>
                            <asp:ListItem>01</asp:ListItem>
                            <asp:ListItem>02</asp:ListItem>
                            <asp:ListItem>03</asp:ListItem>
                            <asp:ListItem>04</asp:ListItem>
                            <asp:ListItem>05</asp:ListItem>
                            <asp:ListItem>06</asp:ListItem>
                            <asp:ListItem>07</asp:ListItem>
                            <asp:ListItem>08</asp:ListItem>
                            <asp:ListItem>09</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>17</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                            <asp:ListItem>19</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>21</asp:ListItem>
                            <asp:ListItem>22</asp:ListItem>
                            <asp:ListItem>23</asp:ListItem>
                            <asp:ListItem>24</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>26</asp:ListItem>
                            <asp:ListItem>27</asp:ListItem>
                            <asp:ListItem>28</asp:ListItem>
                            <asp:ListItem>29</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>31</asp:ListItem>
                            <asp:ListItem>32</asp:ListItem>
                            <asp:ListItem>33</asp:ListItem>
                            <asp:ListItem>34</asp:ListItem>
                            <asp:ListItem>35</asp:ListItem>
                            <asp:ListItem>36</asp:ListItem>
                            <asp:ListItem>37</asp:ListItem>
                            <asp:ListItem>38</asp:ListItem>
                            <asp:ListItem>39</asp:ListItem>
                            <asp:ListItem>40</asp:ListItem>
                            <asp:ListItem>41</asp:ListItem>
                            <asp:ListItem>42</asp:ListItem>
                            <asp:ListItem>43</asp:ListItem>
                            <asp:ListItem>44</asp:ListItem>
                            <asp:ListItem>45</asp:ListItem>
                            <asp:ListItem>46</asp:ListItem>
                            <asp:ListItem>47</asp:ListItem>
                            <asp:ListItem>48</asp:ListItem>
                            <asp:ListItem>49</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>51</asp:ListItem>
                            <asp:ListItem>52</asp:ListItem>
                            <asp:ListItem>53</asp:ListItem>
                            <asp:ListItem>54</asp:ListItem>
                            <asp:ListItem>55</asp:ListItem>
                            <asp:ListItem>56</asp:ListItem>
                            <asp:ListItem>57</asp:ListItem>
                            <asp:ListItem>58</asp:ListItem>
                            <asp:ListItem>59</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style35">
                        <asp:DropDownList ID="cbointo" runat="server" Width="40px" Font-Names="Arial" Font-Bold="False"
                            Height="20px" CssClass="style40" Enabled="False">
                            <asp:ListItem>AM</asp:ListItem>
                            <asp:ListItem>PM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style35">
                        <asp:CheckBox ID="Chktimeout" runat="server" Font-Bold="True" OnCheckedChanged="Chktimeout_CheckedChanged"
                            AutoPostBack="true" Width="16px" />
                    </td>
                    <td class="style33">
                        <asp:Image ID="Image6" runat="server" ImageUrl="~/bioimage/Out Time.jpg" />
                    </td>
                    <td class="style35">
                        <asp:DropDownList ID="cbo_hours" runat="server" Height="20px" Width="40px" Font-Bold="False"
                            Font-Names="Arial" Enabled="False">
                            <asp:ListItem>Hours:</asp:ListItem>
                            <asp:ListItem> 1</asp:ListItem>
                            <asp:ListItem> 2</asp:ListItem>
                            <asp:ListItem> 3</asp:ListItem>
                            <asp:ListItem> 4</asp:ListItem>
                            <asp:ListItem> 5</asp:ListItem>
                            <asp:ListItem> 6</asp:ListItem>
                            <asp:ListItem> 7</asp:ListItem>
                            <asp:ListItem> 8</asp:ListItem>
                            <asp:ListItem> 9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style35">
                        <asp:DropDownList ID="cbo_min" runat="server" Width="40px" Font-Bold="False" Font-Names="Arial"
                            Height="20px" CssClass="style40" Enabled="False">
                            <asp:ListItem>Min</asp:ListItem>
                            <asp:ListItem>00</asp:ListItem>
                            <asp:ListItem>01</asp:ListItem>
                            <asp:ListItem>02</asp:ListItem>
                            <asp:ListItem>03</asp:ListItem>
                            <asp:ListItem>04</asp:ListItem>
                            <asp:ListItem>05</asp:ListItem>
                            <asp:ListItem>06</asp:ListItem>
                            <asp:ListItem>07</asp:ListItem>
                            <asp:ListItem>08</asp:ListItem>
                            <asp:ListItem>09</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>17</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                            <asp:ListItem>19</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>21</asp:ListItem>
                            <asp:ListItem>22</asp:ListItem>
                            <asp:ListItem>23</asp:ListItem>
                            <asp:ListItem>24</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>26</asp:ListItem>
                            <asp:ListItem>27</asp:ListItem>
                            <asp:ListItem>28</asp:ListItem>
                            <asp:ListItem>29</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>31</asp:ListItem>
                            <asp:ListItem>32</asp:ListItem>
                            <asp:ListItem>33</asp:ListItem>
                            <asp:ListItem>34</asp:ListItem>
                            <asp:ListItem>35</asp:ListItem>
                            <asp:ListItem>36</asp:ListItem>
                            <asp:ListItem>37</asp:ListItem>
                            <asp:ListItem>38</asp:ListItem>
                            <asp:ListItem>39</asp:ListItem>
                            <asp:ListItem>40</asp:ListItem>
                            <asp:ListItem>41</asp:ListItem>
                            <asp:ListItem>42</asp:ListItem>
                            <asp:ListItem>43</asp:ListItem>
                            <asp:ListItem>44</asp:ListItem>
                            <asp:ListItem>45</asp:ListItem>
                            <asp:ListItem>46</asp:ListItem>
                            <asp:ListItem>47</asp:ListItem>
                            <asp:ListItem>48</asp:ListItem>
                            <asp:ListItem>49</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>51</asp:ListItem>
                            <asp:ListItem>52</asp:ListItem>
                            <asp:ListItem>53</asp:ListItem>
                            <asp:ListItem>54</asp:ListItem>
                            <asp:ListItem>55</asp:ListItem>
                            <asp:ListItem>56</asp:ListItem>
                            <asp:ListItem>57</asp:ListItem>
                            <asp:ListItem>58</asp:ListItem>
                            <asp:ListItem>59</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style39">
                        <asp:DropDownList ID="cbo_sec" runat="server" Height="20px" Width="40px" Font-Bold="False"
                            Font-Names="Arial" Style="font-family: Arial" Enabled="False">
                            <asp:ListItem>AM</asp:ListItem>
                            <asp:ListItem>PM</asp:ListItem>
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lbltoutto" runat="server" Font-Bold="True" Text="To" Font-Names="Book Antiqua"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cbo_hour2" runat="server" Width="40px" Font-Bold="False" Font-Names="Arial"
                            Height="20px" Enabled="False">
                            <asp:ListItem>Hours:</asp:ListItem>
                            <asp:ListItem> 1</asp:ListItem>
                            <asp:ListItem> 2</asp:ListItem>
                            <asp:ListItem> 3</asp:ListItem>
                            <asp:ListItem> 4</asp:ListItem>
                            <asp:ListItem> 5</asp:ListItem>
                            <asp:ListItem> 6</asp:ListItem>
                            <asp:ListItem> 7</asp:ListItem>
                            <asp:ListItem> 8</asp:ListItem>
                            <asp:ListItem> 9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style35">
                        <asp:DropDownList ID="cbo_min2" runat="server" Width="40px" Font-Bold="False" Font-Names="Arial"
                            Height="20px" Enabled="False">
                            <asp:ListItem>Min</asp:ListItem>
                            <asp:ListItem>00</asp:ListItem>
                            <asp:ListItem>01</asp:ListItem>
                            <asp:ListItem>02</asp:ListItem>
                            <asp:ListItem>03</asp:ListItem>
                            <asp:ListItem>04</asp:ListItem>
                            <asp:ListItem>05</asp:ListItem>
                            <asp:ListItem>06</asp:ListItem>
                            <asp:ListItem>07</asp:ListItem>
                            <asp:ListItem>08</asp:ListItem>
                            <asp:ListItem>09</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>17</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                            <asp:ListItem>19</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>21</asp:ListItem>
                            <asp:ListItem>22</asp:ListItem>
                            <asp:ListItem>23</asp:ListItem>
                            <asp:ListItem>24</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>26</asp:ListItem>
                            <asp:ListItem>27</asp:ListItem>
                            <asp:ListItem>28</asp:ListItem>
                            <asp:ListItem>29</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>31</asp:ListItem>
                            <asp:ListItem>32</asp:ListItem>
                            <asp:ListItem>33</asp:ListItem>
                            <asp:ListItem>34</asp:ListItem>
                            <asp:ListItem>35</asp:ListItem>
                            <asp:ListItem>36</asp:ListItem>
                            <asp:ListItem>37</asp:ListItem>
                            <asp:ListItem>38</asp:ListItem>
                            <asp:ListItem>39</asp:ListItem>
                            <asp:ListItem>40</asp:ListItem>
                            <asp:ListItem>41</asp:ListItem>
                            <asp:ListItem>42</asp:ListItem>
                            <asp:ListItem>43</asp:ListItem>
                            <asp:ListItem>44</asp:ListItem>
                            <asp:ListItem>45</asp:ListItem>
                            <asp:ListItem>46</asp:ListItem>
                            <asp:ListItem>47</asp:ListItem>
                            <asp:ListItem>48</asp:ListItem>
                            <asp:ListItem>49</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>51</asp:ListItem>
                            <asp:ListItem>52</asp:ListItem>
                            <asp:ListItem>53</asp:ListItem>
                            <asp:ListItem>54</asp:ListItem>
                            <asp:ListItem>55</asp:ListItem>
                            <asp:ListItem>56</asp:ListItem>
                            <asp:ListItem>57</asp:ListItem>
                            <asp:ListItem>58</asp:ListItem>
                            <asp:ListItem>59</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style35">
                        <asp:DropDownList ID="cbo_sec2" runat="server" Height="20px" Width="40px" Font-Bold="False"
                            Font-Names="Arial" Enabled="False">
                            <asp:ListItem>AM</asp:ListItem>
                            <asp:ListItem>PM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style35">
                        <asp:ImageButton ID="btnsearch" runat="server" BorderWidth="1px" Height="33px" ImageUrl="~/bioimage/Search Button.jpg"
                            OnClick="btnsearch_Click" Width="91px" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server" BackImageUrl="~/JPG_Biometric/Band.jpg" Height="10px"
                Style="margin-top: 0px" Width="1000px">
            </asp:Panel>
            <table>
                <tr>
                    <td class="style14">
                        <asp:Label ID="lbl_collegename" Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Medium"
                            Text="College" runat="server" CssClass="txtheight"></asp:Label>
                    </td>
                    <td class="style14">
                        <%--  <asp:DropDownList ID="ddl_college" Visible="false" runat="server" CssClass="textbox  ddlheight4"
                            OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>--%>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_college" runat="server" CssClass="textbox txtheight5 textbox1"
                                    ReadOnly="true" onfocus="return myFunction1(this)" Font-Bold="True" Font-Names="Book Antiqua"
                                    Font-Size="Medium">--Select--</asp:TextBox>
                                <asp:Panel ID="Panel5" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                    CssClass="multxtpanel" Style="width: 260px; height: 200px;">
                                    <asp:CheckBox ID="cb_clg" runat="server" Text="Select All" AutoPostBack="True" OnCheckedChanged="cb_clg_checkedchange"
                                        Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Medium" />
                                    <asp:CheckBoxList ID="cbl_clg" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbl_clg_SelectedIndexChanged"
                                        Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Medium">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                                <asp:PopupControlExtender ID="PopupControlExtender15" runat="server" TargetControlID="txt_college"
                                    PopupControlID="Panel5" Position="Bottom">
                                </asp:PopupControlExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="style14">
                        <asp:Image ID="Image7" runat="server" ImageUrl="~/bioimage/Year.jpg" />
                           <asp:Label ID="Lblbatch" runat="server" Text="Batch" Font-Bold="true" Visible="false"
                        Font-Names="Book Antiqua" Font-Size="Medium"></asp:Label>
                    </td>
                    <td class="style14">
                        <%--<asp:DropDownList ID="ddlBatch" runat="server" Height="25px" Width="130px" Font-Bold="True"
                            Font-Names="Book Antiqua" AutoPostBack="true" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"
                            Font-Size="Medium">
                        </asp:DropDownList>--%>
                        <asp:UpdatePanel ID="Upp4" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txt_batchyr" runat="server" CssClass="textbox  textbox1 txtheight3"
                                Width="70px" ReadOnly="true">--Select--</asp:TextBox>
                            <asp:Panel ID="p3" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="2px" CssClass="multxtpanel" Width="84px" Height="180px" Style="position: absolute;">
                                <asp:CheckBox ID="cb_batchyear" runat="server" Text="Select All" AutoPostBack="true"
                                    OnCheckedChanged="cb_batchyear_checkedchange" />
                                <asp:CheckBoxList ID="cbl_batchyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbl_batchyear_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <asp:PopupControlExtender ID="PopupControlExtender3" runat="server" TargetControlID="txt_batchyr"
                                PopupControlID="p3" Position="Bottom">
                            </asp:PopupControlExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    </td>
                    <td class="style7">
                        <asp:Image ID="Image8" runat="server" ImageUrl="~/bioimage/Degree.jpg" />
                          <asp:Label ID="Lbldegree" runat="server" Text="Degree" Font-Bold="True" Visible="false"
                        Font-Names="Book Antiqua" Font-Size="Medium"></asp:Label>
                      
                    </td>
                    <td class="style7">
                        <%--<asp:DropDownList ID="ddlDegree" runat="server" AutoPostBack="True" Height="25px"
                            Width="130px" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged1" Font-Bold="True"
                            Font-Names="Book Antiqua" Font-Size="Medium">
                        </asp:DropDownList>--%>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txt_degree" runat="server" CssClass="textbox  textbox1 txtheight3"
                                ReadOnly="true">--Select--</asp:TextBox>
                            <asp:Panel ID="pnldegree" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="2px" CssClass="multxtpanel" Width="150px" Height="180px" Style="position: absolute;">
                                <asp:CheckBox ID="cb_degree" runat="server" Text="Select All" AutoPostBack="true"
                                    OnCheckedChanged="cb_degree_checkedchange" />
                                <asp:CheckBoxList ID="cbl_degree" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbl_degree_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txt_degree"
                                PopupControlID="pnldegree" Position="Bottom">
                            </asp:PopupControlExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                        
                    </td>
                    <td class="style14">
                        <asp:Image ID="Image10" runat="server" ImageUrl="~/bioimage/Branch.jpg" />
                         <asp:Label ID="LblBranch" runat="server" Text="Branch" Font-Bold="True" Visible="false"
                        Font-Names="Book Antiqua" Font-Size="Medium"></asp:Label>
                    </td>
                    <td class="style7">
                        <%--<asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="True" Height="27px"
                            Width="130px" Style="margin-left: 0px" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged1"
                            Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Medium">
                        </asp:DropDownList>--%>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtbranch" runat="server" CssClass="textbox  textbox1 txtheight3"
                                ReadOnly="true">--Select--</asp:TextBox>
                            <asp:Panel ID="pnlbranch" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="2px" CssClass="multxtpanel" Width="150px" Height="180px" Style="position: absolute;">
                                <asp:CheckBox ID="cb_branch" runat="server" Text="Select All" AutoPostBack="true"
                                    OnCheckedChanged="cb_branch_checkedchange" />
                                <asp:CheckBoxList ID="cbl_branch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbl_branch_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <asp:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="txtbranch"
                                PopupControlID="pnlbranch" Position="Bottom">
                            </asp:PopupControlExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="style14">
                        <asp:Image ID="Image11" runat="server" ImageUrl="~/bioimage/Hostel Name.jpg" />
                          <asp:Label ID="lblHostelname" runat="server" Text="Hostel Name" Font-Bold="True" Visible="false"
                        Font-Names="Book Antiqua" Font-Size="Medium"></asp:Label>
                    </td>
                    <td class="style7">
                        <%--<asp:DropDownList ID="Cbo_HostelName" runat="server" AutoPostBack="True" Font-Bold="True"
                            OnSelectedIndexChanged="Cbo_HostelName_SelectedIndexChanged" Font-Names="Book Antiqua"
                            Font-Size="Medium" Width="130px">
                        </asp:DropDownList>--%>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txthostelname" runat="server" CssClass="textbox  textbox1 txtheight3"
                                ReadOnly="true">--Select--</asp:TextBox>
                            <asp:Panel ID="pnlhostelname" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="2px" CssClass="multxtpanel" Width="150px" Height="180px" Style="position: absolute;">
                                <asp:CheckBox ID="cb_hostelname" runat="server" Text="Select All" AutoPostBack="true"
                                    OnCheckedChanged="cb_hostelname_checkedchange" />
                                <asp:CheckBoxList ID="cbl_hostelname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbl_hostelname_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <asp:PopupControlExtender ID="PopupControlExtender4" runat="server" TargetControlID="txthostelname"
                                PopupControlID="pnlhostelname" Position="Bottom">
                            </asp:PopupControlExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="Floor Name" Width="100px"></asp:Label>

                    </td>
                    <td>
                      <%--  <asp:DropDownList ID="cbofloorname" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Width="130px" AutoPostBack="True" OnSelectedIndexChanged="cbofloorname_SelectedIndexChanged">
                        </asp:DropDownList>--%>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtfloorname" runat="server" CssClass="textbox  textbox1 txtheight3"
                                ReadOnly="true">--Select--</asp:TextBox>
                            <asp:Panel ID="pnlfloorName" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="2px" CssClass="multxtpanel" Width="150px" Height="180px" Style="position: absolute;">
                                <asp:CheckBox ID="cb_floorName" runat="server" Text="Select All" AutoPostBack="true"
                                    OnCheckedChanged="cb_floorName_checkedchange" />
                                <asp:CheckBoxList ID="cbl_floorName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbl_floorName_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <asp:PopupControlExtender ID="PopupControlExtender5" runat="server" TargetControlID="txtfloorname"
                                PopupControlID="pnlfloorName" Position="Bottom">
                            </asp:PopupControlExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:Image ID="Image12" runat="server" ImageUrl="~/bioimage/Room No1.gif" />
                          <asp:Label ID="lbl_romm" runat="server" Font-Bold="True" Visible="false" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="Room No" Width="100px"></asp:Label>
                    </td>
                    <td>
                        <%--<asp:DropDownList ID="Cbo_Room" runat="server" AutoPostBack="True" Font-Bold="True"
                            OnSelectedIndexChanged="Cbo_Room_SelectedIndexChanged" Width="130px" Font-Names="Book Antiqua"
                            Font-Size="Medium">
                        </asp:DropDownList>--%>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtroom_no" runat="server" CssClass="textbox  textbox1 txtheight3"
                                ReadOnly="true">--Select--</asp:TextBox>
                            <asp:Panel ID="pnlroomno" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="2px" CssClass="multxtpanel" Width="150px" Height="180px" Style="position: absolute;">
                                <asp:CheckBox ID="cb_roomNo" runat="server" Text="Select All" AutoPostBack="true"
                                    OnCheckedChanged="cb_roomNo_checkedchange" />
                                <asp:CheckBoxList ID="cbl_room_no" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbl_room_no_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <asp:PopupControlExtender ID="PopupControlExtender6" runat="server" TargetControlID="txtroom_no"
                                PopupControlID="pnlroomno" Position="Bottom">
                            </asp:PopupControlExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:Image ID="Image13" runat="server" ImageUrl="~/bioimage/Roll No1.gif" />
                        <asp:Label ID="lbl_RollNo" runat="server" Font-Bold="True" Visible="false" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="Roll No" Width="100px"></asp:Label>
                    </td>
                    <td>
                     <%--   <asp:DropDownList ID="cboroll" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Width="130px">
                        </asp:DropDownList>--%>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtrollnum" runat="server" CssClass="textbox  textbox1 txtheight3"
                                ReadOnly="true">--Select--</asp:TextBox>
                            <asp:Panel ID="pnlroll_num" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="2px" CssClass="multxtpanel" Width="150px" Height="180px" Style="position: absolute;">
                                <asp:CheckBox ID="cb_roolnum" runat="server" Text="Select All" AutoPostBack="true"
                                    OnCheckedChanged="cb_roolnum_checkedchange" />
                                <asp:CheckBoxList ID="cbl_rollnum" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbl_rollnum_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <asp:PopupControlExtender ID="PopupControlExtender7" runat="server" TargetControlID="txtrollnum"
                                PopupControlID="pnlroll_num" Position="Bottom">
                            </asp:PopupControlExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Image ID="Image14" runat="server" ImageUrl="~/bioimage/Student Name.jpg" />
                        <asp:Label ID="lbl_studname" runat="server" Font-Bold="True" Visible="false" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="Student Name" Width="100px"></asp:Label>
                    </td>
                    <td>
                       <%-- <asp:DropDownList ID="cbostudentname" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Width="130px">
                        </asp:DropDownList>--%>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtstudename" runat="server" CssClass="textbox  textbox1 txtheight3"
                                ReadOnly="true">--Select--</asp:TextBox>
                            <asp:Panel ID="pnl_studeName" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="2px" CssClass="multxtpanel" Width="150px" Height="180px" Style="position: absolute;">
                                <asp:CheckBox ID="cb_studeName" runat="server" Text="Select All" AutoPostBack="true"
                                    OnCheckedChanged="cb_studeName_checkedchange" />
                                <asp:CheckBoxList ID="cbl_studeName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbl_studeName_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <asp:PopupControlExtender ID="PopupControlExtender8" runat="server" TargetControlID="txtstudename"
                                PopupControlID="pnl_studeName" Position="Bottom">
                            </asp:PopupControlExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                    <td colspan="3">
                        <asp:RadioButton ID="rdb_deptname" runat="server" Text="DepartmentName" GroupName="d"
                            Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Medium" />
                        <asp:RadioButton ID="rdb_deptacr" runat="server" Text="DepartmentAcronym" GroupName="d"
                            Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Medium" />
                    </td>
                    <td class="style14">
                        <asp:Image ID="Image21" runat="server" ImageUrl="~/bioimage/Attendance.jpg" />
                    </td>
                    <td class="style7">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Height="20px" CssClass="Dropdown_Txt_Box"
                                    ReadOnly="true" Width="100px" Style="top: 369px; left: 134px; height: 20px; width: 121px;
                                    font-family: 'Book Antiqua'" Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Medium">---Select---</asp:TextBox>
                                <asp:Panel ID="pnlCustomers" runat="server" CssClass="multxtpanel" Height="86px"
                                    Width="130px">
                                    <asp:CheckBox ID="SelectAll" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                                        Font-Size="Medium" OnCheckedChanged="SelectAll_CheckedChanged" Text="Select All"
                                        AutoPostBack="True" />
                                    <asp:CheckBoxList ID="cbo_att" runat="server" Font-Names="Book Antiqua" Font-Size="Medium"
                                        AutoPostBack="True" OnSelectedIndexChanged="cbo_att_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Selected="false">P</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="false">A</asp:ListItem>
                                    </asp:CheckBoxList>
                                </asp:Panel>
                                <asp:PopupControlExtender ID="pceSelections" runat="server" TargetControlID="TextBox1"
                                    PopupControlID="pnlCustomers" Position="Bottom">
                                </asp:PopupControlExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:Panel ID="attfiltertype" BorderColor="#993333" BorderWidth="1px" runat="server"
                            BorderStyle="Solid" Style="height: 24px; width: 260px; border-radius: 10px;"
                            Visible="true">
                            <asp:RadioButton ID="rdb_morn" Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Medium"
                                runat="server" Text="Morning" GroupName="bb" Enabled="false" />
                            <asp:RadioButton ID="rdb_even" Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Medium"
                                runat="server" Text="Evening" GroupName="bb" Enabled="false" />
                            <asp:RadioButton ID="rdoboth1" Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Medium"
                                runat="server" Text="Both" GroupName="bb" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="style14">
                        <asp:Label ID="lblSec" runat="server" Text="Section" Font-Bold="True" Visible="False"></asp:Label>
                    </td>
                    <td class="style14">
                        <asp:DropDownList ID="ddlSec" runat="server" AutoPostBack="true" Font-Bold="True"
                            Font-Names="Book Antiqua" Height="25px" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td class="style11">
                        <asp:Label ID="lblDuration" runat="server" Text="Duration" Font-Bold="True" Visible="False"></asp:Label>
                    </td>
                    <td class="style14">
                        <asp:DropDownList ID="ddlSemYr" runat="server" AutoPostBack="True" Height="25px"
                            Width="74px" Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Medium" Visible="False">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel2" runat="server" BackImageUrl="~/JPG_Biometric/Band.jpg" Height="10px"
                Width="1000px">
            </asp:Panel>
            <div>
                <asp:Panel ID="pheaderfilter" runat="server" CssClass="cpHeader" Height="14px">
                    <%--&nbsp;Filter your Search here&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                    <asp:Label ID="Labelfilter" Text="Column Order" runat="server" Font-Size="Medium"
                        Font-Bold="True" Font-Names="Book Antiqua" />
                    <asp:Image ID="Imagefilter" runat="server" CssClass="cpimage" ImageUrl="right.jpeg" />
                </asp:Panel>
            </div>
            <div>
                <asp:Panel ID="pbodyfilter" runat="server" CssClass="cpBody">
                    <table>
                        <tr>
                            <asp:TextBox ID="tborder" Visible="false" Width="1000" TextMode="MultiLine" CssClass="style1"
                                AutoPostBack="true" runat="server">
                            </asp:TextBox>
                            <td>
                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                                    Text="Select Column" Width="112px" Font-Size="Medium"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="cblsearch" runat="server" AutoPostBack="true" Height="20px"
                                    RepeatColumns="5" RepeatDirection="Horizontal" Style="font-family: 'Book Antiqua';
                                    font-weight: 700; font-size: medium;" Width="874px" OnSelectedIndexChanged="cblcolumnorder_SelectedIndexChanged">
                                    <asp:ListItem Value="Roll No">Roll No</asp:ListItem>
                                    <asp:ListItem Value="Student Name">Student Name</asp:ListItem>
                                    <asp:ListItem Value="Department">Department</asp:ListItem>
                                    <asp:ListItem Value="Hostel Name">Hostel Name</asp:ListItem>
                                    <asp:ListItem Value="Room No">Room No</asp:ListItem>
                                    <asp:ListItem Value="Father Name">Father Name</asp:ListItem>
                                    <asp:ListItem Value="Father MobileNo">Father MobileNo</asp:ListItem>
                                    <asp:ListItem Value="Mother Name">Mother Name</asp:ListItem>
                                    <asp:ListItem Value="Mother MobileNo">Mother MobileNo</asp:ListItem>
                                    <asp:ListItem Value="Student MobileNo">Student MobileNo</asp:ListItem>
                                    <asp:ListItem Value="In Time">In Time</asp:ListItem>
                                    <asp:ListItem Value="Out Time">Out Time</asp:ListItem>
                                    <asp:ListItem Value="Attendance">Attendance</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <%--Width="963px"--%>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:CollapsiblePanelExtender ID="cpefilter" runat="server" TargetControlID="pbodyfilter"
                    CollapseControlID="pheaderfilter" ExpandControlID="pheaderfilter" Collapsed="true"
                    TextLabelID="Labelfilter" CollapsedSize="0" ImageControlID="Imagefilter" CollapsedImage="right.jpeg"
                    ExpandedImage="down.jpeg">
                </asp:CollapsiblePanelExtender>
            </div>
            <asp:Panel ID="Panel4" runat="server" Height="16px" Width="1001px">
                <asp:Image ID="Image4" runat="server" Height="8px" ImageUrl="~/JPG_Biometric/Band.jpg"
                    Width="1000px" />
            </asp:Panel>
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgpresent" runat="server" ImageUrl="~/bioimage/Present.jpg"
                            Visible="False" OnClick="imgpresent_Click" />
                        <asp:Label ID="lbl_headermorn" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="Morning" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblpresent1" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" ForeColor="Green" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_headereven" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="Evening" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblpresent2" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" ForeColor="Green" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgabsent" runat="server" ImageUrl="~/bioimage/Absent.jpg" Visible="False"
                            OnClick="imgabsent_Click" />
                    </td>
                    <td>
                        <asp:Label ID="lblheaderabsent1" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="Morning" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblabsent1" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" ForeColor="Red" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblheaderabsent2" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="Evening" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblabsent2" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" ForeColor="Red" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:ImageButton ID="imglate" runat="server" ImageUrl="~/bioimage/Late.jpg" OnClick="imglate_Click"
                            Visible="False" />
                        <asp:Label ID="lbllatetext" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="OD" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblmornlate" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="Morning" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbllate" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" ForeColor="DarkRed" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblevenlate" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="Evening" Visible="false"></asp:Label>
                        <asp:Label ID="lbllate1" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" ForeColor="DarkRed" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgper" runat="server" ImageUrl="~/bioimage/Permission.jpg"
                            Visible="False" OnClick="imgpermission_Click" />
                        <asp:Label ID="lblmornper" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="Morning" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblpermission" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblevenper" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Text="Evening" Visible="false"></asp:Label>
                        <asp:Label ID="lblpermission1" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" Visible="false"></asp:Label>
                    </td>
                    <%-- <td colspan="2">
                        <asp:ImageButton ID="imgontime" runat="server" ImageUrl="~/bioimage/On Time.jpg"
                            OnClick="imgontime_Click" Visible="False" />
                    </td>
                    <td>
                        <asp:Label ID="lblontime" runat="server" Font-Bold="True" Font-Names="Book Antiqua"
                            Font-Size="Medium" ForeColor="Violet" Visible="False"></asp:Label>
                    </td>--%>
                </tr>
            </table>
            <asp:Label ID="lblnorec" runat="server" Text="There are no Records Found" ForeColor="Red"
                Visible="False" Font-Bold="True" Font-Names="Book Antiqua" Font-Size="Small"></asp:Label>
            <br />
            <center>
                <FarPoint:FpSpread ID="fpbiomatric" runat="server" BorderColor="Black" BorderStyle="Solid"
                    BorderWidth="1px" Height="295px" Width="1000px">
                    <CommandBar BackColor="Control" ButtonType="PushButton" ButtonFaceColor="Control"
                        ButtonHighlightColor="ControlLightLight" ButtonShadowColor="ControlDark">
                    </CommandBar>
                    <Sheets>
                        <FarPoint:SheetView SheetName="Sheet1">
                        </FarPoint:SheetView>
                    </Sheets>
                </FarPoint:FpSpread>
            </center>
            <asp:Button ID="btnprintmaster" runat="server" Text="Print" OnClick="btnprintmaster_Click"
                Font-Names="Book Antiqua" Font-Size="Medium" Font-Bold="true" Visible="False" />
            <Insproplus:PRINTPDF runat="server" ID="Printcontrol" Visible="false" />
    </body>
    </html>
</asp:Content>
