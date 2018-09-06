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

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/LogInTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class AuthorizationActivity : AppCompatActivity
    {
        public event EventHandler<string> CodeReceived;
        private FirebaseAuth _auth;
        private FirebaseApp _app;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("a90aca45-91cc-4e4f-80fe-bc7fffde8d57", typeof(Analytics), typeof(Crashes));
            InitFirebaseAuth();
            SetContentView(Resource.Layout.activity_authorization);
            LoadFragment();
        }

        private void InitFirebaseAuth()
        {
            if (_app == null)
                _app = FirebaseApp.InitializeApp(this);
            _auth=new FirebaseAuth(_app);
        }

        private void LoadFragment()
        {
            var fragment = new AuthorizationFragment();
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.authorizationContainer, fragment)
                .Commit();
        }

        public  void OnVerificationAuthorized(object s, string phoneNo)
        {
            InitFirebaseAuth();
            PhoneAuthCallBacks callBacks=new PhoneAuthCallBacks();
            PhoneAuthProvider.GetInstance(_auth).VerifyPhoneNumber(
                phoneNo,
                2,
                TimeUnit.Minutes,
                this,
                callBacks);

            OnCodeReceivedHandle("12qw4r47u7");
        }

        protected virtual void OnCodeReceivedHandle(string code)
        {
            CodeReceived+=new PhoneNumberVerificationFragment().OnCodeReceivedHandle;
            CodeReceived?.Invoke(this, code);
        }
    }
}