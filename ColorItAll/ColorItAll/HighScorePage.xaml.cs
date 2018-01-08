using System.Linq;
using Xamarin.Forms;
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