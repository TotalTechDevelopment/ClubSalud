using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ClubSalud.Pages.Home
{
	public partial class ProfileDependetPage : ContentPage
	{
		public ProfileDependetPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

            InitInfo();
		}

        void InitInfo()
        {
            var dependent = Helpers.DependentHelper.CurrentDependent;

            _DependentName.Text = dependent.Nombre;
			_Member.Text = App.CurrentUser.Numero_de_Seguro;
			_Vigencia.Text = App.CurrentUser.VigenciaFormatted;
        }
	}
}
