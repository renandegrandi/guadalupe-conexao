
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;
using FFImageLoading;
using Guadalupe.Conexao.App.Repository;
using Plugin.FacebookClient;
using System;
using System.Linq;

namespace Guadalupe.Conexao.App.Droid
{
    [Activity(Label = "Conexão", Icon = "@mipmap/icon", RoundIcon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
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

            var initialize = GetInitiliazeApp();

            LoadApplication(new App(initialize));

            //PushNotificationManager.ProcessIntent(this, Intent);
        }

        //protected override void OnNewIntent(Intent intent)
        //{
        //    base.OnNewIntent(intent);
        //    PushNotificationManager.ProcessIntent(this, intent);
        //}

        protected override void OnResume()
        {
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

        public Initialize GetInitiliazeApp()
        {
            try
            {
                if (Intent.Extras != null)
                {
                    var firstKey = Intent.Extras.KeySet().FirstOrDefault((key) => key.Equals("Notice") || key.Equals("Project"));

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