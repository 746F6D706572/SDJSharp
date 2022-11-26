using System;
using System.Runtime.Serialization;

namespace SchedulesDirect
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class SDErrorResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "response")]
        public string Response;
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "code")]
        public int Code;
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "serverID")]
        public string ServerID;
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "message")]
        public string Message;
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "datetime")]
        public DateTime? DateTime;
    }
}