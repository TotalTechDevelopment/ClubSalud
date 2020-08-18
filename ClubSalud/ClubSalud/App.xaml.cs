using Xamarin.Forms;
using ClubSalud.Managers;
using ClubSalud.Pages.Master;
using ClubSalud.Pages.Session;
using ClubSalud.Models.ClubSalud;
using Totaltech.Core.Data.Services;
using System.Collections.Generic;

namespace ClubSalud
{
    public partial class App : Application
    {
        public static App CurrentApp { get; internal set; }
        public static NavigationManager NavigatorManager { get; internal set; }
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }
        public static User CurrentUser { get; set; }
        public Services Services { get; set; }
        public List<DetalleDeDependientesDeUsuario> ListaDependientes { set; get; }

        public App()
        {
            ListaDependientes = new List<DetalleDeDependientesDeUsuario>();
            InitializeComponent();
            InitServices();
        }

        void InitServices()
        {
            CurrentApp = this;
            Services = new Services();

            NavigatorManager = new NavigationManager();
            //CurrentUser = new User();
            //InitRealmServices();
            var user = Helpers.UserHelper.CurrentUser();
            if (user == null)
            {
                MainPage = new LogInPage();
            }
            else
            {
                MainPage = new NavigationPage(new MasterPage());
            }
        }

        public void ChangeToRootPage()
        {
            MainPage = new NavigationPage(new MasterPage());
        }
    }
}
