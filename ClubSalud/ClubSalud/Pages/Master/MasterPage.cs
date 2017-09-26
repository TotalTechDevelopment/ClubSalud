using System;
using ClubSalud.Pages.Home;
using Xamarin.Forms;

namespace ClubSalud.Pages.Master
{
	public partial class MasterPage : MasterDetailPage
	{
		DrawerPage drawerPage;
		public MasterPage()
		{
			drawerPage = new DrawerPage();
			Master = drawerPage;
			Detail = new NavigationPage(new HomePage ());
		}
		public void ChangeRootPage(Page page)
		{
			var navigationPage = new NavigationPage(page);
			Detail = navigationPage;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			App.Master = this;
		}
	}
}

