using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Mukesh.CountryPickerLib;
using Com.Syncfusion.Sfbusyindicator;
using Com.Syncfusion.Sfbusyindicator.Enums;
using e_SpaMobileApp.ExtensionsAndHelpers;
using e_SpaMobileApp.Models;
using e_SpaMobileApp.ServiceModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.CurrentActivity;
using Syncfusion.Android.Buttons;
using Syncfusion.Android.DataForm;
using Fragment = Android.Support.V4.App.Fragment;

namespace e_SpaMobileApp.Fragments
{
    public class RegisterNewUserFragment : Fragment
    {
        private SfDataForm dataForm;
        private SfDataForm dataForm2;
        private SfDataForm dataForm3;
        private SfCheckBox sfCheckbox;
        private UserInfo userInfo;
        private PhoneInfo phoneInfo;
        private CodeInfo codeInfo;
        private Client client;
        private bool isChecked;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity.Window.SetSoftInputMode(SoftInput.AdjustPan);
            var view=new RelativeLayout(Context.ApplicationContext);
            view.SetBackgroundColor(Color.ParseColor("#80000000"));
            view.SetPadding(8,8,8,8);

            var dataFormParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dataFormParams.AddRule(LayoutRules.CenterInParent);
            dataFormParams.Width = ViewGroup.LayoutParams.MatchParent;
            dataFormParams.Height = ViewGroup.LayoutParams.WrapContent;

            dataForm=new SfDataForm(Context.ApplicationContext);
            dataForm.Id=View.GenerateViewId();
            userInfo = new UserInfo();
            dataForm.DataObject=userInfo;
            dataForm.LayoutManager=new DataFormLayoutManagerExt(dataForm,1);
            dataForm.LabelPosition = LabelPosition.Top;
            dataForm.ValidationMode = ValidationMode.LostFocus;
            dataForm.CommitMode = CommitMode.LostFocus;
            view.AddView(dataForm,dataFormParams);

            var dataForm3Params = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dataForm3Params.AddRule(LayoutRules.Below, dataForm.Id);
            dataForm3Params.Width = 300;
            dataForm3Params.Height = ViewGroup.LayoutParams.WrapContent;

            dataForm3 = new SfDataForm(Context.ApplicationContext);
            codeInfo = new CodeInfo();
            dataForm3.DataObject = codeInfo;
            dataForm3.LayoutManager = new DataFormLayoutManagerExt(dataForm3, 1, FragmentManager, Context.ApplicationContext);
            dataForm3.LabelPosition = LabelPosition.Top;
            dataForm3.Id = View.GenerateViewId();
            dataForm3.ValidationMode = ValidationMode.LostFocus;
            dataForm3.CommitMode = CommitMode.LostFocus;
            view.AddView(dataForm3, dataForm3Params);

            var dataForm2Params = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dataForm2Params.AddRule(LayoutRules.Below, dataForm.Id);
            dataForm2Params.AddRule(LayoutRules.RightOf, dataForm3.Id);
            dataForm2Params.LeftMargin=-150;
            dataForm2Params.Width = ViewGroup.LayoutParams.WrapContent;
            dataForm2Params.Height = ViewGroup.LayoutParams.WrapContent;

            dataForm2 = new SfDataForm(Context.ApplicationContext);
            phoneInfo = new PhoneInfo();
            dataForm2.DataObject = phoneInfo;
            dataForm2.LayoutManager=new DataFormLayoutManagerExt(dataForm2,1);
            dataForm2.LabelPosition = LabelPosition.Top;
            dataForm2.Id = View.GenerateViewId();
            dataForm2.ValidationMode = ValidationMode.LostFocus;
            dataForm2.CommitMode = CommitMode.LostFocus;
            view.AddView(dataForm2, dataForm2Params);

            var txtViewLayoutParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            txtViewLayoutParams.AddRule(LayoutRules.AlignParentBottom);
            txtViewLayoutParams.AddRule(LayoutRules.AlignParentRight);
            txtViewLayoutParams.Width = ViewGroup.LayoutParams.WrapContent;
            txtViewLayoutParams.Height = ViewGroup.LayoutParams.WrapContent;

