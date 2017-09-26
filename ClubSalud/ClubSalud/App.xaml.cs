using Xamarin.Forms;
using Realms;
using System;
using Realms.Exceptions;
using ClubSalud.Managers;
using ClubSalud.Pages.Master;
using ClubSalud.Pages.Session;
using ClubSalud.Models.ClubSalud;
using Totaltech.Core.Data;

namespace ClubSalud
{
    public partial class App : Application
    {
        public static App CurrentApp { get; internal set; }
        public static NavigationManager NavigatorManager { get; internal set; }
        public static Realm InstanceRealm { get; set; }
		public static NavigationPage Navigator { get; internal set; }
		public static MasterPage Master { get; internal set; }
        public static User CurrentUser { get; set; }
        public Services Services { get; set; }

        public App()
        {
            InitializeComponent();
            InitServices();
        }

        void InitServices()
        {
            CurrentApp = this;
            NavigatorManager = new NavigationManager();
			CurrentUser = new User();
			InitRealmServices();
            MainPage = new LogInPage();
        }

        void InitRealmServices()
        {
            RealmConfiguration configuration = RealmConfiguration.DefaultConfiguration;
            try
            {
                InstanceRealm = Realm.GetInstance();
            }
            catch(RealmMigrationNeededException e){
				try
				{
                    Realm.DeleteRealm(configuration);
                    InstanceRealm = Realm.GetInstance();
				}
				catch (Exception ex)
				{
                    System.Diagnostics.Debug.WriteLine(ex.Message);
				}
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
    }
}
