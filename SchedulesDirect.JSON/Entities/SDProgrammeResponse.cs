using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Programme response. Provides details about requested programs.
    /// </summary>
    [DataContract]
    public class SDProgrammeResponse {
        [DataMember]
        public string programID;
        [DataMember]
        public int code;
        [DataMember]
        public string message;
        [DataMember]
        public SDProgrammeTitles[] titles;
        [DataMember]
        public SDProgrammeEventDetails eventDetails;
        [DataMember]
        public SDProgrammeDescriptions descriptions;
        [DataMember]
        public string originalAirDate;
        [DataMember]
        public string[] genres;
        [DataMember]
        public string episodeTitle150;
        [DataMember]
        public SDProgrammeMetadata[] metadata;
        [DataMember]
        public SDProgrammePerson[] cast;
        [DataMember]
        public SDProgrammePerson[] crew;
        [DataMember]
        public string showType;
        [DataMember]
        public bool hasImageArtwork;
        [DataMember]
        public string md5;

        [DataContract]
        public class SDProgrammeTitles {
            [DataMember]
            public string title120;
        }

        [DataContract]
        public class SDProgrammeEventDetails {
            [DataMember]
            public string subType;
        }

        [DataContract]
        public class SDProgrammeDescriptions {
            [DataMember]
            public SDProgrammeDescription100[] description1000;

            [DataContract]
            public class SDProgrammeDescription100 {
                [DataMember]
                public string descriptionLanguage;
                [DataMember]
                public string description;
            }
        }

        [DataContract]
        public class SDProgrammeMetadata {
            [DataMember]
            public SDProgrammeMetadataGracenote Gracenote;

            [DataContract]
            public class SDProgrammeMetadataGracenote {
                [DataMember]
                public int season;
                [DataMember]
                public int episode;
            }
        }

        [DataContract]
        public class SDProgrammePerson {
            [DataMember]
            public string personId;
            [DataMember]
            public string nameId;
            [DataMember]
            public string name;
            [DataMember]
            public string role;
            [DataMember]
            public string billingOrder;
        }
    }
}
