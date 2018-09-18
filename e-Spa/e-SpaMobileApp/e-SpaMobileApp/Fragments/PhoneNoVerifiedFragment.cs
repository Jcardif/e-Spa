using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace e_SpaMobileApp.Fragments
{
    public class PhoneNoVerifiedFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
             base.OnCreateView(inflater, container, savedInstanceState);
            var view = new RelativeLayout(Context.ApplicationContext);

            var imageViewParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            imageViewParams.AddRule(LayoutRules.CenterInParent);

            var imageView=new ImageView(Context.ApplicationContext);
            imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
            imageView.Id = View.GenerateViewId();
            imageView.SetImageResource(Resource.Drawable.ic_verified_user_purple_dark_48dp);
            view.AddView(imageView, imageViewParams);

            var textViewParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            textViewParams.AddRule(LayoutRules.RightOf, imageView.Id);
            textViewParams.AddRule(LayoutRules.CenterHorizontal);

            var textView=new TextView(Context.ApplicationContext);
            textView.SetTextColor(Color.White);
            textView.TextSize = 18;
            textView.Text = "Phone Number Verified";
            textView.SetPadding(2,2,4,2);
            view.AddView(textView, textViewParams);

            var textViewParams2 = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            textViewParams2.AddRule(LayoutRules.AlignParentBottom);
            textViewParams2.AddRule(LayoutRules.CenterHorizontal);

            var textView2 = new TextView(Context.ApplicationContext);
            textView2.SetTextColor(Color.White);
            textView2.TextSize = 14;
            textView2.Text = "Finishing Registration...";
            textView2.SetPadding(4, 4, 4, 4);
            view.AddView(textView2, textViewParams2);

            return view;
        }
    }
}