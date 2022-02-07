using Platform.Data.Model.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Repositories.Interfaces
{
    public interface IAccountVerificationRepository : IRepository<AccountVerificationToken>
    {
        Task<int> AddAccountVerificationToken(AccountVerificationToken accountVerificationToken);

        Task<AccountVerificationToken> GetLastToken(int userId);

        Task<AccountVerificationToken> GetLastTokenByToken(string token);
        Task<List<AccountVerificationToken>> GetAllAccountVerificationApprovals(string via);
        Task<AccountVerificationToken> GetAccountVerificationTokenById(int id);
    }
}
