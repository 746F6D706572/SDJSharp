using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Image data (used by multiple command structures)
    /// </summary>
    [DataContract]
    public class SDImageData {
        [DataMember]
        public string width;
        [DataMember]
        public string height;
        [DataMember]
        public string uri;
        [DataMember]
        public string size;
        [DataMember]
        public string aspect;
        [DataMember]
        public string category;
        [DataMember]
        public string text;
        [DataMember]
        public string primary;
        [DataMember]
        public string tier;
        [DataMember]
        public SDProgrammeImageCaption caption;

        [DataContract]
        public class SDProgrammeImageCaption {
            [DataMember]
            public string content;
            [DataMember]
            public string lang;
        }
    }
}
