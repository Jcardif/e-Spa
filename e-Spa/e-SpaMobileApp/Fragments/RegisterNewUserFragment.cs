using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
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
using Com.Mukesh.CountryPickerLib.Listeners;
using Com.Syncfusion.Sfbusyindicator;
using Com.Syncfusion.Sfbusyindicator.Enums;
using e_SpaMobileApp.ExtensionsAndHelpers;
using e_SpaMobileApp.Models;
using e_SpaMobileApp.ServiceModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
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
        private SfDataForm dataForm3;
        private SfCheckBox sfCheckbox;
        private UserInfo userInfo;
        private PhoneInfo phoneInfo;
        private Client client;
        private bool isChecked;
        private static EditText codeEditText;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("721391dd-e2f0-40be-b57a-55581909179b", typeof(Analytics), typeof(Crashes));
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity.Window.SetSoftInputMode(SoftInput.AdjustPan);
            var view = new RelativeLayout(Context.ApplicationContext);
            view.SetBackgroundColor(Color.ParseColor("#80000000"));
            view.SetPadding(8, 8, 8, 8);

            var dataFormParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dataFormParams.AddRule(LayoutRules.CenterInParent);
            dataFormParams.Width = ViewGroup.LayoutParams.MatchParent;
            dataFormParams.Height = ViewGroup.LayoutParams.WrapContent;

            dataForm = new SfDataForm(Context.ApplicationContext);
            dataForm.Id = View.GenerateViewId();
            userInfo = new UserInfo();
            dataForm.DataObject = userInfo;
            dataForm.LayoutManager = new DataFormLayoutManagerExt(dataForm);
            dataForm.LabelPosition = LabelPosition.Top;
            dataForm.ValidationMode = ValidationMode.LostFocus;
            dataForm.CommitMode = CommitMode.LostFocus;
            view.AddView(dataForm, dataFormParams);

            var edtParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            edtParams.AddRule(LayoutRules.Below, dataForm.Id);
            edtParams.Width = ViewGroup.LayoutParams.WrapContent;
            edtParams.LeftMargin = 48;
            edtParams.Height = ViewGroup.LayoutParams.WrapContent;

            

            var dataForm2Params = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dataForm2Params.AddRule(LayoutRules.Below, dataForm.Id);
            dataForm2Params.Width = ViewGroup.LayoutParams.MatchParent;
            dataForm2Params.RightMargin = 48;
            dataForm2Params.Height = ViewGroup.LayoutParams.WrapContent;

            dataForm2 = new SfDataForm(Context.ApplicationContext);
            phoneInfo = new PhoneInfo();
            dataForm2.DataObject = phoneInfo;
            dataForm2.ColumnCount = 4;
            dataForm2.LayoutManager = new DataFormLayoutManagerExt(dataForm2, ShowCountryListDialog);
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
            txtView.SetPadding(2, 2, 2, 2);
            txtView.Clickable = true;
            txtView.SetTextColor(Color.White);
            view.AddView(txtView, txtViewLayoutParams);

            var sfCheckboxParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            sfCheckboxParams.AddRule(LayoutRules.Below, dataForm2.Id);
            sfCheckboxParams.Width = ViewGroup.LayoutParams.WrapContent;
            sfCheckboxParams.Height = ViewGroup.LayoutParams.WrapContent;
            sfCheckboxParams.SetMargins(6, 4, 2, 2);


            sfCheckbox = new SfCheckBox(Context.ApplicationContext);
            int[][] states = { new[] { Android.Resource.Attribute.StateChecked }, new[] { -Android.Resource.Attribute.StateChecked } };
            int[] colors = { Color.Purple, Color.White };
            sfCheckbox.Checked = false;
            sfCheckbox.Text = "I Accept the terms of use of the Application";
            sfCheckbox.TextSize = 10;
            sfCheckbox.CornerRadius = 5.0f;
            sfCheckbox.SetTextColor(Color.White);
            sfCheckbox.ButtonTintList = new ColorStateList(states, colors);
            sfCheckbox.CheckedChange += SfCheckbox_CheckedChange;
            view.AddView(sfCheckbox, sfCheckboxParams);

            
            txtView.Click += TxtView_Click;
            return view;
        }

        private void ShowCountryListDialog(EditText editText)
        {
            codeEditText = editText;
                var builder = new CountryPicker.Builder()
                    .With(CrossCurrentActivity.Current.Activity)
                    .Listener(this);
                var picker = builder.Build();
                picker.ShowDialog(CrossCurrentActivity.Current.Activity);
           
        }
        private void SfCheckbox_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            isChecked = e.IsChecked;
        }


        private async void TxtView_Click(object sender, EventArgs e)
        {
            var isValid = dataForm.Validate() && dataForm2.Validate();
            if (!isValid) return;
            if (!isChecked)
                Toast.MakeText(Context.ApplicationContext, "Accept the terms of use", ToastLength.Short).Show();
            else
            {
                if (!CrossConnectivity.Current.IsConnected)
                    Toast.MakeText(Context.ApplicationContext,"No Internet Connection", ToastLength.Short).Show();
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

                    var builder = new AlertDialog.Builder(CrossCurrentActivity.Current.Activity,
                        Resource.Style.AlertDialogTheme);
                    var alertDialog = builder.SetView(sfBsyIndicator).Create();
                    alertDialog.SetCanceledOnTouchOutside(false);
                    alertDialog.Show();
                    alertDialog.Window.SetLayout(1000, 300);

                    var phoneNumber = string.Concat(phoneInfo.CountryCode, phoneInfo.PhoneNumber);
                    var httpClient = new HttpClient();
                    var funcUri =
                        $"https://e-spafunctions.azurewebsites.net/api/UserExistence?code=FvvkqYX9HQxJsr4AliXfv6jqZ3uttw8wzUNezzKiXHowx4EwUVdqdQ==&phoneNo={phoneNumber}";
                    var response = await httpClient.GetAsync(funcUri);

                    alertDialog.Cancel();


                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Toast.MakeText(Context.ApplicationContext,
                            "An Account is already associated with that phoneNumber", ToastLength.Long).Show();
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


                        var alertDialogBuilder = new AlertDialog.Builder(CrossCurrentActivity.Current.Activity,Resource.Style.AppTheme)
                            .SetTitle("Phone Number Verification")
                            .SetMessage($"We shall be verifying the phone Number {string.Concat(phoneInfo.CountryCode,phoneInfo.PhoneNumber)} by sending an SMS with a verification Code. Press Yes to continue or No modify the Phone Number")
                            .SetNegativeButton("No", (s, arg) => {Dispose();})
                            .SetPositiveButton("Yes",((s, args) =>
                            {
                                var transaction = FragmentManager.BeginTransaction();
                                transaction.SetCustomAnimations(Resource.Animation.anim_enter, Resource.Animation.anim_exit)
                                    .Replace(Resource.Id.authorizationContainer, fragment)
                                    .AddToBackStack("RegisterNewUser")
                                    .Commit();
                            }))
                            .Create();
                        alertDialogBuilder.Window.SetLayout(1000,450);
                        alertDialogBuilder.Show();
                    }
                }
            }
        }

        public void OnSelectCountry(Country country)
        {
            codeEditText.Text = country.DialCode;
            codeEditText.ClearFocus();
        }
    }
}