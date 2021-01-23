using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Firebase.Messaging;
using System.Collections.Generic;

namespace Guadalupe.Conexao.App.Droid.Push
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";

        public override void OnMessageReceived(RemoteMessage message)
        {
            var notification = message.GetNotification();

            SendNotification(notification.Title, notification.Body, message.Data);
        }

        private void SendNotification(string title, string body, IDictionary<string, string> data)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            foreach (var key in data.Keys)
            {
                intent.PutExtra(key, data[key]);
            }

            var pendingIntent = PendingIntent.GetActivity(this,
                                                          MainActivity.NOTIFICATION_ID,
                                                          intent,
                                                          PendingIntentFlags.OneShot);

            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                                      .SetSmallIcon(Resource.Mipmap.launcher_foreground)
                                      .SetContentTitle(title)
                                      .SetContentText(body)
                                      .SetAutoCancel(true)
                                      .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManagerCompat.From(this);
            var notification = notificationBuilder.Build();
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, notification);
        }
    }
}