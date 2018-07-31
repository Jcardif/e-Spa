using System.Collections.Generic;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using e_SpaMobileApp.Adapters;
using e_SpaMobileApp.Models;

namespace e_SpaMobileApp.Fragments
{
    public class SalonsFragment : Fragment
    {
        private List<Salon> _salons;
        private RecyclerView.Adapter adapter;
        private RecyclerView.LayoutManager layoutManager;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
             base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_salons, container, false);
            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.salonsRecyclerView);
            _salons = new List<Salon>()
            {
                #region salons
                new Salon()
                {
                    Location = "Nyeri",
                    PhoneNumber = "07345527752",
                    SalonManager = "Mary Martin",
                    SalonName = "Aslet salons",
                    SalonProfilePicUrl = "Some URl"
                },
                new Salon()
                {
                    Location = "Nyeri",
                    PhoneNumber = "07345527752",
                    SalonManager = "Mary Martin",
                    SalonName = "Aslet salons",
                    SalonProfilePicUrl = "Some URl"
                },
                new Salon()
                {
                    Location = "Nyeri",
                    PhoneNumber = "07345527752",
                    SalonManager = "Mary Martin",
                    SalonName = "Aslet salons",
                    SalonProfilePicUrl = "Some URl"
                },
                new Salon()
                {
                    Location = "Nyeri",
                    PhoneNumber = "07345527752",
                    SalonManager = "Mary Martin",
                    SalonName = "Aslet salons",
                    SalonProfilePicUrl = "Some URl"
                },
                new Salon()
                {
                    Location = "Nyeri",
                    PhoneNumber = "07345527752",
                    SalonManager = "Mary Martin",
                    SalonName = "Aslet salons",
                    SalonProfilePicUrl = "Some URl"
                },
                new Salon()
                {
                    Location = "Nyeri",
                    PhoneNumber = "07345527752",
                    SalonManager = "Mary Martin",
                    SalonName = "Aslet salons",
                    SalonProfilePicUrl = "Some URl"
                },
                new Salon()
                {
                    Location = "Nyeri",
                    PhoneNumber = "07345527752",
                    SalonManager = "Mary Martin",
                    SalonName = "Aslet salons",
                    SalonProfilePicUrl = "Some URl"
                },
                new Salon()
                {
                    Location = "Nyeri",
                    PhoneNumber = "07345527752",
                    SalonManager = "Mary Martin",
                    SalonName = "Aslet salons",
                    SalonProfilePicUrl = "Some URl"
                },
                new Salon()
                {
                    Location = "Nyeri",
                    PhoneNumber = "07345527752",
                    SalonManager = "Mary Martin",
                    SalonName = "Aslet salons",
                    SalonProfilePicUrl = "Some URl"
                },
                new Salon()
                {
                    Location = "Nyeri",
                    PhoneNumber = "07345527752",
                    SalonManager = "Mary Martin",
                    SalonName = "Aslet salons",
                    SalonProfilePicUrl = "Some URl"
                },
                new Salon()
                {
                    Location = "Nyeri",
                    PhoneNumber = "07345527752",
                    SalonManager = "Mary Martin",
                    SalonName = "Aslet salons",
                    SalonProfilePicUrl = "Some URl"
                },
                new Salon()
                {
                    Location = "Nyeri",
                    PhoneNumber = "07345527752",
                    SalonManager = "Mary Martin",
                    SalonName = "Aslet salons",
                    SalonProfilePicUrl = "Some URl"
                }
                #endregion
            };
            adapter=new SalonsRecyclerViewAdapter(Context.ApplicationContext, _salons);
            recyclerView.SetAdapter(adapter);
            layoutManager=new LinearLayoutManager(Context.ApplicationContext);
            recyclerView.SetLayoutManager(layoutManager);
            return view;
        }
    }
}