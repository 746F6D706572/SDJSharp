using System;

namespace SchedulesDirect {
    /// <summary>
    /// MD5 hash response structure. Generated from dynamic data.
    /// </summary>
    public class SDMD5Response {
        public string stationID;
        public SDMD5Day[] md5day;

        public class SDMD5Day {
            public string date;
            public SDMD5Data md5data;

            public class SDMD5Data {
                public int code;
                public string message;
                public DateTime? lastModified;
                public string md5;
            }

            public SDMD5Day() {
                md5data = new SDMD5Data();
            }
        }
    }
}
