using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Lineup response. Provides list of lineup maps, stations and metadata.
    /// </summary>
    [DataContract]
    public class SDGetLineupResponse : SDCachedElement {
        [DataMember]
        public SDLineupMap[] map;
        [DataMember]
        public SDLineupStation[] stations;
        [DataMember]
        public SDLineupMetadata metadata;

        [DataContract]
        public class SDLineupMap : SDCachedElement {
            [DataMember]
            public string stationID;
            [DataMember]
            public int uhfVhf;
            [DataMember]
            public int atscMajor;
            [DataMember]
            public int atscMinor;
            [DataMember]
            public string channel;
            [DataMember]
            public string providerCallsign;
            [DataMember]
            public string logicalChannelNumber;
            [DataMember]
            public string matchType;
            [DataMember]
            public UInt64 frequencyHz;
            [DataMember]
            public string polarization;
            [DataMember]
            public string deliverySystem;
            [DataMember]
            public string modulationSystem;
            [DataMember]
            public int symbolrate;
            [DataMember]
            public string fec;
            [DataMember]
            public string serviceID;
            [DataMember]
            public string networkID;
            [DataMember]
            public string transportID;
        }

        [DataContract]
        public class SDLineupStation : SDCachedElement {
            [DataMember]
            public string stationID;
            [DataMember]
            public string name;
            [DataMember]
            public string callsign;
            [DataMember]
            public string affiliate;
            [DataMember]
            public string[] broadcastLanguage;
            [DataMember]
            public string[] descriptionLanguage;
            [DataMember]
            public SDStationBroadcaster broadcaster;
            [DataMember]
            public bool isCommercialFree;
            [DataMember]
            public SDStationLogo logo;

            [DataContract]
            public class SDStationBroadcaster {
                [DataMember]
                public string city;
                [DataMember]
                public string state;
                [DataMember]
                public string postalcode;
                [DataMember]
                public string country;
            }

            [DataContract]
            public class SDStationLogo {
                [DataMember]
                public string URL;
                [DataMember]
                public int height;
                [DataMember]
                public int width;
                [DataMember]
                public string md5;
            }
        }

        [DataContract]
        public class SDLineupMetadata {
            [DataMember]
            public string lineup;
            [DataMember]
            public DateTime? modified;
            [DataMember]
            public string transport;
            [DataMember]
            public string modulation;
        }
    }
}
