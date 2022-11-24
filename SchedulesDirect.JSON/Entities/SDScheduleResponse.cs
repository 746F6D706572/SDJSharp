using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Schedule response. Provides data regarding program schedules for requested stations
    /// </summary>
    [DataContract]
    public class SDScheduleResponse {
        [DataMember]
        public string stationID;
        [DataMember]
        public SDScheduleProgramme[] programs;
        [DataMember]
        public SDScheduleMetadata metadata;
        // Possible error fields
        [DataMember]
        public string serverID;
        [DataMember]
        public int code;
        [DataMember]
        public string response;
        [DataMember]
        public DateTime? retryTime;

        [DataContract]
        public class SDScheduleProgramme {
            [DataMember]
            public string programID;
            [DataMember]
            public DateTime? airDateTime;
            [DataMember]
            public int duration;
            [DataMember]
            public string md5;
            [DataMember(Name = "new")]
            public bool isNew;
            [DataMember]
            public bool cableInTheClassroom;
            [DataMember]
            public bool catchup;
            [DataMember]
            public bool continued;
            [DataMember]
            public bool educational;
            [DataMember]
            public bool joinedInProgress;
            [DataMember]
            public bool leftInProgress;
            [DataMember]
            public bool premiere;
            [DataMember]
            public bool programBreak;
            [DataMember]
            public bool repeat;
            [DataMember]
            public bool signed;
            [DataMember]
            public bool subjectToBlackout;
            [DataMember]
            public bool timeApproximate;
            [DataMember]
            public bool free;
            [DataMember]
            public string liveTapeDelay;
            [DataMember]
            public string isPremiereOrFinale;
            [DataMember]
            public string[] audioProperties;
            [DataMember]
            public string[] videoProperties;
            [DataMember]
            public SDScheduleRatings[] ratings;
            [DataMember]
            public SDScheduleMultipart multipart;

            [DataContract]
            public class SDScheduleRatings {
                [DataMember]
                public string body;
                [DataMember]
                public string code;
            }

            [DataContract]
            public class SDScheduleMultipart {
                [DataMember]
                public int partNumber;
                [DataMember]
                public int totalParts;
            }
        }

        [DataContract]
        public class SDScheduleMetadata {
            [DataMember]
            public int code;
            [DataMember]
            public DateTime? modified;
            [DataMember]
            public string md5;
            [DataMember]
            public string startDate;
        }
    }
}
