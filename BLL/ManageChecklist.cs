using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAF_Website.BLL
{
    //Insert the checklist
    public class InsertChecklist
    {
        [JsonProperty("checklist_name")]
        public string checklist_name { get; set; }

        [JsonProperty("created_checklist")]
        public string created_checklist { get; set; }

        [JsonProperty("user_id")]
        public int user_id { get; set; }
    }

    public class UpdateChecklistName 
    {
        [JsonProperty("checklist_name")]
        public string checklist_name { get; set; }

        [JsonProperty("checklist_id")]
        public int checklist_id { get; set; }
    }

    public class getChecklistName
    {
        [JsonProperty("checklist_id")]
        public int checklist_id { get; set; }

        [JsonProperty("checklist_name")]
        public string checklist_name { get; set; }

        public getChecklistName(int checklist_id, string checklist_name)
        {
            this.checklist_id = checklist_id;
            this.checklist_name = checklist_name;
          
        }
    }

    public class ManageChecklist
    {
        [JsonProperty("checklist_id")]
        public int checklist_id { get; set; }

        [JsonProperty("checklist_name")]
        public string checklist_name { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("created_checklist")]
        public string created_checklist { get; set; }

    
        //Default Constructor
        public ManageChecklist()
        {

        }

        public ManageChecklist(int checklist_id, string checklist_name, string name, string created_checklist)
        {
            this.checklist_id = checklist_id;
            this.checklist_name = checklist_name;
            this.name = name;
            this.created_checklist = created_checklist;

        }
    }
}