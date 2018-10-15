using System;
using Android.Gms.Tasks;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using static e_SpaMobileApp.ExtensionsAndHelpers.FirebaseHelpers;
using static  Firebase.Auth.PhoneAuthProvider;

namespace e_SpaMobileApp.ExtensionsAndHelpers
{
    public class PhoneAuthCallBacks : OnVerificationStateChangedCallbacks, IOnCompleteListener
    {
        private IOnSingInCallbacks _singInCallbacks;
        private string _verificationId;
        private ForceResendingToken _token;


        public PhoneAuthCallBacks(IOnSingInCallbacks singInCallbacks)
        {
            _singInCallbacks = singInCallbacks;
        }

        public override void OnVerificationCompleted(PhoneAuthCredential credential)
        {
            _auth.SignInWithCredential(credential)
                .AddOnCompleteListener(this);
        }

        public override void OnVerificationFailed(FirebaseException exception)
        {
            _singInCallbacks.OnVerificationFailed(exception);
        }

        public void OnComplete(Task task)
        {
            if(task.IsSuccessful)
                _singInCallbacks.OnSignInSuccess(true);
        }

        public override void OnCodeSent(string verificationId, ForceResendingToken forceResendingToken)
        {
            base.OnCodeSent(verificationId, forceResendingToken);
            _verificationId = verificationId;
            _token = forceResendingToken;
            _singInCallbacks.OnCodeSent();
        }
    }
}