using System.Collections.Generic;
using Android.Support.V4.App;
using Java.Lang;

namespace e_SpaMobileApp.Adapters
{
    public class TrendsTabAdapter : FragmentPagerAdapter
    {
        public TrendsTabAdapter(FragmentManager fragmentMan) : base(fragmentMan)
        {
            Fragments = new List<Fragment>();
            FragmentNames = new List<string>();
        }

        private List<Fragment> Fragments { get; }
        private List<string> FragmentNames { get; }
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
            return new String(FragmentNames[position]);
        }
    }
}