using Firebase;
using Firebase.Auth;

namespace e_SpaMobileApp.ExtensionsAndHelpers
{
    public interface IOnSingInCallbacks
    {
        void OnVerificationCompleted(PhoneAuthCredential credential);
        void OnCodeSent(string verificationId, PhoneAuthProvider.ForceResendingToken forceResendingToken);
        void OnVerificationFailed(FirebaseException exception);
        void OnCodeAutoRetrivalTimeOut();
    }
}