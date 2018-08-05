using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.ServiceModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Xamarin.Facebook.Login.Widget;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/LogInTheme", MainLauncher = false)]
    public class SocialNetworksRegisterActivity : AppCompatActivity
    {
        Client client=new Client();

        private TextInputEditText firstNameInputEditText,
            lastNameInputEditText,
            emailInputEditText,
            phoneNoInputEditText;

        private CheckBox acceptConditionsCheckBox;
        private Button registerBtn;
        private TextView termsOfUseTxtView, privacyPolicyTxtView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));
            client = JsonConvert.DeserializeObject<Client>(Intent.GetStringExtra("user"));
            SetContentView(Resource.Layout.activity_social_networks_register);

            firstNameInputEditText = FindViewById<TextInputEditText>(Resource.Id.firstNameInputEdtTxt);
            lastNameInputEditText = FindViewById<TextInputEditText>(Resource.Id.lastNameInputEdtTxt);
            emailInputEditText = FindViewById<TextInputEditText>(Resource.Id.emailInputEdtTxt);
            phoneNoInputEditText = FindViewById<TextInputEditText>(Resource.Id.phoneNoInputEdtTxt);
            acceptConditionsCheckBox = FindViewById<CheckBox>(Resource.Id.acceptConditionsCheckBox);
            registerBtn = FindViewById<Button>(Resource.Id.registerBtn);
            termsOfUseTxtView = FindViewById<TextView>(Resource.Id.termsOfUseTxtView);
            privacyPolicyTxtView = FindViewById<TextView>(Resource.Id.privacyPolicyTxtView);

            firstNameInputEditText.Text = client.FirstName;
            lastNameInputEditText.Text = client.LastName;
            emailInputEditText.Text = client.Email;
            phoneNoInputEditText.Text = client.PhoneNumber;
        }
    }
}