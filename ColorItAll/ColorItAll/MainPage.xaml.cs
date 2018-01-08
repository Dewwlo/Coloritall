using System;
using ColorItAll.Data;
using Xamarin.Forms;

namespace ColorItAll
{
    public partial class MainPage : ContentPage
	{
	    private readonly FamousQuoteManager _manager = new FamousQuoteManager();

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

	    private void RandomFamousQuote_OnClicked(object sender, EventArgs e)
	    {
            _manager.LoadQuotePrompt();
        }
	}
}
