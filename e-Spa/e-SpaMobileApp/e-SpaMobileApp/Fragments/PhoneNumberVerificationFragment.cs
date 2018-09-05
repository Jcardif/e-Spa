﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
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
using Timer = System.Timers.Timer;

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
        private TextView _verifyTxtView, _resendCodeTxtView, _timerTxtView;
        private PhoneInfo _phoneInfo;
        private UserInfo _userInfo;
        private Timer _timer;
        private int _min=3;
        private int _sec=59;
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
            _resendCodeTxtView = view.FindViewById<TextView>(Resource.Id.resendCodeTxtView);

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
                EnableAndDisableViews(true);
                LoadRelativeLayout();
            };
            return view;
        }

        private  void LoadRelativeLayout()
        {
            var timerParams=new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            timerParams.AddRule(LayoutRules.CenterInParent);
            timerParams.Width = ViewGroup.LayoutParams.WrapContent;
            timerParams.Height = ViewGroup.LayoutParams.WrapContent;

            _timerTxtView =new TextView(Context.ApplicationContext);
            _timerTxtView.Text = $"{_min} : {_sec}";
            _timerTxtView.SetTextColor(Color.White);
            _timerTxtView.TextSize = 16;

            _relativeLayout.AddView(_timerTxtView, timerParams);
            ThreadPool.QueueUserWorkItem(o => BeginTimer());
        }

        private void BeginTimer()
        {
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _sec--;
            if (_sec == 0)
            {
                _min--;
                _sec = 59;
            }
            Activity.RunOnUiThread(() => { _timerTxtView.Text = $"{_min} : {_sec}"; });
            
            if (_min==0&&_sec==0)
                _timer.Stop();
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
            _relativeLayout.Enabled = isEnabled;
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