using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Syncfusion.Android.DataForm;

namespace e_SpaMobileApp.ExtensionsAndHelpers
{
    public class DataFormLayoutManagerExt : DataFormLayoutManager
    {
        public DataFormLayoutManagerExt(SfDataForm dataForm) : base(dataForm)
        {
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
            ((EditText)editor).SetHintTextColor(Color.White);
            switch (dataFormItem.Name)
            {
                case "FirstName":
                    ((EditText) editor).Hint = "First Name";
                    break;
                case "LastName":
                    ((EditText) editor).Hint = "Last Name";
                    break;
                case "Email":
                    ((EditText) editor).Hint = "Email";
                    break;
                case "Residence":
                    ((EditText) editor).Hint = "Residence";
                    break;
                case "PhoneNumber":
                    ((EditText) editor).Hint = "Phone Number";
                    break;
            }
        }
    }
}