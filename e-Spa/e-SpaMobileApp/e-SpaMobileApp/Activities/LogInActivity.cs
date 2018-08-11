using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.APIClients;
using e_SpaMobileApp.Helpers;
using e_SpaMobileApp.ServiceModels;
using Firebase;
using Firebase.Auth;
using Java.Lang;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;
using AlertDialog=Android.Support.V7.App.AlertDialog;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/LogInTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class LogInActivity : AppCompatActivity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener, IFacebookCallback,
         Android.Gms.Tasks.IOnCompleteListener
    {
        private Button _googleLogInBtn, _emailLoginButton;
        private LoginButton _facebookLoginButton;
        private TextInputEditText _usernameTxtInputedtTxt, _passwordTxtInputTxt;
        private TextView _createAccountTxtView, _forgotPassTxtView;
        private GoogleApiClient _googleApiClient;
        private int googleSignInID = 1498;
        private int fbSignInID = 1001;
        private RelativeLayout _parentLayout;
        private LinearLayout _container1;
        private LinearLayout _container2;
        private ProgressBar _loginProgressBar;
        private ICallbackManager callbackManager;
        private FirebaseAuth auth;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTMyNjRAMzEzNjJlMzIyZTMwVCtqVm51dVJSdThoQW1lOXNLN2dVQjRnSG9VMkYxL245QlhQODVISmhjRT0=");
           SetContentView(Resource.Layout.activity_login);
            FirebaseHelper.InitFirebaseAuth(this);
            auth = FirebaseAuth.GetInstance(FirebaseHelper.app);
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

            _createAccountTxtView.Click += (o, e) => { StartActivity(new Intent(this, typeof(RegisterActivity))); };
            _emailLoginButton.Click += _emailLoginButton_Click;
            _facebookLoginButton.SetReadPermissions(new List<string>{"public_profile", "email"});
            callbackManager = CallbackManagerFactory.Create();
            _facebookLoginButton.RegisterCallback(callbackManager, this);
            _googleLogInBtn.Click += GoogleLogInBtn_Click;
            ConfigureGoogleSignIn();
        }

    

        private void _emailLoginButton_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(_usernameTxtInputedtTxt.Text))
            {
                Toast.MakeText(this,"Username Field Cannot be empty", ToastLength.Long).Show();
            }
            else
            {
                if (string.IsNullOrEmpty(_passwordTxtInputTxt.Text))
                {
                    Toast.MakeText(this,"Username Field Cannot be empty", ToastLength.Long).Show();
                }
                else
                {
                    LoginWithEmail(_usernameTxtInputedtTxt.Text, _passwordTxtInputTxt.Text);
                }
            }
        }

        private void LoginWithEmail(string username, string password)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                Toast.MakeText(this, "No Internet Connection", ToastLength.Long).Show();
            }
            else
            {
                auth.SignInWithEmailAndPassword(username, password)
                    .AddOnCompleteListener(this);
            }
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
            else if(requestCode==fbSignInID)
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
                var user = new Client
                {
                    Email = account.Email,
                    FirstName = account.GivenName,
                    LastName = account.FamilyName,
                    ProfilePhotoUrl = account.PhotoUrl.ToString(),
                    Residence = "",
                    PhoneNumber = "",
                    SocialPlatformID_Id = account.Id
                };
                var socialPlatformId = new SocialPlatformID
                {
                    SocialPlatform = SocialPlatform.google,
                    PlatformId = account.Id
                };
                HandleAccountDoesNotExist(user, socialPlatformId);
            }
        }

        private void HandleAccountDoesNotExist(Client user, SocialPlatformID socialPlatformId)
        {
            Intent intent = new Intent(this, typeof(SocialNetworksRegisterActivity));
            intent.PutExtra("user", JsonConvert.SerializeObject(user));
            intent.PutExtra("socialPlatformId", JsonConvert.SerializeObject(socialPlatformId));

            var builder = new AlertDialog.Builder(this, Resource.Style.AlertDialogTheme);
            builder.SetTitle("Account Does not Exists")
                .SetMessage(
                    "No e-Spa account was found associated with that account. If you have no account create one. If the problem persits contact us.")
                .SetNeutralButton("Contact Us", delegate { builder.Dispose(); })
                .SetPositiveButton("Register", delegate { StartActivity(intent); })
                .SetNegativeButton("Cancel", delegate { builder.Dispose(); });
            builder.Show();
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
                var user = new Client
                {
                    Email = "",
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    ProfilePhotoUrl = profile.GetProfilePictureUri(220,220).ToString(),
                    Residence = "",
                    PhoneNumber = "",
                    SocialPlatformID_Id = profile.Id
                };
                var socialPlatformId = new SocialPlatformID
                {
                    SocialPlatform = SocialPlatform.facebook,
                    PlatformId = profile.Id
                };
                HandleAccountDoesNotExist(user, socialPlatformId);
            }
        }


        public void OnComplete(Task task)
        {
            if (!task.IsSuccessful)
            {
                var builder = new AlertDialog.Builder(this, Resource.Style.AlertDialogTheme);
                builder.SetTitle("Log In Error")
                    .SetMessage(
                        "Log In was Unsuccessful. The password or Email is incorrect. Please try again, reset your password or if you have no account Create one. If the problem persists contact us.")
                    .SetNeutralButton("Ok", delegate { builder.Dispose(); });
                builder.Show();
            }
        }
    }
}