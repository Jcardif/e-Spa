using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;

namespace e_SpaMobileApp.ExtensionsAndHelpers
{
    public class PhoneAuthCallBacks :PhoneAuthProvider.OnVerificationStateChangedCallbacks
    {
        public override void OnVerificationCompleted(PhoneAuthCredential credential)
        {
            throw new NotImplementedException();
        }

        public override void OnVerificationFailed(FirebaseException exception)
        {
            throw new NotImplementedException();
        }
    }
}