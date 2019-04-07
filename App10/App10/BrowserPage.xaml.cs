using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App10
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BrowserPage : ContentPage
	{
		public BrowserPage ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(entryUrl.Text);
                if (response.IsSuccessStatusCode)
                {
                    wvWeb.Source = entryUrl.Text;
                }
                else
                {
                    await DisplayAlert("Błąd", "Strona źle odpowiada", "OK");
                }
            }
        }
    }
}