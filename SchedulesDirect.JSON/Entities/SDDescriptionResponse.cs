using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Description response. Provides extra descriptions for specified program
    /// </summary>
    [DataContract]
    public class SDDescriptionResponse {
        [DataMember(Name = "episodeID")]
        public string EpisodeID;
		
		[DataMember(Name = "episodeDescription")]
		public SDProgrammeDescription EpisodeDescription;
		
        [DataContract]
        public class SDProgrammeDescription {
			[DataMember(Name = "code")]
            public int Code;
            [DataMember(Name = "description100")]
            public string Description100;
            [DataMember(Name = "description1000")]
            public string Description1000;
        }
		
		public SDDescriptionResponse() {
            EpisodeDescription = new SDProgrammeDescription();
        }
	}
}