using App10.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App10
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeachersPage : ContentPage
    {
        public TeachersPage()
        {
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            
            var teacher = (Teacher)e.Item;

            await Xamarin.Essentials.TextToSpeech.SpeakAsync($"Teacher: {teacher.Id}, {teacher.FirstName} {teacher.LastName}");

            await Xamarin.Essentials.Share.RequestAsync($"Teacher: {teacher.Id}, {teacher.FirstName} {teacher.LastName}");

            if (await DisplayAlert("Czy chcesz edytować nauczyciela?", $"Teacher: {teacher.Id}, {teacher.FirstName} {teacher.LastName}", "TAK", "NIE"))
            {
                await Navigation.PushAsync(new Views.TeacherDetailsPage(teacher));
            }
            else
            {
                await Navigation.PushAsync(new StudentsPage(teacher));
            }

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshList();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var firstName = entryFirstName.Text;
            var lastName = entryLastName.Text;

            var teacher = new Teacher()
            {
                FirstName = firstName,
                LastName = lastName
            };

            await App.LocalDB.SaveItemAsync(teacher);
            await RefreshList();
        }

        private async Task RefreshList()
        {
            var teachers = await App.LocalDB.GetItemsAsync<Teacher>();
            lvTeachers.ItemsSource = teachers;
        }
    }
}
