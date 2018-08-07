﻿using Android.App;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Syncfusion.Sfbusyindicator;
using Com.Syncfusion.Sfbusyindicator.Enums;
using e_SpaMobileApp.APIClients;
using e_SpaMobileApp.ServiceModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Facebook.Login.Widget;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/LogInTheme", MainLauncher = true)]
    public class LogInActivity : AppCompatActivity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        private Button googleLogInBtn, emailLoginButton;
        private LoginButton facebookLoginButton;
        private TextInputEditText usernameTxtInputedtTxt, passwordTxtInputTxt;
        private TextView createAccountTxtView, forgotPassTxtView;
        private GoogleApiClient googleApiClient;
        private int googleSignInID = 1498;
        private RelativeLayout parentLayout;
        private LinearLayout container1;
        private LinearLayout container2;
        private ProgressBar loginProgressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTMyNjRAMzEzNjJlMzIyZTMwVCtqVm51dVJSdThoQW1lOXNLN2dVQjRnSG9VMkYxL245QlhQODVISmhjRT0=");
            SetContentView(Resource.Layout.activity_login);
            googleLogInBtn = FindViewById<Button>(Resource.Id.googleLoginBtn);
            emailLoginButton = FindViewById<Button>(Resource.Id.loginBtn);
            facebookLoginButton = FindViewById<LoginButton>(Resource.Id.fbBtnLogin);
            parentLayout = FindViewById<RelativeLayout>(Resource.Id.loginParentLayout);
            loginProgressBar = FindViewById<ProgressBar>(Resource.Id.progressbarLogin);
            container1 = FindViewById<LinearLayout>(Resource.Id.linearLayoutContainer1);
            container2 = FindViewById<LinearLayout>(Resource.Id.linearLayoutContainer2);
         

            googleLogInBtn.Click += GoogleLogInBtn_Click;
            ConfigureGoogleSignIn();
        }

        private void ConfigureGoogleSignIn()
        {
            GoogleSignInOptions options = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                                                                 .RequestEmail()
                                                                 .RequestId()
                                                                 .RequestProfile()
                                                                 .Build();
            googleApiClient = new GoogleApiClient.Builder(this)
                .EnableAutoManage(this, this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, options)
                .AddConnectionCallbacks(this)
                .Build();
        }

        private void GoogleLogInBtn_Click(object sender, System.EventArgs e)
        {
            container1.Visibility = ViewStates.Invisible;
            container2.Visibility = ViewStates.Invisible;
            createAccountTxtView.Visibility = ViewStates.Invisible;
            forgotPassTxtView.Visibility = ViewStates.Invisible;
            loginProgressBar.Visibility = ViewStates.Visible;
            Intent intent = Auth.GoogleSignInApi.GetSignInIntent(googleApiClient);
            StartActivityForResult(intent, googleSignInID);
        }


        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode != googleSignInID) return;
            var result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
            if (!result.IsSuccess) return;
            var account = result.SignInAccount;
            CheckAccountExistence(account);
        }

        private async void CheckAccountExistence(GoogleSignInAccount account)
        {
            var socialPlatformApi = new SocialPlatformIdApi();
            var exists=await socialPlatformApi.CheeckIfPlatforIdExistAsync(account.Id, SocialPlatform.google);

            loginProgressBar.Visibility = ViewStates.Invisible;
            container1.Visibility = ViewStates.Visible;
            container2.Visibility = ViewStates.Visible;
            createAccountTxtView.Visibility = ViewStates.Visible;
            forgotPassTxtView.Visibility = ViewStates.Visible;

            if (exists)
            {
                Toast.MakeText(this, $"Welcome {account.DisplayName}", ToastLength.Short).Show();
                StartActivity(new Intent(this, typeof(MainActivity)));
            }
            else
            {
                Snackbar.Make(parentLayout, "Account Does not exist.", Snackbar.LengthLong)
                    .SetAction("Register", (view) => { StartActivity(new Intent(this, typeof(RegisterActivity))); })
                    .Show();
            }
        }

        public void OnConnected(Bundle connectionHint)
        {
            
        }

        public void OnConnectionSuspended(int cause)
        {
            
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            
        }
    }
}