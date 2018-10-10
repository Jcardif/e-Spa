using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Com.Mukesh.CountryPickerLib;
using e_SpaMobileApp.Activities;
using e_SpaMobileApp.APIClients;
using e_SpaMobileApp.ExtensionsAndHelpers;
using e_SpaMobileApp.Models;
using e_SpaMobileApp.ServiceModels;
using Java.Lang;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.CurrentActivity;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

namespace e_SpaMobileApp.Fragments
{
    public class PhoneNumberVerificationFragment : Fragment, ITextWatcher, IOnCountryPickerListener, IOnSingInCallbacks
    {
        private FrameLayout _frameLayout;

        private TextInputEditText _textInputEditText1,
            _textInputEditText2,
            _textInputEditText3,
            _textInputEditText4,
            _textInputEditText5,
            _textInputEditText6,
            _codeInputEdtTxt,
            _phoneInputEdtTxt;

        private Button _authorizeVerificationBtn;
        private TextView _verifyTxtView, _resendCodeTxtView;
        private PhoneInfo _phoneInfo;
        private UserInfo _userInfo;
        private static FragmentTransaction _transaction;
        private TimerFragment _fragment;
        private static Context _context;
 

        public event EventHandler<LogInPath> VerificationAuthorized;
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));

            if (Arguments == null) return;
            var phInfo = Arguments.GetString("phoneInfo");
            var usInfo = Arguments.GetString("userInfo");

             _phoneInfo = JsonConvert.DeserializeObject<PhoneInfo>(phInfo);
             _userInfo = JsonConvert.DeserializeObject<UserInfo>(usInfo);

        }

   

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_phoneVerification, container, false);
            _frameLayout = view.FindViewById<FrameLayout>(Resource.Id.frameLayout1);
            _authorizeVerificationBtn = view.FindViewById<Button>(Resource.Id.authorizeVerificationBtn);
            _verifyTxtView = view.FindViewById<TextView>(Resource.Id.verifyCodeTxtView);
            _textInputEditText1 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText1);
            _textInputEditText2 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText2);
            _textInputEditText3 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText3);
            _textInputEditText4 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText4);
            _textInputEditText5 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText5);
            _textInputEditText6 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText6);
            _codeInputEdtTxt = view.FindViewById<TextInputEditText>(Resource.Id.countryCodeTxtInputEdtTxt);
            _phoneInputEdtTxt = view.FindViewById<TextInputEditText>(Resource.Id.phoneNumberTxtInputEdtTxt);
            _resendCodeTxtView = view.FindViewById<TextView>(Resource.Id.resendCodeTxtView);
            _context = Application.Context;

            EnableAndDisableViews(false);

            if (_phoneInfo != null && _userInfo != null)
            {
                _codeInputEdtTxt.Text = _phoneInfo.CountryCode;
                _phoneInputEdtTxt.Text = _phoneInfo.PhoneNumber;
            }

            _textInputEditText1.AddTextChangedListener(this);
            _textInputEditText2.AddTextChangedListener(this);
            _textInputEditText3.AddTextChangedListener(this);
            _textInputEditText4.AddTextChangedListener(this);
            _textInputEditText5.AddTextChangedListener(this);
            _textInputEditText6.AddTextChangedListener(this);

            _codeInputEdtTxt.Focusable = false;
            _codeInputEdtTxt.Click += (s, e) => { GetCountryCode(); };
            _authorizeVerificationBtn.Click += (s, e) =>
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    EnableAndDisableViews(true);
                    _phoneInfo.PhoneNumber = _phoneInputEdtTxt.Text;
                    _phoneInfo.CountryCode = _codeInputEdtTxt.Text;
                    OnVerificationAuthorized(string.Concat(_phoneInfo.CountryCode, _phoneInfo.PhoneNumber));
                }
                else
                {
                    Toast.MakeText(_context, "No internet connection", ToastLength.Long).Show();
                }
            };
            return view;
        }

        private void LoadTimer()
        {
            _fragment = new TimerFragment();
            _transaction = ChildFragmentManager.BeginTransaction();
            ManageTimerFragment(false);
        }

        protected virtual void OnVerificationAuthorized(string phoneNo)
        {
            var logInPath = new LogInPath {PhoneNumber = phoneNo};
            VerificationAuthorized+=new AuthorizationActivity().OnVerificationAuthorized;
            VerificationAuthorized?.Invoke(this, logInPath);
        }


        private void ManageTimerFragment(bool toDismiss)
        {
            if (!toDismiss)
            {
                _transaction.Replace(Resource.Id.frameLayout1, _fragment)
                    .SetCustomAnimations(Resource.Animation.fade_in, Resource.Animation.fade_out)
                    .Commit();
            }
            else
            {
                var fragment=new PhoneNoVerifiedFragment();
                    FragmentManager.BeginTransaction()
                        .Remove(_fragment)
                        .Replace(Resource.Id.frameLayout1,fragment)
                        .SetCustomAnimations(Resource.Animation.zoom_in, Resource.Animation.fade_out)
                        .Commit();

            }
        }


        private void EnableAndDisableViews(bool isEnabled)
        {
            _codeInputEdtTxt.Enabled = !isEnabled;
            _phoneInputEdtTxt.Enabled = !isEnabled;
            _authorizeVerificationBtn.Enabled = !isEnabled;
            _textInputEditText1.Enabled = isEnabled;
            _textInputEditText2.Enabled = isEnabled;
            _textInputEditText3.Enabled = isEnabled;
            _textInputEditText4.Enabled = isEnabled;
            _textInputEditText5.Enabled = isEnabled;
            _textInputEditText6.Enabled = isEnabled;
            _verifyTxtView.Enabled = isEnabled;
            _resendCodeTxtView.Enabled = isEnabled;
            _frameLayout.Enabled = isEnabled;
        }

        private void GetCountryCode()
        {
            var builder = new CountryPicker.Builder()
                .With(Context.ApplicationContext)
                .Listener(this);
            var picker = builder.Build();
            picker.ShowDialog(FragmentManager);
        }

        public void AfterTextChanged(IEditable s)
        {
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            var edtTxt=GetCurrentTextInputEditText();
            if (string.IsNullOrEmpty(edtTxt.Text) || edtTxt.Text.Length != 1) return;
            var nextView = edtTxt.FocusSearch(FocusSearchDirection.Forward);
            nextView?.RequestFocus();
        }

        private TextInputEditText GetCurrentTextInputEditText()
        {
            List<TextInputEditText> lst = new List<TextInputEditText>
            {
                _textInputEditText1,
                _textInputEditText2,
                _textInputEditText3,
                _textInputEditText4,
                _textInputEditText5,
                _textInputEditText6
            };
            for (var i = 0; i <= 5; i++)
            {
                if (lst[i].HasFocus)
                    return lst[i];
            }

            return null;
        }

        public void OnSelectCountry(Country country)
        {
            _codeInputEdtTxt.Text = country.DialCode;
        }

        public void OnSignInSuccess(bool isSuccess)
        {
            if (!isSuccess) return;
            ManageTimerFragment(true);
            CommitNewUserToDatabase();

        }

        private async void CommitNewUserToDatabase()
        {
            var client = new Client
            {
                Email = _userInfo.Email,
                FirstName = _userInfo.FirstName,
                LastName = _userInfo.LastName,
                PhoneNumber = string.Concat(_phoneInfo.CountryCode, _phoneInfo.PhoneNumber),
                Residence ="empty",
                ProfilePhotoUrl = "some-url"
            };
            var userApiClient = new UserApiClient();
            if (CrossConnectivity.Current.IsConnected)
            {
                var newClient=await userApiClient.AddClientAsync(client);
                var fragment=new CompleteRegistrationFragment();
                var bundle = new Bundle();
                bundle.PutString("client", JsonConvert.SerializeObject(newClient));
                fragment.Arguments = bundle;

                FragmentManager.BeginTransaction()
                    .SetCustomAnimations(Resource.Animation.anim_enter, Resource.Animation.anim_exit)
                    .Replace(Resource.Id.authorizationContainer, fragment)
                    .Commit();

                //TODO Remove From Backstack
            }
            else
            {
                Toast.MakeText(Context.ApplicationContext,"No Internet Connection", ToastLength.Long).Show();
            }
        }

        public void OnCodeSent()
        {
            LoadTimer();
        }
    }
}