using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// MD5 hash request structure. Contains stations and date ranges to request hash values for
    /// </summary>
    [DataContract]
    public class SDStationMD5Request {
        [DataMember(Name = "stationID", IsRequired = true)]
        public string StationID;
        /// <summary>
        /// Array of date strings in YYYY-MM-DD format
        /// </summary>
        [DataMember(Name = "date", IsRequired = false, EmitDefaultValue = false)]
        public string[] Date;
        /// <summary>
        /// Gets MD5 data for a station ID
        /// </summary>
        /// <param name="station">ID of the station to get MD5 data for</param>
        /// <param name="days">Number of days starting from the current date to get MD5 data for</param>
        public SDStationMD5Request(string station, int days)
            :this(station, DateTime.Now, days) {
        }
        /// <summary>
        /// Gets MD5 data for a station ID
        /// </summary>
        /// <param name="station">ID of the station to get MD5 data for</param>
        /// <param name="startDate">First date to get MD5 data for</param>
        /// <param name="days">Number of days to get MD5 data for</param>
        public SDStationMD5Request(string station, DateTime startDate, int days)
            : this(station, Enumerable.Range(0, days).Select(x => startDate.Date.AddDays(x)).AsEnumerable()) {
        }
        /// <summary>
        /// Gets MD5 data for a station ID
        /// </summary>
        /// <param name="station">ID of the station to get MD5 data for</param>
        /// <param name="dates">Array of dates to get MD5 data for</param>
        public SDStationMD5Request(string station, IEnumerable<DateTime> dates) {
            StationID = station;
            var dateStrings = new List<string>();
            foreach (var thisDate in dates)
                dateStrings.Add(thisDate.ToString("yyyy-MM-dd"));
            Date = dateStrings.ToArray();
        }
        /// <summary>
        /// Gets all available MD5 data for a station ID
        /// </summary>
        /// <param name="station">ID of the station to get MD5 data for</param>
        public SDStationMD5Request(string station) {
            StationID = station;
        }
        public SDStationMD5Request() {
		}
    }
}