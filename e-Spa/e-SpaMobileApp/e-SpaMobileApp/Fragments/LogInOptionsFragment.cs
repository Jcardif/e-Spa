using System.Collections.Generic;
using Android.Content;
using Android.OS;
using Android.Views;
using e_SpaMobileApp.Activities;
using e_SpaMobileApp.Helpers;
using Java.Lang;
using Xamarin.Facebook;
using Xamarin.Facebook.Login.Widget;
using Android.Support.V4.App;
using Android.Widget;

namespace e_SpaMobileApp.Fragments
{
    public class LogInOptionsFragment : Fragment, IFacebookCallback
    {
        private ICallbackManager mFBCallManager;
        
        private MyProfileTracker mprofileTracker;
        LoginButton BtnFBLogin;
        private Button googleBtn;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            mprofileTracker = new MyProfileTracker();
            mprofileTracker.mOnProfileChanged += MprofileTracker_mOnProfileChanged; ;
            mprofileTracker.StartTracking();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var logInFragmentView = inflater.Inflate(Resource.Layout.login_options_layout, container, false);

            googleBtn = logInFragmentView.FindViewById<Button>(Resource.Id.googleBtn);
            BtnFBLogin = logInFragmentView.FindViewById<LoginButton>(Resource.Id.fbbtn);
            BtnFBLogin.SetReadPermissions(new List<string> { "email", "public_profile" });
            mFBCallManager = CallbackManagerFactory.Create();
            BtnFBLogin.RegisterCallback(mFBCallManager, this);
            googleBtn.Click += GoogleBtn_Click;
            return logInFragmentView;
        }

        private void GoogleBtn_Click(object sender, System.EventArgs e)
        {
            StartActivity(new Intent(Context.ApplicationContext, typeof(MainActivity)));
        }

        private void MprofileTracker_mOnProfileChanged(object sender, OnProfileChangedEventArgs e)
        {
            if (e.mProfile != null)
            {
                try
                {
                    //Toast.MakeText(ApplicationContext, e.mProfile.Name, ToastLength.Long).Show();
                    
                }
                catch (Java.Lang.Exception ex)
                {
                    //Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
            }
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            mFBCallManager.OnActivityResult(requestCode, (int)resultCode, data);
        }
        

        public void OnCancel()
        {
            
        }

        public void OnError(FacebookException error)
        {
            
        }

        public void OnSuccess(Object result)
        {
            StartActivity(new Intent(this.Activity, typeof(MainActivity)));
        }
    }
}