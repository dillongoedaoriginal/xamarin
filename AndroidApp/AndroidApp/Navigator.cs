using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;

namespace AndroidApp
{
    public static class Navigator
    {
        public static Intent GetOpenMapIntent(float lat, float lon)
        {
            var geoUri = Android.Net.Uri.Parse($"geo:{lat},{lon}");
            return new Intent(Intent.ActionView, geoUri);
        }


        public static async Task Navigate(float lat, float lon)
        {
            var location = new Location(lat, lon);
            var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };
            await Map.OpenAsync(location, options);
        }
    }
}