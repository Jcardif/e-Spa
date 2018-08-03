using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.Models;
using e_SpaMobileApp.ViewHolders;
using Refractored.Controls;

namespace e_SpaMobileApp.Adapters
{
    public class SalonsRecyclerViewAdapter : RecyclerView.Adapter
    {
        private Context _context;
        private List<MySalon> _salons;

        public SalonsRecyclerViewAdapter(Context context, List<MySalon> salons)
        {
            _context = context;
            _salons = salons;
        }

        public override int ItemCount => _salons.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is SalonsRecyclerViewHolder vh)
            {
                vh.SalonName.Text = _salons[position].SalonName;
                vh.SalonManager.Text = _salons[position].SalonManager;
                vh.Location.Text = _salons[position].Location;
                vh.PhoneNo.Text = _salons[position].PhoneNumber;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.salon_item, parent, false);
            var salonProfileCircleImageView = view.FindViewById<CircleImageView>(Resource.Id.salonCircleImageView);
            var salonNameTxtView = view.FindViewById<TextView>(Resource.Id.salonNameTxtView);
            var locationTxtView = view.FindViewById<TextView>(Resource.Id.salonLocationTxtView);
            var phoneNoTxtView = view.FindViewById<TextView>(Resource.Id.salonPhoneTxtView);
            var salonManagerTxtView = view.FindViewById<TextView>(Resource.Id.salonManagerTxtView);

            var viewHolder = new SalonsRecyclerViewHolder(view)
            {
                SalonProfilePic = salonProfileCircleImageView,
                Location = locationTxtView,
                PhoneNo = phoneNoTxtView,
                SalonManager = salonManagerTxtView,
                SalonName = salonNameTxtView
            };
            return viewHolder;
        }
    }
}