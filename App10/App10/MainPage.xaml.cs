using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App10
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            lblOne.Text = "Zmieniliśmy po wejściu";
            stack.Children.Add(new Label()
            {
                TextColor = Color.Green,
                FontSize = 22.0,
                Text = "123"
            });
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var login = entryLogin?.Text;
            var pass = entryPassword?.Text;

            if (login == "admin" && pass == "admin123")
            {
                var nazwa = sender as Button;
                var nazwa2 = (Button)sender;
                nazwa.IsVisible = false;

                await Task.Delay(1000);

                await Navigation.PushAsync(new SecondPage("https://google.pl"));
            }
            else
            {
                lblValidation.IsVisible = true;
                entryLogin.BackgroundColor = Color.OrangeRed;
                entryPassword.BackgroundColor = Color.OrangeRed;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            btnLogin.IsVisible = true;
            lblValidation.IsVisible = false;
            entryLogin.BackgroundColor = Color.White;
            entryPassword.BackgroundColor = Color.White;
            entryLogin.Text = string.Empty;
            entryPassword.Text = string.Empty;
        }

        private void EntryLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;

            if (VerifyEntry(e.NewTextValue))
            {
                entry.BackgroundColor = Color.LightGreen;
            }
            else
            {
                entry.BackgroundColor = Color.LightGray;
            }

            btnLogin.IsEnabled = VerifyEntry(entryLogin.Text) && VerifyEntry(entryPassword.Text);
        }

        private bool VerifyEntry(string text)
        {
            return text != null && text.Length > 4;
        }

        private async void EntryUrl_Completed(object sender, EventArgs e)
        {
            if (IsUrlValid(entryUrl.Text))
            {
                if (await DisplayAlert("Czy na pewno?", $"Czy na pewno przejść na stronę {entryUrl.Text}?", "TAK", "NIE"))
                {
                    await Navigation.PushAsync(new HttpPage(entryUrl.Text));
                }
            }
            else
            {
                await DisplayAlert("Błąd!", "Podano błędny adres www", "OK");
            }
        }

        private void EntryUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsUrlValid(e.NewTextValue))
            {
                entryUrl.BackgroundColor = Color.LightGreen;
            }
            else
            {
                entryUrl.BackgroundColor = Color.LightGray;
            }
        }

        private bool IsUrlValid(string url)
        {
            return url.ToLower().StartsWith("https://");
        }
    }
}
