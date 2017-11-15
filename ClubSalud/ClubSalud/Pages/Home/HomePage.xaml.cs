using System;
using System.Collections.ObjectModel;
using ClubSalud.Managers;
using ClubSalud.Models.Home;
using Xamarin.Forms;
using ClubSalud.Models.Menu;
using FFImageLoading.Forms;
using FFImageLoading.Transformations;
using ClubSalud.Providers;
using System.Collections.Generic;
using System.Linq;
using ClubSalud.Models.ClubSalud;
using ClubSalud.Pages.Home;
using ClubSalud.Utils;

namespace ClubSalud
{
    public partial class HomePage : BaseContentPage
    {

        private ObservableCollection<Dependent> Dependents;
        private NavigationManager navigation;

        private List<DetalleDeDependientesDeUsuario> ListaDependientes { set; get; }

        public HomePage()
        {
            InitializeComponent();

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

            NavigationPage.SetHasNavigationBar(this, false);
            navigation = new NavigationManager();
            PopulatingProfile();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            GetDependents();
        }

        async void GetDependents()
        {
            try
            {
                DependencyService.Get<IProgress>().ShowProgress("Actualizando información");

                string where = "Detalle_de_Dependientes_de_Usuario.Usuario='" + Helpers.UserHelper.CurrentUser().Folio + "'";
                var resp = await App.CurrentApp.Services.ListaSelAll<DependientePagingModel>(DetalleDeDependientesDeUsuario.TABLE_NAME, 0, 1000, where);


                if (resp != null && resp.Detalle_de_Dependientes_de_Usuarios.Count > 0)
                {
                    _ListDepents.Children.Clear();
                    if (ListaDependientes != null)
                    {
                        ListaDependientes.Clear();
                    }
                    ListaDependientes = resp.Detalle_de_Dependientes_de_Usuarios;
                    if (Helpers.DependentHelper.ListaDependientes != null)
                    {
                        Helpers.DependentHelper.ListaDependientes.Clear();
                    }
                    Helpers.DependentHelper.ListaDependientes = ListaDependientes;

                    PopulatingDependent();
                }

                DependencyService.Get<IProgress>().Dismiss();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                DependencyService.Get<IProgress>().Dismiss();
            }
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
                    MessagingCenter.Send<HomePage>(this, "UpdateUserInfo");

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

        async void PopulatingProfile()
        {
            _Name.Text = App.CurrentUser.Nombre_del_Titular;
            var lastNameP = AppViewUtils.RemoveWhiteSpaces(App.CurrentUser.Apellido_Paterno_del_Titular);
            var lastNameM = AppViewUtils.RemoveWhiteSpaces(App.CurrentUser.Apellido_Materno_del_Titular);
            _LastName.Text = lastNameP + " " + lastNameM;
            _Member.Text = App.CurrentUser.Numero_de_Seguro;
            _Vigencia.Text = App.CurrentUser.VigenciaFormatted;

            var image = "";
            if (Helpers.UserHelper.CurrentUser().Foto_de_Perfil != -1)
            {
                image = await  App.CurrentApp.Services.GetImage((int) Helpers.UserHelper.CurrentUser().Foto_de_Perfil);
                _profileImage.Source = image;
            }
        }

        async void PopulatingDependent()
        {
            StackLayout[] layouts = new StackLayout[ListaDependientes.Count];
            TapGestureRecognizer tapChangePicture = new TapGestureRecognizer();
            tapChangePicture.Tapped += TapedChangePicture;
            for (int i = 0; i < ListaDependientes.Count; i++)
            {
                var dependiente = ListaDependientes[i];
                var userImage = "";
                if (dependiente.Foto != null)
                {
                    userImage = await App.CurrentApp.Services.GetImage((int) dependiente.Foto);
                    ListaDependientes.Where(x => x.Clave.Equals(dependiente.Clave)).FirstOrDefault().FotoUrl = userImage;
                }
                else
                {
                    ListaDependientes.Where(x => x.Clave.Equals(dependiente.Clave)).FirstOrDefault().FotoUrl = "no_image.png";
                }

                var image = new CachedImage()
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 50,
                    HeightRequest = 50,
                    CacheDuration = TimeSpan.FromDays(30),
                    DownsampleToViewSize = true,
                    TransparencyEnabled = false,
                    //ErrorPlaceholder = string.IsNullOrEmpty(userImage)? "no_image.png" : userImage,
                    Source = !string.IsNullOrEmpty(userImage) ? userImage : "no_image.png"
                };
                image.Transformations.Add(new CircleTransformation());

                layouts[i] = new StackLayout();
                layouts[i].HorizontalOptions = LayoutOptions.CenterAndExpand;
                layouts[i].Spacing = 1;
                layouts[i].Children.Add(image);
                layouts[i].Children.Add(new Label() { Text = dependiente.Nombre, FontSize = 8, HorizontalOptions = LayoutOptions.CenterAndExpand });
                layouts[i].GestureRecognizers.Add(tapChangePicture);
                _ListDepents.Children.Add(layouts[i]);
            }
        }

        void TapedChangePicture(object sender, EventArgs e)
        {
            try
            {
				var stackItem = (StackLayout)sender;
				var textItem = (Label)stackItem.Children[1];
				var dependentName = textItem.Text;

				var dependent = ListaDependientes.Where(x => x.Nombre.Equals(dependentName)).FirstOrDefault();

				if (dependent != null)
				{
                    Helpers.DependentHelper.CurrentDependent = dependent;
                    var dependetPosition = ListaDependientes.IndexOf(dependent);
                    Helpers.DependentHelper.CurrentDependentPosition = dependetPosition;

                    //navigation.NavigatePages(ItemPage.ProfileDependent);
                    App.Master.Navigation.PushAsync(new ProfileDependetPage());
				}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }

        async void ChangePicture(object sender, EventArgs e)
        {
    //        if (Helpers.UserHelper.CurrentUser().Foto_de_Perfil == -1)
    //        {
				TakePictureActionSheet(_profileImage);
            //}
        }
    }
}