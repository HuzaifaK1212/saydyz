using Platform.Data.Model.Configuration;
using System.Threading.Tasks;

namespace Platform.Data.Repositories.Interfaces
{
    public interface IConfigurationParameterRepository : IRepository<ConfigurationParameter>
    {
        Task<ConfigurationParameter> GetByKey(string key);
        Task<ConfigurationParameter> GetParameterByIdAsync(int parameterId);
    }
}
