using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Headends response. Provides a list of headends and lineups
    /// </summary>
    [DataContract]
	public class SDHeadendsResponse : SDCachedElement {
        [DataMember(Name = "headend")]
        public string Headend;
        [DataMember(Name = "transport")]
        public string Transport;
        [DataMember(Name = "location")]
        public string Location;
        [DataMember(Name = "lineups")]
        public SDLineup[] Lineups;

        [DataContract]
        public class SDLineup : SDCachedElement {
            [DataMember(Name = "name")]
            public string Name;
            [DataMember(Name = "lineup")]
            public string Lineup;
            [DataMember(Name = "uri")]
            public string URI;
        }
    }
}