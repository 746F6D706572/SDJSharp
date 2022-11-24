using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Headends response. Provides a list of headends and lineups
    /// </summary>
    [DataContract]
    public class SDHeadendsResponse : SDCachedElement {
        [DataMember]
        public string headend;
        [DataMember]
        public string transport;
        [DataMember]
        public string location;
        [DataMember]
        public SDLineup[] lineups;

        [DataContract]
        public class SDLineup : SDCachedElement {
            [DataMember]
            public string name;
            [DataMember]
            public string lineup;
            [DataMember]
            public string uri;
        }
    }
}
