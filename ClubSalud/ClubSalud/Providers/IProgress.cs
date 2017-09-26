using System;
namespace ClubSalud.Providers
{
	public interface IProgress
	{
		void ShowProgress(string text);

		void Dismiss();
	}
}
