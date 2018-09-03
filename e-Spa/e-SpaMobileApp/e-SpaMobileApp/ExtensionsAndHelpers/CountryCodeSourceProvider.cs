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
        private CountryPicker.Builder builder;

        public  CountryCodeSourceProvider(Context context)
        {
            this.context = context;
             builder = new CountryPicker
                    .Builder()
                .With(context)
                .Listener(this);
        }
        public override IList GetSource(string sourceName)
        {
            CountryPicker picker = new CountryPicker(builder);
            List<string> list = new List<string>();
            List<Country> countries = picker.GetAllCountries();
            foreach (var c in countries)
            {
                list.Add($"{c.Name}  {c.DialCode}");
            }
            return list;
        }

        public string GetCountryCode(string countryName)
        {
            CountryPicker picker=new CountryPicker(builder);
            var countryList = picker.GetAllCountries().ToList();
            var countryCode = countryList.Find(c => c.Name == countryName).DialCode;
            return countryCode;
        }

        public void OnSelectCountry(Country country)
        {
        }
    }
}