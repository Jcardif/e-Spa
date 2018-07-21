using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace e_SpaMobileApp.Fragments
{
    public class LogInOptionsFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var logInFragmentView = inflater.Inflate(Resource.Layout.login_options_layout, container, false);
            return logInFragmentView;
        }
    }
}