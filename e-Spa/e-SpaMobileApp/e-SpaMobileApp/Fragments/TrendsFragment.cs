using System;
using System.Collections.Generic;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;


namespace e_SpaMobileApp.Fragments
{
    public class TrendsFragment : Fragment
    {
        private RecyclerView _recyclerView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.client_home_trends_tab, container, false);
            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.trendsRecyclerView);
            return view;
        }
    }
}