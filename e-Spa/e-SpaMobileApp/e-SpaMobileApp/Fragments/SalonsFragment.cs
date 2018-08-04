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
        private RecyclerView.Adapter _adapter;
        private RecyclerView.LayoutManager _layoutManager;
        private readonly List<MySalon> _salons;

        public SalonsFragment(List<MySalon> salons)
        {
            _salons = salons;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_salons, container, false);
            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.salonsRecyclerView);


            _layoutManager = new LinearLayoutManager(Context.ApplicationContext);
            recyclerView.SetLayoutManager(_layoutManager);
            _adapter = new SalonsRecyclerViewAdapter(Context.ApplicationContext, _salons);
            recyclerView.SetAdapter(_adapter);
            return view;
        }
    }
}