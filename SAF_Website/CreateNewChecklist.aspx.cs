using SAF_Website.BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAF_Website
{
	public partial class CreateNewChecklist : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//Verify which user is logging in
			User registration = BLL.User.GetUsersOTP(base.Session["loggedInUsername"].ToString(), base.Session["loggedInOTP"].ToString());
			if (Session["loggedInUsername"] != null)
			{
				Master.linksearch.Visible = true;
				Master.linkmanage.Visible = true;
				Master.linklogin.Visible = false;
				Master.linklogout.Visible = true;
				Master.labeluser.Text = registration.name.ToString();

			}

			//Session passed from other pages
			Convert.ToInt32(Session["checkForChecklistId"]);
			Convert.ToInt32(Session["insertUserforchecklist"]);
			Session["bool"] = null;
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
				LabelQn.Visible = true;
				LabelQn2.Visible = true;
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
				//The message label visibility and the options 
				theMsg.Visible = false;
				LabelMsgError.Visible = false;
				LabelQn.Visible = false;
				LabelOption1.Visible = false;
				LabelOption2.Visible = false;
				LabelOption3.Visible = false;
				LabelOption4.Visible = false;

				return true;
			}

			else
			{
				if (TextBoxQuestion.Text == "" && TextBoxOption1.Text == "" && TextBoxOption2.Text == "")
				{
					//The message label visibility
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

		//Validations for radio buttons if checked
		private bool ValidateRadioButtons()
		{
			LabelOptionErrors.Text = String.Empty;

			if (RadioButtonOption1.Checked == false && RadioButtonOption2.Checked == false && RadioButtonOption3.Checked == false && RadioButtonOption4.Checked == false)
			{
				theRadioButtons.Visible = true;
				LabelMsgRadioButtons.Text = "Please choose one radio button for threshold!";
				return false;
			}

			return true;
		}

		protected void TextBoxTitle_TextChanged(object sender, EventArgs e)
		{
			Session["ChecklistTitle"] = TextBoxTitle.Text;
		}

		//Button for add questions
		protected void ButtonAdd_Click(object sender, EventArgs e)
		{
			int seq = 0;


			if (ValidateInputs() == true)
			{
				//Get the sequence number 
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
							if (item.sequence_no >= 1)
							{
								seq = item.sequence_no + 1;
							}

						}
					}
				}

				//Check if there is exsiting question in database before inserting
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
							if (item.question == TextBoxQuestion.Text)
							{
								theMsg.Visible = true;
								LabelMsgError.Text = "There is exisitng question in database! Cannot add!";
								LabelMsgError.Visible = true;
								Session["bool"] = "true";
							}
						}

						if (Session["bool"] == null)
						{
							if (ValidateInput() == true)
							{
								theMsgOptions.Visible = false;
								if (ValidateRadioButtons() == true)
								{
									// Inserting the question
									using (var client2 = new HttpClient())
									{
										client2.BaseAddress = new Uri("https://localhost:44384/api/ChecklistQuestion/");
										client2.DefaultRequestHeaders.Accept.Clear();
										client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
										if (seq == 0)
										{
											seq += 1;
										}

										var content2 = new InsertQuestion() { checklist_id = Convert.ToInt32(Session["checkForChecklistId"]), question = TextBoxQuestion.Text, type = Session["type"].ToString(), sequence_no = seq };
										var response2 = client2.PostAsJsonAsync("AddChecklistQuestion", content2).Result;

										if (response2.IsSuccessStatusCode)
										{
											string responseresult2 = response2.Content.ReadAsStringAsync().Result;
										}
									}

									//Get the question id
									using (var client3 = new HttpClient())
									{
										client3.BaseAddress = new Uri("https://localhost:44384/api/ChecklistQuestion/");
										client3.DefaultRequestHeaders.Accept.Clear();
										client3.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
										var response3 = client3.GetAsync("GetLastQuestionID").Result;

										if (response3.IsSuccessStatusCode)
										{
											string responseresult3 = response3.Content.ReadAsStringAsync().Result;
											var results3 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CheckChecklistQuestionID>>(responseresult3);
											foreach (var item3 in results3)
											{
												Session["checkForQuestionId"] = item3.question_id;

											}
										}
									}

									//Inserting the options to the question id
									using (var client4 = new HttpClient())
									{
										client4.BaseAddress = new Uri("https://localhost:44384/api/QuestionOption/");
										client4.DefaultRequestHeaders.Accept.Clear();
										client4.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));



										var content4 = new InsertQuestionOptions() { question_id = Convert.ToInt32(Session["checkForQuestionId"]), option_one = TextBoxOption1.Text, option_two = TextBoxOption2.Text, option_three = TextBoxOption3.Text, option_four = TextBoxOption4.Text, threshold_flag = Session["threshold_value"].ToString() };

										var response4 = client4.PostAsJsonAsync("AddOption", content4).Result;

										if (response4.IsSuccessStatusCode)
										{
											string responseresult4 = response4.Content.ReadAsStringAsync().Result;
										}
										//Reset the textbox to empty string and the droplist selected index
										TextBoxQuestion.Text = "";
										TextBoxOption1.Text = "";
										TextBoxOption2.Text = "";
										TextBoxOption3.Text = "";
										TextBoxOption4.Text = "";
										DropDownList1.SelectedIndex = 0;
										RadioButtonOption1.Checked = false;
										RadioButtonOption2.Checked = false;
										RadioButtonOption3.Checked = false;
										RadioButtonOption4.Checked = false;

										//Set the visibility of the options to default as index is 0
										divOpt1.Visible = false;
										divOpt2.Visible = false;
										divOpt3.Visible = false;
										divOpt4.Visible = false;

										//Set the second textbox title to true and the sucess msg label
										Successlabel.Visible = true;
										ButtonPreview.Visible = true;
										theRadioButtons.Visible = false;
									}
								}

							}
						}

					}
				}
			}
		}

		//Button for preview the checklist
		protected void ButtonPreview_Click(object sender, EventArgs e)
		{
			//Pass in the session to the next page
			Session["ChecklistTitle"] = TextBoxTitle.Text;
			Convert.ToInt32(Session["checkForChecklistId"]);
			Response.Redirect("PreviewChecklistQuestionCreated.aspx");
		}

		//Button to save 
		protected void LinkButtonSave_Click(object sender, EventArgs e)
		{
			if (TextBoxTitle.Text == "")
			{
				theMsg.Visible = true;
				LabelMsgError.Text = "Please fill in the checklist field !";
				LabelMsgError.Visible = true;
			}
			else
			{
				theMsg.Visible = false;
				LabelMsgError.Visible = false;
				string checklistname = TextBoxTitle.Text;
				var today = DateTime.Today.ToString("dd-MMM-yyyy");

				//Get the user details 
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("https://localhost:44384/api/User/");
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

					var getUsername = client.GetAsync($"GetByEmail/" + Session["loggedInUsername"] + "/").Result;

					if (getUsername.IsSuccessStatusCode)
					{
						string responseresult = getUsername.Content.ReadAsStringAsync().Result;
						var results = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(responseresult);
						foreach (var item in results)
						{
							Session["insertUserforchecklist"] = item.user_id;
						}
					}
				}

				//Check if such checklist exist in database
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("https://localhost:44384/api/Checklist/");
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

					var response = client.GetAsync("GetByChecklistName/" + TextBoxTitle.Text).Result;

					if (response.IsSuccessStatusCode)
					{
						string responseresult = response.Content.ReadAsStringAsync().Result;
						var results = Newtonsoft.Json.JsonConvert.DeserializeObject<List<getChecklistName>>(responseresult);
						foreach (var item in results)
						{
							if (item.checklist_name == TextBoxTitle.Text)
							{
								theMsg.Visible = true;
								LabelMsgError.Text = "There is exisitng checklist in database! Cannot save!";
								LabelMsgError.Visible = true;
							}

						}
						if (results.Count == 0)
						{
							//Update the checklist insert 
							using (var client2 = new HttpClient())
							{
								client2.BaseAddress = new Uri("https://localhost:44384/api/Checklist/");
								client2.DefaultRequestHeaders.Accept.Clear();
								client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

								var sys = new InsertChecklist() { checklist_name = checklistname, created_checklist = today, user_id = Convert.ToInt32(Session["insertUserforchecklist"]) };

								var response2 = client2.PostAsJsonAsync("AddChecklist", sys).Result;

								if (response2.IsSuccessStatusCode)
								{
									string responseresul2t = response2.Content.ReadAsStringAsync().Result;
								}
							}

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
										Session["checkForChecklistId"] = item.checklist_id;
									}
								}
							}
							table_section.Visible = true;
							ButtonAdd.Visible = true;
							TextBoxTitle.ReadOnly = true;
							LinkButtonEdit.Visible = true;
							LinkButtonSave.Visible = false;
						}
					}
				}
			}
		}

		//First edit button
		protected void LinkButtonEdit_Click(object sender, EventArgs e)
		{
			TextBoxTitle.ReadOnly = false;
			LinkButtonEdit.Visible = false;
			LinkButtonEdit2.Visible = true;
			LinkButtonCancel.Visible = true;
		}

		//Second edit button
		protected void LinkButtonEdit2_Click(object sender, EventArgs e)
		{

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
						Session["checkForChecklistName"] = item.checklist_name;
					}
				}
			}

			//Get the list of checklist name if got existing name cannot update to that name
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://localhost:44384/api/Checklist/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

				var response = client.GetAsync("GetByChecklistName/" + TextBoxTitle.Text).Result;

				if (response.IsSuccessStatusCode)
				{
					string responseresult = response.Content.ReadAsStringAsync().Result;
					var results = Newtonsoft.Json.JsonConvert.DeserializeObject<List<getChecklistName>>(responseresult);
					foreach (var item in results)
					{
						if (item.checklist_name == TextBoxTitle.Text)
						{
							theMsg.Visible = true;
							LabelMsgError.Text = "There is exisitng checklist in database! Cannot update!";
							LabelMsgError.Visible = true;
							TextBoxTitle.Text = Session["checkForChecklistName"].ToString();
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

							var content = new UpdateChecklistName() { checklist_name = TextBoxTitle.Text, checklist_id = Convert.ToInt32(Session["checkForChecklistId"]) };

							var response2 = client2.PutAsJsonAsync("UpdateChecklist", content).Result;

							if (response2.IsSuccessStatusCode)
							{
								string responseresult2 = response.Content.ReadAsStringAsync().Result;
								var results2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UpdateChecklistName>>(responseresult2);
							}
						}
						TextBoxTitle.ReadOnly = true;
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

		//Cancel button
		protected void LinkButtonCancel_Click(object sender, EventArgs e)
		{
			TextBoxTitle.ReadOnly = true;
			LinkButtonCancel.Visible = false;
			LinkButtonEdit.Visible = true;
			LinkButtonEdit2.Visible = false;
			theMsg.Visible = false;
		}

		protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (DropDownList1.SelectedIndex == 0)
			{
				divOpt1.Visible = false;
				divOpt2.Visible = false;
				divOpt3.Visible = false;
				divOpt4.Visible = false;
				Successlabel.Visible = false;
				theMsg.Visible = false;
			}

			if (DropDownList1.SelectedIndex == 1)
			{
				Session["type"] = DropDownList1.SelectedValue;
				divOpt1.Visible = true;
				divOpt2.Visible = true;
				divOpt3.Visible = true;
				divOpt4.Visible = true;
				Successlabel.Visible = false;
				theMsg.Visible = false;
			}
			if (DropDownList1.SelectedIndex == 2)
			{
				Session["type"] = DropDownList1.SelectedValue;
				divOpt1.Visible = true;
				divOpt2.Visible = true;
				divOpt3.Visible = true;
				divOpt4.Visible = false;
				Successlabel.Visible = false;
				theMsg.Visible = false;
			}
			if (DropDownList1.SelectedIndex == 3)
			{
				Session["type"] = DropDownList1.SelectedValue;
				divOpt1.Visible = true;
				divOpt2.Visible = true;
				divOpt3.Visible = false;
				divOpt4.Visible = false;
				Successlabel.Visible = false;
				theMsg.Visible = false;
			}

			if (DropDownList1.SelectedIndex == 4)
			{
				Session["type"] = DropDownList1.SelectedValue;
				divOpt1.Visible = true;
				divOpt2.Visible = true;
				divOpt3.Visible = true;
				divOpt4.Visible = true;
				Successlabel.Visible = false;
				theMsg.Visible = false;
			}
		}

		protected void RadioButtonOption1_CheckedChanged1(object sender, EventArgs e)
		{
			if (RadioButtonOption1.Checked == true)
			{
				var threshold = "1";
				RadioButtonOption1.Checked = true;
				Session["threshold_value"] = threshold;
				theRadioButtons.Visible = false;
				theMsgOptions.Visible = false;
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
				theRadioButtons.Visible = false;
				theMsgOptions.Visible = false;
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
				theRadioButtons.Visible = false;
				theMsgOptions.Visible = false;
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
				theRadioButtons.Visible = false;
				theMsgOptions.Visible = false;
			}
			else
			{
				RadioButtonOption4.Checked = false;
			}
		}
	}
}