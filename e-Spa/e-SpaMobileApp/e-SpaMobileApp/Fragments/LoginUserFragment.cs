using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.ExtensionsAndHelpers;
using e_SpaMobileApp.Models;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.CurrentActivity;
using Syncfusion.Android.DataForm;

namespace e_SpaMobileApp.Fragments
{
    public class LoginUserFragment : Fragment
    {
        private PhoneInfo phoneInfo;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity.Window.SetSoftInputMode(SoftInput.AdjustPan);
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = new RelativeLayout(Context.ApplicationContext);
            view.SetBackgroundColor(Color.ParseColor("#80000000"));
            view.SetPadding(8, 8, 8, 8);
            

            var dataformParams=new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,ViewGroup.LayoutParams.MatchParent);
            dataformParams.AddRule(LayoutRules.CenterInParent);
            dataformParams.Height = ViewGroup.LayoutParams.WrapContent;
            dataformParams.Width = ViewGroup.LayoutParams.MatchParent;

            var dataform = new SfDataForm(Context.ApplicationContext);
            dataform.Id = View.GenerateViewId();
            phoneInfo=new PhoneInfo();
            dataform.DataObject = phoneInfo;
            dataform.ColumnCount = 4;
            dataform.LabelWidth = 30;
            dataform.LayoutManager = new DataFormLayoutManagerExt(dataform);
            dataform.LabelPosition = LabelPosition.Top;
            dataform.ValidationMode = ValidationMode.LostFocus;
            dataform.CommitMode = CommitMode.LostFocus;
            view.AddView(dataform, dataformParams);

            return view;
        }
    }
}