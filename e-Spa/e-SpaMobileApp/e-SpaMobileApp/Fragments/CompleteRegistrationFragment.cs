using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.ServiceModels;
using Newtonsoft.Json;
using Refractored.Controls;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace e_SpaMobileApp.Fragments
{
    public class CompleteRegistrationFragment : Fragment
    {
        private Client _client;
        private Toolbar _toolbar;
        private CircleImageView profilePicImgView;
        private FloatingActionButton _fab;
        private RelativeLayout dataContainerRelativeLayout;
        private Button completeRegBtn;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if(Arguments==null) return; 
            _client = JsonConvert.DeserializeObject<Client>(Arguments.GetString("client"));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_completeRegistration, container, false);
            _toolbar = view.FindViewById<Toolbar>(Resource.Id.completeRegistrationToolbar);
            profilePicImgView = view.FindViewById<CircleImageView>(Resource.Id.profilePictureCircularImgView);
            _fab = view.FindViewById<FloatingActionButton>(Resource.Id.selectProfileImageFab);
            dataContainerRelativeLayout = view.FindViewById<RelativeLayout>(Resource.Id.relativeDataformContainer);
            completeRegBtn = view.FindViewById<Button>(Resource.Id.completeRegistartionBtn);

            

            return view;
        }
    }
}