using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SAF_Website.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static SAF_Website.BLL.Participants;
using System.Web.Script.Serialization;
using System.Net.Http;

namespace SAF_Website
{
    public partial class SearchParticipant : System.Web.UI.Page
    {
        //convert list to datatable
        public DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        protected void Page_Load(object sender, EventArgs e)
        {   
            User registration = BLL.User.GetUsersOTP(base.Session["loggedInUsername"].ToString(), base.Session["loggedInOTP"].ToString());
            if (Session["loggedInUsername"] != null)
            {
                Master.linksearch.Visible = true;
                Master.linkmanage.Visible = true;
                Master.linklogin.Visible = false;
                Master.linklogout.Visible = true;
                Master.labeluser.Text = registration.name.ToString();
            }

            DataTable dt = new DataTable();
            dt = this.GetData();
            GridView1.DataSource = this.GetData();
            GridView1.DataBind();

            for (int i = 0; i < 5; i++)
            {
                TableCell tableCell = GridView1.HeaderRow.Cells[i];
                Image img = new Image();
                img.ImageUrl = "~/Images/sortt.png";
                img.CssClass = "imgg";
                img.Width = 20;
                img.Height = 20;
                tableCell.Controls.Add(new LiteralControl("&nbsp;"));
                tableCell.Controls.Add(img);
                ViewState["tables"] = dt;
            }
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        //populate the data into a datatable
        private DataTable GetData()
        {
            IEnumerable<Participants> listofParticipant = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44384/api/Activity/");
            var response = hc.GetAsync("GetALL");
            response.Wait();

            var results = response.Result;
            DataTable dt = new DataTable();
            if (results.IsSuccessStatusCode)
            {
                var displayContent = results.Content.ReadAsStringAsync().Result;
                var resultcontents = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Participants>>(displayContent);
                dt = ToDataTable(resultcontents);
            }
            return dt;
        }

        //store the view of each select inside the gridview into a session and display into another page
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = GridView1.SelectedRow;
            var label2 = ((Label)GridView1.SelectedRow.Cells[1].FindControl("label2")).Text;
            var label4 = ((Label)GridView1.SelectedRow.Cells[3].FindControl("label4")).Text;
            var label6 = ((LinkButton)GridView1.SelectedRow.Cells[5].FindControl("EditButton")).Text;

            base.Session["nameofParticipant"] = label4;
            base.Session["nameofChecklist"] = label6;
            base.Session["dateofActivity"] = label2;

            Response.Redirect("ViewIndividualChecklistResponses.aspx");
        }

