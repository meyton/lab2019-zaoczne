using App10.Model;
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
        private const string Draw = "draw";
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
                await SaveWin(_currentSymbol);
                await DisplayAlert("Gratulacje", $"Wygrywa {_currentSymbol}", "OK");
                Application.Current.MainPage = new NavigationPage(new TicTacToePage());
                return;
            }

            if (IsItDraw())
            {
                await SaveWin(Draw);
                await DisplayAlert("Remis", "Gra zakończona remisem", "OK");
                Application.Current.MainPage = new NavigationPage(new TicTacToePage());
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

        private bool IsItDraw()
        {
            var btns = GetButtons();
            if (btns.Any(b => string.IsNullOrEmpty(((Button)b).Text)))
            {
                return false;
            }

            return true;
        }

        private async Task SaveWin(string currentSymbol)
        {
            int score = 1;
            if (Data.Properties.AppProperties.ContainsKey(currentSymbol))
            {
                score += (int)Data.Properties.AppProperties[currentSymbol];
            }

            Data.Properties.AppProperties[currentSymbol] = score;
            await Application.Current.SavePropertiesAsync();
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
            var btns = GetButtons();
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Data.Properties.AppProperties.ContainsKey(Cross))
            {
                lblCross.Text = $"Krzyżyk: {Data.Properties.AppProperties[Cross]}";
            }

            if (Data.Properties.AppProperties.ContainsKey(Circle))
            {
                lblCircle.Text = $"Kółko: {Data.Properties.AppProperties[Circle]}";
            }

            if (Data.Properties.AppProperties.ContainsKey(Draw))
            {
                lblDraw.Text = $"Remis: {Data.Properties.AppProperties[Draw]}";
            }

            if (App.LastRunDate != null)
            {
                lblRun.Text = $"Ostatnie uruchomienie: {App.LastRunDate}";
            }

            var students = await App.LocalDB.GetItemsAsync<Student>();
            await DisplayAlert("Studenci", $"Liczba studentów w bazie: {students.Count}", "OK");
        }

        private List<View> GetButtons()
        {
            var grid = Content as Grid;
            return grid.Children.Where(c => c.GetType() == typeof(Button)).Take(9).ToList();
        }

        private async void ButtonReset_Clicked(object sender, EventArgs e)
        {
            await App.LocalDB.SaveItemAsync(new Student()
            {
                FirstName = "Marcin",
                LastName = "Wesel",
                Birthday = new DateTime(1989, 5, 19),
                Grade = 3
            });
            Application.Current.MainPage = new NavigationPage(new TicTacToePage());
        }
    }
}