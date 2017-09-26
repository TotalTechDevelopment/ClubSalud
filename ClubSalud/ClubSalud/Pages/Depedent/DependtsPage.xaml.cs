using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ClubSalud.Models.Dependt;
using Xamarin.Forms;

namespace ClubSalud.Pages.Depedent
{
	public partial class DependtsPage : ContentPage
	{
		ObservableCollection<Dependent> Dependents;
		public DependtsPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PopulatingDependts();
		}
		void PopulatingDependts()
		{
			Dependents = new ObservableCollection<Dependent>()
			{
				new Dependent(){Image="cam_image.png", Name="Nombre"},
				new Dependent(){Image="cam_image.png", Name="Nombre"},
				new Dependent(){Image="cam_image.png", Name="Nombre"},
				new Dependent(){Image="cam_image.png", Name="Nombre"},
				new Dependent(){Image="cam_image.png", Name="Nombre"},
				new Dependent(){Image="cam_image.png", Name="Nombre"},
				new Dependent(){Image="cam_image.png", Name="Nombre"}
			};
			_ListDependts.ItemsSource = Dependents;
		}
	}
}