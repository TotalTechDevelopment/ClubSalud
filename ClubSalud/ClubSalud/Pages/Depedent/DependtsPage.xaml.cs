using ClubSalud.Managers;
using ClubSalud.Models.Menu;
using ClubSalud.Pages.Home;
using ClubSalud.Providers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ClubSalud.Pages.Depedent
{
	public partial class DependtsPage : ContentPage
	{
        private NavigationManager navigation;

        private List<DetalleDeDependientesDeUsuario> ListaDependientes { set; get; }

        public DependtsPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

            navigation = new NavigationManager();

            _ListDependts.ItemSelected += (sender, e) =>
            {

                var dependent = ((ListView)sender).SelectedItem as DetalleDeDependientesDeUsuario;

                Helpers.DependentHelper.CurrentDependent = dependent;
                Helpers.DependentHelper.CurrentDependentPosition = ListaDependientes.IndexOf(dependent);
                Navigation.PushAsync(new ProfileDependetPage());
                //navigation.NavigatePages(ItemPage.ProfileDependent);
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Helpers.DependentHelper.ListaDependientes == null)
            {
               GetDependents();
            }
            else
            {
                ListaDependientes = Helpers.DependentHelper.ListaDependientes;
                PopulatingDependents();
            }
           
        }

        void PopulatingDependents()
		{
            _ListDependts.ItemsSource = ListaDependientes;
		}

        async void GetDependents()
        {
            try
            {
                DependencyService.Get<IProgress>().ShowProgress("Actualizando información");

                string where = "Detalle_de_Dependientes_de_Usuario.Usuario='" + Helpers.UserHelper.CurrentUser().Folio + "'";
                var resp = await App.CurrentApp.Services.ListaSelAll<DependientePagingModel>(DetalleDeDependientesDeUsuario.TABLE_NAME, 0, 1000, where);


                if (resp != null && resp.Detalle_de_Dependientes_de_Usuarios.Count > 0)
                {
                    ListaDependientes = resp.Detalle_de_Dependientes_de_Usuarios;
                    Helpers.DependentHelper.ListaDependientes = ListaDependientes;

                    PopulatingDependents();
                }

                DependencyService.Get<IProgress>().Dismiss();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                DependencyService.Get<IProgress>().Dismiss();
            }
        }
	}
}