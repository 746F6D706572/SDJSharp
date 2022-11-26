using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Image data (used by multiple command structures)
    /// </summary>
    [DataContract]
    public class SDImageData {
        [DataMember(Name = "width")]
        public string Width;
        [DataMember(Name = "height")]
        public string Height;
        [DataMember(Name = "uri")]
        public string URI;
        [DataMember(Name = "size")]
        public string Size;
        [DataMember(Name = "aspect")]
        public string Aspect;
        [DataMember(Name = "category")]
        public string Category;
        [DataMember(Name = "text")]
        public string Text;
        [DataMember(Name = "primary")]
        public string Primary;
        [DataMember(Name = "tier")]
        public string Tier;
        [DataMember(Name = "caption")]
        public SDProgrammeImageCaption Caption;

        [DataContract]
        public class SDProgrammeImageCaption {
            [DataMember(Name = "content")]
            public string Content;
            [DataMember(Name = "lang")]
            public string Lang;
        }
    }
}