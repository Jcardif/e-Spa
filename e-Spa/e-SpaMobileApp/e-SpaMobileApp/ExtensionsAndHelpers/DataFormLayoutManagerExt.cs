using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Com.Mukesh.CountryPickerLib;
using Plugin.CurrentActivity;
using Syncfusion.Android.DataForm;

namespace e_SpaMobileApp.ExtensionsAndHelpers
{
    public class DataFormLayoutManagerExt : DataFormLayoutManager
    {
        
        private EditText _editText;
        private static EditText _myEdtTxt;

        public delegate void LoadCountryDialog(EditText edtTxt);

        private LoadCountryDialog loadCountryDialog;

        public DataFormLayoutManagerExt(SfDataForm dataForm) : base(dataForm)
        {
        }

        public DataFormLayoutManagerExt(SfDataForm dataForm, LoadCountryDialog loadCountryDialog):base(dataForm)
        {
            this.loadCountryDialog = loadCountryDialog;
        }
        protected override View GenerateViewForLabel(DataFormItem dataFormItem)
        {
            var label = base.GenerateViewForLabel(dataFormItem);
            if (label is TextView view)
            {
                view.Typeface = Typeface.DefaultBold;
                view.TextSize = 16;
                view.SetTextColor(Color.White);
            }
            return label;
        }
        
       

        protected override void OnEditorCreated(DataFormItem dataFormItem, View editor)
        {
            if (editor is EditText edtTxt)
            {
                _editText = edtTxt;
            }
            

            _editText.Typeface = Typeface.Default;
            _editText.SetBackgroundResource(Resource.Drawable.syncfusion_editText_style);
            _editText.SetTextColor(Color.White);
            _editText.SetHintTextColor(Color.WhiteSmoke);
            
            _editText.FocusChange += EditText_FocusChange;

            if (dataFormItem.Name == "CountryCode")
            {
                _editText.Focusable = true;
                _editText.Id = 456342;
                _myEdtTxt = _editText;
            }

        }

        private void EditText_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if(!e.HasFocus)return;
            var id = ((EditText)sender).Id;
            if (id == 456342)
            {
                loadCountryDialog.Invoke(_myEdtTxt);
            }
        }

    }
}