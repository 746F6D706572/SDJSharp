using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Preview programme response. Provides details about channels in a line-up we don't have added
    /// </summary>
    [DataContract]
    public class SDPreviewLineupResponse : SDCachedElement {
        [DataMember]
        public string channel;
        [DataMember]
        public string name;
        [DataMember]
        public string callsign;
    }
}
