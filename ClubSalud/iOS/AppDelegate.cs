using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Transformations;
using Foundation;
using UIKit;

namespace ClubSalud.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
			var dummy = new FFImageLoading.Forms.Touch.CachedImageRenderer();
			var ignore = new CircleTransformation();

            return base.FinishedLaunching(app, options);
        }
    }
}
