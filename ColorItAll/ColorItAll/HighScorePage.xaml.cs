using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace ColorItAll
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HighScorePage : ContentPage
	{
		public HighScorePage (string difficulty)
		{
			InitializeComponent ();
		    HighScoreList.ItemsSource = App.HighScoreList.Where(h => h.Difficulty == difficulty).OrderBy(h => h.Clicks);
		}
	}
}