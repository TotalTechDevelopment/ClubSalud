using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ClubSalud.Providers
{
    public interface IImageProcessor
    {
        string ImageFileName(string path);
        Task<string> SavePictureToDisk(ImageSource source, string imageName, float maxHeight = 500, float maxWidth = 500);
        byte[] ImageToByteArray(string path);
    }
}
