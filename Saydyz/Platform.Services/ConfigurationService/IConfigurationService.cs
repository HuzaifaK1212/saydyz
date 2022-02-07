using Platform.Data.Model.Configuration;
using Platform.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.Services.ConfigurationService
{
    public interface IConfigurationService
    {
        Task<Response<ConfigurationProfile>> GetAllConfigurations(string profileKey);
        Task<Response<ConfigurationProfile>> GetConfigurationProfileById(int profileId);
        Task<Response<List<ConfigurationProfile>>> GetAllConfigurationProfiles();
        Task<Response<List<ConfigurationProfile>>> GetAllConfigurationProfiles(int pageNo, int limit);
        Task<Response<int>> AddConfigurationProfile(ConfigurationProfile m);
        Task<Response<ConfigurationProfile>> UpdateConfigurationProfile(ConfigurationProfile m);
        Task<Response<ConfigurationProfile>> AddOrUpdateAllConfigurationParameters(List<ConfigurationParameter> list);
        Task<Response<ConfigurationParameter>> GetConfigurationParameterByProfileAndKey(string profile, int parameterId);
        Task<Response<ConfigurationParameter>> GetConfigurationParameterById(int parameterId);
    }
}
