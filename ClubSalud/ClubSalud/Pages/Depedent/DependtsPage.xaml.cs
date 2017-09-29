using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ClubSalud.Pages.Depedent
{
	public partial class DependtsPage : ContentPage
	{
		public DependtsPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PopulatingDependts();
		}
		void PopulatingDependts()
		{
            _ListDependts.ItemsSource = App.CurrentApp.ListaDependientes;
		}
	}
}