using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace SAF_Website.BLL
{
    //Insert the Question
    public class InsertQuestion
    {

        [JsonProperty("checklist_id")]
        public int checklist_id { get; set; }

        [JsonProperty("question")]
        public string question { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("sequence_no")]
        public int sequence_no { get; set; }

     
    }

    //Insert the options for that question
    public class InsertQuestionOptions
    {
        [JsonProperty("question_id")]
        public int question_id { get; set; }

        [JsonProperty("option_one")]
        public string option_one { get; set; }

        [JsonProperty("option_two")]
        public string option_two { get; set; }

        [JsonProperty("option_three")]
        public string option_three { get; set; }

        [JsonProperty("option_four")]
        public string option_four { get; set; }

        [JsonProperty("threshold_flag")]
        public string threshold_flag { get; set; }
    }

    //Update Question Name
    public class UpdateQuestionName
    {
        [JsonProperty("question_id")]
        public int question_id { get; set; }

        [JsonProperty("question")]
        public string question { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }
    }

    //Update options for question
    public class UpdateQuestionOptions
    {
        [JsonProperty("question_id")]
        public int question_id { get; set; }

        [JsonProperty("option_one")]
        public string option_one { get; set; }

        [JsonProperty("option_two")]
        public string option_two { get; set; }

        [JsonProperty("option_three")]
        public string option_three { get; set; }

        [JsonProperty("option_four")]
        public string option_four { get; set; }

        [JsonProperty("threshold_flag")]
        public string threshold_flag { get; set; }
    }

    public class CheckChecklist
    {
        [JsonProperty("checklist_id")]
        public int checklist_id { get; set; }

        [JsonProperty("checklist_name")]
        public string checklist_name { get; set; }
    }

    public class CheckChecklistQuestionID
    {
        [JsonProperty("question_id")]
        public int question_id { get; set; }

        [JsonProperty("checklist_id")]
        public int checklist_id { get; set; }

        [JsonProperty("question")]
        public string question { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("sequence_no")]
        public int sequence_no { get; set; }
    }

    public class ListChecklist
    {
        public List<Checklist> CheckList { get; set; }
    }
    public class Checklist
    {
        [JsonProperty("question_id")]
        public int question_id { get; set; }

        [JsonProperty("question")]
        public string question { get; set; }

        [JsonProperty("option_one")]
        public string option_one { get; set; }

        [JsonProperty("option_two")]
        public string option_two { get; set; }

        [JsonProperty("option_three")]
        public string option_three { get; set; }

        [JsonProperty("option_four")]
        public string option_four { get; set; }

        [JsonProperty("threshold_flag")]
        public string threshold_flag { get; set; }


        [JsonProperty("type")]
        public string type { get; set; }
        // Default constructor
        public Checklist()
        {

        }

        public Checklist(int question_id, string question,string option_one, string option_two, string option_three, string option_four, string threshold_flag,string type)
        {
            this.question_id = question_id;
            this.question = question;
            this.option_one = option_one;
            this.option_two = option_two;
            this.option_three = option_three;
            this.option_four = option_four;
            this.threshold_flag = threshold_flag;
            this.type = type;
        }

        
    }
    
}