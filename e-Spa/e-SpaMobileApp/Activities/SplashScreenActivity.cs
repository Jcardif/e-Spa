using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using static e_SpaMobileApp.ExtensionsAndHelpers.FirebaseHelpers;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/SplashTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
 public class SplashScreenActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            InitFirebaseAuth(this);
            AppCenter.Start("a90aca45-91cc-4e4f-80fe-bc7fffde8d57", typeof(Analytics), typeof(Crashes));
        }
        protected override void OnResume()
        {
            base.OnResume();
            var startupTask = new Task(() => { Task.Delay(4000); });
            startupTask.ContinueWith(t =>
            {
                _auth.AuthState += (sender, e) =>
                {
                    var user = e?.Auth?.CurrentUser;

                    if (user != null)
                    {
                        StartActivity(new Intent(Application.Context, typeof(MainActivity)));
                        Finish();
                    }
                    else
                    {
                        StartActivity(new Intent(Application.Context, typeof(AuthorizationActivity)));
                        Finish();
                    }
                };
            }, TaskScheduler.FromCurrentSynchronizationContext());
            startupTask.Start();
        }
    }
}