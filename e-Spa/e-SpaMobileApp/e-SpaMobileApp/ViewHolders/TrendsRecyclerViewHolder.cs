using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Refractored.Controls;

namespace e_SpaMobileApp.ViewHolders
{
    public class TrendsRecyclerViewHolder : RecyclerView.ViewHolder
    {
        public TextView TrendTitle { get; set; }
        public TextView TrendDescription { get; set; }
        public CircleImageView UserProfilePic { get; set; }
        public TextView SenderName { get; set; }
        public View TView { get; set; }

        public TrendsRecyclerViewHolder(View itemView) : base(itemView)
        {
            TView = itemView;
        }
    }
}