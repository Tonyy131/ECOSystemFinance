using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using ECOSystemFinance;
using Plugin.Fingerprint;
using FFImageLoading;
using Xamarin.Forms;
using ECOSystemFinance.Views;

namespace ECOSystemFinance.Droid
{
    [Activity(
        Label = "EcoPower Finance", 
        Icon = "@drawable/logo", 
        Theme = "@style/SplashTheme", 
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize
    )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //base.OnCreate(savedInstanceState);

            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            //global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            //CrossFingerprint.SetCurrentActivityResolver(() => Xamarin.Essentials.Platform.CurrentActivity);


            //LoadApplication(new App());

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            CrossFingerprint.SetCurrentActivityResolver(() => Xamarin.Essentials.Platform.CurrentActivity);

            //// Load the splash screen
            //SetContentView(Resource.Style.SplashTheme);

            // Navigate to the main page
            RunOnUiThread(() =>
            {
                var app = new App();
                LoadApplication(app);
                //Shell.Current.GoToAsync("//mainpage");
                //Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            });
            //// Simulate a delay
            //System.Threading.Tasks.Task.Delay(2000).ContinueWith(t =>
            //{
            //});
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}