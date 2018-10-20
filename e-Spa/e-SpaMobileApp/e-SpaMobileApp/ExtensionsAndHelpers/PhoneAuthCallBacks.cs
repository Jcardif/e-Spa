using System;
using Android.Gms.Tasks;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using static e_SpaMobileApp.ExtensionsAndHelpers.FirebaseHelpers;
using static  Firebase.Auth.PhoneAuthProvider;

namespace e_SpaMobileApp.ExtensionsAndHelpers
{
    public class PhoneAuthCallBacks : OnVerificationStateChangedCallbacks
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
            _singInCallbacks.OnVerificationCompleted(credential);
        }

        public override void OnVerificationFailed(FirebaseException exception)
        {
            _singInCallbacks.OnVerificationFailed(exception);
        }
        

        public override void OnCodeSent(string verificationId, ForceResendingToken forceResendingToken)
        {
            base.OnCodeSent(verificationId, forceResendingToken);
            _verificationId = verificationId;
            _token = forceResendingToken;
            _singInCallbacks.OnCodeSent();
        }

        public override void OnCodeAutoRetrievalTimeOut(string verificationId)
        {
            base.OnCodeAutoRetrievalTimeOut(verificationId);
            _singInCallbacks.OnCodeAutoRetrivalTimeOut();
        }
        
    }
}