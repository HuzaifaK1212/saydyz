using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Platform.Data.Model.Configuration;
using Platform.Data.Repositories.Context;
using Platform.Data.Repositories.Interfaces;
using Platform.Utilities;
using Platform.Utilities.UserSession;

namespace Platform.Services.ConfigurationService
{
    public class ConfigurationService : IConfigurationService
    {

        private readonly IUserSession UserSession;
        private IUserRepository UserRepo;

        private IConfigurationProfileRepository ConfigurationProfileRepo;
        private IConfigurationParameterRepository ConfigurationParameterRepo;

        public ConfigurationService(IUserSession userSession, IUserRepository userRepo, IConfigurationProfileRepository configurationProfileRepo, IConfigurationParameterRepository configurationParameterRepo)
        {
            this.UserSession = userSession;
            this.UserRepo = userRepo;

            this.ConfigurationProfileRepo = configurationProfileRepo;
            this.ConfigurationParameterRepo = configurationParameterRepo;
        }


        public async Task<Response<List<ConfigurationProfile>>> GetAllConfigurationProfiles()
        {
            var profiles = await ConfigurationProfileRepo.GetAllConfigurationProfiles();

            if (profiles == null || profiles.Count <= 0)
                return new Response<List<ConfigurationProfile>>()
                {
                    Success = false,
                    Message = $"No configuration profiles found."
                };

            return new Response<List<ConfigurationProfile>>()
            {
                Success = true,
                Message = $"[{profiles.Count}] Configuration profiles fetched successfully.",
                Data = profiles
            };
        }

        public async Task<Response<ConfigurationProfile>> GetAllConfigurations(string profileKey)
        {
            if (string.IsNullOrEmpty(profileKey) || string.IsNullOrWhiteSpace(profileKey))
                return new Response<ConfigurationProfile>() { 
                    Success = false,
                    Message = "Invalid profile key."
                };


            var profile = await ConfigurationProfileRepo.GetProfileByKey(profileKey);

            if(profile == null)
                return new Response<ConfigurationProfile>()
                {
                    Success = false,
                    Message = $"No profile found for Key [{profileKey}]."
                };

            return new Response<ConfigurationProfile>()
            {
                Success = true,
                Message = $"Configuration profile fetched successfully for key [{profileKey}].",
                Data = profile
            };
        }

        public async Task<Response<ConfigurationProfile>> GetConfigurationProfileById(int profileId)
        {
            var profile = await ConfigurationProfileRepo.GetProfileById(profileId);

            if (profile == null)
                return new Response<ConfigurationProfile>()
                {
                    Success = false,
                    Message = $"No profile found for ID [{profileId}]."
                };

            return new Response<ConfigurationProfile>()
            {
                Success = true,
                Message = $"Configuration profile fetched successfully for ID [{profileId}].",
                Data = profile
            };
        }


        /// <summary>
        /// Add ConfigurationProfile
        /// </summary>
        /// <param name="m">ConfigurationProfile</param>
        /// <returns>Id of newly created ConfigurationProfile. </int></returns>
        public async Task<Response<int>> AddConfigurationProfile(ConfigurationProfile m)
        {
            if (String.IsNullOrEmpty(m.Key) || String.IsNullOrWhiteSpace(m.Key))
                throw new ArgumentException("Name required for adding ConfigurationProfile");

            var configurationProfileCountByKey = await ConfigurationProfileRepo.GetConfigurationProfileCountByKey(m.Key);

            if (configurationProfileCountByKey > 0)
                throw new ArgumentException($"ConfigurationProfile already defined with Name [{m.Name}]");

            var ConfigurationProfileId = await ConfigurationProfileRepo.AddConfigurationProfile(m);
            return new Response<int>()
            {
                Success = ConfigurationProfileId > 0,
                Message = ConfigurationProfileId > 0 ? "ConfigurationProfile added successfully" : "ConfigurationProfile addition failed.",
                Data = ConfigurationProfileId
            };
        }
        /// <summary>
        /// Update ConfigurationProfile
        /// </summary>
        /// <param name="m">Updated ConfigurationProfile</param>
        /// <returns>ConfigurationProfile with updated values</returns>
        public async Task<Response<ConfigurationProfile>> UpdateConfigurationProfile(ConfigurationProfile m)
        {
            var ConfigurationProfile = ConfigurationProfileRepo.GetById(m.Id);
            if (ConfigurationProfile == null)
                return new Response<ConfigurationProfile>()
                {
                    Success = false,
                    Message = $"Configuration Profile not found for ID [{m.Id}]"
                };

            if (!m.Key.Equals(ConfigurationProfile.Key))
            {
                var riskCountByName = await ConfigurationProfileRepo.GetConfigurationProfileCountByKey(m.Name);
                if (riskCountByName > 0)
                    throw new ArgumentException($"Configuration Profile already defined with Name [{m.Name}]");

            }

            ConfigurationProfile.Name = m.Name;
            ConfigurationProfile.Description = m.Description;
            ConfigurationProfile.Active = m.Active;


            ConfigurationProfile.UpdatedBy = UserSession.LoginUser.Id;
            ConfigurationProfile.UpdatedOn = DateTime.UtcNow;

            ConfigurationProfileRepo.Update(m);
            int x = ConfigurationProfileRepo.GetUow<ConfigurationContext>().Commit();
            return new Response<ConfigurationProfile>()
            {
                Success = x > 0,
                Message = x > 0 ? "Configuration profile updated successfully" : "Configuration profile update failed",
                Data = x > 0 ? ConfigurationProfile : null
            };
        }

