<%@ Page Title="" Language="C#" MasterPageFile="~/Nav.Master" AutoEventWireup="true" CodeBehind="SearchParticipant.aspx.cs" Inherits="SAF_Website.SearchParticipant"  EnableEventValidation="false" %>
<%@ MasterType VirtualPath="~/Nav.Master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Contents/Css/Search.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">

    <div style="background: rgb(0,93,129); background: linear-gradient(-160deg, rgba(0,93,129,1) 0%, rgba(0,93,129,1) 29%, rgba(0,142,168,1) 100%);">
        <div class="container">
            <br />
            <div class="alert alert-info alert-dismissible fade show" role="alert" runat="server" id="Div1" visible="false" auto-close="3000">
                <asp:Label ID="Label6" runat="server" Text="Please enter at least one field"></asp:Label>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
           
            <h4 style="color: white; font-family: Raleway;">Search Participants</h4>
            <br>
            <div class="alert alert-warning alert-dismissible fade show" role="alert" runat="server" id="theMsg" visible="false">
                <asp:Label ID="LabelMsgError" runat="server" Text="Please enter at least one field"></asp:Label>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="row">
                <div class="col-md-5">
                    <!-- Activity Date Section: -->
                    <div class="row" style="margin-bottom: 20px;">
                        <div class="col-md-4 mt-1">
                            <asp:Label ID="Label7" runat="server" Text="Activity Date:" ForeColor="White"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="TextBoxDate" runat="server" CssClass="form-control" placeholder="" TextMode="Date" OnTextChanged="TextBoxDate_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    <!-- Activity Name Section: -->
                    <div class="row" style="margin-bottom: 20px;">
                        <div class="col-md-4 mt-1">
                            <asp:Label ID="Label8" runat="server" Text="Activity Name:" ForeColor="White"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" AutoPostBack="false" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                <asp:ListItem Value="Value 3" Text="--Please Select One Activity--" />
                                <asp:ListItem Value="Parachuting Exercise" Text="Parachuting Exercise" />
                                <asp:ListItem Value="Marching" Text="Marching" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <!-- Group Section: -->
                <div class="col-md-5">
                    <div class="row" style="margin-bottom: 20px;">
                        <div class="col-md-4 mt-1">
                            <asp:Label ID="Label9" runat="server" Text="Group:&amp;nbsp" ForeColor="White"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="TextBoxGroupNumber" runat="server" CssClass="form-control" placeholder="Enter group number" OnTextChanged="TextBoxGroupNumber_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    <!-- Participant's Name Section: -->
                    <div class="row" style="margin-bottom: 20px;">
                        <div class="col-md-4 mt-1">
                            <asp:Label ID="Label10" runat="server" Text="Participant Name:" ForeColor="White"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="TextBoxName" runat="server" CssClass="form-control" placeholder="Enter name" OnTextChanged="TextBoxName_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!-- Search button section: -->
                <div class="col-md-2">
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-warning form-control" Text="" Style="margin-top: 57px;" OnClick="LinkButton1_Click"> <i class="fas fa-search"></i>&nbsp&nbspSearch </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    
    <!-- Export Button Section: -->
    <div class="container mt-3">
         <div class="row"">
             <div class="col-md-5"></div>
             <div class="col-md-5"></div>
             <div class="col-md-2">
                 <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"  CssClass="btn btn-default form-control" BackColor="#48baff" ForeColor="White"><i class="fas fa-table"></i>&nbspExport to Excel</asp:LinkButton>
                 <asp:LinkButton ID="LinkButton3" runat="server" Visible="false" CssClass="btn btn-default form-control" BackColor="#48baff" ForeColor="White" OnClick="LinkButton3_Click"><i class="fas fa-table"></i>&nbspExport to Excel</asp:LinkButton>
             </div>
         </div>
    </div>

    <!-- Gridview Section: -->
    <div style="background-color: #eaeaea;">
        <br />
        <div class="container">
            <asp:Label ID="Errorlabel" runat="server" Text="No such user found in the database!" Visible ="false"></asp:Label>
            
           
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover align-content-center table-responsive-sm " GridLines="None" Border-Style="none" Style="font-size: 15px;overflow:hidden;width: 100%;word-wrap:break-word;table-layout: fixed;" AllowPaging="True" AllowSorting="True" PageSize="5" OnSorting="GridView1_Sorting" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" BackColor="White">

                    <Columns>
                        <asp:TemplateField HeaderText="Id" SortExpression="activity_id" Visible ="false">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("activity_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Activity Date" SortExpression="activity_date">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("activity_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Activity Name" SortExpression="activity_name">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("activity_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Participant's Name" SortExpression="name">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Group" SortExpression="group_name">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("group_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="EditButton"
                                    runat="server"
                                    OnSelectedIndexChanged="Checklist_SelectedIndexChanged1"
                                    CommandName="Select"
                                    Text=<%# Bind("checklist_name") %> />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle ForeColor="White" Font-Size="12px" BackColor="#505050"/>
                </asp:GridView>
            </div>
    </div>
    <!-- Script for label msg-->
    <script type="text/javascript">
        $(function () {
            var alert = $('div.alert[auto-close]');
            alert.each(function () {
                var that = $(this);
                var time_period = that.attr('auto-close');
                setTimeout(function () {
                    that.alert('close');
                }, time_period);
            });
        });
    </script>
</asp:Content>
