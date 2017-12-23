using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ColorItAll
{
	public partial class MainPage : ContentPage
	{
	    public static ObservableCollection<HighScore> HighScoreList = new ObservableCollection<HighScore>();
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
            HighScoreList.Add(new HighScore{Name = "Derp", Clicks = 12});
            HighScoreList.Add(new HighScore { Name = "Herp", Clicks = 13 });
            HighScoreList.Add(new HighScore { Name = "Flerp", Clicks = 14 });
            await Navigation.PushAsync(new HighScorePage());
        }
    }
}
