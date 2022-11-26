using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Live/Still running response structure. Contains scores for ongoing live events
    /// </summary>
    [DataContract]
    public class SDStillRunningResponse : SDErrorResponse {
        //[DataMember(Name = "code")]
        //public int Code;
        //[DataMember(Name = "message")]
        //public string Message;
        [DataMember(Name = "programID")]
        public string ProgramID;
        //[DataMember(Name = "response")]
        //public string Response;
        [DataMember(Name = "isComplete")]
        public bool IsComplete;
        //[DataMember(Name = "serverID")]
        //public string ServerID;
        //[DataMember(Name = "datetime")]
        //public DateTime? DateTime;
        [DataMember(Name = "eventStartDateTime")]
        public string EventStartDateTime;
        [DataMember(Name = "result")]
        public SDStillRunningResult Result;

        [DataContract]
        public class SDStillRunningResult {
            [DataMember(Name = "homeTeam")]
            public SDStillRunningTeamInfo HomeTeam;
            [DataMember(Name = "awayTeam")]
            public SDStillRunningTeamInfo AwayTeam;

            [DataContract]
            public class SDStillRunningTeamInfo {
                [DataMember(Name = "name")]
                public string Name;
                [DataMember(Name = "score")]
                public string Score;
            }
        }
    }
}