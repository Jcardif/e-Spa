using Android.Content;
using Firebase;
using Firebase.Auth;

namespace e_SpaMobileApp.Helpers
{
    public class FirebaseHelper
    {
        private static FirebaseApp app;
        public static void InitFirebaseAuth(FirebaseAuth auth, Context context)
        {

            if (app == null)
                app = FirebaseApp.InitializeApp(context);
            auth = FirebaseAuth.GetInstance(app);
        }
    }
}