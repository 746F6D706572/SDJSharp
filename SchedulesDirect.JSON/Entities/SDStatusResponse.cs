using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Response to Status command, contains information about account
    /// </summary>
    [DataContract]
    public class SDStatusResponse : SDErrorResponse {
        [DataMember(Name = "account")]
        public SDAccount Account;
        [DataMember(Name = "lineups")]
        public SDLineUps[] Lineups;
        [DataMember(Name = "lastDataUpdate")]
        public DateTime? LastDataUpdate;
        [DataMember(Name = "notifications")]
        public string[] Notifications;
        [DataMember(Name = "systemStatus")]
        public SDSystemStatus[] SystemStatus;
        //[DataMember(Name = "serverID")]
        //public string ServerID;
        //[DataMember(Name = "datetime")]
        //public DateTime? DateTime;
        //[DataMember(Name = "code")]
        //public int Code;

        [DataContract]
        public class SDAccount {
            [DataMember(Name = "expires")]
            public string Expires;
            [DataMember(Name = "messages")]
            public string[] Messages;
            [DataMember(Name = "maxLineups")]
            public int MaxLineups;
        }

        [DataContract]
        public class SDLineUps {
            [DataMember(Name = "lineup")]
            public string Lineup;
            [DataMember(Name = "modified")]
            public DateTime? Modified;
            [DataMember(Name = "uri")]
            public string URI;
            [DataMember(Name = "isDeleted")]
            public bool IsDeleted;
        }

        [DataContract]
        public class SDSystemStatus {
            [DataMember(Name = "date")]
            public DateTime? Date;
            [DataMember(Name = "status")]
            public string Status;
            [DataMember(Name = "message")]
            public string Message;
        }
    }
}