using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulesDirect {
    public static class SDErrors {
        public static int OK = 0;
        public static int INVALID_JSON = 1001;
        public static int DEFLATE_REQUIRED = 1002;
        public static int TOKEN_MISSING = 1004;
        public static int UNSUPPORTED_COMMAND = 2000;
        public static int REQUIRED_ACTION_MISSING = 2001;
        public static int REQUIRED_REQUEST_MISSING = 2002;
        public static int REQUIRED_PARAMETER_MISSING_COUNTRY = 2004;
        public static int REQUIRED_PARAMETER_MISSING_POSTALCODE = 2005;
        public static int REQUIRED_PARAMETER_MISSING_MSGID = 2006;
        public static int INVALID_PARAMETER_COUNTRY = 2050;
        public static int INVALID_PARAMETER_POSTALCODE = 2051;
        public static int INVALID_PARAMETER_FETCHTYPE = 2052;
        public static int DUPLICATE_LINEUP = 2100;
        public static int LINEUP_NOT_FOUND = 2101;
        public static int UNKNOWN_LINEUP = 2102;
        public static int INVALID_LINEUP_DELETE = 2103;
        public static int LINEUP_WRONG_FORMAT = 2104;
        public static int INVALID_LINEUP = 2105;
        public static int LINEUP_DELETED = 2106;
        public static int LINEUP_QUEUED = 2107;
        public static int INVALID_COUNTRY = 2108;
        public static int STATIONID_NOT_FOUND = 2200;
        public static int SERVICE_OFFLINE = 3000;
        public static int ACCOUNT_EXPIRED = 4001;
        public static int INVALID_HASH = 4002;
        public static int INVALID_USER = 4003;
        public static int ACCOUNT_LOCKOUT = 4004;
        public static int ACCOUNT_DISABLED = 4005;
        public static int TOKEN_EXPIRED = 4006;
        public static int MAX_LINEUP_CHANGES_REACHED = 4100;
        public static int MAX_LINEUPS = 4101;
        public static int NO_LINEUPS = 4102;
        public static int IMAGE_NOT_FOUND = 5000;
        public static int INVALID_PROGRAMID = 6000;
        public static int PROGRAMID_QUEUED = 6001;
        public static int SCHEDULE_NOT_FOUND = 7000;
        public static int INVALID_SCHEDULE_REQUEST = 7010;
        public static int SCHEDULE_RANGE_EXCEEDED = 7020;
        public static int SCHEDULE_NOT_IN_LINEUP = 7030;
        public static int SCHEDULE_QUEUED = 7100;
        public static int HCF = 9999;
    }
}