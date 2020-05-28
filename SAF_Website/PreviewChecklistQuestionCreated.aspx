<%@ Page Title="" Language="C#" MasterPageFile="~/Nav.Master" AutoEventWireup="true" CodeBehind="PreviewChecklistQuestionCreated.aspx.cs" Inherits="SAF_Website.PreviewChecklistQuestionCreated" %>
<%@ MasterType VirtualPath="~/Nav.Master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="Contents/Css/PreviewChecklist.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
      <div style="background: rgb(0,93,129);background: linear-gradient(-160deg, rgba(0,93,129,1) 0%, rgba(0,93,129,1) 29%, rgba(0,142,168,1) 100%);"">
       <div class="container">
           <br />
           <!--Title Section-->
           <h4 style="color: white; font-family:Raleway;">View Checklist Questions Created</h4>

           <br>
           <!--Label Message Section-->
           <div class="alert alert-info alert-dismissible fade show" role="alert" runat="server" id="theMsg" visible="false" auto-close="3000">
               <asp:Label ID="LabelMsgError" runat="server" Text="Please enter the question name!"></asp:Label>
               <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                   <span aria-hidden="true">&times;</span>
               </button>
           </div>
           <div class="row">
               <!-- Checklist Section: -->
               <div class="col-md-10">
                   <div class="row" style="margin-bottom: 20px;">
                       <div class="col-md-4 mt-2">
                           <asp:Label ID="Label9" runat="server" Text="" ForeColor="White"></asp:Label>
                       </div>
                       <div class="col-md-8">
                           <asp:TextBox ID="TextBoxChecklist" runat="server" CssClass="form-control" placeholder="Enter name of question" OnTextChanged="TextBoxChecklist_TextChanged"></asp:TextBox>
                       </div>
                   </div>
                   <!-- Checklist Section: -->
                   <div class="row">
                       <div class="col-md-4"></div>
                       <div class="col-md-8"></div>
                   </div>
               </div>
               <!-- Search button section: -->
               <div class="col-md-2">
                   <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-warning form-control" Text="" OnClick="LinkButton1_Click"> <i class="fas fa-search"></i>&nbsp&nbspSearch </asp:LinkButton>
               </div>
           </div>
       </div>
    </div>

       <div class="container mt-3">
         <div class="row"">
             <div class="col-md-5"></div>
             <div class="col-md-5"></div>
             <div class="col-md-2">
                 <asp:LinkButton ID="LinkButton2" runat="server"  CssClass="btn btn-default form-control" BackColor="#48baff" ForeColor="White" OnClick="LinkButton2_Click"><i class="fas fa-plus"></i>&nbspAdd Questions</asp:LinkButton>
             </div>
         </div>
    </div>
    <!-- Gridview Section: -->
    <div>
        <div class="container">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover align-content-center table-responsive-sm mt-4 " GridLines="None" Border-Style="none" Style="font-size: 15px;overflow:hidden;width: 100%;word-wrap:break-word;table-layout: fixed;" AllowPaging="True" AllowSorting="True" PageSize="5" OnSorting="GridView1_Sorting" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" BackColor="White">
                    <Columns>
                        <asp:TemplateField HeaderText="Qid" SortExpression="question_id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("question_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Question Name" SortExpression="question">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("question") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="First Option" SortExpression="option_one">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("option_one") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Second Option" SortExpression="option_two">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("option_two") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Third Option" SortExpression="option_three">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("option_three") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fouth Option" SortExpression="option_four">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("option_four") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type" SortExpression="type">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("type") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Threshold_Option" SortExpression="threshold_flag"  ItemStyle-Width="150px">
                            <HeaderStyle  ForeColor="White" Font-Size="12px" BackColor="#505050" Width="150px"/>
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("threshold_flag") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <a class="mx-2" href="#" data-toggle="dropdown" style="color: black;"><i class="fas fa-ellipsis-h"></i></a>
                                    <div runat="server" class="dropdown-menu" id="dropdowncontents">
                                         <asp:LinkButton ID="LinkButtonEdit" CommandName="Update" CssClass="dropdown-item"  runat="server">Update</asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonDelete" CommandName="Delete" CssClass="dropdown-item"  runat="server">Delete</asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle  ForeColor="White" Font-Size="12px" BackColor="#505050" />
                </asp:GridView>
            </div>
        <!-- Back button section: -->
        <div class="container mt-3">
             <div class="row"">
                 <div class="col-md-4"></div>
                 <div class="col-md-4">
                     <asp:LinkButton ID="LinkButton3" runat="server"  CssClass="btn btn-default form-control" BackColor="#48baff" ForeColor="White" OnClick="LinkButton3_Click">Done</asp:LinkButton>
                 </div>
                 <div class="col-md-4"></div>
             </div>
        </div>
    </div>
    <!-- Script for success label msg -->
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
