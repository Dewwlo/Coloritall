﻿using System;
using System.Collections.Generic;
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
        private ToolbarItem _clickCounterToolbar = new ToolbarItem();
	    private int _clickCounter = 0;
        
		public GamePage (int gridSize)
		{
			InitializeComponent ();

            ToolbarItems.Add(_clickCounterToolbar);
		    GenerateGame(gridSize);
		}

	    private void GenerateGame(int gridSize)
	    {
	        var gameGrid = new Grid();
	        var createdButtons = 0;
	        _clickCounterToolbar.Text = "0";

            for (int i = 0; i < gridSize; i++)
	        {
	            gameGrid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Star});
	            gameGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Star});
	        }

            for (int vertical = 0; vertical < gridSize; vertical++)
            {
                for (int horizontal = 0; horizontal < gridSize; horizontal++)
                {
                    var button = new Button { BackgroundColor = (createdButtons % 2 == 0) ? Color.Blue : Color.Red, AutomationId = $"{vertical};{horizontal}"};
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

            button.BackgroundColor = buttonColor == Color.Red ? Color.Blue : Color.Red;
	        ChangeNeighbourButtonColor(button.AutomationId);
	        _clickCounter++;
	        _clickCounterToolbar.Text = $"{_clickCounter} Clicks";
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
            neighbourButtons.ForEach(b => b.BackgroundColor = b.BackgroundColor == Color.Red ? Color.Blue : Color.Red);

	        //if (IsVicotryConditionMet())

	            //var name = new PromptConfig
	            //{
	            //    InputType = InputType.Name
	            //};
	    }

	    private bool IsVicotryConditionMet()
	    {
	        return _buttonList.All(b => b.BackgroundColor == Color.Red) || _buttonList.All(b => b.BackgroundColor == Color.Blue);
	    }
	}
}