using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App10
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TicTacToePage : ContentPage
	{
        private string _currentSymbol;
        private const string Circle = "O";
        private const string Cross = "X";
        private const int GameDimension = 3;

        List<List<string>> Buttons { get; set; }

        public TicTacToePage ()
		{
            _currentSymbol = Circle;
			InitializeComponent ();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            btn.Text = _currentSymbol;

            if (DoWeHaveAWinner())
            {
                await DisplayAlert("Gratulacje", $"Wygrywa {_currentSymbol}", "OK");
            }

            //_currentSymbol = _currentSymbol == Circle ? Cross : Circle;

            if (_currentSymbol == Circle)
            {
                _currentSymbol = Cross;
            }
            else
            {
                _currentSymbol = Circle;
            }

            btn.IsEnabled = false;
            lblMove.Text = $"Ruch: {_currentSymbol}";
        }

        private bool DoWeHaveAWinnerAlt()
        {
            var grid = Content as Grid;
            var btns = grid.Children.Where(x => x.GetType() == typeof(Button)).Take(9).ToList();
            Buttons = new List<List<string>>();

            Buttons.Add(new List<string>()
            {
                ((Button)btns[0]).Text,
                ((Button)btns[1]).Text,
                ((Button)btns[2]).Text
            });

            Buttons.Add(new List<string>()
            {
                ((Button)btns[3]).Text,
                ((Button)btns[4]).Text,
                ((Button)btns[5]).Text
            });

            Buttons.Add(new List<string>()
            {
                ((Button)btns[6]).Text,
                ((Button)btns[7]).Text,
                ((Button)btns[8]).Text
            });

            for (int i = 0; i < GameDimension; i++)
            {
                if (!Buttons[i].Any(b => b != Buttons[i][0]) && !string.IsNullOrWhiteSpace(Buttons[i][0]))
                {
                    return true;
                }

                if (!Buttons.Any(b => b[i] != Buttons[0][i]) && !string.IsNullOrWhiteSpace(Buttons[0][i]))
                {
                    return true;
                }
            }
            
            return false;
        }
        private bool DoWeHaveAWinner()
        {
            var grid = Content as Grid;
            var btns = grid.Children.Where(c => c.GetType() == typeof(Button)).Take(9).ToList();

            bool isRowChecked = CheckOne(btns, 0, 1, 2) || CheckOne(btns, 3, 4, 5) || CheckOne(btns, 6, 7, 8);
            bool isColumnChecked = CheckOne(btns, 0, 3, 6) || CheckOne(btns, 1, 4, 7) || CheckOne(btns, 2, 5, 8);
            bool isDiagonalChecked = CheckOne(btns, 0, 4, 8) || CheckOne(btns, 2, 4, 6);

            return isColumnChecked || isRowChecked || isDiagonalChecked;
        }

        private bool CheckOne(List<View> btns, int b1, int b2, int b3)
        {
            return !string.IsNullOrWhiteSpace(((Button)btns[b1]).Text)
                && ((Button)btns[b1]).Text == ((Button)btns[b2]).Text
                && ((Button)btns[b2]).Text == ((Button)btns[b3]).Text;
        }
    }
}