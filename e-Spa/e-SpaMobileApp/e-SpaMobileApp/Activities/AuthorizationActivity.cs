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

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/LogInTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class AuthorizationActivity : AppCompatActivity
    {
        private FrameLayout _authorisationFrameLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
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
    }
}