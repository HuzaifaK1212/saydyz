using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Platform.Data.Model.Configuration;
using Platform.Data.Repositories.Context;
using Platform.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Data.Repositories
{
    public class ConfigurationProfileRepository : BaseRepository<ConfigurationProfile, ConfigurationContext>, IConfigurationProfileRepository
    {

        public DbSet<ConfigurationProfile> ConfigurationProfiles { get; set; }

        private string KEY;

        public ConfigurationProfileRepository(IUnitOfWork<ConfigurationContext> uow, IConfiguration configuration) : base(uow)
        {
            var configContext = (ConfigurationContext)Context;


            this.ConfigurationProfiles = configContext.ConfigurationProfiles;

            this.KEY = configuration["ConfigurationProfile"];
        }

        public async Task<ConfigurationParameter> GetParameterByKey(string k)
        {
            return await ConfigurationProfiles.Where(c => c.Key.Equals(this.KEY))
                .Include(c => c.Parameters)
                .SelectMany(c => c.Parameters)
                .Where(cp => cp.Key.Equals(k))
                .FirstOrDefaultAsync();
        }

        public async Task<ConfigurationParameter> GetByProfileAndKey(string profileKey, string k)
        {
            return await ConfigurationProfiles.Where(c => c.Key.Equals(profileKey))
                .Include(c => c.Parameters)
                .SelectMany(c => c.Parameters)
                .Where(cp => cp.Key.Equals(k))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the value directly casted if the casting provided is a valid one.
        /// 
        /// Can throw Exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="profileKey">Profile Key</param>
        /// <param name="k">Profile Parameter</param>
        /// <returns>Type-casted required value</returns>
        public async Task<T> GetValueByProfileAndKey<T>(string profileKey, string k)
        {
            var param = await ConfigurationProfiles.Where(c => c.Key.Equals(profileKey))
                .Include(c => c.Parameters)
                .SelectMany(c => c.Parameters)
                .Where(cp => cp.Key.Equals(k))
                .FirstOrDefaultAsync();

            if (param == null)
                return default;

            return (T)Convert.ChangeType(param.Value, typeof(T)); ;
        }

        /// <summary>
        /// Gets the value directly casted if the casting provided is a valid one.
        /// 
        /// Can throw Exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="k">Profile Parameter</param>
        /// <returns>Type-casted required value</returns>
        public async Task<T> GetValueByKey<T>(string k)
        {
            var param = await ConfigurationProfiles.Where(c => c.Key.Equals(this.KEY))
                .Include(c => c.Parameters)
                .SelectMany(c => c.Parameters)
                .Where(cp => cp.Key.Equals(k))
                .FirstOrDefaultAsync();

            if (param == null)
                return default;

            return (T)Convert.ChangeType(param.Value, typeof(T)); ;
        }

        public async Task<T> GetValueByKey<T>(string k, T defaultValue)
        {
            var param = await ConfigurationProfiles.Where(c => c.Key.Equals(this.KEY))
                .Include(c => c.Parameters)
                .SelectMany(c => c.Parameters)
                .Where(cp => cp.Key.Equals(k))
                .FirstOrDefaultAsync();

            if (param == null)
                return defaultValue;

            return (T)Convert.ChangeType(param.Value, typeof(T)); ;
        }

        public async Task<ConfigurationParameter> GetByProfileAndParameterId(string profile, int parameterId)
        {
            return await ConfigurationProfiles.Where(c => c.Key.Equals(profile))
                .Include(c => c.Parameters)
                .SelectMany(c => c.Parameters)
                .Where(cp => cp.Id.Equals(parameterId))
                .FirstOrDefaultAsync();
        }

        public async Task<ConfigurationProfile> GetProfileByKey(string profileKey)
        {
            return await ConfigurationProfiles.Where(c => c.Key.Equals(profileKey))
                .Include(c => c.Parameters)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ConfigurationProfile>> GetAllConfigurationProfiles()
        {
            return await ConfigurationProfiles
                .Include(c => c.Parameters)
                .ToListAsync();
        }

        public async Task<ConfigurationProfile> GetProfileById(int profileId)
        {
            return await ConfigurationProfiles.Where(c => c.Id.Equals(profileId))
                .Include(c => c.Parameters)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ConfigurationProfile>> GetAllConfigurationProfiles(int pageNo, int limit)
        {
            return await ConfigurationProfiles
                .Include(c => c.Parameters)
                .Skip(pageNo * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<int> AddConfigurationProfile(ConfigurationProfile m)
        {
            return 0;
        }

        public async Task<int> GetConfigurationProfileCountByKey(string key)
        {
            return await ConfigurationProfiles.Where(c => c.Key.Equals(key))
                .Include(c => c.Parameters)
                .CountAsync();
        }
    }
}
