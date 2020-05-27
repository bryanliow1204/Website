using Newtonsoft.Json;
using SAF_Website.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace SAF_Website
{
    public partial class PreviewChecklistQuestionCreated : System.Web.UI.Page
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
            Label9.Text = Session["ChecklistTitle"].ToString();
            Session["titleOfchecklist"] = Label9.Text;
            Convert.ToInt32(Session["checkForChecklistId"]);

            DataTable dt = new DataTable();
            dt = this.GetData();
            GridView1.DataSource = this.GetData();
            GridView1.DataBind();

            for (int i = 0; i < 8; i++)
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
            IEnumerable<Checklist> listofChecklists = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44384/api/QuestionOption/");
            var response = hc.GetAsync("GetByIndividualChecklistDetails/" + Convert.ToInt32(Session["checkForChecklistId"]));
            response.Wait();

            var results = response.Result;
            DataTable dt = new DataTable();
            if (results.IsSuccessStatusCode)
            {
                var displayContent = results.Content.ReadAsStringAsync().Result;
                var resultcontents = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Checklist>>(displayContent);
                dt = ToDataTable(resultcontents);
            }
            return dt;
        }

        //store the view of each select inside the gridview into a session and display into another page
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridViewRow row = GridView1.SelectedRow;
            var label1 = ((System.Web.UI.WebControls.Label)GridView1.SelectedRow.Cells[0].FindControl("label1")).Text;
            var label2 = ((System.Web.UI.WebControls.Label)GridView1.SelectedRow.Cells[1].FindControl("label2")).Text;
            var label3 = ((System.Web.UI.WebControls.Label)GridView1.SelectedRow.Cells[2].FindControl("label3")).Text;
            var label4 = ((System.Web.UI.WebControls.Label)GridView1.SelectedRow.Cells[3].FindControl("label4")).Text;
            var label5 = ((System.Web.UI.WebControls.Label)GridView1.SelectedRow.Cells[4].FindControl("label5")).Text;
            var label6 = ((System.Web.UI.WebControls.Label)GridView1.SelectedRow.Cells[5].FindControl("label6")).Text;

            base.Session["QuestionID"] = label1;
            base.Session["Qname"] = label2;
            base.Session["FirstOpt"] = label3;
            base.Session["SecondOpt"] = label4;
            base.Session["ThirdOpt"] = label5;
            base.Session["FouthOpt"] = label6;
            Session["ChecklistTitle"] = Label9.Text;

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
                for (int i = 0; i < 8; i++)
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
                GridView1.DataSource = dataView;
                GridView1.DataBind();
                for (int i = 0; i < 8; i++)
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

        //Add Question Button
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Convert.ToInt32(Session["checkForChecklistId"]);
            Session["titleOfchecklist"] = Label9.Text;
            Response.Redirect("AddNewQuestion.aspx");
        }

        //Search Button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageChecklists.aspx");
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string message = "Do you want to delete this question?";
            string title = "Delete Prompt";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                System.Web.UI.WebControls.Label Label1 = GridView1.Rows[e.RowIndex].FindControl("Label1") as System.Web.UI.WebControls.Label;
                int id;
                int.TryParse(Label1.Text, out id);
                base.Session["question_ID"] = id;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44384/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.DeleteAsync($"api/ChecklistQuestion/DeleteChecklistQuestion/" + id).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        DataTable dt = new DataTable();
                        dt = this.GetData();
                        GridView1.DataSource = this.GetData();
                        GridView1.DataBind();

                        for (int i = 0; i < 8; i++)
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

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var Label1 = GridView1.Rows[e.RowIndex].FindControl("Label1") as System.Web.UI.WebControls.Label;
            var Label2 = GridView1.Rows[e.RowIndex].FindControl("Label2") as System.Web.UI.WebControls.Label;
            var Label3 = GridView1.Rows[e.RowIndex].FindControl("Label3") as System.Web.UI.WebControls.Label;
            var Label4 = GridView1.Rows[e.RowIndex].FindControl("Label4") as System.Web.UI.WebControls.Label;
            var Label5 = GridView1.Rows[e.RowIndex].FindControl("Label5") as System.Web.UI.WebControls.Label;
            var Label6 = GridView1.Rows[e.RowIndex].FindControl("Label6") as System.Web.UI.WebControls.Label;
            var Label7 = GridView1.Rows[e.RowIndex].FindControl("Label7") as System.Web.UI.WebControls.Label;
            var Label8 = GridView1.Rows[e.RowIndex].FindControl("Label8") as System.Web.UI.WebControls.Label;

            System.Web.UI.WebControls.Label label1 = new System.Web.UI.WebControls.Label() { Text = Label1.Text };
            System.Web.UI.WebControls.Label label2 = new System.Web.UI.WebControls.Label() { Text = Label2.Text };
            System.Web.UI.WebControls.Label label3 = new System.Web.UI.WebControls.Label() { Text = Label3.Text };
            System.Web.UI.WebControls.Label label4 = new System.Web.UI.WebControls.Label() { Text = Label4.Text };
            System.Web.UI.WebControls.Label label5 = new System.Web.UI.WebControls.Label() { Text = Label5.Text };
            System.Web.UI.WebControls.Label label6 = new System.Web.UI.WebControls.Label() { Text = Label6.Text };
            System.Web.UI.WebControls.Label label7 = new System.Web.UI.WebControls.Label() { Text = Label7.Text };
            System.Web.UI.WebControls.Label label8 = new System.Web.UI.WebControls.Label() { Text = Label8.Text };

            base.Session["question_no"] = label1.Text;
            base.Session["Qname"] = label2.Text;
            base.Session["type"] = label7.Text;
            base.Session["FirstOpt"] = label3.Text;
            base.Session["SecondOpt"] = label4.Text;
            base.Session["ThirdOpt"] = label5.Text;
            base.Session["FouthOpt"] = label6.Text;
            base.Session["threshold_no"] = label8.Text;
            Session["titleOfchecklist"].ToString();
            Convert.ToInt32(Session["checkForChecklistId"]);
            Convert.ToInt32(Session["question_no"]);

            Response.Redirect("EditQuestionFromPreviewChecklistCreated.aspx");
        }
    }
}
