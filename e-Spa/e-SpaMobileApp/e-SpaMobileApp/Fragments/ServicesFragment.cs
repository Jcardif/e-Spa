using System.Collections.Generic;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using e_SpaMobileApp.Adapters;
using e_SpaMobileApp.Models;

namespace e_SpaMobileApp.Fragments
{
    public class ServicesFragment : Fragment
    {
        private RecyclerView.Adapter adapter;
        private RecyclerView.LayoutManager layoutManager;
        private List<MyService> services;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_services, container, false);
            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.servicesRecyclerView);
            #region listServices
            services = new List<MyService>()
            {
                new MyService()
                {
                    SalonsOffering = 12,
                    ServiceDescription =
                        "Description here Description here Description here Description here Description here",
                    ServiceName = "Service Name",
                    ServicePopularity = 34,
                    ServiceProfileImgUrl = "someurl"
                },
                new MyService()
                {
                    SalonsOffering = 12,
                    ServiceDescription =
                        "Description here Description here Description here Description here Description here",
                    ServiceName = "Service Name",
                    ServicePopularity = 34,
                    ServiceProfileImgUrl = "someurl"
                },
                new MyService()
                {
                    SalonsOffering = 12,
                    ServiceDescription =
                        "Description here Description here Description here Description here Description here",
                    ServiceName = "Service Name",
                    ServicePopularity = 34,
                    ServiceProfileImgUrl = "someurl"
                },
                new MyService()
                {
                    SalonsOffering = 12,
                    ServiceDescription =
                        "Description here Description here Description here Description here Description here",
                    ServiceName = "Service Name",
                    ServicePopularity = 34,
                    ServiceProfileImgUrl = "someurl"
                },
                new MyService()
                {
                    SalonsOffering = 12,
                    ServiceDescription =
                        "Description here Description here Description here Description here Description here",
                    ServiceName = "Service Name",
                    ServicePopularity = 34,
                    ServiceProfileImgUrl = "someurl"
                },
                new MyService()
                {
                    SalonsOffering = 12,
                    ServiceDescription =
                        "Description here Description here Description here Description here Description here",
                    ServiceName = "Service Name",
                    ServicePopularity = 34,
                    ServiceProfileImgUrl = "someurl"
                },
                new MyService()
                {
                    SalonsOffering = 12,
                    ServiceDescription =
                        "Description here Description here Description here Description here Description here",
                    ServiceName = "Service Name",
                    ServicePopularity = 34,
                    ServiceProfileImgUrl = "someurl"
                },
                new MyService()
                {
                    SalonsOffering = 12,
                    ServiceDescription =
                        "Description here Description here Description here Description here Description here",
                    ServiceName = "Service Name",
                    ServicePopularity = 34,
                    ServiceProfileImgUrl = "someurl"
                },
                new MyService()
                {
                    SalonsOffering = 12,
                    ServiceDescription =
                        "Description here Description here Description here Description here Description here",
                    ServiceName = "Service Name",
                    ServicePopularity = 34,
                    ServiceProfileImgUrl = "someurl"
                },
                new MyService()
                {
                    SalonsOffering = 12,
                    ServiceDescription =
                        "Description here Description here Description here Description here Description here",
                    ServiceName = "Service Name",
                    ServicePopularity = 34,
                    ServiceProfileImgUrl = "someurl"
                },
            };
            #endregion
            adapter=new ServicesRecyclerViewAdapter(Context.ApplicationContext, services);
            recyclerView.SetAdapter(adapter);
            layoutManager=new LinearLayoutManager(Context.ApplicationContext);
            recyclerView.SetLayoutManager(layoutManager);
            return view;
        }
    }
}