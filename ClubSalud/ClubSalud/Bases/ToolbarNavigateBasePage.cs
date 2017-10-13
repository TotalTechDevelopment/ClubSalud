using System;
using ClubSalud.Managers;
using Xamarin.Forms;

namespace ClubSalud.Bases
{
    public class ToolbarNavigateBasePage : Grid
    {
        private NavigationManager navigator;
        public ToolbarNavigateBasePage()
        {
			Padding = 5;
            BackgroundColor = Color.FromHex("#0090ab");
            navigator = new NavigationManager();
			AddElementsToolbar();
			RowDefinitions.Add(new RowDefinition { Height = new GridLength(35) });
        }
        void AddElementsToolbar()
        {
			var menu = new Image()
			{
				Source = "menu.png",
				WidthRequest = 50,
				HeightRequest = 50,
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Center
			};
			var tapMenu = new TapGestureRecognizer();
			tapMenu.Tapped += (sender, e) => App.Master.IsPresented = true;
			menu.GestureRecognizers.Add(tapMenu);
			Children.Add(menu, 0, 0);
			var logo = new Image
			{
				Source = "logo_toolbar.png",
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				WidthRequest = 180,
				HeightRequest = 88,
			};
			Children.Add(logo, 1, 0);
			var home = new Image()
			{
				Source = "home.png",
				WidthRequest = 50,
				HeightRequest = 50,
				HorizontalOptions = LayoutOptions.End,
				VerticalOptions = LayoutOptions.Center
			};
			var tapHome = new TapGestureRecognizer();
            tapHome.Tapped += (sender, e) => navigator.NavigateMenu(Models.Menu.ItemPageMenu.Home);
			home.GestureRecognizers.Add(tapHome);
			Children.Add(home, 2, 0);
        }
    }
}

