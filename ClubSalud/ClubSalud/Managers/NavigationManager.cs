using System;
using System.Threading.Tasks;
using ClubSalud.Models.Menu;
using ClubSalud.Pages.Master;
using Xamarin.Forms;
using ClubSalud.Pages.Home;
using ClubSalud.Pages.Depedent;
using ClubSalud.Pages.Directory;
using ClubSalud.Pages.Session;

namespace ClubSalud.Managers
{
    public class NavigationManager
    {
        public void NavigateMenu(ItemPageMenu page)
        {
            App.Master.IsPresented = false;
            switch(page)
            {
                case ItemPageMenu.Home:
                    NavigateMaster(new HomePage());
                    break;
                case ItemPageMenu.MyEarrings:
                    NavigateMaster(new DependtsPage());
                    break;
                case ItemPageMenu.Directory:
                    NavigateMaster( new DirectoryPage());
                    break;
                case ItemPageMenu.Exit:
                    Helpers.UserHelper.CurrentUser = null;
                    App.CurrentApp.MainPage = new LogInPage();
                    break;
            }
        }
        public  void NavigatePages(ItemPage page)
        {
            App.Master.IsPresented = false;
            switch(page)
            {
                case ItemPage.ProfileDependent:
                    NavigateMaster(new ProfileDependetPage());
                    break;
            }
        }
		private static async Task Navigate<T>(T page) where T : Page
		{
			await App.Navigator.PushAsync(page);
		}
		private static void NavigateMaster<T>(T page) where T : Page
		{
			App.Master.ChangeRootPage(page);
		}
    }
}
