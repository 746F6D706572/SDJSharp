using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// MD5 hash request structure. Contains stations and date ranges to request hash values for
    /// </summary>
    [DataContract]
    public class SDMD5Request {
        [DataMember]
        public string stationID;
        [DataMember]
        public string[] date;

        public SDMD5Request() {

        }

        public SDMD5Request(string station, IEnumerable<DateTime> dates) {
            stationID = station;
            //string dateStart = start.ToString("yyyy-MM-dd");
            //string dateEnd = end.ToString("yyyy-MM-dd");
            var dateStrings = new List<string>();
            foreach (var thisDate in dates)
                dateStrings.Add(thisDate.ToString("yyyy-MM-dd"));
            date = dateStrings.ToArray();
        }

    }
}
