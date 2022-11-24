using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Lineup response. Success/failure result for adding lineup
    /// </summary>
    [DataContract]
    public class SDAddRemoveLineupResponse {
        [DataMember]
        public string response;
        [DataMember]
        public int code;
        [DataMember]
        public string serverID;
        [DataMember]
        public string message;
        [DataMember]
        public string changesRemaining;
        [DataMember]
        public DateTime? datetime;
    }
}
