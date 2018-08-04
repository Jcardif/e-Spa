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
using Android.Widget;
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));
            SetContentView(Resource.Layout.activity_login);
            googleLogInBtn = FindViewById<Button>(Resource.Id.googleLoginBtn);
            emailLoginButton = FindViewById<Button>(Resource.Id.loginBtn);
            facebookLoginButton = FindViewById<LoginButton>(Resource.Id.fbBtnLogin);

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
            Intent intent = Auth.GoogleSignInApi.GetSignInIntent(googleApiClient);
            StartActivityForResult(intent, googleSignInID);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == googleSignInID)
            {
                var result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                if (result.IsSuccess)
                {
                    var details = result.SignInAccount;
                    Toast.MakeText(this, details.DisplayName.ToString(), ToastLength.Long).Show();
                }
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