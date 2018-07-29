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
using e_SpaMobileApp.Models;

namespace e_SpaMobileApp.Fragments
{
    public class TrendsRecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<Trend> _trends;
        private Context _context;
        private int _pos = -1;

        public TrendsRecyclerViewAdapter(List<Trend>_trends, Context _context)
        {
            this._trends = _trends;
            this._context = _context;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            throw new NotImplementedException();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            throw new NotImplementedException();
        }

        public override int ItemCount { get; }
    }
}