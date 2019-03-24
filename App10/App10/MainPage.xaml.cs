using System;
using System.Collections.Generic;
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

        private void Button_Clicked(object sender, EventArgs e)
        {
            var nazwa = sender as Button;
            var nazwa2 = (Button)sender;
            nazwa.IsVisible = false;
        }
    }
}
