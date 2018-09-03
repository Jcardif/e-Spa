using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.Models;
using Syncfusion.Android.DataForm;

namespace e_SpaMobileApp.Fragments
{
    public class RegisterNewUserFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view=new RelativeLayout(Context.ApplicationContext);
            

            var dataForm=new SfDataForm(Context.ApplicationContext);
            dataForm.DataObject=new UserInfo();
            dataForm.LabelPosition = LabelPosition.Top;
            view.AddView(dataForm,ViewGroup.LayoutParams.WrapContent);

            var btnSubmit=new Button(Context.ApplicationContext);
            btnSubmit.SetBackgroundResource(Resource.Drawable.login_button_style);
            btnSubmit.Text = "Next";
            btnSubmit.Gravity = GravityFlags.Center;
            view.AddView(btnSubmit,ViewGroup.LayoutParams.MatchParent,ViewGroup.LayoutParams.WrapContent);
            
            return view;
        }
    }
}