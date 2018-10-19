using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using e_SpaMobileApp.ExtensionsAndHelpers;
using e_SpaMobileApp.Fragments;
using Firebase.Auth;
using Java.Util.Concurrent;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.CurrentActivity;
using static e_SpaMobileApp.ExtensionsAndHelpers.FirebaseHelpers;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using Fragment=Android.Support.V4.App.Fragment;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/LogInTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class AuthorizationActivity : AppCompatActivity
    {
        private  Fragment _fragment;
        private  FragmentTransaction _transaction;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("a90aca45-91cc-4e4f-80fe-bc7fffde8d57", typeof(Analytics), typeof(Crashes));
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            InitFirebaseAuth(this);
            SetContentView(Resource.Layout.activity_authorization);
            LoadFragment();
        }


        private void LoadFragment()
        {
            _fragment = new AuthorizationFragment();
            _transaction = SupportFragmentManager.BeginTransaction();
            _transaction.Replace(Resource.Id.authorizationContainer, _fragment)
            .Commit();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}