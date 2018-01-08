using System;
using System.Collections.ObjectModel;
using ColorItAll.Data;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
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
            AppCenter.Start("android=bb3170c4-006b-4c90-9b7b-2b734a476a31;" + "uwp={Your UWP App secret here};" +
                            "ios={Your iOS App secret here}",
                typeof(Analytics), typeof(Crashes));

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

        }

		protected override void OnResume ()
		{
            var getHighScore = DependencyService.Get<ISaveAndLoad>().LoadText("highscore.txt");
            HighScoreList = JsonConvert.DeserializeObject<ObservableCollection<HighScore>>(getHighScore);
        }
	}
}
