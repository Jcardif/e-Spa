using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.AccessibilityServices;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Hardware.Input;
using Android.InputMethodServices;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using e_SpaMobileApp.ExtensionsAndHelpers;
using e_SpaMobileApp.Models;
using e_SpaMobileApp.ServiceModels;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.CurrentActivity;
using Refractored.Controls;
using Syncfusion.Android.DataForm;
using Fragment = Android.Support.V4.App.Fragment;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace e_SpaMobileApp.Fragments
{
    public class CompleteRegistrationFragment : Fragment
    {
        private Client _client=new Client();
        FullName fullName=new FullName();
        private Toolbar _toolbar;
        private CircleImageView profilePicImgView;
        private FloatingActionButton _fab;
        private RelativeLayout dataContainerRelativeLayout;
        private Button completeRegBtn;
        private int imagePicker=9001;
        private SfDataForm sfDataForm;
        private SfDataForm sfDataForm2;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if(Arguments==null) return; 
            _client = JsonConvert.DeserializeObject<Client>(Arguments.GetString("client"));
            if (_client.Residence == "empty")
                _client.Residence = null;
            fullName.FirstName = _client.FirstName;
            fullName.LastName = _client.LastName;
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

            //!  Handle button Soft Keyboard
            // TODO: Find a more effective way to do this
            //! Already did
            activity.Window.SetSoftInputMode(SoftInput.AdjustPan);

            //! select image an load into circle image view
            _fab.Click += (s, e) =>
            {
                var intent = new Intent();
                intent.SetType("image/*");
                intent.SetAction(Intent.ActionGetContent);
                
                StartActivityForResult(Intent.CreateChooser(intent,"Select Profile Image"), imagePicker);
            };

            var dataFormParams2 = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dataFormParams2.AddRule(LayoutRules.CenterHorizontal);
            dataFormParams2.Width = ViewGroup.LayoutParams.MatchParent;
            dataFormParams2.Height = ViewGroup.LayoutParams.WrapContent;

            sfDataForm2 = new SfDataForm(Context.ApplicationContext);
            sfDataForm2.DataObject = fullName;
            sfDataForm2.LayoutManager = new DataFormLayoutManagerExt(sfDataForm2,2);
            sfDataForm2.LabelPosition = LabelPosition.Left;
            sfDataForm2.Id = View.GenerateViewId();
            sfDataForm2.ValidationMode = ValidationMode.LostFocus;
            sfDataForm2.CommitMode = CommitMode.LostFocus;
            sfDataForm2.ColumnCount = 2;
            dataContainerRelativeLayout.AddView(sfDataForm2, dataFormParams2);

            var dataFormParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dataFormParams.AddRule(LayoutRules.Below, sfDataForm2.Id);
            dataFormParams.AddRule(LayoutRules.CenterHorizontal);
            dataFormParams.Width = ViewGroup.LayoutParams.MatchParent;
            dataFormParams.Height = ViewGroup.LayoutParams.WrapContent;

            sfDataForm = new SfDataForm(Context.ApplicationContext);
            sfDataForm.DataObject = _client;
            sfDataForm.LayoutManager = new DataFormLayoutManagerExt(sfDataForm,2);
            sfDataForm.LabelPosition = LabelPosition.Left;
            sfDataForm.Id = View.GenerateViewId();
            sfDataForm.ValidationMode = ValidationMode.LostFocus;
            sfDataForm.CommitMode = CommitMode.LostFocus;
            sfDataForm.ColumnCount = 1;
            dataContainerRelativeLayout.AddView(sfDataForm, dataFormParams);

            completeRegBtn.Click += CompleteRegBtn_Click;

            return view;
        }

        private void CompleteRegBtn_Click(object sender, EventArgs e)
        {
            if (!(sfDataForm.Validate() && sfDataForm2.Validate())) return;
            sfDataForm.Commit();
            sfDataForm2.Commit();
            _client.FirstName = fullName.FirstName;
            _client.LastName = fullName.LastName;
            if (CrossConnectivity.Current.IsConnected)
            {
                //! Upload data to database and image to blob storage
            }
            else
            {
                Toast.MakeText(Context.ApplicationContext, "No Internet Connection", ToastLength.Short).Show();
            }
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            if (requestCode != imagePicker || resultCode != (int) Result.Ok || data == null) return;
            var uri = data.Data;
            profilePicImgView.SetImageURI(uri);
        }
    }
}