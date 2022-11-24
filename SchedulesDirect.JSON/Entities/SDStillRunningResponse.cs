using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Live/Still running response structure. Contains scores for ongoing live events
    /// </summary>
    [DataContract]
    public class SDStillRunningResponse {
        [DataMember]
        public int code;
        [DataMember]
        public string message;
        [DataMember]
        public string programID;
        [DataMember]
        public string response;
        [DataMember]
        public bool isComplete;
        [DataMember]
        public string serverID;
        [DataMember]
        public DateTime? datetime;
        [DataMember]
        public string eventStartDateTime;
        [DataMember]
        public SDStillRunningResult result;

        [DataContract]
        public class SDStillRunningResult {
            [DataMember]
            public SDStillRunningTeamInfo homeTeam;
            [DataMember]
            public SDStillRunningTeamInfo awayTeam;

            [DataContract]
            public class SDStillRunningTeamInfo {
                [DataMember]
                public string name;
                [DataMember]
                public string score;
            }
        }
    }
}
