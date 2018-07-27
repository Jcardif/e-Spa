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
using e_SpaMobileApp.Helpers;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;
using Object = Java.Lang.Object;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme ="@style/AppTheme", MainLauncher=false)]
    public class LogInActivity : AppCompatActivity, IFacebookCallback
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

        public void OnCancel()
        {
            throw new NotImplementedException();
        }

        public void OnError(FacebookException error)
        {
            throw new NotImplementedException();
        }

        public void OnSuccess(Object result)
        {
            throw new NotImplementedException();
        }
    }
}