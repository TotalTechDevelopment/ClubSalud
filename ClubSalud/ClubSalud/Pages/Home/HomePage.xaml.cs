using System;
using System.Collections.ObjectModel;
using ClubSalud.Managers;
using ClubSalud.Models.Home;
using Xamarin.Forms;
using ClubSalud.Models.Menu;
using FFImageLoading.Forms;
using FFImageLoading.Transformations;

namespace ClubSalud
{
    public partial class HomePage : BaseContentPage
	{

		private ObservableCollection<Dependent> Dependents;
		private NavigationManager navigation;

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
                
			};

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
			StackLayout[] layouts = new StackLayout[5];
			TapGestureRecognizer tapChangePicture = new TapGestureRecognizer();
			tapChangePicture.Tapped += TapedChangePicture;
            for (int i = 0; i < App.CurrentApp.ListaDependientes.Count; i++)
			{
                var dependiente = App.CurrentApp.ListaDependientes[i];

                var image = new CachedImage()
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 50,
                    HeightRequest = 50,
                    CacheDuration = TimeSpan.FromDays(30),
                    DownsampleToViewSize = true,
                    TransparencyEnabled = false,
                    ErrorPlaceholder = "error.png",
                    Source = dependiente.Foto
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
			navigation.NavigatePages(ItemPage.ProfileDependent);
		}

		async void ChangePicture(object sender, EventArgs e)
		{
            TakePictureActionSheet(_profileImage);
		}
	}
}