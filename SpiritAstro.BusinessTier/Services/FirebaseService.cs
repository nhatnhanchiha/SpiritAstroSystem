using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;

namespace SpiritAstro.BusinessTier.Services
{
    public interface IFirebaseService
    {
        Task<string> PushNotificationToTheSpecificClient(string registrationToken, Dictionary<string, string> data,
            Notification notification);

        Task<string> PushNotificationToSubscribedTopic(string topic, Dictionary<string, string> data,
            Notification notification);
    }
    
    public class FirebaseService : IFirebaseService
    {
        public async Task<string> PushNotificationToTheSpecificClient(string registrationToken, Dictionary<string, string> data, Notification notification)
        {
            var message = new Message
            {
                Data = data,
                Token = registrationToken,
                Notification = notification
            };

            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return response;
        }

        public async Task<string> PushNotificationToSubscribedTopic(string topic, Dictionary<string, string> data, Notification notification)
        {
            var message = new Message
            {
                Data = data,
                Topic = topic,
                Notification = notification
            };

            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return response;
        }
    }
}