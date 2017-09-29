using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ClubSalud.Managers;
using ClubSalud.Models.Menu;
using Xamarin.Forms;

namespace ClubSalud.Pages.Master
{
	public partial class DrawerPage : ContentPage
	{
		private ObservableCollection<ItemMenu> menuItems;
		private NavigationManager navigation;

		public DrawerPage()
		{
			InitializeComponent();
			navigation = new NavigationManager();
			LoadMenu();
		}
		void LoadMenu()
		{
			_LabelNombre.Text = Helpers.UserHelper.CurrentUser.Nombre_del_Titular;
			menuItems = new ObservableCollection<ItemMenu>();
			string[] titles = new string[] { "Inicio", "Directorio", "Mis dependientes", "Salir" };
			string[] icons = new string[] { "home.png", "book.png", "users.png", "logout.png" };
			int i = 0;
			foreach (ItemPageMenu page in Enum.GetValues(typeof(ItemPageMenu)))
			{
				menuItems.Add(new ItemMenu()
				{
					Title = titles[i],
					Page = page,
					Icon = icons[i]
				});
				i++;
			}
			ListMenu.ItemsSource = menuItems;
			ListMenu.ItemSelected += (sender, e) => {

				if (e.SelectedItem == null) return;

				var item = e.SelectedItem as ItemMenu;

				navigation.NavigateMenu(item.Page);
				foreach (var n in menuItems) n.Selected = false;
				item.Selected = true;
				ListMenu.ItemsSource = new ObservableCollection<ItemMenu>(menuItems);
				ListMenu.SelectedItem = null;
			};
		}
	}
}
