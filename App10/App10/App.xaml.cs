using App10.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace App10
{
    public partial class App : Application
    {
        private const string LastRunKey = "lastRun";
        public static DateTime? LastRunDate { get; set; } = null;

        private static LocalDatabase localDB;
        public static LocalDatabase LocalDB
        {
            get
            {
                if (localDB == null)
                {
                    var fileHelper = DependencyService.Get<IFileHelper>();
                    var path = fileHelper.GetLocalFilepath("app.database");
                    localDB = new Data.LocalDatabase(path);
                }

                return localDB;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new TicTacToePage());
        }

        protected override async void OnStart()
        {
            if (Data.Properties.AppProperties.ContainsKey(LastRunKey))
            {
                LastRunDate = (DateTime)Data.Properties.AppProperties[LastRunKey];
            }

            Data.Properties.AppProperties[LastRunKey] = DateTime.Now;
            await SavePropertiesAsync();
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
