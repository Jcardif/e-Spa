using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
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
using Newtonsoft.Json;
using Xamarin.Facebook.Login.Widget;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/LogInTheme", MainLauncher = false)]
    public class RegisterActivity : AppCompatActivity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        private Button googleBtn, registerBtn;
        private LoginButton facebookLoginBtn;
        private TextInputEditText firstNameInputEditText,
            lastNameInputEditText,
            phoneNoInputEditText,
            emailInputEditText,
            passwordInputEditText;
        private CheckBox acceptConditionsCheckBox;
        private TextView termsofUseTxtView, privacyPolicyTxtView;
        private GoogleApiClient googleApiClient;
        private int signInCode=1001;
        private LinearLayout container1;
        private ProgressBar registerProgressBar;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTMyNjRAMzEzNjJlMzIyZTMwVCtqVm51dVJSdThoQW1lOXNLN2dVQjRnSG9VMkYxL245QlhQODVISmhjRT0=");
            SetContentView(Resource.Layout.activity_register);
            registerBtn = FindViewById<Button>(Resource.Id.registerBtn);
            googleBtn = FindViewById<Button>(Resource.Id.googleRegisterButton);
            facebookLoginBtn = FindViewById<LoginButton>(Resource.Id.facebookRegisterBtn);
            firstNameInputEditText = FindViewById<TextInputEditText>(Resource.Id.firstNameInputEdtTxt);
            lastNameInputEditText = FindViewById<TextInputEditText>(Resource.Id.lastNameInputEdtTxt);
            phoneNoInputEditText = FindViewById<TextInputEditText>(Resource.Id.phoneNoInputEdtTxt);
            emailInputEditText = FindViewById<TextInputEditText>(Resource.Id.emailInputEdtTxt);
            passwordInputEditText = FindViewById<TextInputEditText>(Resource.Id.passwordInputEdtTxt);
            acceptConditionsCheckBox = FindViewById<CheckBox>(Resource.Id.acceptConditionsCheckBox);
            termsofUseTxtView = FindViewById<TextView>(Resource.Id.termsOfUseTxtView);
            privacyPolicyTxtView = FindViewById<TextView>(Resource.Id.privacyPolicyTxtView);
            container1 = FindViewById<LinearLayout>(Resource.Id.linearLayoutContainer1);
            registerProgressBar = FindViewById<ProgressBar>(Resource.Id.progressbarRegister);

            googleBtn.Click += GoogleBtn_Click;
            ConfigureGoogleSigIn();
        }

        private void GoogleBtn_Click(object sender, EventArgs e)
        {
            registerProgressBar.Visibility = ViewStates.Visible;
            container1.Visibility = ViewStates.Invisible;
            privacyPolicyTxtView.Visibility = ViewStates.Invisible;
            termsofUseTxtView.Visibility = ViewStates.Invisible;
            Intent intent = Auth.GoogleSignInApi.GetSignInIntent(googleApiClient);
            StartActivityForResult(intent,signInCode);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode != signInCode) return;
            var result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
            if (!result.IsSuccess) return;
            var account = result.SignInAccount;
            CreateAccountForUser(account);
        }

        private  void CreateAccountForUser(GoogleSignInAccount account)
        {
            var user = new Client
            {
                Email = account.Email,
                FirstName = account.DisplayName,
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
            var intent=new Intent(this, typeof(SocialNetworksRegisterActivity));
            intent.PutExtra("user", JsonConvert.SerializeObject(user));
            intent.PutExtra("socialPlatformId", JsonConvert.SerializeObject(socialPlatformId));

            registerProgressBar.Visibility = ViewStates.Invisible;
            container1.Visibility = ViewStates.Visible;
            privacyPolicyTxtView.Visibility = ViewStates.Visible;
            termsofUseTxtView.Visibility = ViewStates.Visible;

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
    }
}