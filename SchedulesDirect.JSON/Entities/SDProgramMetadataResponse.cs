using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Programme metadata (image information) for programmes response.
    /// </summary>
    [DataContract]
    public class SDProgramMetadataResponse {
        [DataMember(Name = "programID")]
        public string ProgramID;
        [DataMember(Name = "data")]
        public SDImageData[] Data;
    }
}