using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Support.V7.App;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.Adapters;
using e_SpaMobileApp.Fragments;
using Xamarin.Facebook.Login;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "LogInActivity", Theme ="@style/AppTheme", MainLauncher=true)]
    public class LogInActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_login);

            var logInTabs = FindViewById<TabLayout>(Resource.Id.loginAppBarTabLayout);
            var logInViewPager = FindViewById<ViewPager>(Resource.Id.loginViewPager);
            SetUpViewPager(logInViewPager);
            logInTabs.SetupWithViewPager(logInViewPager);
        }

        private void SetUpViewPager(ViewPager logInViewPager)
        {
            var adapter = new TabAdapter(SupportFragmentManager);
            adapter.AddFragment(new LogInOptionsFragment(), "LogIn");
            logInViewPager.Adapter = adapter;
            logInViewPager.Adapter.NotifyDataSetChanged();
        }
    }
}