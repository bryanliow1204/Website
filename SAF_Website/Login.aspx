<%@ Page Title="" Language="C#" MasterPageFile="~/Nav.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SAF_Website.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Contents/Css/Login.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <br />
    <br />
    <div class="container">
        <!--Label Message Section-->
        <div class="alert alert-warning alert-dismissible fade show" role="alert" runat="server" id="theMsg" visible="false"  auto-close="3000">
            <asp:Label ID="LabelMsgError" runat="server" Text=""></asp:Label>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto table-responsive-sm">
                <div class="card card-signin my-5"  style="width:500px;">
                    <div class="card-body">
                        <h5 class="card-title text-center"><i class="fas fa-user-circle  fa-5x"></i></h5>
                    
                             <!-- Textbox for Email Section-->
                            <div class="form-label-group">
                                <asp:TextBox ID="EmailTextbox" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
                                <label for="inputEmail">Email address</label>
                            </div>

                             <!--Sign In button Section-->
                            <div class="input-group-append">
                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-lg btn-dark btn-block text-uppercase" OnClick="LinkButton1_Click">Sign In&nbsp<i class="fas fa-sign-in-alt"></i></asp:LinkButton>
                            </div>
                         <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--Script for label msg-->
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
