using System;
using System.Collections.ObjectModel;
using ClubSalud.Managers;
using ClubSalud.Models.Home;
using Xamarin.Forms;
using ClubSalud.Models.Menu;

namespace ClubSalud.Pages.Home
{
	public partial class HomePage : ContentPage
	{

		private ObservableCollection<Dependent> Dependents;
		private NavigationManager navigation;

		public HomePage()
		{
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);
			navigation = new NavigationManager();
			PopulatingProfile();
			PopulatingDependent();
		}
		void PopulatingProfile()
		{
            _Name.Text = App.CurrentUser.Nombre_del_Titular;
			_Member.Text = App.CurrentUser.Numero_de_Seguro;
			_Vigencia.Text = App.CurrentUser.Vigencia.ToString();
		}
		void PopulatingDependent()
		{
			Dependents = new ObservableCollection<Dependent>();
			StackLayout[] layouts = new StackLayout[5];
			TapGestureRecognizer tapChangePicture = new TapGestureRecognizer();
			tapChangePicture.Tapped += TapedChangePicture;
			for (int i = 0; i < 5; i++)
			{
				layouts[i] = new StackLayout();
				layouts[i].HorizontalOptions = LayoutOptions.CenterAndExpand;
				layouts[i].Spacing = 1;
				layouts[i].Children.Add(new Image() { Source = "cam_image.png", WidthRequest = 50, HorizontalOptions = LayoutOptions.CenterAndExpand });
				layouts[i].Children.Add(new Label() { Text = "Nombre", FontSize = 8, HorizontalOptions = LayoutOptions.CenterAndExpand });
				layouts[i].GestureRecognizers.Add(tapChangePicture);
				_ListDepents.Children.Add(layouts[i]);
			}
		}
		void TapedChangePicture(object sender, EventArgs e)
		{
			navigation.NavigatePages(ItemPage.ProfileDependent);
		}
	}
}