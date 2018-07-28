using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.Adapters;
using Android.Support.V4.App;

namespace e_SpaMobileApp.Fragments
{
    public class HomeFragment : Fragment
    {
        private Button _addGroomingRemindersBtn,
            _groomingReminderNoBtn,
            _addPurchaseReminderBtn,
            _purchaseReminderNoBtn,
            _addCustomReminderBtn,
            _customReminderNoBtn;

        private ViewPager _viewPager;
        private AppBarLayout _appBarLayout;
        private TabLayout _tabLayout;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
             base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment_client_home, container, false);
            _addGroomingRemindersBtn = view.FindViewById<Button>(Resource.Id.addGroomingReminderBtn);
            _groomingReminderNoBtn = view.FindViewById<Button>(Resource.Id.groomingReminderNoBtn);
            _addPurchaseReminderBtn = view.FindViewById<Button>(Resource.Id.addPurchaseReminderBtn);
            _purchaseReminderNoBtn = view.FindViewById<Button>(Resource.Id.PurchaseReminderNoBtn);
            _addCustomReminderBtn = view.FindViewById<Button>(Resource.Id.addCustomReminderBtn);
            _customReminderNoBtn = view.FindViewById<Button>(Resource.Id.CustomReminderNoBtn);
            _viewPager = view.FindViewById<ViewPager>(Resource.Id.clientHomeTrendsViewPager);
            _appBarLayout = view.FindViewById<AppBarLayout>(Resource.Id.clientHomeTrendsAppBar);
            _tabLayout = view.FindViewById<TabLayout>(Resource.Id.clientHomeTrendsTabLayout);

            SetUpViewPager(_viewPager);

            return view;
        }

        private void SetUpViewPager(ViewPager viewPager)
        {
            var adapter = new TrendsTabAdapter(FragmentManager);
          //  adapter.AddFragment(new LogInOptionsFragment(), "LogIn");
            viewPager.Adapter = adapter;
            viewPager.Adapter.NotifyDataSetChanged();
        }
    }
}