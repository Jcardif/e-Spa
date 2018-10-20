using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.ExtensionsAndHelpers;
using e_SpaMobileApp.Models;
using Firebase;
using Java.Lang;
using Newtonsoft.Json;

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

        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var phInfo = Arguments.GetString("phoneInfo");
            var usInfo = Arguments.GetString("userInfo");
            if(phInfo==null)
                return;
            _phoneInfo = JsonConvert.DeserializeObject<PhoneInfo>(phInfo);
            _userInfo = JsonConvert.DeserializeObject<UserInfo>(Arguments.GetString(usInfo));

            await SendVerificationCode();

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

            _verifyTitleTxtView.Text = $"Verify {string.Concat(_phoneInfo.CountryCode, _phoneInfo.PhoneNumber)}";
            _waitingTxtView.Text =
                $"Waiting to automatically detect the verification code to sent to {string.Concat(_phoneInfo.CountryCode, _phoneInfo.PhoneNumber)}." +
                $"If the code is not automatically detected please input the six digit Code here.";

            return view;
        }

        private Task SendVerificationCode()
        {
            throw new NotImplementedException();
        }


        public void AfterTextChanged(IEditable s)
        {
            throw new NotImplementedException();
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            throw new NotImplementedException();
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


        
        public void OnSignInSuccess(bool isSuccess)
        {
            throw new NotImplementedException();
        }

        public void OnCodeSent()
        {
            throw new NotImplementedException();
        }

        public void OnVerificationFailed(FirebaseException exception)
        {
            throw new NotImplementedException();
        }

        public void OnCodeAutoRetrivalTimeOut()
        {
            throw new NotImplementedException();
        }
    }
}