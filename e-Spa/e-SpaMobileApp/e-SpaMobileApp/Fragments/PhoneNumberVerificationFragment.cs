using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.ExtensionsAndHelpers;
using Firebase;

namespace e_SpaMobileApp.Fragments
{
    public class PhoneNumberVerificationFragment : Fragment, IOnSingInCallbacks
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        #region OnSignInCallBacks
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
        #endregion
    }
}