using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using e_SpaMobileApp.Activities;
using e_SpaMobileApp.APIClients;
using e_SpaMobileApp.ExtensionsAndHelpers;
using e_SpaMobileApp.Models;
using e_SpaMobileApp.ServiceModels;
using Firebase;
using Firebase.Auth;
using Java.Util.Concurrent;
using Newtonsoft.Json;
using Plugin.CurrentActivity;
using static e_SpaMobileApp.ExtensionsAndHelpers.FirebaseHelpers;

namespace e_SpaMobileApp.Fragments
{
    public class PhoneNumberVerificationFragment : Fragment, IOnSingInCallbacks, IOnCompleteListener
    {
        private string _verificationId;
        private TextInputEditText _textInputEditText1,
            _textInputEditText2,
            _textInputEditText3,
            _textInputEditText4,
            _textInputEditText5,
            _textInputEditText6;
        
        private UserInfo _userInfo;
        private PhoneInfo _phoneInfo;
        private TextView _resendCodeTxtView, _verifyTitleTxtView, _waitingTxtView;
        private string _phoneNo;

        public override  void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (Arguments==null)
                return;
            var phInfo = Arguments.GetString("phoneInfo");
            var usInfo = Arguments.GetString("userInfo");
            if(phInfo==null & _userInfo==null)
                return;
            _phoneInfo = JsonConvert.DeserializeObject<PhoneInfo>(phInfo);
            _userInfo = JsonConvert.DeserializeObject<UserInfo>(usInfo);
            _phoneNo = string.Concat(_phoneInfo.CountryCode, _phoneInfo.PhoneNumber);


