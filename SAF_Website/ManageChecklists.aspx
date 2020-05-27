<%@ Page Title="" Language="C#" MasterPageFile="~/Nav.Master" AutoEventWireup="true" CodeBehind="ManageChecklists.aspx.cs" Inherits="SAF_Website.Manage" %>
<%@ MasterType VirtualPath="~/Nav.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Contents/Css/Search.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
   
    <div style="background: rgb(0,93,129);background: linear-gradient(-160deg, rgba(0,93,129,1) 0%, rgba(0,93,129,1) 29%, rgba(0,142,168,1) 100%);"">
        <div class="container">
            <br />
             <!--Label Message Section-->
            <div class="alert alert-info alert-dismissible fade show " role="alert" runat="server" id="theMsg" visible="false" auto-close="3000">
                <asp:Label ID="LabelMsgError" runat="server" Text=""></asp:Label>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>

            <div class="alert alert-info alert-dismissible fade show " role="alert" runat="server" id="Div1" visible="false" auto-close="3000">
                <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
           
            <h4 style="color: white; font-family:Raleway;">Manage Checklists</h4>
            <br>
           
            <div class="row">
                 <!-- Checklist Section: -->
                <div class="col-md-10">
                    <div class="row" style="margin-bottom: 20px;">
                        <div class="col-md-4">
                            <asp:Label ID="Label9" runat="server" Text="Checklist&amp;nbsp" ForeColor="White"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="TextBoxChecklist" runat="server" CssClass="form-control" placeholder="Enter name of checklist you wish to search" OnTextChanged="TextBoxChecklist_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                     <!-- Checklist Section: -->
                    <div class="row" >
                        <div class="col-md-4">
                              <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-warning form-control" Text="" OnClick="LinkButton2_Click" > <i class="fas fa-pen"></i>&nbsp&nbspNew Checklist </asp:LinkButton>
                           
                        </div>
                        <div class="col-md-8"></div>
                    </div>
                </div>
                 <!-- Search button section: -->
                <div class="col-md-2">
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-warning form-control" Text="" OnClick="LinkButton1_Click"> <i class="fas fa-search"></i>&nbsp&nbspSearch </asp:LinkButton>

                </div>
            </div>

            <div class="row" style="margin-top: 30px;">
                <div class="col-md-6">
                </div>
                <div class="col-md-6">
                </div>
            </div>
        </div>
    </div>

    <!-- Table Section: -->
    <!-- Gridview Section: -->
    <div style="background-color: #eaeaea;">
        <br />
        <div class="container">
            <asp:Label ID="Errorlabel" runat="server" Text="No such user found in the database!" Visible ="false"></asp:Label>
            <div class="rounded-corners" style="-webkit-border-radius: 8px; -moz-border-radius: 8px; border-radius: 8px;">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover align-content-center table-responsive-sm " GridLines="None" Border-Style="none" Style="font-size: 15px;overflow:hidden;width: 100%;word-wrap:break-word;table-layout: fixed;" AllowPaging="True" AllowSorting="True" PageSize="5" OnSorting="GridView1_Sorting" OnPageIndexChanging="GridView1_PageIndexChanging" BackColor="White"  OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting1" OnRowCreated="GridView1_RowCreated"  OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Id" SortExpression="checklist_id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("checklist_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Checklist" SortExpression="checklist_name">
                            <ItemTemplate>
                                <asp:LinkButton ID="EditButton"
                                    runat="server"
                                    OnSelectedIndexChanged="Checklist_SelectedIndexChanged1"
                                    CommandName="Select"
                                    Text='<%# Bind("checklist_name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created By" SortExpression="name">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created On" SortExpression="created_checklist">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("created_checklist") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <a class="mx-2" href="#" data-toggle="dropdown" style="color: black;"><i class="fas fa-ellipsis-h"></i></a>
                                <div runat="server" class="dropdown-menu" id="dropdowncontents">
                                    <asp:LinkButton ID="LinkEdit" CommandName="Select" CssClass="dropdown-item" runat="server">Edit</asp:LinkButton>
                                    <asp:LinkButton ID="LinkDuplicate" CommandName="Add" CssClass="dropdown-item" runat="server">Duplicate</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButtonDelete" CommandName="Delete" CssClass="dropdown-item" runat="server">Delete</asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="" ForeColor="White" Font-Size="12px" BackColor="#505050" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <!-- Script for label msg -->
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
