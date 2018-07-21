using System.Collections.Generic;
using Android.Support.V4.App;
using Java.Lang;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace e_SpaMobileApp.Adapters
{
    public class TabAdapter : FragmentPagerAdapter
    {
        private List<Fragment> Fragments { get; }
        private List<string> FragmentNames { get; }

        //constructor
        public TabAdapter(FragmentManager fragmentMan) : base(fragmentMan)
        {
            Fragments = new List<Fragment>();
            FragmentNames = new List<string>();
        }


        public override int Count => Fragments.Count;

        public void AddFragment(Fragment fragment, string name)
        {
            Fragments.Add(fragment);
            FragmentNames.Add(name);
        }

        public override Fragment GetItem(int position)
        {
            return Fragments[position];
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(FragmentNames[position]);
        }
    }
}