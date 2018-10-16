using Firebase;

namespace e_SpaMobileApp.ExtensionsAndHelpers
{
    public interface IOnSingInCallbacks
    {
        void OnSignInSuccess(bool isSuccess);
        void OnCodeSent();
        void OnVerificationFailed(FirebaseException exception);
        void OnCodeAutoRetrivalTimeOut();
    }
}