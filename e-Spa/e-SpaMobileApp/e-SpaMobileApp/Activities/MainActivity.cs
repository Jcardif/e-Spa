using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using e_SpaMobileApp.APIClients;
using e_SpaMobileApp.Fragments;
using e_SpaMobileApp.Models;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Fragment = Android.Support.V4.App.Fragment;

namespace e_SpaMobileApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private Toolbar _mainToolbar;
        private List<MySalon> _salons;

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


        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));
            SetContentView(Resource.Layout.activity_main);


            _mainToolbar = FindViewById<Toolbar>(Resource.Id.mainToolbar);

            _mainToolbar.Title = "e-Spa";
            SetSupportActionBar(_mainToolbar);
            SupportActionBar.SetHomeButtonEnabled(false);
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            var navigation = FindViewById<BottomNavigationView>(Resource.Id.mainBottomNavigationView);
            navigation.SetOnNavigationItemSelectedListener(this);
            LoadFragment(Resource.Id.navigation_home);

            _salons = new List<MySalon>();
            _salons = await GetSalons();
        }


        private async Task<List<MySalon>> GetSalons()
        {
            var salonApiClient = new SalonsAPIClient();
            var salonList = await salonApiClient.GetSalons();
            var salons = new List<MySalon>();
            foreach (var cmSalon in salonList)
            {
                var salon = new MySalon
                {
                    Location = cmSalon.Locality,
                    PhoneNumber = "0700637853",
                    SalonName = cmSalon.Name,
                    SalonProfilePicUrl = cmSalon.ImageUrl,
                    SalonManager = "me"
                };
                salons.Add(salon);
            }

            return salons;
        }

        private void LoadFragment(int id)
        {
            Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.navigation_home:
                    fragment = new HomeFragment();
                    break;
                case Resource.Id.navigation_salons:
                    fragment = new SalonsFragment(_salons);
                    break;
                case Resource.Id.navigation_services:
                    fragment = new ServicesFragment();
                    break;
                case Resource.Id.navigation_appointments:
                    fragment = new AppointmentFragment();
                    break;
            }

            if (fragment == null)
                return;
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.mainFrameLayout, fragment)
                .Commit();
        }
    }
}