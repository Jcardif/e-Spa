using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Com.Mukesh.CountryPickerLib;
using e_SpaMobileApp.Models;
using Syncfusion.Android.DataForm;

namespace e_SpaMobileApp.ExtensionsAndHelpers
{
    public class DataFormLayoutManagerExt : DataFormLayoutManager,IOnCountryPickerListener
    {
        private int _code;
        private Context _context;
        private FragmentManager _fragmentManager;
        private EditText codeEdtTxt;
        private SfDataForm _dataForm;
        public DataFormLayoutManagerExt(SfDataForm dataForm, int code) : base(dataForm)
        {
            _dataForm = dataForm;
            _code = code;
        }

        public DataFormLayoutManagerExt(SfDataForm dataForm, int code, FragmentManager fragmentManager, Context context):base(dataForm)
        {
            _dataForm = dataForm;
            _code = code;
            _fragmentManager = fragmentManager;
            _context = context;
        }

        protected override View GenerateViewForLabel(DataFormItem dataFormItem)
        {
            var label = base.GenerateViewForLabel(dataFormItem);
            if (label is TextView view)
            {
                view.Typeface=Typeface.DefaultBold;
                view.TextSize = 16;
                view.SetTextColor(Color.White);
            }

            return label;
        }

        protected override void OnEditorCreated(DataFormItem dataFormItem, View editor)
        {
            if(editor is EditText edtTxt)
                edtTxt.SetTextColor(Color.White);

            ((EditText) editor).Typeface = Typeface.Default;
            ((EditText) editor).SetBackgroundResource(Resource.Drawable.syncfusion_editText_style);
            ((EditText) editor).SetTextColor(Color.White);
            ((EditText)editor).SetHintTextColor(Color.WhiteSmoke);
            if (dataFormItem.Name == "CountryCode")
            {
                codeEdtTxt = ((EditText) editor);
                codeEdtTxt.Focusable = false;
                editor.Click += Editor_Click; ;
            }
            if(_code==1) return;
            switch (dataFormItem.Name)
            {
                case "FirstName":
                    ((EditText)editor).Hint = "First Name";
                    break;
                case "LastName":
                    ((EditText)editor).Hint = "Last Name";
                    break;
                case "Email":
                    ((EditText)editor).Hint = "Email";
                    break;
                case "Residence":
                    ((EditText)editor).Hint = "Residence";
                    break;
                case "PhoneNumber":
                    ((EditText)editor).Hint = "Phone Number";
                    break;
                default:
                    return;
            }
        }

        private void Editor_Click(object sender, System.EventArgs e)
        {
            var builder = new CountryPicker.Builder()
                .With(_context)
                .Listener(this);
            var picker = builder.Build();
            picker.ShowDialog(_fragmentManager);
        }

        public void OnSelectCountry(Country country)
        {
            codeEdtTxt.Text = country.DialCode;
        }
    }
}