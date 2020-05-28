using OfficeOpenXml;
using SAF_Website.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAF_Website
{
    public partial class ViewIndividualChecklistResponses : System.Web.UI.Page
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

        //bind the datatable to the gridview
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
            Label9.Text = Session["nameofChecklist"].ToString();
            Session["nameofParticipant"].ToString();
            Session["dateofActivity"].ToString();

            DataTable dt = new DataTable();
            dt = this.GetData();
            GridView1.DataSource = this.GetData();
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
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        //populate the data into a datatable
        private DataTable GetData()
        {
            IEnumerable<ParticipantsResponses> detailsOfIndividualChecklistResponses = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44384/api/ChecklistResponses/");
            var response = hc.GetAsync("GetByIndividualChecklistResponses/" + Session["nameofParticipant"].ToString());
            response.Wait();

            var results = response.Result;
            DataTable dt = new DataTable();
            if (results.IsSuccessStatusCode)
            {
                var displayContent = results.Content.ReadAsStringAsync().Result;
                var resultcontents = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ParticipantsResponses>>(displayContent);
                dt = ToDataTable(resultcontents);
            }
            return dt;
        }


        //store the view of each select inside the gridview into a session and display into another page
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("ViewIndividual.aspx");
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
        }

        //Search the input inside the gridview
        protected void TextBoxChecklist_TextChanged(object sender, EventArgs e)
        {
            string question = TextBoxChecklist.Text;
            DataTable dt = this.GetData();
            DataView dataView = dt.DefaultView;
            if (!string.IsNullOrEmpty(question))
            {
                dataView.RowFilter = string.Format("question LIKE '{0}%' OR question LIKE '% {0}%'", TextBoxChecklist.Text);
            }
            if (dataView.Count == 0)
            {
                theMsg.Visible = true;
                LabelMsgError.Text = "No such data found";
                LabelMsgError.Visible = true;
            }
            else
            {
                theMsg.Visible = false;
                Session["SearchResult"] = dataView;
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

        //Export Question Button
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Session["nameofChecklist"].ToString();
            Session["nameofParticipant"].ToString();
            Session["dateofActivity"].ToString();

            string filename = Session["dateofActivity"].ToString() +"-" + Session["nameofChecklist"].ToString() +"-"+ Session["nameofParticipant"].ToString() +".xls";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Response.ContentType = "application/vnd.msexcel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);

            if(Session["SearchResult"] != null)
            {
                DataTable dt = this.GetData();
                GridView1.DataSource = Session["SearchResult"];
                GridView1.AllowSorting = true;
                GridView1.AllowPaging = true;
                GridView1.DataBind();

                GridView1.RenderControl(htw);
                string sTemp = sw.ToString();
               
                sTemp = sTemp.Replace("href=", "");
                Response.Write(sTemp);
                Response.End();
            }
            else
            {
                DataTable dt1 = this.GetData();
                GridView1.DataSource = dt1;
                GridView1.AllowSorting = true;
                GridView1.AllowPaging = true;
                GridView1.DataBind();

                GridView1.RenderControl(htw);
                string sTemp = sw.ToString();
                sTemp = sTemp.Replace("href=", "");
                Response.Write(sTemp);
                Response.End();
            }
        }

        //Search Button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchParticipant.aspx");
        }
    }
}