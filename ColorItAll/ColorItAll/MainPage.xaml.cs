using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ColorItAll
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

	    private async void Play_OnClicked(object sender, EventArgs e)
	    {
            var button = (Button)sender;
	        await Navigation.PushAsync(new GamePage(DictionaryTranslater.GameMode[button.ClassId]));
	    }
	}
}
