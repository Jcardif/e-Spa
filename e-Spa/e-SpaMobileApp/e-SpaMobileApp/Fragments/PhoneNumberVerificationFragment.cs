using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace e_SpaMobileApp.Fragments
{
    public class PhoneNumberVerificationFragment : Fragment
    {
        private RelativeLayout relativeLayout;

        private TextInputEditText textInputEditText1,
            textInputEditText2,
            textInputEditText3,
            textInputEditText4,
            textInputEditText5,
            textInputEditText6,
            codeInputEdtTxt,
            phoneInputEdtTxt;

        private Button authorizeVerificationBtn;
        private TextView verifyTxtView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_phoneVerification, container, false);
            return view;
        }
    }
}