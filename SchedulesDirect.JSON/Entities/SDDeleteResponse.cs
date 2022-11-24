using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Delete message response structure. Contains result and messages for this operation
    /// </summary>
    [DataContract]
    public class SDDeleteResponse {
        [DataMember]
        public int code;
        [DataMember]
        public string response;
        [DataMember]
        public string serverID;
        [DataMember]
        public string message;
        [DataMember]
        public DateTime? datetime;
    }
}
