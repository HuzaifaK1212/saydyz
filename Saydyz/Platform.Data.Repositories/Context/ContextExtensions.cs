using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Platform.Data.Model.Configuration;
using Platform.Data.Model.Logs;
using Platform.Data.Model.Notification;
using Platform.Utilities.Hash;
using RestSharp;
using System;
using System.IO;

namespace Platform.Data.Repositories.Context
{
    public static class ContextExtensions
    {
        public static void ConstructDatabaseModel(this ModelBuilder modelBuilder, IConfiguration configuration)
        {
            
          
            modelBuilder.Entity<ConfigurationProfile>().ToTable("ConfigurationProfile");
            modelBuilder.Entity<ConfigurationParameter>().ToTable("ConfigurationParameter");
             
            modelBuilder.Entity<LogRequest>().ToTable("LogRequest");
            modelBuilder.Entity<LogMsg>().ToTable("LogMsg");
           

             

            modelBuilder.Entity<Notification>().ToTable("Notifications");
            modelBuilder.Entity<NotificationKey>().ToTable("NotificationKeys");
            
        }

        public static void SeedMasterData(this ModelBuilder modelBuilder, IConfiguration configuration,
            IPasswordHash passwordHash)
        {
            
        }

        public static void SeedNotificationKeys(this ModelBuilder modelBuilder, IConfiguration configuration)
        {
            //since running this from ef database tool, it wont be possible to get the notification attributes from Web.Api
            //using notifications data from file seemed feasible.

            var shouldSeed = configuration.GetSection("NotificationKeys").GetValue("SeedDatabase", false);
            if (!shouldSeed) return;

            var notificationsFilePath =
                configuration.GetSection("NotificationKeys").GetValue("FilePath", "");

            if (string.IsNullOrEmpty(notificationsFilePath) || !File.Exists(notificationsFilePath))
                return;

            var notificationsData = File.ReadAllLines(notificationsFilePath);
            var notificationKeys = new NotificationKey[notificationsData.Length];
            for (var i = 0; i < notificationsData.Length; i++)
            {

                var n = notificationsData[i];
                
                var jsonObject = JsonConvert.DeserializeObject<JsonObject>(n);
                var notificationKey = new NotificationKey()
                {
                    Id = i+1,
                        Key = jsonObject["Key"].ToString(),
                    Text =  jsonObject["DefaultText"].ToString(),
                    CreatedBy = 7,
                    CreatedOn = new DateTime(2020, 12, 06),
                    Active = true
                };

                notificationKeys[i] = notificationKey;
            }
            
            modelBuilder.Entity<NotificationKey>().HasData(notificationKeys);
        }
    }
}