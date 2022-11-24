using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Version response, contains information about specified client programme
    /// </summary>
    [DataContract]
    public class SDVersionResponse {
        [DataMember]
        public string response;
        [DataMember]
        public int code;
        [DataMember]
        public string client;
        [DataMember]
        public string version;
        [DataMember]
        public string serverID;
        [DataMember]
        public DateTime? datetime;
        [DataMember]
        public string message;
    }
}
