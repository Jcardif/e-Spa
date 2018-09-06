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
using e_SpaMobileApp.Fragments;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/LogInTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class AuthorizationActivity : AppCompatActivity
    {
        private FrameLayout _authorisationFrameLayout;
        public event EventHandler<string> CodeReceived; 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("a90aca45-91cc-4e4f-80fe-bc7fffde8d57", typeof(Analytics), typeof(Crashes));
            SetContentView(Resource.Layout.activity_authorization);
            _authorisationFrameLayout = FindViewById<FrameLayout>(Resource.Id.authorizationContainer);
            LoadFragment();
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
            Console.WriteLine($"verification authorised for {phoneNo}");
            OnCodeReceivedHandle("12qw4r47u7");
        }

        protected virtual void OnCodeReceivedHandle(string code)
        {
            CodeReceived+=new PhoneNumberVerificationFragment().OnCodeReceivedHandle;
            CodeReceived?.Invoke(this, code);
        }
    }
}