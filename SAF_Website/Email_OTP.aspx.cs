using SAF_Website.BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAF_Website
{
    public partial class Email_OTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //validations
        private bool ValidateInput()
        {
           
            LabelMsgError.Text = String.Empty;
            LabelMsgError.ForeColor = Color.Red;


            if (String.IsNullOrEmpty(OTPTextbox.Text))
            {
                LabelMsgError.Text += "OTP field  is required!" + "<br/>";
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

        public string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            User user = new User(Session["loggedInUsername"].ToString(), OTPTextbox.Text);
            User identifyOtp = BLL.User.GetUsersOTP(Session["loggedInUsername"].ToString(), EnryptString(OTPTextbox.Text));
           
            theMsg.Visible = true;
            if (ValidateInput())
            {
                if (identifyOtp != null)
                {
                    theMsg.Visible = false;
                    base.Session["loggedInOTP"] = identifyOtp.otp;
                    Response.Redirect("SearchParticipant.aspx");
                }
                else
                {
                    theMsg.Visible = true;
                    LabelMsgError.Text = "Please enter a valid otp!";
                    LabelMsgError.ForeColor = Color.Red;
                }
            }
        }

        protected void OTPTextbox_TextChanged(object sender, EventArgs e)
        {
            EnryptString(OTPTextbox.Text);
        }
    }
}