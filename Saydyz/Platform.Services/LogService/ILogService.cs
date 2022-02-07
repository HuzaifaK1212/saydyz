using Platform.Data.Model.Logs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.Services.LogService
{
    public interface ILogService 
    {
        void StartLog(string requestUrl, string method, string host, string data);
        void EndLog(int responseCode);
        void Log(string logMsg, LogTypes logType);
        void LogError(string errorMsg, string stackTrace);
        Task<int> GetLogRequestCount();
        Task<int> GetLogRequestCount(string filter);
        Task<List<LogRequest>> GetLogRequest(int pageIndex, int pageSize);
        Task<List<LogRequest>> GetLogRequestAsync(int pageIndex, int pageSize, string filter);
        Task<int> GetLogMessagesCount(int logRequestId);
        Task<List<LogMsg>> GetLogMessages(int logRequestId);
    }
}
