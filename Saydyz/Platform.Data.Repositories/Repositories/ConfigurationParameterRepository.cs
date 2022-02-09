using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Platform.Data.Model.Configuration;
using Platform.Data.Repositories.Context;
using Platform.Data.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Data.Repositories
{
    public class ConfigurationParameterRepository : BaseRepository<ConfigurationParameter, ConfigurationContext>, IConfigurationParameterRepository
    {
        public DbSet<ConfigurationParameter> ConfigurationParameteres { get; set; }

        public ConfigurationParameterRepository(IUnitOfWork<ConfigurationContext> uow, IConfiguration configuration) : base(uow)
        {
            var configContext = (ConfigurationContext)Context;
            this.ConfigurationParameteres  = configContext.ConfigurationParameters;
        }

        public async  Task<ConfigurationParameter> GetByKey(string k)
        {
            return await this.ConfigurationParameteres
                .Include(x=>x.Profile)
                .Where(s=>s.Key.Equals(k)).FirstOrDefaultAsync();
        }

        public async Task<ConfigurationParameter> GetParameterByIdAsync(int parameterId)
        {
            return await this.ConfigurationParameteres
                .Include(x=>x.Profile)
                .Where(s=>s.Id.Equals(parameterId))
                .FirstOrDefaultAsync();        }
    }
}
