using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Lineup response. Success/failure result for adding lineup
    /// </summary>
    [DataContract]
    public class SDAddRemoveLineupResponse : SDErrorResponse {
		//[DataMember(Name = "response")]
        //public string Response;
        //[DataMember(Name = "code")]
        //public int Code;
        //[DataMember(Name = "serverID")]
        //public string ServerID;
        //[DataMember(Name = "message")]
        //public string Message;
        [DataMember(Name = "changesRemaining")]
        public string ChangesRemaining;
        //[DataMember(Name = "datetime")]
        //public DateTime? DateTime;
    }
}