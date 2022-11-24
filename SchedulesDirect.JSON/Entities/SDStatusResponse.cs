using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Response to Status command, contains information about account
    /// </summary>
    [DataContract]
    public class SDStatusResponse {
        [DataMember]
        public SDAccount account;
        [DataMember]
        public SDLineUps[] lineups;
        [DataMember]
        public DateTime? lastDataUpdate;
        [DataMember]
        public string[] notifications;
        [DataMember]
        public SDSystemStatus[] systemStatus;
        [DataMember]
        public string serverID;
        [DataMember]
        public DateTime? datetime;
        [DataMember]
        public int code;

        [DataContract]
        public class SDAccount {
            [DataMember]
            public string expires;
            [DataMember]
            public string[] messages;
            [DataMember]
            public int maxLineups;
        }

        [DataContract]
        public class SDLineUps {
            [DataMember]
            public string lineup;
            [DataMember]
            public DateTime? modified;
            [DataMember]
            public string uri;
            [DataMember]
            public bool isDeleted;
        }

        [DataContract]
        public class SDSystemStatus {
            [DataMember]
            public DateTime? date;
            [DataMember]
            public string status;
            [DataMember]
            public string message;
        }
    }
}
