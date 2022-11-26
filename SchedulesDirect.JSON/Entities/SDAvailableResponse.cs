using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Available response. Provide list of available services
    /// </summary>
    [DataContract]
    public class SDAvailableResponse {
        [DataMember(Name = "type")]
        public string Type;
        [DataMember(Name = "description")]
        public string Description;
        [DataMember(Name = "uri")]
        public string URI;
    }
}