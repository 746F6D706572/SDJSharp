using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Message response, contains result of authentication request
    /// </summary>
    [DataContract]
    public class SDTokenResponse {
        [DataMember]
        public int code;
        [DataMember]
        public string message;
        [DataMember]
        public DateTime? datetime;
        [DataMember]
        public string token;
        [DataMember]
        public string response;
    }
}
