using Platform.Data.Model.Logs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.Data.Repositories.Interfaces
{
    public interface ILogRepository : IRepository<LogRequest>
    {
        Task<int> GetLogRequestCount();
        Task<int> GetLogRequestCount(string filter);
        Task<List<LogRequest>> GetLogRequest(int pageIndex, int pageSize);
        Task<List<LogRequest>> GetLogRequestAsync(int pageIndex, int pageSize, string filter);
        Task<int> GetLogMessagesCount(int logRequestId);
        Task<List<LogMsg>> GetLogMessagesAsync(int logRequestId);
    }
}
