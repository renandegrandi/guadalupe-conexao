using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Services
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(string title, string body, string image, Dictionary<string, string> data, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SendByTopicAsync(string title, string body, string image, string topic, Dictionary<string, string> data, CancellationToken cancellationToken)
        {
            var message = new Message
            {
                Topic = topic,
                Notification = new Notification
                {
                    Body = body,
                    Title = title,
                    ImageUrl = image
                },
                Data = data
            };

            return FirebaseMessaging.DefaultInstance.SendAsync(message, cancellationToken);
        }
    }
}
