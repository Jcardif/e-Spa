using System;
using System.Collections.Generic;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.ExtensionsAndHelpers;
using e_SpaMobileApp.Models;
using Firebase;
using Firebase.Auth;
using Java.Lang;
using Java.Util.Concurrent;
using Newtonsoft.Json;
using Plugin.CurrentActivity;
using static e_SpaMobileApp.ExtensionsAndHelpers.FirebaseHelpers;

namespace e_SpaMobileApp.Fragments
{
    public class PhoneNumberVerificationFragment : Fragment, IOnSingInCallbacks, ITextWatcher
    {
        private TextInputEditText _textInputEditText1,
            _textInputEditText2,
            _textInputEditText3,
            _textInputEditText4,
            _textInputEditText5,
            _textInputEditText6;
        
        private UserInfo _userInfo;
        private PhoneInfo _phoneInfo;
        private TextView _resendCodeTxtView, _verifyTitleTxtView, _waitingTxtView;
        private string _phoneNo;

        public override  void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var phInfo = Arguments.GetString("phoneInfo");
            var usInfo = Arguments.GetString("userInfo");
            if(phInfo==null)
                return;
            _phoneInfo = JsonConvert.DeserializeObject<PhoneInfo>(phInfo);
            _userInfo = JsonConvert.DeserializeObject<UserInfo>(Arguments.GetString(usInfo));
            _phoneNo = string.Concat(_phoneInfo.CountryCode, _phoneInfo.PhoneNumber);


            InitFirebaseAuth(CrossCurrentActivity.Current.Activity);
            SendVerificationCode();

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_verifyPhoneNumber, container, false);
            _textInputEditText1 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText1);
            _textInputEditText2 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText2);
            _textInputEditText3 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText3);
            _textInputEditText4 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText4);
            _textInputEditText5 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText5);
            _textInputEditText6 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText6);
            _resendCodeTxtView = view.FindViewById<TextView>(Resource.Id.resendCodeTxtView);
            _verifyTitleTxtView = view.FindViewById<TextView>(Resource.Id.verifyTitleTxtView);
            _waitingTxtView = view.FindViewById<TextView>(Resource.Id.waitingTxtView);

            _verifyTitleTxtView.Text = $"Verify {_phoneNo}";
            _waitingTxtView.Text =
                $"Waiting to automatically detect the verification code to sent to {_phoneNo}." +
                $"If the code is not automatically detected please input the six digit Code here.";
            _resendCodeTxtView.Click += (s, e) => { SendVerificationCode(); };
            return view;
        }

        private  void SendVerificationCode()
        {
            var callbacks=new PhoneAuthCallBacks(this);
             PhoneAuthProvider.GetInstance(_auth).VerifyPhoneNumber(
                _phoneNo,
                3,
                TimeUnit.Minutes, 
                CrossCurrentActivity.Current.Activity,
                callbacks);
        }


        public void AfterTextChanged(IEditable s)
        {
            
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {

            var edtTxt = GetCurrentTextInputEditText();
            if (string.IsNullOrEmpty(edtTxt.Text) || edtTxt.Text.Length != 1) return;
            if (edtTxt.Id == Resource.Id.textInputEditText6)
                VerifySentCode();
            else
            {
                var nextView = edtTxt.FocusSearch(FocusSearchDirection.Forward);
                nextView?.RequestFocus();
            }
        }

        private void VerifySentCode()
        {
            throw new NotImplementedException();
        }

        private TextInputEditText GetCurrentTextInputEditText()
        {
            var lst = new List<TextInputEditText>
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


        
        public void OnVerificationCompleted(PhoneAuthCredential credential)
        {
            throw new NotImplementedException();
        }

        public void OnCodeSent()
        {
            _resendCodeTxtView.Enabled = false;
        }

        public void OnVerificationFailed(FirebaseException exception)
        {
            throw new NotImplementedException();
        }

        public void OnCodeAutoRetrivalTimeOut()
        {
            _resendCodeTxtView.Enabled = true;
        }
    }
}