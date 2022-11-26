using System;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Your program must be able to process UTF-8 characters.
    /// </summary>
    [DataContract]
    public class SDStationScheduleResponse : SDErrorResponse
    {
        //[DataMember(Name = "serverID")]
        //public string ServerID;
        //[DataMember(Name = "code")]
        //public int Code;
        //[DataMember(Name = "response")]
        //public string Response;
        [DataMember(Name = "retryTime")]
        public DateTime? RetryTime;

        //Schedule fields

        /// <summary>
        /// StationID - Max 12 characters. Mandatory. This is a string, and your code must not treat it as an integer. Provides information to the grabber regarding the stationID that this schedule is for.
        /// </summary>
        [DataMember(Name = "stationID")]
        public string StationID;

        //Note: Australian and New Zealand stationIDs will start with "AU" or "NZ".

        /// <summary>
        /// Programs - An array of program data. Mandatory. Must contain at least one element.
        /// </summary>
        [DataMember(Name = "programs")]
        public SDProgramme[] Programs;

        //Each element of the programs array will contain:
        [DataContract]
        public class SDProgramme
        {
            /// <summary>
            /// ProgramID - 14 characters. Mandatory. What programID will the data be for.
            /// </summary>
            [DataMember(Name = "programID")]
            public string ProgramID;
            /// <summary>
            /// airDateTime - 20 characters. Mandatory. "2014-10-03T00:00:00Z". This will always be in "Z" time; your grabber must make any adjustments to localtime if it does not natively work in "Z" time internally.
            /// </summary>
            [DataMember(Name = "airDateTime")]
            public DateTime? AirDateTime;
            /// <summary>
            /// duration - integer. Mandatory. Duration of the program in seconds.
            /// </summary>
            [DataMember(Name = "duration")]
            public int Duration;
            /// <summary>
            /// md5 - 22 characters. Mandatory. The MD5 hash value of the JSON data on the server for the programID. If your application has cached the JSON for the program, but the cached MD5 isn't the same as what is in the schedule, that should trigger your grabber to refresh the JSON for the programID because it's changed.
            /// </summary>
            [DataMember(Name = "md5")]
            public string MD5;

            //The following program elements are booleans and are optional. If a boolean is not in the schedule, then assume "FALSE". If a boolean is in the schedule, do not assume "TRUE".

            /// <summary>
            /// new - is this showing new?
            /// </summary>
            [DataMember(Name = "new")]
            public bool IsNew;
            /// <summary>
            /// cableInTheClassroom
            /// </summary>
            [DataMember(Name = "cableInTheClassroom")]
            public bool CableInTheClassroom;
            /// <summary>
            /// catchup - typically only found outside of North America
            /// </summary>
            [DataMember(Name = "catchup")]
            public bool Catchup;
            /// <summary>
            /// continued - typically only found outside of North America
            /// </summary>
            [DataMember(Name = "continued")]
            public bool Continued;
            /// <summary>
            /// educational
            /// </summary>
            [DataMember(Name = "educational")]
            public bool Educational;
            /// <summary>
            /// joinedInProgress
            /// </summary>
            [DataMember(Name = "joinedInProgress")]
            public bool JoinedInProgress;
            /// <summary>
            /// leftInProgress
            /// </summary>
            [DataMember(Name = "leftInProgress")]
            public bool LeftInProgress;
            /// <summary>
            /// premiere - Should only be found in Miniseries and Movie program types.
            /// </summary>
            [DataMember(Name = "premiere")]
            public bool Premiere;
            /// <summary>
            /// programBreak - Program stops and will restart later(frequently followed by a continued). Typically only found outside of North America.
            /// </summary>
            [DataMember(Name = "programBreak")]
            public bool ProgramBreak;
            /// <summary>
            /// repeat - An encore presentation. Repeat should only be found on a second telecast of sporting events.
            /// </summary>
            [DataMember(Name = "repeat")]
            public bool Repeat;
            /// <summary>
            /// signed - Program has an on-screen person providing sign-language translation.
            /// </summary>
            [DataMember(Name = "signed")]
            public bool Signed;
            /// <summary>
            /// subjectToBlackout
            /// </summary>
            [DataMember(Name = "subjectToBlackout")]
            public bool SubjectToBlackout;
            /// <summary>
            /// timeApproximate
            /// </summary>
            [DataMember(Name = "timeApproximate")]
            public bool TimeApproximate;
            /// <summary>
            /// free - the program is on a channel which typically has a cost, such as pay-per-view, but in this instance is free.
            /// </summary>
            [DataMember(Name = "free")]
            public bool Free;

            //The following are optional, but can be used to indicate additional information.

            //liveTapeDelay - is this showing Live, or Tape Delayed?. Possible values: "Live", "Tape", "Delay".
            [DataMember(Name = "liveTapeDelay")]
            public string LiveTapeDelay;

            //isPremiereOrFinale - Values are: "Season Premiere", "Season Finale", "Series Premiere", "Series Finale", "Premiere", "Finale"
            [DataMember(Name = "isPremiereOrFinale")]
            public string IsPremiereOrFinale;

            //ratings - an array of ratings values.

            [DataMember(Name = "ratings")]
            public SDRatings[] Ratings;

            [DataContract]
            public class SDRatings
            {
                /// <summary>
                /// body: what classification body is providing the rating?
                /// </summary>
                [DataMember(Name = "body")]
                public string Body;
                /// <summary>
                /// code: what rating was assigned?
                /// </summary>
                [DataMember(Name = "code")]
                public string Code;
            }
            /// <summary>
            /// multipart - indicates whether the program is one of a series.
            /// </summary>
            [DataMember(Name = "multipart")]
            public SDMultipart Multipart;

            //"multipart": {
            //  "partNumber": 1,
            //  "totalParts": 3
            //}

            [DataContract]
            public class SDMultipart
            {
                [DataMember(Name = "partNumber")]
                public int PartNumber;
                [DataMember(Name = "totalParts")]
                public int TotalParts;
            }

            //audioProperties - optional. An array of audio properties.

            //Atmos         - Dolby Atmos
            //cc            - Closed Captioned
            //DD
            //DD 5.1
            //Dolby
            //dubbed
            //dvs           - Descriptive Video Service
            //SAP           - Secondary Audio Program
            //stereo
            //subtitled
            //surround

            [DataMember(Name = "audioProperties")]
            public string[] AudioProperties;

            /// <summary>
            /// videoProperties - optional. An array of video properties.<para/>
            /// <list type="table">
            /// <item><term>3d</term><description>Is this showing in 3d</description></item><para/>
            /// <item><term>enhanced</term><description>Enhanced is better video quality than Standard Definition, but not true High Definition. (720p / 1080i)</description></item><para/>
            /// <item><term>hdtv</term><description>The content is in High Definition</description></item><para/>
            /// <item><term>hdr</term><description>The content has High Dynamic Range. All HDR content is UHDTV, but not all UHDTV content has HDR</description></item><para/>
            /// <item><term>letterbox</term><description></description></item><para/>
            /// <item><term>sdtv</term><description></description></item><para/>
            /// <item><term>uhdtv</term><description>The content is in "UHDTV"; this is provider-dependent and does not imply any particular resolution or encoding</description></item><para/>
            /// </list>
            /// </summary>
            [DataMember(Name = "videoProperties")]
            public string[] VideoProperties;
        }

        /// <summary>
        /// The following metadata is associated with the schedule (it is not inside the programs array)
        /// </summary>
        [DataMember(Name = "metadata")]
        public SDMetadata Metadata;

        [DataContract]
        public class SDMetadata
        {
            /// <summary>
            /// code - optional. Indicates if there were errors with the station. It is possible that your client has requested a stationID which has been deleted upstream, but that your client still believes is "live" because it hasn't updated the lineup.
            /// </summary>
            [DataMember(Name = "code")]
            public int Code;
            /// <summary>
            /// modified - 20 characters. Mandatory. Follows strftime() format; "Y-m-d\TH:i:s\Z" The timestamp for the creation time of this schedule on the server.
            /// </summary>
            [DataMember(Name = "modified")]
            public DateTime? Modified;
            /// <summary>
            /// md5 - 22 characters. Mandatory. What is the MD5 for this schedule (stationID and programs elements) - metadata is not included in the MD5 calculation.
            /// </summary>
            [DataMember(Name = "md5")]
            public string MD5;
            /// <summary>
            /// startDate - the start date of the JSON schedule. Mandatory. strftime() "Y-m-d" format.
            /// </summary>
            [DataMember(Name = "startDate")]
            public string StartDate;
        }
    }
}