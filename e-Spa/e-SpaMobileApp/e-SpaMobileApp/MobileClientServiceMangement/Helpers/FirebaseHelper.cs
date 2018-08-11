using Android.Content;
using Firebase;
using Firebase.Auth;

namespace e_SpaMobileApp.Helpers
{
    public class FirebaseHelper
    {
        public static FirebaseApp app;
        public static void InitFirebaseAuth(Context context)
        {

            if (app == null)
                app = FirebaseApp.InitializeApp(context);
        }
    }
}