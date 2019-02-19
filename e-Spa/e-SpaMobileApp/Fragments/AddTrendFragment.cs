using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace e_SpaMobileApp.Fragments
{
    public class AddTrendFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.client_home_addTrend_tab, container, false);
            var loginBtn = view.FindViewById<Button>(Resource.Id.loginBtn);
            var registerBtn = view.FindViewById<Button>(Resource.Id.resgisterBtn);
            var privacyPolicyTxtView = view.FindViewById<TextView>(Resource.Id.privacyPolicyTxtView);
            var termsOfUseTxtView = view.FindViewById<TextView>(Resource.Id.termsofUsetxtView);
            return view;
        }
    }
}