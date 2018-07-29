using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Refractored.Controls;

namespace e_SpaMobileApp
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