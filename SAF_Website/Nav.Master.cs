using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAF_Website
{
    public partial class Nav : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public  HyperLink linksearch
        {
            get
            {
                return this.LinkSearch;
            }
        }

        public HyperLink linkmanage
        {
            get
            {
                return this.LinkManage;
            }
        }


        public Label labeluser
        {
            get
            {
                return this.LabelUser;
            }
        }
        public HyperLink linklogin
        {
            get
            {
                return this.LinkLogin;
            }
        }

        public HyperLink linklogout
        {
            get
            {
                return this.LinkLogout;
            }
        }

    }
}