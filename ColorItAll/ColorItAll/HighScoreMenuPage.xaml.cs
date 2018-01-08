﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ColorItAll
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HighScoreMenuPage : ContentPage
	{
		public HighScoreMenuPage()
		{
			InitializeComponent ();
		}

	    private async void HighScore_OnClicked(object sender, EventArgs e)
	    {
	        var button = (Button)sender;
            await Navigation.PushAsync(new HighScorePage(button.AutomationId));
        }
	}
}