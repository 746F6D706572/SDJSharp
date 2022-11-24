using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Available response. Provide list of available services
    /// </summary>
    [DataContract]
    public class SDAvailableResponse {
        [DataMember]
        public string type;
        [DataMember]
        public string description;
        [DataMember]
        public string uri;
    }
}
