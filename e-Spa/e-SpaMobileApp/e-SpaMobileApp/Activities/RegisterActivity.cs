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
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Facebook.Login.Widget;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : AppCompatActivity
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
        }
    }
}