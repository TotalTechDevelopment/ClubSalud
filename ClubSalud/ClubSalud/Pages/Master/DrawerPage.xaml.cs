using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ClubSalud.Managers;
using ClubSalud.Models.Menu;
using Xamarin.Forms;
using ClubSalud.Providers;
using ClubSalud.Models.ClubSalud;
using ClubSalud.Pages.Directory;
using ClubSalud.Pages.Depedent;
using ClubSalud.Pages.Session;

namespace ClubSalud.Pages.Master
{
	public partial class DrawerPage : BaseContentPage
	{
		private ObservableCollection<ItemMenu> menuItems;
		private NavigationManager navigation;
        private MasterPage _master;

        public DrawerPage(MasterPage master)
		{
			InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            _master = master;



            navigation = new NavigationManager();
			
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadMenu();
            MessagingCenter.Subscribe<HomePage>(this, "UpdateUserInfo", (sender) =>
            {
                _LabelNombre.Text = Helpers.UserHelper.CurrentUser().Nombre_del_Titular;
                LoadUserPhoto();
            });

            ImageSourceChanged = async () =>
            {
                if (LastView is FFImageLoading.Forms.CachedImage)
                    (LastView as FFImageLoading.Forms.CachedImage).Source = Source;

                _profileImage.Source = Source;

                await PostLastFoto();
            };

            ImagesUploaded += (folio) =>
            {
                UpdateUserPhoto(folio);
            };
        }

        async void UpdateUserPhoto(int folio)
        {
            try
            {
                DependencyService.Get<IProgress>().ShowProgress("Actualizando fotografía");

                var user = Helpers.UserHelper.CurrentUser();
                user.Foto_de_Perfil = folio;
                Helpers.UserHelper.UpdateUser(user);

                var resp = await App.CurrentApp.Services.PutObjectToTable<User>(user, user.Folio + "", User.TABLE_NAME);

                DependencyService.Get<IProgress>().Dismiss();

                if (resp != -1)
                {
                    await DisplayAlert("", "Fotografía actualizada correctamente", "Ok");
                }
                else
                {
                    await DisplayAlert("", "Hubo un problema al actualizar la fotografía", "Ok");
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IProgress>().Dismiss();

                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        void LoadMenu()
		{
            var currentUser = Helpers.UserHelper.CurrentUser();
            _LabelNombre.Text = currentUser.Nombre_del_Titular;

            if (Helpers.UserHelper.CurrentUser().Foto_de_Perfil != -1)
            {
                LoadUserPhoto();
            }

			menuItems = new ObservableCollection<ItemMenu>();
			string[] titles = new string[] { "Inicio", "Directorio", "Mis dependientes", "¿Necesitas ayuda?", "Salir" };
            string[] icons = new string[] { "home.png", "book.png", "users.png", "logout.png", "logout.png" };
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

                //navigation.NavigateMenu(item.Page);
                Page toPage = null;
                switch (item.Page)
                {
                    case ItemPageMenu.Directory:
                        toPage = new DirectoryPage();
                        Navigation.PushAsync(toPage);
                        break;
                    case ItemPageMenu.MyEarrings:
                        toPage = new DependtsPage();
                        Navigation.PushAsync(toPage);
                        break;
                    case ItemPageMenu.NeedHelp:
                        ShowNeedHelpInfo();
                        break;
                    case ItemPageMenu.Exit:
                        Helpers.UserHelper.Logout();
                        App.Master.ChangeRootPage(new LogInPage());
                        break;
                    case ItemPageMenu.Home:
                        App.Master.ChangeRootPage(new HomePage());
                        break;
                    default:
                        break;
                }

				foreach (var n in menuItems) n.Selected = false;
				item.Selected = true;
				ListMenu.ItemsSource = new ObservableCollection<ItemMenu>(menuItems);
				ListMenu.SelectedItem = null;

                _master.IsPresented = false;
			};
		}

        void ShowNeedHelpInfo()
        {
            try
            {
                Device.OpenUri(new Uri("whatsapp://send?phone=5218110056739&text="));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        async void LoadUserPhoto()
        {
            try
            {
                var userImage = await App.CurrentApp.Services.GetImage((int) Helpers.UserHelper.CurrentUser().Foto_de_Perfil);
                _profileImage.Source = userImage;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        async void ChangePicture(object sender, EventArgs e)
        {
            if (Helpers.UserHelper.CurrentUser().Foto_de_Perfil == -1)
            {
                TakePictureActionSheet(_profileImage);
            }
        }
    }
}
