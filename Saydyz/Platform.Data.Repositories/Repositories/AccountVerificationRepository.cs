using Platform.Data.Model.Users;
using Platform.Data.Repositories.Context;
using Platform.Data.Repositories.Interfaces;
using Platform.Utilities.UserSession;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Repositories.Repositories
{
    public class AccountVerificationRepository : 
        BaseRepository<AccountVerificationToken, UserContext>, IAccountVerificationRepository
    {
        private readonly IUnitOfWork<UserContext> uow;
        private readonly IConfiguration configuration;
        private readonly IUserSession userSession;



        public DbSet<AccountVerificationToken> AccountVerificationTokens { get; }




        public AccountVerificationRepository(IUnitOfWork<UserContext> uow, IConfiguration configuration,
           IUserSession userSession) : base(uow)
        {
            this.uow = uow;
            this.configuration = configuration;
            this.userSession = userSession;

            var userContext = (UserContext)Context;
            this.AccountVerificationTokens = userContext.AccountVerificationTokens;

        }

        public async Task<int> AddAccountVerificationToken(AccountVerificationToken accountVerificationToken)
        {
            return await AccountVerificationTokens.AddAsync(accountVerificationToken)
             .ContinueWith(roleEntity => _uow.Commit())
             .ContinueWith(saved => saved.Result > 0 ? accountVerificationToken.Id : 0);
        }

        public async Task<AccountVerificationToken> GetLastToken(int userId)
        {
            return await AccountVerificationTokens
                .Where(x => x.UserId == userId)
                .OrderByDescending(a=>a.CreatedOn)
                .FirstOrDefaultAsync();
        }

        public async Task<AccountVerificationToken> GetLastTokenByToken(string token)
        {
            return await AccountVerificationTokens
                 .Where(x => x.VerificationKey == token)                
                 .LastOrDefaultAsync();
        }

        public Task<List<AccountVerificationToken>> GetAllAccountVerificationApprovals(string via)
        {
            return AccountVerificationTokens
                .Include(av => av.User)
                .Where(av =>
                    string.IsNullOrEmpty(via) || (av.VerifiedVia != null && av.VerifiedVia.Equals(via, StringComparison.OrdinalIgnoreCase)))
                .OrderByDescending(av => av.CreatedOn)
                .ToListAsync();

        }

        public Task<AccountVerificationToken> GetAccountVerificationTokenById(int id)
        {
            return AccountVerificationTokens.OrderByDescending(r => r.CreatedOn).FirstOrDefaultAsync(r => r.Id.Equals(id));
        }
    }
}
