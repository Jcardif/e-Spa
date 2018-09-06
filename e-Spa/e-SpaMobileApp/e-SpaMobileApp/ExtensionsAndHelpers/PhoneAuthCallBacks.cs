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
        private LogInPath logInPath=new LogInPath();
        public event EventHandler<LogInPath> CompletedSignInSuccessfully;
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
            logInPath.Exception = exception;
            logInPath.IsSuccess = false;
            OnFirebaseSignInSuccessful(logInPath);
        }
        public override void OnCodeSent(string verificationId, PhoneAuthProvider.ForceResendingToken forceResendingToken)
        {
            base.OnCodeSent(verificationId, forceResendingToken);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
                _isSuccessful = true;
            else
            {
                logInPath.Exception = task.Exception as FirebaseException;
                _isSuccessful = false;
            }
            logInPath.IsSuccess = _isSuccessful;
            OnFirebaseSignInSuccessful(logInPath);
        }

        protected virtual void OnFirebaseSignInSuccessful(LogInPath logInPth)
        {
            CompletedSignInSuccessfully += new AuthorizationActivity().OnFirebaseSignInSuccessful;
            CompletedSignInSuccessfully?.Invoke(this, logInPth);
        }
    }
}