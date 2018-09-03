using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.Fragments;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "AuthorizationActivity")]
    public class AuthorizationActivity : AppCompatActivity
    {
        private FrameLayout _authorisationFrameLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_authorization);
            _authorisationFrameLayout = FindViewById<FrameLayout>(Resource.Id.authorizationContainer);
        }

        public void LoadFragment(object obj)
        {
            var fragment = new AuthorizationFragment();
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.authorizationContainer, fragment)
                .Commit();
        }
    }
}