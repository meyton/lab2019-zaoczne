using App10.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace App10
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new TicTacToePage());
            //var fileHelper = DependencyService.Get<IFileHelper>();
            //var path = fileHelper.GetLocalFilepath("app.database");
            //var db = new Data.LocalDatabase(path);
        }
        
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