            var txtView = new TextView(Context.ApplicationContext);
            txtView.Text = "Next";
            txtView.TextSize = 28;
            txtView.SetPadding(2, 2, 4, 4);
            txtView.Clickable = true;
            txtView.SetTextColor(Color.White);
            view.AddView(txtView, txtViewLayoutParams);

            var sfCheckboxParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            sfCheckboxParams.AddRule(LayoutRules.Below, dataForm3.Id);
            sfCheckboxParams.Width = ViewGroup.LayoutParams.WrapContent;
            sfCheckboxParams.Height = ViewGroup.LayoutParams.WrapContent;
            sfCheckboxParams.SetMargins(6,4,2,2);


            sfCheckbox = new SfCheckBox(Context.ApplicationContext);
            int[][] states ={new[]{ Android.Resource.Attribute.StateChecked },new[]{-Android.Resource.Attribute.StateChecked }};
            int[] colors = {Color.Purple, Color.White};
            sfCheckbox.Checked = false;
            sfCheckbox.Text = "I Accept the terms of use of the Application";
            sfCheckbox.TextSize = 10;
            sfCheckbox.CornerRadius = 5.0f;
            sfCheckbox.SetTextColor(Color.White);
            sfCheckbox.ButtonTintList = new ColorStateList(states, colors);
            sfCheckbox.StateChanged += SfCheckbox_StateChanged;
            view.AddView(sfCheckbox, sfCheckboxParams);

            txtView.Click += TxtView_Click;
            return view;
        }

        private void SfCheckbox_StateChanged(object sender, StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
                isChecked = true;
        }


        private async void TxtView_Click(object sender, EventArgs e)
        {
            var isValid = dataForm.Validate() && dataForm2.Validate();
            if (!isValid) return;
            if(!isChecked)
                Toast.MakeText(Context.ApplicationContext, "Accept the terms of use", ToastLength.Short).Show();
            else
            {
                dataForm.Commit();
                dataForm2.Commit();
      
                var sfBsyIndicator = new SfBusyIndicator(Context.ApplicationContext);
                sfBsyIndicator.AnimationType = AnimationTypes.Ball;
                sfBsyIndicator.TitlePlacement = TitlePlacement.Top;
                sfBsyIndicator.TextColor = Color.Purple;
                sfBsyIndicator.ViewBoxHeight = 100;
                sfBsyIndicator.ViewBoxWidth = 100;
                sfBsyIndicator.IsBusy = true;
                sfBsyIndicator.SecondaryColor = Color.Purple;
                sfBsyIndicator.SetBackgroundResource(Color.Transparent);

                var builder = new AlertDialog.Builder(CrossCurrentActivity.Current.Activity, Resource.Style.AlertDialogTheme);
                var alertDialog = builder.SetView(sfBsyIndicator).Create();
                alertDialog.SetCanceledOnTouchOutside(false);
                alertDialog.Show();
                alertDialog.Window.SetLayout(1000, 300);

                var phoneNumber = string.Concat(codeInfo.CountryCode, phoneInfo.PhoneNumber);
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(
                    $"https://e-spafunc.azurewebsites.net/api/UserExistence?code=nmiRKDPhdRQRteTlJTy97pyr213nx8KWgKxqCxq6CYINniEpg0RsSg==&phoneNo={phoneNumber}");

                alertDialog.Cancel();
                

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Toast.MakeText(Context.ApplicationContext, "An Account is already associated with that phoneNumber", ToastLength.Long).Show();
                    var str = await response.Content.ReadAsStringAsync();
                    client = JsonConvert.DeserializeObject<Client>(str);
                }
                else
                {
                    var fragment = new PhoneNumberVerificationFragment();
                    var phInfo = JsonConvert.SerializeObject(phoneInfo);
                    var usInfo = JsonConvert.SerializeObject(userInfo);

                    var bundle = new Bundle();
                    bundle.PutString("phoneInfo", phInfo);
                    bundle.PutString("userInfo", usInfo);
                    fragment.Arguments = bundle;

                    var transaction = FragmentManager.BeginTransaction();
                    transaction.SetCustomAnimations(Resource.Animation.anim_enter, Resource.Animation.anim_exit)
                        .Replace(Resource.Id.authorizationContainer, fragment)
                        .AddToBackStack(null)
                        .Commit();
                }
            }
        }
    }
}