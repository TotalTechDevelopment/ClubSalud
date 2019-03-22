using ClubSalud.Providers;
using ClubSalud.Utils;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ClubSalud.Pages.Home
{
	public partial class ProfileDependetPage : BaseContentPage
	{
		public ProfileDependetPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

            ImageSourceChanged = async () =>
            {
                if (LastView is FFImageLoading.Forms.CachedImage)
                    (LastView as FFImageLoading.Forms.CachedImage).Source = Source;

                _profileImage.Source = Source;

                await PostLastFoto();
            };

            ImagesUploaded += (folio) =>
            {
                UpdateUserPhoto(folio);
            };

            InitInfo();
		}

        async void InitInfo()
        {
            var dependent = Helpers.DependentHelper.CurrentDependent;

            _DependentName.Text = dependent.Nombre;
            var lastNameP = AppViewUtils.RemoveWhiteSpaces(dependent.Apellido_Paterno);
            var lastNameM = AppViewUtils.RemoveWhiteSpaces(dependent.Apellido_Materno);
            _LastName.Text = lastNameP + " " + lastNameM;
			_Member.Text = App.CurrentUser.Numero_de_Seguro;
			_Vigencia.Text = App.CurrentUser.VigenciaFormatted;

            var image = "";
            if (dependent.Foto != null && dependent.Foto != -1 && dependent.Foto != 0)
            {
                image = await App.CurrentApp.Services.GetImage((int)dependent.Foto);
                _profileImage.Source = image;
            }
            else
            {
                _profileImage.Source = "no_image.png";
            }

            UpdateButtonViews();
        }

        async void UpdateUserPhoto(int folio)
        {
            try
            {
                DependencyService.Get<IProgress>().ShowProgress("Actualizando fotografía");

                var dependent = Helpers.DependentHelper.CurrentDependent;
                dependent.Foto = folio;

                var resp = await App.CurrentApp.Services.PutObjectToTable<DetalleDeDependientesDeUsuario>(dependent, dependent.Clave + "", DetalleDeDependientesDeUsuario.TABLE_NAME);

                DependencyService.Get<IProgress>().Dismiss();

                if (resp != -1)
                {
                    await DisplayAlert("", "Fotografía actualizada correctamente", "Ok");
                }
                else
                {
                    await DisplayAlert("", "Hubo un problema al actualizar la fotografía", "Ok");
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IProgress>().Dismiss();

                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        void NextClicked(object sender, EventArgs e)
        {
            if (Helpers.DependentHelper.CurrentDependentPosition != Helpers.DependentHelper.ListaDependientes.Count - 1)
            {
                Helpers.DependentHelper.CurrentDependentPosition += 1;
            }

            UpdateUserInfo();
        }

        void BackClicked(object sender, EventArgs e)
        {
            if (Helpers.DependentHelper.CurrentDependentPosition != 0)
            {
                Helpers.DependentHelper.CurrentDependentPosition -= 1;
            }

            UpdateUserInfo();
        }

        void UpdateUserInfo()
        {
            Helpers.DependentHelper.CurrentDependent = Helpers.DependentHelper.ListaDependientes[Helpers.DependentHelper.CurrentDependentPosition];

            UpdateButtonViews();
            InitInfo();
        }

        async void ChangePicture(object sender, EventArgs e)
        {
            var n = await DisplayActionSheet("Elige una imagen", "cancelar", null, new string[] { "Cámara", "Galería" });
            switch (n)
            {
                case "Cámara":
                    var photo = await TakePhotoMedia();
                    if (photo != null)
                    {
                        var post = await PostPhoto(photo);
                        if (post != -1)
                        {
                            UpdateUserPhoto(post);
                        }
                        else
                        {
                            await DisplayAlert("", "Hubo un problema al actualizar la fotografía", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("", "Hubo un problema al actualizar la fotografía", "Ok");
                    }
                    break;
                case "Galería":
                    var photoGalery = await GetPhotoGaleryMedia();
                    if (photoGalery != null)
                    {
                        var post = await PostPhoto(photoGalery);
                        if (post != -1)
                        {
                            UpdateUserPhoto(post);
                        }
                        else
                        {
                            await DisplayAlert("", "Hubo un problema al actualizar la fotografía", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("", "Hubo un problema al actualizar la fotografía", "Ok");
                    }
                    break;
            }

        }

        void UpdateButtonViews()
        {
            _BackButton.IsVisible = true;
            _NextButton.IsVisible = true;

            if (Helpers.DependentHelper.CurrentDependentPosition == 0)
            {
                _BackButton.IsVisible = false;
            }
            else if (Helpers.DependentHelper.CurrentDependentPosition == Helpers.DependentHelper.ListaDependientes.Count - 1)
            {
                _NextButton.IsVisible = false;
            }
            
        }

    }
}
