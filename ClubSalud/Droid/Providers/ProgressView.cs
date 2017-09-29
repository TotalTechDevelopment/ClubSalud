using System;
using ClubSalud.Providers;
using Xamarin.Forms;
using ClubSalud.Droid.Providers;
using AndroidHUD;

[assembly: Dependency(typeof(ProgressView))]
namespace ClubSalud.Droid.Providers
{
	public class ProgressView : IProgress
	{
		bool show = false;

		public void ShowProgress(string text)
		{
			if (!show)
			{
				show = true;
				AndHUD.Shared.Show(Forms.Context, text, -1, MaskType.Black);
			}
		}

		public void Dismiss()
		{
			show = false;
			AndHUD.Shared.Dismiss(Forms.Context);
		}
	}
}