        public async Task<Response<List<ConfigurationProfile>>> GetAllConfigurationProfiles(int pageNo, int limit)
        {
            var profiles = await ConfigurationProfileRepo.GetAllConfigurationProfiles(pageNo,limit);

            if (profiles == null || profiles.Count <= 0)
                return new Response<List<ConfigurationProfile>>()
                {
                    Success = false,
                    Message = $"No configuration profiles found."
                };

            return new Response<List<ConfigurationProfile>>()
            {
                Success = true,
                Message = $"[{profiles.Count}] configuration profiles fetched successfully.",
                Data = profiles
            };
        }

        public async Task<Response<ConfigurationProfile>> AddOrUpdateAllConfigurationParameters(List<ConfigurationParameter> configurationParameters)
        {
            if (configurationParameters.Count == 0)
                return new Response<ConfigurationProfile>()
                {
                    Success = false,
                    Message = "No configuration parameters provided."
                };

            var profileId = configurationParameters.FirstOrDefault().ProfileId;

            var profile = await ConfigurationProfileRepo.GetProfileById(profileId);

            if (profile == null)
                return new Response<ConfigurationProfile>()
                {
                    Success = false,
                    Message = $"Invalid configuration profile ID provided [{profileId}]"
                };

            foreach(var parameter in configurationParameters)
            {

                if (string.IsNullOrEmpty(parameter.Key) || string.IsNullOrWhiteSpace(parameter.Key))
                    continue;

                parameter.Profile = profile;

                if (parameter.Id == 0)
                {
                    //add
                    parameter.CreatedBy = UserSession.LoginUser.Id;
                    parameter.CreatedOn = DateTime.UtcNow;
                    parameter.Active = true;

                    ConfigurationParameterRepo.Add(parameter);
                }else
                {
                    //update

                    var p = ConfigurationParameterRepo.GetById(parameter.Id);

                    if (p == null)
                        continue;

                    p.Name = parameter.Name;
                    p.Key = parameter.Key;
                    p.Value = parameter.Value;
                    p.Type = parameter.Type;


                    p.UpdatedBy = UserSession.LoginUser.Id;
                    p.UpdatedOn = DateTime.UtcNow;
                    p.Active = true;

                    ConfigurationParameterRepo.Update(p);
                }
            }

            var x = ConfigurationProfileRepo.GetUow<ConfigurationContext>().Commit();

            return new Response<ConfigurationProfile>()
            {
                Success = x > 0,
                Message = x > 0 ? $"[{x}] configuration parameters updated successfully" : $"Configuration parameter update failed for profile [{profileId}]",
                Data = x > 0 ? profile : null

            };
        }

        public async Task<Response<ConfigurationParameter>> GetConfigurationParameterByProfileAndKey(string profile, int parameterId)
        {
            var configurationParameter =
                await ConfigurationProfileRepo.GetByProfileAndParameterId(profile, parameterId);

            if (configurationParameter == null)
                return new Response<ConfigurationParameter>()
                {
                    Message = $"No parameter available with ID [{parameterId}] for profile [{profile}]"
                };

            return new Response<ConfigurationParameter>()
            {
                Success = true,
                Message = $"Configuration parameter ID [{parameterId}] fetched successfully for profile [{profile}]",
                Data = configurationParameter
            };
        }

        public async Task<Response<ConfigurationParameter>> GetConfigurationParameterById(int parameterId)
        {
            var configurationParameter = await ConfigurationParameterRepo.GetParameterByIdAsync(parameterId);
            if (configurationParameter == null)
                return new Response<ConfigurationParameter>()
                {
                    Message = $"No configuration parameter found for ID [{parameterId}]"
                };
            
            return new Response<ConfigurationParameter>()
            {
                Success = true,
                Message = $"No configuration parameter found for ID [{parameterId}]",
                Data = configurationParameter
            };

        }
    }
}
