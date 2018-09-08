using System;
using Android.Gms.Tasks;
using Firebase;
using Firebase.Auth;
using static e_SpaMobileApp.ExtensionsAndHelpers.FirebaseHelpers;
using static  Firebase.Auth.PhoneAuthProvider;

namespace e_SpaMobileApp.ExtensionsAndHelpers
{
    public class PhoneAuthCallBacks : OnVerificationStateChangedCallbacks, IOnCompleteListener
    {
        private IOnSingInCallbacks singInCallbacks;

        public PhoneAuthCallBacks(IOnSingInCallbacks singInCallbacks)
        {
            this.singInCallbacks = singInCallbacks;
        }

        public override void OnVerificationCompleted(PhoneAuthCredential credential)
        {
            _auth.SignInWithCredential(credential)
                .AddOnCompleteListener(this);
        }

        public override void OnVerificationFailed(FirebaseException exception)
        {
            throw new NotImplementedException();
        }

        public void OnComplete(Task task)
        {
            if(task.IsSuccessful)
                singInCallbacks.OnSignInSuccess(true);
        }
    }
}