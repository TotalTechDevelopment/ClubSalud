using FFImageLoading.Transformations;
using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using UIKit;

namespace ClubSalud.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Rg.Plugins.Popup.Popup.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            AppCenter.Start("c19f239d-176c-4d7e-8ad6-d7dfa43c0a87",
                               typeof(Analytics), typeof(Crashes));
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }
    }
}
