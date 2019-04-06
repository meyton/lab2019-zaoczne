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
	public partial class HttpPage : ContentPage
	{
        private string _url;

		public HttpPage(string url)
		{
            _url = url;
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_url);
                await DisplayAlert("PING", $"Strona zwraca {response.StatusCode}", "OK");
            }
        }
    }
}