
using Platform.Data.Model.Internals;
using System;
using System.Linq;

namespace Platform.Data.Model.Extensions
{
    public static class Extensions
    {
  
        public static Notification.Notification CreateNotificationFromActivityLog(this ActivityLog activityLog)
        {
            var key = activityLog.Path;
            var apiStart = key.IndexOf("/api/", StringComparison.Ordinal);

            if (apiStart < 0)
                return null;

            var indexingStart = apiStart + ("/api/".Length) - 1;

            //unable to create the entity from indexing path
            if (indexingStart > key.Length)
                return null;

            var basePath = key.Substring(indexingStart);
            var information = basePath.Split("/").Where(s => !string.IsNullOrEmpty(s)).ToArray();

            var action = "";
            var entity = "n/a";


            var module = "n/a";
            var baseModule = "";
            var pathAction = "";


            switch (information.Length)
            {
                case 0:
                    return null;
                case 1:
                case 2:
                    baseModule = information[0];
                    entity = information[0];
                    module = information[0];
                    pathAction = information[information.Length - 1];
                    break;

                default:
                    baseModule = information[0];
                    entity = information[information.Length - 2];
                    module = information[information.Length - 3]; //just before the entity
                    pathAction = information[information.Length - 1];
                    break;
            }

            if (information.Contains("login"))
            {
                action = "performed";
                entity = "login";
            }
            else
            {
                if (activityLog.Action.Equals("post", StringComparison.OrdinalIgnoreCase))
                    action = "created";
                else if (activityLog.Action.Equals("put", StringComparison.OrdinalIgnoreCase))
                    action = "updated";
                else if (activityLog.Action.Equals("delete", StringComparison.OrdinalIgnoreCase))
                    action = "deleted";
            }

            //simple concatenation of url with ids removed.
            var f = "";
            for (var i = 0; i < information.Length; i++)
            {
                try
                {
                    // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                    int.Parse(information[i]);
                    continue;
                }
                catch (Exception e)
                {
                    // ignored
                }

                if (i == information.Length - 1)
                    f += $"{information[i]}";
                else
                    f += $"{information[i]}.";
            }

            return new Notification.Notification()
            {
                For = f,
                Text = $"{activityLog.UserName} {action} {entity} on {activityLog.CreatedOn.ToShortDateString()}",
                Time = activityLog.CreatedOn,
                GeneratedBy = activityLog.UserName
            };
        }

      

       

    }
}