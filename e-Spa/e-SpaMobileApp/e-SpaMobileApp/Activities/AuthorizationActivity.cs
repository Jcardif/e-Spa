using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.ExtensionsAndHelpers;
using e_SpaMobileApp.Fragments;
using Firebase;
using Firebase.Auth;
using Java.Util.Concurrent;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using static e_SpaMobileApp.ExtensionsAndHelpers.FirebaseHelpers;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/LogInTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class AuthorizationActivity : AppCompatActivity
    {
        public event EventHandler<LogInPath> LogInPathSentBackHome;
 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("a90aca45-91cc-4e4f-80fe-bc7fffde8d57", typeof(Analytics), typeof(Crashes));
            InitFirebaseAuth(this);
            SetContentView(Resource.Layout.activity_authorization);
            LoadFragment();
        }


        private void LoadFragment()
        {
            var fragment = new AuthorizationFragment();
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.authorizationContainer, fragment)
                .Commit();
        }

        public  void OnVerificationAuthorized(object s, LogInPath logInPath)
        {
            InitFirebaseAuth(this);
            PhoneAuthCallBacks callBacks=new PhoneAuthCallBacks(this);
            PhoneAuthProvider.GetInstance(_auth).VerifyPhoneNumber(
                logInPath.PhoneNumber,
                2,
                TimeUnit.Minutes,
                this,
                callBacks);
            //
        }

        protected virtual void OnLogInPathSentBackHome(LogInPath logInPath)
        {
            LogInPathSentBackHome+=new PhoneNumberVerificationFragment().OnLogInPathSentBackHome;
            LogInPathSentBackHome?.Invoke(this, logInPath);
        }

        public void OnFirebaseSignInSuccessful(object sender, bool isSuccess)
        {
            var logInPath = new LogInPath {IsSuccess = isSuccess, PhoneNumber="+254742197114"};
            OnLogInPathSentBackHome(logInPath);
        }
    }
}