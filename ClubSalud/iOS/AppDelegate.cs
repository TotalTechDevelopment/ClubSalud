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
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
			var dummy = new FFImageLoading.Forms.Platform.CachedImageRenderer();
			var ignore = new CircleTransformation();

            return base.FinishedLaunching(app, options);
        }
    }
}
