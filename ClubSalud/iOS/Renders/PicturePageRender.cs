using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Newtonsoft.Json;
using Xamarin.Media;
using System.Threading.Tasks;
using System.IO;
using UIKit;
using CoreGraphics;
using System.Drawing;
using ClubSalud.iOS.Renders;
using ClubSalud;

[assembly: ExportRenderer(typeof(BaseContentPage), typeof(PicturePageRender))]
namespace ClubSalud.iOS.Renders
{
    public class PicturePageRender : PageRenderer
    {
        BaseContentPage _basePage;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            if (e.NewElement is BaseContentPage)
            {
                _basePage = e.NewElement as BaseContentPage;

                _basePage.SelectFromGallery = SelectFromGallery;
                _basePage.TakePicture = TakePicture;
            }
        }

        async void TakePicture()
        {
            var picker = new MediaPicker();
            if (picker.IsCameraAvailable)
            {
                MediaPickerController controller = picker.GetTakePhotoUI(new StoreCameraMediaOptions
                {
                    Name = string.Format("{0}.jpg", Guid.NewGuid()),
                    Directory = "Pictures",
                });


                // On iPad, you'll use UIPopoverController to present the controller
                PresentViewController(controller, true, null);

                await controller.GetResultAsync().ContinueWith(t =>
                {
                    // We need to dismiss the controller ourselves
                    DismissViewController(true, () =>
                    {
                        // User canceled or something went wrong
                        if (t.IsCanceled || t.IsFaulted)
                            return;

                        // We get back a MediaFile
                        MediaFile media = t.Result;
                        //ShowPhoto(media);
                        MemoryStream mstr = new MemoryStream();
                        media.GetStream().CopyTo(mstr);

                        if (_basePage != null)
                        {
                            _basePage.ImagePath = media.Path;
                            _basePage.bytes = ResizeTheImage(mstr.ToArray(), 700, 700);
                            _basePage.Source = ImageSource.FromStream(() => new MemoryStream(_basePage.bytes));
                        }
                    });
                    // Make sure we use the UI thread to show our photo.
                }, TaskScheduler.FromCurrentSynchronizationContext());

            }
        }

        async void SelectFromGallery()
        {
            var picker = new MediaPicker();

            MediaPickerController controller = picker.GetPickPhotoUI();

            // On iPad, you'll use UIPopoverController to present the controller
            PresentViewController(controller, true, null);

            //var result = await controller.GetResultAsync();
            //controller.DismissViewController(true, null);
            //MediaFile file = result;
            //_page.ImagePath = file.Path;

            await controller.GetResultAsync().ContinueWith(t =>
            {
                // We need to dismiss the controller ourselves
                controller.DismissViewController(true, () =>
                {
                    // User canceled or something went wrong
                    if (t.IsCanceled || t.IsFaulted)
                        return;
                    MediaFile media = t.Result;
                    MemoryStream mstr = new MemoryStream();
                    media.GetStream().CopyTo(mstr);

                    if (_basePage != null)
                    {
                        _basePage.ImagePath = media.Path;
                        _basePage.bytes = ResizeTheImage(mstr.ToArray(), 700, 700);
                        _basePage.Source = ImageSource.FromStream(() => new MemoryStream(_basePage.bytes));
                    }

                });
                // Make sure we use the UI thread to show our photo.
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }



        public byte[] ResizeTheImage(byte[] imageData, float width, float height)
        {
            UIImage originalImage = ImageFromByteArray(imageData);
            UIImageOrientation orientation = originalImage.Orientation;

            //create a 24bit RGB image
            using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
                                                 (int)width, (int)height, 8,
                                                 (int)(4 * width), CGColorSpace.CreateDeviceRGB(),
                                                 CGImageAlphaInfo.PremultipliedFirst))
            {

                RectangleF imageRect = new RectangleF(0, 0, width, height);

                // draw the image
                context.DrawImage(imageRect, originalImage.CGImage);

                UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage(), 0, orientation);

                // save the image as a jpeg
                return resizedImage.AsJPEG().ToArray();
            }
        }

        public static UIKit.UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            UIKit.UIImage image;
            try
            {
                image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Image load failed: " + e.Message);
                return null;
            }
            return image;
        }
    }
}
