using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Lineups command response. Provides list of lineups available.
    /// </summary>
    [DataContract]
    public class SDLineupsResponse {
        [DataMember]
        public int code;
        [DataMember]
        public string serverID;
        [DataMember]
        public DateTime? datetime;
        [DataMember]
        public SDLineups[] lineups;

        [DataContract]
        public class SDLineups {
            [DataMember]
            public string lineup;
            [DataMember]
            public string name;
            [DataMember]
            public string transport;
            [DataMember]
            public string location;
            [DataMember]
            public string uri;
        }
    }
}
