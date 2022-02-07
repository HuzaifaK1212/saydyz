using Platform.Data.Model.Users;
using System;

namespace Platform.Utilities.UserSession
{
    public interface IUserSession
    {
        User LoginUser { get; }

        void SetLoginUser(User loginUser);

        string lastMessageDateTime(DateTime createdDate);
    }
}
