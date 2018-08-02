using System.Collections.Generic;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using e_SpaMobileApp.Adapters;
using e_SpaMobileApp.Models;

namespace e_SpaMobileApp.Fragments
{
    public class TrendsFragment : Fragment
    {
        private RecyclerView.Adapter _adapter;
        private RecyclerView.LayoutManager _layoutManager;
        private RecyclerView _recyclerView;
        private List<MyTrend> _trends;

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

            _trends = new List<MyTrend>
            {
                #region trendItems

                new MyTrend
                {
                    SenderName = "Josh Cardif",
                    SenderProfileImageUrl = "some Url",
                    TrendDescripton = "Share something Share something Share" +
                                      " something Share something Share something " +
                                      "Share something Share something " +
                                      "Share something Share something" +
                                      " Share something Share something Share something ",
                    TrendTitle = "My Title"
                },
                new MyTrend
                {
                    SenderName = "Josh Cardif",
                    SenderProfileImageUrl = "some Url",
                    TrendDescripton = "Share something Share something Share" +
                                      " something Share something Share something " +
                                      "Share something Share something " +
                                      "Share something Share something" +
                                      " Share something Share something Share something ",
                    TrendTitle = "My Title"
                },
                new MyTrend
                {
                    SenderName = "Josh Cardif",
                    SenderProfileImageUrl = "some Url",
                    TrendDescripton = "Share something Share something Share" +
                                      " something Share something Share something " +
                                      "Share something Share something " +
                                      "Share something Share something" +
                                      " Share something Share something Share something ",
                    TrendTitle = "My Title"
                },

                #endregion
            };

            _layoutManager = new LinearLayoutManager(Context.ApplicationContext);
            _recyclerView.SetLayoutManager(_layoutManager);
            _adapter = new TrendsRecyclerViewAdapter(_trends, Context.ApplicationContext);
            _recyclerView.SetAdapter(_adapter);

            return view;
        }
    }
}