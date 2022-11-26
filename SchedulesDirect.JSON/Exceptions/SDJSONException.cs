using System;

namespace SchedulesDirect
{
    [Serializable]
    public class SchedulesDirectException : Exception
    {
        private static readonly string DefaultMessage = "Unknown Schedules Direct Error.";
        public string Response { get; set; }
        public int Code { get; set; }
        public string ServerID { get; set; }
        public DateTime? DateTime { get; set; }
        public SchedulesDirectException() : base(DefaultMessage) 
        { 
        }
        public SchedulesDirectException(string message) : base(message) 
        { 
        }
        public SchedulesDirectException(Exception innerException) : base(DefaultMessage, innerException) 
        { 
        }
        public SchedulesDirectException(string message, Exception innerException) : base(message, innerException) 
        { 
        }
        public SchedulesDirectException(SDErrorResponse errorResponse) : this(errorResponse.Response, errorResponse.Code, errorResponse.DateTime, errorResponse.ServerID, errorResponse.Message) 
        { 
        }
        public SchedulesDirectException(SDErrorResponse errorResponse, Exception innerException) : this(errorResponse.Response, errorResponse.Code, errorResponse.DateTime, errorResponse.ServerID, errorResponse.Message, innerException) 
        { 
        }
        public SchedulesDirectException(string response, int code, DateTime? dateTime) : base(DefaultMessage)
        {
            Response = response;
            Code = code;
            DateTime = dateTime;
            ServerID = null;
        }
        public SchedulesDirectException(string response, int code, DateTime? dateTime, string serverID) : base(DefaultMessage)
        {
            Response = response;
            Code = code;
            DateTime = dateTime;
            ServerID = serverID;
        }
        public SchedulesDirectException(string response, int code, DateTime? dateTime, string serverID, string message) : base(message)
        {
            Response = response;
            Code = code;
            DateTime = dateTime;
            ServerID = serverID;
        }
        public SchedulesDirectException(string response, int code, DateTime? dateTime, string serverID, Exception innerException) : base(DefaultMessage, innerException)
        {
            Response = response;
            Code = code;
            DateTime = dateTime;
            ServerID = serverID;
        }
        public SchedulesDirectException(string response, int code, DateTime? dateTime, string serverID, string message, Exception innerException) : base(message, innerException)
        {
            Response = response;
            Code = code;
            DateTime = dateTime;
            ServerID = serverID;
        }
        public SchedulesDirectException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) 
        {        
        }
    }
}

