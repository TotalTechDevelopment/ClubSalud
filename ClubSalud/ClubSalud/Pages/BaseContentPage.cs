using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ClubSalud.Providers;
using Plugin.Media;
using Totaltech.Core.Data.Models;
using Totaltech.Core.Data.Services;
using Xamarin.Forms;

namespace ClubSalud
{
    public class BaseContentPage : ContentPage
    {
        public string imagePath = null;
        public Action TakePicture, SelectFromGallery;

        public bool imageChanged = false;

        public string ImagePath
        {
            get
            {
                return imagePath;
            }

            set
            {
                imagePath = value;
                imageChanged = true;
                //if (_imageView != null)
                //  Device.BeginInvokeOnMainThread(() =>
                //  {
                //      _imageView.Source = imagePath;
                //  });
            }
        }

        public object LastView;
        public Action ImageSourceChanged;

        ImageSource _source;
        public ImageSource Source
        {
            set
            {
                imageChanged = true;
                _source = value;

                if (ImageSourceChanged != null)
                    ImageSourceChanged();
            }
            get
            {
                return _source;
            }
        }

        byte[] _bytes;

        public byte[] bytes
        {
            get
            {
                return _bytes;
            }

            set
            {
                _bytes = value;
            }
        }



        public async void TakePictureActionSheet(object imageView = null)
        {
            LastView = imageView;
            var n = await DisplayActionSheet("Elige una imagen", "cancelar", null, new string[] { "Cámara", "Galería" });
            switch (n)
            {
                case "Cámara":
                    if (TakePicture != null)
                    {
                        TakePicture();
                    }
                    break;
                case "Galería":
                    if (SelectFromGallery != null)
                    {
                        SelectFromGallery();
                    }
                    break;
            }
        }

        public async Task<byte[]> TakePhotoMedia()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Alerta", "Camara no disponible.", "OK");
                return null;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "ClubSaludImage",
                Name = "clubsalud_avatar.jpg",
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front
            });
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();
                    return memoryStream.ToArray();
                }
            }
            return null;
        }

        public async Task<byte[]> GetPhotoGaleryMedia()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported )
            {
                await DisplayAlert("Alerta", "Galeria de fotos no disponible.", "OK");
                return null;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();
                    return memoryStream.ToArray();
                }
            }
            return null;
        }


        public async Task<int> PostPhoto(byte[] bytes)
        {
            DependencyService.Get<IProgress>().ShowProgress("Guardando fotografía");
            var image = new SpartanFile
            {
                Description = "avatar_clubsalud.png",
                File = bytes,
                File_Size = bytes.Length
            };
            var folio = await App.CurrentApp.Services.PostObjectToTable<SpartanFile>(image, SpartanFile.TABLE_NAME);

            DependencyService.Get<IProgress>().Dismiss();
            return folio;
        }

        public Action<int> ImagesUploaded;
        // uri - folio case
        public Dictionary<object, int> _fotosDictionary = new Dictionary<object, int>();
        public Dictionary<int, TTArchivo> _ttarchivosDict = new Dictionary<int, TTArchivo>();
        public async Task PostLastFoto()
        {
            try
            {

                DependencyService.Get<IProgress>().ShowProgress("Guardando fotografía");

                bool imageUploaded = false;

                var imageView = LastView;

                if (imageChanged)
                {
                    var inputFile = new InputFile();
                    inputFile.FileArray = bytes;
                    inputFile.FileName = DependencyService.Get<IImageProcessor>().ImageFileName(ImagePath);

                    var image = new SpartanFile
                    {
                        Description = inputFile.FileName,
                        File = bytes,
                        File_Size = bytes.Length
                    };
                    var folio = await App.CurrentApp.Services.PostObjectToTable<SpartanFile>(image, SpartanFile.TABLE_NAME);

                    DependencyService.Get<IProgress>().Dismiss();

                    if (folio != -1)
                    {
                        imageUploaded = true;

                        if (_fotosDictionary.ContainsKey(imageView))
                        {
                            _fotosDictionary.Remove(imageView);
                        }

                        _fotosDictionary.Add(imageView, folio);

                        ImagesUploaded(folio);
					}

                    //var result = await App.CurrentApp.Services.PostFotos(new List<InputFile>()
                    //        {
                    //            inputFile
                    //        });
                    //TTArchivo n = null;
                    //if (result != null && result.Count == 1)
                    //{
                    //    imageUploaded = true;

                    //    n = result[0];
                    //    //foreach (var n in result)
                    //    //{
                    //    if (n.ArchivoURL != null)
                    //    {
                    //        //var str = n.ArchivoURL;
                    //        //str = str.Replace(Configuration.BaseReplaceUrl, Configuration.BaseURL + "Uploads/TTArchivos/");
                    //        //n.ArchivoURL = str;
                    //    }


                    //    if (!_ttarchivosDict.ContainsKey(n.Folio))
                    //        _ttarchivosDict.Add(n.Folio, n);

                    //    System.Diagnostics.Debug.WriteLine("{0}", n.Folio);

                    //    if (_fotosDictionary.ContainsKey(imageView))
                    //    {
                    //        _fotosDictionary.Remove(imageView);
                    //    }

                    //    _fotosDictionary.Add(imageView, n.Folio);

                    //    //}
                    //}

                    //if (ImagesUploaded != null && n != null)
                    //ImagesUploaded(n.Folio);

                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IProgress>().Dismiss();
                await DisplayAlert("", "Ocurrió un error", "OK");
            }

            DependencyService.Get<IProgress>().Dismiss();

        }
    }
}

