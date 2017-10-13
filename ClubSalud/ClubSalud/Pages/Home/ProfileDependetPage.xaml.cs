using ClubSalud.Providers;
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
            _LastName.Text = dependent.Apellido_Paterno + " " + dependent.Apellido_Materno;
			_Member.Text = App.CurrentUser.Numero_de_Seguro;
			_Vigencia.Text = App.CurrentUser.VigenciaFormatted;

            var image = "";
            if (dependent.Foto != -1 && dependent.Foto != 0)
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
            if (Helpers.DependentHelper.CurrentDependent.Foto == -1 || Helpers.DependentHelper.CurrentDependent.Foto == 0)
            {
                TakePictureActionSheet(_profileImage);
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
