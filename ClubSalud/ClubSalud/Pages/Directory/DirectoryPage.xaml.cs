using ClubSalud.Providers;
using Xamarin.Forms;

namespace ClubSalud.Pages.Directory
{
	public partial class DirectoryPage : ContentPage
	{
        private readonly IProgress progress;
        private bool isProgress;
		public DirectoryPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
            progress = DependencyService.Get<IProgress>();
        }

        void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if(!isProgress)
            {
                progress.ShowProgress("Cargando");
                isProgress = true;
            } 
        }

        void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (isProgress)
            {
                progress.Dismiss();
            }
        }
    }
}
