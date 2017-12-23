using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ColorItAll
{
	public partial class App : Application
	{
	    public static ObservableCollection<HighScore> HighScoreList = new ObservableCollection<HighScore>();
        public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainPage());
		}

		protected override void OnStart ()
		{
            var getHighScore = "";

            try
            {
                    getHighScore = DependencyService.Get<ISaveAndLoad>().LoadText("highscore.txt");
            }
            catch (Exception e)
            {

            }

            if (getHighScore == String.Empty)
                DependencyService.Get<ISaveAndLoad>().SaveText("highscore.txt", JsonConvert.SerializeObject(HighScoreList));
            else
                HighScoreList = JsonConvert.DeserializeObject<ObservableCollection<HighScore>>(getHighScore);
        }

        protected override void OnSleep ()
		{
            DependencyService.Get<ISaveAndLoad>().SaveText("highscore.txt", JsonConvert.SerializeObject(HighScoreList));
        }

		protected override void OnResume ()
		{
            var getHighScore = DependencyService.Get<ISaveAndLoad>().LoadText("highscore.txt");
            HighScoreList = JsonConvert.DeserializeObject<ObservableCollection<HighScore>>(getHighScore);
        }
	}
}
