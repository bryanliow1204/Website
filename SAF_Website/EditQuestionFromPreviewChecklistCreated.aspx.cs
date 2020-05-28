using SAF_Website.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAF_Website
{
    public partial class EditQuestionFromPreviewChecklistCreated : System.Web.UI.Page
    {
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
			Session["threshold_radiobutton1"] = "1";
			Session["threshold_radiobutton2"] = "2";
			Session["threshold_radiobutton3"] = "3";
			Session["threshold_radiobutton4"] = "4";

			Session["titleOfchecklist"].ToString();
			Convert.ToInt32(Session["checkForChecklistId"]);
			Session["bool"] = null;
			if (!IsPostBack)
			{
				TextBoxTitle.Text = base.Session["titleOfchecklist"].ToString();
				Convert.ToInt32(Session["question_no"]);
				TextBoxQuestion.Text = base.Session["Qname"].ToString();
				DropDownList1.Text = base.Session["type"].ToString();
				TextBoxOption1.Text = base.Session["FirstOpt"].ToString();
				TextBoxOption2.Text = base.Session["SecondOpt"].ToString();
				TextBoxOption3.Text = base.Session["ThirdOpt"].ToString();
				TextBoxOption4.Text = base.Session["FouthOpt"].ToString();
				Session["threshold_no"].ToString();

				//check the radio button 
				if (RadioButtonOption1.Checked == false)
				{
					if (Session["threshold_no"].ToString() == Session["threshold_radiobutton1"].ToString())
					{
						RadioButtonOption1.Checked = true;
						Session["threshold_value"] = Session["threshold_no"].ToString();
					}
				}
				if (RadioButtonOption2.Checked == false)
				{
					if (Session["threshold_no"].ToString() == Session["threshold_radiobutton2"].ToString())
					{
						RadioButtonOption2.Checked = true;
						Session["threshold_value"] = Session["threshold_no"].ToString();
					}
				}
				if (RadioButtonOption3.Checked == false)
				{
					if (Session["threshold_no"].ToString() == Session["threshold_radiobutton3"].ToString())
					{
						RadioButtonOption3.Checked = true;
						Session["threshold_value"] = Session["threshold_no"].ToString();
					}
				}
				if (RadioButtonOption4.Checked == false)
				{
					if (Session["threshold_no"].ToString() == Session["threshold_radiobutton4"].ToString())
					{
						RadioButtonOption4.Checked = true;
						Session["threshold_value"] = Session["threshold_no"].ToString();
					}
				}
			}

			//check the dropdownlist value
			if (DropDownList1.SelectedValue == "MCQ")
			{
				divOpt1.Visible = true;
				divOpt2.Visible = true;
				divOpt3.Visible = true;
				divOpt4.Visible = true;
			}
			if (DropDownList1.SelectedValue == "Slider")
			{
				divOpt1.Visible = true;
				divOpt2.Visible = true;
				divOpt3.Visible = true;
				divOpt4.Visible = false;
			}
			if (DropDownList1.SelectedValue == "Short Question")
			{
				divOpt1.Visible = true;
				divOpt2.Visible = true;
				divOpt3.Visible = false;
				divOpt4.Visible = false;
			}
			if (DropDownList1.SelectedValue == "Checkbox")
			{
				divOpt1.Visible = true;
				divOpt2.Visible = true;
				divOpt3.Visible = true;
				divOpt4.Visible = true;
			}

			if (TextBoxTitle2.Text != "")
			{
				TextBoxTitle.Visible = false;
			}

			else
			{
				TextBoxTitle.Text = Session["titleOfchecklist"].ToString();
				TextBoxTitle2.Visible = false;
				TextBoxTitle2.Text = Session["titleOfchecklist"].ToString();
			}
			TextBoxTitle.ReadOnly = true;
		}

		//Validations
		private bool ValidateInputs()
		{
			if (TextBoxTitle.Text == "")
			{
				theMsg.Visible = true;
				LabelMsgError.Text = "Checkfield field required!";
				LabelQn.Visible = true;
			}

			if (TextBoxQuestion.Text == "")
			{
				LabelQn.Text = "*";
				LabelQn2.Visible = true;
				LabelQn.Visible = true;
			}

			if (TextBoxOption1.Text == "")
			{
				LabelOption1.Text = "*";
				LabelOption1.Visible = true;
				LabelOptionMsg.Visible = true;
			}

			if (TextBoxOption2.Text == "")
			{
				LabelOption2.Text = "*";
				LabelOption2.Visible = true;
				LabelOption2Msg.Visible = true;
			}

			if (TextBoxTitle.Text != "")
			{
				LabelMsgError.Visible = false;
				theMsg.Visible = false;
			}

			if (TextBoxQuestion.Text != "")
			{

				LabelQn.Visible = false;
				LabelQn2.Visible = false;
			}

			if (TextBoxOption1.Text != "")
			{

				LabelOption1.Visible = false;
				LabelOptionMsg.Visible = false;
			}

			if (TextBoxOption2.Text != "")
			{
				LabelOption2.Visible = false;
				LabelOption2Msg.Visible = false;
			}

			if (TextBoxQuestion.Text != "" && TextBoxOption1.Text != "" && TextBoxOption2.Text != "")
			{
				theMsg.Visible = false;
				LabelMsgError.Visible = false;
				LabelQn.Visible = false;
				LabelOption1.Visible = false;
				LabelOption2.Visible = false;

				return true;
			}

			else
			{
				if (TextBoxQuestion.Text == "" && TextBoxOption1.Text == "" && TextBoxOption2.Text == "")
				{
					Successlabel.Visible = false;
					theMsg.Visible = false;
					LabelQn.Visible = true;

					LabelQn.Text = "*";
					LabelQn.Visible = true;
					LabelQn2.Visible = true;


					LabelOption1.Text = "*";
					LabelOption1.Visible = true;

					LabelOption2.Text = "*";
					LabelOption2.Visible = true;

					return false;
				}
			}
			return false;
		}

		//Validations for options
		private bool ValidateInput()
		{
			LabelOptionErrors.Text = String.Empty;
			if (TextBoxOption1.Text == TextBoxOption2.Text || TextBoxOption1.Text == TextBoxOption3.Text || TextBoxOption1.Text == TextBoxOption4.Text)
			{
				theMsgOptions.Visible = true;
				LabelOptionErrors.Text = "Cannot have same options for the question!";
				return false;
			}

			if (TextBoxOption2.Text == TextBoxOption1.Text || TextBoxOption2.Text == TextBoxOption3.Text || TextBoxOption1.Text == TextBoxOption4.Text)
			{
				theMsgOptions.Visible = true;
				LabelOptionErrors.Text = "Cannot have same options for the question!";
				return false;
			}

			if (TextBoxOption3.Text == TextBoxOption1.Text || TextBoxOption3.Text == TextBoxOption2.Text || TextBoxOption3.Text == TextBoxOption4.Text)
			{
				if (String.IsNullOrEmpty(TextBoxOption3.Text) && String.IsNullOrEmpty(TextBoxOption4.Text))
				{
					if (TextBoxOption3.Text == TextBoxOption1.Text || TextBoxOption3.Text == TextBoxOption2.Text)
					{
						theMsgOptions.Visible = true;
						LabelOptionErrors.Text = "Cannot have same options for the question!";
						return false;
					}

				}
			}

			if (TextBoxOption4.Text == TextBoxOption1.Text || TextBoxOption4.Text == TextBoxOption2.Text || TextBoxOption4.Text == TextBoxOption3.Text)
			{
				if (String.IsNullOrEmpty(TextBoxOption3.Text) && String.IsNullOrEmpty(TextBoxOption4.Text))
				{
					if (TextBoxOption4.Text == TextBoxOption1.Text || TextBoxOption4.Text == TextBoxOption2.Text)
					{
						theMsgOptions.Visible = true;
						LabelOptionErrors.Text = "Cannot have same options for the question!";
						return false;
					}
				}
			}

			return true;

		}

		protected void TextBoxTitle_TextChanged(object sender, EventArgs e)
		{
		}

		//Button for update questions
		protected void ButtonUpdate_Click(object sender, EventArgs e)
		{
			if (ValidateInputs() == true)
			{
				//check if there is exsiting question before updating 
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("https://localhost:44384/api/ChecklistQuestion/");
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

					var response = client.GetAsync("GetByChecklistID/" + Convert.ToInt32(Session["checkForChecklistId"])).Result;
					if (response.IsSuccessStatusCode)
					{
						string responseresult = response.Content.ReadAsStringAsync().Result;
						var results = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CheckChecklistQuestionID>>(responseresult);
						foreach (var item in results)
						{
							if (item.question_id == Convert.ToInt32(Session["question_no"]))
							{
								if (item.question == TextBoxQuestion.Text)
								{
									Session["bool"] = "true";
									theMsgQn.Visible = false;
								}
								if (item.question != TextBoxQuestion.Text)
								{
									string text = TextBoxQuestion.Text;
									string final1 = text.Replace(" ", "%20");
									string final2 = final1.Replace("?", "%3F");
									string final3 = final2.Replace("!", "%21");


									using (var client10 = new HttpClient())
									{
										client10.BaseAddress = new Uri("https://localhost:44384/api/ChecklistQuestion/");
										client10.DefaultRequestHeaders.Accept.Clear();
										client10.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

										var response10 = client10.GetAsync("GetByQuestionName/?id=" + Convert.ToInt32(Session["checkForChecklistId"]) + "&question=" + final3).Result;
										if (response10.IsSuccessStatusCode)
										{
											string responseresult10 = response10.Content.ReadAsStringAsync().Result;
											var results10 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CheckChecklistQuestionID>>(responseresult10);
											if (results10.Count == 1)
											{
												theMsgQn.Visible = true;
												LabelQuestionErrors.Text = "Cannot update question as there is existing question in the database.";
											}
											if (results10.Count == 0)
											{
												theMsgQn.Visible = false;
												Session["bool"] = "true";
											}
										}
									}
								}
							}
						}
						if (Session["bool"] == "true")
						{
							if (ValidateInput() == true)
							{
								//update the question name
								using (var client2 = new HttpClient())
								{
									client2.BaseAddress = new Uri("https://localhost:44384/api/ChecklistQuestion/");
									client2.DefaultRequestHeaders.Accept.Clear();
									client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

									var content2 = new UpdateQuestionName() { question_id = Convert.ToInt32(Session["question_no"]), question = TextBoxQuestion.Text, type = Session["type"].ToString() };

									var response2 = client2.PutAsJsonAsync("UpdateQuestion", content2).Result;
									if (response2.IsSuccessStatusCode)
									{

									}
								}

								//update the options
								using (var client3 = new HttpClient())
								{
									client3.BaseAddress = new Uri("https://localhost:44384/api/QuestionOption/");
									client3.DefaultRequestHeaders.Accept.Clear();
									client3.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

									var content3 = new UpdateQuestionOptions() { question_id = Convert.ToInt32(Session["question_no"]), option_one = TextBoxOption1.Text, option_two = TextBoxOption2.Text, option_three = TextBoxOption3.Text, option_four = TextBoxOption4.Text, threshold_flag = Session["threshold_value"].ToString() };

									var response3 = client3.PutAsJsonAsync("UpdateOptions", content3).Result;
									if (response3.IsSuccessStatusCode)
									{

									}
								}
								Successlabel.Visible = true;
								Response.AddHeader("REFRESH", "2;URL=PreviewChecklists.aspx");
								theMsgOptions.Visible = false;
							}
						}
					}
				}
			}
		}

		protected void LinkButtonEdit_Click(object sender, EventArgs e)
		{
			//The readibility
			TextBoxTitle.ReadOnly = false;
			TextBoxTitle2.ReadOnly = false;

			//The visibility
			LinkButtonEdit.Visible = false;
			LinkButtonEdit2.Visible = true;
			TextBoxTitle.Visible = false;
			TextBoxTitle2.Visible = true;
			Successlabel.Visible = false;
			LinkButtonCancel.Visible = true;
		}

		protected void LinkButtonEdit2_Click(object sender, EventArgs e)
		{
			TextBoxTitle.ReadOnly = true;
			TextBoxTitle2.ReadOnly = true;
			LinkButtonEdit.Visible = true;
			LinkButtonEdit2.Visible = false;
			TextBoxTitle2.Visible = true;
			TextBoxTitle.Visible = false;

			Session["titleOfThechecklist"] = TextBoxTitle2.Text;

			//Check if such checklist exist in database
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://localhost:44384/api/Checklist/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

				var response = client.GetAsync("GetByChecklistName/" + TextBoxTitle2.Text).Result;

				if (response.IsSuccessStatusCode)
				{
					string responseresult = response.Content.ReadAsStringAsync().Result;
					var results = Newtonsoft.Json.JsonConvert.DeserializeObject<List<getChecklistName>>(responseresult);
					foreach (var item in results)
					{
						if (item.checklist_name == TextBoxTitle2.Text)
						{
							theMsg.Visible = true;
							LabelMsgError.Text = "There is exisitng checklist in database! Cannot update!";
							LabelMsgError.Visible = true;
							TextBoxTitle2.Text = Session["titleOfThechecklist"].ToString();
							TextBoxTitle2.ReadOnly = false;
							LinkButtonEdit2.Visible = true;
							LinkButtonEdit.Visible = false;
						}
					}

					if (results.Count == 0)
					{
						//Update the checklistName
						using (var client2 = new HttpClient())
						{
							client2.BaseAddress = new Uri("https://localhost:44384/api/Checklist/");
							client2.DefaultRequestHeaders.Accept.Clear();
							client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

							var content = new UpdateChecklistName() { checklist_name = TextBoxTitle2.Text, checklist_id = Convert.ToInt32(Session["checkForChecklistId"]) };

							var response2 = client2.PutAsJsonAsync("UpdateChecklist", content).Result;

							if (response2.IsSuccessStatusCode)
							{
								string responseresult2 = response2.Content.ReadAsStringAsync().Result;
							}
							TextBoxTitle2.ReadOnly = true;
							LinkButtonEdit.Visible = true;
							LinkButtonEdit2.Visible = false;
							LinkButtonCancel.Visible = false;
							Successlabel.Visible = false;
							theMsg.Visible = false;
							LabelMsgError.Visible = false;
						}
					}
				}
			}
		}

		protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (DropDownList1.SelectedIndex == 0)
			{
				divOpt1.Visible = false;
				divOpt2.Visible = false;
				divOpt3.Visible = false;
				divOpt4.Visible = false;
				TextBoxTitle2.Visible = true;
				TextBoxTitle.Visible = false;
				TextBoxTitle2.ReadOnly = true;
				Successlabel.Visible = false;
				theMsgOptions.Visible = false;
				TextBoxOption1.Text = "";
				TextBoxOption2.Text = "";
				TextBoxOption3.Text = "";
				TextBoxOption4.Text = "";
			}

			if (DropDownList1.SelectedIndex == 1)
			{
				Session["type"] = DropDownList1.SelectedValue;
				divOpt1.Visible = true;
				divOpt2.Visible = true;
				divOpt3.Visible = true;
				divOpt4.Visible = true;
				TextBoxTitle2.Visible = true;
				TextBoxTitle.Visible = false;
				TextBoxTitle2.ReadOnly = true;
				Successlabel.Visible = false;
				theMsgOptions.Visible = false;
			}

			if (DropDownList1.SelectedIndex == 2)
			{
				Session["type"] = DropDownList1.SelectedValue;
				divOpt1.Visible = true;
				divOpt2.Visible = true;
				divOpt3.Visible = true;
				divOpt4.Visible = false;
				TextBoxTitle2.Visible = true;
				TextBoxTitle.Visible = false;
				TextBoxTitle2.ReadOnly = true;
				Successlabel.Visible = false;
				theMsgOptions.Visible = false;
				TextBoxOption4.Text = "";
			}

			if (DropDownList1.SelectedIndex == 3)
			{
				Session["type"] = DropDownList1.SelectedValue;
				divOpt1.Visible = true;
				divOpt2.Visible = true;
				divOpt3.Visible = false;
				divOpt4.Visible = false;
				TextBoxTitle2.Visible = true;
				TextBoxTitle.Visible = false;
				TextBoxTitle2.ReadOnly = true;
				Successlabel.Visible = false;
				theMsgOptions.Visible = false;
				TextBoxOption3.Text = "";
				TextBoxOption4.Text = "";
			}

			if (DropDownList1.SelectedIndex == 4)
			{
				Session["type"] = DropDownList1.SelectedValue;
				divOpt1.Visible = true;
				divOpt2.Visible = true;
				divOpt3.Visible = true;
				divOpt4.Visible = true;
				TextBoxTitle.Visible = false;
				Successlabel.Visible = false;
				theMsgOptions.Visible = false;
			}
		}

		protected void LinkButtonCancel_Click(object sender, EventArgs e)
		{
			TextBoxTitle2.ReadOnly = true;
			LinkButtonCancel.Visible = false;
			LinkButtonEdit.Visible = true;
			LinkButtonEdit2.Visible = false;
			theMsg.Visible = false;

			//Get the last checklist ID
			using (var client3 = new HttpClient())
			{
				client3.BaseAddress = new Uri("https://localhost:44384/api/Checklist/");
				client3.DefaultRequestHeaders.Accept.Clear();
				client3.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

				var response3 = client3.GetAsync("GetLastChecklistID").Result;

				if (response3.IsSuccessStatusCode)
				{
					string responseresult3 = response3.Content.ReadAsStringAsync().Result;
					var results2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CheckChecklist>>(responseresult3);
					foreach (var item in results2)
					{
						Session["lastChecklistName"] = item.checklist_name;
					}
				}
			}
			TextBoxTitle2.Text = Session["lastChecklistName"].ToString();
		}

		protected void RadioButtonOption1_CheckedChanged(object sender, EventArgs e)
		{

			if (RadioButtonOption1.Checked == true)
			{
				var threshold = "1";
				RadioButtonOption1.Checked = true;
				Session["threshold_value"] = threshold;
				theMsgOptions.Visible = false;
				theMsg.Visible = false;
				TextBoxTitle2.Visible = true;
				TextBoxTitle2.ReadOnly = true;
			}
			else
			{
				RadioButtonOption1.Checked = false;
			}
		}

		protected void RadioButtonOption2_CheckedChanged(object sender, EventArgs e)
		{
			if (RadioButtonOption2.Checked == true)
			{
				var threshold = "2";
				RadioButtonOption1.Checked = true;
				Session["threshold_value"] = threshold;
				theMsgOptions.Visible = false;
				theMsg.Visible = false;
				TextBoxTitle2.Visible = true;
				TextBoxTitle2.ReadOnly = true;
			}
			else
			{
				RadioButtonOption2.Checked = false;
			}
		}

		protected void RadioButtonOption3_CheckedChanged(object sender, EventArgs e)
		{
			if (RadioButtonOption3.Checked == true)
			{
				var threshold = "3";
				RadioButtonOption3.Checked = true;
				Session["threshold_value"] = threshold;
				theMsgOptions.Visible = false;
				theMsg.Visible = false;
				TextBoxTitle2.Visible = true;
				TextBoxTitle2.ReadOnly = true;
			}
			else
			{
				RadioButtonOption3.Checked = false;
			}
		}

		protected void RadioButtonOption4_CheckedChanged(object sender, EventArgs e)
		{
			if (RadioButtonOption4.Checked == true)
			{
				var threshold = "4";
				RadioButtonOption1.Checked = true;
				Session["threshold_value"] = threshold;
				theMsgOptions.Visible = false;
				theMsg.Visible = false;
				TextBoxTitle2.Visible = true;
				TextBoxTitle2.ReadOnly = true;
			}
			else
			{
				RadioButtonOption4.Checked = false;
			}
		}

		protected void ButtonBack_Click(object sender, EventArgs e)
		{
			Response.Redirect("PreviewChecklistQuestionCreated.aspx");
		}
	}
}