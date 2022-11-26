using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Version response, contains information about specified client programme
    /// </summary>
    [DataContract]
    public class SDVersionResponse : SDErrorResponse {
        //[DataMember(Name = "response")]
        //public string Response;
        //[DataMember(Name = "code")]
        //public int Code;
        [DataMember(Name = "client")]
        public string Client;
        [DataMember(Name = "version")]
        public string Version;
        //[DataMember(Name = "serverID")]
        //public string ServerID;
        //[DataMember(Name = "datetime")]
        //public DateTime? DateTime;
        //[DataMember(Name = "message")]
        //public string Message;
    }
}