using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using e_SpaMobileApp.Models;
using e_SpaMobileApp.ViewHolders;
using Refractored.Controls;

namespace e_SpaMobileApp.Adapters
{
    public class TrendsRecyclerViewAdapter : RecyclerView.Adapter
    {
        private readonly Context _context;
        private readonly List<MyTrend> _trends;
        private int _pos = -1;

        public TrendsRecyclerViewAdapter(List<MyTrend> trends, Context context)
        {
            _trends = trends;
            _context = context;
        }

        public override int ItemCount => _trends.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var trendsVh = holder as TrendsRecyclerViewHolder;
            var indexPos = _trends.Count - 1 - position;

            if (trendsVh == null) return;
            trendsVh.SenderName.Text = _trends[indexPos].SenderName;
            trendsVh.TrendDescription.Text = _trends[indexPos].TrendDescripton;
            trendsVh.TrendTitle.Text = _trends[indexPos].TrendTitle;

            if (position > _pos)
            {
                SetAnimation(trendsVh.Tview);
                _pos = position;
            }
        }

        private void SetAnimation(View view)
        {
            var animation = AnimationUtils.LoadAnimation(_context, Resource.Animation.slide_up);
            view.StartAnimation(animation);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var trend = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.client_home_trend_item, parent, false);
            var trendTitleTxtView = trend.FindViewById<TextView>(Resource.Id.trendsTitleTxtView);
            var trendDescTxtView = trend.FindViewById<TextView>(Resource.Id.trendsDescTxtView);
            var userProfilePic = trend.FindViewById<CircleImageView>(Resource.Id.trendProfilePicCircleImgView);
            var userProfileName = trend.FindViewById<TextView>(Resource.Id.trendsProfileNameTxtView);

            var vh = new TrendsRecyclerViewHolder(trend)
            {
                TrendTitle = trendTitleTxtView,
                TrendDescription = trendDescTxtView,
                UserProfilePic = userProfilePic,
                SenderName = userProfileName
            };
            return vh;
        }
    }
}