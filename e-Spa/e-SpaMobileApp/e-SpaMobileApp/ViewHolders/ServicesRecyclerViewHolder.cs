using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Refractored.Controls;

namespace e_SpaMobileApp.ViewHolders
{
    public class ServicesRecyclerViewHolder : RecyclerView.ViewHolder
    {
        public ServicesRecyclerViewHolder(View itemView) : base(itemView)
        {
            Seview = itemView;
        }

        public View Seview { get; set; }
        public CircleImageView ServicesProfilePic { get; set; }
        public TextView ServiceName { get; set; }
        public TextView Description { get; set; }
        public TextView SalonsNumber { get; set; }
        public TextView ServicePopularity { get; set; }
    }
}