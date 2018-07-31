using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.Fragments;
using Fragment = Android.Support.V4.App.Fragment;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private Android.Support.V7.Widget.Toolbar _mainToolbar;
        private FrameLayout _mainFrameLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);


            _mainToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.mainToolbar);
            _mainFrameLayout = FindViewById<FrameLayout>(Resource.Id.mainFrameLayout);

            _mainToolbar.Title = "e-Spa";
            SetSupportActionBar(_mainToolbar);
            SupportActionBar.SetHomeButtonEnabled(false);
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.mainBottomNavigationView);
            navigation.SetOnNavigationItemSelectedListener(this);
            LoadFragment(Resource.Id.navigation_home);
        }

        private void LoadFragment(int id)
        {
            Android.Support.V4.App.Fragment _fragment = null;
            switch (id)
            {
                case Resource.Id.navigation_home:
                    _fragment = new HomeFragment();
                    break;
                case Resource.Id.navigation_salons:
                  //  _fragment=new SalonsFragment();
                    break;
                case Resource.Id.navigation_services:
                   // _fragment=new ServicesFragment();
                    break;
                case Resource.Id.navigation_appointments:
                    _fragment=new AppointmentFragment();
                    break;
            }

            if (_fragment==null)
                return;
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.mainFrameLayout, _fragment)
                .Commit();
        }


        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    LoadFragment(Resource.Id.navigation_home);
                    return true;
                case Resource.Id.navigation_salons:
                    LoadFragment(Resource.Id.navigation_salons);
                    return true;
                case Resource.Id.navigation_services:
                    LoadFragment(Resource.Id.navigation_services);
                    return true;
                case Resource.Id.navigation_appointments:
                    LoadFragment(Resource.Id.navigation_appointments);
                    return true;
            }
            return false;
        }

  
    }
}

