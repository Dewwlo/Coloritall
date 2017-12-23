using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ColorItAll
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HighScorePage : ContentPage
	{
		public HighScorePage ()
		{
			InitializeComponent ();
		    HighScoreList.ItemsSource = MainPage.HighScoreList.OrderBy(h => h.Clicks);
		}
	}
}