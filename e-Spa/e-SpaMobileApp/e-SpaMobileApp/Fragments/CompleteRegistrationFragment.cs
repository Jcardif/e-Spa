﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.ServiceModels;
using Newtonsoft.Json;
using Plugin.CurrentActivity;
using Refractored.Controls;
using Fragment = Android.Support.V4.App.Fragment;
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
        private int imagePicker=9001;
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

            //!  Init Toolbar
            var activity=(AppCompatActivity) CrossCurrentActivity.Current.Activity;
            activity.SetSupportActionBar(_toolbar);
            activity.SupportActionBar.Title = "Edit Profile";
            activity.SupportActionBar.SetDisplayHomeAsUpEnabled(false);

            _fab.Click += (s, e) =>
            {
                var intent = new Intent();
                intent.SetType("image/*");
                intent.SetAction(Intent.ActionGetContent);
                
                StartActivityForResult(Intent.CreateChooser(intent,"Select Profile Image"), imagePicker);
            };

            return view;
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            if (requestCode != imagePicker || resultCode != (int) Result.Ok || data == null) return;
            var uri = data.Data;
            profilePicImgView.SetImageURI(uri);
        }
    }
}