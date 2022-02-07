using Platform.Data.Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Utilities.UserSession
{
    public class UserSession : IUserSession
    {
        private User _logInUser = null;
        public User LoginUser => _logInUser;

        public void SetLoginUser(User loginUser)
        {
            if (_logInUser != null)
                throw new Exception("User session already exist.");

            _logInUser = loginUser;
        }

        public string lastMessageDateTime(DateTime createdDate)
        {
            string LastMessage = "";

            TimeSpan date = DateTime.UtcNow - createdDate;

            if (date.Days == 0 && date.Hours == 0 && date.Minutes == 0)
            {
                LastMessage = "now";
            }
            else if (date.Days > 0)
            {
                LastMessage = date.Days.ToString() + " d ago";
            }
            else if (date.Hours > 0)
            {
                LastMessage = date.Hours.ToString() + " h ago";
            }
            else if (date.Minutes > 0)
            {
                LastMessage = date.Minutes.ToString() + " min ago";
            }
            else if (date.Seconds > 0)
            {
                LastMessage = date.Seconds.ToString() + " s ago";
            }

            return LastMessage;
        }
    }
}
