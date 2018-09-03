using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace e_SpaMobileApp.Fragments
{
    public class AuthorizationFragment:Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_authorisation, container, false);
            var loginBtn = view.FindViewById<Button>(Resource.Id.loginBtn);
            var registerBtn = view.FindViewById<Button>(Resource.Id.resgisterBtn);
            var privacyPolicy = view.FindViewById<TextView>(Resource.Id.privacyPolicyTxtView);
            var termsOfUse = view.FindViewById<TextView>(Resource.Id.termsofUsetxtView);
            return view;
        }

      
    }
}