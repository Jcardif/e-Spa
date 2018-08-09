using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
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

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/LogInTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SocialNetworksRegisterActivity : AppCompatActivity
    {
        Client _client=new Client();
        SocialPlatformID _socialPlatformId=new SocialPlatformID();
        private TextInputEditText _firstNameInputEditText,
            _lastNameInputEditText,
            _emailInputEditText,
            _phoneNoInputEditText;

        private CheckBox _acceptConditionsCheckBox;
        private Button _registerBtn;
        private TextView _termsOfUseTxtView, _privacyPolicyTxtView;
        private LinearLayout container1;
        private ProgressBar socialRegisterProgressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));
            _client = JsonConvert.DeserializeObject<Client>(Intent.GetStringExtra("user"));
            _socialPlatformId =
                JsonConvert.DeserializeObject<SocialPlatformID>(Intent.GetStringExtra("socialPlatformId"));
            SetContentView(Resource.Layout.activity_social_networks_register);

            _firstNameInputEditText = FindViewById<TextInputEditText>(Resource.Id.firstNameInputEdtTxt);
            _lastNameInputEditText = FindViewById<TextInputEditText>(Resource.Id.lastNameInputEdtTxt);
            _emailInputEditText = FindViewById<TextInputEditText>(Resource.Id.emailInputEdtTxt);
            _phoneNoInputEditText = FindViewById<TextInputEditText>(Resource.Id.phoneNoInputEdtTxt);
            _acceptConditionsCheckBox = FindViewById<CheckBox>(Resource.Id.acceptConditionsCheckBox);
            _registerBtn = FindViewById<Button>(Resource.Id.registerBtn);
            _termsOfUseTxtView = FindViewById<TextView>(Resource.Id.termsOfUseTxtView);
            _privacyPolicyTxtView = FindViewById<TextView>(Resource.Id.privacyPolicyTxtView);
            container1 = FindViewById<LinearLayout>(Resource.Id.linearLayoutContainer1);
            socialRegisterProgressBar = FindViewById<ProgressBar>(Resource.Id.progressbarSocialRegister);

            _firstNameInputEditText.Text = _client.FirstName;
            _lastNameInputEditText.Text = _client.LastName;
            _emailInputEditText.Text = _client.Email;
            _phoneNoInputEditText.Text = _client.PhoneNumber;

            _registerBtn.Click += RegisterBtn_Click;
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            socialRegisterProgressBar.Visibility = ViewStates.Visible;
            container1.Visibility = ViewStates.Invisible;
            _privacyPolicyTxtView.Visibility = ViewStates.Invisible;
            _termsOfUseTxtView.Visibility = ViewStates.Invisible;

            if (string.IsNullOrEmpty(_firstNameInputEditText.Text))
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
                       if(string.IsNullOrEmpty(_phoneNoInputEditText.Text))
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

        private async void CreateUserAccount()
        {
            var socialPlatformIdApi = new SocialPlatformIdApi();
            await socialPlatformIdApi.AddSocialPlatformId(_socialPlatformId);
            var spltId =
                await socialPlatformIdApi.GetSocialPlatformIdBySocialPlatformAsync(_socialPlatformId.PlatformId);
            var newUser = new Client
            {
                FirstName = _firstNameInputEditText.Text,
                LastName = _lastNameInputEditText.Text,
                Email = _emailInputEditText.Text,
                PhoneNumber = _phoneNoInputEditText.Text,
                ProfilePhotoUrl = _client.ProfilePhotoUrl,
                SocialPlatformID_Id = spltId.Id,
                Residence = "RESIDENCE"
            };
            var userApiClient = new UserApiClient();
            await userApiClient.AddClientAsync(newUser);


            Toast.MakeText(this, $"Welcome {newUser.FirstName}, Thanks for Registering", ToastLength.Short).Show();
            var intent = new Intent(this, typeof(MainActivity));
            intent = Intent.PutExtra("user", JsonConvert.SerializeObject(newUser));

            socialRegisterProgressBar.Visibility = ViewStates.Invisible;
            container1.Visibility = ViewStates.Visible;
            _privacyPolicyTxtView.Visibility = ViewStates.Visible;
            _termsOfUseTxtView.Visibility = ViewStates.Visible;

            StartActivity(intent);
        }

        private void MakeToast(string field)
        {
            socialRegisterProgressBar.Visibility = ViewStates.Invisible;
            container1.Visibility = ViewStates.Visible;
            _privacyPolicyTxtView.Visibility = ViewStates.Visible;
            _termsOfUseTxtView.Visibility = ViewStates.Visible;
            Toast.MakeText(this, $"The Field {field} Cannot be empty", ToastLength.Short).Show();
        }

    }
}