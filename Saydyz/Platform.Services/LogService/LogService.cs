using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Platform.Data.Model.Logs;
using Platform.Data.Repositories.Context;
using Platform.Data.Repositories.Interfaces;

namespace Platform.Services.LogService
{
    public class LogService : ILogService
    {
        private ILogRepository logRepository;
        private LogRequest logRequest;

        public LogService(ILogRepository logRepository)
        {
            this.logRepository = logRepository;
        }

        public void StartLog(string requestUrl, string requestMethod, string host, string data)
        {
            logRequest = new LogRequest();
            logRequest.RequestUrl = requestUrl;
            logRequest.RequestStartTime = DateTime.UtcNow;
            logRequest.Method = requestMethod;
            logRequest.Host = host;
            logRequest.Data = data;
            logRequest.Active = true;
            logRequest.CreatedOn = DateTime.UtcNow;
            logRequest.UpdatedOn = DateTime.UtcNow;
            logRequest.UpdatedBy = logRequest.CreatedBy = 0;
        }

        public void EndLog(int responseCode)
        {
            try
            {
                logRequest.RequestEndTime = DateTime.UtcNow;
                logRequest.ResponseCode = responseCode;
                logRepository.Add(logRequest);
                logRepository.GetUow<LogContext>().Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating End log. Reason: {ex.Message}");
            }
        }

        public void Log(string logMsg, LogTypes logType)
        {
            if (logRequest == null)
                throw new Exception("Log has not been started.");

            LogMsg msg = new LogMsg();
            msg.CreatedBy = 0;
            msg.UpdatedBy = 0;
            msg.CreatedOn = DateTime.UtcNow;
            msg.UpdatedOn = DateTime.UtcNow;
            msg.LogMsgType = logType.ToString();
            msg.Msg = logMsg;
            msg.Active = true;
            logRequest.LogMsgs.Add(msg);
        }

        public void LogError(string errorMsg, string stackTrace)
        {
            LogMsg msg = new LogMsg();
            msg.CreatedBy = 0;
            msg.UpdatedBy = 0;
            msg.CreatedOn = DateTime.UtcNow;
            msg.UpdatedOn = DateTime.UtcNow;
            msg.LogMsgType = LogTypes.Error.ToString();
            msg.Msg = errorMsg;
            msg.StackTrace = stackTrace;
            msg.Active = true;
            logRequest.LogMsgs.Add(msg);
        }

        public async Task<int> GetLogRequestCount()
        {
            return await logRepository.GetLogRequestCount();
        }

        public async Task<int> GetLogRequestCount(string filter)
        {
            return await logRepository.GetLogRequestCount(filter);
        }

        public async Task<List<LogRequest>> GetLogRequest(int pageIndex, int pageSize)
        {
            return await logRepository.GetLogRequest(pageIndex, pageSize);
        }

        public async Task<List<LogRequest>> GetLogRequestAsync(int pageIndex, int pageSize, string filter)
        {
            return await logRepository.GetLogRequestAsync(pageIndex, pageSize, filter);
        }

        public async Task<int> GetLogMessagesCount(int logRequestId)
        {
            return await logRepository.GetLogMessagesCount(logRequestId);
        }

        public async Task<List<LogMsg>> GetLogMessages(int logRequestId)
        {
            return await logRepository.GetLogMessagesAsync(logRequestId);
        }
    }
}
