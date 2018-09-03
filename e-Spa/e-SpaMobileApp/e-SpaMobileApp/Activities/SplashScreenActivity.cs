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
using e_SpaMobileApp.Helpers;
using Firebase.Auth;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/SplashTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
 public class SplashScreenActivity : AppCompatActivity
    {
        private FirebaseAuth auth;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            FirebaseHelper.InitFirebaseAuth(this);
            auth = FirebaseAuth.GetInstance(FirebaseHelper.app);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnResume()
        {
            base.OnResume();
            var startUpTask = new Task(() => { Task.Delay(7000); });
            //startUpTask.ContinueWith(t =>
            //{
            //    FirebaseAuth.Instance.AuthState += (sender, e) =>
            //    {
            //        var user = e?.Auth?.CurrentUser;

            //        if (user != null)
            //        {
            //            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            //            Finish();
            //        }
            //        else
            //        {
            //            var token = AccessToken.CurrentAccessToken;
            //            if (token != null && !token.IsExpired)
            //            {
            //                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            //                Finish();
            //            }
            //            else
            //            {
            //                if (GoogleSignInOptions.DefaultSignIn.Account != null)
            //                {
            //                    StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            //                    Finish();
            //                }
            //                else
            //                {
            //                    StartActivity(new Intent(Application.Context, typeof(LogInActivity)));
            //                    Finish();
            //                }
            //            }
            //        }
            //    };
            //}, TaskScheduler.FromCurrentSynchronizationContext());
            startUpTask.Start();
        }
    }
}