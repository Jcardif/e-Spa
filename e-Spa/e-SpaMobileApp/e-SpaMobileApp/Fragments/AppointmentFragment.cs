using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace e_SpaMobileApp.Fragments
{
    public class AppointmentFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_appointment, container, false);
            return view;
        }
    }
}