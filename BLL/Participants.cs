using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAF_Website.BLL
{
    public class ListParticipants
    {
        public List<Participants> Participants { get; set; }
    }
    public class Participants
    {
        [JsonProperty("activity_id")]
        public int activity_id { get; set; }

        [JsonProperty("activity_date")]
        public string activity_date { get; set; }

        [JsonProperty("activity_name")]
        public string activity_name { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("group_name")]
        public string group_name { get; set; }

        [JsonProperty("checklist_name")]
        public string checklist_name { get; set; }

        // Default constructor
        public Participants()
        {
            
        }

        public Participants(int id, string activitydate, string activityname, string name, string group, string checklistName)
        {
          
            activity_id = id;
            activity_date = activitydate;
            activity_name = activityname;
            this.name = name;
            group_name = group;
            checklist_name = checklistName;

        }

    }

}