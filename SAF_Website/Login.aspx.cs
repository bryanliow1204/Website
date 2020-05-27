using Newtonsoft.Json;
using SAF_Website.BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAF_Website
{
    public partial class Login : System.Web.UI.Page
    {
        static string activationcode;
        public SmtpDeliveryMethod DeliveryMethod { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //validations
        private bool ValidateInput()
        {
            LabelMsgError.Text = String.Empty;
            LabelMsgError.ForeColor = Color.Red;


            if (String.IsNullOrEmpty(EmailTextbox.Text))
            {
                LabelMsgError.Text += "Email field  is required!" + "<br/>";
            }

            if (String.IsNullOrEmpty(LabelMsgError.Text))
            {
                return true;
            }
            else
            {

                return false;
            }
        }

        //Login button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            User user = new User(EmailTextbox.Text, activationcode);
         
            //call the method in the BLL class(User)
            User identify = BLL.User.GetUsers(EmailTextbox.Text,activationcode);

            //check if it return item means data exsits
            if(identify != null)
            {
                base.Session["loggedInUsername"] = user.email_addr;
                Response.Redirect("Email_OTP.aspx");
            }

            //check if it return null means no such data exsits
            if(identify == null)
            {
                theMsg.Visible = true;
                LabelMsgError.Text = "No such user in database ! Please enter a valid user!";
                LabelMsgError.ForeColor = Color.Red;
            }
        }
    }
}