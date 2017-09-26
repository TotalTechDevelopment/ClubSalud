using System;
using BigTed;
using ClubSalud.iOS.Providers;
using ClubSalud.Providers;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgressView))]
namespace ClubSalud.iOS.Providers
{
	public class ProgressView : IProgress
	{
		bool show = false;

		public void ShowProgress(string text)
		{
			if (!show)
			{
				show = true;
				BTProgressHUD.ForceiOS6LookAndFeel = true;
				BTProgressHUD.Show(text, -1, ProgressHUD.MaskType.Black);
			}
		}

		public void Dismiss()
		{
			show = false;
			BTProgressHUD.Dismiss();
		}
	}
}
