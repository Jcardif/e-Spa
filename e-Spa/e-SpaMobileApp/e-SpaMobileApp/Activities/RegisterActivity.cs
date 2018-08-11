using System;
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
using e_SpaMobileApp.ServiceModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;
using Object = Java.Lang.Object;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/LogInTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class RegisterActivity : AppCompatActivity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener, IFacebookCallback
    {
        private Button googleBtn, _registerBtn;
        private LoginButton _facebookLoginBtn;
        private TextInputEditText _firstNameInputEditText,
            _lastNameInputEditText,
            _phoneNoInputEditText,
            _emailInputEditText,
            _passwordInputEditText;
        private CheckBox _acceptConditionsCheckBox;
        private TextView _termsofUseTxtView, _privacyPolicyTxtView;
        private int signInCode=1001;
        private int fbSignInID = 1498;
        private LinearLayout _container1;
        private ProgressBar _registerProgressBar;
        private GoogleApiClient googleApiClient;
        private ICallbackManager callbackManager;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTMyNjRAMzEzNjJlMzIyZTMwVCtqVm51dVJSdThoQW1lOXNLN2dVQjRnSG9VMkYxL245QlhQODVISmhjRT0=");
            

            SetContentView(Resource.Layout.activity_register);
            _registerBtn = FindViewById<Button>(Resource.Id.registerBtn);
            googleBtn = FindViewById<Button>(Resource.Id.googleRegisterButton);
            _facebookLoginBtn = FindViewById<LoginButton>(Resource.Id.facebookRegisterBtn);
            _firstNameInputEditText = FindViewById<TextInputEditText>(Resource.Id.firstNameInputEdtTxt);
            _lastNameInputEditText = FindViewById<TextInputEditText>(Resource.Id.lastNameInputEdtTxt);
            _phoneNoInputEditText = FindViewById<TextInputEditText>(Resource.Id.phoneNoInputEdtTxt);
            _emailInputEditText = FindViewById<TextInputEditText>(Resource.Id.emailInputEdtTxt);
            _passwordInputEditText = FindViewById<TextInputEditText>(Resource.Id.passwordInputEdtTxt);
            _acceptConditionsCheckBox = FindViewById<CheckBox>(Resource.Id.acceptConditionsCheckBox);
            _termsofUseTxtView = FindViewById<TextView>(Resource.Id.termsOfUseTxtView);
            _privacyPolicyTxtView = FindViewById<TextView>(Resource.Id.privacyPolicyTxtView);
            _container1 = FindViewById<LinearLayout>(Resource.Id.linearLayoutContainer1);
            _registerProgressBar = FindViewById<ProgressBar>(Resource.Id.progressbarRegister);

            googleBtn.Click += GoogleBtn_Click;
            _registerBtn.Click += _registerBtn_Click;
            _facebookLoginBtn.SetReadPermissions(new List<string>{"public_profile", "email"});
            callbackManager = CallbackManagerFactory.Create();
            _facebookLoginBtn.RegisterCallback(callbackManager, this);
            ConfigureGoogleSigIn();
        }

        private void _registerBtn_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(_firstNameInputEditText.Text))
            {
                MakeToast("First Name");
            }
            else
            {
                if (string.IsNullOrEmpty(_lastNameInputEditText.Text))
                {
                    MakeToast("Last Name");
                }
                else
                {
                    if (string.IsNullOrEmpty(_emailInputEditText.Text))
                    {
                        MakeToast("Email");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(_phoneNoInputEditText.Text))
                        {
                            MakeToast("Phone Number");
                        }
                        else
                        {
                            if (!_acceptConditionsCheckBox.Checked)
                            {
                                Toast.MakeText(this, "Accept the terms of use of the application", ToastLength.Short).Show();
                            }
                            else
                            {
                                CreateUserAccount();
                            }
                        }
                    }
                }
            }
        }

        private void CreateUserAccount()
        {
            
        }

        private void MakeToast(string field)
        {
        //    socialRegisterProgressBar.Visibility = ViewStates.Invisible;
        //    container1.Visibility = ViewStates.Visible;
        //    _privacyPolicyTxtView.Visibility = ViewStates.Visible;
        //    _termsOfUseTxtView.Visibility = ViewStates.Visible;
            Toast.MakeText(this, $"The Field {field} Cannot be empty", ToastLength.Short).Show();
        }

        private void GoogleBtn_Click(object sender, EventArgs e)
        {
            _registerProgressBar.Visibility = ViewStates.Visible;
            _container1.Visibility = ViewStates.Invisible;
            _privacyPolicyTxtView.Visibility = ViewStates.Invisible;
            _termsofUseTxtView.Visibility = ViewStates.Invisible;
            Intent intent = Auth.GoogleSignInApi.GetSignInIntent(googleApiClient);
            StartActivityForResult(intent,signInCode);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == signInCode)
            {
                var result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);

                if (!result.IsSuccess) return;
                var account = result.SignInAccount;
                CreateAccountForUserWithGoogle(account);
            }
            else if (requestCode == fbSignInID)
            {
                callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
            }
        }

        private  void CreateAccountForUserWithGoogle(GoogleSignInAccount account)
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
            RegisterNewUser(user, socialPlatformId);
        }

        private void RegisterNewUser(Client user, SocialPlatformID socialPlatformId)
        {
            var intent = new Intent(this, typeof(SocialNetworksRegisterActivity));
            intent.PutExtra("user", JsonConvert.SerializeObject(user));
            intent.PutExtra("socialPlatformId", JsonConvert.SerializeObject(socialPlatformId));

            _registerProgressBar.Visibility = ViewStates.Invisible;
            _container1.Visibility = ViewStates.Visible;
            _privacyPolicyTxtView.Visibility = ViewStates.Visible;
            _termsofUseTxtView.Visibility = ViewStates.Visible;

            StartActivity(intent);
        }

        private void ConfigureGoogleSigIn()
        {
            GoogleSignInOptions options = new GoogleSignInOptions
                    .Builder(GoogleSignInOptions.DefaultSignIn)
                .RequestEmail()
                .RequestId()
                .RequestProfile()
                .Build();
            googleApiClient = new GoogleApiClient
                    .Builder(this)
                .EnableAutoManage(this, this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, options)
                .AddConnectionCallbacks(this)
                .Build();
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
            
        }

        public void OnError(FacebookException error)
        {
            
        }

        public void OnSuccess(Object result)
        {
            var loginResult = result as LoginResult;
            var profile = Profile.CurrentProfile;
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
            RegisterNewUser(user, socialPlatformId);
            Log.Info("FB", "Login Success!");
        }
    }
}