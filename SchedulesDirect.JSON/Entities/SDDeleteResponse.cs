using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Delete message response structure. Contains result and messages for this operation
    /// </summary>
    [DataContract]
    public class SDDeleteResponse : SDErrorResponse {
        //[DataMember(Name = "code")]
        //public int Code;
        //[DataMember(Name = "response")]
        //public string Response;
        //[DataMember(Name = "serverID")]
        //public string ServerID;
        //[DataMember(Name = "message")]
        //public string Message;
        //[DataMember(Name = "datetime")]
        //public DateTime? DateTime;
    }
}