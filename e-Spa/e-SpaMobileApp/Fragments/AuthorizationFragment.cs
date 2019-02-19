using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Com.Mukesh.CountryPickerLib;
using Com.Mukesh.CountryPickerLib.Listeners;
using e_SpaMobileApp.ExtensionsAndHelpers;
using e_SpaMobileApp.Models;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.CurrentActivity;
using Syncfusion.Android.DataForm;
using Fragment = Android.Support.V4.App.Fragment;

namespace e_SpaMobileApp.Fragments
{
    public class AuthorizationFragment:Fragment, IOnCountryPickerListener
    {
        private PhoneInfo phoneInfo;
        private SfDataForm dataForm;
        private static EditText codeEditText;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));

            phoneInfo=new PhoneInfo();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_authorisation, container, false);
            var loginBtn = view.FindViewById<Button>(Resource.Id.loginBtn);
            var registerBtn = view.FindViewById<Button>(Resource.Id.resgisterBtn);
            var privacyPolicy = view.FindViewById<TextView>(Resource.Id.privacyPolicyTxtView);
            var termsOfUse = view.FindViewById<TextView>(Resource.Id.termsofUsetxtView);
            loginBtn.Click += (s, e) => { ReplaceFragment("login"); };
            registerBtn.Click += (s, e) => { ReplaceFragment("register"); };
            return view;
        }

        private void ReplaceFragment(string sender)
        {
            var transaction = FragmentManager.BeginTransaction();
            switch (sender)
            {
                case "login":
                    ShowDialog();
                    break;
                case "register":
                    transaction.SetCustomAnimations(Resource.Animation.anim_enter, Resource.Animation.anim_exit);
                    transaction.Replace(Resource.Id.authorizationContainer, new RegisterNewUserFragment())
                        .AddToBackStack(null)
                        .Commit();
                    break;
            }
            
        }

        private void ShowDialog()
        {
            var relativeLayout=new RelativeLayout(Context.ApplicationContext);
            relativeLayout.SetMinimumHeight(450);
            relativeLayout.Background=CrossCurrentActivity.Current.Activity.GetDrawable(Resource.Drawable.client_app_layout_background);

            var dataFormParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dataFormParams.Width = ViewGroup.LayoutParams.MatchParent;
            dataFormParams.RightMargin = 48;
            dataFormParams.TopMargin = 12;
            dataFormParams.Height = ViewGroup.LayoutParams.WrapContent;

            dataForm = new SfDataForm(Context.ApplicationContext);
            phoneInfo = new PhoneInfo();
            dataForm.DataObject = phoneInfo;
            dataForm.ColumnCount = 4;
            dataForm.LayoutManager = new DataFormLayoutManagerExt(dataForm, ShowCountryListDialog);
            dataForm.LabelPosition = LabelPosition.Top;
            dataForm.Id = View.GenerateViewId();
            dataForm.ValidationMode = ValidationMode.LostFocus;
            dataForm.CommitMode = CommitMode.LostFocus;
            relativeLayout.AddView(dataForm, dataFormParams);

            var loginTxtParams=new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            loginTxtParams.AddRule(LayoutRules.Below, dataForm.Id);
            loginTxtParams.SetMargins(12,12,12,8);
            loginTxtParams.AddRule(LayoutRules.CenterHorizontal);
            loginTxtParams.AddRule(LayoutRules.AlignBottom);
            loginTxtParams.Height = ViewGroup.LayoutParams.WrapContent;
            loginTxtParams.Width = ViewGroup.LayoutParams.WrapContent;

            var loginTxtView=new TextView(Context.ApplicationContext);
            loginTxtView.Id = View.GenerateViewId();
            loginTxtView.Text = Resources.GetString(Resource.String.log_in);
            loginTxtView.SetTextColor(Color.White);
            loginTxtView.Id = View.GenerateViewId();
            loginTxtView.TextSize = 20;
            loginTxtView.Typeface=Typeface.DefaultBold;
            loginTxtView.Clickable = true;
            relativeLayout.AddView(loginTxtView,loginTxtParams);

            loginTxtView.Click += LoginTxtView_Click;

            var builder = new AlertDialog.Builder(CrossCurrentActivity.Current.Activity, Resource.Style.LogInTheme)
                .SetView(relativeLayout)
                .Show();
                
            builder.Window.SetLayout(1000, 450);

        }

        private void LoginTxtView_Click(object sender, System.EventArgs e)
        {
            var phInfo = JsonConvert.SerializeObject(phoneInfo);
            var fragment=new PhoneNumberVerificationFragment();
            var bundle=new Bundle();
            bundle.PutString("phoneInfo", phInfo);
            fragment.Arguments = bundle;

            
            var alertDialogBuilder = new AlertDialog.Builder(CrossCurrentActivity.Current.Activity, Resource.Style.AppTheme)
                .SetTitle("Phone Number Verification")
                .SetMessage($"We shall be verifying the phone Number {string.Concat(phoneInfo.CountryCode, phoneInfo.PhoneNumber)} by sending an SMS with a verification Code. Press Yes to continue or No modify the Phone Number")
                .SetNegativeButton("No", (s, arg) => { Dispose(); })
                .SetPositiveButton("Yes", ((s, args) =>
                {
                    var transaction = FragmentManager.BeginTransaction();
                    transaction.SetCustomAnimations(Resource.Animation.anim_enter, Resource.Animation.anim_exit)
                        .Replace(Resource.Id.authorizationContainer, fragment)
                        .AddToBackStack(null)
                        .Commit();
                }))
                .Create();
            alertDialogBuilder.Window.SetLayout(1000, 450);
            alertDialogBuilder.Show();
        }

        private void ShowCountryListDialog(EditText editText)
        {
            codeEditText = editText;
            Activity.RunOnUiThread(() =>
            {
                var builder = new CountryPicker.Builder()
                    .With(CrossCurrentActivity.Current.Activity)
                    .Listener(this);
                var picker = builder.Build();
                picker.ShowDialog(CrossCurrentActivity.Current.Activity);
            });

        }

        public void OnSelectCountry(Country country)
        {
            codeEditText.Text = country.DialCode;
            codeEditText.ClearFocus();
        }
    }
}