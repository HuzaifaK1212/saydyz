using Platform.Data.Model.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.Data.Repositories.Interfaces
{
    public interface IConfigurationProfileRepository : IRepository<ConfigurationProfile>
    {
        Task<ConfigurationParameter> GetParameterByKey(string k);
        Task<ConfigurationParameter> GetByProfileAndKey(string profileKey, string k);
        Task<ConfigurationProfile> GetProfileByKey(string profileKey);
        Task<List<ConfigurationProfile>> GetAllConfigurationProfiles();
        Task<List<ConfigurationProfile>> GetAllConfigurationProfiles(int pageNo, int limit);
        Task<ConfigurationProfile> GetProfileById(int profileId);
        Task<int> AddConfigurationProfile(ConfigurationProfile m);
        Task<int> GetConfigurationProfileCountByKey(string key);
        Task<T> GetValueByProfileAndKey<T>(string profileKey, string k);
        Task<T> GetValueByKey<T>(string k);
        Task<T> GetValueByKey<T>(string k, T defaultValue);
        Task<ConfigurationParameter> GetByProfileAndParameterId(string profile, int parameterId);
    }
}
