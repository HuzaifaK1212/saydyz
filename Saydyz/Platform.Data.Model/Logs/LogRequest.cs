using System;
using System.Collections.Generic;

namespace Platform.Data.Model.Logs
{
    public class LogRequest: BaseEntity
    {
        public LogRequest()
        {
            LogMsgs = new List<LogMsg>();
        }

        public string RequestUrl { get; set; }
        public DateTime RequestStartTime { get; set; }
        public DateTime RequestEndTime { get; set; }
        public int ResponseCode { get; set; }

        public string Host { get; set; }
        public string Data { get; set; }

        public string Method { get; set; }
        public string Message { get; set; }
        public List<LogMsg> LogMsgs { get; set; }
    }
}
