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
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace AndroidApp
{
    public static class Camera
    {
        public static async Task<MediaFile> CaptureImage()
        {
            await CrossMedia.Current.Initialize();

            return await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                CompressionQuality = 50,
                Name = "img.jpg",
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                Directory = "laser"
            });
        }
    }
}