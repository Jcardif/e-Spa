using System;
using Firebase.Auth;

namespace e_SpaMobileApp.ExtensionsAndHelpers
{
    public class LogInPath : EventArgs
    {
        public bool IsSuccess { get; set; }
        public string PhoneNumber { get; set; }
        //public string VerificationCode { get; set; }
        //public FirebaseUser User { get; set; }
    }
}