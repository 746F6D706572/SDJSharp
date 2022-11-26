using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Lineups command response. Provides list of lineups available.
    /// </summary>
    [DataContract]
    public class SDLineupsResponse : SDErrorResponse {
        //[DataMember(Name = "code")]
        //public int Code;
        //[DataMember(Name = "serverID")]
        //public string ServerID;
        //[DataMember(Name = "datetime")]
        //public DateTime? DateTime;
        [DataMember(Name = "lineups")]
        public SDLineups[] Lineups;

        [DataContract]
        public class SDLineups {
            [DataMember(Name = "lineup")]
            public string Lineup;
            [DataMember(Name = "name")]
            public string Name;
            [DataMember(Name = "transport")]
            public string Transport;
            [DataMember(Name = "location")]
            public string Location;
            [DataMember(Name = "uri")]
            public string URI;
        }
    }
}