using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V4.Util;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.Models;
using e_SpaMobileApp.ViewHolders;
using Refractored.Controls;

namespace e_SpaMobileApp.Adapters
{
    internal class ServicesRecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<SService> _services;
        private Context _context;
        private RecyclerView.Adapter adapter;
        private RecyclerView.LayoutManager layoutManager;

        public ServicesRecyclerViewAdapter(Context context, List<SService> services)
        {
            _context = context;
            _services = services;
        }
        public override int ItemCount => _services.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (!(holder is ServicesRecyclerViewHolder vh)) return;
            vh.SalonsNumber.Text = _services[position].SalonsOffering.ToString();
            vh.ServiceName.Text = _services[position].ServiceName;
            vh.Description.Text = _services[position].ServiceDescription;
            vh.ServicePopularity.Text = _services[position].ServicePopularity.ToString();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.services_item, parent, false);
            var serviceCircularImgView = view.FindViewById<CircleImageView>(Resource.Id.serviceCircleImageView);
            var serviceName = view.FindViewById<TextView>(Resource.Id.serviceNameIdTxtView);
            var serviceDesc = view.FindViewById<TextView>(Resource.Id.serviceDescTxtView);
            var salonsNoTxtView = view.FindViewById<TextView>(Resource.Id.salonsNumberTxtView);
            var servicePopularity = view.FindViewById<TextView>(Resource.Id.servicePopularityTxtView);

            var viewHolder = new ServicesRecyclerViewHolder(view)
            {
                ServiceName = serviceName,
                Description = serviceDesc,
                ServicesProfilePic = serviceCircularImgView,
                SalonsNumber = salonsNoTxtView,
                ServicePopularity = servicePopularity
            };
            return viewHolder;
        }
    }
}