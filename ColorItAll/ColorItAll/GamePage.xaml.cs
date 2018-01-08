using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Acr.UserDialogs;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ColorItAll
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GamePage : ContentPage
	{
	    private readonly List<Button> _buttonList = new List<Button>();
        private readonly ToolbarItem _clickCounterToolbar = new ToolbarItem();
	    private readonly int _gridSize;
	    private int _clickCounter = 0;
        
		public GamePage (int gridSize)
		{
			InitializeComponent ();

		    _gridSize = gridSize;
            ToolbarItems.Add(_clickCounterToolbar);
		    GenerateGame(gridSize);
		}

	    private void GenerateGame(int gridSize)
	    {
	        var gameGrid = new Grid{RowSpacing = 0, ColumnSpacing = 0, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center};
            var createdButtons = 0;
	        _clickCounterToolbar.Text = $"{_clickCounter} Clicks";

	        var ratio = Application.Current.MainPage.Height < Application.Current.MainPage.Width ? Application.Current.MainPage.Height / gridSize : Application.Current.MainPage.Width / gridSize;

            for (int i = 0; i < gridSize; i++)
	        {
	            gameGrid.RowDefinitions.Add(new RowDefinition {Height = ratio});
	            gameGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = ratio});
	        }

            for (int vertical = 0; vertical < gridSize; vertical++)
            {
                for (int horizontal = 0; horizontal < gridSize; horizontal++)
                {
                    var button = new Button { BackgroundColor = (createdButtons % 2 == 0) ? Color.DeepPink : Color.Lime, AutomationId = $"{vertical};{horizontal}"};
                    button.Clicked += ColorClicked;
                    _buttonList.Add(button);
                    gameGrid.Children.Add(button, vertical, horizontal);
                    createdButtons += 1;
                }
            }

            Content = gameGrid;
        }

	    private void ColorClicked(object sender, EventArgs e)
	    {
	        var button = (Button) sender;
	        var buttonColor = button.BackgroundColor;

            button.BackgroundColor = buttonColor == Color.Lime ? Color.DeepPink : Color.Lime;
	        _clickCounter++;
	        _clickCounterToolbar.Text = $"{_clickCounter} Clicks";
	        ChangeNeighbourButtonColor(button.AutomationId);
        }

	    private void ChangeNeighbourButtonColor(string buttonId)
	    {
	        var gridLocataion = Regex.Split(buttonId, ";");
	        var neighbourButtons = new List<Button>
	        {
	            _buttonList.SingleOrDefault(b => b.AutomationId == $"{int.Parse(gridLocataion[0])};{int.Parse(gridLocataion[1]) - 1}"),
	            _buttonList.SingleOrDefault(b => b.AutomationId == $"{int.Parse(gridLocataion[0])};{int.Parse(gridLocataion[1]) + 1}"),
	            _buttonList.SingleOrDefault(b => b.AutomationId == $"{int.Parse(gridLocataion[0]) + 1};{int.Parse(gridLocataion[1])}"),
	            _buttonList.SingleOrDefault(b => b.AutomationId == $"{int.Parse(gridLocataion[0]) - 1};{int.Parse(gridLocataion[1])}")
	        };
	        neighbourButtons.RemoveAll(item => item == null);
            neighbourButtons.ForEach(b => b.BackgroundColor = b.BackgroundColor == Color.Lime ? Color.DeepPink : Color.Lime);

            if (IsVicotryConditionMet())
                HandleVictoryModal();
	    }

	    private bool IsVicotryConditionMet()
	    {
	        return _buttonList.All(b => b.BackgroundColor == Color.Lime) || _buttonList.All(b => b.BackgroundColor == Color.DeepPink);
	    }

	    private void HandleVictoryModal()
	    {
	        var highScoreListDifficulty = App.HighScoreList.Where(h => h.Difficulty == GetGameDifficulty()).ToList();
	        if (highScoreListDifficulty.Count() < 10 || highScoreListDifficulty.Max(h => h.Clicks >= _clickCounter))
	            CreateInputVicotryModal();
	        else
	            CreateMessageVicotryModal();
        }

	    private async void CreateMessageVicotryModal()
	    {
	        await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
	        {
	            Title = $"Completed game with {_clickCounter} clicks."
            });

	        ResetGame();
        }

        private async void CreateInputVicotryModal()
	    {
	        var inputName = await UserDialogs.Instance.PromptAsync(new PromptConfig
	        {
	            InputType = InputType.Name,
	            IsCancellable = true,
	            Text = "Noname",
	            Title = $"Completed game with {_clickCounter} clicks. Enter a highscore name."
	        });

            CreateHighScore(inputName.Text);
            ResetGame();
	    }

	    private void CreateHighScore(string inputName = null)
	    {
	        App.HighScoreList.Add(inputName == string.Empty
	            ? new HighScore { Name = "Noname", Difficulty = GetGameDifficulty(), Clicks = _clickCounter }
	            : new HighScore { Name = inputName, Difficulty = GetGameDifficulty(), Clicks = _clickCounter });
	        UpdateHighScoreList();

	    }
	    private void UpdateHighScoreList()
	    {
            App.HighScoreList = new ObservableCollection<HighScore>(App.HighScoreList.OrderBy(h => h.Clicks).Take(10));
	    }

	    private string GetGameDifficulty()
	    {
	        return DictionaryTranslater.GameMode.FirstOrDefault(d => d.Value == _gridSize).Key;
        }

	    private void ResetGame()
	    {
	        _clickCounter = 0;
	        _buttonList.Clear();
            GenerateGame(_gridSize);
	    }
	}
}