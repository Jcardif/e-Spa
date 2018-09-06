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
    public static class FirebaseHelpers
    {
        public static FirebaseAuth _auth;
        public static FirebaseApp _app;
        public static void InitFirebaseAuth(Context context)
        {
            if (_app == null)
                _app = FirebaseApp.InitializeApp(context);
            _auth = new FirebaseAuth(_app);
        }
    }
}