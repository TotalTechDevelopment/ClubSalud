using System.IO;
using System.Threading.Tasks;
using ClubSalud.iOS.Providers;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageProcessor))]
namespace ClubSalud.iOS.Providers
{
	public class ImageProcessor : ClubSalud.Providers.IImageProcessor
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
