using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Programme metadata (image information) for programmes response.
    /// </summary>
    [DataContract]
    public class SDProgrammeMetadataResponse {
        [DataMember]
        public string programID;
        [DataMember]
        public SDImageData[] data;
    }
}
