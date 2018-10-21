using System;
using System.Net;
using System.Net.Http;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Cocosw.BottomSheetActions;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Load.Engine;
using Com.Bumptech.Glide.Request;
using e_SpaMobileApp.Activities;
using e_SpaMobileApp.APIClients;
using e_SpaMobileApp.ExtensionsAndHelpers;
using e_SpaMobileApp.Models;
using e_SpaMobileApp.ServiceModels;
using Java.Security;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.CurrentActivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Refractored.Controls;
using Syncfusion.Android.DataForm;
using Fragment = Android.Support.V4.App.Fragment;
using IKey = Com.Bumptech.Glide.Load.IKey;
using Permission = Android.Content.PM.Permission;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace e_SpaMobileApp.Fragments
{
    public class CompleteRegistrationFragment : Fragment, IDialogInterfaceOnClickListener
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
        private ProgressBar _progressBar;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if(Arguments==null) return; 
            _client = JsonConvert.DeserializeObject<Client>(Arguments.GetString("client"));
            if (_client.Residence == "Residence")
                _client.Residence = null;
            if (_client.ProfilePhotoUrl == "my-url")
                _client.ProfilePhotoUrl = null;
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
            _progressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBarInCircleImgView);
            
            
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
                    new BottomSheet.Builder(CrossCurrentActivity.Current.Activity, Resource.Style.SoarcnBottomSheet)
                        .Sheet(Resource.Menu.bottomsheetmedia)
                        .Grid()
                        .Title("Select Image From")
                        .Icon(Resource.Drawable.Camera_alt_24px)
                        .Listener(this)
                        .Show();
                };

            var dataFormParams2 = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dataFormParams2.AddRule(LayoutRules.CenterHorizontal);
            dataFormParams2.Width = ViewGroup.LayoutParams.MatchParent;
            dataFormParams2.Height = ViewGroup.LayoutParams.WrapContent;

            _sfDataForm2 = new SfDataForm(Context.ApplicationContext);
            _sfDataForm2.DataObject = fullName;
            _sfDataForm2.LayoutManager = new DataFormLayoutManagerExt(_sfDataForm2);
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
            _sfDataForm.LayoutManager = new DataFormLayoutManagerExt(_sfDataForm);
            _sfDataForm.LabelPosition = LabelPosition.Left;
            _sfDataForm.Id = View.GenerateViewId();
            _sfDataForm.ValidationMode = ValidationMode.LostFocus;
            _sfDataForm.CommitMode = CommitMode.LostFocus;
            _sfDataForm.ColumnCount = 1;
            _dataContainerRelativeLayout.AddView(_sfDataForm, dataFormParams);

            _completeRegBtn.Click += CompleteRegBtn_Click;

            return view;
        }

        private async void CompleteRegBtn_Click(object sender, EventArgs e)
        {
            if (!(_sfDataForm.Validate() && _sfDataForm2.Validate())) return;
            _sfDataForm.Commit();
            _sfDataForm2.Commit();
            _client.FirstName = fullName.FirstName;
            _client.LastName = fullName.LastName;
            if (_client.ProfilePhotoUrl == null) _client.ProfilePhotoUrl = "default";
            if (CrossConnectivity.Current.IsConnected)
            {
                //! Upload data to database and image to blob storage
                var userApiClient = new UserApiClient();
                await userApiClient.UpdateClient(_client);
                Toast.MakeText(Context.ApplicationContext, $"Welcome {_client.FirstName}", ToastLength.Short).Show();
                StartActivity(new Intent(Context.ApplicationContext, typeof(MainActivity)));
            }
            else
            {
                Toast.MakeText(Context.ApplicationContext, "No Internet Connection", ToastLength.Short).Show();
            }
        }

        public void OnClick(IDialogInterface dialog, int which)
        {
            switch (which)
            {
                case Resource.Id.action_avatar:
                    Toast.MakeText(Context.ApplicationContext, "Avatar will be available soon", ToastLength.Short).Show();
                    break;
                case Resource.Id.action_camera:
                    OpenCameraAndTakePhoto();
                    break;
                case Resource.Id.action_gallery:
                    PickAndLoadImage();
                    break;
            }
        }

        private async void PickAndLoadImage()
        {
            try
            {
                 var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions());
                if(file==null) return;
                UploadFileToStorage(file);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Context.ApplicationContext, ex.Message, ToastLength.Short).Show();
            }
        }

        private async void OpenCameraAndTakePhoto()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    Toast.MakeText(Context.ApplicationContext, "Camera Not Available", ToastLength.Short).Show();
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "images",
                    Name = $"{_client.Id}.jpg"
                });
                if (file == null)
                    return;
                UploadFileToStorage(file);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Context.ApplicationContext,ex.Message,ToastLength.Short).Show();
            }
        }

        private async void UploadFileToStorage(MediaFile file)
        {
            _progressBar.Visibility = ViewStates.Visible;
            var blobStorageService = new BlobStorageService.BlobStorageService();
            blobStorageService.Init("espa-clients-profle-images");
            _client.ProfilePhotoUrl=await blobStorageService.UploadToBlobStorage(file.Path, _client.Id);
            var uri = _client.ProfilePhotoUrl.Replace("espa-clients-profle-images", "espa-clients-profle-images-sm");
            _client.ProfilePhotoUrl = uri;
            var token = "";


            var response = new HttpClient().GetAsync(
                "https://e-spafunctions.azurewebsites.net/api/GenerateBlobStorageSas?code=AwY0HVt1H13fCEX3Qy4vIeIgFjHNjFE72FPqwAlRXQlVE0BrfeFgdg==&containerName=espa-clients-profle-images-sm");
            if (response.Result.StatusCode == HttpStatusCode.OK)
            {
                token = response.Result.Content.ReadAsStringAsync().Result.Replace("\"", "");
            }
            else
            {
                Toast.MakeText(Context.ApplicationContext, "An Error Occured", ToastLength.Short).Show();
            }

            var final = uri + token;
            Glide.With(Context.ApplicationContext)
                .Load(final)
                .Into(_profilePicImgView);
            _progressBar.Visibility = ViewStates.Invisible;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}