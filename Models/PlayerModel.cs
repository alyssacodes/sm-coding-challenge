using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace sm_coding_challenge.Models
{
    [DataContract]
    public class PlayerModel
    {
        [DataMember(Name = "player_id")]
        [JsonProperty(Order = -2)]
        public string Id { get; set; }

        [DataMember(Name = "entry_id")]
        public string EntryId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "position")]
        public string Position { get; set; }
    }    

    public class RushingModel : PlayerModel
    {
        [DataMember(Name = "yds")]
        public int Yds { get; set; }

        [DataMember(Name = "att")]
        public int Att { get; set; }

        [DataMember(Name = "tds")]
        public int Tds { get; set; }

        [DataMember(Name = "fum")]
        public int Fum { get; set; }
    }

    public class PassingModel : PlayerModel
    {
        [DataMember(Name = "yds")]
        public int Yds { get; set; }

        [DataMember(Name = "att")]
        public int Att { get; set; }

        [DataMember(Name = "tds")]
        public int Tds { get; set; }

        [DataMember(Name = "cmp")]
        public int Cmp { get; set; }

        [DataMember(Name = "int")]
        public int Int { get; set; }
    }

    public class ReceivingModel : PlayerModel
    {
        [DataMember(Name = "yds")]
        public int Yds { get; set; }

        [DataMember(Name = "tds")]
        public int Tds { get; set; }

        [DataMember(Name = "rec")]
        public int Rec { get; set; }
    }

    public class KickingModel : PlayerModel
    {
        [DataMember(Name = "fld_goals_made")]
        public int FldGoalsMade { get; set; }

        [DataMember(Name = "fld_goals_att")]
        public int FldGoalsAtt { get; set; }

        [DataMember(Name = "extra_pt_made")]
        public int ExtraPtMade { get; set; }

        [DataMember(Name = "extra_pt_att")]
        public int ExtraPtAtt { get; set; }
    }    
}

