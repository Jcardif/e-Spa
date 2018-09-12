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
            if (label is TextureView)
            {
                (label as TextView).Typeface=Typeface.Monospace;
                GradientDrawable drawable = new GradientDrawable();
                drawable.SetStroke(5,Color.ForestGreen);
                (label as TextView).Background = drawable;
                (label as TextView).SetTextColor(Color.DarkRed);
            }

            return label;
        }

        protected override void OnEditorCreated(DataFormItem dataFormItem, View editor)
        {
            if(editor is EditText)
                (editor as EditText).SetTextColor(Color.White);

            (editor as EditText).Typeface = Typeface.Monospace;
            GradientDrawable drawable = new GradientDrawable();
            drawable.SetStroke(5, Color.ForestGreen);
            (editor as EditText).Background = drawable;
            (editor as EditText).SetTextColor(Color.DarkRed);
        }
    }
}