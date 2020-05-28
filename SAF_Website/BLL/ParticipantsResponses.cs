using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAF_Website.BLL
{
    public class ParticipantsResponses
    {
        [JsonProperty("user_activity_checklist_response_id")]
        public int user_activity_checklist_response_id { get; set; }

        [JsonProperty("question")]
        public string question { get; set; }

        [JsonProperty("chosen_option")]
        public string chosen_option { get; set; }

        [JsonProperty("sequence_no")]
        public int sequence_no { get; set; }

        [JsonProperty("threshold_flag")]
        public string threshold_flag { get; set; }


        public ParticipantsResponses()
        {

        }

        public ParticipantsResponses(int user_activity_checklist_response_id, string question , string chosen_option, int sequence_no,string threshold_flag)
        {
            this.user_activity_checklist_response_id = user_activity_checklist_response_id;
            this.question = question;
            this.chosen_option = chosen_option;
            this.sequence_no = sequence_no;
            this.threshold_flag = threshold_flag;

        }
    }
}