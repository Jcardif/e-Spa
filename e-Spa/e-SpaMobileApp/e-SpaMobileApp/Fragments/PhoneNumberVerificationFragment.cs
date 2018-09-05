using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Mukesh.CountryPickerLib;
using e_SpaMobileApp.Models;
using Java.Lang;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace e_SpaMobileApp.Fragments
{
    public class PhoneNumberVerificationFragment : Fragment, ITextWatcher, IOnCountryPickerListener
    {
        private RelativeLayout _relativeLayout;

        private TextInputEditText _textInputEditText1,
            _textInputEditText2,
            _textInputEditText3,
            _textInputEditText4,
            _textInputEditText5,
            _textInputEditText6,
            _codeInputEdtTxt,
            _phoneInputEdtTxt;

        private Button _authorizeVerificationBtn;
        private TextView _verifyTxtView;
        private PhoneInfo _phoneInfo;
        private UserInfo _userInfo;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (Arguments == null) return;
            var phInfo = Arguments.GetString("phoneInfo");
            var usInfo = Arguments.GetString("userInfo");

             _phoneInfo = JsonConvert.DeserializeObject<PhoneInfo>(phInfo);
             _userInfo = JsonConvert.DeserializeObject<UserInfo>(usInfo);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_phoneVerification, container, false);
            _relativeLayout = view.FindViewById<RelativeLayout>(Resource.Id.relativeLayout1);
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

            _codeInputEdtTxt.Text = _phoneInfo.CountryCode;
            _phoneInputEdtTxt.Text = _phoneInfo.PhoneNumber;

            _textInputEditText1.AddTextChangedListener(this);
            _textInputEditText2.AddTextChangedListener(this);
            _textInputEditText3.AddTextChangedListener(this);
            _textInputEditText4.AddTextChangedListener(this);
            _textInputEditText5.AddTextChangedListener(this);
            _textInputEditText6.AddTextChangedListener(this);

            _codeInputEdtTxt.Focusable = false;
            _codeInputEdtTxt.Click += (s, e) => { GetCountryCode(); };
            return view;
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
    }
}