using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
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
        private CircleImageView _profilePicImgView;
        private FloatingActionButton _fab;
        private RelativeLayout _dataContainerRelativeLayout;
        private Button _completeRegBtn;
        private SfDataForm _sfDataForm;
        private SfDataForm _sfDataForm2;
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
            _profilePicImgView = view.FindViewById<CircleImageView>(Resource.Id.profilePictureCircularImgView);
            _fab = view.FindViewById<FloatingActionButton>(Resource.Id.selectProfileImageFab);
            _dataContainerRelativeLayout = view.FindViewById<RelativeLayout>(Resource.Id.relativeDataformContainer);
            _completeRegBtn = view.FindViewById<Button>(Resource.Id.completeRegistartionBtn);
            
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

            };

            var dataFormParams2 = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dataFormParams2.AddRule(LayoutRules.CenterHorizontal);
            dataFormParams2.Width = ViewGroup.LayoutParams.MatchParent;
            dataFormParams2.Height = ViewGroup.LayoutParams.WrapContent;

            _sfDataForm2 = new SfDataForm(Context.ApplicationContext);
            _sfDataForm2.DataObject = fullName;
            _sfDataForm2.LayoutManager = new DataFormLayoutManagerExt(_sfDataForm2,2);
            _sfDataForm2.LabelPosition = LabelPosition.Left;
            _sfDataForm2.Id = View.GenerateViewId();
            _sfDataForm2.ValidationMode = ValidationMode.LostFocus;
            _sfDataForm2.CommitMode = CommitMode.LostFocus;
            _sfDataForm2.ColumnCount = 2;
            _dataContainerRelativeLayout.AddView(_sfDataForm2, dataFormParams2);

            var dataFormParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dataFormParams.AddRule(LayoutRules.Below, _sfDataForm2.Id);
            dataFormParams.AddRule(LayoutRules.CenterHorizontal);
            dataFormParams.Width = ViewGroup.LayoutParams.MatchParent;
            dataFormParams.Height = ViewGroup.LayoutParams.WrapContent;

            _sfDataForm = new SfDataForm(Context.ApplicationContext);
            _sfDataForm.DataObject = _client;
            _sfDataForm.LayoutManager = new DataFormLayoutManagerExt(_sfDataForm,2);
            _sfDataForm.LabelPosition = LabelPosition.Left;
            _sfDataForm.Id = View.GenerateViewId();
            _sfDataForm.ValidationMode = ValidationMode.LostFocus;
            _sfDataForm.CommitMode = CommitMode.LostFocus;
            _sfDataForm.ColumnCount = 1;
            _dataContainerRelativeLayout.AddView(_sfDataForm, dataFormParams);

            _completeRegBtn.Click += CompleteRegBtn_Click;

            return view;
        }

        private void CompleteRegBtn_Click(object sender, EventArgs e)
        {
            if (!(_sfDataForm.Validate() && _sfDataForm2.Validate())) return;
            _sfDataForm.Commit();
            _sfDataForm2.Commit();
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
    }
}