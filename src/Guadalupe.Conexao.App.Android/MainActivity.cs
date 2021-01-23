
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Guadalupe.Conexao.App.Repository;
using Plugin.FacebookClient;
//using Android.Gms.Common;
//using Firebase.Messaging;
//using Firebase.Iid;
using Android.Util;
using FFImageLoading;
using System;

namespace Guadalupe.Conexao.App.Droid
{
    [Activity(Label = "Conexão", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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
            LoadApplication(new App());
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
    }
}