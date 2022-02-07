namespace Platform.Data.Model.Logs
{
    public class LogMsg : BaseEntity
    {
        public int LogRequestId { get; set; }
        public LogRequest LogRequest { get; set; }
        public string LogMsgType { get; set; }
        public string Msg { get; set; }
        public string StackTrace { get; set; }      
    } 
}
