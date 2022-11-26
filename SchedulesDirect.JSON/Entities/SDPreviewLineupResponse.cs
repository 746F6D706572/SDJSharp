using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Preview programme response. Provides details about channels in a line-up we don't have added
    /// </summary>
    [DataContract]
    public class SDPreviewLineupResponse : SDCachedElement {
        [DataMember(Name = "channel")]
        public string Channel;
        [DataMember(Name = "name")]
        public string Name;
        [DataMember(Name = "callsign")]
        public string Callsign;
    }
}