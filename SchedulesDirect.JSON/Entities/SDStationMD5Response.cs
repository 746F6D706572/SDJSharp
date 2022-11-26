using System;

namespace SchedulesDirect {
    public class SDStationMD5Response {
        public string StationId;
        //public List<SDMD5Day> Days;
        public SDMD5Day[] Days;

        public class SDMD5Day {
            /// <summary>
            /// YYYY-MM-DD
            /// </summary>
            public string Date;
            public SDMD5Metadata Metadata;

            public class SDMD5Metadata {
                public int Code;
                public string Message;
                public DateTime? LastModified;
                public string MD5;
            }

            public SDMD5Day() {
                Metadata = new SDMD5Metadata();
            }
        }
    }
}