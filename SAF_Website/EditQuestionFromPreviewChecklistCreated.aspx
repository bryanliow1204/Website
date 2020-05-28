<%@ Page Title="" Language="C#" MasterPageFile="~/Nav.Master" AutoEventWireup="true" CodeBehind="EditQuestionFromPreviewChecklistCreated.aspx.cs" Inherits="SAF_Website.EditQuestionFromPreviewChecklistCreated" %>
<%@ MasterType VirtualPath="~/Nav.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Contents/Css/IndividualChecklist.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
       <script type="text/javascript">
        function funover()
        {
            var name = document.getElementById('<%= Labelerrors.ClientID %>').innerHTML;
            document.getElementById('<%= LabelQn2.ClientID %>').innerHTML = name;
            document.getElementById('<%= LabelQn2.ClientID %>').style.display = "block";
        }
        function funover1() {
            var name = document.getElementById('<%= Labelerrors.ClientID %>').innerHTML;
            document.getElementById('<%= LabelOptionMsg.ClientID %>').innerHTML = name;
            document.getElementById('<%= LabelOptionMsg.ClientID %>').style.display = "block";
        }
        function funover2() {
            var name = document.getElementById('<%= Labelerrors.ClientID %>').innerHTML;
            document.getElementById('<%= LabelOption2Msg.ClientID %>').innerHTML = name;
            document.getElementById('<%= LabelOption2Msg.ClientID %>').style.display = "block";
        }
        function funover3() {
            var name = document.getElementById('<%= Labelerrors.ClientID %>').innerHTML;
                document.getElementById('<%= LabelOption3Msg.ClientID %>').innerHTML = name;
                document.getElementById('<%= LabelOption3Msg.ClientID %>').style.display = "block";
        }
        function funover4() {
            var name = document.getElementById('<%= Labelerrors.ClientID %>').innerHTML;
            document.getElementById('<%= LabelOption4Msg.ClientID %>').innerHTML = name;
            document.getElementById('<%= LabelOption4Msg.ClientID %>').style.display = "block";
        }
        function funout()
        {
            document.getElementById('<%= LabelQn2.ClientID %>').style.display = "none";
        }
        function funout1()
        {
            document.getElementById('<%= LabelOptionMsg.ClientID %>').style.display = "none";
        }
        function funout2()
        {
            document.getElementById('<%= LabelOption2Msg.ClientID %>').style.display = "none";
        }
        function funout3() {
            document.getElementById('<%= LabelOption3Msg.ClientID %>').style.display = "none";
        }
        function funout4() {
            document.getElementById('<%= LabelOption4Msg.ClientID %>').style.display = "none";
        }
    </script>
    <div style="background: rgb(0,93,129); background: linear-gradient(-160deg, rgba(0,93,129,1) 0%, rgba(0,93,129,1) 29%, rgba(0,142,168,1) 100%);">
        <div class="container">
            <br />
            <h4 style="color: white; margin-bottom: 10px; font-family: Raleway;">Update Checklist Questions</h4>
            <p style="color: white;"><i class="fas fa-list-ul"></i>&nbspManage Checklists / Create New Checklist</p>
            <br>
            <!-- Label Msg Section: -->
            <div class="alert alert-info alert-dismissible fade show" role="alert" runat="server" id="theMsg" visible="false">
                <asp:Label ID="LabelMsgError" runat="server" Text=""></asp:Label>
            </div>
            <div class="alert alert-info alert-dismissible fade show" role="alert" runat="server" id="theMsgOptions" visible="false" auto-close="3000">
                <asp:Label ID="LabelOptionErrors" runat="server" Text=""></asp:Label>
            </div>
            <div class="alert alert-info alert-dismissible fade show" role="alert" runat="server" id="theMsgQn" visible="false" auto-close="3000">
                <asp:Label ID="LabelQuestionErrors" runat="server" Text=""></asp:Label>
            </div>

            <div class="row">
                <!-- Checklist Section: -->
                <div class="col-md-8">
                    <div class="row" style="margin-bottom: 20px;">
                        <div class="col-md-4 mt-2">
                            <asp:Label ID="Label9" runat="server" Text="Checklist&amp;nbsp Name" ForeColor="White"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="TextBoxTitle" runat="server" CssClass="form-control" placeholder="Enter name of checklist" OnTextChanged="TextBoxTitle_TextChanged"></asp:TextBox>
                            <asp:TextBox ID="TextBoxTitle2" runat="server" Visible="false" CssClass="form-control" placeholder="Enter name of checklist" OnTextChanged="TextBoxTitle_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    <!-- Checklist Section: -->
                    <div class="row">
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-8">
                        </div>
                    </div>
                </div>
                <!-- Save button section: -->
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CssClass="btn btn-warning form-control" Visible="true" OnClick="LinkButtonEdit_Click"><i class="far fa-edit"></i>&nbsp&nbspEdit</asp:LinkButton>
                            <asp:LinkButton ID="LinkButtonEdit2" runat="server" CssClass="btn btn-warning form-control" Visible="false" OnClick="LinkButtonEdit2_Click"><i class="far fa-edit"></i>&nbsp&nbspUpdate</asp:LinkButton>
                            <%--<asp:LinkButton ID="LinkButtonSave" runat="server" CssClass="btn btn-warning form-control" Text="" OnClick="LinkButtonSave_Click"> <i class="fas fa-download"></i>&nbsp&nbspSave </asp:LinkButton>--%>
                        </div>
                        <div class="col-md-6">
                            <asp:LinkButton ID="LinkButtonCancel" runat="server" CssClass="btn btn-warning form-control" Text="" OnClick="LinkButtonCancel_Click" Visible="false"> <i class="fas fa-times"></i>&nbsp&nbspCancel </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-- Table Section: -->
    <div>
        <div class="container text-center">
            <div class="alert alert-success alert-dismissible fade show mt-2" role="alert" runat="server" id="Successlabel" auto-close="3000" visible="false">
                <asp:Label ID="LabelSuccess" runat="server" Text="Successfully Updated" ForeColor="Green"></asp:Label>
            </div>
            <div class="row" style="text-align: center;">

                <!-- Displaying the Error msg using the text of html in the javascript Section: -->
                <label for="error" id="Labelerrors" runat="server" style="visibility: hidden; font-size: 5px;">Required</label>
                <label for="error" id="Labelerrors2" runat="server" style="visibility: hidden; font-size: 5px;"></label>

                <table class="table table-hover align-content-center table-responsive-sm mt-4" id="myTable" style="font-size: 14px; background-color: white; overflow: hidden;">

                    <t>
                     <!-- Table content for question title -->
                    <tr style="background-color:grey;max-width:none;">
                      <div class="col-sm-10">
                      <th scope="row" style="background-color:grey;color:white;"><div class="col-sm-4 mt-2" style="color:white;">Question:</div></th>
                  
                      <td style="background-color:grey;">
                          <div class="col-sm-8">
                              <div class="row">
                                  <!-- content for question title -->
                                  <div class="col-sm-9">
                                      <asp:TextBox ID="TextBoxQuestion" CssClass="form-control" runat="server"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-1"></div>
                                  <div class="col-sm-1 " style="font-size:25px;">
                                       <asp:Label ID="LabelQn" runat="server" Visible="false" Text="" ForeColor="Red" onmouseover="funover()"  onmouseout="funout()"  ></asp:Label>
                                  </div>
                                  <div class="col-sm-1 mt-1" style="font-size:20px;">
                                      <asp:Label ID="LabelQn2" runat="server" style="font-size:15px;" Visible="false" ForeColor="Red"></asp:Label>
                                  </div>
                              </div>
                          </div>
                      </td>
                      </div>      
                      <div class="col-sm-2"></div>      
                      
                    </tr>

                    <!-- Table content for Question Type row data: -->
                    <tr>
                        <div class="col-sm-10">
                            <th scope="row" style="background-color:white;color:black;"><div class="col-sm-4 mt-2" style="color:black;">Type:</div></th>
                            <td style="background-color:white;">
                                <div class="col-sm-8">
                                    <div class="row">
                                        <!-- content for question title -->
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                <asp:ListItem>--Select--</asp:ListItem>
                                                <asp:ListItem>MCQ</asp:ListItem>
                                                <asp:ListItem>Slider</asp:ListItem>
                                                <asp:ListItem>Short Question</asp:ListItem>
                                                <asp:ListItem>Checkbox</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-1 " style="font-size:25px;">
                                            <asp:Label ID="Label1" runat="server" Visible="false" Text="" ForeColor="Red" onmouseover="funover()"  onmouseout="funout()"  ></asp:Label>
                                        </div>
                                        <div class="col-sm-1 mt-1" style="font-size:20px;">
                                            <asp:Label ID="Label2" runat="server" style="font-size:15px;" Visible="false" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </div>
                        <div class="col-sm-2"></div>
                    </tr>

                    <!-- Table content for first row data: -->
                    <tr>
                      <div class="col-sm-10" style="background-color:white;"  runat="server" id="divOpt1" visible="false">
                      <th scope="row"><div class="col-sm-4 mt-2" style="max-width:none;">Option 1:</div></th>
                      <td>
                          <div class="col-sm-8">
                              <div class="row">
                              <!--content for first option textbox: -->
                                  <div class="col-sm-9">
                                       <asp:TextBox ID="TextBoxOption1" CssClass="form-control" runat="server"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-1 mt-2">
                                       <asp:RadioButton ID="RadioButtonOption1" runat="server" OnCheckedChanged="RadioButtonOption1_CheckedChanged" AutoPostBack="True" GroupName = "radiobutton"></asp:RadioButton>
                                  </div>
                                   <div class="col-sm-1"style="font-size:25px;">
                                      <asp:Label ID="LabelOption1" runat="server" Visible="false" Text="" ForeColor="Red" onmouseover="funover1()"  onmouseout="funout1()"></asp:Label>
                                  </div>
                                   <div class="col-sm-1 mt-1">
                                       <asp:Label ID="LabelOptionMsg" Visible="false" runat="server" style="font-size:15px;" ForeColor="Red"></asp:Label>
                                  </div>
                              </div>
                          </div>
                      </td>
                      </div>      
                      <div class="col-sm-2"></div>
                      
                    </tr>

                    <!-- Table content for Option 2 -->
                    <tr>
                      <div class="col-sm-10"  runat="server" id="divOpt2" visible="false">
                      <th scope="row"><div class="col-sm-4 mt-2" style="max-width:none;">Option 2:</div></th>
                      <td>
                          <div class="col-sm-8">
                              <div class="row">
                                  <!-- content for Option 2 -->
                                  <div class="col-sm-9">
                                       <asp:TextBox ID="TextBoxOption2" CssClass="form-control" runat="server"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-1 mt-2">
                                       <asp:RadioButton ID="RadioButtonOption2" runat="server" OnCheckedChanged="RadioButtonOption2_CheckedChanged" AutoPostBack="True" GroupName = "radiobutton"></asp:RadioButton>
                                  </div>
                                  <div class="col-sm-1 " style="font-size:25px;">
                                     <asp:Label ID="LabelOption2" runat="server" Text="" ForeColor="Red" onmouseover="funover2()"  onmouseout="funout2()"></asp:Label>
                                  </div>
                                  <div class="col-sm-1 mt-1" >
                                      <asp:Label ID="LabelOption2Msg" runat="server" style="font-size:15px;" ForeColor="Red"></asp:Label>
                                  </div>
                              </div>
                          </div>
                      </td>
                      </div>      
                      <div class="col-sm-2">
                      </div>   
                    </tr>

                    <!-- Table content for Option 3 -->
                    <tr>
                      <div class="col-sm-10"  runat="server" id="divOpt3" visible="false">
                      <th scope="row">
                          <div class="col-sm-4 mt-2" style="max-width:none;">Option 3:</div>
                      </th>
                      <td>
                          <div class="col-sm-8">
                              <div class="row">
                              <!--content for Option 3 -->
                                  <div class="col-sm-9">
                                      <asp:TextBox ID="TextBoxOption3" CssClass="form-control" runat="server"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-1 mt-2">
                                      <asp:RadioButton ID="RadioButtonOption3" runat="server" OnCheckedChanged="RadioButtonOption3_CheckedChanged" AutoPostBack="True" GroupName = "radiobutton"></asp:RadioButton>
                                  </div>
                                  <div class="col-sm-1" style="font-size:25px;">
                                       <asp:Label ID="LabelOption3"  runat="server" Text="" ForeColor="Red" onmouseover="funover3()"  onmouseout="funout3()" ></asp:Label>
                                  </div>
                                  <div class="col-sm-1 mt-1">
                                      <asp:Label ID="LabelOption3Msg" runat="server" style="font-size:15px;" ForeColor="Red"></asp:Label>
                                  </div>
                              </div>
                          </div>
                      </td>
                      </div>  
                      <div class="col-sm-2"></div>      
                    </tr>

                    <!-- Table content for Option 4 -->
                    <tr>
                      <div class="col-sm-10"  runat="server" id="divOpt4" visible="false">
                      <th scope="row">
                          <div class="col-sm-4 mt-2" style="max-width:none;">Option 4:</div>
                      </th>
                      <td>
                          <div class="col-sm-8">
                              <div class="row">
                              <!--content for Option 4 -->
                                  <div class="col-sm-9">
                                       <asp:TextBox ID="TextBoxOption4" CssClass="form-control" runat="server"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-1 mt-2">
                                      <asp:RadioButton ID="RadioButtonOption4" runat="server" OnCheckedChanged="RadioButtonOption4_CheckedChanged" AutoPostBack="True" GroupName = "radiobutton"></asp:RadioButton>
                                  </div>
                                  <div class="col-sm-1"style="font-size:25px;">
                                      <asp:Label ID="LabelOption4" runat="server" ForeColor="Red" onmouseover="funover4()"  onmouseout="funout4()"></asp:Label>
                                  </div>
                                   <div class="col-sm-1 mt-1">
                                       <asp:Label ID="LabelOption4Msg" runat="server" style="font-size:15px;" ForeColor="Red"></asp:Label>
                                  </div>
                              </div>
                          </div>
                      </td>
                      </div>
                      <div class="col-sm-2"></div>      
                    </tr>


                    <!-- Buttons to update the contents -->
                    <tr>
                        <div class="col-sm-10">
                        <th scope="row">
                            <div class="col-sm-4 mt-2"></div>
                        </th>
                        <td>
                            <div class="col-sm-8">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <asp:Button ID="ButtonUpdate" CssClass="btn-primary form-control" runat="server" Text="Update Question" OnClick="ButtonUpdate_Click"></asp:Button>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="ButtonBack" CssClass="btn-primary form-control" runat="server" Text="Back" OnClick="ButtonBack_Click"></asp:Button>
                                    </div>
                                    
                                    <div class="col-sm-4"></div>
                                </div>
                            </div>
                        </td>
                        </div>
                        <div class="col-sm-2"></div>      
                    </tr>
                  </t>
                </table>
            </div>
            <asp:Label ID="LabelMsg" runat="server" Text=""></asp:Label>
            <br />
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
