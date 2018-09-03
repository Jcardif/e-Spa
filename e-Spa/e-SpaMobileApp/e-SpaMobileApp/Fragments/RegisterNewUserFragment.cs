using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.Models;
using Syncfusion.Android.DataForm;
using Fragment = Android.Support.V4.App.Fragment;

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
            view.SetBackgroundColor(Color.ParseColor("#80000000"));

            var txtViewLayoutParams = new RelativeLayout.LayoutParams(200, 200);
            txtViewLayoutParams.AddRule(LayoutRules.AlignParentBottom);
            txtViewLayoutParams.AddRule(LayoutRules.AlignParentRight);
            txtViewLayoutParams.Width=ViewGroup.LayoutParams.WrapContent;
            txtViewLayoutParams.Height = ViewGroup.LayoutParams.WrapContent;

            var dataFormParams = new RelativeLayout.LayoutParams(200, 200);
            dataFormParams.AddRule(LayoutRules.CenterInParent);
            dataFormParams.Width = ViewGroup.LayoutParams.MatchParent;
            dataFormParams.Height = ViewGroup.LayoutParams.WrapContent;

            var dataForm=new SfDataForm(Context.ApplicationContext);
            dataForm.Id=View.GenerateViewId();
            var userInfo = new UserInfo();
            dataForm.DataObject=userInfo;
            dataForm.LabelPosition = LabelPosition.Top;
            view.AddView(dataForm,dataFormParams);
            
            var dataForm2Params = new RelativeLayout.LayoutParams(200, 200);
            dataForm2Params.AddRule(LayoutRules.Below, dataForm.Id);
            dataForm2Params.Width = ViewGroup.LayoutParams.MatchParent;
            dataForm2Params.Height = ViewGroup.LayoutParams.WrapContent;

            var dataForm2 = new SfDataForm(Context.ApplicationContext);
            var phoneInfo = new PhoneInfo();
            dataForm2.DataObject = phoneInfo;
            dataForm2.LabelPosition = LabelPosition.Top;
            dataForm2.ColumnCount = 2;
            view.AddView(dataForm2, dataForm2Params);

            var txtView = new TextView(Context.ApplicationContext);
            txtView.Text = "Next";
            txtView.TextSize = 28;
            txtView.SetPadding(2, 2, 2, 2);
            txtView.Clickable = true;
            txtView.SetTextColor(Color.White);
            view.AddView(txtView, txtViewLayoutParams);

            txtView.Click += TxtView_Click;
            return view;
        }

        private void TxtView_Click(object sender, EventArgs e)
        {
        }
    }
}