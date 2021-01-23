using Android.App;
using Android.Content;
using Firebase.Iid;
using Firebase.Messaging;
using Guadalupe.Conexao.App.Repository;

namespace Guadalupe.Conexao.App.Droid.Push
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;

            FirebaseMessaging.Instance.SubscribeToTopic("notices");

            SendRegistrationToServer(refreshedToken);
        }
        void SendRegistrationToServer(string token)
        {
            new UserRepository().RegisterFCMTokenAsync(token)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
    }
}