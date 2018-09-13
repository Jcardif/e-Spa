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
    public class RegisterNewUserFragment : Fragment, IOnCountryPickerListener
    {
        private SfDataForm dataForm;
        private SfDataForm dataForm2;
        private SfCheckBox sfCheckbox;
        private UserInfo userInfo;
        private PhoneInfo phoneInfo;
        private EditText edtTxt;
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
            dataForm.LayoutManager=new DataFormLayoutManagerExt(dataForm);
            dataForm.LabelPosition = LabelPosition.Top;
            dataForm.ValidationMode = ValidationMode.LostFocus;
            dataForm.CommitMode = CommitMode.LostFocus;
            dataForm.RegisterEditor("Text", new CustomTextEditor(dataForm));
            view.AddView(dataForm,dataFormParams);

            var edtParams=new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            edtParams.AddRule(LayoutRules.Below, dataForm.Id);
            edtParams.Width=ViewGroup.LayoutParams.WrapContent;
            edtParams.LeftMargin = 48;
            edtParams.Height=ViewGroup.LayoutParams.WrapContent;

            edtTxt = new EditText(Context.ApplicationContext);
            edtTxt.Id = View.GenerateViewId();
            edtTxt.Hint = "Code";
            edtTxt.SetHintTextColor(Color.White);
            edtTxt.Focusable = false;
            edtTxt.Id = View.GenerateViewId();
            edtTxt.SetTextColor(Color.White);
            edtTxt.SetMaxLines(1);
            edtTxt.Gravity = GravityFlags.Top;
            view.AddView(edtTxt, edtParams);
            
            var dataForm2Params = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dataForm2Params.AddRule(LayoutRules.Below, dataForm.Id);
            dataForm2Params.AddRule(LayoutRules.RightOf, edtTxt.Id);
            dataForm2Params.Width = ViewGroup.LayoutParams.MatchParent;
            dataForm2Params.RightMargin = 48;
            dataForm2Params.Height = ViewGroup.LayoutParams.WrapContent;

            dataForm2 = new SfDataForm(Context.ApplicationContext);
            phoneInfo = new PhoneInfo();
            dataForm2.DataObject = phoneInfo;
            dataForm2.LayoutManager=new DataFormLayoutManagerExt(dataForm2);
            dataForm2.LabelPosition = LabelPosition.Top;
            dataForm2.Id = View.GenerateViewId();
            dataForm2.RegisterEditor("Text", new CustomTextEditor(dataForm2));
            dataForm2.RegisterEditor("PhoneNumber", "Text");
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
            txtView.SetPadding(2, 2, 2, 2);
            txtView.Clickable = true;
            txtView.SetTextColor(Color.White);
            view.AddView(txtView, txtViewLayoutParams);

            var sfCheckboxParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            sfCheckboxParams.AddRule(LayoutRules.Below, dataForm2.Id);
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


            edtTxt.Click += (s,e) => { GetCountryCode(); };
            txtView.Click += TxtView_Click;
            return view;
        }

        private void SfCheckbox_StateChanged(object sender, StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
                isChecked = true;
        }

        private void GetCountryCode()
        {
            var builder = new CountryPicker.Builder()
                .With(Context.ApplicationContext)
                .Listener(this);
            var picker = builder.Build();
            picker.ShowDialog(FragmentManager);
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

                var phoneNumber = string.Concat(phoneInfo.CountryCode, phoneInfo.PhoneNumber);
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

        public void OnSelectCountry(Country country)
        {
            phoneInfo.CountryCode = country.DialCode;
            edtTxt.Text = phoneInfo.CountryCode;
        }
    }
}