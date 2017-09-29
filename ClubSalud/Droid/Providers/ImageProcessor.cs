using System;
using System.IO;
using System.Threading.Tasks;
using ClubSalud.Droid.Providers;
using ClubSalud.Providers;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageProcessor))]
namespace ClubSalud.Droid.Providers
{
    public class ImageProcessor : IImageProcessor
    {


        public string ImageFileName(string path)
        {
            var file = Path.GetFileName(path);


            return file;
        }

        public byte[] ImageToByteArray(string path)
        {
            try
            {
                return File.ReadAllBytes(path);
            }
            catch
            {
                return null;
            }
        }

        public Task<string> SavePictureToDisk(ImageSource source, string imageName, float maxHeight = 500, float maxWidth = 500)
        {
            return null;
        }
    }
}
