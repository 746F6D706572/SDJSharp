using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SchedulesDirect {
    /// <summary>
    /// Schedule request. Provides information to server regarding required station ID and date ranges to include program data for.
    /// </summary>
    [DataContract]
    public class SDStationScheduleRequest : IEquatable<SDStationScheduleRequest> {
		/// <summary>
        /// ID of the station to get schedules for
        /// </summary>
        [DataMember(Name = "stationID")]
        public string StationID;
        /// <summary>
        /// Array of date strings in YYYY-MM-DD format
        /// </summary>
        [DataMember(Name = "date", EmitDefaultValue = false)]
        public string[] Date;
		/// <summary>
        /// Gets schedules for a station
        /// </summary>
        /// <param name="station">ID of the station to get schedules for</param>
        /// <param name="days">Number of days starting from the current date to get schedules for</param>
        public SDStationScheduleRequest(string station, int days)
            : this(station, DateTime.Now, days) {
        }
        /// <summary>
        /// Gets schedules for a station
        /// </summary>
        /// <param name="station">ID of the station to get schedules for</param>
        /// <param name="startDate">First date to get schedules for</param>
        /// <param name="days">Number of days to get schedules for</param>
        public SDStationScheduleRequest(string station, DateTime startDate, int days)
            : this(station, Enumerable.Range(0, days).Select(x => startDate.Date.AddDays(x)).AsEnumerable()) {
        }
        /// <summary>
        /// Gets schedules for a station
        /// </summary>
        /// <param name="station">ID of the station to get schedules for</param>
        /// <param name="dates">Array of dates to get schedules for</param>
        public SDStationScheduleRequest(string station, IEnumerable<DateTime> dates) {
            StationID = station;
            var dateStrings = new List<string>();
            foreach (var thisDate in dates)
                dateStrings.Add(thisDate.ToString("yyyy-MM-dd"));
            Date = dateStrings.ToArray();
        }
		/// <summary>
        /// Gets all available schedules for a station
        /// </summary>
        /// <param name="station">ID of the station to get schedules for</param>
        public SDStationScheduleRequest(string station) {
            StationID = station;
        }

        public bool Equals(SDStationScheduleRequest compare) {
            if (compare.StationID == StationID)
                return true;
            return false;
        }
    }
}