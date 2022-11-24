using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Schedule request. Provides information to server regarding required station ID and date ranges to include program data for.
    /// </summary>
    [DataContract]
    public class SDScheduleRequest : IEquatable<SDScheduleRequest> {
        [DataMember]
        public string stationID;
        [DataMember]
        public string[] date;

        public SDScheduleRequest(string station, IEnumerable<DateTime> dates) {
            stationID = station;
            var dateStrings = new List<string>();
            foreach (var thisDate in dates)
                dateStrings.Add(thisDate.ToString("yyyy-MM-dd"));
            date = dateStrings.ToArray();
        }

        public bool Equals(SDScheduleRequest compare) {
            if (compare.stationID == stationID)
                return true;

            return false;
        }
    }
}
