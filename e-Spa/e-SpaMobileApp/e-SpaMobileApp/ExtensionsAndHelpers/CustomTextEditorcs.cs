using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Syncfusion.Android.DataForm;
using Syncfusion.Android.DataForm.Editors;

namespace e_SpaMobileApp.ExtensionsAndHelpers
{
    public class CustomTextEditor : DataFormTextEditor
    {
        public CustomTextEditor(SfDataForm dataForm) : base(dataForm)
        {
        }

        protected override void OnInitializeView(DataFormItem dataFormItem, EditText view)
        {
            view.SetTextColor(Color.White);
            view.SetHintTextColor(Color.White);
            view.SetBackgroundResource(Resource.Drawable.syncfusion_editText_style);
            base.OnInitializeView(dataFormItem, view);
        }
    }
}