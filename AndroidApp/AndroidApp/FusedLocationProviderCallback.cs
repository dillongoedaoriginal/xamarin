using System;
using System.Linq;
using Android.Gms.Location;
using Android.Support.V7.App;
using Android.Util;

namespace AndroidApp
{
    public class FusedLocationProviderCallback : LocationCallback
    {
        readonly Action<Android.Locations.Location> callback;

        public FusedLocationProviderCallback(Action<Android.Locations.Location> callback)
        {
            this.callback = callback;
        }

        public override void OnLocationAvailability(LocationAvailability locationAvailability)
        {
            Log.Debug("FusedLocationProviderSample", "IsLocationAvailable: {0}", locationAvailability.IsLocationAvailable);
        }

        public override void OnLocationResult(LocationResult result)
        {
            if (result.Locations.Any())
            {
                var location = result.Locations.First();
                callback(location);
            }

        }
    }
}