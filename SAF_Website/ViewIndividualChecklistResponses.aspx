<%@ Page Title="" Language="C#" MasterPageFile="~/Nav.Master" AutoEventWireup="true" CodeBehind="ViewIndividualChecklistResponses.aspx.cs" Inherits="SAF_Website.ViewIndividualChecklistResponses"   EnableEventValidation="false"  %>
<%@ MasterType VirtualPath="~/Nav.Master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="Contents/Css/PreviewChecklist.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
      <div style="background: rgb(0,93,129);background: linear-gradient(-160deg, rgba(0,93,129,1) 0%, rgba(0,93,129,1) 29%, rgba(0,142,168,1) 100%);"">
       <div class="container">
           <br />
           <!--Title Section-->
           <h4 style="color: white; font-family:Raleway;">View Checklists Questions</h4>

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
                 <asp:LinkButton ID="LinkButton2" runat="server"  CssClass="btn btn-default form-control" BackColor="#48baff" ForeColor="White" OnClick="LinkButton2_Click"><i class="fas fa-table"></i>&nbspExport to Excel</asp:LinkButton>
             </div>
         </div>
    </div>
    <!-- Gridview Section: -->
    <div>
        <div class="container">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover align-content-center table-responsive-sm mt-4 " GridLines="None" Border-Style="none" Style="font-size: 15px;overflow:hidden;width: 100%;word-wrap:break-word;table-layout: fixed;" AllowPaging="True" AllowSorting="True" PageSize="5" OnSorting="GridView1_Sorting" OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" BackColor="White">
                    <Columns>
                        <asp:TemplateField HeaderText="Qid" SortExpression="user_activity_checklist_response_id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("user_activity_checklist_response_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sequence Number" SortExpression="sequence_no">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("sequence_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Question Name" SortExpression="question">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("question") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User's Response" SortExpression="chosen_option">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("chosen_option") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                   
                        <asp:TemplateField HeaderText="Threshold Option Number" SortExpression="threshold_flag">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("threshold_flag") %>'></asp:Label>
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
                     <asp:LinkButton ID="LinkButton3" runat="server"  CssClass="btn btn-default form-control" BackColor="#48baff" ForeColor="White" OnClick="LinkButton3_Click">Back</asp:LinkButton>
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
