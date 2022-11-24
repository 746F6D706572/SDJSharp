using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Description response. Provides extra descriptions for specified program
    /// </summary>
    [DataContract]
    public class SDDescriptionResponse {
        [DataMember]
        public string episodeID;
        [DataMember]
        public SDProgrammeDescription episodeDescription;

        public SDDescriptionResponse() {
            episodeDescription = new SDProgrammeDescription();
        }

        [DataContract]
        public class SDProgrammeDescription {
            [DataMember]
            public int code;
            [DataMember]
            public string description100;
            [DataMember]
            public string description1000;
        }
    }
}
