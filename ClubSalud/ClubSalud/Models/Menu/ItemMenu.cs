using System;
namespace ClubSalud.Models.Menu
{
    public class ItemMenu
    {
		public string Title { get; set; }

		public string Icon { get; set; }

		public bool Selected { get; set; }

		public ItemPageMenu Page { get; set; }
    }
}
