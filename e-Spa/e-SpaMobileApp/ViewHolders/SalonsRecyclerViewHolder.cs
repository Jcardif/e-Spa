using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Refractored.Controls;

namespace e_SpaMobileApp.ViewHolders
{
    public class SalonsRecyclerViewHolder : RecyclerView.ViewHolder
    {
        public SalonsRecyclerViewHolder(View itemView) : base(itemView)
        {
            Sview = itemView;
        }

        public View Sview { get; set; }
        public CircleImageView SalonProfilePic { get; set; }
        public TextView SalonName { get; set; }
        public TextView Location { get; set; }
        public TextView PhoneNo { get; set; }
        public TextView SalonManager { get; set; }
    }
}