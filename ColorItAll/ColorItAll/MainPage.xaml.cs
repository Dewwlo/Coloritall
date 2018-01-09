using System;
using Acr.UserDialogs;
using ColorItAll.Data;
using ColorItAll.Interface;
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

            
		    var tgr = new TapGestureRecognizer();
            tgr.Tapped += CallUs_OnClicked;
            CallUs.GestureRecognizers.Add(tgr);
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

	    async void CallUs_OnClicked(object sender, EventArgs e)
	    {
	        var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
	        {
	            Title = "Call us",
	            Message = "Are you sure?",
	            OkText = "Yes",
	            CancelText = "No"
	        });

            if (result) {
	            var dialer = DependencyService.Get<IDialer>();
	            if (dialer != null)
	            {
	                await dialer.DialAsync("00000000000");
	            }
	        }
	    }
    }
}
