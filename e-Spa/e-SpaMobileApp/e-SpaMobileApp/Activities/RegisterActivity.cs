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
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
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


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));
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

            googleBtn.Click += GoogleBtn_Click;
            ConfigureGoogleSigIn();
        }

        private void GoogleBtn_Click(object sender, EventArgs e)
        {
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

        private async void CreateAccountForUser(GoogleSignInAccount account)
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
            var platformIdApiClient = new SocialPlatformIdApi();
            await platformIdApiClient.AddSocialPlatformId(socialPlatformId);
            Intent intent=new Intent(this, typeof(SocialNetworksRegisterActivity));
            intent.PutExtra("user", JsonConvert.SerializeObject(user));
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