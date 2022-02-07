using Platform.Data.Model.Status;
using Platform.Data.Model.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Repositories.Interfaces
{
    public interface IConfigurationParameterRepository : IRepository<ConfigurationParameter>
    {
        Task<ConfigurationParameter> GetByKey(string key);
        Task<ConfigurationParameter> GetParameterByIdAsync(int parameterId);
    }
}
