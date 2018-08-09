using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.APIClients;
using e_SpaMobileApp.ServiceModels;
using Java.Lang;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/LogInTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class LogInActivity : AppCompatActivity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener, IFacebookCallback
    {
        private Button _googleLogInBtn, _emailLoginButton;
        private LoginButton _facebookLoginButton;
        private TextInputEditText _usernameTxtInputedtTxt, _passwordTxtInputTxt;
        private TextView _createAccountTxtView, _forgotPassTxtView;
        private GoogleApiClient _googleApiClient;
        private int googleSignInID = 1498;
        private RelativeLayout _parentLayout;
        private LinearLayout _container1;
        private LinearLayout _container2;
        private ProgressBar _loginProgressBar;
        private ICallbackManager callbackManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTMyNjRAMzEzNjJlMzIyZTMwVCtqVm51dVJSdThoQW1lOXNLN2dVQjRnSG9VMkYxL245QlhQODVISmhjRT0=");
           SetContentView(Resource.Layout.activity_login);
            _googleLogInBtn = FindViewById<Button>(Resource.Id.googleLoginBtn);
            _emailLoginButton = FindViewById<Button>(Resource.Id.loginBtn);
            _facebookLoginButton = FindViewById<LoginButton>(Resource.Id.fbBtnLogin);
            _parentLayout = FindViewById<RelativeLayout>(Resource.Id.loginParentLayout);
            _loginProgressBar = FindViewById<ProgressBar>(Resource.Id.progressbarLogin);
            _container1 = FindViewById<LinearLayout>(Resource.Id.linearLayoutContainer1);
            _container2 = FindViewById<LinearLayout>(Resource.Id.linearLayoutContainer2);
            _createAccountTxtView = FindViewById<TextView>(Resource.Id.createAccountTxtView);
            _forgotPassTxtView = FindViewById<TextView>(Resource.Id.ForgotPassWordTxTView);
            _usernameTxtInputedtTxt = FindViewById<TextInputEditText>(Resource.Id.usernameInputEdtTxt);
            _passwordTxtInputTxt = FindViewById<TextInputEditText>(Resource.Id.passwordInputEdtTxt);
            _emailLoginButton = FindViewById<Button>(Resource.Id.loginBtn);

            _facebookLoginButton.SetReadPermissions(new List<string>{"public_profile"});
            callbackManager = CallbackManagerFactory.Create();
            _facebookLoginButton.RegisterCallback(callbackManager, this);
            _googleLogInBtn.Click += GoogleLogInBtn_Click;
            ConfigureGoogleSignIn();
        }

        private void ConfigureGoogleSignIn()
        {
            GoogleSignInOptions options = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                                                                 .RequestEmail()
                                                                 .RequestId()
                                                                 .RequestProfile()
                                                                 .Build();
            _googleApiClient = new GoogleApiClient.Builder(this)
                .EnableAutoManage(this, this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, options)
                .AddConnectionCallbacks(this)
                .Build();
        }

        private void GoogleLogInBtn_Click(object sender, System.EventArgs e)
        {
            _container1.Visibility = ViewStates.Invisible;
            _container2.Visibility = ViewStates.Invisible;
            _createAccountTxtView.Visibility = ViewStates.Invisible;
            _forgotPassTxtView.Visibility = ViewStates.Invisible;
            _loginProgressBar.Visibility = ViewStates.Visible;
            Intent intent = Auth.GoogleSignInApi.GetSignInIntent(_googleApiClient);
            StartActivityForResult(intent, googleSignInID);
        }


        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == googleSignInID)
            {
                var result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                if (!result.IsSuccess) return;
                var account = result.SignInAccount;
                CheckGoogleAccountExistence(account);
            }
            else
            {
                callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
            }
        }

        private async void CheckGoogleAccountExistence(GoogleSignInAccount account)
        {
            var socialPlatformApi = new SocialPlatformIdApi();
            var exists=await socialPlatformApi.CheeckIfPlatforIdExistAsync(account.Id, SocialPlatform.google);

            _loginProgressBar.Visibility = ViewStates.Invisible;
            _container1.Visibility = ViewStates.Visible;
            _container2.Visibility = ViewStates.Visible;
            _createAccountTxtView.Visibility = ViewStates.Visible;
            _forgotPassTxtView.Visibility = ViewStates.Visible;

            if (exists)
            {
                Toast.MakeText(this, $"Welcome {account.DisplayName}", ToastLength.Short).Show();
                StartActivity(new Intent(this, typeof(MainActivity)));
            }
            else
            {
                Snackbar.Make(_parentLayout, "Account Does not exist.", Snackbar.LengthLong)
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

        public void OnCancel()
        {
            Log.Info("FB", "Login Cancelled!");
        }

        public void OnError(FacebookException error)
        {
            Log.Info("FB", "Login Error!");
        }

        public void OnSuccess(Object result)
        {
            var loginResult = result as LoginResult;
            CheckFacebookAccountExistence(Profile.CurrentProfile);
            Log.Info("FB", "Login Success!");
        }

        private async void CheckFacebookAccountExistence(Profile profile)
        {
            var socialPlatformApi = new SocialPlatformIdApi();
            var exists = await socialPlatformApi.CheeckIfPlatforIdExistAsync(profile.Id, SocialPlatform.facebook);

            _loginProgressBar.Visibility = ViewStates.Invisible;
            _container1.Visibility = ViewStates.Visible;
            _container2.Visibility = ViewStates.Visible;
            _createAccountTxtView.Visibility = ViewStates.Visible;
            _forgotPassTxtView.Visibility = ViewStates.Visible;

            if (exists)
            {
                Toast.MakeText(this, $"Welcome {profile.FirstName}", ToastLength.Short).Show();
                StartActivity(new Intent(this, typeof(MainActivity)));
            }
            else
            {
                Snackbar.Make(_parentLayout, "Account Does not exist.", Snackbar.LengthLong)
                    .SetAction("Register", (view) => { StartActivity(new Intent(this, typeof(RegisterActivity))); })
                    .Show();
            }
        }
    }
}