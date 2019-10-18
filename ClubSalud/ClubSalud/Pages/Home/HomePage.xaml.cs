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
using ClubSalud.Helpers;
using System.Threading.Tasks;
using ClubSalud.Pages.Master;
using ClubSalud.Pages.Session;
using ClubSalud.DB;
using Xamarin.Essentials;

namespace ClubSalud
{
    public partial class HomePage : BaseContentPage
    {

        private ObservableCollection<Dependent> Dependents = new ObservableCollection<Dependent>();
        private NavigationManager navigation;

        private List<DetalleDeDependientesDeUsuario> ListaDependientes { set; get; } = new List<DetalleDeDependientesDeUsuario>();

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
            MessagingCenter.Subscribe<DrawerPage>(this, "UpdateUserInfoHome",(sender)=> {
                LoadUserPhoto();
            });
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            VersionTracking.Track();
            await ValidateStatusUser();
            var user = UserHelper.CurrentUser();
            if (user != null)
            {
                if(user.Estatus == 2)
                {
                    UserHelper.Logout();
                    await DisplayAlert("Alerta", "La Vigencia de tu Membresía ha vencido.\nPara Renovación puedes contactar a tu Asesor o llamar a Club Salud Familiar a los teléfonos 13667893 y 94.", "Ok");
                    Application.Current.MainPage = new LogInPage();
                }
                else if(user.Estatus == 1)
                {
                    await PopulatingProfile();
                    await GetDependents();
                }
            }
        }
        ClubSaludDatabase dbConnection = new ClubSaludDatabase();
        public async Task ValidateStatusUser()
        {
            DependencyService.Get<IProgress>().ShowProgress("Cargando");
            var user = Helpers.UserHelper.CurrentUser();
            if (user != null)
            {
                
                string where = "Clave_de_Acceso='" + user.Clave_de_Acceso + "' ";
                var res = await App.CurrentApp.Services.ListaSelAll<UserPagingModel>(User.TABLE_NAME, 0, 1, where);
                if (res.Registro_de_Usuarios != null && res.RowCount > 0)
                {
                    var u = res.Registro_de_Usuarios[0];
                    u.EmpresaNombre = u.Empresa_Registro_de_Empresa != null ? u.Empresa_Registro_de_Empresa.Nombre : "";
                    dbConnection.UpdateUser(u);
                }
            }
            DependencyService.Get<IProgress>().Dismiss();
        }

        async Task GetDependents()
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

                    await PopulatingDependent();
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
                    LoadUserPhoto();
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

        async void LoadUserPhoto()
        {
            try
            {
                var userImage = await App.CurrentApp.Services.GetImage((int)Helpers.UserHelper.CurrentUser().Foto_de_Perfil);
                _profileImage.Source = userImage;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        private bool existImage;
        async Task PopulatingProfile()
        {
            var user = Helpers.UserHelper.CurrentUser();
            _Name.Text = App.CurrentUser.Nombre;
            string lastNameP = string.Empty;
            string lastNameM = string.Empty;
            if (!string.IsNullOrEmpty(App.CurrentUser.Apellido_Paterno))
            {
                lastNameP = AppViewUtils.RemoveWhiteSpaces(App.CurrentUser.Apellido_Paterno);
            }
            if(!string.IsNullOrEmpty(App.CurrentUser.Apellido_Materno))
            {
                lastNameM = AppViewUtils.RemoveWhiteSpaces(App.CurrentUser.Apellido_Materno);
            }
            
            _LastName.Text = lastNameP + " " + lastNameM;
            _Member.Text = App.CurrentUser.Numero_de_Seguro;
            _Vigencia.Text = App.CurrentUser.VigenciaFormatted;
            _Empresa.Text = App.CurrentUser.EmpresaNombre;
            var date = DateTime.Now;
            _LabelFecha.Text = date.DayOfWeek + ", " + date.Day + " de " + date.ToString("MMMM") + " del " + date.Year;
            var image = "";
            if (Helpers.UserHelper.CurrentUser().Foto_de_Perfil != -1 && Helpers.UserHelper.CurrentUser().Foto_de_Perfil != null)
            {
                image = await  App.CurrentApp.Services.GetImage((int) Helpers.UserHelper.CurrentUser().Foto_de_Perfil);
                _profileImage.Source = image;
                existImage = true;
            }
            else
            {
                _profileImage.Source = "no_image.png";
            }
        }

        async Task PopulatingDependent()
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
            if (existImage)
            {
                await DisplayAlert("Alerta", "No es posible reemplazar la fotografía, si necesitas cambiarla \ndebes acceder al menú principal y dar click en la opción “¿Necesitas ayuda?”", "Ok");
                return;
            }
            var n = await DisplayActionSheet("Elige una imagen", "cancelar", null, new string[] { "Cámara", "Galería" });
            switch (n)
            {
                case "Cámara":
                    var photo = await TakePhotoMedia();
                    if(photo != null)
                    {
                        var post = await PostPhoto(photo);
                        if(post != -1)
                        {
                            UpdateUserPhoto(post);
                        }
                        else
                        {
                            await DisplayAlert("", "Hubo un problema al actualizar la fotografía", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("", "Hubo un problema al actualizar la fotografía", "Ok");
                    }
                    break;
                case "Galería":
                    var photoGalery = await GetPhotoGaleryMedia();
                    if (photoGalery != null)
                    {
                        var post = await PostPhoto(photoGalery);
                        if (post != -1)
                        {
                            UpdateUserPhoto(post);
                        }
                        else
                        {
                            await DisplayAlert("", "Hubo un problema al actualizar la fotografía", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("", "Hubo un problema al actualizar la fotografía", "Ok");
                    }
                    break;
            }
            
        }
    }
}