            InitFirebaseAuth(CrossCurrentActivity.Current.Activity);
            SendVerificationCode();

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_verifyPhoneNumber, container, false);
            _textInputEditText1 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText1);
            _textInputEditText2 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText2);
            _textInputEditText3 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText3);
            _textInputEditText4 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText4);
            _textInputEditText5 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText5);
            _textInputEditText6 = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText6);
            _resendCodeTxtView = view.FindViewById<TextView>(Resource.Id.resendCodeTxtView);
            _verifyTitleTxtView = view.FindViewById<TextView>(Resource.Id.verifyTitleTxtView);
            _waitingTxtView = view.FindViewById<TextView>(Resource.Id.waitingTxtView);

            _verifyTitleTxtView.Text = $"Verify {_phoneNo}";
            _waitingTxtView.Text =
                $"Waiting to automatically detect the verification code to sent to {_phoneNo}." +
                $"If the code is not automatically detected please input the six digit Code here.";
            _textInputEditText1.TextChanged += _textInputEditText_TextChanged;
            _textInputEditText2.TextChanged += _textInputEditText_TextChanged;
            _textInputEditText3.TextChanged += _textInputEditText_TextChanged;
            _textInputEditText4.TextChanged += _textInputEditText_TextChanged;
            _textInputEditText5.TextChanged += _textInputEditText_TextChanged;
            _textInputEditText6.TextChanged += _textInputEditText_TextChanged;

            _resendCodeTxtView.Click += (s, e) => { SendVerificationCode(); };
            return view;
        }

        private void _textInputEditText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var edtTxt = (EditText) sender;
            if (e.AfterCount==0)
            {
                switch (edtTxt.Id)
                {
                    case Resource.Id.textInputEditText1:
                        return;
                    case Resource.Id.textInputEditText2:
                        _textInputEditText1.RequestFocus();
                        break;
                    case Resource.Id.textInputEditText3:
                        _textInputEditText2.RequestFocus();
                        break;
                    case Resource.Id.textInputEditText4:
                        _textInputEditText3.RequestFocus();
                        break;
                    case Resource.Id.textInputEditText5:
                        _textInputEditText4.RequestFocus();
                        break;
                    case Resource.Id.textInputEditText6:
                        _textInputEditText5.RequestFocus();
                        break;
                    default:
                        return;
                }
            }
            else
            {

                switch (edtTxt.Id)
                {
                    case Resource.Id.textInputEditText1 when !string.IsNullOrEmpty(edtTxt.Text):
                        _textInputEditText2.RequestFocus();
                        break;
                    case Resource.Id.textInputEditText2 when !string.IsNullOrEmpty(edtTxt.Text):
                        _textInputEditText3.RequestFocus();
                        break;
                    case Resource.Id.textInputEditText3 when !string.IsNullOrEmpty(edtTxt.Text):
                        _textInputEditText4.RequestFocus();
                        break;
                    case Resource.Id.textInputEditText4 when !string.IsNullOrEmpty(edtTxt.Text):
                        _textInputEditText5.RequestFocus();
                        break;
                    case Resource.Id.textInputEditText5 when !string.IsNullOrEmpty(edtTxt.Text):
                        _textInputEditText6.RequestFocus();
                        break;
                    case Resource.Id.textInputEditText6 when !string.IsNullOrEmpty(edtTxt.Text):
                        var verificationCode = string.Concat(_textInputEditText1.Text, _textInputEditText2.Text,
                            _textInputEditText3.Text, _textInputEditText4.Text, _textInputEditText5.Text,
                            _textInputEditText6.Text);
                        VerifySentCode(verificationCode);
                        break;
                }
            }
        }

        private  void SendVerificationCode()
        {
            var callbacks=new PhoneAuthCallBacks(this);
             PhoneAuthProvider.GetInstance(_auth).VerifyPhoneNumber(
                _phoneNo,
                120,
                TimeUnit.Seconds, 
                CrossCurrentActivity.Current.Activity,
                callbacks);
        }

        private void VerifySentCode(string verificationCode)
        {
            var credential = PhoneAuthProvider.GetCredential(_verificationId, verificationCode);
            OnVerificationCompleted(credential);
        }



        
        public void OnVerificationCompleted(PhoneAuthCredential credential)
        {
            var smsCode = credential.SmsCode;
            _textInputEditText1.Focusable = false;
            _textInputEditText2.Focusable = false;
            _textInputEditText3.Focusable = false;
            _textInputEditText4.Focusable = false;
            _textInputEditText5.Focusable = false;
            _textInputEditText6.Focusable = false;

            _textInputEditText1.Text = smsCode[0].ToString();
            _textInputEditText2.Text = smsCode[1].ToString();
            _textInputEditText3.Text = smsCode[2].ToString();
            _textInputEditText4.Text = smsCode[3].ToString();
            _textInputEditText5.Text = smsCode[4].ToString();
            _textInputEditText6.Text = smsCode[5].ToString();

            DisplayVerificationSuccess();

            _auth.SignInWithCredential(credential)
                .AddOnCompleteListener(this);
        }

        private void DisplayVerificationSuccess()
        {
            var fragment = new PhoneNoVerifiedFragment();
            FragmentManager.BeginTransaction()
                .Replace(Resource.Id.frameLayout1, fragment)
                .SetCustomAnimations(Resource.Animation.zoom_in, Resource.Animation.fade_out)
                .Commit();
        }

        public void OnCodeSent(string verificationId, PhoneAuthProvider.ForceResendingToken forceResendingToken)
        {
            Toast.MakeText(Context.ApplicationContext,"Sent",ToastLength.Long).Show();
            _verificationId = verificationId;
            _resendCodeTxtView.Enabled = false;
        }

        public void OnVerificationFailed(FirebaseException exception)
        {
            switch (exception)
            {
                case FirebaseAuthInvalidCredentialsException _:
                    Toast.MakeText(Context.ApplicationContext, "Invalid request", ToastLength.Long).Show();
                    break;
                case FirebaseTooManyRequestsException _:
                    Toast.MakeText(Context.ApplicationContext, "Too Many Request", ToastLength.Long).Show();
                    break;
                default:
                    Toast.MakeText(Context.ApplicationContext, exception.Message, ToastLength.Long).Show();
                    break;
            }
        }

        public void OnCodeAutoRetrivalTimeOut()
        {
            _resendCodeTxtView.Enabled = true;
        }

        public async void OnComplete(Task task)
        {
            if(task.IsSuccessful)
            {
                if (_userInfo == null)
                StartActivity(new Intent(Context.ApplicationContext, typeof(MainActivity)));
            else
            {
                var client = new Client
                {
                    Email = _userInfo.Email,
                    FirstName = _userInfo.FirstName,
                    LastName = _userInfo.LastName,
                    PhoneNumber = string.Concat(_phoneInfo.CountryCode, _phoneInfo.PhoneNumber),
                    Residence = "Residence",
                    ProfilePhotoUrl = "my-url"
                };
                var userApiClient = new UserApiClient();
                    var newClient = await userApiClient.AddClientAsync(client);
                    var fragment = new CompleteRegistrationFragment();
                    var bundle = new Bundle();
                    bundle.PutString("client", JsonConvert.SerializeObject(newClient));
                    fragment.Arguments = bundle;

                    FragmentManager.BeginTransaction()
                        .SetCustomAnimations(Resource.Animation.anim_enter, Resource.Animation.anim_exit)
                        .Replace(Resource.Id.authorizationContainer, fragment)
                        .Commit();

                    //TODO Remove From Backstack
                
            }}
            else
            {
                var exception = task.Exception;
                switch (exception)
                {
                    case FirebaseAuthInvalidCredentialsException _:
                        Toast.MakeText(Context.ApplicationContext, "The verification code entered was invalid", ToastLength.Long).Show();
                        break;
                    default:
                        Toast.MakeText(Context.ApplicationContext,exception.Message,ToastLength.Long).Show();
                        break;
                }
            }
        }
    }
}