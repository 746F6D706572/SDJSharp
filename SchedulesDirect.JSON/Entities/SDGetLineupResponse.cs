using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Lineup response. Provides list of lineup maps, stations and metadata.
    /// </summary>
    [DataContract]
	public class SDGetLineupResponse : SDCachedElement {
        [DataMember(Name = "map")]
        public SDLineupMap[] Map;
        [DataMember(Name = "stations")]
        public SDLineupStation[] Stations;
        [DataMember(Name = "metadata")]
        public SDLineupMetadata Metadata;

        [DataContract]
		public class SDLineupMap : SDCachedElement {
            [DataMember(Name = "stationID")]
            public string StationID;
            [DataMember(Name = "uhfVhf")]
            public int UhfVhf;
            [DataMember(Name = "atscMajor")]
            public int AtscMajor;
            [DataMember(Name = "atscMinor")]
            public int AtscMinor;
            [DataMember(Name = "channel")]
            public string Channel;
            [DataMember(Name = "providerCallsign")]
            public string ProviderCallsign;
            [DataMember(Name = "logicalChannelNumber")]
            public string LogicalChannelNumber;
            [DataMember(Name = "matchType")]
            public string MatchType;
            [DataMember(Name = "frequencyHz")]
            public UInt64 FrequencyHz;
            [DataMember(Name = "polarization")]
            public string Polarization;
            [DataMember(Name = "deliverySystem")]
            public string DeliverySystem;
            [DataMember(Name = "modulationSystem")]
            public string ModulationSystem;
            [DataMember(Name = "symbolrate")]
            public int Symbolrate;
            [DataMember(Name = "fec")]
            public string Fec;
            [DataMember(Name = "serviceID")]
            public string ServiceID;
            [DataMember(Name = "networkID")]
            public string NetworkID;
            [DataMember(Name = "transportID")]
            public string TransportID;
        }

        [DataContract]
        public class SDLineupStation : SDCachedElement {
            [DataMember(Name = "stationID")]
            public string StationID;
            [DataMember(Name = "name")]
            public string Name;
            [DataMember(Name = "callsign")]
            public string Callsign;
            [DataMember(Name = "affiliate")]
            public string Affiliate;
            [DataMember(Name = "isRadioStation")]
            public bool IsRadioStation; // experimental
            [DataMember(Name = "broadcastLanguage")]
            public string[] BroadcastLanguage;
            [DataMember(Name = "descriptionLanguage")]
            public string[] DescriptionLanguage;
            [DataMember(Name = "broadcaster")]
            public SDStationBroadcaster Broadcaster;
            [DataMember(Name = "isCommercialFree")]
            public bool IsCommercialFree;
            [DataMember(Name = "logo")]
            public SDStationLogo Logo;

            [DataContract]
            public class SDStationBroadcaster {
                [DataMember(Name = "city")]
                public string City;
                [DataMember(Name = "state")]
                public string State;
                [DataMember(Name = "postalcode")]
                public string Postalcode;
                [DataMember(Name = "country")]
                public string Country;
            }

            [DataContract]
            public class SDStationLogo {
                [DataMember(Name = "URL")]
                public string URL;
                [DataMember(Name = "height")]
                public int Height;
                [DataMember(Name = "width")]
                public int Width;
                [DataMember(Name = "md5")]
                public string MD5;
            }
        }
		
        [DataContract]
        public class SDLineupMetadata {
            [DataMember(Name = "lineup")]
            public string Lineup;
            [DataMember(Name = "modified")]
            public DateTime? Modified;
            [DataMember(Name = "transport")]
            public string Transport;
            [DataMember(Name = "modulation")]
            public string Modulation;
        }
    }
}