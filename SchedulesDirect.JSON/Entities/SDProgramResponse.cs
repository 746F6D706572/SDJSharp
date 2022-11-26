using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Programme response. Provides details about requested programs.
    /// </summary>
    [DataContract]
    public class SDProgramResponse : SDErrorResponse {
        //#region Error fields
        //[DataMember]
        //public string response;
        //[DataMember]
        //public int code;
        //[DataMember]
        //public string serverID;
        //[DataMember]
        //public string message;
        //[DataMember]
        //public string datetime;
        //#endregion

        //--------------------------------------------------------------------------------------------------

        //programID: 14 characters. Mandatory.

        /// <summary>
        /// 14 characters. Mandatory.
        /// </summary>
		[DataMember(Name = "programID")]
		public string ProgramID;

        //--------------------------------------------------------------------------------------------------

        //titles: array containing program titles. Mandatory.
        /// <summary>
        /// Array containing program titles. Mandatory.
        /// </summary>
		[DataMember(Name = "titles")]
        public SDProgramTitles[] Titles;

        [DataContract]
        public class SDProgramTitles
        {
            //title120: 120 character description of the program. Mandatory.
            /// <summary>
            /// 120 character description of the program. Mandatory.
            /// </summary>
            [DataMember(Name = "title120")]
            public string Title120;
        }

        //--------------------------------------------------------------------------------------------------

        //episodeTitle150: 150 character episode title. Optional.
        /// <summary>
        /// 150 character episode title. Optional.
        /// </summary>
        [DataMember(Name = "episodeTitle150")]
        public string EpisodeTitle150;

        //--------------------------------------------------------------------------------------------------

        //descriptions: array containing descriptions of the program. Optional.
        /// <summary>
        /// Array containing descriptions of the program. Optional.
        /// </summary>
        [DataMember(Name = "descriptions")]
        public SDDescriptions Descriptions;

        [DataContract]
        public class SDDescriptions
        {
            //description100: array containing short description of the program. The description contained will have a maximum of 100 characters. Optional.
            /// <summary>
            /// Array containing short description of the program. The description contained will have a maximum of 100 characters. Optional.
            /// </summary>
            [DataMember(Name = "description100")]
            public SDDescription[] Description100;

            //description1000: array containing long description of the program. The description contained will have a maximum of 1000 characters. Optional.
            /// <summary>
            /// Array containing long description of the program. The description contained will have a maximum of 1000 characters. Optional.
            /// </summary>
            [DataMember(Name = "description1000")]
            public SDDescription[] Description1000;

            //Consists of the following fields:

            [DataContract]
            public class SDDescription
            {
                //description Language. Mandatory.
                /// <summary>
                /// Description Language. Mandatory.
                /// </summary>
                [DataMember(Name = "descriptionLanguage")]
                public string DescriptionLanguage;

                //description: text containing description. Mandatory.
                /// <summary>
                /// Text containing description. Mandatory.
                /// </summary>
                [DataMember(Name = "description")]
                public string Description;
            }
        }

        //--------------------------------------------------------------------------------------------------

        //eventDetails: indicates the type of program. Optional. Sport programs will have the following additional information:
        /// <summary>
        /// Indicates the type of program. Optional. Sport programs will have the following additional information
        /// </summary>
        [DataMember(Name = "eventDetails")]
        public SDEventDetails EventDetails;

        //Fields:

        [DataContract]
        public class SDEventDetails
        {
            //venue100: location of the event
            /// <summary>
            /// Location of the event
            /// </summary>
            [DataMember(Name = "venue100")]
            public string Venue100;

            //teams: array containing the teams that are playing. Optional.
            /// <summary>
            /// Array containing the teams that are playing. Optional.
            /// </summary>
            [DataMember(Name = "teams")]
            public SDTeams[] Teams;

            [DataContract]
            public class SDTeams
            {
                //name - name of the team. Mandatory.
                /// <summary>
                /// Name of the team.Mandatory.
                /// </summary>
                [DataMember(Name = "name")]
                public string Name;

                //"isHome" - boolean indicating this team is the home team. Optional.
                /// <summary>
                /// Boolean indicating this team is the home team. Optional.
                /// </summary>
                [DataMember(Name = "isHome")]
                public bool IsHome;
            }

            //gameDate: YYYY-MM-DD. Optional.
            /// <summary>
            /// YYYY-MM-DD. Optional.
            /// </summary>
            [DataMember(Name = "gameDate")]
            public string GameDate;
        }

        //--------------------------------------------------------------------------------------------------

        //originalAirDate: YYYY-MM-DD. Optional.
        /// <summary>
        /// YYYY-MM-DD. Optional.
        /// </summary>
        [DataMember(Name = "originalAirDate")]
        public string OriginalAirDate;

        //--------------------------------------------------------------------------------------------------

        //genres: array of genres this program falls under. Optional.
        /// <summary>
        /// Array of genres this program falls under. Optional.
        /// </summary>
        [DataMember(Name = "genres")]
        public string[] Genres;

        //--------------------------------------------------------------------------------------------------

        //officialURL: string containing the official URL of the program. Optional.
        /// <summary>
        /// String containing the official URL of the program. Optional.
        /// </summary>
        [DataMember(Name = "officialURL")]
        public string OfficialURL;

        //--------------------------------------------------------------------------------------------------

        //keyWords: array containing the following elements:

        [DataMember(Name = "keyWords")]
        public SDProgramKeyWords KeyWords;

        [DataContract]
        public class SDProgramKeyWords
        {
            //Mood. Array of strings. Optional.
            /// <summary>
            /// Array of strings. Optional.
            /// </summary>
            [DataMember(Name = "Mood")]
            public string[] Mood;

            //Time Period. Array of strings. Optional.
            /// <summary>
            /// Array of strings. Optional.
            /// </summary>
            [DataMember(Name = "Time Period")]
            public string[] TimePeriod;

            //Theme. Array of strings. Optional.
            /// <summary>
            /// Array of strings. Optional.
            /// </summary>
            [DataMember(Name = "Theme")]
            public string[] Theme;

            //Character. Array of strings. Optional.
            /// <summary>
            /// Array of strings. Optional.
            /// </summary>
            [DataMember(Name = "Character")]
            public string[] Character;

            //Setting. Array of strings. Optional.
            /// <summary>
            /// Array of strings. Optional.
            /// </summary>
            [DataMember(Name = "Setting")]
            public string[] Setting;

            //Subject. Array of strings. Optional.
            /// <summary>
            /// Array of strings. Optional.
            /// </summary>
            [DataMember(Name = "Subject")]
            public string[] Subject;

            //General. Array of strings. Optional.
            /// <summary>
            /// Array of strings. Optional.
            /// </summary>
            [DataMember(Name = "General")]
            public string[] General;
        }

        //--------------------------------------------------------------------------------------------------

        //metadata: key / value array of metadata about the program. Optional. Consists of the following fields:
        /// <summary>
        /// Key / value array of metadata about the program. Optional.
        /// </summary>
        [DataMember(Name = "metadata")]
        public SDMetadata[] Metadata;

        [DataContract]
        public class SDMetadata
        {
            //key - string indicating who is providing the information. Example: "Gracenote"
            /// <summary>
            /// String indicating who is providing the information.
            /// </summary>
            [DataMember(Name = "Gracenote")]
            public SDGracenote Gracenote;

            //value: array consisting of:

            [DataContract]
            public class SDGracenote
            {
                //season: integer indicating the season number. Mandatory.
                /// <summary>
                /// Integer indicating the season number. Mandatory.
                /// </summary>
                [DataMember(Name = "season")]
                public int Season;

                //episode: integer indicating the episode number. Optional.
                /// <summary>
                /// Integer indicating the episode number. Optional.
                /// </summary>
                [DataMember(Name = "episode")]
                public int Episode;

                //totalEpisodes: an integer indicating the total number of episodes. Note: in an "EP" program this indicates the total number of episodes in this season. In an "SH" program, it will indicate the total number of episodes in the series. Optional.
                /// <summary>
                /// Integer indicating the total number of episodes.</para>
                /// Note: in an "EP" program this indicates the total number of episodes in this season. In an "SH" program, it will indicate the total number of episodes in the series. Optional.
                /// </summary>
                [DataMember(Name = "totalEpisodes")]
                public int TotalEpisodes;

                //totalSeasons: integer indicating the total number of seasons in the series. SH programs only. Optional.
                /// <summary>
                /// Integer indicating the total number of seasons in the series. SH programs only. Optional.
                /// </summary>
                [DataMember(Name = "totalSeasons")]
                public int TotalSeasons;
            }
        }

        //--------------------------------------------------------------------------------------------------

        //entityType: string. Mandatory.
        //[Show | Episode | Sports | Movie]
        /// <summary>
        /// String. Mandatory.</para>
        /// [Show | Episode | Sports | Movie]
        /// </summary>
        [DataMember(Name = "entityType")]
        public string EntityType;

        //NOTE: Not all sports events will have a leading "SP" programID; your application should use the entity type.

        //--------------------------------------------------------------------------------------------------

        //showType: what sort of program is this. Optional.
        /// <summary>
        /// What sort of program is this. Optional.
        /// </summary>
        [DataMember(Name = "showType")]
        public string ShowType;

        //--------------------------------------------------------------------------------------------------

        //contentAdvisory: array of advisories about the program, such as adult situations, violence, etc. Optional.
        /// <summary>
        /// Array of advisories about the program, such as adult situations, violence, etc. Optional.
        /// </summary>
        [DataMember(Name = "contentAdvisory")]
        public string[] ContentAdvisory;

        //--------------------------------------------------------------------------------------------------

        //contentRating: array consisting of various rating boards' ratings. Optional. 
        /// <summary>
        /// Array consisting of various rating boards' ratings. Optional. 
        /// </summary>
        [DataMember(Name = "contentRating")]
        public SDContentRating[] ContentRating;

        //Consists of the following fields:

        [DataContract]
        public class SDContentRating
        {
            //body: name of the rating body. Mandatory.
            /// <summary>
            /// Name of the rating body. Mandatory.
            /// </summary>
            [DataMember(Name = "body")]
            public string Body;

            //code: The rating assigned. Dependent on the rating body. Mandatory.
            /// <summary>
            /// The rating assigned. Dependent on the rating body. Mandatory.
            /// </summary>
            [DataMember(Name = "code")]
            public string Code;

            /// <summary>
            /// Three letter country code of the rating body (eg. GBR, USA)
            /// </summary>
            [DataMember(Name = "country")]
            public string Country;

        }

        //--------------------------------------------------------------------------------------------------

        //movie: array of information specific to a movie type. Optional, and only found with "MV" programIDs. 
        /// <summary>
        /// Array of information specific to a movie type. Optional, and only found with "MV" programIDs.
        /// </summary>
        [DataMember(Name = "movie")]
        public SDMovie Movie;

        //Consists of the following fields:

        [DataContract]
        public class SDMovie
        {
            //year: YYYY.The year the movie was released. Optional.
            /// <summary>
            /// YYYY. The year the movie was released. Optional.
            /// </summary>
            [DataMember(Name = "year")]
            public string Year;

            //duration: Duration (in integer seconds). Optional. NOTE: in a future API this will be removed from the movie array and will be an element of the program itself.
            /// <summary>
            /// Duration (in integer seconds). Optional. NOTE: in a future API this will be removed from the movie array and will be an element of the program itself.
            /// </summary>
            [DataMember(Name = "duration")]
            public string Duration;

            //qualityRating: an array of ratings for the quality of the movie. Optional. 
            /// <summary>
            /// Array of ratings for the quality of the movie. Optional. 
            /// </summary>
            [DataMember(Name = "qualityRating")]
            public SDQualityRating[] QualityRating;

            //Consists of the following fields:

            [DataContract]
            public class SDQualityRating
            {
                //ratingsBody: srray of ratings for the quality of the movie. Optional. 
                /// <summary>
                /// Array of ratings for the quality of the movie. Optional. 
                /// </summary>
                [DataMember(Name = "ratingsBody")]
                public string RatingsBody;

                //rating: string indicating the rating. Mandatory.
                /// <summary>
                /// String indicating the rating. Mandatory.
                /// </summary>
                [DataMember(Name = "rating")]
                public string Rating;

                //minRating: string indicating the lowest rating. Optional.
                /// <summary>
                /// String indicating the lowest rating. Optional.
                /// </summary>
                [DataMember(Name = "minRating")]
                public string MinRating;

                //maxRating: string indicating the highest rating. Optional.
                /// <summary>
                /// String indicating the highest rating. Optional.
                /// </summary>
                [DataMember(Name = "maxRating")]
                public string MaxRating;

                //increment: string indicating the increment. Optional.
                /// <summary>
                /// String indicating the increment. Optional.
                /// </summary>
                [DataMember(Name = "increment")]
                public string Increment;
            }
        }

        //--------------------------------------------------------------------------------------------------

        //cast - array of cast members. Optional. 
        /// <summary>
        /// Array of cast members. Optional.
        /// </summary>
        [DataMember(Name = "cast")]
        public SDPerson[] Cast;

        //crew - array of crew members. Optional. Follows the same pattern as "cast".
        /// <summary>
        /// Array of crew members. Optional.
        /// </summary>
        [DataMember(Name = "crew")]
        public SDPerson[] Crew;

        //Each cast member element consists of the following fields:

        [DataContract]
        public class SDPerson
        {
            //personID: string for this person. Used to retrieve images. Optional.
            /// <summary>
            /// String for this person. Used to retrieve images. Optional.
            /// </summary>
            [DataMember(Name = "personId")]
            public string PersonId;

            //nameID: string for this person. Used to differentiate people that have various names, such as due to marriage, divorce, etc. Optional. Actors in adult movies will typically not have a personID or nameID.
            /// <summary>
            /// string for this person. Used to differentiate people that have various names, such as due to marriage, divorce, etc. Optional. Actors in adult movies will typically not have a personID or nameID.
            /// </summary>
            [DataMember(Name = "nameId")]
            public string NameId;

            //name: string indicating the person's name. Mandatory.
            /// <summary>
            /// String indicating the person's name. Mandatory.
            /// </summary>
            [DataMember(Name = "name")]
            public string Name;

            //role: string indicating what role this person had. Mandatory.
            /// <summary>
            /// String indicating what role this person had. Mandatory.
            /// </summary>
            [DataMember(Name = "role")]
            public string Role;

            //characterName: string indicating the name of the character this person played. Optional.
            /// <summary>
            /// String indicating the name of the character this person played. Optional.
            /// </summary>
            [DataMember(Name = "characterName")]
            public string CharacterName;

            //billingOrder: string indicating billing order. Mandatory.
            /// <summary>
            /// String indicating billing order. Mandatory.
            /// </summary>
            [DataMember(Name = "billingOrder")]
            public string BillingOrder;
        }

        //--------------------------------------------------------------------------------------------------

        //recommendations - array of programs similar to this one that you may also enjoy. Optional. 
        /// <summary>
        /// Array of programs similar to this one that you may also enjoy. Optional. 
        /// </summary>
        [DataMember(Name = "recommendations")]
        public SDRecomendation[] Recommendations;

        //Each recommendation element consists of the following fields:

        [DataContract]
        public class SDRecomendation
        {
            //programID: programID of the recommendation. Mandatory.
            /// <summary>
            /// ProgramID of the recommendation. Mandatory.
            /// </summary>
            [DataMember(Name = "programID")]
            public string ProgramID;

            //title120: string indicating the name of the similar program. Mandatory.
            /// <summary>
            /// String indicating the name of the similar program. Mandatory.
            /// </summary>
            [DataMember(Name = "title120")]
            public string Title120;
        }

        //--------------------------------------------------------------------------------------------------

        //hasImageArtwork: boolean indicating that there are images available for this program. Optional.
        /// <summary>
        /// Boolean indicating that there are images available for this program. Optional.
        /// </summary>
        [DataMember(Name = "hasImageArtwork")]
        public bool HasImageArtwork;

        //--------------------------------------------------------------------------------------------------

        //duration: Duration of the program without commercials (in integer seconds). Optional.
        /// <summary>
        /// Duration of the program without commercials (in integer seconds). Optional.
        /// </summary>
        [DataMember(Name = "duration")]
        public int Duration;

        //--------------------------------------------------------------------------------------------------

        //episodeImage: Contains a link to an image from this particular episode. Optional.
        /// <summary>
        /// Contains a link to an image from this particular episode. Optional.
        /// </summary>
        [DataMember(Name = "episodeImage")]
        public string EpisodeImage;

        //--------------------------------------------------------------------------------------------------

        //awards: an array containing elements 
        /// <summary>
        /// Array containing elements 
        /// </summary>
        [DataMember(Name = "awards")]
        public SDAward[] Awards;

        //consisting of the following fields:

        [DataContract]
        public class SDAward
        {
            //name: string containing the name of the award. Optional.
            /// <summary>
            /// String containing the name of the award. Optional.
            /// </summary>
            [DataMember(Name = "name")]
            public string Name;

            //awardName: string containing the name of the award. Optional.
            /// <summary>
            /// String containing the name of the award. Optional.
            /// </summary>
            [DataMember(Name = "awardName")]
            public string AwardName;

            //recipient: string containing the name of the recipient. Optional.
            /// <summary>
            /// String containing the name of the recipient. Optional.
            /// </summary>
            [DataMember(Name = "recipient")]
            public string Recipient;

            //personId: personId of the recipient. Optional.
            /// <summary>
            /// PersonId of the recipient. Optional.
            /// </summary>
            [DataMember(Name = "personId")]
            public string PersonId;

            //won: boolean. Optional.
            /// <summary>
            /// Boolean. Optional.
            /// </summary>
            [DataMember(Name = "won")]
            public bool Won;

            //year: string. Year of award. Optional.
            /// <summary>
            /// String. Year of award. Optional.
            /// </summary>
            [DataMember(Name = "year")]
            public string Year;

            //category: string. Optional.
            /// <summary>
            /// String. Optional.
            /// </summary>
            [DataMember(Name = "category")]
            public string Category;
        }

        //--------------------------------------------------------------------------------------------------

        //md5: md5 hash value of the JSON. Mandatory.
        /// <summary>
        /// MD5 hash value of the JSON. Mandatory.
        /// </summary>
        [DataMember(Name = "md5")]
        public string MD5;

        //--------------------------------------------------------------------------------------------------

        //Undocumented Series

        //"resourceID":"8157901"

        [DataMember(Name = "resourceID")]
        public int ResourceID;

        //"metadata":[{"Gracenote":{"season":4,"episode":1}},{"TheTVDB":{"seriesID":183491,"episodeID":0,"season":0,"episode":0}}]

        //"hasImageArtwork":true,"hasSeriesArtwork":true,"hasEpisodeArtwork":true

        [DataMember(Name = "hasSeriesArtwork")]
        public bool HasSeriesArtwork;

        [DataMember(Name = "hasEpisodeArtwork")]
        public bool HasEpisodeArtwork;


        //Undocumented Movie

        //"hasMovieArtwork": true,

        [DataMember(Name = "hasMovieArtwork")]
        public bool HasMovieArtwork;

        //--------------------------------------------------------------------------------------------------

        public SDProgramResponse()
        {
        }
    }
}