        //Find the control inside the div section
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // here you can access the div
                HtmlGenericControl div = e.Row.FindControl("dropdowncontents") as HtmlGenericControl;
                e.Row.Cells[0].Visible = false;
            }
        }

        //Gridview sorting 
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string SortDir = string.Empty;
            DataTable dt = new DataTable();
            dt = ViewState["tables"] as DataTable;
            {
                if (dir == SortDirection.Ascending)
                {
                    dir = SortDirection.Descending;
                    SortDir = "Desc";
                }

                else
                {
                    dir = SortDirection.Ascending;
                    SortDir = "Asc";
                }
                dt = this.GetData();
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + SortDir;
                Session["SortedResults"] = sortedView;
                GridView1.DataSource = sortedView;
                GridView1.DataBind();
                for (int i = 0; i < 5; i++)
                {

                    string lbText = ((LinkButton)GridView1.HeaderRow.Cells[i].Controls[0]).Text;

                    if (lbText == e.SortExpression)
                    {
                        TableCell tableCell = GridView1.HeaderRow.Cells[i];
                        Image img = new Image();
                        img.ImageUrl = (SortDir == "Asc") ? "~/Images/asc.png" : "~/Images/desc.png";
                        img.CssClass = "imgg";
                        img.Width = 20;
                        img.Height = 20;
                        tableCell.Controls.Add(new LiteralControl("&nbsp"));
                        tableCell.Controls.Add(img);
                    }
                    else
                    {
                        TableCell tableCell = GridView1.HeaderRow.Cells[i];
                        Image img = new Image();
                        img.ImageUrl = (SortDir == "Asc") ? "~/Images/asc.png" : "~/Images/desc.png";
                        img.CssClass = "imgg";
                        img.Width = 20;
                        img.Height = 20;
                        tableCell.Controls.Add(new LiteralControl("&nbsp"));
                        tableCell.Controls.Add(img);
                    }
                }
            }
        }

        //Sort the direction of the arrow
        public SortDirection dir
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }
            set
            {
                ViewState["dirState"] = value;
            }
        }

        //this is the gridview page index
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            DataTable dt = new DataTable();
            dt = this.GetData();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        
            for (int i = 0; i < 5; i++)
            {
                TableCell tableCell = GridView1.HeaderRow.Cells[i];
                Image img = new Image();
                img.ImageUrl = "~/Images/sortt.png";
                img.CssClass = "imgg";
                img.Width = 20;
                img.Height = 20;
                tableCell.Controls.Add(new LiteralControl("&nbsp;"));
                tableCell.Controls.Add(img);
                ViewState["tables"] = dt;
            }
        }

        //Search Button 
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
        
        }

        //Textbox for Date:
        protected void TextBoxDate_TextChanged(object sender, EventArgs e)
        {
            string question = TextBoxDate.Text;
            DataTable dt = this.GetData();
            DataView dataView = dt.DefaultView;
            if (!string.IsNullOrEmpty(question))
            {
                StringBuilder sb = new StringBuilder();

                if (TextBoxDate.Text.Length > 0)
                {
                    string DateString = question;
                    IFormatProvider culture = new CultureInfo("en-US", true);
                    DateTime dateVal = DateTime.ParseExact(DateString, "yyyy'-'MM'-'dd", culture);
                    var item = dateVal.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                    sb.Append("activity_date like '%" + item + "%'");
                }

                if (TextBoxGroupNumber.Text.Length > 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" and ");
                    }

                    sb.Append("group_name like '%" + TextBoxGroupNumber.Text + "%'");
                }

                if (TextBoxName.Text.Length > 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" and ");
                    }

                    sb.Append("name like '%" + TextBoxName.Text + "%'");
                }

                if( DropDownList1.SelectedValue.Length > 0)
                {
                    string text = DropDownList1.SelectedValue.ToString();
                    if (text != "Value 3")
                    {
                        if (sb.Length > 0)
                        {
                            sb.Append(" and ");
                        }
                        sb.Append("activity_name like '%" + text + "%'");
                    }
                }

                else
                {
                    dataView.RowFilter = sb.ToString();
                }
                dataView.RowFilter = sb.ToString();
             }

            if(dataView.Count == 0)
            {
                Div1.Visible = true;
                Label6.Text = "No such data found";
                LabelMsgError.Visible = true;
            }
            else
            {
                Div1.Visible = false;
                LinkButton2.Visible = false;
                LinkButton3.Visible = true;
                Session["SearchedResult"] = dataView;
                GridView1.DataSource = dataView;
                GridView1.DataBind();
                for (int i = 0; i < 5; i++)
                {
                    string lbText = ((LinkButton)GridView1.HeaderRow.Cells[i].Controls[0]).Text;

                    TableCell tableCell = GridView1.HeaderRow.Cells[i];
                    Image img = new Image();
                    img.ImageUrl = "~/Images/sortt.png";
                    img.CssClass = "imgg";
                    img.Width = 20;
                    img.Height = 20;
                    tableCell.Controls.Add(new LiteralControl("&nbsp;"));
                    tableCell.Controls.Add(img);
                    ViewState["tables"] = dt;
                }
            }
        }

        //Textbox for Group Number:
        protected void TextBoxGroupNumber_TextChanged(object sender, EventArgs e)
        {
            string question = TextBoxGroupNumber.Text;
            string question1 = TextBoxDate.Text;
            DataTable dt = this.GetData();
            DataView dataView = dt.DefaultView;
            if (!string.IsNullOrEmpty(question))
            {
                StringBuilder sb = new StringBuilder();

                if (TextBoxGroupNumber.Text.Length > 0)
                {
                    sb.Append("group_name like '%" + TextBoxGroupNumber.Text + "%'");
                }

                if (TextBoxDate.Text.Length > 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" and ");
                        string DateString = question1;
                        IFormatProvider culture = new CultureInfo("en-US", true);
                        DateTime dateVal = DateTime.ParseExact(DateString, "yyyy'-'MM'-'dd", culture);
                        var item = dateVal.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        sb.Append("activity_date like '%" + item + "%'");
                    }

                }


                if (TextBoxName.Text.Length > 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" and ");
                    }

                    sb.Append("name like '%" + TextBoxName.Text + "%'");
                }

                if (DropDownList1.SelectedValue.Length > 0)
                {

                    string text = DropDownList1.SelectedValue.ToString();
                    if (text != "Value 3")
                    {
                        if (sb.Length > 0)
                        {
                            sb.Append(" and ");
                        }
                        sb.Append("activity_name like '%" + text + "%'");
                    }

                }

                else
                {
                    dataView.RowFilter = sb.ToString();
                }
                dataView.RowFilter = sb.ToString();
            }

            if (dataView.Count == 0)
            {
                Div1.Visible = true;
                Label6.Text = "No such data found";
                LabelMsgError.Visible = true;
            }
            else
            {
                Div1.Visible = false;
                LinkButton2.Visible = false;
                LinkButton3.Visible = true;
                Session["SearchedResult"] = dataView;
                GridView1.DataSource = dataView;
                GridView1.DataBind();
                for (int i = 0; i < 5; i++)
                {
                    string lbText = ((LinkButton)GridView1.HeaderRow.Cells[i].Controls[0]).Text;

                    TableCell tableCell = GridView1.HeaderRow.Cells[i];
                    Image img = new Image();
                    img.ImageUrl = "~/Images/sortt.png";
                    img.CssClass = "imgg";
                    img.Width = 20;
                    img.Height = 20;
                    tableCell.Controls.Add(new LiteralControl("&nbsp;"));
                    tableCell.Controls.Add(img);
                    ViewState["tables"] = dt;
                }
            }
        }

       //Textbox for Participant's Name: 
        protected void TextBoxName_TextChanged(object sender, EventArgs e)
        {
            string question = TextBoxName.Text;
            string question1 = TextBoxDate.Text;
            DataTable dt = this.GetData();
            DataView dataView = dt.DefaultView;
            if (!string.IsNullOrEmpty(question))
            {
                StringBuilder sb = new StringBuilder();

                if (TextBoxName.Text.Length > 0)
                {
                    sb.Append("name like '%" + TextBoxName.Text + "%'");
                }

                if (TextBoxDate.Text.Length > 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" and ");
                        string DateString = question1;
                        IFormatProvider culture = new CultureInfo("en-US", true);
                        DateTime dateVal = DateTime.ParseExact(DateString, "yyyy'-'MM'-'dd", culture);
                        var item = dateVal.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        sb.Append("activity_date like '%" + item + "%'");
                    }
                }

                if (TextBoxGroupNumber.Text.Length > 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" and ");
                    }

                    sb.Append("group_name like '%" + TextBoxGroupNumber.Text + "%'");
                }

                if (DropDownList1.SelectedValue.Length > 0)
                {

                    string text = DropDownList1.SelectedValue.ToString();
                    if (text != "Value 3")
                    {
                        if (sb.Length > 0)
                        {
                            sb.Append(" and ");
                        }
                        sb.Append("activity_name like '%" + text + "%'");
                    }
                }

                else
                {
                    dataView.RowFilter = sb.ToString();
                }
                dataView.RowFilter = sb.ToString();
            }
            if (dataView.Count == 0)
            {
                Div1.Visible = true;
                Label6.Text = "No such data found";
                LabelMsgError.Visible = true;
            }
            else
            {
                Div1.Visible = false;
                LinkButton2.Visible = false;
                LinkButton3.Visible = true;
                Session["SearchedResult"] = dataView;
                GridView1.DataSource = dataView;
                GridView1.DataBind();
                for (int i = 0; i < 5; i++)
                {
                    string lbText = ((LinkButton)GridView1.HeaderRow.Cells[i].Controls[0]).Text;

                    TableCell tableCell = GridView1.HeaderRow.Cells[i];
                    Image img = new Image();
                    img.ImageUrl = "~/Images/sortt.png";
                    img.CssClass = "imgg";
                    img.Width = 20;
                    img.Height = 20;
                    tableCell.Controls.Add(new LiteralControl("&nbsp;"));
                    tableCell.Controls.Add(img);
                    ViewState["tables"] = dt;
                }
            }
        }

        //Dropdown list for the Activity Name
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string question = DropDownList1.Text;
            string question1 = TextBoxDate.Text;
            DataTable dt = this.GetData();
            DataView dataView = dt.DefaultView;
            if (!string.IsNullOrEmpty(question))
            {
                StringBuilder sb = new StringBuilder();

                if (DropDownList1.SelectedValue.Length > 0)
                {

                    string text = DropDownList1.SelectedValue.ToString();
                    if (text != "Value 3")
                    {
                        sb.Append("activity_name like '%" + text + "%'");
                    }
                }

                if (TextBoxDate.Text.Length > 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" and ");
                        string DateString = question1;
                        IFormatProvider culture = new CultureInfo("en-US", true);
                        DateTime dateVal = DateTime.ParseExact(DateString, "yyyy'-'MM'-'dd", culture);
                        var item = dateVal.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        sb.Append("activity_date like '%" + item + "%'");
                    }
                }

                if(TextBoxName.Text.Length > 0)
                {
                    if(sb.Length > 0)
                    {
                        sb.Append(" and ");
                    }
                    sb.Append("name like '%" + TextBoxName.Text + "%'");
                }

                if (TextBoxGroupNumber.Text.Length > 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" and ");
                    }
                    sb.Append("group_name like '%" + TextBoxGroupNumber.Text + "%'");
                }

                else
                {
                    dataView.RowFilter = sb.ToString();
                }
                dataView.RowFilter = sb.ToString();
            }

            if (dataView.Count == 0)
            {
                Div1.Visible = true;
                Label6.Text = "No such data found";
                LabelMsgError.Visible = true;
            }

            else
            {
                Div1.Visible = false;
                LinkButton2.Visible = false;
                LinkButton3.Visible = true;
                Session["SearchedResult"] = dataView;
                GridView1.DataSource = dataView;
                GridView1.DataBind();
                for (int i = 0; i < 5; i++)
                {
                    string lbText = ((LinkButton)GridView1.HeaderRow.Cells[i].Controls[0]).Text;

                    TableCell tableCell = GridView1.HeaderRow.Cells[i];
                    Image img = new Image();
                    img.ImageUrl = "~/Images/sortt.png";
                    img.CssClass = "imgg";
                    img.Width = 20;
                    img.Height = 20;
                    tableCell.Controls.Add(new LiteralControl("&nbsp;"));
                    tableCell.Controls.Add(img);
                    ViewState["tables"] = dt;
                }
            }
        }

        //Needed for the excel part
        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        //Export button to excel 
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string filename = "users.xls";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Response.ContentType = "application/vnd.msexcel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);

            if(Session["SortedResults"] != null)
            {
                GridView1.DataSource = Session["SortedResults"];
                GridView1.AllowSorting = true;
                GridView1.AllowPaging = true;
                GridView1.DataSource = this.GetData();
                GridView1.DataBind();

                GridView1.RenderControl(htw);
                string sTemp = sw.ToString();
                sTemp = sTemp.Replace("href=", "");

                Response.Write(sTemp);
                Response.End();
            }
            else
            {
                DataTable dt = this.GetData();
                GridView1.DataSource = dt;
                GridView1.AllowSorting = true;
                GridView1.AllowPaging = true;
                GridView1.PagerSettings.Visible = false;
                GridView1.DataSource = this.GetData();
                GridView1.DataBind();

                GridView1.RenderControl(htw);
                string sTemp1 = sw.ToString();
                sTemp1 = sTemp1.Replace("href=", "");
                Response.Write(sTemp1);
                Response.End();
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            string filename = "users.xls";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Response.ContentType = "application/vnd.msexcel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);

            DataTable dt = this.GetData();
            GridView1.DataSource = Session["SearchedResult"];
            GridView1.AllowSorting = true;
            GridView1.AllowPaging = false;
            GridView1.DataBind();

            GridView1.RenderControl(htw);
            string sTemp = sw.ToString();
            sTemp = sTemp.Replace("href=", "");
            Response.Write(sTemp);
            Response.End();
        }
    }
}