using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

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
	        await Navigation.PushAsync(new GamePage(DictionaryTranslater.GameMode[button.ClassId]));
	    }

        private async void ShowHighScore(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HighScorePage());
        }
    }
}
