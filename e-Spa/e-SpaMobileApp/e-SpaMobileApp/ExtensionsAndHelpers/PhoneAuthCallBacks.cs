using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.Activities;
using Firebase;
using Firebase.Auth;
using static e_SpaMobileApp.ExtensionsAndHelpers.FirebaseHelpers;

namespace e_SpaMobileApp.ExtensionsAndHelpers
{
    public class PhoneAuthCallBacks :PhoneAuthProvider.OnVerificationStateChangedCallbacks,IOnCompleteListener
    {
        private Activity _activity;
        bool _isSuccessful;
        public event EventHandler<bool> CompletedSignInSuccessfully;
        public PhoneAuthCallBacks(Activity activity)
        {
            _activity = activity;
        }

        public override void OnVerificationCompleted(PhoneAuthCredential credential)
        {
            SignInWithPhoneAuthCredential(credential);
        }

        private void SignInWithPhoneAuthCredential(PhoneAuthCredential credential)
        {
            _auth.SignInWithCredential(credential)
                .AddOnCompleteListener(_activity, this);
        }

        public override void OnVerificationFailed(FirebaseException exception)
        {
            throw new NotImplementedException();
        }
        public override void OnCodeSent(string verificationId, PhoneAuthProvider.ForceResendingToken forceResendingToken)
        {
            base.OnCodeSent(verificationId, forceResendingToken);
        }

        public void OnComplete(Task task)
        {
            _isSuccessful = task.IsSuccessful;
            OnFirebaseSignInSuccessful(_isSuccessful);
        }

        protected virtual void OnFirebaseSignInSuccessful(bool isSuccessful)
        {
            CompletedSignInSuccessfully += new AuthorizationActivity().OnFirebaseSignInSuccessful;
            CompletedSignInSuccessfully?.Invoke(this, isSuccessful);
        }
    }
}