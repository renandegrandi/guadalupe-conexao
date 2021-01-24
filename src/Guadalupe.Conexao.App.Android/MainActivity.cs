
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Guadalupe.Conexao.App.Repository;
using Plugin.FacebookClient;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using FFImageLoading;
using System;
using System.Linq;

namespace Guadalupe.Conexao.App.Droid
{
    [Activity(Label = "Conexão", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly string TAG = "MainActivity";
        internal static readonly string CHANNEL_ID = "guadalupe_conexao_geral_channel";
        internal static readonly int NOTIFICATION_ID = 100;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            FacebookClientManager.Initialize(this);

            Database.InitializeAsync()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);

            var config = FFImageLoading.Config.Configuration.Default;
            config.ClearMemoryCacheOnOutOfMemory = true;
            ImageService.Instance.Initialize(config);

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Initialize initApp = GetInitiliazeApp();

            IsPlayServicesAvailable();

            CreateNotificationChannel();

            LoadApplication(new App(initApp));
        }

        protected override void OnResume()
        {
            IsPlayServicesAvailable();

            base.OnResume();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            FacebookClientManager.OnActivityResult(requestCode, resultCode, data);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);

            if (resultCode != ConnectionResult.Success)
            {
                if (!GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Finish();

                return false;
            }
            else
            {
                return true;
            }
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID,
                                                  "FCM Notifications",
                                                  NotificationImportance.Default)
            {

                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        public Initialize GetInitiliazeApp() 
        {
            try
            {
                if (Intent.Extras != null)
                {
                    var firstKey = Intent.Extras.KeySet().FirstOrDefault();

                    if (firstKey != null) 
                    {
                        var value = Intent.Extras.GetString(firstKey);

                        var id = new Guid(value);

                        Initialize.Pages page;

                        Enum.TryParse(firstKey, out page);

                        return new Initialize(page, id);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception", $"Falha para carregar a inicialização por notificação: ({ex.Message})");
            }

            return null;
        }
    }
}