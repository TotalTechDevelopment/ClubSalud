using System;
using ClubSalud.Managers;
using Xamarin.Forms;

namespace ClubSalud.Bases
{
    public class ToolbarNavigateBasePage : Grid
    {
        private NavigationManager navigator;
        static Image _menuImage;

        public static readonly BindableProperty HomeButtonProperty =
            BindableProperty.Create("ShowHome", typeof(bool), typeof(ToolbarNavigateBasePage), true, propertyChanged: OnEventShowHomeChanged);

        public bool ShowHome
        {
            get
            {
                return (bool)GetValue(HomeButtonProperty);
            }
            set
            {
                SetValue(HomeButtonProperty, value);
            }
        }


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
            var menuImage = "";
            if (ShowHome == true)
            {
                menuImage = "menu.png";
            }
            else
            {
                menuImage = "left_white.png";
            }
            _menuImage = new Image()
            {
                Source = menuImage,
                WidthRequest = 60,
                HeightRequest = 50,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center
            };
            var tapMenu = new TapGestureRecognizer();
            tapMenu.Tapped += (sender, e) =>
            {
                if (ShowHome)
                {
                    App.Master.IsPresented = true;
                }
                else
                {
                    App.CurrentApp.MainPage.Navigation.PopAsync();
                }
            };
            _menuImage.GestureRecognizers.Add(tapMenu);
            Children.Add(_menuImage, 0, 0);

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
                WidthRequest = 60,
                HeightRequest = 50,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };
            var tapHome = new TapGestureRecognizer();
            tapHome.Tapped += (sender, e) =>
            {
                if (App.CurrentApp.MainPage.Navigation.NavigationStack.Count > 2)
                {
                    //App.CurrentApp.ChangeToRootPage();
                    App.CurrentApp.MainPage.Navigation.PopAsync();
                    MessagingCenter.Send(this, "ToMainPage");
                }
                else
                {
                    App.CurrentApp.MainPage.Navigation.PopAsync();
                }
            };
            home.GestureRecognizers.Add(tapHome);
            Children.Add(home, 2, 0);
        }

        static void OnEventShowHomeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            _menuImage.Source = "left_white.png";
        }
    }
}

