using Firebase;
using Firebase.Auth;

namespace e_SpaMobileApp.ExtensionsAndHelpers
{
    public interface IOnSingInCallbacks
    {
        void OnVerificationCompleted(PhoneAuthCredential credential);
        void OnCodeSent();
        void OnVerificationFailed(FirebaseException exception);
        void OnCodeAutoRetrivalTimeOut();
    }
}