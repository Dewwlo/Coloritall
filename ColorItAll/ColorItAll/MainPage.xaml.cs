using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ColorItAll.Data;
using Java.IO;
using Xamarin.Forms;
using Console = System.Console;

namespace ColorItAll
{
	public partial class MainPage : ContentPage
	{
        public MainPage()
		{
			InitializeComponent();
            var highScore = new ToolbarItem { Text = "Highscore" };
            highScore.Clicked += ShowHighScore;
            ToolbarItems.Add(highScore);
        }

	    private async void Play_OnClicked(object sender, EventArgs e)
	    {
            var button = (Button)sender;
	        await Navigation.PushAsync(new GamePage(DictionaryTranslater.GameMode[button.AutomationId]));
	    }

        private async void ShowHighScore(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HighScoreMenuPage());
        }

	    private async void RandomFamousQuote_OnClicked(object sender, EventArgs e)
	    {
            var manager = new NasaApiManager();
            var quote = await manager.GetFamousQuote();
	        if (quote != null)
	        {
	            await DisplayAlert("Author: " + quote.Author, quote.Quote, "Ok");
            }
        }
	}
}
