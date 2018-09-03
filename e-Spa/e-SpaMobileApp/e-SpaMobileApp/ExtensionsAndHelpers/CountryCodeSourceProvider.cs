using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Mukesh.CountryPickerLib;
using Syncfusion.Android.DataForm;

namespace e_SpaMobileApp.ExtensionsAndHelpers
{
    public class CountryCodeSourceProvider : SourceProvider, IOnCountryPickerListener
    {
        private Context context;

        public  CountryCodeSourceProvider(Context context)
        {
            this.context = context;
        }
        public override IList GetSource(string sourceName)
        {
            CountryPicker.Builder builder = new CountryPicker
                    .Builder()
                .With(context)
                .Listener(this);

            CountryPicker picker = new CountryPicker(builder);
            List<string> list = picker.GetAllCountries().Select(c => c.Name).ToList();
            return list;
        }

        public void OnSelectCountry(Country country)
        {
            throw new NotImplementedException();
        }
    }
}