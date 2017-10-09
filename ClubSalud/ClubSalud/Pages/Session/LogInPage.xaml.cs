using System;
using System.Collections.Generic;
using System.Linq;
using ClubSalud.Managers;
using ClubSalud.Models;
using ClubSalud.Models.ClubSalud;
using ClubSalud.Pages.Master;
using ClubSalud.Providers;
using Xamarin.Forms;

namespace ClubSalud.Pages.Session
{
    public partial class LogInPage : ContentPage
    {

        private NavigationManager navigation;

        public LogInPage()
        {
            InitializeComponent();
            navigation = new NavigationManager();
#if DEBUG
            _EntryUsername.Text = "HEB1641@mail.com";
            _EntryPassword.Text = "1641";
#endif
            InitUI();
		}
		void InitUI()
		{
			TapGestureRecognizer tapLogIn = new TapGestureRecognizer();
			tapLogIn.Tapped += TapLogIn;
			_BtnLogIn.GestureRecognizers.Add(tapLogIn);
		}

		async void TapLogIn(object sender, EventArgs e)
		{
			DependencyService.Get<IProgress>().ShowProgress("Validando credenciales");
			if (string.IsNullOrEmpty(_EntryPassword.Text) || string.IsNullOrEmpty(_EntryUsername.Text))
			{
				_LabelErrorLogIn.Text = "No se ingreso usuario o contraseña";
				_LabelErrorLogIn.IsVisible = true;
			}
			else
			{
				try
				{
					string where = "Clave_de_Acceso='" + _EntryUsername.Text + "' ";
                    var res = await App.CurrentApp.Services.ListaSelAll<UserPagingModel>(User.TABLE_NAME, 0, 1, where);
					_LabelErrorLogIn.IsVisible = false;
					if (res != null && res.Registro_de_Usuarios.Count > 0)
					{
						if (res.Registro_de_Usuarios[0].Contrasena.Equals(_EntryPassword.Text))
						{
							var user = res.Registro_de_Usuarios.FirstOrDefault();
                            Helpers.UserHelper.SaveUserInfo(user);
							App.CurrentApp.MainPage = new MasterPage();
						}
						else
						{
							_LabelErrorLogIn.Text = "Tus credenciales son incorrectas";
							_LabelErrorLogIn.IsVisible = true;
						}

					}
					else
					{
						_LabelErrorLogIn.Text = "Tus credenciales son incorrecta";
						_LabelErrorLogIn.IsVisible = true;
					}
				}
				catch (Exception ex)
				{
					_LabelErrorLogIn.IsVisible = true;
					_LabelErrorLogIn.Text = "Existio un problema al validar las credenciales";
				}
				DependencyService.Get<IProgress>().Dismiss();
			}

		}

	}
}
