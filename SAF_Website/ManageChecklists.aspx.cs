using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SAF_Website.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace SAF_Website
{
    public partial class Manage : System.Web.UI.Page
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

            for (int i = 0; i < 4; i++)
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
            Convert.ToInt32(Session["question_ID"]);
        }

        //validations
        private bool ValidateInput()
        {
            Label5.Text = String.Empty;
            Label5.ForeColor = System.Drawing.Color.Black;


            if (String.IsNullOrEmpty(TextBoxChecklist.Text))
            {
                Label5.Text += "Checklist field  is required!" + "<br/>";
            }

            if (String.IsNullOrEmpty(Label5.Text))
            {
                return true;
            }
            else
            {

                return false;
            }
        }

        //populate the data into a datatable
        private DataTable GetData()
        {
            IEnumerable<ManageChecklist> listofChecklists = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44384/api/Checklist/");
            var response = hc.GetAsync("GetAllChecklistDetails");
            response.Wait();

            var results = response.Result;
            DataTable dt = new DataTable();
            if (results.IsSuccessStatusCode)
            {
                var displayContent = results.Content.ReadAsStringAsync().Result;
                var resultcontents = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ManageChecklist>>(displayContent);
                dt = ToDataTable(resultcontents);
            }
            return dt;
        }

        //store the view of each select inside the gridview into a session and display into another page
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            var Label1 = ((System.Web.UI.WebControls.Label)GridView1.SelectedRow.Cells[0].FindControl("Label1")).Text;
            var Label2 = ((LinkButton)GridView1.SelectedRow.Cells[1].FindControl("EditButton")).Text;
            var Label3 = ((System.Web.UI.WebControls.Label)GridView1.SelectedRow.Cells[2].FindControl("Label3")).Text;
            var Label4 = ((System.Web.UI.WebControls.Label)GridView1.SelectedRow.Cells[3].FindControl("Label4")).Text;
         
            base.Session["question_id"] = Label1;
            base.Session["titleOfchecklist"] = Label2;
            base.Session["checkForChecklistId"] = Label1;

            Response.Redirect("PreviewChecklists.aspx");
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
                for (int i = 0; i < 4; i++)
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
                        //System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                        //// setting the dynamically URL of the image
                        //img.ImageUrl = "~/Images/" + (GridView1.SortDirection == SortDirection.Ascending ? "asc" : "desc") + ".png";
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

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateNewChecklist.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Div1.Visible = true;
            if (ValidateInput())
            {
                Div1.Visible = false;
            }
        }

      

        //Filter what the textbox input in gridview  
        protected void TextBoxChecklist_TextChanged(object sender, EventArgs e)
        {
            string question = TextBoxChecklist.Text;

            DataTable dt = this.GetData();
            DataView dataView = dt.DefaultView;
            if (!string.IsNullOrEmpty(question))
            {
                StringBuilder sb = new StringBuilder();

                if (TextBoxChecklist.Text.Length > 0)
                {
                    sb.Append("checklist_name like '%" + TextBoxChecklist.Text + "%'");
                }
                dataView.RowFilter = sb.ToString();
            }

            if (dataView.Count == 0)
            {
                theMsg.Visible = true;
                LabelMsgError.Text = "No such data found";
                LabelMsgError.ForeColor = System.Drawing.Color.Red;
                LabelMsgError.Visible = true;
            }

            else
            {
                theMsg.Visible = false;
                GridView1.DataSource = dataView;
                GridView1.DataBind();
                for (int i = 0; i < 4; i++)
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

        protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            string message = "Do you want to delete this checklist?";
            string title = "Delete Prompt";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                System.Web.UI.WebControls.Label Label1 = GridView1.Rows[e.RowIndex].FindControl("Label1") as System.Web.UI.WebControls.Label;
                int id;
                int.TryParse(Label1.Text, out id);
                base.Session["question_Id"] = id;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44384/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.DeleteAsync($"api/Checklist/DeleteIndividualChecklist/" + id).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        DataTable dt = new DataTable();
                        dt = this.GetData();
                        GridView1.DataSource = this.GetData();
                        GridView1.DataBind();

                        for (int i = 0; i < 4; i++)
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
                }
            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }
    }
}