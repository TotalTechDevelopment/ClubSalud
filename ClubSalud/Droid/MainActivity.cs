﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;

namespace ClubSalud.Droid
{
    [Activity(Label = "ClubSalud.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

			if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
			{
				int mycode = 0;

                if (CheckSelfPermission(Android.Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted || (CheckSelfPermission(Android.Manifest.Permission.Camera) != (int)Permission.Granted))
				{
                    ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.Camera, Android.Manifest.Permission.WriteExternalStorage, Android.Manifest.Permission.ReadExternalStorage }, mycode);
				}
			}

            LoadApplication(new App());
        }

		private Action<int, Result, Intent> _resultCallback;

		public void StartActivity(Intent intent, Action<int, Result, Intent> resultCallback)
		{
			_resultCallback = resultCallback;

			StartActivityForResult(intent, 0);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			if (_resultCallback != null)
			{
				_resultCallback(requestCode, resultCode, data);
				_resultCallback = null;
			}
		}
    }
}