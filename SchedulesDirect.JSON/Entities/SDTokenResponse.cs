using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Message response, contains result of authentication request
    /// </summary>
    [DataContract]
    public class SDTokenResponse : SDErrorResponse {
        //[DataMember(Name = "code")]
        //public int Code;
        //[DataMember(Name = "message")]
        //public string Message;
        //[DataMember(Name = "serverID")]
        //public string ServerID;
        //[DataMember(Name = "datetime")]
        //public DateTime? Datetime;
        [DataMember(Name = "token")]
        public string Token;

        public bool IsExpired
        {
            get => (!DateTime.HasValue || System.DateTime.Now > DateTime.Value.AddHours(24));
        }
    }
}