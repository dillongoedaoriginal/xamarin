using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Plugin.CurrentActivity;
using System;
using Android.Graphics;
using Android.Gms.Location;
using Android;
using Android.Support.V4.Content;
using System.Threading.Tasks;

namespace AndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            FindViewById<Button>(Resource.Id.btnCapture).Click += HandleImageCapture;
            FindViewById<Button>(Resource.Id.btnGps).Click += HandleGpsClick;


            FindViewById<Button>(Resource.Id.btnNav).Click += async (o, a) => await Navigator.Navigate(-33.9893627f, 18.4686743f);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private async void HandleImageCapture(object sender, EventArgs e)
        {
            var file = await Camera.CaptureImage();

            if (file != null)
            {
                using (var stream = file.GetStream())
                {
                    FindViewById<ImageView>(Resource.Id.img).SetImageBitmap(BitmapFactory.DecodeStream(stream));
                }
            }
        }

        private async void HandleGpsClick(object sender, EventArgs e)
        {
            var fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient(this);
            await GetLocationUpdates(fusedLocationProviderClient, new FusedLocationProviderCallback(location =>
            {
                FindViewById<TextView>(Resource.Id.txtLat).Text = $"Lat: {location.Latitude}";
                FindViewById<TextView>(Resource.Id.txtLon).Text = $"Lon: {location.Longitude}";

            }));
        }

        private async Task GetLocationUpdates(FusedLocationProviderClient fusedLocationProviderClient, LocationCallback locationCallback)
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == Android.Content.PM.Permission.Granted)
            {
                var locationRequest = new LocationRequest()
                                  .SetPriority(LocationRequest.PriorityHighAccuracy)
                                  .SetInterval(60 * 1000 * 5)
                                  .SetFastestInterval(60 * 500);

                await fusedLocationProviderClient.RequestLocationUpdatesAsync(locationRequest, locationCallback);

                FindViewById<TextView>(Resource.Id.txtStatus).Text = $"Status: Tracking";
            }
            else
            {
                FindViewById<TextView>(Resource.Id.txtStatus).Text = $"Status: Permission not granted";
            }

        }
    }
}