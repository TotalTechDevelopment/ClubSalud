﻿using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.App;
using Newtonsoft.Json;
using Xamarin.Media;
using Android.Content;
using System.Threading.Tasks;
using System.IO;
using Android.Graphics;
using Android.Provider;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;
using ClubSalud.Droid;
using ClubSalud.Pages;

[assembly: ExportRenderer(typeof(BaseContentPage), typeof(BasePageRenderer))]
namespace ClubSalud.Droid
{
    public class BasePageRenderer : PageRenderer
    {
        BaseContentPage _basePage;
        bool isFromCamera = false;
        Java.IO.File _file;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is BaseContentPage)
            {
                _basePage = e.NewElement as BaseContentPage;
                _basePage.TakePicture = TakePicture;
                _basePage.SelectFromGallery = SelectFromGallery;
            }
        }

        void SelectFromGallery()
        {
            isFromCamera = false;
            var picker = new MediaPicker(this.Context);
            var intent = picker.GetPickPhotoUI();

            var mainActivity = this.Context as MainActivity;
            mainActivity.StartActivity(intent, OnActivityResult);
        }

        void TakePicture()
        {
            var picker = new MediaPicker(this.Context);
            if (!picker.IsCameraAvailable)
                Console.WriteLine("No camera!");
            else
            {
                var intent = picker.GetTakePhotoUI(new StoreCameraMediaOptions
                {
                    Name = string.Format("{0}.jpg", Guid.NewGuid()),
                    Directory = "Pictures"
                });
                //isFromCamera = true;
                //Intent intent = new Intent(MediaStore.ActionImageCapture);
                //var pictureDirectory = new Java.IO.File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "CameraAppDemo");

                //if (!pictureDirectory.Exists())
                //{
                //    pictureDirectory.Mkdirs();
                //}

                //_file = new Java.IO.File(pictureDirectory, String.Format("photo_{0}.jpg", Guid.NewGuid()));

                //intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(_file));
                var mainActivity = this.Context as MainActivity;
                mainActivity.StartActivity(intent, OnActivityResult);
            }
        }

        private async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (resultCode == Result.Canceled)
                return;

            await data.GetMediaFileExtraAsync(this.Context).ContinueWith(t =>
            {
                //Console.WriteLine(t.Result.Path);
                //_page.ImagePath = t.Result.Path;
                MediaFile media = t.Result;
                //ShowPhoto(media);

                MemoryStream mstr = new MemoryStream();
                media.GetStream().CopyTo(mstr);

                if (_basePage != null)
                {
                    _basePage.ImagePath = media.Path;
                    _basePage.bytes = ResizeImageAndroid(mstr.ToArray(), 700, 700, 100);
                    _basePage.Source = ImageSource.FromStream(() => new MemoryStream(_basePage.bytes));
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

            //if (!isFromCamera)
            //{
            //  await data.GetMediaFileExtraAsync(this.Context).ContinueWith(t =>
            //  {
            //      Console.WriteLine(t.Result.Path);
            //      //_page.ImagePath = t.Result.Path;
            //      MediaFile media = t.Result;
            //      //ShowPhoto(media);

            //      MemoryStream mstr = new MemoryStream();
            //      media.GetStream().CopyTo(mstr);

            //      if (_basePage != null)
            //      {
            //          _basePage.ImagePath = media.Path;
            //          _basePage.bytes = ResizeImageAndroid(mstr.ToArray(), 700, 700, 100);
            //          _basePage.Source = ImageSource.FromStream(() => new MemoryStream(_basePage.bytes));
            //      }
            //  }, TaskScheduler.FromCurrentSynchronizationContext());
            //}
            //else
            //{
            //  var image = (Bitmap)data.Extras.Get("data");

            //  //convert bitmap into byte array
            //  byte[] bitmapData;
            //  using (var stream = new MemoryStream())
            //  {
            //      image.Compress(Bitmap.CompressFormat.Png, 0, stream);
            //      //bitmapData = stream.ToArray();

            //      if (_basePage != null)
            //      {
            //          _basePage.ImagePath = _file.Path;
            //          _basePage.bytes = ResizeImageAndroid(stream.ToArray(), 700, 700, 100);
            //          _basePage.Source = ImageSource.FromStream(() => new MemoryStream(_basePage.bytes));
            //      }
            //  }

            //}

        }

        public static byte[] ResizeImageAndroid(byte[] imageData, float width, float height, int quality)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);

            float oldWidth = (float)originalImage.Width;
            float oldHeight = (float)originalImage.Height;
            float scaleFactor = 0f;

            if (oldWidth > oldHeight)
            {
                scaleFactor = width / oldWidth;
            }
            else
            {
                scaleFactor = height / oldHeight;
            }

            float newHeight = oldHeight * scaleFactor;
            float newWidth = oldWidth * scaleFactor;

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)newWidth, (int)newHeight, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, quality, ms);
                return ms.ToArray();
            }
        }
    }
}
