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
using e_SpaMobileApp.Activities;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.App.FragmentTransaction;

namespace e_SpaMobileApp.Fragments
{
    public class AuthorizationFragment:Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_authorisation, container, false);
            var loginBtn = view.FindViewById<Button>(Resource.Id.loginBtn);
            var registerBtn = view.FindViewById<Button>(Resource.Id.resgisterBtn);
            var privacyPolicy = view.FindViewById<TextView>(Resource.Id.privacyPolicyTxtView);
            var termsOfUse = view.FindViewById<TextView>(Resource.Id.termsofUsetxtView);
            loginBtn.Click += (s, e) => { ReplaceFragment("login"); };
            registerBtn.Click += (s, e) => { ReplaceFragment("register"); };
            return view;
        }

        private void ReplaceFragment(string sender)
        {
            var transaction = FragmentManager.BeginTransaction();
            switch (sender)
            {
                case "login":
                    transaction.SetCustomAnimations(Resource.Animation.anim_enter, Resource.Animation.anim_exit);
                    transaction.Replace(Resource.Id.authorizationContainer, new LoginUserFragment())
                        .AddToBackStack(null)
                        .Commit();
                    break;
                case "register":
                    transaction.SetCustomAnimations(Resource.Animation.anim_enter, Resource.Animation.anim_exit);
                    transaction.Replace(Resource.Id.authorizationContainer, new RegisterNewUserFragment())
                        .AddToBackStack(null)
                        .Commit();
                    break;
            }
            
        }
    }
}