using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SchedulesDirect {
    [DataContract]
    public class SDCachedElement {
        private DateTime? _cacheDate;
        public DateTime? cacheDate {
            get => _cacheDate ?? DateTime.UtcNow;
            set => _cacheDate = value;
        }
    }
}
